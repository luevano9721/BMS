using BusManagementSystem.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusManagementSystem.Catalogos;


namespace BusManagementSystem._01Catalogos
{
    public partial class C_Route : System.Web.UI.Page
    {
        #region Variables

        private Route initialRoute = new Route();

        private DataTable dtNewSearch = new DataTable();

        private AddPrivilege privilege = new AddPrivilege();
        string language = null;
        string msg_Route_Add;
        string msg_Route_Delete;
        string msg_Route_Updated;
        string msg_Route_NoChanges;
        #endregion

        #region Methods

        /// <summary>
        /// Get Table to show in the catalog
        /// </summary>
        /// <returns>datatable to fill grid</returns>
        protected DataTable generalDataTable()
        {
            DataTable dtReturn = new DataTable();

            Session["Excel"] = null;

            Session["name"] = string.Empty;

            try
            {
                string zoneFilter = ddlZone.SelectedValue.Equals("0") ? "" : " Zone=" + ddlZone.SelectedValue;

                string typeFilter = ddlType.SelectedValue.Equals("0") ? "" : ddlType.SelectedValue.Equals("1") ? "Org_ID not in (select stop_id from STOP_POINT where End_Point=1)" : "Org_ID not in (select stop_id from STOP_POINT where End_Point=0)";

                if (zoneFilter.Equals(string.Empty))
                {
                    dtReturn = GenericClass.SQLSelectObj(new Route(), mappingQueryName: "Catalog", WhereClause: typeFilter.Equals(string.Empty) ? "" : "Where " + typeFilter);
                }
                else
                {
                    dtReturn = GenericClass.SQLSelectObj(new Route(), mappingQueryName: "Catalog", WhereClause: typeFilter.Equals(string.Empty) ? "Where " + zoneFilter : "Where " + zoneFilter + " And " + typeFilter);
                }


                Session["Excel"] = dtReturn;

                Session["name"] = "Routes";

                ViewState["backupData"] = dtReturn;

                GridView_Routes.PageSize = 15;
            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);

                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", "");

                functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

            return dtReturn;

        }

        public void PrivilegeAndProcessFlow(string turnOnOffBNuttons)
        {
            privilege.applyPrivilege(turnOnOffBNuttons, GridView_Routes);
            ProcessFlow();

        }




        protected void FillStopPointAndService()
        {
            Stop_Point initialStopPoint = new Stop_Point();
            DataTable dtStopPoint = GenericClass.SQLSelectObj(initialStopPoint, mappingQueryName: "Catalog");


            Service initialService = new Service();
            DataTable dtService = GenericClass.SQLSelectObj(initialService, mappingQueryName: "Catalog");

            Zone initialZones = new Zone();
            DataTable dtZone = GenericClass.SQLSelectObj(initialZones);

            ddlZoneID.Items.Clear();
            ddlZoneID.DataSource = dtZone;
            ddlZoneID.DataValueField = "Zone_ID";
            ddlZoneID.DataTextField = "Zone_ID";
            ddlZoneID.DataBind();

            ddlZoneID.SelectedIndex = 0;

            ddlZone.Items.Clear();
            ddlZone.DataSource = dtZone;
            ddlZone.DataValueField = "Zone_ID";
            ddlZone.DataTextField = "Zone_ID";
            ddlZone.DataBind();

            ListItem li = new ListItem("ALL", "0");

            ddlZone.Items.Insert(0, li);
            
            ddlZone.SelectedIndex = 0;
            
            

            cbOrigin.Items.Clear();
            cbOrigin.DataSource = dtStopPoint;
            cbOrigin.DataValueField = "Stop_ID";
            cbOrigin.DataTextField = "Name";
            cbOrigin.DataBind();
            cbOrigin.SelectedIndex = 0;

            cbDestination.Items.Clear();
            cbDestination.DataSource = dtStopPoint;
            cbDestination.DataValueField = "Stop_ID";
            cbDestination.DataTextField = "Name";
            cbDestination.DataBind();
            cbDestination.SelectedIndex = 0;

            cbService.Items.Clear();
            cbService.DataSource = dtService;
            cbService.DataValueField = "Service_ID";
            cbService.DataTextField = "Service_ID";
            cbService.DataBind();
            cbService.SelectedIndex = 0;

        }

