using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusManagementSystem.Class;

namespace BusManagementSystem._01Catalogos
{
    public partial class C_Fare : System.Web.UI.Page
    {
        #region Private Variables

        Fare initialFare = new Fare();

        string vendorFilter;

        private AddPrivilege privilege = new AddPrivilege();
        string language = null;
        string msg_Fare_Add;
        string msg_Fare_Update;
        string msg_Fare_Delete;
        string msg_Fare_AddService;
        string msg_Fare_NoDelete;
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
                privilege = new AddPrivilege("10001", btAdd, btReset, btSave, btDelete, btExcel);

                vendorFilter = usr.Vendor_ID.Equals("ALL") ? "" : "Where [Vendor_ID]= '" + usr.Vendor_ID + "'";

                Vendor initialVendor = new Vendor();
                DataTable dt = GenericClass.SQLSelectObj(initialVendor, WhereClause: string.IsNullOrWhiteSpace(vendorFilter) ? null : vendorFilter);
                DataView dvVendor = new DataView(dt);
                DataTable distincVendor = dvVendor.ToTable(true, "VENDOR_ID", "NAME");


                if (usr.Vendor_ID.Equals("ALL"))
                {
                    cbVendorAdm.Enabled = true;
                }
                else
                {
                    cbVendorAdm.Enabled = false;

                }
                string prevSelect = cbVendorAdm.SelectedValue;
                cbVendorAdm.Items.Clear();


                cbVendorAdm.DataSource = distincVendor;
                cbVendorAdm.DataValueField = "VENDOR_ID";
                cbVendorAdm.DataTextField = "NAME";
                cbVendorAdm.DataBind();
                cbVendorAdm.SelectedValue = prevSelect;
                cbVendor.Items.Clear();

                cbVendor.DataSource = distincVendor;
                cbVendor.DataValueField = "VENDOR_ID";
                cbVendor.DataTextField = "NAME";
                cbVendor.DataBind();


                cbService.Items.Clear();
                Service initialService = new Service();
                DataView dvService = new DataView(GenericClass.SQLSelectObj(initialService, mappingQueryName: "Catalog"));
                DataTable distincVService = dvService.ToTable(true, "Service_ID");
                cbService.DataSource = distincVService;
                cbService.DataValueField = "Service_ID";
                cbService.DataTextField = "Service_ID";
                try
                {
                    cbService.DataBind();
                    cbService.SelectedIndex = 0;
                    btSave.Enabled = true;
                }
                catch (Exception)
                {
                    functions.ShowMessage(this, this.GetType(), msg_Fare_AddService + " ", MessageType.Error);
                    btSave.Enabled = false;
                }


                if (usr.Vendor_ID.Equals("ALL"))
                {
                    ListItem li = new ListItem("ALL", "ALL");
                    cbVendorAdm.Items.Insert(0, li);

                    cbVendor.Items.Insert(0, li);

                }

                cbVendorAdm.SelectedIndex = 0;

                cbVendor.SelectedValue = cbVendorAdm.SelectedValue;


            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        protected void Page_Load(object sender, EventArgs e)
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


                //Language

                String Language = null;

                functions func = new functions();
                if (Session["Language"] != null)
                {
                    Language = Session["Language"].ToString();
                }
                else
                {
                    Language = functions.GetLanguage(usr);
                }
                func.languageTranslate(this.Master, Language);


                //Set buttons to initial mode
                PrivilegeAndProcessFlow("10001");


                ViewState["searchStatus"] = false;
                ViewState["isSorting"] = false;
            }

            if ((bool)ViewState["searchStatus"] == false && (bool)ViewState["isSorting"] == false)
                fillGridFares();

