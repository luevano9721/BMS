using BusManagementSystem.Class;
using System;
using Microsoft.Reporting.WebForms;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusManagementSystem._01Catalogos;
using System.Reflection;
using System.Runtime.Remoting;
using System.Data.SqlClient;
using System.Configuration;

namespace BusManagementSystem.Activities
{
    public partial class Activities_Awaiting : System.Web.UI.Page
    {
        List<string> columnList = new List<string>();
        string msg_ActivitiesAwait_ActivityID;
        string msg_ActivitiesAwait_Canceled;
        string msg_ActivitiesAwait_Processed;
        string msg_ActivitiesAwait_NotExecuted;
        string msg_ActivitiesAwait_Executed;
        String language = null;

        public DataTable generalDataTable(List<string> columnlist, string userID = "", string type = "", string from = "", string to = "", string status = "")
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            Users usr = (Users)(Session["C_USER"]);
            DataTable dtReturn = new DataTable();
            string statusFilter;
            if (status.Equals("P"))
            {
                statusFilter = "[Admin_Approve].[Admin_Confirm]= 0 and [Admin_Approve].[Confirm_Date] IS NULL ";
                if (usr.Rol_ID.Contains("ADMIN"))
                    GridView_AdminApprove.Columns[0].Visible = true;
            }
            else
            {
                statusFilter = "[Admin_Approve].[Admin_Confirm]=" + status + " and [Admin_Approve].[Confirm_Date] IS NOT NULL";
                GridView_AdminApprove.Columns[0].Visible = false;
            }