        protected Route GetNewRoute()
        {
            DateTime dtime = new DateTime();

            DateTime.TryParseExact(tbTime.Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtime);

            TimeSpan ts = new TimeSpan(dtime.Hour, dtime.Minute, dtime.Second);

            Route addRoute = new Route();

            addRoute.Route_ID =Convert.ToInt32( tbRoute.Text);

            addRoute.Org_ID = cbOrigin.SelectedValue.ToString();

            addRoute.Dest_ID = cbDestination.SelectedValue.ToString();

            addRoute.Travel_Time_Est = ts;

            addRoute.Service_ID = cbService.SelectedValue.ToString();

            addRoute.Zone = Convert.ToInt32(ddlZoneID.Text);

            return addRoute;
        }

        private void fillRouteGrid()
        {
            try
            {
                DataTable getData = generalDataTable();
                GridView_Routes.DataSource = getData;
                ViewState["backupData"] = getData;
                GridView_Routes.DataBind();
            }
            catch (Exception ex)
            {
       
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        /// <summary>
        /// Add,Reset,Edit,Save,Delete
        /// </summary>
        private void ProcessFlow()
        {

            if (this.btAdd.Visible == true || (this.btAdd.Visible == false & this.btSave.Visible == false))
            {

                cbOrigin.Enabled = false;

                cbDestination.Enabled = false;

                tbTime.Enabled = false;

                cbService.Enabled = false;

                ddlZoneID.Enabled = false;

                tbRoute.Text = "0";

                tbSearchCH1.Text = string.Empty;

                tbTime.Text = string.Empty;

                btnUpdate_Route.Visible = false;

                GridView_Routes.Columns[0].Visible = true;

                privilege.applyPrivilege("10001", GridView_Routes);

                tbSearchCH1.Enabled = true;

                btSearchCH1.Enabled = true;

                ddlType.Enabled = true;

                ddlZone.Enabled = true;

                btExcel.Enabled = true;

            }
            if (this.btSave.Visible == true || this.btDelete.Visible == true)
            {

                cbOrigin.Enabled = true;

                cbDestination.Enabled = true;

                tbTime.Enabled = true;

                cbService.Enabled = true;

                ddlZoneID.Enabled = true;

                tbSearchCH1.Enabled = false;

                btSearchCH1.Enabled = false;

                tbSearchCH1.Text = string.Empty;

                btExcel.Enabled = false;

                ddlType.Enabled = false;

                ddlZone.Enabled = false;


                GridView_Routes.Columns[0].Visible = false;

            }

            if (this.btDelete.Visible == true)
            {
                this.btSave.Visible = false;

                btnUpdate_Route.Visible = true;
            }
        }

        protected void SelectRowFromRoutes(int rowIndex)
        {
            try
            {

                tbRoute.Text = GridView_Routes.Rows[rowIndex].Cells[1].Text;

                functions selectFunction = new functions();

                selectFunction.SelectDropdownList(cbOrigin, GridView_Routes.Rows[rowIndex].Cells[2].Text.ToString().Trim());

                selectFunction.SelectDropdownList(cbDestination, GridView_Routes.Rows[rowIndex].Cells[3].Text.ToString().Trim());

                selectFunction.SelectDropdownList(cbService, GridView_Routes.Rows[rowIndex].Cells[5].Text.ToString().Trim());

                selectFunction.SelectDropdownList(ddlZoneID, GridView_Routes.Rows[rowIndex].Cells[6].Text.ToString().Trim());
                
                DateTime getTime = new DateTime();

                DateTime.TryParseExact(GridView_Routes.Rows[rowIndex].Cells[4].Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal, out getTime);

                tbTime.Text = getTime.ToString("HH:mm");


            }
            catch (Exception ex)
            {
               

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void DeleteRoute()
        {
            try
            {
                int routeID = 0;

                int.TryParse(tbRoute.Text.Trim(), out routeID);

                Route deleteRoute = new Route();

                deleteRoute.Route_ID = routeID;

                GenericClass.SQLDeleteObj(initialRoute, "Where Route_ID=" + deleteRoute.Route_ID);

                functions.ShowMessage(this, this.GetType(), msg_Route_Delete + " " + tbRoute.Text.Trim(), MessageType.Success);

            }
            catch (Exception ex)
            {
             

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected Route GetCurrentRoute()
        {
            DateTime currentTime = new DateTime();

            Route currentRoute = new Route();

            try
            {


                DataTable getRoute = GenericClass.SQLSelectObj(currentRoute, WhereClause: "Where Route_ID=" + tbRoute.Text);

                currentRoute.Route_ID = Convert.ToInt32(getRoute.Rows[0]["Route_ID"].ToString());

                currentRoute.Org_ID = getRoute.Rows[0]["Org_ID"].ToString();

                currentRoute.Dest_ID = getRoute.Rows[0]["Dest_ID"].ToString();

                DateTime.TryParseExact(getRoute.Rows[0]["Travel_Time_Est"].ToString(), "HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out currentTime);

                TimeSpan cuttentTSRoute = new TimeSpan(currentTime.Hour, currentTime.Minute, currentTime.Second);

                currentRoute.Travel_Time_Est = cuttentTSRoute;

                currentRoute.Service_ID = getRoute.Rows[0]["Service_ID"].ToString();

                currentRoute.Zone = Convert.ToInt32(getRoute.Rows[0]["Zone"].ToString());

            }
            catch (Exception ex)
            {

                

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);

            }

            return currentRoute;
        }

        protected void UpdateRoute()
        {
            try
            {

                Admin_Approve newApprove = new Admin_Approve();

                Route oldValuesRoute = GetCurrentRoute();

                Route newValuesRoute = GetNewRoute();

                newApprove = adminApprove.compareObjects(oldValuesRoute, newValuesRoute);

                if (newApprove.New_Values != "No values changed")
                {
                    GenericClass.SQLUpdateObj(new Route(), adminApprove.compareObjects(oldValuesRoute, newValuesRoute, ""), "Where Route_ID=" + tbRoute.Text);

                    functions.ShowMessage(this, this.GetType(), msg_Route_Updated + " " + tbRoute.Text.Trim(), MessageType.Success);

                }
                else
                {
                    functions.ShowMessage(this, this.GetType(), msg_Route_NoChanges, MessageType.Info);
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

        private void search()
        {
            try
            {
                DataTable dtNew = new DataTable();

                GridView_Routes.PageIndex = 0;

                string searchTerm = tbSearchCH1.Text.ToLower();

                if (string.IsNullOrEmpty(searchTerm))
                {
                    fillRouteGrid();

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
                        if (ddlType.SelectedValue.Equals("1"))
                        {
                            if (row["Origin"].ToString().ToLower().Contains(searchTerm))
                            {
                                //when found copy the row to the cloned table
                                dtNewSearch.Rows.Add(row.ItemArray);
                            }
                        }
                        else if (ddlType.SelectedValue.Equals("1"))
                        {
                            if (row["Destination"].ToString().ToLower().Contains(searchTerm))
                            {
                                //when found copy the row to the cloned table
                                dtNewSearch.Rows.Add(row.ItemArray);
                            }
                        }
                        else
                        {
                            //add your own columns to be searched here
                            if (row["Origin"].ToString().ToLower().Contains(searchTerm) || row["Destination"].ToString().ToLower().Contains(searchTerm) || row["Service_ID"].ToString().ToLower().Contains(searchTerm))
                            {
                                //when found copy the row to the cloned table
                                dtNewSearch.Rows.Add(row.ItemArray);
                            }
                        }
                        
                    }

                    //rebind the grid
                    GridView_Routes.DataSource = dtNewSearch;

                    ViewState["searchResults"] = dtNewSearch;

                    GridView_Routes.DataBind();
                }
            }
            catch (Exception ex)
            {
                

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        private void execute_query()
        {
            HttpContext.Current.ApplicationInstance.CompleteRequest();

            Response.Redirect("./ExportToExcel.aspx?btn=excel", false);
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

        #endregion

        #region Events

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

                if (cbOrigin.Items.Count <= 0)
                {
                    FillStopPointAndService();
                }
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
                fillRouteGrid();
            }




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
            if (language.Equals("Eng"))
            {
                ddlType.Items[0].Text = "All";
                ddlType.Items[1].Text = "Entry";
                ddlType.Items[2].Text = "Exit";

            }

            msg_Route_Add = (string)GetGlobalResourceObject(language, "msg_Route_Add");
            msg_Route_Delete = (string)GetGlobalResourceObject(language, "msg_Route_Delete");
            msg_Route_Updated = (string)GetGlobalResourceObject(language, "msg_Route_Updated");
            msg_Route_NoChanges = (string)GetGlobalResourceObject(language, "msg_Route_NoChanges");

        }

        protected void btAdd_Click(object sender, EventArgs e)
        {
            PrivilegeAndProcessFlow("01101");

            cbOrigin.Focus();

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
        }

        protected void btReset_Click(object sender, EventArgs e)
        {
            PrivilegeAndProcessFlow("10001");

            fillRouteGrid();
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (tbRoute.Text == "0")
                {
                    Route newRoute = GetNewRoute();
                    GenericClass.SQLInsertObj(newRoute);

                    functions.ShowMessage(this, this.GetType(), msg_Route_Add + cbOrigin.Text.Trim() + "/" + cbDestination.Text.Trim(), MessageType.Success);
                }



            }
            catch (Exception ex)
            {
                

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

            PrivilegeAndProcessFlow("10001");

            GridView_Routes.DataSource = GenericClass.SQLSelectObj(initialRoute, mappingQueryName: "Catalog");

            GridView_Routes.DataBind();
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "", true);
            //Delete

            DeleteRoute();

            PrivilegeAndProcessFlow("10001");

            GridView_Routes.DataSource = generalDataTable();

            GridView_Routes.DataBind();
        }

        protected void GridView_Routes_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectRowFromRoutes(GridView_Routes.SelectedRow.RowIndex);

            PrivilegeAndProcessFlow("01111");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //Update
            UpdateRoute();

            PrivilegeAndProcessFlow("10001");

            GridView_Routes.DataSource = generalDataTable();

            GridView_Routes.DataBind();

        }

        protected void btExcel_Click(object sender, EventArgs e)
        {
            execute_query();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.Redirect("~/Catalogos/ExportToExcel.aspx?btn=excel", false);
        }

        protected void GridView_Routes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            pagination(GridView_Routes, e.NewPageIndex);

        }

        protected void GridView_Routes_Sorting(object sender, GridViewSortEventArgs e)
        {
            tbSearchCH1.Text = string.Empty;

            ViewState["searchStatus"] = false;

            srotingGrid(GridView_Routes, e);
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
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Rutas')", true);
        }



        protected void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView_Routes.PageIndex = 0;

            fillRouteGrid();
                       

            if (!tbSearchCH1.Text.Equals(string.Empty))
            {
                
                search();
            }
            
        }

        protected void cbZone_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridView_Routes.PageIndex = 0;

            fillRouteGrid();
                       
            if (!tbSearchCH1.Text.Equals(string.Empty))
            {
                search();
            }
        }
        #endregion
    }
}