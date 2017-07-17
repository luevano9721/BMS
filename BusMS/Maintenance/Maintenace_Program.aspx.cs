using BusManagementSystem._01Catalogos;
using BusManagementSystem.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusManagementSystem.Maintenance
{
    public partial class Maintenace_Program : System.Web.UI.Page
    {

        #region Private Variables

        Bus initialBus = new Bus();

        private AddPrivilege privilege = new AddPrivilege();
        string language = null;
        string msg_MaintenanceProgram_AddedAlert;
        string msg_MaintenanceProgram_successfully;

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            fillBusCombo();
            fillAlertCombo();
            fillVendorCombo();
            //Load buttons to hide or unhide for privileges
            privilege = new AddPrivilege("10001", btAdd, btReset, btSave, btDelete, btExcel);

        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["C_USER"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            //Get Privilege
            Users usr = (Users)(Session["C_USER"]);

            privilege.DtPrivilege = privilege.GetPrivilege(this.Page.Title, usr);

            if (!IsPostBack)
            {
                PrivilegeAndProcessFlow("10001");
                ViewState["searchStatus"] = false;
                ViewState["isSorting"] = false;

            }

            applyLanguage();
            messages_ChangeLanguage();

            if ((bool)ViewState["searchStatus"] == false && (bool)ViewState["isSorting"] == false)
            {

                fillMaintenanceGrid();
                Session["indexBusValue"] = cbBus.SelectedValue.ToString();
                Session["indexPriorityValue"] = cbPriority.SelectedValue.ToString();
            }
        }
        private void applyLanguage()
        {
            Users usr = (Users)(Session["C_USER"]);
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
        }
        protected void messages_ChangeLanguage()
        {
            msg_MaintenanceProgram_AddedAlert = (string)GetGlobalResourceObject(language, "msg_MaintenanceProgram_AddedAlert");
            msg_MaintenanceProgram_successfully = (string)GetGlobalResourceObject(language, "msg_MaintenanceProgram_successfully");
        }

        public DataTable generalDataTable(string vendor = "")
        {
            Users usr = (Users)(Session["C_USER"]);
            DataTable dtReturn = new DataTable();
            try
            {
                Alert_Log initAlertLog = new Alert_Log();
                List<string> lsAlert = new List<string>();
                lsAlert.Add("Alert_log_ID");
                lsAlert.Add("Alert_Date");
                lsAlert.Add("Bus_ID");
                lsAlert.Add("Rissed_By");
                lsAlert.Add("Comments");
                lsAlert.Add("Priority");
                lsAlert.Add("Status");
                lsAlert.Add("Vendor_ID");
                lsAlert.Add("Off_Date");

                //string vendorFilter = usr.Vendor_ID.Equals("ALL") ? "" : "[Vendor_ID]= '" + usr.Vendor_ID + "'";
                //string vendorFilterAdmin = vendor.Equals("ALL") ? "" : "[Vendor_ID]= '" + vendor + "'";

                if (vendor == "ALL")
                    dtReturn = GenericClass.SQLSelectObj(initAlertLog, lsAlert, mappingQueryName: "Catalog", WhereClause: "Where UPPER([Alert_Log].CODE) = UPPER('Ma1') "
                  + " AND [Alert_Log].[Status] = 'OPEN'  ORDER BY Alert_log_ID DESC");
                else
                    dtReturn = GenericClass.SQLSelectObj(initAlertLog, lsAlert, mappingQueryName: "Catalog", WhereClause: "Where UPPER([Alert_Log].CODE) = UPPER('Ma1') "
                  + " AND [Alert_Log].[Status] = 'OPEN' AND  [Vendor_ID]= '" + cbVendorAdm.SelectedValue.ToString() + "'" + " ORDER BY Alert_log_ID DESC");




            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

            return dtReturn;
        }

        /// <summary>
        /// Add,Reset,Edit,Save,Delete
        /// </summary>
        /// <param name="binary"></param>
        private void ProcessFlow()
        {

            if (this.btAdd.Visible == true || (this.btAdd.Visible == false & this.btSave.Visible == false))
            {
                cbVendor.Enabled = false;

                cbBus.Enabled = false;

                commenst.Enabled = false;

                cbPriority.Enabled = false;

                tbExpireddate.Enabled = false;

                commenst.Text = string.Empty;

                Session["Alert_log_ID"] = 0;

                tbSearchCH1.Enabled = true;

                btSearchCH1.Enabled = true;

                btExcel.Enabled = true;

                cbVendorAdm.Enabled = true;

                GridView_alert.Columns[0].Visible = true;

                privilege.applyPrivilege("10001", GridView_alert);
            }

            if (this.btSave.Visible == true || this.btDelete.Visible == true)
            {
                cbBus.Enabled = true;

                cbVendor.Enabled = true;

                commenst.Enabled = true;

                cbPriority.Enabled = true;

                tbExpireddate.Enabled = true;

                tbSearchCH1.Enabled = false;

                btSearchCH1.Enabled = false;

                btExcel.Enabled = false;

                cbVendorAdm.Enabled = false;

                GridView_alert.Columns[0].Visible = false;
            }


            int alertLogid = 0;

            int.TryParse(Session["Alert_log_ID"].ToString(), out alertLogid);

            if (alertLogid > 0)
            {

                cbBus.Enabled = false;

                commenst.Enabled = true;

                cbPriority.Enabled = true;

                tbExpireddate.Enabled = true;
            }

        }
        private void fillAlertCombo()
        {
            try
            {
                DataTable dtAlert = GenericClass.GetCustomData("SELECT [Code] ,[Description] "
                + " FROM [ALERT] where [Alert_Item_ID] = 5 and UPPER(code) =UPPER('Ma1')");
                cbAlert.Items.Clear();
                cbAlert.DataSource = dtAlert;
                cbAlert.DataValueField = "Code";
                cbAlert.DataTextField = "Description";
                cbAlert.DataBind();
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        private void fillBusCombo()
        {
            try
            {
                string vendorID = cbVendor.SelectedValue.ToString();
                DataTable dtBus = new DataTable();
                dtBus = GenericClass.SQLSelectObj(initialBus, mappingQueryName: "Catalog", WhereClause: "Where [Bus].[Vendor_ID]= '" + cbVendor.SelectedValue + "'");

                cbBus.Items.Clear();
                cbBus.DataSource = dtBus;
                cbBus.DataValueField = "BUS_ID";
                cbBus.DataTextField = "BUS_ID";
                cbBus.DataBind();
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        private void fillBusComboWhenSelectRow(string vendorID)
        {
            try
            {
                DataTable dtBus = new DataTable();
                dtBus = GenericClass.SQLSelectObj(initialBus, mappingQueryName: "Catalog", WhereClause: "Where [Bus].[Vendor_ID]= '" + cbVendor.SelectedValue + "'");
                cbBus.Items.Clear();
                cbBus.DataSource = dtBus;
                cbBus.DataValueField = "BUS_ID";
                cbBus.DataTextField = "BUS_ID";
                cbBus.DataBind();
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        private void fillVendorCombo()
        {
            try
            {
                Users usr = (Users)(Session["C_USER"]);
                Vendor initialVendor = new Vendor();
                DataTable dt = GenericClass.SQLSelectObj(initialVendor, WhereClause: usr.Vendor_ID.Equals("ALL") ? "" : "Where [Vendor_ID]= '" + usr.Vendor_ID + "'");
                DataView dvVendor = new DataView(dt);
                DataTable distincVendor = dvVendor.ToTable(true, "VENDOR_ID", "NAME");

                cbVendorAdm.Items.Clear();
                cbVendorAdm.DataSource = distincVendor;
                cbVendorAdm.DataValueField = "VENDOR_ID";
                cbVendorAdm.DataTextField = "NAME";
                cbVendorAdm.DataBind();

                cbVendor.Items.Clear();
                cbVendor.DataSource = distincVendor;
                cbVendor.DataValueField = "VENDOR_ID";
                cbVendor.DataTextField = "NAME";
                cbVendor.DataBind();

                if (usr.Vendor_ID.Equals("ALL"))
                {
                    ListItem li = new ListItem("ALL", "ALL");
                    cbVendorAdm.Items.Insert(0, li);
                    cbVendor.Items.Insert(0, li);
                }

                cbVendorAdm.SelectedIndex = 0;
                cbVendor.SelectedIndex = cbVendorAdm.SelectedIndex;

                fillBusCombo();

                if (usr.Profile != "INTERNAL")
                {
                    cbVendor.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btAdd_Click(object sender, EventArgs e)
        {
            PrivilegeAndProcessFlow("01101");
            cleanSearchAndSorting();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
        }
        protected void btReset_Click(object sender, EventArgs e)
        {
            PrivilegeAndProcessFlow("10001");
            fillMaintenanceGrid();
            cleanSearchAndSorting();
            cbVendorAdm.Enabled = true;
        }
        protected void btExcel_Click(object sender, EventArgs e)
        {
            execute_query();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.Redirect("~/Catalogos/ExportToExcel.aspx?btn=excel", false);
        }

        private void fillMaintenanceGrid()
        {
            try
            {
                DataTable getData = generalDataTable(cbVendorAdm.SelectedValue);

                    GridView_alert.DataSource = getData;
                    ViewState["backupData"] = getData;

                    GridView_alert.PageSize = 15;
                    GridView_alert.DataBind();

            }
            catch (Exception ex)
            {
                
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

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
                DataTable dtLoad = generalDataTable(cbVendorAdm.SelectedValue);
                Session["Excel"] = dtLoad;
                Session["name"] = "Maintenance";
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    cleanSearchAndSorting();
                    if (Session["Alert_log_ID"] == null)
                    {
                        Session["Alert_log_ID"] = 0;
                    }
                    Alert_Log alertAdd = new Alert_Log();
                    DateTime expliredDate = new DateTime();
                    if (tbExpireddate.Text == string.Empty)
                    {
                        throw new Exception("You need select a date");
                    }
                    DateTime.TryParse(tbExpireddate.Text, out expliredDate);
                    alertAdd.Alert_log_ID = (int)Session["Alert_log_ID"];
                    alertAdd.Code = cbAlert.SelectedValue.ToString();
                    alertAdd.Bus_ID = Session["indexBusValue"].ToString();
                    alertAdd.Comments = commenst.Text.Trim();
                    alertAdd.Priority = Session["indexPriorityValue"].ToString();
                    alertAdd.Status = "OPEN";
                    alertAdd.Vendor_ID = cbVendor.SelectedValue.ToString();
                    alertAdd.Rissed_By = "Manual";
                    alertAdd.Off_Date = expliredDate;
                    DataTable drResult = GenericClass.InsertToAlertLog(alertAdd);
                    functions.ShowMessage(this, this.GetType(), msg_MaintenanceProgram_AddedAlert + alertAdd.Bus_ID + msg_MaintenanceProgram_successfully, MessageType.Success);
                    fillMaintenanceGrid();
                    PrivilegeAndProcessFlow("10001");
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void GridView_alert_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow gvRow = GridView_alert.SelectedRow;

            try
            {
                int alertLogID = 0;
                int.TryParse(GridView_alert.Rows[gvRow.RowIndex].Cells[1].Text.ToString(), out alertLogID);
                Session["Alert_log_ID"] = alertLogID;

                functions selectFunction = new functions();

                selectFunction.SelectDropdownList(cbVendor, GridView_alert.Rows[gvRow.RowIndex].Cells[8].Text.ToString().Trim());

                fillBusComboWhenSelectRow(cbVendor.SelectedValue.ToString().Trim());

                selectFunction.SelectDropdownList(cbBus, GridView_alert.Rows[gvRow.RowIndex].Cells[3].Text.ToString().Trim());

                selectFunction.SelectDropdownList(cbPriority, GridView_alert.Rows[gvRow.RowIndex].Cells[6].Text.ToString().Trim());

                tbExpireddate.Text = HttpUtility.HtmlDecode(GridView_alert.Rows[gvRow.RowIndex].Cells[9].Text);

                commenst.Text =  HttpUtility.HtmlDecode(GridView_alert.Rows[gvRow.RowIndex].Cells[5].Text);

                Session["indexBusValue"] = cbBus.SelectedValue.ToString();

                Session["indexPriorityValue"] = cbPriority.SelectedValue.ToString();

                PrivilegeAndProcessFlow("01101");

                cbVendorAdm.Enabled = false;

                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btEdit_Click(object sender, EventArgs e)
        {
            PrivilegeAndProcessFlow("01101");
        }

        protected void cbVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillBusCombo();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
        }

        protected void GridView_alert_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_alert.PageIndex = e.NewPageIndex;
            if ((bool)ViewState["searchStatus"] == true)
            { GridView_alert.DataSource = ViewState["searchResults"]; }
            if ((bool)ViewState["isSorting"] == true)
            { GridView_alert.DataSource = Session["Objects_MP"]; }
            GridView_alert.DataBind();
        }

        protected void cbBus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["indexBusValue"] = cbBus.SelectedValue.ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
        }

        protected void cbPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["indexPriorityValue"] = cbPriority.SelectedValue.ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
        }

        protected void GridView_alert_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header | e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Visible = false;
            }
        }

        protected void cbVendorAdm_SelectedIndexChanged(object sender, EventArgs e)
        {
            // cbVendor.SelectedValue = cbVendorAdm.SelectedValue;
            GridView_alert.DataSource = generalDataTable(cbVendorAdm.SelectedValue);
           
            GridView_alert.DataBind();
            // PrivilegeAndProcessFlow("10001");
            //  cbVendor.SelectedValue = cbVendorAdm.SelectedValue == "ALL" ? cbVendorAdm.Items[1].Value : cbVendorAdm.SelectedValue;
            //   fillBusCombo();
        }

        protected void btSearchCH1_Click(object sender, EventArgs e)
        {
            search();
        }

        private void cleanSearchAndSorting()
        {
            ViewState["searchStatus"] = false;
            ViewState["isSorting"] = false;
            tbSearchCH1.Text = string.Empty;

        }
        private void search()
        {
            try
            {
                DataTable dtNew = new DataTable();
                GridView_alert.PageIndex = 0;
                string searchTerm = tbSearchCH1.Text.ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {
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
                        if (row["Bus_ID"].ToString().ToLower().Contains(searchTerm) || row["Rissed_By"].ToString().ToLower().Contains(searchTerm) || row["Comments"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtNew.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    GridView_alert.DataSource = dtNew;
                    ViewState["searchResults"] = dtNew;
                    GridView_alert.DataBind();
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void GridView_alert_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                ViewState["isSorting"] = true;
                ViewState["searchStatus"] = false;
                Users usr = (Users)(Session["C_USER"]);
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

                DataTable dt = generalDataTable(cbVendorAdm.SelectedValue);
                DataView sortedView = new DataView(dt);
                sortedView.Sort = e.SortExpression + " " + sortingDirection;
                Session["Objects_MP"] = sortedView;
                GridView_alert.DataSource = sortedView;
                GridView_alert.DataBind();

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

        public void PrivilegeAndProcessFlow(string turnOnOffBNuttons)
        {
            privilege.applyPrivilege(turnOnOffBNuttons, GridView_alert);
            ProcessFlow();

        }

        protected void btHelp_Click(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("./Login.aspx");
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Camión / Conductor')", true);
        }

    }
}