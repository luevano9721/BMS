using BusManagementSystem.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusManagementSystem._01Catalogos;

namespace BusManagementSystem.Catalogos
{
    public partial class C_Shift : System.Web.UI.Page
    {
        #region Private Variables

        private DataTable dtNewSearch = new DataTable();

        private AddPrivilege privilege = new AddPrivilege();

        String language = null;
        string msg_C_Shift_UnitCost;
        string msg_C_Shift_AddedShift;
        string msg_C_Shift_UpdatedShift;
        string msg_C_Shift_NoChanges;
        string msg_C_Shift_DeletedShift;
        string msg_C_Shift_SpecialService;




        #endregion

        #region Methods

        public void PrivilegeAndProcessFlow(string turnOnOffBNuttons)
        {
            privilege.applyPrivilege(turnOnOffBNuttons, dtgShift);
            ProcessFlow();

        }

        /// <summary>
        /// Select all shift in the database
        /// </summary>
        /// <returns>datatable with results of select</returns>
        protected DataTable generalDataTable()
        {
            DataTable dtReturn = new DataTable();

            Session["Excel"] = null;

            Session["name"] = string.Empty;

            try
            {
                dtReturn = GenericClass.SQLSelectObj(new Shift(), mappingQueryName: "Catalog");

                Session["Excel"] = dtReturn;

                Session["name"] = "Shift";

                ViewState["backupData"] = dtReturn;

                dtgShift.PageSize = 15;
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

            return dtReturn;

        }
        /// <summary>
        /// Fill dtgShift grid with datatable from generalDataTable
        /// </summary>
        protected void filldtgShift()
        {
            dtgShift.DataSource = generalDataTable();

            dtgShift.DataBind();
        }

        private void cleanSearchAndSorting()
        {
            ViewState["searchStatus"] = false;
            ViewState["isSorting"] = false;
            tbSearch.Text = string.Empty;

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

                DataTable dt = generalDataTable();

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

        /// <summary>
        /// Pass values into textboxes and checkbox to edit
        /// </summary>
        /// <param name="dtg">Datagrid to select</param>
        /// <param name="rowIndex">index selected</param>
        protected void SelectRowFromGrid(GridView dtg, int rowIndex)
        {
            try
            {
                

                DateTime getTime = new DateTime();

                tbShiftID.Text = dtg.Rows[rowIndex].Cells[1].Text;

                tbShiftName.Text =HttpUtility.HtmlDecode( dtg.Rows[rowIndex].Cells[2].Text);

                tbInvoiceGroup.Text =HttpUtility.HtmlDecode( dtg.Rows[rowIndex].Cells[5].Text);

                DateTime.TryParseExact(dtg.Rows[rowIndex].Cells[3].Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal, out getTime);

                tbTime.Text = getTime.ToString("h:mm tt");

                CheckBox chkExitTemp = (CheckBox)dtg.Rows[rowIndex].Cells[6].Controls[0];

                chkExit.Checked = chkExitTemp.Checked;

                CheckBox chkSpecialTemp = (CheckBox)dtg.Rows[rowIndex].Cells[4].Controls[0];

                chkSpecial.Checked = chkSpecialTemp.Checked;

                tbSpecialService.Text = HttpUtility.HtmlDecode(dtg.Rows[rowIndex].Cells[7].Text);
            }
            catch (Exception ex)
            {

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        /// <summary>
        /// Search inside datagrid and return results
        /// </summary>
        /// <param name="dtg">datagrid to search in </param>
        private void search(GridView dtg)
        {
            try
            {
                dtg.PageIndex = 0;

                string searchTerm = tbSearch.Text.ToLower();

                if (string.IsNullOrEmpty(searchTerm))
                {
                    filldtgShift();

                    dtNewSearch.Clear();

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
                    dtNewSearch = dt.Clone();

                    //search the datatable for the correct fields
                    foreach (DataRow row in dt.Rows)
                    {
                        //add your own columns to be searched here
                        if (row["Shift_ID"].ToString().ToLower().Contains(searchTerm) || row["Name"].ToString().ToLower().Contains(searchTerm) || row["Invoice_GroupName"].ToString().ToLower().Contains(searchTerm) || row["Special_Service"].ToString().ToLower().Contains(searchTerm) || row["Start_Time"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtNewSearch.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    dtg.DataSource = dtNewSearch;

                    ViewState["searchResults"] = dtNewSearch;

                    dtg.DataBind();
                }
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        /// <summary>
        /// Insert new service
        /// </summary>
        /// <param name="newService">object to be inserted</param>
        protected void InsertNewService(Service newService)
        {
            try
            {
                GenericClass.SQLInsertObj(newService);
            }
            catch (Exception ex)
            {

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        /// <summary>
        /// Insert new Fare     
        /// </summary>
        /// <param name="newFare">Object to be inserted</param>
        protected void InsertNewFare(Fare newFare)
        {
            try
            {
                GenericClass.SQLInsertObj(newFare);
            }
            catch (Exception ex)
            {

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        /// <summary>
        /// Get values from database, if not exists then insert new service
        /// </summary>
        /// <param name="service">Name of service</param>
        /// <returns></returns>
        protected void GetNewService(string service)
        {
            try
            {
                Service newService = new Service();

                DataTable isInDB = GenericClass.SQLSelectObj(newService, WhereClause: "Where UPPER(Service_ID)='" + service.ToUpper() + "'");

                if (isInDB.Rows.Count < 1)
                {

                    newService.Service_ID = service;
                    newService.Min_Distance = 0;
                    newService.Max_Distance = 0;
                    InsertNewService(newService);

                }
            }
            catch (Exception ex)
            {

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        /// <summary>
        /// Get values from database, if not exists then insert new fare for each vendor
        /// </summary>
        /// <param name="service">Service name</param>
        /// <param name="cost">Service cost</param>
        /// <returns></returns>
        protected void GetNewFare(string service, double cost)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(cost.ToString()))
                {
                    DataTable distincVendor = GenericClass.SQLSelectObj(new Vendor());

                    Fare newFare = new Fare();

                    foreach (DataRow item in distincVendor.Rows)
                    {
                        DataTable isInDB = GenericClass.SQLSelectObj(newFare, WhereClause: "Where Vendor_ID='" + item[0].ToString() + "' And Service_ID='" + service + "'");

                        if (isInDB.Rows.Count < 1)
                        {

                            newFare.Service_ID = service;
                            newFare.Vendor_ID = item[0].ToString();
                            newFare.Unit_Cost = cost;
                            InsertNewFare(newFare);
                        }
                    }

                }
                else
                {
                    functions.ShowMessage(this, this.GetType(), msg_C_Shift_UnitCost, MessageType.Error);
                }
            }
            catch (Exception ex)
            {

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        /// <summary>
        /// Call InsertObj function from generic class to insert a new row
        /// </summary>
        protected void saveShift()
        {
            try
            {
                GenericClass.SQLInsertObj(GetNewShift());

                functions.ShowMessage(this, this.GetType(), msg_C_Shift_AddedShift + tbShiftName.Text, MessageType.Success);
                cleanSearchAndSorting();
            }
            catch (Exception ex)
            {

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }


        }

        /// <summary>
        /// Get current values of a shift
        /// </summary>
        /// <returns>Shift object searched</returns>
        protected Shift GetCurrentShift()
        {
            DateTime currentDateTimeShift = new DateTime();

            Shift currentShift = new Shift();
            try
            {
                DataTable getShift = GenericClass.SQLSelectObj(currentShift, WhereClause: "Where Shift_ID='" + tbShiftID.Text + "'");

                currentShift.Shift_ID = getShift.Rows[0]["Shift_ID"].ToString();

                currentShift.Name = getShift.Rows[0]["Name"].ToString();

                DateTime.TryParseExact(getShift.Rows[0]["Start_Time"].ToString(), "HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out currentDateTimeShift);

                TimeSpan cuttentTSShift = new TimeSpan(currentDateTimeShift.Hour, currentDateTimeShift.Minute, currentDateTimeShift.Second);

                currentShift.Start_Time = cuttentTSShift;

                currentShift.Special_Request = Convert.ToBoolean(getShift.Rows[0]["Special_Request"].ToString());

                if (!String.IsNullOrWhiteSpace(getShift.Rows[0]["Special_Service"].ToString()))
                {
                    currentShift.Special_Service = getShift.Rows[0]["Special_Service"].ToString();
                }

                currentShift.Invoice_GroupName = getShift.Rows[0]["Invoice_GroupName"].ToString();

                currentShift.Exit_Shift = Convert.ToBoolean(getShift.Rows[0]["Exit_Shift"].ToString());
            }
            catch (Exception ex)
            {

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }


            return currentShift;
        }

        /// <summary>
        /// Get values inserted by user
        /// </summary>
        /// <returns>Shift object with values given by user</returns>
        protected Shift GetNewShift()
        {
            DateTime dateTimeShift = new DateTime();

            Shift newShift = new Shift();
            try
            {

                newShift.Shift_ID = tbShiftID.Text;

                newShift.Name = tbShiftName.Text;

                DateTime.TryParseExact(tbTime.Text, "h:mm tt", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateTimeShift);

                TimeSpan tsShift = new TimeSpan(dateTimeShift.Hour, dateTimeShift.Minute, dateTimeShift.Second);

                newShift.Start_Time = tsShift;

                newShift.Special_Request = chkSpecial.Checked;

                if (chkSpecial.Checked)
                {
                    newShift.Special_Service = tbSpecialService.Text;
                }
                else
                {
                    tbUnitCost.Text = null;
                }

                newShift.Invoice_GroupName = tbInvoiceGroup.Text;

                newShift.Exit_Shift = chkExit.Checked;
            }
            catch (Exception ex)
            {

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            return newShift;

        }
        protected void updateShift()
        {
            try
            {
                
                Admin_Approve newApprove = new Admin_Approve();

                Shift oldValuesShift = GetCurrentShift();

                Shift newValuesShift = GetNewShift();

                newApprove = adminApprove.compareObjects(oldValuesShift, newValuesShift);

                if (newApprove.New_Values != "No values changed")
                {
                    GenericClass.SQLUpdateObj(new Shift(), adminApprove.compareObjects(oldValuesShift, newValuesShift, ""), "Where Shift_ID='" + newValuesShift.Shift_ID + "'");

                    functions.ShowMessage(this, this.GetType(), msg_C_Shift_UpdatedShift + tbShiftName.Text, MessageType.Success);
                    cleanSearchAndSorting();
                }
                else
                {
                    functions.ShowMessage(this, this.GetType(), msg_C_Shift_NoChanges, MessageType.Info);
                }


            }
            catch (Exception ex)
            {

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }


        protected void deleteShift(string shiftID)
        {
            try
            {
                
                GenericClass.SQLDeleteObj(new Shift(), "Where Shift_ID='" + shiftID + "'");
                functions.ShowMessage(this, this.GetType(), msg_C_Shift_DeletedShift + tbShiftName.Text, MessageType.Success);
                cleanSearchAndSorting();
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        /// <summary>
        /// Add,Reset,Save,Delete
        /// </summary>
        private void ProcessFlow()
        {


            if (this.btAdd.Visible == true || (this.btAdd.Visible == false & this.btSave.Visible == false))
            {
                tbShiftID.Enabled = false;

                tbShiftName.Enabled = false;

                tbInvoiceGroup.Enabled = false;

                tbTime.Enabled = false;

                chkExit.Enabled = false;

                chkSpecial.Enabled = false;

                tbShiftID.Text = string.Empty;

                tbShiftName.Text = string.Empty;

                tbInvoiceGroup.Text = string.Empty;

                tbTime.Text = string.Empty;

                tbSpecialService.Text = string.Empty;

                chkExit.Checked = false;

                chkSpecial.Checked = false;

                dtgShift.Columns[0].Visible = true;

                privilege.applyPrivilege("10001", dtgShift);

                tbSearch.Enabled = true;

                btSearchCH1.Enabled = true;

                btExcel.Enabled = true;

            }
            if (this.btSave.Visible == true && tbShiftID.Text == string.Empty)
            {

                tbShiftID.Enabled = true;

                tbShiftName.Enabled = true;

                tbInvoiceGroup.Enabled = true;

                tbTime.Enabled = true;

                chkExit.Enabled = true;

                chkSpecial.Enabled = true;

                dtgShift.Columns[0].Visible = false;

                tbSearch.Enabled = false;

                btSearchCH1.Enabled = false;

                btExcel.Enabled = false;

                btDelete.Visible = false;


            }
            if (this.btSave.Visible == true && tbShiftID.Text != string.Empty)
            {

                tbShiftID.Enabled = false;

                tbShiftName.Enabled = true;

                tbInvoiceGroup.Enabled = true;

                tbTime.Enabled = true;

                chkExit.Enabled = true;

                chkSpecial.Enabled = true;

                tbSearch.Enabled = false;

                btSearchCH1.Enabled = false;

                btExcel.Enabled = false;

                dtgShift.Columns[0].Visible = false;
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

            msg_C_Shift_UnitCost = (string)GetGlobalResourceObject(language, "msg_C_Shift_UnitCost");
            msg_C_Shift_AddedShift = (string)GetGlobalResourceObject(language, "msg_C_Shift_AddedShift");
            msg_C_Shift_UpdatedShift = (string)GetGlobalResourceObject(language, "msg_C_Shift_UpdatedShift");
            msg_C_Shift_NoChanges = (string)GetGlobalResourceObject(language, "msg_C_Shift_NoChanges");
            msg_C_Shift_DeletedShift = (string)GetGlobalResourceObject(language, "msg_C_Shift_DeletedShift");
            msg_C_Shift_SpecialService = (string)GetGlobalResourceObject(language, "msg_C_Shift_SpecialService");


        }

        #endregion

        #region Events

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

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
                //Set buttons to initial mode
                PrivilegeAndProcessFlow("10001");

                ViewState["searchStatus"] = false;

                ViewState["isSorting"] = false;

            }

            if ((bool)ViewState["searchStatus"] == false && (bool)ViewState["isSorting"] == false)
            {
                filldtgShift();
               
            }

            applyLanguage();
            translateMessages(language);

        }


        protected void btAdd_Click(object sender, EventArgs e)
        {

            PrivilegeAndProcessFlow("01111");
            cleanSearchAndSorting();
            filldtgShift();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
        }

        protected void btReset_Click(object sender, EventArgs e)
        {
            PrivilegeAndProcessFlow("10001");
            cleanSearchAndSorting();
            filldtgShift();
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "", true);

            deleteShift(tbShiftID.Text);

            PrivilegeAndProcessFlow("10001");

            filldtgShift();
            cleanSearchAndSorting();
        }

        protected void btExcel_Click(object sender, EventArgs e)
        {
            HttpContext.Current.ApplicationInstance.CompleteRequest();

            Response.Redirect("./ExportToExcel.aspx?btn=excel", false);
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            cleanSearchAndSorting();
            if ((chkSpecial.Checked & !String.IsNullOrWhiteSpace(tbSpecialService.Text) & !String.IsNullOrWhiteSpace(tbUnitCost.Text))
             || (!chkSpecial.Checked & String.IsNullOrWhiteSpace(tbSpecialService.Text) & String.IsNullOrWhiteSpace(tbUnitCost.Text)))
            {

                if (tbShiftID.Enabled)
                {
                    if (chkSpecial.Checked)
                    {
                        GetNewService(tbSpecialService.Text.Trim());
                        GetNewFare(tbSpecialService.Text.Trim(), Convert.ToDouble(tbUnitCost.Text));
                    }

                    saveShift();

                }

                else
                {
                    if (chkSpecial.Checked)
                    {
                        GetNewService(tbSpecialService.Text.Trim());
                        GetNewFare(tbSpecialService.Text.Trim(), Convert.ToDouble(tbUnitCost.Text));
                    }

                    updateShift();

                }

                tbUnitCost.Text = string.Empty;



            }
            else if (!chkSpecial.Checked)
            {

                tbSpecialService.Text = null;
                tbUnitCost.Text = null;
            }
            else 
            {
                functions.ShowMessage(this, this.GetType(), msg_C_Shift_SpecialService, MessageType.Error);
            }


            PrivilegeAndProcessFlow("10001");

            filldtgShift();
        }


        protected void dtgShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectRowFromGrid(dtgShift, dtgShift.SelectedRow.RowIndex);

            PrivilegeAndProcessFlow("01111");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
        }


        protected void dtgShift_Sorting(object sender, GridViewSortEventArgs e)
        {
            srotingGrid(dtgShift, e);
        }

        protected void dtgShift_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            pagination(dtgShift, e.NewPageIndex);
        }

        protected void chkSpecial_CheckedChanged(object sender, EventArgs e)
        {

            if (chkSpecial.Checked)
            {
                tbSpecialService.Text = string.Empty;

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showNewFareDialog();", true);
            }
            //else
            //{ 
            
            
            //}

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
        }

        protected void btSearchCH1_Click1(object sender, EventArgs e)
        {
            search(dtgShift);
        }

        protected void btSaveUnitCost_Click(object sender, EventArgs e)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "closeDialog();", true);
        }

        #endregion
    }
}