using BusManagementSystem._01Catalogos;
using BusManagementSystem.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusManagementSystem.Catalogos
{
    public partial class C_Alerts : System.Web.UI.Page
    {
    
        private Alert initialAlert = new Alert();
        
        private AddPrivilege privilege = new AddPrivilege();

        private string vendorFilter;
        String language = null;
        string msg_Calert_Added;
        string msg_Calert_Success;
        string msg_Calert_Watting;
        string msg_Calert_Updated;
        string msg_Calert_NoChanges;

        /// <summary>
        /// Identify which buttons to show for the user's privileges
        /// </summary>
        /// <param name="turnOnOffBNuttons">String (insert,reset,save,delete,export)</param>
        public void PrivilegeAndProcessFlow(string turnOnOffBNuttons)
        {
            privilege.applyPrivilege(turnOnOffBNuttons, GridView_Alerts);
            ProcessFlow();

        }

        /// <summary>
        /// Get Table to show in the catalog
        /// </summary>
        /// <param name="vendor">Vendor_Id to filter data</param>
        /// <returns>datatable to fill grid</returns>
        public DataTable generalDataTable(string vendor = "")
        {
            DataTable dtReturn = new DataTable();

            List<string> alertSelect = new List<string>();

            if (Session["C_USER"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            Session["Excel"] = null;

            Session["name"] = string.Empty;
            
            Users usr = (Users)(Session["C_USER"]);            

            alertSelect.Add("Code");

            alertSelect.Add("Alert_Item_ID");

            alertSelect.Add("Description");

            alertSelect.Add("Action");

            alertSelect.Add("Period");

            alertSelect.Add("Priority");

            alertSelect.Add("Create_Date");

            alertSelect.Add("Last_Check");

            alertSelect.Add("Is_Active");

            alertSelect.Add("Off_Date");

            alertSelect.Add("Vendor_ID");

            alertSelect.Add("Alert_Type");

            try
            {
                dtReturn = GenericClass.SQLSelectObj(initialAlert, alertSelect, mappingQueryName: "Catalog", WhereClause: vendor == "ALL" ? "Where Alert_Type <>'SYSTEM'" : "Where [Alert].[Vendor_ID]= '" 
                    + vendor + "' and Alert_Type <>'SYSTEM'", OrderByClause:"Order By Create_Date Desc, Code");

                Session["Excel"] = dtReturn;

                Session["name"] = "Alerts";

                ViewState["backupData"] = dtReturn;
            }
            catch (Exception ex)
            {


                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

            return dtReturn;
        }

        private void fillAlertGrid()
        {
            DataTable getData = generalDataTable(cbVendorAdm.SelectedValue);

            GridView_Alerts.DataSource = getData;
            
            GridView_Alerts.DataBind();
        }

        private void fillVendor()
        {
            Users usr = (Users)(Session["C_USER"]);

            vendorFilter = usr.Vendor_ID.Equals("ALL") ? "" : "Where [Vendor_ID]= '" + usr.Vendor_ID + "'";

            Vendor initialVendor = new Vendor();

            DataTable dt = GenericClass.SQLSelectObj(initialVendor, WhereClause: string.IsNullOrWhiteSpace(vendorFilter) ? null : vendorFilter);

            DataView dvVendor = new DataView(dt);

            DataTable distincVendor = dvVendor.ToTable(true, "VENDOR_ID", "NAME");

            cbVendorAdm.Items.Clear();

            cbVendor.Items.Clear();

            cbVendorAdm.DataSource = distincVendor;

            cbVendorAdm.DataValueField = "VENDOR_ID";

            cbVendorAdm.DataTextField = "NAME";

            cbVendorAdm.DataBind();

            cbVendor.DataSource = distincVendor;

            cbVendor.DataValueField = "VENDOR_ID";

            cbVendor.DataTextField = "NAME";

            cbVendor.DataBind();

            

            if (usr.Vendor_ID.Equals("ALL"))
            {
                cbVendorAdm.Enabled = true;

                ListItem li = new ListItem("ALL", "ALL");

                cbVendorAdm.Items.Insert(0, li);

                cbVendor.Items.Insert(0, li);
            }
            else
            {
                cbVendorAdm.Enabled = false;

            }

            cbVendor.SelectedValue = cbVendorAdm.SelectedValue;
        }

        private void fillAlert()
        {
            Alert_Item initialAlertItem = new Alert_Item();

            DataTable dtAlertItem = GenericClass.SQLSelectObj(initialAlertItem);

            cbAlertItem.DataSource = dtAlertItem;

            cbAlertItem.DataTextField = "Description";

            cbAlertItem.DataValueField = "Alert_Item_Id";

            cbAlertItem.DataBind();
        }
        private void search()
        {
            try
            {
                DataTable dtNew = new DataTable();

                GridView_Alerts.PageIndex = 0;

                string searchTerm = tbSearchCH1.Text.ToLower();

                if (string.IsNullOrEmpty(searchTerm))
                {
                    fillAlertGrid();

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
                        if (row["Code"].ToString().ToLower().Contains(searchTerm) || row["Description"].ToString().ToLower().Contains(searchTerm) || row["Action"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtNew.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    GridView_Alerts.DataSource = dtNew;

                    ViewState["searchResults"] = dtNew;

                    GridView_Alerts.DataBind();
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        private void ProcessFlow()
        {


            if (this.btAdd.Visible == true || (this.btAdd.Visible == false & this.btSave.Visible == false))
            {
                tbCode.Enabled = false;

                tbDescription.Enabled = false;

                tbAction.Enabled = false;

                cbPriority.Enabled = false;

                cbAlertItem.Enabled = false;

                cbEnabled.Enabled = false;

                cbVendor.Enabled = false;

                tbOffDate.Enabled = false;

                tbPeriod.Enabled = false;

                cbVendorAdm.Enabled = true;

                tbSearchCH1.Enabled = true;

                btSearchCH1.Enabled = true;

                btExcel.Enabled = true;

                tbCode.Text = string.Empty;

                tbDescription.Text = string.Empty;

                tbAction.Text = string.Empty;

                tbOffDate.Text = string.Empty;

                tbPeriod.Text = string.Empty;

                GridView_Alerts.Columns[0].Visible = true;

                privilege.applyPrivilege("10001", GridView_Alerts);

            }
            if (this.btSave.Visible == true && tbCode.Text == string.Empty)
            {
                tbCode.Enabled = false;

                tbDescription.Enabled = true;

                tbAction.Enabled = true;

                cbPriority.Enabled = true;

                cbAlertItem.Enabled = true;

                cbEnabled.Enabled = true;

                cbVendor.Enabled = true;

                tbPeriod.Enabled = true;

                tbOffDate.Enabled = true;

                cbVendorAdm.Enabled = false;

                tbSearchCH1.Enabled = false;

                btSearchCH1.Enabled = false;

                btExcel.Enabled = false;

                GridView_Alerts.Columns[0].Visible = false;
            }
            if (this.btSave.Visible == true && tbCode.Text != string.Empty)
            {
                tbCode.Enabled = false;

                tbDescription.Enabled = true;

                tbAction.Enabled = true;

                cbPriority.Enabled = true;

                cbAlertItem.Enabled = true;

                cbEnabled.Enabled = true;

                cbVendor.Enabled = true;

                tbPeriod.Enabled = true;

                tbOffDate.Enabled = true;

                cbVendorAdm.Enabled = false;

                tbSearchCH1.Enabled = false;

                btSearchCH1.Enabled = false;

                btExcel.Enabled = false;

                GridView_Alerts.Columns[0].Visible = false;

            }
        }

        /// <summary>
        /// Databind  correct viewState to datagrid
        /// </summary>
        /// <param name="dtg">datagrid</param>
        /// <param name="pageIndex">index pagination</param>
        protected void pagination(GridView dtg, int pageIndex)
        {
            try
            {
                dtg.PageIndex = pageIndex;

                if ((bool)ViewState["searchStatus"] == true)
                {
                    dtg.DataSource = ViewState["searchResults"];
                }
                if ((bool)ViewState["isSorting"] == true)
                {
                    dtg.DataSource = Session["sortObjects"];
                }

                dtg.DataBind();
            }
            catch (Exception ex)
            {


                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected Alert GetNewAlert()
        {
            // Add record to driver table
            Alert addAlert = new Alert();

            DateTime dtPeriod = new DateTime();

            DateTime dtCreateAlert = new DateTime();

            string[] formats = { "MM/dd/yyyy", "MM/dd/yy" };

            addAlert.Code = tbCode.Text.Trim();

            addAlert.Description = tbDescription.Text;

            addAlert.Action = tbAction.Text;

            addAlert.Priority = cbPriority.SelectedValue.ToString();

            addAlert.Alert_Item_ID = Int32.Parse(cbAlertItem.SelectedValue.ToString());

            addAlert.Is_Active = Convert.ToBoolean(cbEnabled.Checked);

            addAlert.Period = Int32.Parse(tbPeriod.Text.Trim());

            DateTime.TryParseExact(DateTime.Now.ToString("MM/dd/yyyy"), formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtCreateAlert);

            addAlert.Create_Date = dtCreateAlert;

            addAlert.Last_Check = dtCreateAlert;

            DateTime.TryParseExact(tbOffDate.Text, formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtPeriod);

            addAlert.Off_Date = dtPeriod;

            addAlert.Vendor_ID = cbVendor.SelectedValue.ToString();

            addAlert.Alert_Type = "MANUAL";

            addAlert.Last_Check = dtCreateAlert;

            return addAlert;
        }

        protected Alert GetCurrentAlert()
        {
            Alert currentAlert = new Alert();

            DateTime dtCurrentOffDate = new DateTime();

            string[] formats = { "MM/dd/yyyy", "MM/dd/yy" };

            try
            {
                DataTable getAlert = GenericClass.SQLSelectObj(initialAlert, WhereClause: " Where [Code]='" + tbCode.Text + "'");

                DateTime.TryParseExact(getAlert.Rows[0]["Off_Date(M/D/Y)"].ToString(), formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtCurrentOffDate);

                currentAlert.Code = getAlert.Rows[0]["Code"].ToString();

                currentAlert.Description = getAlert.Rows[0]["Description"].ToString();

                currentAlert.Action = getAlert.Rows[0]["Action"].ToString();

                currentAlert.Priority = getAlert.Rows[0]["Priority"].ToString();

                currentAlert.Alert_Item_ID = Int32.Parse(getAlert.Rows[0]["Alert_Item_Id"].ToString());

                currentAlert.Period = Int32.Parse(getAlert.Rows[0]["Period"].ToString());

                currentAlert.Vendor_ID = getAlert.Rows[0]["Vendor_ID"].ToString();

                currentAlert.Off_Date = dtCurrentOffDate;

                currentAlert.Is_Active = Convert.ToBoolean(getAlert.Rows[0]["Is_Active"].ToString());

            }
            catch (Exception ex)
            {


                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);

            }
            return currentAlert;
        }

        protected void SaveAlert(Users usr, Alert newAlert)
        {
            if (usr.Vendor_ID.Equals("ALL"))
            {
                GenericClass.SQLInsertObj(newAlert);

                functions.ShowMessage(this, this.GetType(), msg_Calert_Added + " " + newAlert.Description + " " + msg_Calert_Success, MessageType.Success);
            }
            else
            {
                Admin_Approve confirmInsert = new Admin_Approve(0, DateTime.Now, usr.User_ID.ToString(), "INSERT", newAlert.GetType().Name, "", GenericClass.formatValues(newAlert), "", "New alert is added: " + newAlert.Description);

                GenericClass.SQLInsertObj(confirmInsert);

                functions.ShowMessage(this, this.GetType(),  msg_Calert_Watting + " " + newAlert.Description, MessageType.Warning); 
            }
        }

        protected void UpdateAlert(Users usr, Alert currentAlert, Alert newAlert, Admin_Approve newApprove)
        {
            if (usr.Vendor_ID.Equals("ALL"))
            {
                GenericClass.SQLUpdateObj(newAlert, adminApprove.compareObjects(currentAlert, newAlert, ""), "Where Code='" + currentAlert.Code + "'");

                functions.ShowMessage(this, this.GetType(), msg_Calert_Updated + " " + currentAlert.Code + "-" + newAlert.Description + " " + msg_Calert_Success, MessageType.Success);
            }
            else
            {
                newApprove.Activity_ID = 0;

                newApprove.Admin_Confirm = false;

                newApprove.Activity_Date = DateTime.Now;

                newApprove.User_ID = usr.User_ID;

                newApprove.Type = "UPDATE";

                newApprove.Module = newAlert.GetType().Name;

                newApprove.Where_Clause = "Where Code='" + newAlert.Code + "'";

                newApprove.Comments = "[Alert][" + newAlert.Code + "]<br>" + newApprove.Comments;

                GenericClass.SQLInsertObj(newApprove);

                SendEmail.sendEmailToAdmin(usr.User_ID.ToString(), newApprove);

                functions.ShowMessage(this, this.GetType(),  msg_Calert_Watting + " " + currentAlert.Code + "-" + currentAlert.Description, MessageType.Warning);

            }
        }

        /// <summary>
        /// Sorting function
        /// </summary>
        /// <param name="dtg">Grid to be sorted</param>
        /// <param name="gridSorting">direction to be sorted</param>
        protected void srotingGrid(GridView dtg, GridViewSortEventArgs gridSorting)
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

                DataTable dt = generalDataTable(cbVendorAdm.SelectedValue);

                DataView sortedView = new DataView(dt);

                sortedView.Sort = gridSorting.SortExpression + " " + sortingDirection;

                Session["sortObjects"] = sortedView;

                dtg.DataSource = sortedView;

                dtg.DataBind();
            }
            catch (Exception ex)
            {

        

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            //Load buttons to hide or unhide for privileges
            privilege = new AddPrivilege("10001", btAdd, btReset, btSave, null, btExcel);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Get Privilege
            Users usr = (Users)(Session["C_USER"]);
            if (usr == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                //Fill datatable with privileges
                privilege.DtPrivilege = privilege.GetPrivilege(this.Page.Title, usr);

                if (!IsPostBack)
                { 
                    fillVendor();

                    //Set buttons to initial mode
                    PrivilegeAndProcessFlow("10001");

                    ViewState["searchStatus"] = false;

                    ViewState["isSorting"] = false;
                }

                if ((bool)ViewState["searchStatus"] == false && (bool)ViewState["isSorting"] == false)
                {

                    fillAlert();

                    fillAlertGrid();
                }

                applyLanguage();
                if (!string.IsNullOrEmpty(language))
                translateMessages(language);
                
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

        private void translateMessages(string language)
        {

            msg_Calert_Added = (string)GetGlobalResourceObject(language, "msg_Calert_Added");
            msg_Calert_Success = (string)GetGlobalResourceObject(language, "msg_Calert_Success");
            msg_Calert_Watting = (string)GetGlobalResourceObject(language, "msg_Calert_Watting");
            msg_Calert_Updated = (string)GetGlobalResourceObject(language, "msg_Calert_Updated");
            msg_Calert_NoChanges = (string)GetGlobalResourceObject(language, "msg_Calert_NoChanges");

        }

        protected void btAdd_Click(object sender, EventArgs e)
        {
            PrivilegeAndProcessFlow("01111");
            
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);

        }

        protected void btReset_Click(object sender, EventArgs e)
        {
            PrivilegeAndProcessFlow("10001");

            fillAlertGrid();

            cbVendorAdm.Enabled = true;
        }

 
        protected void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["C_USER"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }

                Users usr = (Users)(Session["C_USER"]);

                Alert newAlert = GetNewAlert();

                if (string.IsNullOrEmpty(tbCode.Text.Trim()))
                {

                    SaveAlert(usr, newAlert);
                }
                else
                {

                    Admin_Approve newApprove = new Admin_Approve();

                    Alert currentAlert = GetCurrentAlert();

                    newApprove = adminApprove.compareObjects(currentAlert, newAlert);

                    if (newApprove.New_Values != "No values changed")
                    {
                        UpdateAlert(usr, currentAlert, newAlert, newApprove);
                    }

                    else
                    {
                        functions.ShowMessage(this, this.GetType(), msg_Calert_NoChanges , MessageType.Info);
                    }

                }

                PrivilegeAndProcessFlow("10001");

                fillAlertGrid();
            }
            catch (Exception ex)
            {
             

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);

            }
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {

        }

        protected void btExcel_Click(object sender, EventArgs e)
        {
            HttpContext.Current.ApplicationInstance.CompleteRequest();

            Response.Redirect("~/Catalogos/ExportToExcel.aspx?btn=excel", false);
        }


        protected void GridView_Alerts_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = GridView_Alerts.SelectedRow;
            try
            {

                tbCode.Text = HttpUtility.HtmlDecode(GridView_Alerts.Rows[gvRow.RowIndex].Cells[1].Text);
                
                tbDescription.Text =HttpUtility.HtmlDecode(GridView_Alerts.Rows[gvRow.RowIndex].Cells[3].Text);
                
                tbAction.Text = HttpUtility.HtmlDecode(GridView_Alerts.Rows[gvRow.RowIndex].Cells[4].Text);
                
                tbOffDate.Text = GridView_Alerts.Rows[gvRow.RowIndex].Cells[10].Text;
                
                tbPeriod.Text = GridView_Alerts.Rows[gvRow.RowIndex].Cells[5].Text;

                functions selectFunction = new functions();

                selectFunction.SelectDropdownList(cbPriority, GridView_Alerts.Rows[gvRow.RowIndex].Cells[8].Text.ToString().Trim());

                selectFunction.SelectDropdownList(cbAlertItem, GridView_Alerts.Rows[gvRow.RowIndex].Cells[2].Text.ToString().Trim());

                selectFunction.SelectDropdownList(cbVendor, GridView_Alerts.Rows[gvRow.RowIndex].Cells[11].Text.ToString().Trim());

                cbEnabled.Checked = ((CheckBox)GridView_Alerts.Rows[gvRow.RowIndex].Cells[9].Controls[0]).Checked;                


                PrivilegeAndProcessFlow("01111");

                cbVendorAdm.Enabled = false;

                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
            }
            catch (Exception ex)
            {
               

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void cbVendorAdm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                GridView_Alerts.PageIndex = 0;

                cbVendor.SelectedValue = cbVendorAdm.SelectedValue;

                GridView_Alerts.DataSource = generalDataTable(cbVendorAdm.SelectedValue);

                GridView_Alerts.DataBind();

                PrivilegeAndProcessFlow("10001");

                

                
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

        protected void GridView_Alerts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            pagination(GridView_Alerts, e.NewPageIndex);
        }

        protected void GridView_Alerts_Sorting(object sender, GridViewSortEventArgs e)
        {
            srotingGrid(GridView_Alerts, e);
        }

        protected void btSearchCH1_Click(object sender, EventArgs e)
        {
            search();
        }
        protected void btHelp_Click(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("./Login.aspx");
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Alertas')", true);
        }
        
    }
}