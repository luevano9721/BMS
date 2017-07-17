using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusManagementSystem.Class;
using BusManagementSystem.Catalogos;

namespace BusManagementSystem._01Catalogos
{
    public partial class C_Bus : System.Web.UI.Page
    {
        #region Variables

        private Users usr;

        private Bus initialBus = new Bus();

        private string vendorFilter;

        private DataTable dtNew = new DataTable();

        private AddPrivilege privilege = new AddPrivilege();

        string msg_Bus_Added;
        string msg_Bus_Waitting;
        string msg_Bus_BlacklistInsr;
        string msg_Bus_BlacklistDel;
        string msg_Bus_Update;
        string msg_Bus_NoChanges;
        string msg_Bus_Deleted;
        string msg_Bus_NoPermission;
        string msg_Bus_NoDrivers;
        string language = null;




        #endregion

        #region Methods

        public void PrivilegeAndProcessFlow(string turnOnOffBNuttons)
        {
            privilege.applyPrivilege(turnOnOffBNuttons, GridView_buses);
            ProcessFlow();

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
        public DataTable generalDataTable(string vendor = "")
        {
            Session["Excel"] = null;

            Session["name"] = string.Empty;

            DataTable dtReturn = new DataTable();
            try
            {

                string vendorFilter = usr.Vendor_ID.Equals("ALL") ? "" : "Where [Bus].[Vendor_ID]= '" + usr.Vendor_ID + "'";

                string vendorFilterAdmin = vendor.Equals("ALL") ? "" : "Where [Bus].[Vendor_ID]= '" + vendor + "'";

                dtReturn = GenericClass.SQLSelectObj(initialBus, mappingQueryName: "Catalog", 
                    WhereClause: vendorFilter.Equals("") ? vendorFilterAdmin.Equals("") ? null : vendorFilterAdmin : vendorFilter,
                    OrderByClause: "order by Bus.Is_Active desc, Bus.Is_Block");

                Session["Excel"] = dtReturn;

                Session["name"] = "Buses";
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            return dtReturn;
        }

        private void fillGridBuses()
        {
            DataTable getData = generalDataTable(cbVendorAdm.SelectedValue);

            if (getData.Rows.Count > 0)
            {
                GridView_buses.DataSource = getData;

                ViewState["backupData"] = getData;

                GridView_buses.PageSize = 15;
                GridView_buses.DataBind();
            }
            else
            {
                GridView_buses.DataSource = null;
                GridView_buses.DataBind();
            }
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

                tbVIN.Enabled = false;

                tbCapacity.Enabled = false;

                tbModel.Enabled = false;

                tbMaker.Enabled = false;

                tbLine.Enabled = false;

                tbLicenseP.Enabled = false;

                tbGPS.Enabled = false;

                cbActiveItem.Enabled = false;

                cbBlackListItem.Enabled = false;

                tbSearchCH1.Enabled = true;

                btSearchCH1.Enabled = true;

                btExcel.Enabled = true;

                busID.Enabled = false;

                tbVIN.Text = string.Empty;

                tbCapacity.Text = string.Empty;

                tbModel.Text = string.Empty;

                tbMaker.Text = string.Empty;

                tbLine.Text = string.Empty;

                tbLicenseP.Text = string.Empty;

                tbGPS.Text = string.Empty;

                cbActiveItem.Checked = false;

                cbBlackListItem.Checked = false;

                cbVendorAdm.Enabled = true;

                busID.Text = string.Empty;

                GridView_buses.Columns[0].Visible = true;

                privilege.applyPrivilege("10001", GridView_buses);

            }
            if (this.btSave.Visible == true)
            {

                cbVendor.Enabled = true;

                tbVIN.Enabled = true;

                tbCapacity.Enabled = true;

                tbModel.Enabled = true;

                tbMaker.Enabled = true;

                tbLine.Enabled = true;

                tbLicenseP.Enabled = true;

                tbGPS.Enabled = true;

                cbActiveItem.Enabled = true;

                GridView_buses.Columns[0].Visible = false;

                if (usr.Rol_ID.Contains("ADMIN"))
                {
                    cbBlackListItem.Enabled = true;
                }

                busID.Enabled = true;

                cbVendorAdm.Enabled = false;

                tbSearchCH1.Enabled = false;

                btSearchCH1.Enabled = false;

                btExcel.Enabled = false;
            }
            if (this.btDelete.Visible == true)
            {
                busID.Enabled = false;

            }
        }
        private void saveBus()
        {
            try
            {
                Bus addBus = GetNewBus();
                cleanSearchAndSorting();

                if (usr.Rol_ID.Contains("ADMIN"))
                {
                    GenericClass.SQLInsertObj(addBus);

                    functions.ShowMessage(this, this.GetType(), msg_Bus_Added + " " + busID.Text.Trim(), MessageType.Success);

                    if (cbBlackListItem.Checked == true)
                    {
                        InsertBlackList();
                    }


                }
                else
                {
                    Admin_Approve confirmInsert = new Admin_Approve(0, DateTime.Now, usr.User_ID.ToString(), "INSERT", addBus.GetType().Name, "", GenericClass.formatValues(addBus), "", "Nuevo autobus ha sido agregado: " + addBus.Bus_ID);
                    GenericClass.SQLInsertObj(confirmInsert);
                    SendEmail.sendEmailToAdmin(usr.User_ID.ToString(), confirmInsert);
                    SendEmail.sendEmailToDistributionList(addBus.Vendor_ID, "Catalogs_Email", confirmInsert, usr.User_ID.ToString());
                    functions.ShowMessage(this, this.GetType(), msg_Bus_Waitting + " " + busID.Text.Trim(), MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        private Bus GetNewBus()
        {
            //fill new bus object with new values
            Bus updateBus = new Bus();

            updateBus.Bus_ID = busID.Text.ToString().Trim();

            updateBus.Vendor_ID = cbVendor.SelectedValue.ToString();

            updateBus.VIN = tbVIN.Text.Trim();

            updateBus.Capacity = tbCapacity.Text.Trim();

            updateBus.Model = tbModel.Text.Trim();

            updateBus.Make = tbMaker.Text.Trim();

            updateBus.Line = tbLine.Text.ToString();

            updateBus.License_Pl = tbLicenseP.Text.Trim();

            updateBus.GPS_ID = tbGPS.Text.Trim();

            updateBus.Is_Active = cbActiveItem.Checked;

            updateBus.Is_Block = cbBlackListItem.Checked;


            return updateBus;
        }

        private Bus GetOldBus()
        {
            DataTable getBus = GenericClass.SQLSelectObj(initialBus, mappingQueryName: "getBus", WhereClause: " Where  [VENDOR].[Vendor_ID]= '"
                + cbVendor.SelectedValue + "'" + "and  [BUS].[Bus_ID]='" + busID.Text.Trim() + "'");

            Bus currentBus = new Bus();

            currentBus.Bus_ID = getBus.Rows[0]["Bus_ID"].ToString();

            currentBus.Vendor_ID = getBus.Rows[0]["Vendor_ID"].ToString();

            currentBus.VIN = getBus.Rows[0]["VIN"].ToString();

            currentBus.Capacity = getBus.Rows[0]["Capacity"].ToString();

            currentBus.Model = getBus.Rows[0]["Model"].ToString();

            currentBus.Make = getBus.Rows[0]["Make"].ToString();

            currentBus.Line = getBus.Rows[0]["Line"].ToString();

            currentBus.License_Pl = getBus.Rows[0]["License_Pl"].ToString();

            currentBus.GPS_ID = getBus.Rows[0]["GPS_ID"].ToString();

            currentBus.Is_Active = Convert.ToBoolean(getBus.Rows[0]["Is_Active"].ToString());

            currentBus.Is_Block = Convert.ToBoolean(getBus.Rows[0]["Is_Block"].ToString());


            return currentBus;
        }

        protected void InsertBlackList()
        {
            Blacklist blObj = new Blacklist();

            blObj.Bus_ID = busID.Text.ToString().Trim();

            blObj.BlackList_Date = System.DateTime.Now;

            blObj.Vendor_ID = cbVendor.SelectedValue;

            blObj.Reason = tbReasons.Text;

            blObj.Comments = tbComments.Text;

            GenericClass.SQLInsertObj(blObj);

            functions.ShowMessage(this, this.GetType(), msg_Bus_BlacklistInsr + " ", MessageType.Success);
        }

        protected void UpdateBlackList()
        {
            Blacklist blObj = new Blacklist();

            blObj.Bus_ID = busID.Text.ToString().Trim();

            GenericClass.SQLDeleteObj(blObj, WhereClause: "WHERE BUS_ID= '" + blObj.Bus_ID + "'");

            functions.ShowMessage(this, this.GetType(), msg_Bus_BlacklistDel + " ", MessageType.Success);
        }

        protected void IntoBlackList(bool newBus_isBlacklist)
        {
            Blacklist blObjUpd = new Blacklist();

            DataTable getBlacklistRecord = GenericClass.SQLSelectObj(blObjUpd, WhereClause: "WHERE BUS_ID='" + busID.Text.Trim() + "'");

            if (newBus_isBlacklist)
            {
                if (getBlacklistRecord.Rows.Count <= 0)
                {
                    InsertBlackList();

                }
            }
            else
            {
                if (getBlacklistRecord.Rows.Count > 0)
                {
                    UpdateBlackList();
                }

            }

        }

        private void updateBus()
        {
            try
            {
                cleanSearchAndSorting();
                string idBus = busID.Text.ToString().Trim();

                Admin_Approve newApprove = new Admin_Approve();

                Bus currentBus = GetOldBus();

                Bus updateBus = GetNewBus();

                newApprove = adminApprove.compareObjects(currentBus, updateBus);

                if (newApprove.New_Values != "No values changed")
                {

                    if (usr.Rol_ID.Contains("ADMIN"))
                    {
                        //Get current value of Blacklist field in database
                        if (currentBus.Is_Block != updateBus.Is_Block)
                        {

                            IntoBlackList(updateBus.Is_Block == true);
                        }

                        GenericClass.SQLUpdateObj(updateBus, adminApprove.compareObjects(currentBus, updateBus, ""), "Where Bus_ID='" + updateBus.Bus_ID + "'");

                        functions.ShowMessage(this, this.GetType(), msg_Bus_Update + " " + busID.Text.Trim(), MessageType.Success);



                    }
                    else
                    {
                        newApprove.Activity_ID = 0;

                        newApprove.Admin_Confirm = false;

                        newApprove.Activity_Date = DateTime.Now;

                        newApprove.User_ID = usr.User_ID;

                        newApprove.Type = "UPDATE";

                        newApprove.Module = updateBus.GetType().Name;

                        newApprove.Where_Clause = "Where [BUS].[Bus_ID]='" + idBus + "'";

                        newApprove.Comments = "[Bus_ID][" + idBus + "]<br>" + newApprove.Comments;

                        GenericClass.SQLInsertObj(newApprove);

                        SendEmail.sendEmailToAdmin(usr.User_ID.ToString(), newApprove);
                        SendEmail.sendEmailToDistributionList(currentBus.Vendor_ID, "Catalogs_Email", newApprove, usr.User_ID.ToString());
                        functions.ShowMessage(this, this.GetType(), msg_Bus_Waitting + " " + busID.Text.Trim(), MessageType.Warning);
                    }
                }
                else
                {
                    functions.ShowMessage(this, this.GetType(), msg_Bus_NoChanges + " ", MessageType.Info);
                }
            }

            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
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
                    dtg.DataSource = Session["sortObjects_Bus"];
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

                DataTable dt = generalDataTable(cbVendorAdm.SelectedValue);

                DataView sortedView = new DataView(dt);

                sortedView.Sort = gridSorting.SortExpression + " " + sortingDirection;

                Session["sortObjects_Bus"] = sortedView;

                dtg.DataSource = sortedView;

                dtg.DataBind();
            }
            catch (Exception ex)
            {



                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        private void search()
        {
            try
            {
                GridView_buses.PageIndex = 0;

                string searchTerm = tbSearchCH1.Text.ToLower();

                if (string.IsNullOrEmpty(searchTerm))
                {
                    fillGridBuses();

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
                        if (row["Bus_ID"].ToString().ToLower().Contains(searchTerm) || row["Model"].ToString().ToLower().Contains(searchTerm) || row["License_Pl"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtNew.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    GridView_buses.DataSource = dtNew;

                    ViewState["searchResults"] = dtNew;

                    GridView_buses.DataBind();
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void FillVendorAndAdmin(DataTable distincVendor, bool is_AllVendor)
        {
            if (is_AllVendor)
            {
                cbVendorAdm.Enabled = true;
            }
            else
            {
                cbVendorAdm.Enabled = false;

            }

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
                ListItem li = new ListItem("ALL", "ALL");

                cbVendorAdm.Items.Insert(0, li);

                cbVendor.Items.Insert(0, li);
            }

            cbVendorAdm.SelectedIndex = 0;

            cbVendor.SelectedValue = cbVendorAdm.SelectedValue;
        }

        protected DataTable GetVendors(Users usr)
        {

            vendorFilter = usr.Vendor_ID.Equals("ALL") ? "" : "Where [Vendor_ID]= '" + usr.Vendor_ID + "'";

            Vendor initialVendor = new Vendor();

            DataTable dt = GenericClass.SQLSelectObj(initialVendor, WhereClause: string.IsNullOrWhiteSpace(vendorFilter) ? null : vendorFilter);

            DataView dvVendor = new DataView(dt);

            return dvVendor.ToTable(true, "VENDOR_ID", "NAME");
        }

        protected void DeleteBus(Users usr)
        {
            Bus deleteBus = new Bus();

            deleteBus.Bus_ID = busID.Text.Trim();

            if (usr.Rol_ID.Contains("ADMIN"))
            {
                GenericClass.SQLDeleteObj(initialBus, "Where Bus_ID='" + deleteBus.Bus_ID + "'");

                functions.ShowMessage(this, this.GetType(), msg_Bus_Deleted + " " + busID.Text.Trim(), MessageType.Success);

            }
            else
            {
                Admin_Approve confirmInsert = new Admin_Approve(0, DateTime.Now, usr.User_ID.ToString(), "DELETE", initialBus.GetType().Name, "", "", "Where Bus_ID='" + deleteBus.Bus_ID + "'", "El siguiente autobus ha sido eliminado: " + deleteBus.Bus_ID);

                GenericClass.SQLInsertObj(confirmInsert);

                SendEmail.sendEmailToAdmin(usr.User_ID.ToString(), confirmInsert);
                DataTable getVendor = GenericClass.GetCustomData("SELECT VENDOR_ID FROM BUS WHERE BUS_ID='" + deleteBus.Bus_ID + "'");
                string Vendor_ID = getVendor.Rows[0]["VENDOR_ID"].ToString();
                SendEmail.sendEmailToDistributionList(Vendor_ID, "Catalogs_Email", confirmInsert, usr.User_ID.ToString());
                functions.ShowMessage(this, this.GetType(), msg_Bus_Waitting + " " + busID.Text.Trim(), MessageType.Warning);
            }
        }

        #endregion

        #region Events

        protected void Page_Init(object sender, EventArgs e)
        {

            try
            {
                usr = (Users)(Session["C_USER"]);

                if (Session["C_USER"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }

                //Load buttons to hide or unhide for privileges
                privilege = new AddPrivilege("10001", btAdd, btReset, btSave, btDelete, btExcel);

                DataTable distincVendor = GetVendors(usr);

                FillVendorAndAdmin(distincVendor, usr.Vendor_ID.Equals("ALL"));


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

                fillGridBuses();

            applyLanguage();
            if (!string.IsNullOrEmpty(language))
                translateMessages(language);
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

            msg_Bus_Added = (string)GetGlobalResourceObject(language, "msg_Bus_Added");
            msg_Bus_Waitting = (string)GetGlobalResourceObject(language, "msg_Bus_Waitting");
            msg_Bus_BlacklistInsr = (string)GetGlobalResourceObject(language, "msg_Bus_BlaclistInsr");
            msg_Bus_BlacklistDel = (string)GetGlobalResourceObject(language, "msg_Bus_BlacklistDel");
            msg_Bus_Update = (string)GetGlobalResourceObject(language, "msg_Bus_Update");
            msg_Bus_NoChanges = (string)GetGlobalResourceObject(language, "msg_Bus_NoChanges");
            msg_Bus_Deleted = (string)GetGlobalResourceObject(language, "msg_Bus_Deleted");
            msg_Bus_NoPermission = (string)GetGlobalResourceObject(language, "msg_Bus_NoPermission");
            msg_Bus_NoDrivers = (string)GetGlobalResourceObject(language, "msg_Bus_NoDrivers");

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

            fillGridBuses();
            cleanSearchAndSorting();
            cbVendorAdm.Enabled = true;
            tbReasons.Text = null;
            tbComments.Text = null;
        }

        protected void btSave_Click(object sender, EventArgs e)
        {

            if (busID.Enabled == true)
            {
                saveBus();
            }

            else
            {
                updateBus();
            }
            tbReasons.Text = string.Empty;
            tbComments.Text = string.Empty;
            PrivilegeAndProcessFlow("10001");

            fillGridBuses();
        }

        protected void btExcel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Download", "GotoDownloadPage();", true);
        }

        protected void btClose_Click(object sender, EventArgs e)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "closeDialog();", true);
        }

        protected void btSearchCH1_Click(object sender, EventArgs e)
        {
            search();
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "", true);
            try
            {
                if (Session["C_USER"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                Users usr = (Users)(Session["C_USER"]);

                DeleteBus(usr);

                PrivilegeAndProcessFlow("10001");

                fillGridBuses();

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }



        protected void GridView_buses_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = GridView_buses.SelectedRow;
            try
            {
                busID.Text = GridView_buses.Rows[gvRow.RowIndex].Cells[1].Text;

                functions selectFunction = new functions();

                selectFunction.SelectDropdownList(cbVendor, GridView_buses.Rows[gvRow.RowIndex].Cells[2].Text.ToString().Trim());

                tbVIN.Text = HttpUtility.HtmlDecode(GridView_buses.Rows[gvRow.RowIndex].Cells[3].Text);

                tbCapacity.Text = HttpUtility.HtmlDecode(GridView_buses.Rows[gvRow.RowIndex].Cells[4].Text);

                tbModel.Text = HttpUtility.HtmlDecode(GridView_buses.Rows[gvRow.RowIndex].Cells[5].Text);

                tbMaker.Text = HttpUtility.HtmlDecode(GridView_buses.Rows[gvRow.RowIndex].Cells[6].Text);

                tbLine.Text = HttpUtility.HtmlDecode(GridView_buses.Rows[gvRow.RowIndex].Cells[7].Text);

                tbLicenseP.Text = HttpUtility.HtmlDecode(GridView_buses.Rows[gvRow.RowIndex].Cells[8].Text);

                tbGPS.Text = HttpUtility.HtmlDecode(GridView_buses.Rows[gvRow.RowIndex].Cells[9].Text);

                if (((CheckBox)GridView_buses.Rows[gvRow.RowIndex].Cells[10].Controls[0]).Checked == true)
                {
                    cbActiveItem.Checked = true;
                }

                else
                {
                    cbActiveItem.Checked = false;
                }
                if (((CheckBox)GridView_buses.Rows[gvRow.RowIndex].Cells[11].Controls[0]).Checked == true)
                {
                    cbBlackListItem.Checked = true;
                }
                else
                {
                    cbBlackListItem.Checked = false;
                }

                PrivilegeAndProcessFlow("01111");

                cbVendorAdm.Enabled = false;

                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void GridView_buses_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox c = (CheckBox)e.Row.Cells[11].Controls[0];

                CheckBox inactive = (CheckBox)e.Row.Cells[10].Controls[0];

                if (c.Checked == true)
                {
                    e.Row.CssClass = "highlightRow"; // ...so highlight it
                }

                if (inactive.Checked == false & c.Checked == false)
                {
                    e.Row.CssClass = "highlightInactiveRow"; // ...so highlight it
                }
            }
        }

        private void cleanSearchAndSorting()
        {
            ViewState["searchStatus"] = false;
            ViewState["isSorting"] = false;
            tbSearchCH1.Text = string.Empty;

        }

        protected void GridView_buses_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            pagination(GridView_buses, e.NewPageIndex);
        }

        protected void GridView_buses_Sorting(object sender, GridViewSortEventArgs e)
        {
            srotingGrid(GridView_buses, e);

        }

        protected void GridView_buses_DataBound(object sender, EventArgs e)
        {
            functions.verifyIfBlock(GridView_buses, this, 11);

            functions.verifyIfInactive(GridView_buses, this, 10);
        }

        protected void cbVendorAdm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                GridView_buses.PageIndex = 0;

                fillGridBuses();

                PrivilegeAndProcessFlow("10001");

                cbVendor.SelectedValue = cbVendorAdm.SelectedValue;
                cleanSearchAndSorting();

                
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void cbBlackListItem_CheckedChanged(object sender, EventArgs e)
        {
            Users usr = (Users)(Session["C_USER"]);

            if (usr.Rol_ID.Contains("ADMIN"))
            {

                CheckBox cbBlackListItem = (CheckBox)sender;

                if (cbBlackListItem.Checked == true)
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "openGenericDialog('#blackListDialog' , 330 , 370 , 'Confirmacion de lista negra');", true);
                }
                else
                {
                    tbReasons.Text = string.Empty;

                    tbComments.Text = string.Empty;
                }
            }
            else
            {
                functions.ShowMessage(this, this.GetType(), msg_Bus_NoPermission + " ", MessageType.Warning);

                CheckBox cb = (CheckBox)sender;

                if (cb.Checked == true)
                {
                    cb.Checked = false;
                }

                else
                {
                    cb.Checked = true;
                }

            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
        }

        protected void btHelp_Click(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("./Login.aspx");
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "openGenericDialog('#dialogHelp' , 700 , 600 , 'Ventana de Ayuda');", true);
        }

        #endregion


    }
}