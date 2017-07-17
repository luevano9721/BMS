using BusManagementSystem._01Catalogos;
using BusManagementSystem.Catalogos;
using BusManagementSystem.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusManagementSystem.Activities
{
    public partial class Alerts_Log : System.Web.UI.Page
    {
        Users usr;
        Alert_Log initialAlertLog = new Alert_Log();
        Alert initialAlert = new Alert();
        //static bool search = false;
        //System_Error_Log error;
        String language = null;
        string msg_AlertInformation_Error;
        string msg_AlertInformation_NoPermission;
        string msg_AlertInformation_StatusChanged;
        string msg_AlertInformation_NoRecords;
        string msg_AlertInformation_addComment;
        static string alertID = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            usr = (Users)(Session["C_USER"]);


            //from language

            functions func = new functions();
            if (Session["Language"] != null)
            {
                language = Session["Language"].ToString();
            }
            else
            {
                language = functions.GetLanguage(usr);
            }

            func.languageTranslate(this.Master, language);

            translateControls(language);

            if (!this.IsPostBack)
            {

                if (usr.Profile.Equals("INTERNAL"))
                {
                    GridView_AlertsLog.Columns[12].Visible = true;
                }
                else
                {
                    GridView_AlertsLog.Columns[12].Visible = false;
                }

                fillVendorAdm();

                ViewState["searchStatus"] = false;
                ViewState["isSorting"] = false;
            }

            if ((bool)ViewState["searchStatus"] == false && (bool)ViewState["isSorting"] == false)
                fillAlertLogGrid(cbFilterStatus.SelectedValue.ToString(), cbVendorAdm.SelectedValue.ToString());




        }

        protected void fillVendorAdm()
        {
            Users usr = (Users)(Session["C_USER"]);

            string vendorFilter = usr.Vendor_ID.Equals("ALL") ? "" : "Where [Vendor_ID]= '" + usr.Vendor_ID + "'";

            Vendor initialVendor = new Vendor();

            DataTable dt = GenericClass.SQLSelectObj(initialVendor, WhereClause: string.IsNullOrWhiteSpace(vendorFilter) ? null : vendorFilter);

            DataView dvVendor = new DataView(dt);

            DataTable distincVendor = dvVendor.ToTable(true, "VENDOR_ID", "NAME");

            cbVendorAdm.Items.Clear();

            cbVendorAdm.DataSource = distincVendor;

            cbVendorAdm.DataValueField = "VENDOR_ID";

            cbVendorAdm.DataTextField = "NAME";

            cbVendorAdm.DataBind();

            if (usr.Vendor_ID.Equals("ALL"))
            {
                cbVendorAdm.Enabled = true;

                ListItem li = new ListItem("ALL", "ALL");

                cbVendorAdm.Items.Insert(0, li);
            }
            else
            {
                cbVendorAdm.Enabled = false;

            }
        }

        private void translateControls(string language)
        {
            if (language.Equals("Eng"))
            {
                cbFilterStatus.Items[0].Text = "Open";
                cbFilterStatus.Items[1].Text = "Working";
                cbFilterStatus.Items[2].Text = "Done";
                cbFilterStatus.Items[3].Text = "Close";
            }
            

            msg_AlertInformation_Error = (string)GetGlobalResourceObject(language, "msg_AlertInformation_Error");
            msg_AlertInformation_NoPermission = (string)GetGlobalResourceObject(language, "msg_AlertInformation_NoPermission");
            msg_AlertInformation_StatusChanged = (string)GetGlobalResourceObject(language, "msg_AlertInformation_StatusChanged");
            msg_AlertInformation_NoRecords = (string)GetGlobalResourceObject(language, "msg_AlertInformation_NoRecords");

            msg_AlertInformation_addComment = (string)GetGlobalResourceObject(language, "msg_AlertInformation_addComment");
        }

        private DataTable fillAlertLogGrid(string status, string vendor)
        {
            DataTable dtAlert = null;

            string vendorfilter;

            try
            {
                vendorfilter = vendor.Equals("ALL") ? "" : " And ([ALERT_LOG].Vendor_ID='" + vendor + "' or  [ALERT_LOG].[Vendor_ID]='ALL')";

                if (usr.Vendor_ID == "ALL")
                {
                    dtAlert = GenericClass.SQLSelectObj(initialAlertLog, mappingQueryName: "Alert-QueryLog", WhereClause: "Where Status='" + status + "'" + vendorfilter, OrderByClause: "Order By Alert_Date desc");
                }
                else if (!string.IsNullOrEmpty(usr.Vendor_ID))
                {

                    dtAlert = GenericClass.SQLSelectObj(initialAlertLog, mappingQueryName: "Alert-QueryLog", WhereClause: "Where Status='" + status + "' and ([ALERT_LOG].[Vendor_ID]='" + usr.Vendor_ID + "' or  [ALERT_LOG].[Vendor_ID]='ALL')", OrderByClause: "Order By Alert_Date desc");

                }
                GridView_AlertsLog.DataSource = dtAlert;
                ViewState["viewStateAlert"] = dtAlert;
                GridView_AlertsLog.DataBind();
                GridView_AlertsLog.PageSize = 15;
            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            return dtAlert;
        }

        private void fillFollowingGrid(int idAlert)
        {
            try
            {
                alertID = idAlert.ToString();
                gdFollowing.DataSource = null;
                Alert_Followup follow = new Alert_Followup();
                DataTable dtAlert = GenericClass.SQLSelectObj(follow, mappingQueryName: "AlertLog-query", WhereClause: "Where Alert_Log_ID=" + idAlert);
                gdFollowing.DataSource = dtAlert;
                // string p = dtAlert.Rows[0]["Comment_date"].ToString();
                gdFollowing.DataBind();
            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        protected void btAddComment_Click(object sender, EventArgs e)
        {
            try
            {
                Users usr = (Users)(Session["C_USER"]);
                Alert_Followup follow = new Alert_Followup();
                follow.Comment = tbFollowComments.Text;
                follow.User_id = usr.User_ID;
                follow.Comment_date = DateTime.Now;
                follow.Alert_log_id = Int32.Parse(alertID);

                GenericClass.SQLInsertObj(follow);
                fillFollowingGrid(follow.Alert_log_id);
                tbFollowComments.Text = string.Empty;

                functions.ShowMessage(this, this.GetType(), msg_AlertInformation_StatusChanged, MessageType.Success);

            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            //upFollowing.Update();
        }

        protected void btChangeStatus_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(idTakeActionSelected.Value))
            {
                changeStatusAlert(cbAlertStatus.SelectedValue.ToString(), Int32.Parse(idTakeActionSelected.Value));
                fillAlertLogGrid(cbFilterStatus.SelectedValue.ToString(), cbVendorAdm.SelectedValue.ToString());
            }
            else
            {
                functions.ShowMessage(this, this.GetType(), msg_AlertInformation_Error, MessageType.Warning);
            }

        }


        protected void btClose_Click(object sender, EventArgs e)
        {
            if (usr.Profile.Equals("INTERNAL"))
            {
                if (!string.IsNullOrEmpty(idEndAlertWindow.Value))
                {
                    changeStatusAlert(cbStatusClosed.SelectedValue.ToString(), Int32.Parse(idEndAlertWindow.Value));
                    fillAlertLogGrid(cbFilterStatus.SelectedValue.ToString(), cbVendorAdm.SelectedValue.ToString());
                }
                else
                {
                    functions.ShowMessage(this, this.GetType(), msg_AlertInformation_Error, MessageType.Warning);
                }
            }
            else
            {
                functions.ShowMessage(this, this.GetType(), msg_AlertInformation_NoPermission, MessageType.Warning);
            }
        }

        private void changeStatusAlert(string status, int alertID)
        {
            try
            {

                Alert_Log alertlog = new Alert_Log();

                Alert_Followup alertfollow = new Alert_Followup();
                alertlog.Status = status;
                alertlog.Closed_By = usr.User_ID;
                alertlog.Ticket_Close = DateTime.Now;
                Dictionary<string, dynamic> dicUpdAlert = new Dictionary<string, dynamic>();
                dicUpdAlert.Add("Status", "'" + alertlog.Status + "'");
                if (status == "CLOSED")
                {
                    dicUpdAlert.Add("Closed_By", "'" + alertlog.Closed_By + "'");
                    dicUpdAlert.Add("Ticket_Close", "'" + alertlog.Ticket_Close + "'");
                }
                GenericClass.SQLUpdateObj(alertlog, dicUpdAlert, "Where Alert_log_ID=" + alertID);

                alertfollow.Comment = "SYSTEM: Status has been changed to " + alertlog.Status;
                alertfollow.User_id = usr.User_ID;
                alertfollow.Comment_date = DateTime.Now;
                alertfollow.Alert_log_id = alertID;

                GenericClass.SQLInsertObj(alertfollow);
                functions.ShowMessage(this, this.GetType(), msg_AlertInformation_StatusChanged, MessageType.Success);
            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void GridView_AlertsLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lbStatus = e.Row.FindControl("lbStatus") as Label;
                LinkButton btEndAlert = e.Row.FindControl("btEndAlert") as LinkButton;
                LinkButton btTakeAction = e.Row.FindControl("btTakeAction") as LinkButton;
                // LinkButton btFollowing = e.Row.FindControl("btFollowing") as LinkButton;

                if (usr.Profile.Equals("INTERNAL"))
                { btEndAlert.Enabled = true; }
                // fillCombosCheckpoint2(cbcCheck2);
                if (lbStatus.Text == "CLOSED")
                {

                    e.Row.CssClass = "highlightRowGreen";
                    btEndAlert.Enabled = false;
                    btTakeAction.Enabled = false;
                    // btFollowing.Enabled = false;

                }
               
            }
        }

        protected void GridView_AlertsLog_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridView_AlertsLog.PageIndex = e.NewPageIndex;
            if ((bool)ViewState["searchStatus"] == true)
            { GridView_AlertsLog.DataSource = ViewState["searchResults"]; }
            if ((bool)ViewState["isSorting"] == true)
            { GridView_AlertsLog.DataSource = Session["AlertLogobjects"]; }
            GridView_AlertsLog.DataBind();
        }

        protected void btSearchCH1_Click(object sender, EventArgs e)
        {
            searchFunction();
        }

        private void searchFunction()
        {
            try
            {
                DataTable dtNew = new DataTable();
                string searchTerm = tbSearchCH1.Text.ToLower();
                //search = true;
                if (string.IsNullOrEmpty(searchTerm))
                {
                    fillAlertLogGrid(cbFilterStatus.SelectedValue.ToString(), cbVendorAdm.SelectedValue.ToString());
                    dtNew.Clear();
                    ViewState["searchStatus"] = false;
                    ViewState["isSorting"] = false;
                }
                else
                {
                    ViewState["searchStatus"] = true;
                    ViewState["isSorting"] = false;

                    //always check if the viewstate exists before using it
                    if (ViewState["viewStateAlert"] == null)
                        return;

                    //cast the viewstate as a datatable
                    DataTable dt = ViewState["viewStateAlert"] as DataTable;

                    //make a clone of the datatable
                    dtNew = dt.Clone();

                    //search the datatable for the correct fields
                    foreach (DataRow row in dt.Rows)
                    {
                        //add your own columns to be searched here
                        if (row["Code"].ToString().ToLower().Contains(searchTerm) || row["Description"].ToString().ToLower().Contains(searchTerm) || row["Bus_ID"].ToString().ToLower().Contains(searchTerm)
                            || row["Driver_ID"].ToString().ToLower().Contains(searchTerm) || row["Priority"].ToString().ToLower().Contains(searchTerm) || row["Status"].ToString().ToLower().Contains(searchTerm)
                            )
                        {
                            //when found copy the row to the cloned table
                            dtNew.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    GridView_AlertsLog.DataSource = dtNew;
                    ViewState["searchResults"] = dtNew;
                    GridView_AlertsLog.DataBind();
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btFollowing_Click(object sender, EventArgs e)
        {
            LinkButton btFollowing = sender as LinkButton;
            idFollowingSelected.Value = btFollowing.CommandArgument;
            fillFollowingGrid(Int32.Parse(idFollowingSelected.Value));
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "openWindow('followingWindow', 'Agregar Comentarios' , 700 , 580)", true);
        }

        protected void btTakeAction_Click(object sender, EventArgs e)
        {
            lbTakeActionStatus.Text = string.Empty;
            LinkButton btTakeAction = sender as LinkButton;
            idTakeActionSelected.Value = btTakeAction.CommandArgument;
            DataTable dtStatus = GenericClass.SQLSelectObj(initialAlertLog, WhereClause: "Where Alert_log_ID=" + Int32.Parse(idTakeActionSelected.Value));
            lbTakeActionStatus.Text = dtStatus.Rows[0]["Status"].ToString();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "openWindow('takeActionWindow', 'Tomar acción en alerta' , 300 , 320)", true);
        }

        protected void btEndAlert_Click(object sender, EventArgs e)
        {
            lbEndAlertWindow.Text = string.Empty;
            LinkButton btEndAlert = sender as LinkButton;
            idEndAlertWindow.Value = btEndAlert.CommandArgument;
            DataTable dtStatus = GenericClass.SQLSelectObj(initialAlertLog, WhereClause: "Where Alert_log_ID=" + Int32.Parse(idEndAlertWindow.Value));
            lbEndAlertWindow.Text = dtStatus.Rows[0]["Status"].ToString();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "openWindow('endAlertWindow', 'Terminar Alerta' , 300 , 320)", true);
        }

        protected void btMoreInfo_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarCampos();
                LinkButton btMoreInfo = sender as LinkButton;
                string alertId = btMoreInfo.CommandArgument;
                Alert_Log nlog = new Alert_Log();
                List<string> selectAlert = new List<string>();

                selectAlert.Add("Code");

                DataTable dtGetInfo = GenericClass.SQLSelectObj(nlog, mappingQueryName: "Alert-GetInfo", WhereClause: "WHERE Alert_log_ID=" + alertId, customSelect: selectAlert);
                if (dtGetInfo.Rows.Count > 0)
                {
                    lbAlertIDMI.Text = dtGetInfo.Rows[0]["Alert_log_ID"].ToString();
                    lbCodeMI.Text = dtGetInfo.Rows[0]["Code"].ToString();
                    lbDescriptionMI.Text = dtGetInfo.Rows[0]["Description"].ToString();
                    lbActionMI.Text = dtGetInfo.Rows[0]["Action"].ToString();
                    lbDateMI.Text = dtGetInfo.Rows[0]["Create_Date"].ToString();
                    lbDriverIDMI.Text = dtGetInfo.Rows[0]["Driver_ID"].ToString();
                    lbBusIDMI.Text = dtGetInfo.Rows[0]["Bus_ID"].ToString();
                    lbRissedMI.Text = dtGetInfo.Rows[0]["Rissed_By"].ToString();
                    lbCommentsMI.Text = dtGetInfo.Rows[0]["Comments"].ToString();
                    lbPriorityMI.Text = dtGetInfo.Rows[0]["Priority"].ToString();
                    lbStatusMI.Text = dtGetInfo.Rows[0]["Status"].ToString();
                    lbClosedMI.Text = dtGetInfo.Rows[0]["Closed_by"].ToString();
                    lbLastCheckMI.Text = dtGetInfo.Rows[0]["Last_Check"].ToString();
                    lbVendorIDMI.Text = dtGetInfo.Rows[0]["Vendor_ID"].ToString();

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "openWindow('moreInfoWindow', 'Detalle de alerta' , 500 , 500)", true);
                }
                else
                {
                    functions.ShowMessage(this, this.GetType(), msg_AlertInformation_NoRecords, MessageType.Error);
                }


            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        private void limpiarCampos()
        {
            lbAlertIDMI.Text = string.Empty;
            lbCodeMI.Text = string.Empty;
            lbDescriptionMI.Text = string.Empty;
            lbActionMI.Text = string.Empty;
            lbDateMI.Text = string.Empty;
            lbDriverIDMI.Text = string.Empty;
            lbBusIDMI.Text = string.Empty;
            lbRissedMI.Text = string.Empty;
            lbCommentsMI.Text = string.Empty;
            lbStatusMI.Text = string.Empty;
            lbLastCheckMI.Text = string.Empty;
            lbVendorIDMI.Text = string.Empty;
        }



        protected void GridView_AlertsLog_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                tbSearchCH1.Text = string.Empty;
                ViewState["isSorting"] = true;
                ViewState["searchStatus"] = false;

                string sortingDirection = string.Empty;
                if (direction == SortDirection.Ascending)
                {
                    direction = SortDirection.Descending;
                    sortingDirection = "Desc";
                }
                else
                {
                    direction = SortDirection.Ascending;
                    sortingDirection = "Asc";
                }
                DataTable dt = fillAlertLogGrid(cbFilterStatus.SelectedValue.ToString(), cbVendorAdm.SelectedValue.ToString());

                DataView sortedView = new DataView(dt);

                sortedView.Sort = e.SortExpression + " " + sortingDirection;
                Session["AlertLogobjects"] = sortedView;
                GridView_AlertsLog.DataSource = sortedView;
                GridView_AlertsLog.DataBind();

            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        public SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionState"];
            }
            set
            { ViewState["directionState"] = value; }
        }

        protected void btExcel_Click(object sender, EventArgs e)
        {
            execute_query();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.Redirect("~/Catalogos/ExportToExcel.aspx?btn=excel", false);
        }

        private void execute_query()
        {
            try
            {
                Session["Excel"] = null;
                Session["name"] = string.Empty;

                if (Session["C_USER"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                Users usr = (Users)(Session["C_USER"]);
                DataTable dtLoad = fillAlertLogGrid(cbFilterStatus.SelectedValue.ToString(), cbVendorAdm.SelectedValue.ToString());
                Session["Excel"] = dtLoad;
                Session["name"] = "AlertsSupport";
            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void cbFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //tbSearchCH1.Text = string.Empty;
            GridView_AlertsLog.PageIndex = 0;

            fillAlertLogGrid(cbFilterStatus.SelectedValue.ToString(), cbVendorAdm.SelectedValue.ToString());



        }

        protected void cbVendorAdm_SelectedIndexChanged(object sender, EventArgs e)
        {
            //tbSearchCH1.Text = string.Empty;
            GridView_AlertsLog.PageIndex = 0;

            fillAlertLogGrid(cbFilterStatus.SelectedValue.ToString(), cbVendorAdm.SelectedValue.ToString());
  
        }

        protected void GridView_AlertsLog_Sorted(object sender, EventArgs e)
        {

                tbSearchCH1.Text = string.Empty;
        }

        protected void btHelp_Click(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("./Login.aspx");
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Sopporte de alertas')", true);
        }

    }
}