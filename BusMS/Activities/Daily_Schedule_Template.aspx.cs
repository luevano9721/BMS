using BusManagementSystem._01Catalogos;
using BusManagementSystem.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusManagementSystem.Activities
{
    public partial class Daily_Schedule_Template : System.Web.UI.Page
    {
        Dialy_Schedule_Template dst = new Dialy_Schedule_Template();
        String shiftValue = string.Empty;
        Users usr;
        DataTable dtNew = new DataTable();
        string msg_DST_Updated;
        string msg_DST_Deleted;
        string msg_DST_AddedTemplate;
        String language = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            validateSession();
            if (!IsPostBack)
            {
                ViewState["searchStatus"] = false;
                ViewState["isSorting"] = false;
                ViewState["templateEditing"] = false;
                
            }

            if (cbShift.SelectedValue != string.Empty)
                disableOrEnableAddNewTripElements(true);
            else
                disableOrEnableAddNewTripElements(false);

            if ((bool)ViewState["searchStatus"] == false && (bool)ViewState["isSorting"] == false && (bool)ViewState["templateEditing"] == false)
            {
                if (cbShift.SelectedValue != string.Empty & btLoadData.Enabled==false)
                {
                    BindData(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
                    
                }
            }


            applyLanguage();
            translateControls(language);
        }

        private void cleanSearchAndSorting()
        {
            ViewState["searchStatus"] = false;
            ViewState["isSorting"] = false;
            tbSearchCH1.Text = string.Empty;

        }

        private void validateSession()
        {
            usr = (Users)(Session["C_USER"]);
            if (usr == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void disableOrEnableAddNewTripElements(bool status)
        {

            btAddToGrid.Enabled = status;
            tbCheckPoint.Enabled = status;
            cbBusDriver.Enabled = status;
            cbRoute.Enabled = status;
            btExcel.Enabled = status;
            tbSearchCH1.Enabled = status;
            btSearchCH1.Enabled = status;

        }

        private void applyLanguage()
        {
            Users usr = (Users)(Session["C_USER"]);
            functions func = new functions();
            language = Session["Language"] != null ? Session["Language"].ToString() : language = functions.GetLanguage(usr);
            func.languageTranslate(this.Master, language);
        }

        private void translateControls(string language)
        {

            if (language.Equals("Eng"))
            {
                cbShiftType.Items[0].Text = "Select";
                cbShiftType.Items[1].Text = "Entry";
                cbShiftType.Items[2].Text = "Exit";

            }


            msg_DST_Updated = (string)GetGlobalResourceObject(language, "msg_DST_Updated");
            msg_DST_Deleted = (string)GetGlobalResourceObject(language, "msg_DST_Deleted");
            msg_DST_AddedTemplate = (string)GetGlobalResourceObject(language, "msg_DST_AddedTemplate");
        }

        private void fillAllDropDownList(string selectedShift, string selectedVendor)
        {
            //Fill Routes DropDownList
            Route initialRoute = new Route();
            DataTable dtRoute = GenericClass.SQLSelectObj(initialRoute, mappingQueryName: "DO-Route", WhereClause: " WHERE Stop1.End_Point=" + cbShiftType.SelectedValue.ToString());
            cbRoute.Items.Clear();
            cbRoute.DataSource = dtRoute;
            cbRoute.DataValueField = "Route_ID";
            cbRoute.DataTextField = "Name";
            cbRoute.DataBind();

            //Fill Bus Driver DropDownList 
            Bus_Driver busDriver = new Bus_Driver();
            DataTable dtBusDriver = GenericClass.SQLSelectObj(busDriver, mappingQueryName: "DST-BUS_DRIVER_ID", WhereClause:
            "Where BUS_DRIVER.Bus_Driver_ID not in (select Bus_Driver_ID from DIALY_SCHEDULE_TEMPLATE where Shift_ID=" + "'" + selectedShift + "'" + ")" +
            " and BUS_DRIVER.Vendor_ID=" + "'" + selectedVendor + "'" + " and BUS_DRIVER.Shift_ID=" + "'" + selectedShift + "'" + " and BUS_DRIVER.Is_Active=1" +
            " and BUS.Vendor_ID+BUS.Bus_ID not in (select SUBSTRING(Bus_Driver_ID,0,CHARINDEX('-',Bus_Driver_ID)) from DIALY_SCHEDULE_TEMPLATE where Shift_ID=" + "'" + selectedShift + "'" + ")" +
            " and CAST(Driver.Driver_ID AS VARCHAR(MAX)) not in (select SUBSTRING(Bus_Driver_ID,CHARINDEX('-',Bus_Driver_ID)+1,LEN(Driver.Driver_ID)) from" +
            " DIALY_SCHEDULE_TEMPLATE where Shift_ID=" + "'" + selectedShift + "'" + ")");

            cbBusDriver.Items.Clear();
            cbBusDriver.DataSource = dtBusDriver;
            cbBusDriver.DataValueField = "Bus_Driver_ID";
            cbBusDriver.DataTextField = "Bus_Driver";
            cbBusDriver.DataBind();
        }


        protected void Page_Init(object sender, EventArgs e)
        {
            validateSession();
            Vendor initialVendor = new Vendor();
            DataTable dt = GenericClass.SQLSelectObj(initialVendor, WhereClause: usr.Profile.Equals("INTERNAL") ? "" : "Where [Vendor_ID]= '" + usr.Vendor_ID + "'");
            DataView dvVendor = new DataView(dt);
            DataTable distincVendor = dvVendor.ToTable(true, "VENDOR_ID", "NAME");
            cbShift.Enabled = false;

            if (usr.Profile.Equals("INTERNAL"))
            {
                cbVendorAdm.Enabled = true;
            }
            else
            {
                cbVendorAdm.Enabled = false;

            }

            cbVendorAdm.Items.Clear();
            cbVendorAdm.DataSource = distincVendor;
            cbVendorAdm.DataValueField = "VENDOR_ID";
            cbVendorAdm.DataTextField = "NAME";
            cbVendorAdm.DataBind();
        }


        private void BindData(string shift, string vendor)
        {
            try
            {
                DataTable getData = GenericClass.SQLSelectObj(dst, mappingQueryName: "Catalog", WhereClause:
                   "where BUS_DRIVER.Vendor_ID=" + "'" + vendor + "'" + " and BUS_DRIVER.Is_Active=1 AND DIALY_SCHEDULE_TEMPLATE.Shift_ID=" + "'" + shift + "'");
                GridView_Template.DataSource = getData;
                ViewState["gridDataVS"] = getData;
                GridView_Template.DataBind();

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void editTemplate(object sender, GridViewEditEventArgs e)
        {
            try
            {
                validateSession();
                ViewState["templateEditing"] = true;
                DataView dat = Session["Objects_DST"] as DataView;
                DataTable getDataSearch = (DataTable)ViewState["searchResults"];
                GridView_Template.EditIndex = e.NewEditIndex;

                if ((bool)ViewState["searchStatus"] == true && getDataSearch.Rows.Count > 0 && (bool)ViewState["isSorting"] == false)
                {
                    GridView_Template.DataSource = getDataSearch;
                    GridView_Template.DataBind();
                }
                if ((bool)ViewState["isSorting"] == true && dat.Count > 0 && (bool)ViewState["searchStatus"] == false)
                {
                    GridView_Template.DataSource = dat;
                    GridView_Template.DataBind();
                }
                if ((bool)ViewState["isSorting"] == false && (bool)ViewState["searchStatus"] == false)
                {
                    BindData(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
                }

                DropDownList cbRoute1 = (DropDownList)GridView_Template.Rows[GridView_Template.EditIndex].FindControl("cbRoute1") as DropDownList;
                Label lbRouteHide = (Label)GridView_Template.Rows[GridView_Template.EditIndex].FindControl("lbRouteHide") as Label;
                if (cbRoute1.Items.Count <= 0)
                {
                    Route initialRoute = new Route();
                    DataTable dtRoute = GenericClass.SQLSelectObj(initialRoute, mappingQueryName: "DO-Route", WhereClause: " WHERE Stop1.End_Point=" + cbShiftType.SelectedValue.ToString());
                    cbRoute1.Items.Clear();
                    cbRoute1.DataSource = dtRoute;
                    cbRoute1.DataValueField = "Route_ID";
                    cbRoute1.DataTextField = "Name";
                    cbRoute1.DataBind();

                    int row = 0;

                    ListItem itemFinded = cbRoute1.Items.FindByText(lbRouteHide.Text);
                    row = cbRoute1.Items.IndexOf(itemFinded);
                    cbRoute1.SelectedIndex = row;
                }

                GridView_Template.BottomPagerRow.Visible = false;
                
                disableOrEnableAddNewTripElements(false);
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ViewState["templateEditing"] = false;
            GridView_Template.EditIndex = -1;
            DataView getDataSorting = (DataView)Session["Objects_DST"];
            DataTable getDataSearch = (DataTable)ViewState["searchResults"];
            if ((bool)ViewState["searchStatus"] == true && getDataSearch.Rows.Count > 0 && (bool)ViewState["isSorting"] == false)
            {
                GridView_Template.DataSource = getDataSearch;
                GridView_Template.DataBind();
                fillAllDropDownList(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);

            }
            if ((bool)ViewState["isSorting"] == true && getDataSorting.Count > 0 && (bool)ViewState["searchStatus"] == false)
            {
                GridView_Template.DataSource = getDataSorting;
                GridView_Template.DataBind();
                fillAllDropDownList(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
            }
            if ((bool)ViewState["isSorting"] == false && (bool)ViewState["searchStatus"] == false)
            {
                BindData(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
                fillAllDropDownList(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
            }

            GridView_Template.BottomPagerRow.Visible = true;
        }


        protected void updateTemplate(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                validateSession();

                Dialy_Schedule_Template updateDst = new Dialy_Schedule_Template();
                Dictionary<string, dynamic> dicUpdDst = new Dictionary<string, dynamic>();
                Label lbID = (Label)GridView_Template.Rows[e.RowIndex].FindControl("lblID") as Label;
                TextBox checktime = (TextBox)GridView_Template.Rows[e.RowIndex].FindControl("tbCheckPoint1") as TextBox;
                DropDownList cbRoute = (DropDownList)GridView_Template.Rows[e.RowIndex].FindControl("cbRoute1") as DropDownList;

                updateDst.DST_ID = Int32.Parse(lbID.Text.Trim());
                dicUpdDst.Add("Route_ID", "'" + cbRoute.SelectedValue.ToString() + "'");
                dicUpdDst.Add("Check_Point_Time", "'" + checktime.Text.Trim() + "'");
                GenericClass.SQLUpdateObj(updateDst, dicUpdDst, "Where DST_ID=" + updateDst.DST_ID);

                GridView_Template.EditIndex = -1;

                

                BindData(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);

                if ((bool) ViewState["searchStatus"] == true)
                {
                   
                    search();
                }
                ViewState["templateEditing"] = false;

                GridView_Template.BottomPagerRow.Visible = true;

                //cleanSearchAndSorting();
                //BindData(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
                fillAllDropDownList(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
                ShowMessage(msg_DST_Updated, MessageType.Success);
            }

            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkRemove = (LinkButton)sender;
                Label lbID = (Label)GridView_Template.HeaderRow.FindControl("lblID") as Label;
                Dialy_Schedule_Template deleteDST = new Dialy_Schedule_Template();
                deleteDST.DST_ID = Int32.Parse(lnkRemove.CommandArgument);
                GenericClass.SQLDeleteObj(dst, "Where DST_ID=" + deleteDST.DST_ID);
                ShowMessage(msg_DST_Deleted, MessageType.Success);
                BindData(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
                fillAllDropDownList(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void loadData(object sender, EventArgs e)
        {
            GridView_Template.PageIndex = 0;
            tbCheckPoint.Text = string.Empty;
            shiftValue = cbShift.SelectedValue.ToString();
            btLoadData.Enabled = false;
            cbShift.Enabled = false;
            cbShiftType.Enabled = false;
            cbVendorAdm.Enabled = false;
            btChange.Enabled = true;
            BindData(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
            fillAllDropDownList(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
            disableOrEnableAddNewTripElements(true);

        }
        protected void changeShift(object sender, EventArgs e)
        {
            btLoadData.Enabled = false;
            cbShift.Enabled = false;
            cbShiftType.Enabled = true;
            cbVendorAdm.Enabled = true;
            GridView_Template.DataSource = null;
            GridView_Template.DataBind();
            btChange.Enabled = false;
            cbShiftType.SelectedIndex = 0;
            tbCheckPoint.Text = string.Empty;
            cbShift.Items.Clear();
            disableOrEnableAddNewTripElements(false);
            ViewState["searchStatus"] = false;
            ViewState["isSorting"] = false;
            ViewState["templateEditing"] = false;
        }

        protected void btAddTripToCheckPoint1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dtime = new DateTime();
                DateTime.TryParse(tbCheckPoint.Text, out dtime);
                TimeSpan ts = new TimeSpan(dtime.Hour, dtime.Minute, dtime.Second);
                Dialy_Schedule_Template aadDst = new Dialy_Schedule_Template();
                aadDst.Bus_Driver_ID = cbBusDriver.SelectedValue.ToString();
                aadDst.Route_ID = Int32.Parse(cbRoute.SelectedValue.ToString());
                aadDst.Shift_ID = cbShift.SelectedValue.ToString();
                aadDst.Check_Point_Time = ts;

                GenericClass.SQLInsertObj(aadDst);
                ShowMessage(msg_DST_AddedTemplate, MessageType.Success);
                BindData(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
                fillAllDropDownList(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }

        protected void btExcel_Click(object sender, EventArgs e)
        {
            execute_query();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.Redirect("~/Catalogos/ExportToExcel.aspx?btn=excel", false);
        }

        protected void GridView_Template_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_Template.PageIndex = e.NewPageIndex;
            if ((bool)ViewState["searchStatus"] == true)
            { GridView_Template.DataSource = ViewState["searchResults"]; }
            if ((bool)ViewState["isSorting"] == true)
            { GridView_Template.DataSource = Session["Objects_DST"]; }
            GridView_Template.DataBind();
        }

        protected void GridView_Template_Sorting(object sender, GridViewSortEventArgs e)
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

                DataTable dt = GenericClass.SQLSelectObj(dst, mappingQueryName: "Catalog", WhereClause:
                   "where BUS_DRIVER.Vendor_ID=" + "'" + cbVendorAdm.SelectedValue + "'" + " and BUS_DRIVER.Is_Active=1 AND DIALY_SCHEDULE_TEMPLATE.Shift_ID=" + "'" + cbShift.SelectedValue.ToString() + "'");
                DataView sortedView = new DataView(dt);
                sortedView.Sort = e.SortExpression + " " + sortingDirection;
                Session["Objects_DST"] = sortedView;
                GridView_Template.DataSource = sortedView;
                GridView_Template.DataBind();
                fillAllDropDownList(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
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
                if (Session["C_USER"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                Users usr = (Users)(Session["C_USER"]);
                DataTable dtLoad = GenericClass.SQLSelectObj(dst, mappingQueryName: "Catalog", WhereClause:
               "where BUS_DRIVER.Vendor_ID=" + "'" + cbVendorAdm.SelectedValue + "'" + " and BUS_DRIVER.Is_Active=1 AND DIALY_SCHEDULE_TEMPLATE.Shift_ID=" + "'" + cbShift.SelectedValue.ToString() + "'");
                Session["Excel"] = dtLoad;
                Session["name"] = "DailyScheduleTemplate";
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void GridView_Template_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label id = (Label)GridView_Template.Rows[e.RowIndex].FindControl("lblID") as Label;
                Dialy_Schedule_Template deleteDST = new Dialy_Schedule_Template();
                deleteDST.DST_ID = Int32.Parse(id.Text);
                GenericClass.SQLDeleteObj(dst, "Where DST_ID=" + deleteDST.DST_ID);
                ShowMessage(msg_DST_Deleted, MessageType.Success);
                BindData(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
                fillAllDropDownList(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void cbShiftType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbShiftType.SelectedValue.ToString() == "NONE")
            {
                cbShift.Items.Clear();
                btLoadData.Enabled = false;
                cbShift.Enabled = false;
            }
            else
            {
                Shift initialShift = new Shift();
                DataTable dtGetShifts = GenericClass.SQLSelectObj(initialShift, WhereClause: "WHERE EXIT_SHIFT=" + cbShiftType.SelectedValue.ToString());
                cbShift.Items.Clear();
                cbShift.DataSource = dtGetShifts;
                cbShift.DataValueField = "Shift_ID";
                cbShift.DataTextField = "Name";
                cbShift.DataBind();
                btLoadData.Enabled = true;
                cbShift.Enabled = true;
                cbShiftType.Enabled = true;

            }
        }

        private void search()
        {
            try
            {
                if ((bool)ViewState["templateEditing"] == false)
                {
                    GridView_Template.PageIndex = 0; //al cargar de nuevo se quedo en pagina 3
                }
                
                string searchTerm = tbSearchCH1.Text.ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    BindData(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
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
                        if (row["Bus_ID"].ToString().ToLower().Contains(searchTerm) || row["Driver"].ToString().ToLower().Contains(searchTerm) || row["Route"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtNew.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    GridView_Template.DataSource = dtNew;
                    ViewState["searchResults"] = dtNew;
                    GridView_Template.DataBind();
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void btSearchCH1_Click1(object sender, EventArgs e)
        {
            search();
        }

        protected void btHelp_Click(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("./Login.aspx");
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Plantilla de operacion diaria')", true);
        }

    }
}