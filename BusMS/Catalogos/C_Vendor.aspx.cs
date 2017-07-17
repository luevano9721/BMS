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
    public partial class C_Vendor : System.Web.UI.Page
    {
        #region Private Variables

        Vendor initialVendor = new Vendor();

        private AddPrivilege privilege = new AddPrivilege();
        string language = null;
        string msg_Vendor_Add;
        string msg_Vendor_Update;
        string msg_Vendor_Delete;


        #endregion

        #region Page

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
                fillVendorGrid();

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

            msg_Vendor_Add = (string)GetGlobalResourceObject(language, "msg_Vendor_Add");
            msg_Vendor_Update = (string)GetGlobalResourceObject(language, "msg_Vendor_Update");
            msg_Vendor_Delete = (string)GetGlobalResourceObject(language, "msg_Vendor_Delete");

        }
        #endregion


        #region button
        protected void btAdd_Click(object sender, EventArgs e)
        {
            PrivilegeAndProcessFlow("01101");
            tbVendorID.Focus();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
        }
        protected void btReset_Click(object sender, EventArgs e)
        {
            PrivilegeAndProcessFlow("10001");
            fillVendorGrid();
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (tbVendorID.Enabled == true)
                {
                    //ADD
                    Vendor addVendor = new Vendor();
                    addVendor.Vendor_ID = tbVendorID.Text.Trim();
                    addVendor.Name = tbName.Text.Trim();
                    addVendor.Address = tbAddress.Text.Trim();
                    addVendor.Telephone = tbPhone.Text.Trim();
                    addVendor.RFC = tbRFC.Text.Trim();
                    addVendor.Contact_Name = tbContactName.Text.Trim();
                    addVendor.Firm_Name = tbFirmName.Text.Trim();
                    addVendor.Invoice_building = tbBuilding.Text.Trim();
                    GenericClass.SQLInsertObj(addVendor);
                    functions.ShowMessage(this, this.GetType(), msg_Vendor_Add + " " + tbVendorID.Text.Trim(), MessageType.Success);
                }
                else
                {
                    //Update
                    Vendor updateVendor = new Vendor();
                    updateVendor.Vendor_ID = "'" + tbVendorID.Text.Trim() + "'";
                    Dictionary<string, dynamic> dicUpdVendor = new Dictionary<string, dynamic>();
                    dicUpdVendor.Add("Name", "'" + tbName.Text.Trim() + "'");
                    dicUpdVendor.Add("Address", "'" + tbAddress.Text.Trim() + "'");
                    dicUpdVendor.Add("Telephone", "'" + tbPhone.Text.Trim() + "'");
                    dicUpdVendor.Add("RFC", "'" + tbRFC.Text.Trim() + "'");
                    dicUpdVendor.Add("Contact_Name", "'" + tbContactName.Text.Trim() + "'");
                    dicUpdVendor.Add("Firm_Name", "'" + tbFirmName.Text.Trim() + "'");
                    dicUpdVendor.Add("Invoice_building", "'" + tbBuilding.Text.Trim() + "'");
                    GenericClass.SQLUpdateObj(updateVendor, dicUpdVendor, "Where vendor_ID=" + updateVendor.Vendor_ID);
                    functions.ShowMessage(this, this.GetType(), msg_Vendor_Update + " " + tbVendorID.Text.Trim(), MessageType.Success);
                }

                PrivilegeAndProcessFlow("10001");
                GridView_Vendors.DataSource = GenericClass.SQLSelectObj(initialVendor, mappingQueryName: "Catalog");
                GridView_Vendors.DataBind();

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void btDelete_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "", true);
            //Delete
            try
            {
                Vendor deleteVendor = new Vendor();
                deleteVendor.Vendor_ID = "'" + tbVendorID.Text.Trim() + "'";
                GenericClass.SQLDeleteObj(initialVendor, "Where Vendor_ID=" + deleteVendor.Vendor_ID);
                functions.ShowMessage(this, this.GetType(), msg_Vendor_Delete + " " + tbVendorID.Text.Trim(), MessageType.Success);
                PrivilegeAndProcessFlow("10001");
                GridView_Vendors.DataSource = GenericClass.SQLSelectObj(initialVendor, mappingQueryName: "Catalog");
                GridView_Vendors.DataBind();

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
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Proveedor')", true);
        }
        #endregion


        #region GridView
        protected void GridView_Vendors_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = GridView_Vendors.SelectedRow;

            try
            {
                tbVendorID.Text = HttpUtility.HtmlDecode(GridView_Vendors.Rows[gvRow.RowIndex].Cells[1].Text);
                tbName.Text = HttpUtility.HtmlDecode(GridView_Vendors.Rows[gvRow.RowIndex].Cells[2].Text);
                tbAddress.Text = HttpUtility.HtmlDecode(GridView_Vendors.Rows[gvRow.RowIndex].Cells[3].Text);
                tbPhone.Text = HttpUtility.HtmlDecode(GridView_Vendors.Rows[gvRow.RowIndex].Cells[4].Text);
                tbRFC.Text = HttpUtility.HtmlDecode(GridView_Vendors.Rows[gvRow.RowIndex].Cells[5].Text);
                tbContactName.Text = HttpUtility.HtmlDecode(GridView_Vendors.Rows[gvRow.RowIndex].Cells[6].Text);
                tbFirmName.Text = HttpUtility.HtmlDecode(GridView_Vendors.Rows[gvRow.RowIndex].Cells[7].Text);
                tbBuilding.Text = HttpUtility.HtmlDecode(GridView_Vendors.Rows[gvRow.RowIndex].Cells[8].Text);
                PrivilegeAndProcessFlow("01111");
                tbName.Focus();
                tbVendorID.Enabled = false;


                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

            System.Web.UI.HtmlControls.HtmlGenericControl dynDiv =
    new System.Web.UI.HtmlControls.HtmlGenericControl("collapseGOne");
            dynDiv.Attributes["class"] = "collapse in accordion-body";
        }
        protected void GridView_Vendors_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_Vendors.PageIndex = e.NewPageIndex;
            if ((bool)ViewState["searchStatus"] == true)
            { GridView_Vendors.DataSource = ViewState["searchResults"]; }
            if ((bool)ViewState["isSorting"] == true)
            { GridView_Vendors.DataSource = Session["Objects"]; }
            GridView_Vendors.DataBind();
        }
        protected void GridView_Vendors_Sorting(object sender, GridViewSortEventArgs e)
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
            DataTable dt = GenericClass.SQLSelectObj(initialVendor, mappingQueryName: "Catalog");
            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["objects"] = sortedView;
            GridView_Vendors.DataSource = sortedView;
            GridView_Vendors.DataBind();
        }
        #endregion

        

        #region Methods

        public void PrivilegeAndProcessFlow(string turnOnOffBNuttons)
        {
            privilege.applyPrivilege(turnOnOffBNuttons, GridView_Vendors);
            ProcessFlow();

        }

        /// <summary>
        /// Add,Reset,Edit,Save,Delete
        /// </summary>
        /// <param name="binary"></param>
        private void ProcessFlow()
        {

            if (this.btAdd.Visible == true || (this.btAdd.Visible == false & this.btSave.Visible == false))
            {
                tbVendorID.Enabled = false;
                
                tbName.Enabled = false;
                
                tbAddress.Enabled = false;
                
                tbPhone.Enabled = false;
                
                tbRFC.Enabled = false;
                
                tbContactName.Enabled = false;
                
                tbFirmName.Enabled = false;
                
                tbBuilding.Enabled = false;


                tbVendorID.Text = string.Empty;
                
                tbName.Text = string.Empty;
                
                tbAddress.Text = string.Empty;
                
                tbPhone.Text = string.Empty;
                
                tbRFC.Text = string.Empty;
                
                tbContactName.Text = string.Empty;
                
                tbFirmName.Text = string.Empty;
                
                tbBuilding.Text = string.Empty;

                tbSearchCH1.Enabled = true;

                btSearchCH1.Enabled = true;

                btExcel.Enabled = true;

                GridView_Vendors.Columns[0].Visible = true;

                privilege.applyPrivilege("10001", GridView_Vendors);

            }
            if (this.btSave.Visible == true || this.btDelete.Visible == true)
            {
                tbVendorID.Enabled = true;
                
                tbName.Enabled = true;
                
                tbAddress.Enabled = true;
                
                tbPhone.Enabled = true;
                
                tbRFC.Enabled = true;
                
                tbContactName.Enabled = true;
                
                tbFirmName.Enabled = true;
                
                tbBuilding.Enabled = true;
                
                tbSearchCH1.Enabled = false;

                btSearchCH1.Enabled = false;

                btExcel.Enabled = false;

                GridView_Vendors.Columns[0].Visible = false;
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
                GridView_Vendors.PageIndex = 0;
                string searchTerm = tbSearchCH1.Text.ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    fillVendorGrid();
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
                        if (row["Name"].ToString().ToLower().Contains(searchTerm) || row["Address"].ToString().ToLower().Contains(searchTerm) || row["Contact_Name"].ToString().ToLower().Contains(searchTerm) || row["Firm_Name"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtNew.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    GridView_Vendors.DataSource = dtNew;
                    ViewState["searchResults"] = dtNew;
                    GridView_Vendors.DataBind();
                }
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
                DataTable dtLoad = GenericClass.SQLSelectObj(initialVendor, mappingQueryName: "Catalog");
                Session["Excel"] = dtLoad;
                Session["name"] = "Vendors";
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        private void fillVendorGrid()
        {
            try
            {
                DataTable getData = GenericClass.SQLSelectObj(initialVendor, mappingQueryName: "Catalog");
                GridView_Vendors.DataSource = getData;
                ViewState["backupData"] = getData;
                GridView_Vendors.DataBind();
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        #endregion

       

      


       


       
    }
}