            applyLanguage();
            if (!string.IsNullOrEmpty(language))
                translateMessages(language);
        }

        #endregion


        #region button
        protected void btAdd_Click(object sender, EventArgs e)
        {
            PrivilegeAndProcessFlow("01101");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
        }
        protected void btReset_Click(object sender, EventArgs e)
        {
            PrivilegeAndProcessFlow("10001");
            fillGridFares();
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

                if (tbFareID.Text.ToString() == string.Empty)
                {
                    //ADD
                    double unitCost = 0;
                    double.TryParse(tbUnitCost.Text.ToString(), out unitCost);

                    Fare addFare = new Fare();
                    addFare.Vendor_ID = cbVendor.SelectedValue.ToString().Trim();
                    addFare.Service_ID = cbService.SelectedValue.ToString().Trim();
                    addFare.Unit_Cost = unitCost;
                    GenericClass.SQLInsertObj(addFare);
                    functions.ShowMessage(this, this.GetType(), msg_Fare_Add , MessageType.Success);
                }
                else
                {
                    //Update

                    int fareID = 0;
                    int.TryParse(tbFareID.Text.Trim(), out fareID);

                    Fare updateFare = new Fare();
                    updateFare.Fare_ID = fareID;
                    Dictionary<string, dynamic> dicUpdFares = new Dictionary<string, dynamic>();
                    dicUpdFares.Add("Vendor_ID", "'" + cbVendor.SelectedValue.ToString().Trim() + "'");
                    dicUpdFares.Add("Service_ID", "'" + cbService.SelectedValue.ToString().Trim() + "'");
                    dicUpdFares.Add("Unit_Cost", tbUnitCost.Text.ToString());


                    GenericClass.SQLUpdateObj(updateFare, dicUpdFares, "Where Fare_ID=" + updateFare.Fare_ID);
                    functions.ShowMessage(this, this.GetType(), msg_Fare_Update + " " + tbFareID.Text.Trim(), MessageType.Success);
                }

                ResetFunction();

            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void ResetFunction()
        {
            PrivilegeAndProcessFlow("10001");

            ViewState["searchStatus"] = false;

            ViewState["isSorting"] = false;

            tbSearchCH1.Text = string.Empty;

            GridView_Fares.PageIndex = 0;

            GridView_Fares.DataSource = generalDataTable(cbVendorAdm.SelectedValue);

            GridView_Fares.DataBind();
        }


        protected void btDelete_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "", true);
            //Delete
            try
            {
                if (Session["C_USER"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                Users usr = (Users)(Session["C_USER"]);

                int fareID = 0;
                int.TryParse(tbFareID.Text.Trim(), out fareID);
                

                

                Fare deleteFares = new Fare();
                deleteFares.Fare_ID = fareID;
                deleteFares.Service_ID=cbService.SelectedValue;
                deleteFares.Vendor_ID=cbVendor.SelectedValue;

                DataTable IsInvoice = GenericClass.SQLSelectObj(new Invoice(), WhereClause: "Where Service_ID='" +
                    deleteFares.Service_ID + "' And SUBSTRING(Master_Trip_ID,0,4)='" +
                    deleteFares.Vendor_ID + "'");

                if (IsInvoice.Rows.Count==0)
                {
                    GenericClass.SQLDeleteObj(initialFare, "Where Fare_ID=" + deleteFares.Fare_ID);
                    functions.ShowMessage(this, this.GetType(), msg_Fare_Delete + " " + tbFareID.Text.Trim(), MessageType.Success);
                }
                else
                {
                    functions.ShowMessage(this, this.GetType(), msg_Fare_NoDelete, MessageType.Error);
                }

                
                ResetFunction();

            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void btExcel_Click(object sender, EventArgs e)
        {
            execute_query();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.Redirect("~/Catalogos/ExportToExcel.aspx?btn=excel", false);
        }

        protected void btHelp_Click(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("./Login.aspx");
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Tarifas')", true);
        }
        #endregion


        #region GridView
        protected void GridView_Fares_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = GridView_Fares.SelectedRow;


            try
            {
                tbFareID.Text =HttpUtility.HtmlDecode( GridView_Fares.Rows[gvRow.RowIndex].Cells[1].Text);

                tbUnitCost.Text = GridView_Fares.Rows[gvRow.RowIndex].Cells[4].Text;

                functions selectFunction = new functions();

                selectFunction.SelectDropdownList(cbVendor, GridView_Fares.Rows[gvRow.RowIndex].Cells[2].Text.ToString().Trim());

                selectFunction.SelectDropdownList(cbService, GridView_Fares.Rows[gvRow.RowIndex].Cells[3].Text.ToString().Trim());

                PrivilegeAndProcessFlow("01111");
                cbVendor.Focus();
                cbVendorAdm.Enabled = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void GridView_Fares_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_Fares.PageIndex = e.NewPageIndex;
            if ((bool)ViewState["searchStatus"] == true)
            { GridView_Fares.DataSource = ViewState["searchResults"]; }
            if ((bool)ViewState["isSorting"] == true)
            { GridView_Fares.DataSource = Session["objectFare"]; }
            GridView_Fares.DataBind();
        }
        protected void GridView_Fares_Sorting(object sender, GridViewSortEventArgs e)
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
                Session["objectFare"] = sortedView;
                GridView_Fares.DataSource = sortedView;
                GridView_Fares.DataBind();
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        #endregion

        #region ComboBox
        protected void cbVendorAdm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridView_Fares.PageIndex = 0;

                ResetFunction();
                
                cbVendor.SelectedValue = cbVendorAdm.SelectedValue == "ALL" ? cbVendorAdm.Items[1].Value : cbVendorAdm.SelectedValue;
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        protected void cbVendor_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
        #endregion

        #region Methods

        public void PrivilegeAndProcessFlow(string turnOnOffBNuttons)
        {
            privilege.applyPrivilege(turnOnOffBNuttons, GridView_Fares);
            ProcessFlow();

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
            msg_Fare_Add = (string)GetGlobalResourceObject(language, "msg_Fare_Add");
            msg_Fare_Update = (string)GetGlobalResourceObject(language, "msg_Fare_Update");
            msg_Fare_Delete = (string)GetGlobalResourceObject(language, "msg_Fare_Delete");
            msg_Fare_AddService = (string)GetGlobalResourceObject(language, "msg_Fare_AddService");
            msg_Fare_NoDelete = (string)GetGlobalResourceObject(language, "msg_Fare_NoDelete");
           
        }

        protected void btSearchCH1_Click(object sender, EventArgs e)
        {
            search();
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
                GridView_Fares.PageIndex = 0;
                string searchTerm = tbSearchCH1.Text.ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    fillGridFares();
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
                        if (row["Service_ID"].ToString().ToLower().Contains(searchTerm) || row["Unit_Cost"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtNew.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    GridView_Fares.DataSource = dtNew;
                    ViewState["searchResults"] = dtNew;
                    GridView_Fares.DataBind();
                }
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        public DataTable generalDataTable(string vendor = "")
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            Users usr = (Users)(Session["C_USER"]);

            DataTable dtReturn = new DataTable();

            string vendorFilter = usr.Vendor_ID.Equals("ALL") ? "" : "Where [Fare].[Vendor_ID]= '" + usr.Vendor_ID + "'";
            string vendorFilterAdmin = vendor.Equals("ALL") ? "" : "Where [Fare].[Vendor_ID]= '" + vendor + "'";

            dtReturn = GenericClass.SQLSelectObj(initialFare, mappingQueryName: "Catalog", WhereClause: vendorFilter.Equals("") ? vendorFilterAdmin.Equals("") ? null : vendorFilterAdmin : vendorFilter);

            return dtReturn;
        }
        private void fillGridFares()
        {
            try
            {
                DataTable getData = generalDataTable(cbVendorAdm.SelectedValue);
                GridView_Fares.DataSource = getData;
                ViewState["backupData"] = getData;
                GridView_Fares.DataBind();
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        private void ProcessFlow()
        {


            if (this.btAdd.Visible == true || (this.btAdd.Visible == false & this.btSave.Visible == false))
            {

                cbVendor.Enabled = false;

                cbService.Enabled = false;

                tbUnitCost.Enabled = false;

                tbFareID.Text = string.Empty;

                tbUnitCost.Text = string.Empty;

                GridView_Fares.Columns[0].Visible = true;

                privilege.applyPrivilege("10001", GridView_Fares);

                btSearchCH1.Enabled = true;

                tbSearchCH1.Enabled = true;

                btExcel.Enabled = true;

                cbVendorAdm.Enabled = true;

            }
            if (this.btSave.Visible == true || this.btDelete.Visible == true)
            {
                cbVendor.Enabled = true;

                cbService.Enabled = true;

                tbUnitCost.Enabled = true;

                GridView_Fares.Columns[0].Visible = false;

                cbVendorAdm.Enabled = false;

                btExcel.Enabled = false;

                btSearchCH1.Enabled = false;

                tbSearchCH1.Enabled = false;

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
                Session["name"] = "Fares";
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        #endregion




















    }
}