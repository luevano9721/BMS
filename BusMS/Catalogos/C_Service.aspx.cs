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
    public partial class C_Service : System.Web.UI.Page
    {

        #region Variables

        private Service initialService = new Service();

        private AddPrivilege privilege = new AddPrivilege();
        string language = null;
        string msg_Service_Add;
        string msg_Service_Update;
        string msg_Service_Delete;
        #endregion


        #region Page

        protected void Page_Init(object sender, EventArgs e)
        {
            //Load buttons to hide or unhide for privileges
            privilege = new AddPrivilege("10001", btAdd, btReset, btSave, btDelete, btExcel);

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
                //Set buttons to initial mode
                PrivilegeAndProcessFlow("10001");

                ViewState["searchStatus"] = false;
                ViewState["isSorting"] = false;
            }

            if ((bool)ViewState["searchStatus"] == false && (bool)ViewState["isSorting"] == false)
                fillServiceGrid();


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

            msg_Service_Add = (string)GetGlobalResourceObject(language, "msg_Service_Add");
            msg_Service_Update = (string)GetGlobalResourceObject(language, "msg_Service_Update");
            msg_Service_Delete = (string)GetGlobalResourceObject(language, "msg_Service_Delete");

        }
        #endregion


        #region button
        protected void btAdd_Click(object sender, EventArgs e)
        {
            PrivilegeAndProcessFlow("01101");
            tbName.Focus();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
        }
        protected void btReset_Click(object sender, EventArgs e)
        {
            PrivilegeAndProcessFlow("10001");
            fillServiceGrid();
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            try
            {


                if (tbName.Enabled == true)
                {
                    //ADD
                    double min = 0;
                    double max = 0;

                    double.TryParse(tbMinDistance.Text.Trim(), out min);
                    double.TryParse(tbMaxDistance.Text.Trim(), out max);

                    Service addService = new Service();
                    addService.Service_ID = tbName.Text.Trim();
                    addService.Min_Distance = min;
                    addService.Max_Distance = max;

                    GenericClass.SQLInsertObj(addService);
                    functions.ShowMessage(this, this.GetType(), msg_Service_Add + " " + tbName.Text.Trim(), MessageType.Success);
                }
                else
                {
                    //Update
                    Service updateService = new Service();
                    updateService.Service_ID = "'" + tbName.Text.Trim() + "'";
                    Dictionary<string, dynamic> dicUpdSP = new Dictionary<string, dynamic>();
                    dicUpdSP.Add("Min_Distance", tbMinDistance.Text.Trim());
                    dicUpdSP.Add("Max_Distance", tbMaxDistance.Text.Trim());


                    GenericClass.SQLUpdateObj(updateService, dicUpdSP, "Where Service_ID=" + updateService.Service_ID);
                    functions.ShowMessage(this, this.GetType(), msg_Service_Update + " " + tbName.Text.Trim(), MessageType.Success);
                }

                PrivilegeAndProcessFlow("10001");
                GridView_Services.DataSource = GenericClass.SQLSelectObj(initialService, mappingQueryName: "Catalog");
                GridView_Services.DataBind();

            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void btDelete_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "", true);
            ////Delete
            try
            {
                Service deleteService = new Service();
                deleteService.Service_ID = "'" + tbName.Text.Trim() + "'";
                GenericClass.SQLDeleteObj(initialService, "Where Service_ID=" + deleteService.Service_ID);
                functions.ShowMessage(this, this.GetType(), msg_Service_Delete + " " + tbName.Text.Trim(), MessageType.Success);
                PrivilegeAndProcessFlow("10001");
                GridView_Services.DataSource = GenericClass.SQLSelectObj(initialService, mappingQueryName: "Catalog");
                GridView_Services.DataBind();

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
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Servicios')", true);
        }

        #endregion


        #region GridView
        protected void GridView_Services_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = GridView_Services.SelectedRow;

            try
            {
                tbName.Text = HttpUtility.HtmlDecode(GridView_Services.Rows[gvRow.RowIndex].Cells[1].Text);
                tbMinDistance.Text = HttpUtility.HtmlDecode(GridView_Services.Rows[gvRow.RowIndex].Cells[2].Text);
                tbMaxDistance.Text = HttpUtility.HtmlDecode(GridView_Services.Rows[gvRow.RowIndex].Cells[3].Text);

                PrivilegeAndProcessFlow("01111");
                tbName.Enabled = false;
                tbMinDistance.Focus();


                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void GridView_Services_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_Services.PageIndex = e.NewPageIndex;
            if ((bool)ViewState["searchStatus"] == true)
            { GridView_Services.DataSource = ViewState["searchResults"]; }
            if ((bool)ViewState["isSorting"] == true)
            { GridView_Services.DataSource = Session["Objects"]; }
            GridView_Services.DataBind();
        }
        protected void GridView_Services_Sorting(object sender, GridViewSortEventArgs e)
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
            DataTable dt = GenericClass.SQLSelectObj(initialService, mappingQueryName: "Catalog");
            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["objects"] = sortedView;
            GridView_Services.DataSource = sortedView;
            GridView_Services.DataBind();
        }
        #endregion

        #region ComboBox
        #endregion

        #region Methods

        public void PrivilegeAndProcessFlow(string turnOnOffBNuttons)
        {
            privilege.applyPrivilege(turnOnOffBNuttons, GridView_Services);
            ProcessFlow();

        }

        private void search()
        {
            try
            {
                DataTable dtNew = new DataTable();
                GridView_Services.PageIndex = 0;
                string searchTerm = tbSearchCH1.Text.ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    fillServiceGrid();
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
                        if (row["Service_ID"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtNew.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    GridView_Services.DataSource = dtNew;
                    ViewState["searchResults"] = dtNew;
                    GridView_Services.DataBind();
                }
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
                DataTable dtLoad = GenericClass.SQLSelectObj(initialService, mappingQueryName: "Catalog");
                Session["Excel"] = dtLoad;
                Session["name"] = "Service";
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        private void fillServiceGrid()
        {
            try
            {
                DataTable getData = GenericClass.SQLSelectObj(initialService, mappingQueryName: "Catalog");
                GridView_Services.DataSource = getData;
                GridView_Services.PageSize = 15;
                ViewState["backupData"] = getData;
                GridView_Services.DataBind();
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
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
                tbName.Enabled = false;

                tbMinDistance.Enabled = false;

                tbMaxDistance.Enabled = false;

                btSearchCH1.Enabled = true;

                tbSearchCH1.Enabled = true;

                btExcel.Enabled = true;

                GridView_Services.Columns[0].Visible = true;

                privilege.applyPrivilege("10001", GridView_Services);

                tbName.Text = string.Empty;

                tbMinDistance.Text = string.Empty;

                tbMaxDistance.Text = string.Empty;

            }
            if (this.btSave.Visible == true || this.btDelete.Visible == true)
            {
                tbName.Enabled = true;

                tbMinDistance.Enabled = true;

                tbMaxDistance.Enabled = true;

                GridView_Services.Columns[0].Visible = false;

                btSearchCH1.Enabled = false;

                tbSearchCH1.Enabled = false;

                btExcel.Enabled = false;
            }
        }
        #endregion


      
      
     

       

       

       

      

     
      
    }
}