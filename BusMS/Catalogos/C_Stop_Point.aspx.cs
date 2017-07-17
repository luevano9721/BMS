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
    public partial class C_Stop_Point : System.Web.UI.Page
    {
        #region Private Variables

        Stop_Point initialStopPoint = new Stop_Point();

        private AddPrivilege privilege = new AddPrivilege();
        string language = null;
        string msg_StopPoint_Add;
        string msg_StopPoint_Update;
        string msg_StopPoint_Deleted;


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
                fillGridStopPoints();

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

            msg_StopPoint_Add = (string)GetGlobalResourceObject(language, "msg_StopPoint_Add");
            msg_StopPoint_Update = (string)GetGlobalResourceObject(language, "msg_StopPoint_Update");
            msg_StopPoint_Deleted = (string)GetGlobalResourceObject(language, "msg_StopPoint_Deleted");

        }
        #endregion


        #region button
        protected void btAdd_Click(object sender, EventArgs e)
        {
            PrivilegeAndProcessFlow("01101");
            tbStopID.Focus();
            cleanSearchAndSorting();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
        }
        protected void btReset_Click(object sender, EventArgs e)
        {
            PrivilegeAndProcessFlow("10001");
            fillGridStopPoints();
            cleanSearchAndSorting();
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                cleanSearchAndSorting();

                if (tbStopID.Enabled == true)
                {
                    //ADD
                    Stop_Point addSP = new Stop_Point();
                    addSP.Stop_ID = tbStopID.Text.Trim();
                    addSP.Name = tbName.Text.Trim();
                    addSP.Address = tbAddress.Text.Trim();
                    addSP.Coordinates = tbCoordinates.Text.Trim();
                    addSP.Middle_Point = false;
                    addSP.End_Point = false;

                    GenericClass.SQLInsertObj(addSP);
                    functions.ShowMessage(this, this.GetType(), msg_StopPoint_Add + " " + tbName.Text.Trim(), MessageType.Success);
                }
                else
                {
                    //Update
                    Stop_Point updateStopP = new Stop_Point();
                    updateStopP.Stop_ID = "'" + tbStopID.Text.Trim() + "'";
                    Dictionary<string, dynamic> dicUpdSP = new Dictionary<string, dynamic>();
                    dicUpdSP.Add("Name", "'" + tbName.Text.Trim() + "'");
                    dicUpdSP.Add("Address", "'" + tbAddress.Text.Trim() + "'");
                    dicUpdSP.Add("Coordinates", "'" + tbCoordinates.Text.Trim() + "'");

                    GenericClass.SQLUpdateObj(updateStopP, dicUpdSP, "Where Stop_ID=" + updateStopP.Stop_ID);
                    functions.ShowMessage(this, this.GetType(), msg_StopPoint_Update + " " + tbStopID.Text.Trim(), MessageType.Success);
                }

                PrivilegeAndProcessFlow("10001");
                GridView_StopPoints.DataSource = GenericClass.SQLSelectObj(initialStopPoint, mappingQueryName: "Catalog");
                GridView_StopPoints.DataBind();

            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void btDelete_Click(object sender, EventArgs e)
        {
            cleanSearchAndSorting();
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "", true);
            ////Delete
            try
            {
                Stop_Point deleteSP = new Stop_Point();
                deleteSP.Stop_ID = "'" + tbStopID.Text.Trim() + "'";
                GenericClass.SQLDeleteObj(initialStopPoint, "Where Stop_ID=" + deleteSP.Stop_ID);
                functions.ShowMessage(this, this.GetType(), msg_StopPoint_Deleted + " " + tbStopID.Text.Trim(), MessageType.Success);
                PrivilegeAndProcessFlow("10001");
                GridView_StopPoints.DataSource = GenericClass.SQLSelectObj(initialStopPoint, mappingQueryName: "Catalog");
                GridView_StopPoints.DataBind();

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
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Puntos de Parada')", true);
        }

        #endregion


        #region GridView
        protected void GridView_StopPoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = GridView_StopPoints.SelectedRow;

            try
            {

                tbStopID.Text = HttpUtility.HtmlDecode(GridView_StopPoints.Rows[gvRow.RowIndex].Cells[1].Text);
                tbName.Text = HttpUtility.HtmlDecode(GridView_StopPoints.Rows[gvRow.RowIndex].Cells[2].Text);
                tbAddress.Text = HttpUtility.HtmlDecode(GridView_StopPoints.Rows[gvRow.RowIndex].Cells[3].Text);
                tbCoordinates.Text = HttpUtility.HtmlDecode(GridView_StopPoints.Rows[gvRow.RowIndex].Cells[4].Text);

                PrivilegeAndProcessFlow("01111");
                tbName.Focus();
                tbStopID.Enabled = false;

                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void GridView_StopPoints_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_StopPoints.PageIndex = e.NewPageIndex;
            if ((bool)ViewState["searchStatus"] == true)
            { GridView_StopPoints.DataSource = ViewState["searchResults"]; }
            if ((bool)ViewState["isSorting"] == true)
            { GridView_StopPoints.DataSource = Session["Objects"]; }
            GridView_StopPoints.DataBind();
        }
        protected void GridView_StopPoints_Sorting(object sender, GridViewSortEventArgs e)
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
            DataTable dt = GenericClass.SQLSelectObj(initialStopPoint, mappingQueryName: "Catalog");
            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["objects"] = sortedView;
            GridView_StopPoints.DataSource = sortedView;
            GridView_StopPoints.DataBind();
        }
        #endregion

        #region ComboBox
        #endregion

        #region Methods

        public void PrivilegeAndProcessFlow(string turnOnOffBNuttons)
        {
            privilege.applyPrivilege(turnOnOffBNuttons, GridView_StopPoints);
            ProcessFlow();

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
                GridView_StopPoints.PageIndex = 0;
                string searchTerm = tbSearchCH1.Text.ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    fillGridStopPoints();
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
                        if (row["Stop_ID"].ToString().ToLower().Contains(searchTerm) || row["Name"].ToString().ToLower().Contains(searchTerm) || row["Address"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtNew.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    GridView_StopPoints.DataSource = dtNew;
                    ViewState["searchResults"] = dtNew;
                    GridView_StopPoints.DataBind();
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
                Users usr = (Users)(Session["C_USER"]);
                DataTable dtLoad = GenericClass.SQLSelectObj(initialStopPoint, mappingQueryName: "Catalog"); ;
                Session["Excel"] = dtLoad;
                Session["name"] = "StopPoint";
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
        /// <summary>
        /// Add,Reset,Edit,Save,Delete
        /// </summary>
        /// <param name="binary"></param>
        private void ProcessFlow()
        {


            if (this.btAdd.Visible == true || (this.btAdd.Visible == false & this.btSave.Visible == false))
            {
                tbStopID.Enabled = false;

                tbName.Enabled = false;

                tbAddress.Enabled = false;

                tbCoordinates.Enabled = false;

                tbStopID.Text = string.Empty;

                tbName.Text = string.Empty;

                tbAddress.Text = string.Empty;

                tbCoordinates.Text = string.Empty;

                tbSearchCH1.Enabled = true;

                btSearchCH1.Enabled = true;

                btExcel.Enabled = true;

                GridView_StopPoints.Columns[0].Visible = true;

                privilege.applyPrivilege("10001", GridView_StopPoints);


            }
            if (this.btSave.Visible == true || this.btDelete.Visible == true)
            {
                tbStopID.Enabled = true;

                tbName.Enabled = true;

                tbAddress.Enabled = true;

                tbCoordinates.Enabled = true;

                tbSearchCH1.Enabled = false;

                btSearchCH1.Enabled = false;

                btExcel.Enabled = false;

                GridView_StopPoints.Columns[0].Visible = false;

            }
        }
        private void fillGridStopPoints()
        {
            try
            {
                DataTable getData = GenericClass.SQLSelectObj(initialStopPoint, mappingQueryName: "Catalog");
                GridView_StopPoints.DataSource = getData;
                ViewState["gridDataVS"] = getData;
                GridView_StopPoints.PageSize = 15;
                GridView_StopPoints.DataBind();
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        #endregion

       

       
     
      

       

       
               
        

       

    }
}