            string userFilter = string.IsNullOrWhiteSpace(userID) ? "" : " and [Admin_Approve].[User_ID]= '" + userID + "'";
            string typeFilter = string.IsNullOrWhiteSpace(type) ? "" : " and [Admin_Approve].[Type]= '" + type + "'";
            string dateFilter = string.IsNullOrWhiteSpace(from) | string.IsNullOrWhiteSpace(to) ? "" : " and [Admin_Approve].[Activity_Date] BETWEEN cast('" + FromDate.Text + "' as Datetime)" + " AND cast('" + ToDate.Text + "' as Datetime)+1";
            if (usr.Profile.Equals("INTERNAL"))
            {
                dtReturn = GenericClass.SQLSelectObj(new Admin_Approve(), columnlist,
                    WhereClause: " Where " + statusFilter + userFilter + typeFilter + dateFilter, mappingQueryName: "Activities",
                    OrderByClause: "Order by  [Admin_Approve].[Activity_Date] Desc ");
            }
            else
            {
                dtReturn = GenericClass.SQLSelectObj(new Admin_Approve(), columnlist, 
                    WhereClause: " Where [Admin_Approve].[User_ID]= '" + usr.User_ID + "' AND " + statusFilter + typeFilter + dateFilter,
                    mappingQueryName: "Activities",
                    OrderByClause: "Order by  [Admin_Approve].[Activity_Date] Desc ");

            }
            return dtReturn;
        }
        public void fillFiltersCB()
        {

            if (Session["C_USER"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            Users usr = (Users)(Session["C_USER"]);

            if (usr.Rol_ID.Contains("ADMIN"))
            {
               
                GridView_AdminApprove.Columns[0].Visible = true;

            }
            else
            {
                if (usr.Profile.Equals("INTERNAL"))
                {
                    cbVendor.Enabled = true;                    
                }
                else
                {
                    cbVendor.Enabled = false;
                }
             
                GridView_AdminApprove.Columns[0].Visible = false;
            }


            DataTable dt = new DataTable();
            dt = generalDataTable(columnList, from: FromDate.Text, to: ToDate.Text, status: cbStatus.SelectedValue);
            DataView dvVendor = new DataView(dt);
            DataTable distincVendor = dvVendor.ToTable(true, "RequestedBy");
            DataView dvType = new DataView(dt);
            DataTable distincType = dvVendor.ToTable(true, "Type");

            cbVendor.Items.Clear();
            cbVendor.DataSource = distincVendor;
            cbVendor.DataValueField = "RequestedBy";
            cbVendor.DataTextField = "RequestedBy";
            cbVendor.DataBind();

            cbType.Items.Clear();
            cbType.DataSource = distincType;
            cbType.DataValueField = "Type";
            cbType.DataTextField = "Type";
            cbType.DataBind();

            ListItem li = new ListItem("ALL", "ALL");
            cbVendor.Items.Insert(0, li);
            cbType.Items.Insert(0, li);

            cleanSearchAndSorting();

        }

        private void execute_query()
        {
            try
            {
                Session["Excel"] = null;
                Session["name"] = string.Empty;

                List<string> columnsList = new List<string>();
                columnsList.Add("Activity_Date");
                columnsList.Add("Activity_ID");
                columnList.Add("Admin_User");
                columnsList.Add("User_ID");
                columnsList.Add("Type");
                columnsList.Add("Module");
                columnsList.Add("Comments");

                Session["Excel"] = generalDataTable(columnsList, cbVendor.SelectedValue == "ALL" ? "" : cbVendor.SelectedValue, cbType.SelectedValue == "ALL" ? "" : cbType.SelectedValue, FromDate.Text, ToDate.Text, status: cbStatus.SelectedValue);
                Session["name"] = "Activities_Approve";

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }


        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {


                columnList.Add("Activity_Date");
                columnList.Add("Activity_ID");
                columnList.Add("Admin_User");
                columnList.Add("User_ID");
                columnList.Add("Type");
                columnList.Add("Module");
                columnList.Add("New_Values");
                columnList.Add("Where_Clause");
                columnList.Add("Comments");

                ToDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                FromDate.Text = DateTime.Now.AddDays(-30).ToString("MM/dd/yyyy");
                if (!this.IsPostBack)
                {

                    fillFiltersCB();

                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Users usr = (Users)(Session["C_USER"]);
            if (usr == null)
            {
                Response.Redirect("~/Login.aspx");
            }



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


            if (!this.IsPostBack)
            {
                ViewState["searchStatus"] = false;
                ViewState["isSorting"] = false;
            }

            if ((bool)ViewState["searchStatus"] == false && (bool)ViewState["isSorting"] == false )
                fillActAwaitGrid();

            translateControls(language);
        }

        private void cleanSearchAndSorting()
        {
            ViewState["searchStatus"] = false;
            ViewState["isSorting"] = false;
            tbSearchCH1.Text = string.Empty;

        }

        private void fillActAwaitGrid()
        {
            try
            {
                DataTable getData = generalDataTable(columnList, cbVendor.SelectedValue == "ALL" ? "" : cbVendor.SelectedValue, cbType.SelectedValue == "ALL" ? "" : cbType.SelectedValue, FromDate.Text, ToDate.Text, cbStatus.SelectedValue);
                GridView_AdminApprove.DataSource = getData;
                GridView_AdminApprove.PageSize = 15;
                ViewState["backupData"] = getData;
                GridView_AdminApprove.DataBind();

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void GridView_AdminApprove_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void translateControls(string language)
        {
            msg_ActivitiesAwait_ActivityID = (string)GetGlobalResourceObject(language, "msg_ActivitiesAwait_ActivityID");
            msg_ActivitiesAwait_Canceled = (string)GetGlobalResourceObject(language, "msg_ActivitiesAwait_Canceled");
            msg_ActivitiesAwait_Processed = (string)GetGlobalResourceObject(language, "msg_ActivitiesAwait_Processed");
            msg_ActivitiesAwait_NotExecuted = (string)GetGlobalResourceObject(language, "msg_ActivitiesAwait_NotExecuted");
            msg_ActivitiesAwait_Executed = (string)GetGlobalResourceObject(language, "msg_ActivitiesAwait_Executed");
        }

        protected void GridView_AdminApprove_Sorting(object sender, GridViewSortEventArgs e)
        {

            try
            {

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
                DataTable dt = generalDataTable(columnList, from: FromDate.Text, to: ToDate.Text, status: cbStatus.SelectedValue);

                DataView sortedView = new DataView(dt);
                sortedView.Sort = e.SortExpression + " " + sortingDirection;
                Session["Activitiesobjects"] = sortedView;
                GridView_AdminApprove.DataSource = sortedView;
                GridView_AdminApprove.DataBind();
            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void GridView_AdminApprove_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            ImageButton ViewBT = e.Row.FindControl("IB_View") as ImageButton;
            if (e.Row.Cells.Count > 7)
            {
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;
                string test = Context.Server.HtmlDecode(e.Row.Cells[8].Text);
                e.Row.Cells[8].Text = test;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[5].Text != "Master_Trip")
                {
                    ViewBT.Visible = false;

                }

            }


        }

        protected void GridView_AdminApprove_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_AdminApprove.PageIndex = e.NewPageIndex;
            if ((bool)ViewState["searchStatus"] == true)
            { GridView_AdminApprove.DataSource = ViewState["searchResults"]; }
            if ((bool)ViewState["isSorting"] == true)
            { GridView_AdminApprove.DataSource = Session["Activitiesobjects"]; }
            GridView_AdminApprove.DataBind();
        }




        protected void GridView_AdminApprove_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (Session["C_USER"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }

                Users usr = (Users)(Session["C_USER"]);
                GridView_AdminApprove.Rows[e.RowIndex].Cells[0].Enabled = false;
                Admin_Approve udpApprove = new Admin_Approve();
                List<string> ConfirmStatus = new List<string>();
                ConfirmStatus.Add("Confirm_Date");
                DataTable dt = GenericClass.SQLSelectObj(udpApprove, ConfirmStatus, WhereClause: "Where Activity_ID=" + GridView_AdminApprove.Rows[e.RowIndex].Cells[1].Text);
                if (string.IsNullOrWhiteSpace(dt.Rows[0]["Confirm_Date(M/D/Y)"].ToString()))
                {
                    udpApprove.Admin_Confirm = false;
                    udpApprove.Confirm_Date = DateTime.Now;
                    Dictionary<string, dynamic> dicUpdDriver = new Dictionary<string, dynamic>();
                    dicUpdDriver.Add("Admin_Confirm", false);
                    dicUpdDriver.Add("Admin_User", usr.User_ID);
                    dicUpdDriver.Add("Confirm_Date", "'" + DateTime.Now.ToString("MM/dd/yyyy") + "'");

                    if (usr.Rol_ID.Contains("ADMIN"))
                    {
                        //Update admin_confirm field to false for cancel the activity
                        GenericClass.SQLUpdateObj(udpApprove, dicUpdDriver, WhereClause: "Where Activity_ID=" + GridView_AdminApprove.Rows[e.RowIndex].Cells[1].Text);

                        //Get values from admin_approve table for rollback_action and rollback_value fields
                        DataTable dtAdminApprove = GenericClass.SQLSelectObj(udpApprove, WhereClause: "Where Activity_ID=" + GridView_AdminApprove.Rows[e.RowIndex].Cells[1].Text);
                        string rollback_Action = dtAdminApprove.Rows[0]["Rollback_Action"].ToString();
                        string rollback_Value = dtAdminApprove.Rows[0]["Rollback_Value"].ToString();

                        // if the  Rollback_action variable is not null or empty , then we have to execute query that is located in rollback_value field
                        if (!string.IsNullOrWhiteSpace(rollback_Action) && !string.IsNullOrWhiteSpace(rollback_Value))
                        {
                            //This method is for execute rollback action (if exists) when the activity is cancelled
                            executeRollback(rollback_Action, rollback_Value);
                        }

                        SendEmail.StatusActivity(GridView_AdminApprove.Rows[e.RowIndex].Cells[3].Text,
                        GridView_AdminApprove.Rows[e.RowIndex].Cells[1].Text,
                        GridView_AdminApprove.Rows[e.RowIndex].Cells[8].Text, usr.User_ID, "Canceló");

                        functions.ShowMessage(this, this.GetType(), msg_ActivitiesAwait_ActivityID + " " + GridView_AdminApprove.Rows[e.RowIndex].Cells[1].Text + " " + msg_ActivitiesAwait_Canceled, MessageType.Warning);
                        GridView_AdminApprove.DataSource = generalDataTable(columnList, from: FromDate.Text, to: ToDate.Text, status: cbStatus.SelectedValue);
                        //GenericClass.SQLSelectObj(new Admin_Approve(), columnList, WhereClause: " Where [Admin_Approve].[Admin_Confirm]= 0 and [Admin_Approve].[Confirm_Date] IS NULL and [Admin_Approve].[Activity_Date] BETWEEN cast('" + FromDate.Text + "' as Datetime)" + " AND cast('" + ToDate.Text + "' as Datetime)+1");
                        GridView_AdminApprove.PageSize = 15;
                        GridView_AdminApprove.DataBind();
                    }
                }
                else
                {
                    functions.ShowMessage(this, this.GetType(), msg_ActivitiesAwait_ActivityID + " " + GridView_AdminApprove.Rows[e.RowIndex].Cells[1].Text + " " + msg_ActivitiesAwait_Processed, MessageType.Warning);
                }

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        private void executeRollback(string rollback_action, string rollback_value)
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                switch (rollback_action)
                {
                    case "UPDATE":
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand(rollback_value, con))
                            {
                                cmd.CommandType = CommandType.Text;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        break;
                    default: functions.ShowMessage(this, this.GetType(), msg_ActivitiesAwait_NotExecuted, MessageType.Error);
                        break;
                }
            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }



        protected void GridView_AdminApprove_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            try
            {
                if (Session["C_USER"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }

                Users usr = (Users)(Session["C_USER"]);
                GridView_AdminApprove.Rows[e.RowIndex].Cells[0].Enabled = false;
                Admin_Approve udpApprove = new Admin_Approve();
                List<string> ConfirmStatus = new List<string>();
                ConfirmStatus.Add("Confirm_Date");
                DataTable dt = GenericClass.SQLSelectObj(udpApprove, ConfirmStatus, WhereClause: "Where Activity_ID=" + GridView_AdminApprove.Rows[e.RowIndex].Cells[1].Text);
                if (string.IsNullOrWhiteSpace(dt.Rows[0]["Confirm_Date(M/D/Y)"].ToString()))
                {
                    udpApprove.Admin_Confirm = true;
                    udpApprove.Confirm_Date = DateTime.Now;
                    Dictionary<string, dynamic> dicUpdDriver = new Dictionary<string, dynamic>();
                    dicUpdDriver.Add("Admin_Confirm", true);
                    dicUpdDriver.Add("Admin_User", usr.User_ID);
                    dicUpdDriver.Add("Confirm_Date", "'" + DateTime.Now.ToString("MM/dd/yyyy") + "'");

                    if (usr.Rol_ID.Contains("ADMIN"))
                    {

                        string nameSpaceClass = "BusManagementSystem._01Catalogos";
                        var obj = Activator.CreateInstance(Type.GetType(nameSpaceClass + "." + GridView_AdminApprove.Rows[e.RowIndex].Cells[5].Text));
                        PropertyInfo[] ObjProp = obj.GetType().GetProperties();
                        if (GridView_AdminApprove.Rows[e.RowIndex].Cells[4].Text == "INSERT")
                        {

                            GenericClass.SQLInsertObj(GridView_AdminApprove.Rows[e.RowIndex].Cells[5].Text, HttpUtility.HtmlDecode(GridView_AdminApprove.Rows[e.RowIndex].Cells[6].Text), ObjProp);
                        }
                        else
                        {
                            if (GridView_AdminApprove.Rows[e.RowIndex].Cells[4].Text == "UPDATE")
                            {
                                GenericClass.SQLUpdateObj(obj, adminList: HttpUtility.HtmlDecode(GridView_AdminApprove.Rows[e.RowIndex].Cells[6].Text), WhereClause: HttpUtility.HtmlDecode(GridView_AdminApprove.Rows[e.RowIndex].Cells[7].Text));

                            }
                            else
                            {
                                if (GridView_AdminApprove.Rows[e.RowIndex].Cells[4].Text == "DELETE")
                                {
                                    GenericClass.SQLDeleteObj(obj, WhereClause: HttpUtility.HtmlDecode(GridView_AdminApprove.Rows[e.RowIndex].Cells[7].Text));
                                }
                            }

                        }

                        GenericClass.SQLUpdateObj(udpApprove, dicUpdDriver, WhereClause: "Where Activity_ID=" + GridView_AdminApprove.Rows[e.RowIndex].Cells[1].Text);
                        functions.ShowMessage(this, this.GetType(), msg_ActivitiesAwait_ActivityID + " " + GridView_AdminApprove.Rows[e.RowIndex].Cells[1].Text + " " + msg_ActivitiesAwait_Executed, MessageType.Success);

                        SendEmail.StatusActivity(GridView_AdminApprove.Rows[e.RowIndex].Cells[3].Text,
                           GridView_AdminApprove.Rows[e.RowIndex].Cells[1].Text,
                           GridView_AdminApprove.Rows[e.RowIndex].Cells[8].Text,
                           usr.User_ID,
                           "Aprobó");


                    }
                }
                else
                {
                    functions.ShowMessage(this, this.GetType(), msg_ActivitiesAwait_ActivityID + " " + GridView_AdminApprove.Rows[e.RowIndex].Cells[1].Text + " " + msg_ActivitiesAwait_Processed, MessageType.Warning);
                }
            }
            catch (NullReferenceException ex)
            {
                functions.InsertError(this.Page.Title, "nameSpaceClass might be wrong ", ex.Message); string msg = "Unexpcted error"; functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            catch (Exception ex)
            {
                string msg = "Acción no autorizada o que entra en conflicto";

                if (ex.Message.Contains("TRG:"))
                {
                    msg = ex.Message;
                }
                 functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            finally
            {
                GridView_AdminApprove.DataSource = generalDataTable(columnList, from: FromDate.Text, to: ToDate.Text, status: cbStatus.SelectedValue);//GenericClass.SQLSelectObj(new Admin_Approve(), columnList, WhereClause: " Where [Admin_Approve].[Admin_Confirm]= 0 and [Admin_Approve].[Confirm_Date] IS NULL and [Admin_Approve].[Activity_Date] BETWEEN cast('" + FromDate.Text + "' as Datetime)" + " AND cast('" + ToDate.Text + "' as Datetime)+1");
                GridView_AdminApprove.PageSize = 15;
                GridView_AdminApprove.DataBind();
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

        protected void btnApply_Click(object sender, EventArgs e)
        {
            try
            {

                GridView_AdminApprove.DataSource = generalDataTable(columnList, cbVendor.SelectedValue == "ALL" ? "" : cbVendor.SelectedValue,
                    cbType.SelectedValue == "ALL" ? "" : cbType.SelectedValue, FromDate.Text, ToDate.Text, cbStatus.SelectedValue);
                GridView_AdminApprove.PageSize = 15;
                GridView_AdminApprove.DataBind();
                cleanSearchAndSorting();
            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            execute_query();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.Redirect("~/Catalogos/ExportToExcel.aspx?btn=excel", false);
        }

        protected void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

            fillFiltersCB();

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
        }

        protected void IB_View_Click(object sender, ImageClickEventArgs e)
        {


        }
        protected void Daily_Operation_Report(string Activity_ID)
        {


            if (Session["C_USER"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            Users usr = (Users)(Session["C_USER"]);

            if (usr.Rol_ID.Contains("ADMIN"))
            {

                DataTable DT_Values = GenericClass.GetCustomData("select convert(date,ap.Activity_Date) as Activity," +
                "REPLACE(SUBSTRING(ap.Where_Clause, CHARINDEX('''',ap.Where_Clause)+1, 13),'''','') as Master_TP," +
                "MT.Status as Status, SUBSTRING(ap.Where_Clause, CHARINDEX('''',ap.Where_Clause)+Len(REPLACE(SUBSTRING(ap.Where_Clause, CHARINDEX('''',ap.Where_Clause)+1, 13),'''','')), 1) + '-' + MT.Status as Group_ID ," +
                "MT.Shift_ID, MT.Vendor_ID as Vendor_ID " +
                "from ADMIN_APPROVE ap " +
                "inner join MASTER_TRIP MT on  REPLACE(SUBSTRING(ap.Where_Clause, CHARINDEX('''',ap.Where_Clause)+1, 13),'''','') = MT.Master_Trip_ID  " +
                "where Activity_ID = '" + Activity_ID.Trim() + "'");

                string reportURL = ConfigurationManager.AppSettings["reportServerURL"];
                string reportUser = ConfigurationManager.AppSettings["reporUser"];
                string reportPassword = ConfigurationManager.AppSettings["reportUserPassword"];
                string reportDomain = ConfigurationManager.AppSettings["reportUserDomain"];
                string reportEnviroment = ConfigurationManager.AppSettings["reportServerURL_Enviromnent"];
                string sReportPath = ConfigurationManager.AppSettings["r_dailyOperation"];
                string fullPath = reportEnviroment + sReportPath;

                ReportParameter[] parameters = new ReportParameter[6];


                parameters[0] = new ReportParameter("shift", DT_Values.Rows[0]["Shift_ID"].ToString(), false);
                parameters[1] = new ReportParameter("vendorID", DT_Values.Rows[0]["Vendor_ID"].ToString(), false);
                parameters[2] = new ReportParameter("StartDate", DT_Values.Rows[0]["Activity"].ToString(), false);
                parameters[3] = new ReportParameter("EndDate", DT_Values.Rows[0]["Activity"].ToString(), false);
                parameters[4] = new ReportParameter("status", DT_Values.Rows[0]["Status"].ToString(), false);
                parameters[5] = new ReportParameter("statusGroup", DT_Values.Rows[0]["Group_ID"].ToString(), false);




                ReportCredentials RC = new ReportCredentials(reportUser, reportPassword, reportDomain);
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                ReportViewer1.ServerReport.ReportServerCredentials = RC;
                ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportURL);
                ReportViewer1.ServerReport.ReportPath = fullPath;
                ReportViewer1.ServerReport.SetParameters(parameters);
                ReportViewer1.Visible = true;
            }


        }

        protected void IB_View_Click1(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton imgPreview = (ImageButton)sender;
                string Activity_ID = imgPreview.CommandArgument;
                Daily_Operation_Report(Activity_ID);
                ReportViewer1.AsyncRendering = false;
                ReportViewer1.SizeToReportContent = true;
                ReportViewer1.ZoomMode = ZoomMode.FullPage;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "daily_operation_report()", true);
            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btSearchCH1_Click(object sender, EventArgs e)
        {

            search();
        }

        private void search()
        {
            try
            {
                DataTable dtNew = new DataTable();
                GridView_AdminApprove.PageIndex = 0;
                string searchTerm = tbSearchCH1.Text.ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    fillActAwaitGrid();
                    dtNew.Clear();
                    ViewState["searchStatus"] = false;
                    ViewState["isSorting"] = false;
                }
                else
                {
                    ViewState["searchStatus"] = true;
                    ViewState["isSorting"] = false;
                    //always check if the viewstate exists before using it
                    if (ViewState["backupData"] == null)
                        return;

                    //cast the viewstate as a datatable
                    DataTable dt = ViewState["backupData"] as DataTable;

                    //make a clone of the datatable
                    dtNew = dt.Clone();

                    //search the datatable for the correct fields
                    foreach (DataRow row in dt.Rows)
                    {
                        //add your own columns to be searched here
                        if (row["Type"].ToString().ToLower().Contains(searchTerm) || row["Module"].ToString().ToLower().Contains(searchTerm) || row["Comments"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtNew.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    GridView_AdminApprove.DataSource = dtNew;
                    ViewState["searchResults"] = dtNew;
                    GridView_AdminApprove.DataBind();
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void btHelp_Click(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("./Login.aspx");
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Alertas')", true);
        }

        protected void GridView_AdminApprove_Sorted(object sender, EventArgs e)
        {



        }
    }
}


