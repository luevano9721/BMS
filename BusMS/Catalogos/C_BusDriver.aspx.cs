using BusManagementSystem.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusManagementSystem._01Catalogos
{
    public partial class C_BusDriver : System.Web.UI.Page
    {
        #region private Variables

        private Bus_Driver initialBusDriver = new Bus_Driver();

        private AddPrivilege privilege = new AddPrivilege();

        private string vendorFilter;

        string language = null;
        string msg_BusDriver_LinkBus;
        string msg_BusDriver_Waitting;
        string msg_BusDriver_Driver;
        string msg_BusDriver_NewBus;
        string msg_BusDriver_Updated;
        string msg_BusDriver_NoChanges;
        string msg_BusDriver_NoActive;

        #endregion


        #region Page
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {

                if (Session["C_USER"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                Users usr = (Users)(Session["C_USER"]);

                //Load buttons to hide or unhide for privileges
                privilege = new AddPrivilege("10001", btAdd, btReset, btSave, null, btExcel);

                vendorFilter = usr.Vendor_ID.Equals("ALL") ? "" : "Where [Vendor_ID]= '" + usr.Vendor_ID + "'";
                if (!IsPostBack)
                {
                    FillcbFilters();

                    fillBusDriver();
                }
                

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["C_USER"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                Users usr = (Users)(Session["C_USER"]);

                //Fill datatable with privileges
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

                    DataTable dtLoad = generalDataTable(cbShift.SelectedValue, cbVendorAdm.SelectedValue);
                    GridView_Drivers.DataSource = dtLoad;
                    ViewState["gridDataVS"] = dtLoad;
                    GridView_Drivers.DataBind();
                }


                applyLanguage();
                if (!string.IsNullOrEmpty(language))
                    translateMessages(language);

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }




        #endregion



        #region button
        protected void btAdd_Click(object sender, EventArgs e)
        {

            cbBusId.Focus();
            PrivilegeAndProcessFlow("01101");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
        }
        protected void btReset_Click(object sender, EventArgs e)
        {
            PrivilegeAndProcessFlow("10001");
            cbVendorAdm.Enabled = true;
            cbShift.Enabled = true;
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

                if (tbBusDriverID.Text.ToString() == string.Empty)
                {
                    //ADD

                    int driverId = 0;
                    int.TryParse(cbDriverId.SelectedValue.ToString(), out driverId);

                    Bus_Driver addBusDriver = new Bus_Driver();
                    addBusDriver.Bus_Driver_ID = tbBusDriverID.Text.Trim();
                    addBusDriver.Vendor_ID = cbVendor.SelectedValue.ToString();
                    addBusDriver.Bus_ID = cbBusId.SelectedValue.ToString();
                    addBusDriver.Driver_ID = driverId;
                    addBusDriver.Shift_id = cbShiftID.SelectedValue.ToString();
                    addBusDriver.Is_Active = cbActive.Checked == true;

                    if (usr.Rol_ID.Contains("ADMIN"))
                    {
                        GenericClass.SQLInsertObj(addBusDriver);
                        functions.ShowMessage(this, this.GetType(), msg_BusDriver_LinkBus + " " + cbBusId.Text.Trim() + " " + msg_BusDriver_Driver + " " + cbDriverId.SelectedValue.ToString(), MessageType.Success);
                    }
                    else
                    {
                        Admin_Approve confirmInsert = new Admin_Approve(0, DateTime.Now, usr.User_ID.ToString(), "INSERT", addBusDriver.GetType().Name, "", GenericClass.formatValues(addBusDriver), "", msg_BusDriver_NewBus + " " + addBusDriver.Bus_ID + "/" + cbDriverId.SelectedItem.ToString());
                        GenericClass.SQLInsertObj(confirmInsert);
                        SendEmail.sendEmailToAdmin(usr.User_ID.ToString(), confirmInsert);
                        SendEmail.sendEmailToDistributionList(addBusDriver.Vendor_ID, "Catalogs_Email", confirmInsert, usr.User_ID.ToString());
                        functions.ShowMessage(this, this.GetType(), msg_BusDriver_Waitting + " " + addBusDriver.Bus_ID + "/" + cbDriverId.SelectedValue.ToString(), MessageType.Warning);
                    }

                }
                else
                {
                    //Update
                    DataTable checkBusDriverActive = GenericClass.SQLSelectObj(initialBusDriver, mappingQueryName: "Catalog",
                              WhereClause: " Where Bus_Driver_ID = '" + tbBusDriverID.Text.Trim() + "' ");
                    if ((Boolean)checkBusDriverActive.Rows[0]["BusActive"] & (Boolean)checkBusDriverActive.Rows[0]["DriverActive"])
                    {

                        //Create and fill current bus object
                        string idBusDriver = tbBusDriverID.Text.Trim();

                        Admin_Approve newApprove = new Admin_Approve();
                        DataTable getBusDriver = GenericClass.SQLSelectObj(initialBusDriver, mappingQueryName: "getBusDriver",
                                                    WhereClause: "Where  [BUS_DRIVER].[Bus_Driver_ID]='" + idBusDriver + "'");
                        Bus_Driver currentBusDriver = new Bus_Driver();
                        currentBusDriver.Is_Active = Convert.ToBoolean(getBusDriver.Rows[0]["Is_Active"].ToString());
                        
                        Bus_Driver updateBusDriver = new Bus_Driver();
                        updateBusDriver.Is_Active = cbActive.Checked;

                        newApprove = adminApprove.compareObjects(currentBusDriver, updateBusDriver);

                        if (newApprove.New_Values != "No values changed")
                        {

                            if (usr.Rol_ID.Contains("ADMIN"))
                            {
                                Dictionary<string, dynamic> dicUpdBusDriver = new Dictionary<string, dynamic>();
                                dicUpdBusDriver.Add("Is_Active", cbActive.Checked);

                                GenericClass.SQLUpdateObj(updateBusDriver, dicUpdBusDriver, " Where Bus_Driver_ID = '" + tbBusDriverID.Text.Trim() + "' "
                                    + "AND Vendor_ID= '" + cbVendor.SelectedValue.ToString() + "' "
                                    + "AND Shift_ID = '" + cbShiftID.SelectedValue.ToString() + "'");
                                functions.ShowMessage(this, this.GetType(), msg_BusDriver_Updated + " " + cbBusId.Text.Trim() + "-" + cbDriverId.SelectedItem.ToString(), MessageType.Success);
                            }
                            else
                            {


                                newApprove.Activity_ID = 0;
                                newApprove.Admin_Confirm = false;
                                newApprove.Activity_Date = DateTime.Now;
                                newApprove.User_ID = usr.User_ID;
                                newApprove.Type = "UPDATE";
                                newApprove.Module = updateBusDriver.GetType().Name;
                                newApprove.Where_Clause = "Where  [BUS_DRIVER].[Bus_Driver_ID]='" + idBusDriver + "'";
                                newApprove.Comments = "[Bus_Driver][" + cbBusId.SelectedItem.Text + "] [" + cbDriverId.SelectedItem.Text + "]<br>" + newApprove.Comments;
                                GenericClass.SQLInsertObj(newApprove);
                                SendEmail.sendEmailToAdmin(usr.User_ID.ToString(), newApprove);
                                SendEmail.sendEmailToDistributionList(currentBusDriver.Vendor_ID, "Catalogs_Email", newApprove, usr.User_ID.ToString());
                                functions.ShowMessage(this, this.GetType(), msg_BusDriver_Waitting + " " + idBusDriver, MessageType.Warning);

                            }

                        }

                        else
                        {
                            functions.ShowMessage(this, this.GetType(), msg_BusDriver_NoChanges, MessageType.Info);
                        }
                    }
                    else
                    {
                        functions.ShowMessage(this, this.GetType(), msg_BusDriver_NoActive, MessageType.Error);
                    }
                }

                ResetFunction();
            }
            catch (Exception ex)
            {
                
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btExcel_Click(object sender, EventArgs e)
        {
            execute_query();
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
            //Response.Redirect("~/Catalogos/ExportToExcel.aspx?btn=excel", false);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Download", "GotoDownloadPage();", true);
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
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Camión / Conductor')", true);
        }

        #endregion


        #region GridView
        protected void GridView_Drivers_SelectedIndexChanged(object sender, EventArgs e)
        {


            try
            {

                GridViewRow gvRow = GridView_Drivers.SelectedRow;

                tbBusDriverID.Text = GridView_Drivers.Rows[gvRow.RowIndex].Cells[1].Text;

                functions selectFunction = new functions();

                selectFunction.SelectDropdownList(cbVendor, GridView_Drivers.Rows[gvRow.RowIndex].Cells[2].Text.ToString().Trim());

                fillBusDriver();

                selectFunction.SelectDropdownList(cbShiftID, GridView_Drivers.Rows[gvRow.RowIndex].Cells[6].Text.ToString().Trim());

                selectFunction.SelectDropdownList(cbBusId, GridView_Drivers.Rows[gvRow.RowIndex].Cells[3].Text.ToString().Trim());

                selectFunction.SelectDropdownList(cbDriverId, GridView_Drivers.Rows[gvRow.RowIndex].Cells[4].Text.ToString().Trim());

                CheckBox cbEnable = (CheckBox)GridView_Drivers.Rows[gvRow.RowIndex].Cells[5].Controls[0];
                cbActive.Checked = cbEnable.Checked;
                PrivilegeAndProcessFlow("01111");
                cbBusId.Focus();
                cbVendorAdm.Enabled = false;
                cbShift.Enabled = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void GridView_Drivers_DataBound(object sender, EventArgs e)
        {
            functions.verifyIfInactive(GridView_Drivers, this, 5);
        }
        protected void GridView_Drivers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_Drivers.PageIndex = e.NewPageIndex;
            if ((bool)ViewState["searchStatus"] == true)
            { GridView_Drivers.DataSource = ViewState["searchResults"]; }
            if ((bool)ViewState["isSorting"] == true)
            { GridView_Drivers.DataSource = Session["objectBusDriver"]; }
            GridView_Drivers.DataBind();
        }
        protected void GridView_Drivers_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                tbSearchCH1.Text = string.Empty;
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
                DataTable dt = generalDataTable(cbShift.SelectedValue, cbVendorAdm.SelectedValue);
                DataView sortedView = new DataView(dt);
                sortedView.Sort = e.SortExpression + " " + sortingDirection;
                Session["objectBusDriver"] = sortedView;
                GridView_Drivers.DataSource = sortedView;
                GridView_Drivers.DataBind();
            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        protected void GridView_Drivers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                CheckBox inactive = (CheckBox)e.Row.Cells[5].Controls[0];


                if (inactive.Checked == false)
                {
                    e.Row.CssClass = "highlightInactiveRow"; // ...so highlight it
                }
            }
        }
        #endregion

        #region CoboBox
        protected void cbVendorAdm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btSave.Enabled = true;
                GridView_Drivers.DataSource = generalDataTable(cbShift.SelectedValue, cbVendorAdm.SelectedValue);
                GridView_Drivers.DataBind();
                cbShiftID.SelectedValue = cbShift.SelectedValue;
                cbVendor.SelectedValue = cbVendorAdm.SelectedValue;
                Bus initialBus = new Bus();
                DataTable dtBus = GenericClass.SQLSelectObj(initialBus, mappingQueryName: "Catalog", WhereClause: "Where [Bus].[Vendor_ID] = '" + cbVendor.SelectedValue + "'");
                cbBusId.Items.Clear();
                cbBusId.DataSource = dtBus;
                cbBusId.DataValueField = "Bus_ID";
                cbBusId.DataTextField = "Bus_ID";


                Driver initialDriver = new Driver();
                DataTable dtDriver = GenericClass.SQLSelectObj(initialDriver, mappingQueryName: "Catalog", WhereClause: "Where [Driver].[Vendor_ID] = '" + cbVendor.SelectedValue + "'");
                cbDriverId.Items.Clear();
                cbDriverId.DataSource = dtDriver;
                cbDriverId.DataValueField = "Driver_ID";
                cbDriverId.DataTextField = "Name";




                try
                {
                    cbBusId.DataBind();
                    cbBusId.SelectedIndex = 0;
                    cbDriverId.DataBind();
                    cbDriverId.SelectedIndex = 0;

                }
                catch (Exception)
                {
                    cbBusId.Items.Add("Bus Not registered");
                    cbDriverId.Items.Add("Driver Not registered");
                    cbShiftID.Items.Add("Shift Not registered");
                    cbBusId.Enabled = false;
                    cbDriverId.Enabled = false;
                    cbShiftID.Enabled = false;
                    btSave.Enabled = false;


                }

                PrivilegeAndProcessFlow("10001");

                ViewState["isSorting"] = false;

                ViewState["searchStatus"] = false;

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void cbShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btSave.Enabled = true;
                GridView_Drivers.DataSource = generalDataTable(cbShift.SelectedValue, cbVendorAdm.SelectedValue);
                GridView_Drivers.DataBind();
                cbShiftID.SelectedValue = cbShift.SelectedValue;

                


                try
                {
                    cbBusId.DataBind();
                    cbBusId.SelectedIndex = 0;
                    cbDriverId.DataBind();
                    cbDriverId.SelectedIndex = 0;

                }
                catch (Exception)
                {
                    cbBusId.Items.Add("Bus Not registered");
                    cbDriverId.Items.Add("Driver Not registered");

                    cbBusId.Enabled = false;
                    cbDriverId.Enabled = false;
                    cbShiftID.Enabled = false;
                    btSave.Enabled = false;

                }
                PrivilegeAndProcessFlow("10001");

                ViewState["isSorting"] = false;

                ViewState["searchStatus"] = false;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        #endregion

        #region Methods

        protected void FillcbFilters()
        {
            Vendor initialVendor = new Vendor();
            DataTable dt = GenericClass.SQLSelectObj(initialVendor, WhereClause: string.IsNullOrWhiteSpace(vendorFilter) ? null : vendorFilter);
            DataView dvVendor = new DataView(dt);
            DataTable distincVendor = dvVendor.ToTable(true, "VENDOR_ID", "NAME");

            cbVendorAdm.Items.Clear();
            cbVendorAdm.DataSource = distincVendor;
            cbVendorAdm.DataValueField = "VENDOR_ID";
            cbVendorAdm.DataTextField = "NAME";
            cbVendorAdm.DataBind();
            cbVendorAdm.SelectedIndex = 0;


            cbVendor.Items.Clear();
            cbVendor.DataSource = distincVendor;
            cbVendor.DataValueField = "VENDOR_ID";
            cbVendor.DataTextField = "NAME";
            cbVendor.DataBind();
            cbVendor.SelectedIndex = 0;

            Shift initialShift = new Shift();
            DataTable dtShift = GenericClass.SQLSelectObj(initialShift, mappingQueryName: "Catalog");

            cbShift.Items.Clear();
            cbShift.DataSource = dtShift;
            cbShift.DataValueField = "SHIFT_ID";
            cbShift.DataTextField = "NAME";
            cbShift.DataBind();
            cbShift.SelectedIndex = 0;

            cbShiftID.Items.Clear();
            cbShiftID.DataSource = dtShift;
            cbShiftID.DataValueField = "Shift_ID";
            cbShiftID.DataTextField = "Name";
            cbShiftID.DataBind();
            cbShiftID.SelectedValue = cbShift.SelectedValue;

        }

        protected void fillBusDriver()
        {
            Bus initialBus = new Bus();
            DataTable dtBus = GenericClass.SQLSelectObj(initialBus, mappingQueryName: "Catalog", WhereClause: "Where [Bus].[Vendor_ID] = '" + cbVendor.SelectedValue + "'");
            cbBusId.Items.Clear();
            cbBusId.DataSource = dtBus;
            cbBusId.DataValueField = "Bus_ID";
            cbBusId.DataTextField = "Bus_ID";
            cbBusId.DataBind();
            cbBusId.SelectedIndex = 0;

            Driver initialDriver = new Driver();
            DataTable dtDriver = GenericClass.SQLSelectObj(initialDriver, mappingQueryName: "Catalog", WhereClause: "Where [Driver].[Vendor_ID] = '" + cbVendor.SelectedValue + "'");
            cbDriverId.Items.Clear();
            cbDriverId.DataSource = dtDriver;
            cbDriverId.DataValueField = "Driver_ID";
            cbDriverId.DataTextField = "Name";
            cbDriverId.DataBind();
            cbDriverId.SelectedIndex = 0;
           

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

        protected void ResetFunction()
        {
            PrivilegeAndProcessFlow("10001");

            ViewState["searchStatus"] = false;

            ViewState["isSorting"] = false;

            tbSearchCH1.Text = string.Empty;

            GridView_Drivers.PageIndex = 0;

            GridView_Drivers.DataSource = generalDataTable(cbShift.SelectedValue, cbVendorAdm.SelectedValue);

            GridView_Drivers.DataBind();
        }

        private void translateMessages(string language)
        {


            msg_BusDriver_LinkBus = (string)GetGlobalResourceObject(language, "msg_BusDriver_LinkBus");
            msg_BusDriver_Waitting = (string)GetGlobalResourceObject(language, "msg_BusDriver_Waitting");
            msg_BusDriver_Driver = (string)GetGlobalResourceObject(language, "msg_BusDriver_Driver");
            msg_BusDriver_NewBus = (string)GetGlobalResourceObject(language, "msg_BusDriver_NewBus");
            msg_BusDriver_Updated = (string)GetGlobalResourceObject(language, "msg_BusDriver_Updated");
            msg_BusDriver_NoChanges = (string)GetGlobalResourceObject(language, "msg_BusDriver_NoChanges");
            msg_BusDriver_NoActive = (string)GetGlobalResourceObject(language, "msg_BusDriver_NoActive");


        }

        public void PrivilegeAndProcessFlow(string turnOnOffBNuttons)
        {
            privilege.applyPrivilege(turnOnOffBNuttons, GridView_Drivers);
            ProcessFlow();

        }

        private void ProcessFlow()
        {

            if (this.btAdd.Visible == true || (this.btAdd.Visible == false & this.btSave.Visible == false))
            {
                cbBusId.Enabled = false;

                cbDriverId.Enabled = false;

                cbActive.Enabled = false;

                cbShiftID.Enabled = false;

                cbVendor.Enabled = false;

                tbBusDriverID.Text = string.Empty;

                GridView_Drivers.Columns[0].Visible = true;

                privilege.applyPrivilege("10001", GridView_Drivers);

                cbShift.Enabled = true;

                btSearchCH1.Enabled = true;

                tbSearchCH1.Enabled = true;

                btExcel.Enabled = true;

                cbVendorAdm.Enabled = true;


            }
            if (this.btSave.Visible == true && tbBusDriverID.Text == string.Empty)
            {
                cbBusId.Enabled = true;

                cbDriverId.Enabled = true;

                cbShiftID.Enabled = true;

                cbActive.Enabled = true;

                cbVendor.Enabled = true;

                cbBusId.SelectedIndex = 0;

                cbDriverId.SelectedIndex = 0;

                cbActive.Checked = true;

                GridView_Drivers.Columns[0].Visible = false;

                btExcel.Enabled = false;

                cbVendorAdm.Enabled = false;

                cbShift.Enabled = false;

                btSearchCH1.Enabled = false;

                tbSearchCH1.Enabled = false;

                //GridView_Drivers.BottomPagerRow.Visible = false;

            }
            if (this.btSave.Visible == true && tbBusDriverID.Text != string.Empty)
            {
                cbBusId.Enabled = false;

                cbDriverId.Enabled = false;

                cbShiftID.Enabled = false;

                cbActive.Enabled = true;

                cbVendor.Enabled = false;

                cbVendorAdm.Enabled = false;

                tbSearchCH1.Enabled = false;

                btSearchCH1.Enabled = false;

                btExcel.Enabled = false;

                GridView_Drivers.Columns[0].Visible = false;

                //GridView_Drivers.BottomPagerRow.Visible = false;
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
                DataTable dtLoad = generalDataTable(cbShift.SelectedValue, cbVendorAdm.SelectedValue);
                Session["Excel"] = dtLoad;
                Session["name"] = "BusDrivers";
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
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
        private void search()
        {
            try
            {
                DataTable dtNew = new DataTable();
                GridView_Drivers.PageIndex = 0;
                string searchTerm = tbSearchCH1.Text.ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    generalDataTable(cbShift.SelectedValue, cbVendorAdm.SelectedValue);
                    dtNew.Clear();
                    ViewState["searchStatus"] = false;
                    ViewState["isSorting"] = false;
                }
                else
                {
                    ViewState["searchStatus"] = true;
                    ViewState["isSorting"] = false;
                    //always check if the viewstate exists before using it
                    if (ViewState["gridDataVS"] == null)
                        return;

                    //cast the viewstate as a datatable
                    DataTable dt = ViewState["gridDataVS"] as DataTable;

                    //make a clone of the datatable
                    dtNew = dt.Clone();

                    //search the datatable for the correct fields
                    foreach (DataRow row in dt.Rows)
                    {
                        //add your own columns to be searched here
                        if (row["Bus_ID"].ToString().ToLower().Contains(searchTerm) || row["Driver"].ToString().ToLower().Contains(searchTerm) || row["Bus_Driver_ID"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtNew.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    GridView_Drivers.DataSource = dtNew;
                    ViewState["searchResults"] = dtNew;
                    GridView_Drivers.DataBind();
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        public DataTable generalDataTable(string shift, string vendor = "")
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            Users usr = (Users)(Session["C_USER"]);

            DataTable dtReturn = new DataTable();

            string vendorFilterAdmin = vendor.Equals("") ? "Where BUS_DRIVER.Shift_ID='" + shift + "'" : "Where BUS_DRIVER.Shift_ID='" + shift + "'" + "And [Bus_Driver].[Vendor_ID]= '" + vendor + "'";

            dtReturn = GenericClass.SQLSelectObj(initialBusDriver, mappingQueryName: "Catalog", WhereClause: vendorFilterAdmin,
                OrderByClause: "order by Bus_Driver.Is_Active desc");

            return dtReturn;
        }


        #endregion

        protected void cbVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillBusDriver();

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
        }

    }
}