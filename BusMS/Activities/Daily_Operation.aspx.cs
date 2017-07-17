using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.UI;
using System.Web.UI.WebControls;
using BusManagementSystem._01Catalogos;
using BusManagementSystem.Class;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using BusManagementSystem.Catalogos;
using System.Globalization;

namespace BusManagementSystem.Activities
{
    public partial class Daily_Operation : System.Web.UI.Page
    {
        Dialy_Schedule_Template dst = new Dialy_Schedule_Template();
        Users usr;
        String shiftValue = string.Empty;

        //strings messages from resources files for translating 
        string msg_DailyOperation_btAddHeader;
        string msg_DailyOperation_CH1RowUpdating;
        string msg_DailyOperation_btDelete;
        string msg_DailyOperation_CH1RowDeleting;
        string msg_DailyOperation_CH1RowDeleting2;
        string msg_DailyOperation_CH2RowUpdating;
        string msg_DailyOperation_CH2RowUpdating2;
        string msg_DailyOperation_btTransferCH1;
        string msg_DailyOperation_btTransferCH2;
        string msg_DailyOperation_btEndPoint;
        string msg_DailyOperation_btEndPoint2;
        string msg_DailyOperation_CH3RowUpdating;
        string msg_DailyOperation_CH3RowUpdating2;
        string msg_DailyOperation_btConfirmOperation2;
        string msg_DailyOperation_shift;
        string msg_DailyOperation_btConfirmOperationOk;
        string msg_DailyOperation_btConfirmOperationEmail;
        string msg_DailyOperation_btCancelOperation;
        string msg_DailyOperation_btCancelOperation2;
        string msg_DailyOperation_btCancelOperation3;
        string msg_DailyOperation_Refresh1;
        string msg_DailyOperation_Refresh2;
        string msg_DailyOperation_Refresh3;
        string msg_DailyOperation_btConfirmOperation;
        string msg_DailyOperation_needCancel;
        string msg_DailyOperation_emptyDetail;
        string msg_DailyOperation_Tooltip1;
        string msg_DailyOperation_Tooltip2;
        string msg_DailyOperation_Tooltip3;
        string msg_DailyOperation_reload;
        string msg_DailyOperation_btConfirmOperation5;
        string msg_DailyOperation_selectShift;
        string language = null;


        static DataTable dtNewI = new DataTable();
        static DataTable dtNewR = new DataTable();
        static DataTable dtNewA = new DataTable();
        private DataTable alertLog10trips;
        private DataTable alertLog20trips;
        private DataTable alertLog30trips;

        protected void Page_Load(object sender, EventArgs e)
        {
            validateSession();
            applyLanguage();
            FillInactiveBusDriver(cbShift.SelectedValue, cbVendorAdm.SelectedValue);


            if (!IsPostBack)
            {
                fillVendorCombo();
                restorePageControls();
                btLoadData.Enabled = false;
                cbShift.Enabled = false;
                btExcel.Enabled = false;
            }

            if (cbShift.SelectedValue != string.Empty)
                disableOrEnableAddNewTripElements(true);
            else
                disableOrEnableAddNewTripElements(false);

            if ((bool)ViewState["searchStatus"] == false && (bool)ViewState["isSorting"] == false && (bool)ViewState["checkpoint1Editing"] == false)
            {
                if (cbShift.SelectedValue != string.Empty & btLoadData.Enabled == false )
                    fillGridCheckpoint1(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
            }

            if ((bool)ViewState["searchStatusCH2"] == false && (bool)ViewState["isSortingCH2"] == false && (bool)ViewState["checkpoint1EditingCH2"] == false)
            {
                if (cbShift.SelectedValue != string.Empty & btLoadData.Enabled == false)
                    fillGridCheckpoint2();
            }

            if ((bool)ViewState["searchStatusCH3"] == false && (bool)ViewState["isSortingCH3"] == false && (bool)ViewState["checkpoint1EditingCH3"] == false)
            {
                if (cbShift.SelectedValue != string.Empty & btLoadData.Enabled == false)
                    fillGridCheckpoint3();
            }

            translateControls(language);
            
        }

        private void validateSession()
        {
            usr = (Users)(Session["C_USER"]);
            if (usr == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void applyLanguage()
        {
            Users usr = (Users)(Session["C_USER"]);
            functions func = new functions();
            language = Session["Language"] != null ? Session["Language"].ToString() : language = functions.GetLanguage(usr);
            func.languageTranslate(this.Master, language);
        }

        private void restorePageControls()
        {
            btInactiveDetail.Enabled = false;
            btRevisionDetail.Enabled = false;
            btAlertDetail.Enabled = false;

            ViewState["searchStatus"] = false;
            ViewState["isSorting"] = false;
            ViewState["checkpoint1Editing"] = false;

            ViewState["searchStatusCH2"] = false;
            ViewState["isSortingCH2"] = false;
            ViewState["checkpoint1EditingCH2"] = false;

            ViewState["searchStatusCH3"] = false;
            ViewState["isSortingCH3"] = false;
            ViewState["checkpoint1EditingCH3"] = false;
        }

        private void disableOrEnableAddNewTripElements(bool status)
        {
            btAdd.Enabled = status;
            tbComments.Enabled = status;
            tbCheckPoint3.Enabled = status;
            ddl_BD_New_Trip.Enabled = status;
            ddl_Route_NewTrip.Enabled = status;
            //btExcel.Enabled = status;
            tbSearchCH1.Enabled = status;
            btSearchCH1.Enabled = status;
            btRefreshCH1.Enabled = status;
            tbSearchCH2.Enabled = status;
            btSearchCH2.Enabled = status;
            btRefreshCH2.Enabled = status;
            tbSearchCH3.Enabled = status;
            btSearchCH3.Enabled = status;
            btRefreshCH3.Enabled = status;
        }

        private void fillShiftCombo()
        {
            if (cbShift.Items.Count <= 0)
            {
                try
                {
                    Shift sf = new Shift();
                    DataTable dtShift = GenericClass.SQLSelectObj(sf);

                    cbShift.Items.Clear();
                    cbShift.DataSource = dtShift;
                    cbShift.DataValueField = "Shift_ID";
                    cbShift.DataTextField = "Name";
                    cbShift.DataBind();
                }
                catch (Exception ex)
                {
                    functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
                }
            }
        }
        private void fillVendorCombo()
        {
            try
            {
                Users usr = (Users)(Session["C_USER"]);
                Vendor initialVendor = new Vendor();
                DataTable dt = GenericClass.SQLSelectObj(initialVendor, WhereClause: usr.Profile.Equals("INTERNAL") ? "" : "Where [Vendor_ID]= '" + usr.Vendor_ID + "'");
                DataView dvVendor = new DataView(dt);
                DataTable distincVendor = dvVendor.ToTable(true, "VENDOR_ID", "NAME");
                int idxVendor = cbVendorAdm.SelectedIndex;
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
                cbVendorAdm.SelectedIndex = idxVendor;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }


        private string getMasterTrip()
        {
            //Get Master_Trip_ID to set masterTripIDHidden.Value
            try
            {
                DataTable dtGetMasterTrip = GenericClass.GetCustomData("select Master_Trip_ID from Master_Trip where Shift_ID='" + cbShift.SelectedValue.ToString() + "'"
                           + " and Vendor_ID='" + cbVendorAdm.SelectedValue.ToString() + "'" + " and Status IN ('PENDINGAPPROVE' ,'OPEN')");
                if (dtGetMasterTrip.Rows.Count>0)
                {
                    masterTripIDHidden.Value = dtGetMasterTrip.Rows[0]["Master_Trip_ID"].ToString();    
                }
                
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            return masterTripIDHidden.Value;
        }

        private void fillGridCheckpoint1(string shift, string vendor)
        {
            DataTable dtCheckpoint1 = new DataTable();
            try
            {
                

                dtCheckpoint1 = GenericClass.InsertToHRD(shift, vendor);
                getMasterTrip();

                if (dtCheckpoint1.Rows.Count > 0)
                {

                    ViewState["myViewState"] = dtCheckpoint1;
                    Grid_Checkpoint1.DataSource = dtCheckpoint1;
                    DateTime ts = new DateTime();
                    ts = (DateTime)dtCheckpoint1.Rows[0]["Trip_date"];
                    lbDate.Text = ts.ToString("dd/MM/yyyy");
                    Grid_Checkpoint1.PageSize = 15;
                    Grid_Checkpoint1.DataBind();
                    
                    //fillDropDownListAddNewTrip(shift, vendor);

                }
                else
                {
                    DataTable dummyTable = new DataTable();
                    dummyTable.Columns.Add("Trip_Hrd_ID", typeof(string));
                    dummyTable.Columns.Add("Bus_Driver_ID", typeof(string));
                    dummyTable.Columns.Add("End_Point", typeof(bool));
                    dummyTable.Columns.Add("TransbordCH1", typeof(string));
                    dummyTable.Columns.Add("Bus_Driver", typeof(string));
                    dummyTable.Columns.Add("Bus_ID", typeof(string));
                    dummyTable.Columns.Add("Driver", typeof(string));
                    dummyTable.Columns.Add("Driver_ID", typeof(string));
                    dummyTable.Columns.Add("Route", typeof(string));
                    dummyTable.Columns.Add("Check_Point_Time", typeof(string));
                    dummyTable.Columns.Add("Comment", typeof(string));
                    dummyTable.Columns.Add("Trip_Start", typeof(string));
                    dummyTable.Columns.Add("Psg_Init", typeof(string));

                    dummyTable.Rows.Add("0", "NA", false, "0", "NA", "0", "NA", "NA", "NA", "00:00", "NA", "00:00", "0");
                    Grid_Checkpoint1.DataSource = dummyTable;
                    Grid_Checkpoint1.PageSize = 15;
                    Grid_Checkpoint1.DataBind();

                }
            }

            catch (Exception ex)
            {
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
                    getMasterTrip();

                btConfirmOperation.Enabled = false;
                btCancelOperation.Enabled = false;
                btRefreshCH1.Enabled = false;
                btRefreshCH2.Enabled = false;
                btRefreshCH3.Enabled = false;
                btExcel.Enabled = false;
            }

        }

        private void fillGridCheckpoint2()
        {
            try
            {
                if (Grid_Checkpoint1.Rows.Count>0)
                {

                    Trip_Hrd tripH = new Trip_Hrd();
                    DataTable dtCheckpoint2 = GenericClass.SQLSelectObj(tripH, mappingQueryName: "DO-Checkpoint2", WhereClause:
                               "WHERE [Master_Trip_ID]=" + "'" + masterTripIDHidden.Value + "'" + " AND [TRIP_DETAIL].[Sequence]=2");
                    Grid_CheckPoint2.DataSource = dtCheckpoint2;
                    Grid_CheckPoint2.PageSize = 15;
                    ViewState["myViewState2"] = dtCheckpoint2;
                    Grid_CheckPoint2.DataBind();
                };
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        private void fillGridCheckpoint3()
        {
            try
            {
                if (Grid_Checkpoint1.Rows.Count > 0)
                {
                    Trip_Hrd tripH3 = new Trip_Hrd();
                    DataTable dtCheckpoint3 = GenericClass.SQLSelectObj(tripH3, mappingQueryName: "DO-Checkpoint3", WhereClause:
                               "WHERE [Master_Trip_ID]=" + "'" + masterTripIDHidden.Value + "'" + " AND [TRIP_DETAIL].[Sequence]=3");
                    Grid_CheckPoint3.DataSource = dtCheckpoint3;
                    Grid_CheckPoint3.PageSize = 15;
                    ViewState["myViewState3"] = dtCheckpoint3;
                    Grid_CheckPoint3.DataBind();
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        private void fillAllGrids()
        {
            //fill alertlog datatable with alerts with codes for each qty of trips
            alertLog10trips = NumbreOfTrips(cbVendorAdm.SelectedValue, "Dr2");
            alertLog20trips = NumbreOfTrips(cbVendorAdm.SelectedValue, "Dr3");
            alertLog30trips = NumbreOfTrips(cbVendorAdm.SelectedValue, "Dr4");

            fillGridCheckpoint1(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
            fillGridCheckpoint2();
            fillGridCheckpoint3();

        }



        protected void cbBusDriver2_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cbBusDriver = (DropDownList)sender;
            Label lbBusIDH = (Label)Grid_Checkpoint1.HeaderRow.FindControl("lbBusIDH") as Label;
            Label lbDriverH = (Label)Grid_Checkpoint1.HeaderRow.FindControl("lbDriverH") as Label;
            if (cbBusDriver.SelectedIndex != -1)
            {
                string datos = cbBusDriver.SelectedItem.Text;

                string[] datosArray = datos.Split('/');
                lbBusIDH.Text = datosArray[0];
                lbDriverH.Text = datosArray[1];

            }
            else
            {
                lbBusIDH.Text = "NA";
                lbDriverH.Text = "NA";
            }
        }

        protected void cbBusDriver1_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownList cbBusDriver = (DropDownList)sender;

            Label lbBusIDH = (Label)Grid_Checkpoint1.Rows[Grid_Checkpoint1.EditIndex].FindControl("lbBus") as Label;
            Label lbDriverH = (Label)Grid_Checkpoint1.Rows[Grid_Checkpoint1.EditIndex].FindControl("lbDriverName") as Label;
            if (cbBusDriver.SelectedIndex != -1)
            {
                string datos = cbBusDriver.SelectedItem.Text;

                string[] datosArray = datos.Split('/');
                lbBusIDH.Text = datosArray[0];
                lbDriverH.Text = datosArray[1];

            }
            else
            {
                lbBusIDH.Text = "NA";
                lbDriverH.Text = "NA";
            }
        }

        private void fillDropDownListAddNewTrip(string shift, string vendor)
        {
            try
            {


                Bus_Driver busDriver = new Bus_Driver();
                DataTable dtBusDriver = GenericClass.SQLSelectObj(busDriver, mappingQueryName: "DST-BUS_DRIVER_ID", WhereClause:
                "Where BUS_DRIVER.Bus_Driver_ID not in (select Bus_Driver_ID from TRIP_HRD" +
                " left join MASTER_TRIP on (MASTER_TRIP.Master_Trip_ID=TRIP_HRD.Master_Trip_ID)" +
                " where MASTER_TRIP.Master_Trip_ID =" + "'" + masterTripIDHidden.Value + "'" + ")" +
                " and BUS_DRIVER.Vendor_ID=" + "'" + vendor + "'" + " and BUS_DRIVER.Shift_ID=" + "'" + shift + "'" + "and BUS_DRIVER.Is_Active=1" +
                " and BUS.Vendor_ID+BUS.Bus_ID not in (select SUBSTRING(Bus_Driver_ID,0,CHARINDEX('-',Bus_Driver_ID)) from TRIP_HRD left join MASTER_TRIP" +
                " on (MASTER_TRIP.Master_Trip_ID=TRIP_HRD.Master_Trip_ID) where MASTER_TRIP.Master_Trip_ID ='" + masterTripIDHidden.Value + "') " +
                " and CAST(Driver.Driver_ID AS VARCHAR(MAX)) not in (select SUBSTRING(Bus_Driver_ID,CHARINDEX('-',Bus_Driver_ID)+1,LEN(Driver.Driver_ID)) from TRIP_HRD left join MASTER_TRIP" +
                " on (MASTER_TRIP.Master_Trip_ID=TRIP_HRD.Master_Trip_ID) where MASTER_TRIP.Master_Trip_ID ='" + masterTripIDHidden.Value + "')");
                ddl_BD_New_Trip.Items.Clear();
                ddl_BD_New_Trip.DataSource = dtBusDriver;
                ddl_BD_New_Trip.DataValueField = "Bus_Driver_ID";
                ddl_BD_New_Trip.DataTextField = "Bus_Driver";
                ddl_BD_New_Trip.DataBind();


                Route initialRoute = new Route();
                DataTable dtRoute = GenericClass.SQLSelectObj(initialRoute, mappingQueryName: "DO-Route", WhereClause: " WHERE Stop1.End_Point = (SELECT DISTINCT(SHIFT.Exit_Shift) FROM MASTER_TRIP"
                + " LEFT JOIN SHIFT ON MASTER_TRIP.Shift_ID = SHIFT.Shift_ID"
                + " WHERE MASTER_TRIP.Master_Trip_ID =" + "'" + masterTripIDHidden.Value + "'"
                + " and Stop1.Middle_Point=0)");

                ddl_Route_NewTrip.Items.Clear();
                ddl_Route_NewTrip.DataSource = dtRoute;
                ddl_Route_NewTrip.DataValueField = "Route_ID";
                ddl_Route_NewTrip.DataTextField = "Name";
                ddl_Route_NewTrip.DataBind();

                Label lbBusIDH = (Label)Grid_Checkpoint1.HeaderRow.FindControl("lbBusIDH") as Label;
                Label lbDriverH = (Label)Grid_Checkpoint1.HeaderRow.FindControl("lbDriverH") as Label;
                if (ddl_BD_New_Trip.SelectedIndex != -1)
                {
                    string datos = ddl_BD_New_Trip.SelectedItem.Text;

                    string[] datosArray = datos.Split('/');
                    lbBusIDH.Text = datosArray[0];
                    lbDriverH.Text = datosArray[1];

                }
                else
                {
                    lbBusIDH.Text = "NA";
                    lbDriverH.Text = "NA";
                }
            }

            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        private void fillCombosCheckpoint2(DropDownList cbcCheck2Item, string checkpoint)
        {
            string whereString;
            switch (checkpoint)
            {
                case "Checkpoint2": whereString = "WHERE Middle_Point=1";
                    break;
                case "Checkpoint3": whereString = "WHERE End_Point=1";
                    break;
                default: whereString = "";
                    break;
            }

            if (cbcCheck2Item.Items.Count <= 0)
            {
                Stop_Point stopPoint = new Stop_Point();
                DataTable dtStopPoint = GenericClass.SQLSelectObj(stopPoint, WhereClause: whereString);
                cbcCheck2Item.Items.Clear();
                cbcCheck2Item.DataSource = dtStopPoint;
                cbcCheck2Item.DataValueField = "Stop_ID";
                cbcCheck2Item.DataTextField = "Stop_ID";
                cbcCheck2Item.DataBind();
            }
        }
        public void FillInactiveBusDriver(string shift, string vendor)
        {
            try
            {
                inactiveCount.Text = "0";
                revisionCount.Text = "0";
                AlertCount.Text = "0";

                if (cbShiftType.SelectedValue != "NONE" & cbShift.SelectedIndex > -1)
                {


                    List<string> lsBusDriver = new List<string>();
                    lsBusDriver.Add("Bus_ID");
                    lsBusDriver.Add("Driver_ID");
                    lsBusDriver.Add("Is_Active");

                    DataTable dtRevision = GenericClass.SQLSelectObj(new Bus_Driver(), lsBusDriver, WhereClause: "Where Bus_Driver.Shift_ID='" + shift + "' and Bus_Driver.Vendor_ID='" + vendor + "'",
                        mappingQueryName: "Revision-BusDriver");

                    DataTable dtInactive = GenericClass.SQLSelectObj(new Bus_Driver(), lsBusDriver,
                        WhereClause: "Where Bus_Driver.Shift_ID='" + shift + "' and Bus_Driver.Vendor_ID='" + vendor + "' and Bus_Driver.Is_Active=0",
                        mappingQueryName: "Inactive-BusDriver");

                    List<string> lsAlert = new List<string>();
                    lsAlert.Add("Alert_log_ID");

                    DataTable dtAlert = GenericClass.SQLSelectObj(new Alert_Log(), lsAlert, WhereClause: "Where AL.Status in ('OPEN','WORKING') AND Bus.Vendor_ID='" + vendor + "' OR Driver.Vendor_ID='" + vendor + "'",
                       mappingQueryName: "Alert-BusDriver");

                    GridView_Revision.DataSource = dtRevision;
                    GridView_Revision.DataBind();
                    ViewState["myViewState5"] = dtRevision;

                    GridView_Alerts.DataSource = dtAlert;
                    GridView_Alerts.DataBind();
                    ViewState["myViewState6"] = dtAlert;

                    GridView_InactiveDetails.DataSource = dtInactive;
                    GridView_InactiveDetails.DataBind();
                    ViewState["myViewState4"] = dtInactive;

                    if (dtInactive.Rows.Count == 0)
                    {
                        btInactiveDetail.Enabled = false;
                    }

                    if (dtRevision.Rows.Count == 0)
                    {
                        btRevisionDetail.Enabled = false;
                    }

                    if (dtAlert.Rows.Count == 0)
                    {
                        btAlertDetail.Enabled = false;
                    }


                    inactiveCount.Text = dtInactive.Rows.Count.ToString();
                    revisionCount.Text = dtRevision.Rows.Count.ToString();
                    AlertCount.Text = dtAlert.Rows.Count.ToString();
                }

            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void loadData(object sender, EventArgs e)
        {
            if (cbShiftType.SelectedValue.Equals("1"))
            {
                UpdatePanel2.Visible = false;
                UpdatePanel5.Visible = false;
            }
            else
            {
                UpdatePanel2.Visible = true;
                UpdatePanel5.Visible = true;
                    
            }
            validateSession();
            masterTripIDHidden.Value = string.Empty;
            btCancelOperation.Enabled = true;
            btConfirmOperation.Enabled = true;
            btSearchCH1.Enabled = true;
            btSearchCH2.Enabled = true;
            btSearchCH3.Enabled = true;
            cbShift.Enabled = false;
            cbVendorAdm.Enabled = false;
            btChange.Enabled = true;
            btLoadData.Enabled = false;
            btExcel.Enabled = true;
            cbShiftType.Enabled = false;
            btInactiveDetail.Enabled = true;
            btRevisionDetail.Enabled = true;
            btAlertDetail.Enabled = true;
            btRefreshCH1.Enabled = true;
            btRefreshCH2.Enabled = true;
            btRefreshCH3.Enabled = true;
            tbCheckPoint3.Text = string.Empty;
            tbComments.Text = string.Empty;
            fillAllGrids();
            lbVendorInfo.Text = cbVendorAdm.SelectedItem.Text;
            lbShiftInfo.Text = cbShift.SelectedItem.Text;
            shiftValue = cbShift.SelectedValue.ToString();
            if (Grid_Checkpoint1.Rows.Count>0)
            {
                fillDropDownListAddNewTrip(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue); 
            }
            FillInactiveBusDriver(shiftValue, cbVendorAdm.SelectedValue);
            //translateValidators(language);
        }



        private void translateControls(string language)
        {

            if (language.Equals("Eng"))
            {
                cbShiftType.Items[0].Text = "Select";
                cbShiftType.Items[1].Text = "Entry";
                cbShiftType.Items[2].Text = "Exit";

            }

            msg_DailyOperation_btAddHeader = (string)GetGlobalResourceObject(language, "msg_DailyOperation_btAddHeader");
            msg_DailyOperation_CH1RowUpdating = (string)GetGlobalResourceObject(language, "msg_DailyOperation_CH1RowUpdating");
            msg_DailyOperation_btDelete = (string)GetGlobalResourceObject(language, "msg_DailyOperation_btDelete");
            msg_DailyOperation_CH1RowDeleting = (string)GetGlobalResourceObject(language, "msg_DailyOperation_CH1RowDeleting");
            msg_DailyOperation_CH1RowDeleting2 = (string)GetGlobalResourceObject(language, "msg_DailyOperation_CH1RowDeleting2");
            msg_DailyOperation_CH2RowUpdating = (string)GetGlobalResourceObject(language, "msg_DailyOperation_CH2RowUpdating");
            msg_DailyOperation_CH2RowUpdating2 = (string)GetGlobalResourceObject(language, "msg_DailyOperation_CH2RowUpdating2");
            msg_DailyOperation_btTransferCH1 = (string)GetGlobalResourceObject(language, "msg_DailyOperation_btTransferCH1");
            msg_DailyOperation_btTransferCH2 = (string)GetGlobalResourceObject(language, "msg_DailyOperation_btTransferCH2");
            msg_DailyOperation_btEndPoint = (string)GetGlobalResourceObject(language, "msg_DailyOperation_btEndPoint");
            msg_DailyOperation_btEndPoint2 = (string)GetGlobalResourceObject(language, "msg_DailyOperation_btEndPoint2");
            msg_DailyOperation_CH3RowUpdating = (string)GetGlobalResourceObject(language, "msg_DailyOperation_CH3RowUpdating");
            msg_DailyOperation_CH3RowUpdating2 = (string)GetGlobalResourceObject(language, "msg_DailyOperation_CH3RowUpdating2");
            msg_DailyOperation_btConfirmOperation2 = (string)GetGlobalResourceObject(language, "msg_DailyOperation_btConfirmOperation2");
            msg_DailyOperation_shift = (string)GetGlobalResourceObject(language, "msg_DailyOperation_shift");
            msg_DailyOperation_btConfirmOperationOk = (string)GetGlobalResourceObject(language, "msg_DailyOperation_btConfirmOperationOk");
            msg_DailyOperation_btConfirmOperationEmail = (string)GetGlobalResourceObject(language, "msg_DailyOperation_btConfirmOperationEmail");
            msg_DailyOperation_btCancelOperation = (string)GetGlobalResourceObject(language, "msg_DailyOperation_btCancelOperation");
            msg_DailyOperation_btCancelOperation2 = (string)GetGlobalResourceObject(language, "msg_DailyOperation_btCancelOperation2");
            msg_DailyOperation_btCancelOperation3 = (string)GetGlobalResourceObject(language, "msg_DailyOperation_btCancelOperation3");
            msg_DailyOperation_Refresh1 = (string)GetGlobalResourceObject(language, "msg_DailyOperation_Refresh1");
            msg_DailyOperation_Refresh2 = (string)GetGlobalResourceObject(language, "msg_DailyOperation_Refresh2");
            msg_DailyOperation_Refresh3 = (string)GetGlobalResourceObject(language, "msg_DailyOperation_Refresh3");
            msg_DailyOperation_btConfirmOperation = (string)GetGlobalResourceObject(language, "msg_DailyOperation_btConfirmOperation");
            msg_DailyOperation_reload = (string)GetGlobalResourceObject(language, "msg_DailyOperation_reload");
            msg_DailyOperation_btConfirmOperation5 = (string)GetGlobalResourceObject(language, "msg_DailyOperation_btConfirmOperation5");
            msg_DailyOperation_selectShift = (string)GetGlobalResourceObject(language, "msg_DailyOperation_selectShift");
            msg_DailyOperation_needCancel = (string)GetGlobalResourceObject(language, "msg_DailyOperation_needCancel");
            msg_DailyOperation_emptyDetail = (string)GetGlobalResourceObject(language, "msg_DailyOperation_emptyDetail");
            msg_DailyOperation_Tooltip1 = (string)GetGlobalResourceObject(language, "msg_DailyOperation_Tooltip1");
            msg_DailyOperation_Tooltip2 = (string)GetGlobalResourceObject(language, "msg_DailyOperation_Tooltip2");
            msg_DailyOperation_Tooltip3 = (string)GetGlobalResourceObject(language, "msg_DailyOperation_Tooltip3");

        }

        protected void btAddHeader(object sender, EventArgs e)
        {
            try
            {
                validateSession();
                DateTime dtime = new DateTime();
                DateTime.TryParse(tbCheckPoint3.Text, out dtime);
                TimeSpan ts = new TimeSpan(dtime.Hour, dtime.Minute, dtime.Second);

                Trip_Hrd tripHeader = new Trip_Hrd();
                tripHeader.Bus_Driver_ID = ddl_BD_New_Trip.SelectedValue.ToString();
                tripHeader.Route_ID = Int32.Parse(ddl_Route_NewTrip.SelectedValue.ToString());
                tripHeader.Check_Point_Time = ts;
                tripHeader.Comment = tbComments.Text;
                tripHeader.Master_Trip_ID = masterTripIDHidden.Value;
                GenericClass.SQLInsertObj(tripHeader);

                int route = Int32.Parse(ddl_Route_NewTrip.SelectedValue);
                Route routeObj = new Route();
                DataTable dtRoute = GenericClass.SQLSelectObj(routeObj, WhereClause: "WHERE Route_ID=" + route);
                string stop_PointID = dtRoute.Rows[0]["Org_ID"].ToString();

                DataTable dtBusDriver1 = GenericClass.SQLSelectObj(tripHeader, WhereClause: "WHERE Bus_Driver_ID=" + "'" + tripHeader.Bus_Driver_ID + "'" + " and Master_Trip_ID=" + "'" + tripHeader.Master_Trip_ID + "'");
                int idHrd = Int32.Parse(dtBusDriver1.Rows[0]["Trip_Hrd_ID"].ToString());
                //Pendientel: Validar que exista un id y si lo hay entonces hacer el insert de abajo
                Trip_Detail tripDetail = new Trip_Detail();
                TimeSpan tsStart = new TimeSpan(0, 0, 0);

                tripDetail.Trip_Hrd_ID = idHrd;
                tripDetail.Trip_Start = tsStart;
                tripDetail.Trip_End = tsStart;
                //tripDetail.Psg_Audit = 0;
                tripDetail.Trip_ID_Transbord = string.Empty;
                tripDetail.Comment = string.Empty;
                tripDetail.Sequence = 1;
                tripDetail.Psg_Init = 0;
                tripDetail.End_Point = false;
                tripDetail.Stop_point_ID = stop_PointID;
                GenericClass.SQLInsertObj(tripDetail);
                functions.ShowMessage(this, this.GetType(), msg_DailyOperation_btAddHeader, MessageType.Success);
                fillDropDownListAddNewTrip(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
                ViewState["checkpoint1Editing"] = false;
                fillGridCheckpoint1(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
                tbCheckPoint3.Text = string.Empty;
                tbComments.Text = string.Empty;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void Grid_Checkpoint1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                
                DataTable getDataSorting = null;
                ViewState["checkpoint1Editing"] = true;
                if ((bool)ViewState["isSorting"] == true)
                    getDataSorting = ((DataView)Session["Objects_DO"]).ToTable();

                DataTable getDataSearch = (DataTable)ViewState["searchResults"];
                Grid_Checkpoint1.EditIndex = e.NewEditIndex;

                if ((bool)ViewState["searchStatus"] == true && getDataSearch.Rows.Count > 0 && (bool)ViewState["isSorting"] == false)
                {
                    Grid_Checkpoint1.DataSource = getDataSearch;
                    Grid_Checkpoint1.DataBind();
                }
                if ((bool)ViewState["isSorting"] == true && getDataSorting.Rows.Count > 0 && (bool)ViewState["searchStatus"] == false)
                {
                    Grid_Checkpoint1.DataSource = getDataSorting;
                    Grid_Checkpoint1.DataBind();
                }
                if ((bool)ViewState["isSorting"] == false && (bool)ViewState["searchStatus"] == false)
                {
                    fillGridCheckpoint1(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
                }

                TextBox txtHora = (TextBox)Grid_Checkpoint1.Rows[Grid_Checkpoint1.EditIndex].FindControl("tbHoraIni2") as TextBox;
                txtHora.Focus();

                string vendor = cbVendorAdm.SelectedValue;
                string shift = cbShift.SelectedValue.ToString();

                Label lbBusDriver = (Label)Grid_Checkpoint1.Rows[Grid_Checkpoint1.EditIndex].FindControl("lbBusDriverHide") as Label;
                Label lbBusDriverID = (Label)Grid_Checkpoint1.Rows[Grid_Checkpoint1.EditIndex].FindControl("lbBusDriverID") as Label;
                DropDownList cbBusDriver1 = (DropDownList)Grid_Checkpoint1.Rows[Grid_Checkpoint1.EditIndex].FindControl("cbBusDriver1") as DropDownList;

                Bus_Driver busDriver1 = new Bus_Driver();
                DataTable dtBusDriver1 = GenericClass.SQLSelectObj(busDriver1, mappingQueryName: "DST-BUS_DRIVER_ID", WhereClause:
                "Where BUS_DRIVER.Bus_Driver_ID not in (select Bus_Driver_ID from TRIP_HRD" +
                " left join MASTER_TRIP on (MASTER_TRIP.Master_Trip_ID=TRIP_HRD.Master_Trip_ID)" +
                " where MASTER_TRIP.Master_Trip_ID =" + "'" + masterTripIDHidden.Value + "'" + ")" +
                " and BUS_DRIVER.Vendor_ID=" + "'" + vendor + "'" + " and BUS_DRIVER.Shift_ID=" + "'" + shift + "'" + "and BUS_DRIVER.Is_Active=1" +
                " and BUS.Vendor_ID+BUS.Bus_ID not in (select SUBSTRING(Bus_Driver_ID,0,CHARINDEX('-',Bus_Driver_ID)) from TRIP_HRD left join MASTER_TRIP" +
                " on (MASTER_TRIP.Master_Trip_ID=TRIP_HRD.Master_Trip_ID) where MASTER_TRIP.Master_Trip_ID ='" + masterTripIDHidden.Value + "') " +
                " and CAST(Driver.Driver_ID AS VARCHAR(MAX)) not in (select SUBSTRING(Bus_Driver_ID,CHARINDEX('-',Bus_Driver_ID)+1,LEN(Driver.Driver_ID)) from TRIP_HRD left join MASTER_TRIP" +
                " on (MASTER_TRIP.Master_Trip_ID=TRIP_HRD.Master_Trip_ID) where MASTER_TRIP.Master_Trip_ID ='" + masterTripIDHidden.Value + "')");
                cbBusDriver1.Items.Clear();
                cbBusDriver1.DataSource = dtBusDriver1;
                cbBusDriver1.DataValueField = "Bus_Driver_ID";
                cbBusDriver1.DataTextField = "Bus_Driver";
                cbBusDriver1.DataBind();

                ListItem li = new ListItem(lbBusDriver.Text, lbBusDriverID.Text);
                cbBusDriver1.Items.Insert(0, li);
                cbBusDriver1.SelectedIndex = 0;


                Label lbRouteHide = (Label)Grid_Checkpoint1.Rows[Grid_Checkpoint1.EditIndex].FindControl("lbRouteHide") as Label;
                DropDownList cbRoute1 = (DropDownList)Grid_Checkpoint1.Rows[Grid_Checkpoint1.EditIndex].FindControl("cbRoute1") as DropDownList;

                Route initialRoute1 = new Route();
                DataTable dtRoute1 = GenericClass.SQLSelectObj(initialRoute1, mappingQueryName: "DO-Route", WhereClause: " WHERE Stop1.End_Point = (SELECT DISTINCT(SHIFT.Exit_Shift) FROM MASTER_TRIP"
               + " LEFT JOIN SHIFT ON MASTER_TRIP.Shift_ID = SHIFT.Shift_ID"
               + " WHERE MASTER_TRIP.Master_Trip_ID =" + "'" + masterTripIDHidden.Value + "'"
               + " and Stop1.Middle_Point=0)");
                cbRoute1.Items.Clear();
                cbRoute1.DataSource = dtRoute1;
                cbRoute1.DataValueField = "Route_ID";
                cbRoute1.DataTextField = "Name";
                cbRoute1.DataBind();

                int row = 0;
                foreach (ListItem ii in cbRoute1.Items)
                {
                    if (ii.Text == lbRouteHide.Text)
                    {
                        cbRoute1.SelectedIndex = row;

                        break;
                    }
                    row++;
                }

                cbRoute1.SelectedIndex = row;

                Grid_Checkpoint1.BottomPagerRow.Visible = false;

                disableOrEnableAddNewTripElements(false);
                //  fillDropDownListAddNewTrip(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            finally
            {
                Grid_Checkpoint1.EditIndex = -1;
                tbSearchCH1.Text = string.Empty;

            }
        }

        protected void Grid_Checkpoint1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                validateSession();
                Label lbID = (Label)Grid_Checkpoint1.Rows[e.RowIndex].FindControl("lbTripHD") as Label;
                Label lbBusDriverID = (Label)Grid_Checkpoint1.Rows[e.RowIndex].FindControl("lbBusDriverID") as Label;
                TextBox checktime = (TextBox)Grid_Checkpoint1.Rows[e.RowIndex].FindControl("tbCheckPoint2") as TextBox;
                TextBox tbcomments = (TextBox)Grid_Checkpoint1.Rows[e.RowIndex].FindControl("tbComments") as TextBox;
                TextBox tbstartTime = (TextBox)Grid_Checkpoint1.Rows[e.RowIndex].FindControl("tbHoraIni2") as TextBox;
                TextBox tbPassengers = (TextBox)Grid_Checkpoint1.Rows[e.RowIndex].FindControl("tbNoPassengers2") as TextBox;
                DropDownList cbBusDriver = (DropDownList)Grid_Checkpoint1.Rows[e.RowIndex].FindControl("cbBusDriver1") as DropDownList;
                DropDownList cbRoute = (DropDownList)Grid_Checkpoint1.Rows[e.RowIndex].FindControl("cbRoute1") as DropDownList;
                DateTime dtime = new DateTime();
                DateTime.TryParse(checktime.Text, out dtime);
                TimeSpan ts = new TimeSpan(dtime.Hour, dtime.Minute, dtime.Second);
                DateTime dtimeStart = new DateTime();
                DateTime.TryParse(tbstartTime.Text, out dtimeStart);
                TimeSpan tsStart = new TimeSpan(dtimeStart.Hour, dtimeStart.Minute, dtimeStart.Second);



                Trip_Hrd tripHeader = new Trip_Hrd();

                tripHeader.Bus_Driver_ID = cbBusDriver.SelectedValue.ToString();
                tripHeader.Route_ID = Int32.Parse(cbRoute.SelectedValue.ToString());
                tripHeader.Check_Point_Time = ts;

                tripHeader.Trip_Hrd_ID = Int32.Parse(lbID.Text);
                if (lbBusDriverID.Text != cbBusDriver.SelectedValue.ToString())
                { tripHeader.Bus_Driver_ID = cbBusDriver.SelectedValue.ToString(); }

                Trip_Detail tripDetail = new Trip_Detail();
                tripDetail.Trip_Start = tsStart;
                tripDetail.Psg_Init = Int32.Parse(tbPassengers.Text.Trim());
                tripDetail.Trip_Hrd_ID = tripHeader.Trip_Hrd_ID;
                tripDetail.Comment = tbcomments.Text;

                Dictionary<string, dynamic> dicUpdHdr = new Dictionary<string, dynamic>();
                if (lbBusDriverID.Text != cbBusDriver.SelectedValue.ToString())
                { dicUpdHdr.Add("Bus_Driver_ID", "'" + tripHeader.Bus_Driver_ID + "'"); }
                dicUpdHdr.Add("Route_ID", "'" + tripHeader.Route_ID + "'");
                dicUpdHdr.Add("Check_Point_Time", "'" + tripHeader.Check_Point_Time + "'");

                GenericClass.SQLUpdateObj(tripHeader, dicUpdHdr, "Where Trip_Hrd_ID=" + tripHeader.Trip_Hrd_ID);


                Dictionary<string, dynamic> dicUpdDtll = new Dictionary<string, dynamic>();
                dicUpdDtll.Add("Trip_Start", "'" + tripDetail.Trip_Start + "'");
                dicUpdDtll.Add("Psg_Init", "'" + tripDetail.Psg_Init + "'");
                dicUpdDtll.Add("Comment", "'" + tripDetail.Comment + "'");

                GenericClass.SQLUpdateObj(tripDetail, dicUpdDtll, "Where Trip_Hrd_ID=" + tripHeader.Trip_Hrd_ID + " and Sequence=1");

                functions.ShowMessage(this, this.GetType(), msg_DailyOperation_CH1RowUpdating, MessageType.Success);
                Grid_Checkpoint1.EditIndex = -1;
                ViewState["searchStatus"] = false;
                ViewState["isSorting"] = false;
                ViewState["checkpoint1Editing"] = false;
                fillDropDownListAddNewTrip(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
                fillAllGrids();
                disableOrEnableAddNewTripElements(true);

                tbSearchCH1.Text = string.Empty;

                Grid_Checkpoint1.BottomPagerRow.Visible = true;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        protected void Grid_Checkpoint1_CancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Grid_Checkpoint1.EditIndex = -1;

            tbSearchCH1.Text = string.Empty;

            ViewState["checkpoint1Editing"] = false;
            DataTable getDataSorting = null;
            DataTable getDataSearch = (DataTable)ViewState["searchResults"];

            if ((bool)ViewState["isSorting"] == true)
                getDataSorting = ((DataView)Session["Objects_DO"]).ToTable();

            if ((bool)ViewState["searchStatus"] == true && getDataSearch.Rows.Count > 0 && (bool)ViewState["isSorting"] == false)
            {
                Grid_Checkpoint1.DataSource = getDataSearch;
                Grid_Checkpoint1.DataBind();
            }
            if ((bool)ViewState["isSorting"] == true && getDataSorting.Rows.Count > 0 && (bool)ViewState["searchStatus"] == false)
            {
                Grid_Checkpoint1.DataSource = getDataSorting;
                Grid_Checkpoint1.DataBind();
            }
            if ((bool)ViewState["isSorting"] == false && (bool)ViewState["searchStatus"] == false)
            {
                fillGridCheckpoint1(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
            }
            disableOrEnableAddNewTripElements(true);

            Grid_Checkpoint1.BottomPagerRow.Visible = true;
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {

            try
            {
                LinkButton lnkRemove = (LinkButton)sender;
                Label lbID = (Label)Grid_Checkpoint1.HeaderRow.FindControl("lbTripHD") as Label;
                Trip_Hrd deleteHdr = new Trip_Hrd();
                Trip_Detail deleteDetail = new Trip_Detail();

                deleteHdr.Trip_Hrd_ID = Int32.Parse(lnkRemove.CommandArgument);
                deleteDetail.Trip_Hrd_ID = Int32.Parse(lnkRemove.CommandArgument);

                GenericClass.SQLDeleteObj(deleteDetail, "Where Trip_Hrd_ID=" + deleteDetail.Trip_Hrd_ID + " and Sequence=1");
                GenericClass.SQLDeleteObj(deleteHdr, "Where Trip_Hrd_ID=" + deleteHdr.Trip_Hrd_ID);
                functions.ShowMessage(this, this.GetType(), msg_DailyOperation_btDelete, MessageType.Success);
                fillGridCheckpoint1(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
                fillDropDownListAddNewTrip(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void Grid_Checkpoint1_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                Label id = (Label)Grid_Checkpoint1.Rows[e.RowIndex].FindControl("lbTripHD") as Label;
                TextBox tbHoraini = Grid_Checkpoint1.Rows[e.RowIndex].FindControl("tbHoraIni") as TextBox;
                TextBox tbNoPassengers = Grid_Checkpoint1.Rows[e.RowIndex].FindControl("tbNoPassengers") as TextBox;
                if (string.IsNullOrEmpty(tbNoPassengers.Text)) { tbNoPassengers.Text = "0"; }
                if (Int32.Parse(tbNoPassengers.Text) == 0 || string.IsNullOrEmpty(tbNoPassengers.Text))
                {
                    Trip_Hrd deleteHdr = new Trip_Hrd();
                    Trip_Detail deleteDetail = new Trip_Detail();

                    deleteHdr.Trip_Hrd_ID = Int32.Parse(id.Text);
                    deleteDetail.Trip_Hrd_ID = Int32.Parse(id.Text);

                    GenericClass.SQLDeleteObj(deleteDetail, "Where Trip_Hrd_ID=" + deleteDetail.Trip_Hrd_ID);
                    GenericClass.SQLDeleteObj(deleteHdr, "Where Trip_Hrd_ID=" + deleteHdr.Trip_Hrd_ID);
                    functions.ShowMessage(this, this.GetType(), msg_DailyOperation_CH1RowDeleting, MessageType.Success);
                    fillAllGrids();
                    fillDropDownListAddNewTrip(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
                }
                else
                {
                    functions.ShowMessage(this, this.GetType(), msg_DailyOperation_CH1RowDeleting2, MessageType.Warning);
                }
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
        private void execute_query()
        {
            try
            {
                if (Session["C_USER"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                Users usr = (Users)(Session["C_USER"]);
                DataTable dtLoad = GenericClass.InsertToHRD(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
                Session["Excel"] = dtLoad;
                Session["name"] = "Daily_Operation";
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void Grid_Checkpoint1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tbSearchCH1.Text = string.Empty;
            Grid_Checkpoint1.PageIndex = e.NewPageIndex;
            if ((bool)ViewState["searchStatus"] == true)
            { Grid_Checkpoint1.DataSource = ViewState["searchResults"]; }
            if ((bool)ViewState["isSorting"] == true)
            { Grid_Checkpoint1.DataSource = Session["Objects_DO"]; }
            Grid_Checkpoint1.DataBind();
            //fillDropDownListAddNewTrip(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
        }

        protected void Grid_Checkpoint1_Sorting(object sender, GridViewSortEventArgs e)
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

                DataTable dt = GenericClass.InsertToHRD(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
                DataView sortedView = new DataView(dt);
                sortedView.Sort = e.SortExpression + " " + sortingDirection;
                Session["Objects_DO"] = sortedView;
                Grid_Checkpoint1.DataSource = sortedView;
                Grid_Checkpoint1.DataBind();
                // fillDropDownListAddNewTrip(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);

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


        private void checktime_Enter(Object sender, EventArgs e)
        {

            TextBox checktime = (TextBox)Grid_Checkpoint1.HeaderRow.FindControl("tbCheckPoint3") as TextBox;
            checktime.Text = DateTime.Now.ToString("h:mm:ss tt");

        }

        protected void Grid_CheckPoint2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                Grid_CheckPoint2.EditIndex = e.NewEditIndex;
                Label lbstopPoint = (Label)Grid_CheckPoint2.Rows[Grid_CheckPoint2.EditIndex].FindControl("cbCheck2Item") as Label;
                tbSearchCH2.Text = string.Empty;

                ViewState["checkpoint1EditingCH2"] = true;
                DataTable getDataSorting = null;
                DataTable getDataSearch = (DataTable)ViewState["searchResultsCH2"];

                if ((bool)ViewState["isSortingCH2"] == true)
                    getDataSorting = ((DataView)Session["ObjectsCH2"]).ToTable();

                if ((bool)ViewState["searchStatusCH2"] == true && getDataSearch.Rows.Count > 0 && (bool)ViewState["isSortingCH2"] == false)
                {
                    Grid_CheckPoint2.DataSource = getDataSearch;
                    Grid_CheckPoint2.DataBind();
                }
                if ((bool)ViewState["isSortingCH2"] == true && getDataSorting.Rows.Count > 0 && (bool)ViewState["searchStatusCH2"] == false)
                {
                    Grid_CheckPoint2.DataSource = getDataSorting;
                    Grid_CheckPoint2.DataBind();
                }
                if ((bool)ViewState["isSortingCH2"] == false && (bool)ViewState["searchStatusCH2"] == false)
                {
                    fillGridCheckpoint2();
                }

                TextBox tbHoraChk2 = (TextBox)Grid_CheckPoint2.Rows[Grid_CheckPoint2.EditIndex].FindControl("tbHoraCheck2") as TextBox;

                tbHoraChk2.Focus();

                DropDownList cbCheck2Edit = (DropDownList)Grid_CheckPoint2.Rows[Grid_CheckPoint2.EditIndex].FindControl("cbCheck2Edit") as DropDownList;

                ImageButton btTransbord = (ImageButton)Grid_CheckPoint2.Rows[Grid_CheckPoint2.EditIndex].FindControl("btTransfer2") as ImageButton;

                btTransbord.Enabled = false;
                if (cbCheck2Edit.Items.Count > 0)
                {
                    if (lbstopPoint.Text != "--" || !string.IsNullOrEmpty(lbstopPoint.Text))
                    {
                        cbCheck2Edit.SelectedValue = lbstopPoint.Text.Trim();
                    }
                }

                foreach (GridViewRow row in Grid_CheckPoint2.Rows)
                {
                    if (row.RowIndex != e.NewEditIndex)
                    {
                        row.Enabled = false;
                    }
                }

                Grid_CheckPoint2.BottomPagerRow.Visible = false;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            finally
            {
                Grid_CheckPoint2.EditIndex = -1;
                tbSearchCH2.Text = string.Empty;

            }
        }

        protected void Grid_CheckPoint2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                validateSession();
                bool timeError = false;
                Label lbID = (Label)Grid_CheckPoint2.Rows[e.RowIndex].FindControl("lbTripHD") as Label;
                TextBox comment = Grid_CheckPoint2.Rows[e.RowIndex].FindControl("lbCommentEdit") as TextBox;
                TextBox tbHoraCheck2 = Grid_CheckPoint2.Rows[e.RowIndex].FindControl("tbHoraCheck2") as TextBox;
                TextBox tbNoPassengersCheck2 = Grid_CheckPoint2.Rows[e.RowIndex].FindControl("tbNoPassengersCheck2") as TextBox;

                DropDownList cbCheck2Edit = Grid_CheckPoint2.Rows[e.RowIndex].FindControl("cbCheck2Edit") as DropDownList;


                DateTime dtime = new DateTime();
                DateTime.TryParse(tbHoraCheck2.Text, out dtime);
                TimeSpan ts = new TimeSpan(dtime.Hour, dtime.Minute, dtime.Second);

                Trip_Detail tripDetail = new Trip_Detail();
                tripDetail.Trip_Start = ts;
                tripDetail.Psg_Init = Int32.Parse(tbNoPassengersCheck2.Text.Trim());
                tripDetail.Trip_Hrd_ID = Int32.Parse(lbID.Text);
                tripDetail.Stop_point_ID = cbCheck2Edit.SelectedValue.ToString();
                tripDetail.Comment = comment.Text;

                DataTable getTimeCH1 = GenericClass.SQLSelectObj(new Trip_Detail(), WhereClause: "Where Trip_Hrd_ID=" + tripDetail.Trip_Hrd_ID + " and Sequence=1");
                if (getTimeCH1.Rows.Count > 0)
                {
                    DateTime timeCH = new DateTime();
                    DateTime.TryParse(getTimeCH1.Rows[0]["Trip_Start"].ToString(), out timeCH);
                    TimeSpan tsCH = new TimeSpan(timeCH.Hour, timeCH.Minute, timeCH.Second);
                    if (ts <= tsCH)
                    {
                        timeError = true;
                    }
                }
                if (timeError == true)
                {
                    functions.ShowMessage(this, this.GetType(), msg_DailyOperation_CH2RowUpdating, MessageType.Warning);
                }
                else
                {
                    Dictionary<string, dynamic> dicUpdDtll = new Dictionary<string, dynamic>();
                    dicUpdDtll.Add("Trip_Start", "'" + tripDetail.Trip_Start + "'");
                    dicUpdDtll.Add("Psg_Init", "'" + tripDetail.Psg_Init + "'");
                    dicUpdDtll.Add("Trip_Hrd_ID", "'" + tripDetail.Trip_Hrd_ID + "'");
                    dicUpdDtll.Add("Stop_point_ID", "'" + tripDetail.Stop_point_ID + "'");
                    dicUpdDtll.Add("Comment", "'" + tripDetail.Comment + "'");

                    GenericClass.SQLUpdateObj(tripDetail, dicUpdDtll, "Where Trip_Hrd_ID=" + tripDetail.Trip_Hrd_ID + " and Sequence=2");

                    functions.ShowMessage(this, this.GetType(), msg_DailyOperation_CH2RowUpdating2, MessageType.Success);
                    Grid_CheckPoint2.EditIndex = -1;
                    ViewState["checkpoint1EditingCH2"] = false;
                    ViewState["searchStatusCH2"] = false;
                    ViewState["isSortingCH2"] = false;

                    fillAllGrids();
                }
                tbSearchCH2.Text = string.Empty;

                Grid_CheckPoint2.BottomPagerRow.Visible = true;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void Grid_CheckPoint2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Grid_CheckPoint2.EditIndex = -1;
            tbSearchCH2.Text = string.Empty;
            //  fillGridCheckpoint2();
            ViewState["checkpoint1EditingCH2"] = false;
            DataTable getDataSorting = null;
            DataTable getDataSearch = (DataTable)ViewState["searchResultsCH2"];

            if ((bool)ViewState["isSortingCH2"] == true)
                getDataSorting = ((DataView)Session["ObjectsCH2"]).ToTable();

            if ((bool)ViewState["searchStatusCH2"] == true && getDataSearch.Rows.Count > 0 && (bool)ViewState["isSortingCH2"] == false)
            {
                Grid_CheckPoint2.DataSource = getDataSearch;
                Grid_CheckPoint2.DataBind();
            }
            if ((bool)ViewState["isSortingCH2"] == true && getDataSorting.Rows.Count > 0 && (bool)ViewState["searchStatusCH2"] == false)
            {
                Grid_CheckPoint2.DataSource = getDataSorting;
                Grid_CheckPoint2.DataBind();
            }
            if ((bool)ViewState["isSortingCH2"] == false && (bool)ViewState["searchStatusCH2"] == false)
            {
                fillGridCheckpoint2();
            }

            Grid_CheckPoint2.BottomPagerRow.Visible = true;
        }

        protected void Grid_CheckPoint2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                    {
                        DropDownList cbcCheck2 = e.Row.FindControl("cbCheck2Edit") as DropDownList;
                        fillCombosCheckpoint2(cbcCheck2, "Checkpoint2");
                    }
                    else
                    {
                        Label lbBus = e.Row.FindControl("lbBus") as Label;
                        TextBox tbTransbord = e.Row.FindControl("tbTransbordCH2") as TextBox;
                        Label comment = e.Row.FindControl("lbComment") as Label;
                        ImageButton imgTransfer = e.Row.FindControl("btTransfer2") as ImageButton;
                        ImageButton imgEndPoint = e.Row.FindControl("btEndPoint") as ImageButton;
                        Label cbCheck2Item = e.Row.FindControl("cbCheck2Item") as Label;
                        Label horaCheck = e.Row.FindControl("tbHoraCheck") as Label;
                        Label npassengers = e.Row.FindControl("tbNoPassengersCheck") as Label;
                        Label lb = e.Row.FindControl("lbTripHD") as Label;
                        CheckBox end_Point = e.Row.FindControl("lbEndPointCH2") as CheckBox;
                        if (string.IsNullOrEmpty(horaCheck.Text)) { horaCheck.Text = "--"; }
                        if (string.IsNullOrEmpty(npassengers.Text)) { npassengers.Text = "--"; }
                        if (string.IsNullOrEmpty(cbCheck2Item.Text)) { cbCheck2Item.Text = "--"; }
                        if (string.IsNullOrEmpty(comment.Text)) { comment.Text = "--"; }




                        if (horaCheck.Text == "--")
                        {
                            imgTransfer.Enabled = false;
                        }
                        else
                        {
                            e.Row.CssClass = "highlightRowGreen";

                        }
                        if (end_Point.Checked == true && !string.IsNullOrEmpty(tbTransbord.Text))
                        {
                            e.Row.Cells[9].Enabled = false;
                            
                            e.Row.CssClass = "highlightRowYellow";
                            imgTransfer.Enabled = false;

                            lbBus.Text = lbBus.Text + " - Transfered to :" + tbTransbord.Text;

                        }
                        if (end_Point.Checked == true && string.IsNullOrEmpty(tbTransbord.Text))
                        {
                            if (GenericClass.SQLSelectObj(new Trip_Detail(), WhereClause: "Where Trip_Hrd_ID=" + Int32.Parse(lb.Text) + " and Sequence in (2,3) and End_Point=1 ").Rows.Count == 1)
                            {
                                e.Row.Cells[9].Enabled = false;
                                e.Row.CssClass = "highlightRowOrange";
                                imgTransfer.Enabled = false;
                                imgEndPoint.Enabled = false;
                            }

                        }
                        if (validateTripIDSecuence2(Int32.Parse(lb.Text), 3))
                        { //bloquear row
                            e.Row.Enabled = false;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }



        }

        protected void btTransfer_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                tbCommentsTCH1.Text = string.Empty;
                Trip_Hrd trip = new Trip_Hrd();
                Bus_Driver bd = new Bus_Driver();
                ImageButton btimage = sender as ImageButton;
                string id = btimage.CommandArgument;
                hiddenValueCH1.Text = id;
                DataTable dtTrip = GenericClass.SQLSelectObj(trip, WhereClause: "WHERE Trip_Hrd_ID=" + id);
                bd.Bus_Driver_ID = dtTrip.Rows[0]["Bus_Driver_ID"].ToString();
                DataTable dtBusDriver = GenericClass.SQLSelectObj(bd, mappingQueryName: "DO-Transfer-LB", WhereClause: "WHERE [BUS_DRIVER].[Bus_Driver_ID]=" + "'" + bd.Bus_Driver_ID + "'");
                bd.Bus_ID = dtBusDriver.Rows[0]["Bus_ID"].ToString();
                string driverName = dtBusDriver.Rows[0]["Driver"].ToString();

                lbInfoBusDriver.Text = bd.Bus_ID + "-" + driverName;
                hiddenCurrentBusCH1.Text = bd.Bus_ID;

                Bus_Driver busDriver1 = new Bus_Driver();
                DataTable dtBusDriver1 = GenericClass.SQLSelectObj(busDriver1, mappingQueryName: "DO-Transfer-CB", WhereClause:
                    "Where BUS_DRIVER.Bus_Driver_ID not in (select Bus_Driver_ID from TRIP_HRD" +
                    " left join MASTER_TRIP on (MASTER_TRIP.Master_Trip_ID=TRIP_HRD.Master_Trip_ID)" +
                    " where MASTER_TRIP.Master_Trip_ID =" + "'" + masterTripIDHidden.Value + "'" + ")" +
                    " and BUS_DRIVER.Vendor_ID=" + "'" + cbVendorAdm.SelectedValue + "'" + " and BUS_DRIVER.Shift_ID=" + "'" + cbShift.SelectedValue + "'" + "and BUS_DRIVER.Is_Active=1" +
                    " and BUS.Vendor_ID+BUS.Bus_ID not in (select SUBSTRING(Bus_Driver_ID,0,CHARINDEX('-',Bus_Driver_ID)) from TRIP_HRD left join MASTER_TRIP" +
                    " on (MASTER_TRIP.Master_Trip_ID=TRIP_HRD.Master_Trip_ID) where MASTER_TRIP.Master_Trip_ID ='" + masterTripIDHidden.Value + "') " +
                    " and CAST(Driver.Driver_ID AS VARCHAR(MAX)) not in (select SUBSTRING(Bus_Driver_ID,CHARINDEX('-',Bus_Driver_ID)+1,LEN(Driver.Driver_ID)) from TRIP_HRD left join MASTER_TRIP" +
                    " on (MASTER_TRIP.Master_Trip_ID=TRIP_HRD.Master_Trip_ID) where MASTER_TRIP.Master_Trip_ID ='" + masterTripIDHidden.Value + "')");
                cbTransferCH1.Items.Clear();
                cbTransferCH1.DataSource = dtBusDriver1;
                cbTransferCH1.DataValueField = "Bus_Driver_ID";
                cbTransferCH1.DataTextField = "BusDriver";
                cbTransferCH1.DataBind();

                string language = functions.GetLanguage(usr);

                Trip_Change_Type reasonsObj = new Trip_Change_Type();
                DataTable dtReasons = GenericClass.SQLSelectObj(reasonsObj);
                cbReasons.Items.Clear();
                cbReasons.DataSource = dtReasons;
                cbReasons.DataValueField = "Change_Type";
                if (language == "Eng")
                {
                    cbReasons.DataTextField = "Type_Eng";
                }
                if (language == "Esp")
                {
                   cbReasons.DataTextField = "Change_Type";
                }
                
                cbReasons.DataBind();

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "openGenericDialog('#transferCheckPoint1' , 645 , 535 , 'Ventana de transbordo')", true);
                // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "setSelect()", true);

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void cbTransferCH1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btTransferCH1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    cbTransferCH1.SelectedValue.ToString();
                    Trip_Detail td = new Trip_Detail();
                    Trip_Change tc = new Trip_Change();
                    tc.Trip_Hrd_ID = Int32.Parse(hiddenValueCH1.Text);
                    td.Trip_Hrd_ID = Int32.Parse(hiddenValueCH1.Text);
                    td.Comment = tbCommentsTCH1.Text;

                    Dictionary<string, dynamic> dicUpdDtll = new Dictionary<string, dynamic>();
                    dicUpdDtll.Add("Trip_ID_Transbord", "'" + cbTransferCH1.SelectedValue.ToString() + "'");
                    dicUpdDtll.Add("Comment", "'" + td.Comment + "'");
                    GenericClass.SQLUpdateObj(td, dicUpdDtll, "Where Trip_Hrd_ID=" + td.Trip_Hrd_ID + " and Sequence=1");

                    Dictionary<string, dynamic> dicUpdCh = new Dictionary<string, dynamic>();
                    dicUpdCh.Add("Change_Type", "'" + cbReasons.SelectedValue.ToString() + "'");
                    GenericClass.SQLUpdateObj(tc, dicUpdCh, "Where Trip_Hrd_ID=" + tc.Trip_Hrd_ID + " and Prev_Bus=" + "'" + hiddenCurrentBusCH1.Text + "'");

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "genericCloseDialog('#transferCheckPoint1')", true);
                    functions.ShowMessage(this, this.GetType(), msg_DailyOperation_btTransferCH1, MessageType.Success);
                    fillAllGrids();
                    fillDropDownListAddNewTrip(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
                }
                catch (Exception ex)
                {
                    functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
                }
            }
        }

        protected void Grid_Checkpoint1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //DateTime CheckPnt_Time = new DateTime();
                //DateTime TripStart = new DateTime(); 
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                    {

                        ImageButton imgTransfer = e.Row.FindControl("btTransfer") as ImageButton;
                        ImageButton imgDelete = e.Row.FindControl("IBtnDelete") as ImageButton;
                        imgTransfer.Enabled = false;
                        imgDelete.Enabled = false;
                    }
                    else
                    {
                        TextBox tbTransbord = e.Row.FindControl("tbTransbordCH1") as TextBox;
                        Label lbTripHD = e.Row.FindControl("lbTripHD") as Label;
                        ImageButton imgTransfer = e.Row.FindControl("btTransfer") as ImageButton;
                        ImageButton IBtnDelete = e.Row.FindControl("IBtnDelete") as ImageButton;
                        TextBox horaCheck = e.Row.FindControl("tbHoraIni") as TextBox;
                        TextBox noPassengers = e.Row.FindControl("tbNoPassengers") as TextBox;
                        CheckBox end_Point = e.Row.FindControl("lbEndPoint") as CheckBox;
                        Label lbBusDriver = e.Row.FindControl("lbBusDriver") as Label;
                        Label lbBusDriverID = e.Row.FindControl("lbBusDriverID") as Label;

                        Label lbBusID = e.Row.FindControl("lbBus") as Label;
                        Label lbDriverID = e.Row.FindControl("lbDriverID") as Label;
                        Image imgAlertBUs = e.Row.FindControl("imgAlertBus") as Image;
                        Image imgAlertDriver = e.Row.FindControl("imgAlertDriver") as Image;

                        // int BusID = Convert.ToInt32(lbBusID.Text);

                        int DriverID = Convert.ToInt32(lbDriverID.Text == "NA" ? "0" : lbDriverID.Text);

                        if (validateBusIsInAlertLog(lbBusID.Text))
                        {
                            imgAlertBUs.Visible = true;
                            e.Row.CssClass = "highlightRowBlue";
                        }

                        if (validateDriverIsInAlertLog(DriverID))
                        {
                            imgAlertDriver.Visible = true;
                            e.Row.CssClass = "highlightRowBlue";
                        }

                        if (validateTripIDSecuence2(Int32.Parse(lbTripHD.Text), 2))
                        { //bloquear row
                            e.Row.Enabled = false;
                        }

                        DriverInAlertLog(DriverID, e);

                        if (string.IsNullOrEmpty(horaCheck.Text) || DriverID == 0)
                        {
                            imgTransfer.Enabled = false;
                        }
                        else
                        {

                            if (Int32.Parse(noPassengers.Text) == 0 && !string.IsNullOrEmpty(horaCheck.Text))
                            {
                                IBtnDelete.Enabled = true;

                                imgTransfer.Enabled = false;
                            }
                            else
                            {
                                IBtnDelete.Enabled = false;
                            }

                            e.Row.CssClass = "highlightRowGreen";

                            //Add Tooltip if bus started with 10 minutes of difference 
                            if (DriverID != 0)
                            {
                                AddTooltipTime(e, lbTripHD.Text, horaCheck);
                            }
                        }

                        if (end_Point.Checked == true && !string.IsNullOrEmpty(tbTransbord.Text))
                        {
                            e.Row.Cells[15].Enabled = false;

                            e.Row.CssClass = "highlightRowYellow";

                            imgTransfer.Enabled = false;

                            IBtnDelete.Enabled = false;

                            lbBusDriver.Text = lbBusDriver.Text + " - Transbordado a :" + tbTransbord.Text;

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        /// <summary>
        /// Check if a bus has started with more than 9 minutes of difference
        /// </summary>
        /// <param name="e">Row being edited</param>
        protected void AddTooltipTime(GridViewRowEventArgs e, string TripID, TextBox horaCheck)
        {
            int rowIndex = e.Row.RowIndex;
            DateTime tripDate;
            DataTable dtTime = Grid_Checkpoint1.DataSource as DataTable;
            DataView dvTime = Grid_Checkpoint1.DataSource as DataView;

            if (dtTime != null)
            {
                DataRow[] foundRows = dtTime.Select("Trip_Hrd_ID=" + TripID);
                tripDate = Convert.ToDateTime(foundRows[0]["Trip_Date"].ToString());

                string formatTripDate = tripDate.ToString("MM/dd/yyyy") + " " + foundRows[0]["Check_Point_Time"].ToString();

                DateTime currentlDate = DateTime.Parse(formatTripDate);

                DateTime currentTime = DateTime.Parse(foundRows[0]["Trip_Start"].ToString());

                TimeSpan difTime = currentlDate.Subtract(currentTime);

                string formatDiftime = difTime.ToString("c");

                double Hour = difTime.Hours;

                double Min = difTime.Minutes;

                string toolTipText = null;


                TextBox tbHour = e.Row.FindControl("tbHoraIni") as TextBox;

                if (formatDiftime.Contains('-'))
                {


                    int ConvertToPositive = -1;

                    Hour = Hour * ConvertToPositive;

                    Min = Min * ConvertToPositive;

                    if (Hour > 0 || Min > 9)
                    {
                        toolTipText = Hour.ToString() + msg_DailyOperation_Tooltip1 + Min.ToString() + msg_DailyOperation_Tooltip2;


                        horaCheck.BackColor = System.Drawing.Color.PeachPuff;
                        //   e.Row.Cells[13].CssClass = "CellColorStartdif";
                    }


                }
                else if (Hour > 0 || Min > 9)
                {
                    toolTipText = Hour.ToString() + msg_DailyOperation_Tooltip1 + Min.ToString() + msg_DailyOperation_Tooltip3;
                    horaCheck.BackColor = System.Drawing.Color.PeachPuff;
                   // e.Row.Cells[13].CssClass = "CellColorStartdif";
                }

                tbHour.ToolTip = toolTipText;
            }
        }
        /// <summary>
        /// Determine if the driver exists in any of the 3 cases and apply css style
        /// </summary>
        /// <param name="driver">Driver Id to compare</param>
        /// <param name="e">Row to apply css style</param>
        protected void DriverInAlertLog(int driver, GridViewRowEventArgs e)
        {

            if (alertLog10trips != null)
            {
                bool containsDriver = alertLog10trips.AsEnumerable().Any(row => driver == row.Field<int>("Driver_ID"));
                if (containsDriver)
                {
                    e.Row.CssClass = "highlightRowRed0";
                }
            }
            if (alertLog20trips != null)
            {
                bool containsDriver = alertLog20trips.AsEnumerable().Any(row => driver == row.Field<int>("Driver_ID"));
                if (containsDriver)
                {
                    e.Row.CssClass = "highlightRowRed1";
                }
            }
            if (alertLog30trips != null)
            {
                bool containsDriver = alertLog30trips.AsEnumerable().Any(row => driver == row.Field<int>("Driver_ID"));
                if (containsDriver)
                {
                    e.Row.CssClass = "highlightRowRed2";
                }
            }

        }

        /// <summary>
        /// Select Alert log deppending on Vendor and Code
        /// </summary>
        /// <param name="vendorID">Vendor ID</param>
        /// <param name="code">Code from Alert table</param>
        /// <returns></returns>
        protected DataTable NumbreOfTrips(string vendorID, string code)
        {
            DataTable dtListNTrips = new DataTable();
            try
            {
                dtListNTrips = GenericClass.SQLSelectObj(new Alert_Log(),
                        WhereClause: "WHERE Code ='" + code + "'"
                        + "AND Vendor_ID='" + vendorID + "'"
                        + "AND Status='OPEN'");
            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            return dtListNTrips;
        }

        ///<summary>
        ///This method is to know if BusID is in Alert_Log Table 
        ///</summary>
        private bool validateBusIsInAlertLog(string BusID)
        {
            bool result = false;
            Alert_Log alert = new Alert_Log();
            try
            {
                DataTable getAlertInfo = GenericClass.SQLSelectObj(alert, WhereClause: "Where Bus_ID=" + "'" + BusID + "'" + " AND Status in ('OPEN' , 'WORKING') AND Code Not in ('Dr2','Dr3','Dr4')");
                if (getAlertInfo.Rows.Count > 0)
                { result = true; }
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

            return result;
        }

        ///<summary>
        ///This method is to know if DriverID is in Alert_Log Table 
        ///</summary>
        private bool validateDriverIsInAlertLog(int DriverID)
        {
            bool result = false;
            Alert_Log alert = new Alert_Log();
            try
            {
                DataTable getAlertInfo = GenericClass.SQLSelectObj(alert, WhereClause: "Where Driver_ID=" + DriverID + " AND Status in ('OPEN' , 'WORKING') AND Code Not in ('Dr2','Dr3','Dr4')");
                if (getAlertInfo.Rows.Count > 0)
                { result = true; }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

            return result;
        }

        protected void btTransferCH2_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    tbCommentsTCH2.Text = string.Empty;
                    cbTransferCH1.SelectedValue.ToString();
                    Trip_Detail td = new Trip_Detail();
                    Trip_Change tc = new Trip_Change();
                    td.Trip_Hrd_ID = Int32.Parse(hiddenValueCH2.Text);
                    td.Comment = tbCommentsTCH2.Text;

                    Dictionary<string, dynamic> dicUpdDtll = new Dictionary<string, dynamic>();
                    dicUpdDtll.Add("Trip_ID_Transbord", "'" + cbTransferCH2.SelectedValue.ToString() + "'");
                    dicUpdDtll.Add("Comment", "'" + td.Comment + "'");
                    GenericClass.SQLUpdateObj(td, dicUpdDtll, "Where Trip_Hrd_ID=" + td.Trip_Hrd_ID + " and Sequence=2");

                    Dictionary<string, dynamic> dicUpdCh = new Dictionary<string, dynamic>();
                    dicUpdCh.Add("Change_Type", "'" + cbReasons2.SelectedValue.ToString() + "'");
                    GenericClass.SQLUpdateObj(tc, dicUpdCh, "Where Trip_Hrd_ID=" + td.Trip_Hrd_ID + " and Prev_Bus=" + "'" + hiddenCurrentBusCH2.Text + "'");

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "genericCloseDialog('#transferCheckPoint2')", true);
                    functions.ShowMessage(this, this.GetType(), "Updated template successfully", MessageType.Success);
                    fillAllGrids();
                }
                catch (Exception ex)
                {
                    functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
                }
            }
        }
        protected void btEndPoint_Click(object sender, ImageClickEventArgs e)
        {

            try
            {

                Trip_Detail dtTripDetail = new Trip_Detail();
                ImageButton btimage = sender as ImageButton;
                string id = btimage.CommandArgument;

                if (GenericClass.SQLSelectObj(dtTripDetail, WhereClause: "Where Trip_Hrd_ID=" + id + " and Sequence=2 and Psg_Init=0").Rows.Count > 0)
                {
                    Dictionary<string, dynamic> dicTripDetail = new Dictionary<string, dynamic>();
                    dicTripDetail.Add("End_Point", true);
                    dtTripDetail.End_Point = true;
                    DataTable updDetail = GenericClass.SQLUpdateObj(dtTripDetail, dicTripDetail, "Where Trip_Hrd_ID=" + id);
                    DataTable delDetail = GenericClass.SQLDeleteObj(dtTripDetail, "Where Trip_Hrd_ID=" + id + " and Sequence=3 and Trip_Start IS NULL");
                    functions.ShowMessage(this, this.GetType(), msg_DailyOperation_btEndPoint, MessageType.Warning);
                    fillAllGrids();
                }
                else
                {
                    functions.ShowMessage(this, this.GetType(), msg_DailyOperation_btEndPoint2, MessageType.Error);
                }


            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btTransfer2_Click(object sender, ImageClickEventArgs e)
        {

            try
            {
                tbCommentsTCH2.Text = string.Empty;
                Trip_Hrd trip = new Trip_Hrd();
                Bus_Driver bd = new Bus_Driver();
                ImageButton btimage = sender as ImageButton;
                string id = btimage.CommandArgument;
                hiddenValueCH2.Text = id;
                DataTable dtTrip = GenericClass.SQLSelectObj(trip, WhereClause: "WHERE Trip_Hrd_ID=" + id);
                bd.Bus_Driver_ID = dtTrip.Rows[0]["Bus_Driver_ID"].ToString();
                DataTable dtBusDriver = GenericClass.SQLSelectObj(bd, mappingQueryName: "DO-Transfer-LB", WhereClause: "WHERE [BUS_DRIVER].[Bus_Driver_ID]=" + "'" + bd.Bus_Driver_ID + "'");
                bd.Bus_ID = dtBusDriver.Rows[0]["Bus_ID"].ToString();
                string driverName = dtBusDriver.Rows[0]["Driver"].ToString();

                hiddenCurrentBusCH2.Text = bd.Bus_ID;
                lbInfoBusDriver2.Text = bd.Bus_ID + "-" + driverName;

                Bus_Driver busDriver2 = new Bus_Driver();
                DataTable dtBusDriver2 = GenericClass.SQLSelectObj(busDriver2, mappingQueryName: "DO-Transfer-CB", WhereClause:
                "WHERE [BUS_DRIVER].[Shift_ID]=" + "'" + cbShift.SelectedValue.ToString() + "'" + " AND [BUS_DRIVER].[Vendor_ID]=" + "'" + cbVendorAdm.SelectedValue.ToString() + "'" 
                + " AND [BUS_DRIVER].[Is_Active]=1 AND [BUS_DRIVER].[Bus_Driver_ID]!=" + "'" + bd.Bus_Driver_ID + "'"
                +"AND [BUS_DRIVER].[Bus_Driver_ID] not in (select Bus_Driver_ID from TRIP_HRD Where Master_Trip_ID ='"+ masterTripIDHidden.Value
                +"' and Trip_Hrd_ID in (select Trip_Hrd_ID from TRIP_DETAIL"
                +" where Trip_ID_Transbord is not null"
                +" and Trip_Hrd_ID in (select Trip_Hrd_ID from TRIP_HRD "
                + " Where Master_Trip_ID ='" + masterTripIDHidden.Value + "')))");

                cbTransferCH2.Items.Clear();
                cbTransferCH2.DataSource = dtBusDriver2;
                cbTransferCH2.DataValueField = "Bus_Driver_ID";
                cbTransferCH2.DataTextField = "BusDriver";
                cbTransferCH2.DataBind();

                Trip_Change_Type reasonsObj = new Trip_Change_Type();
                DataTable dtReasons = GenericClass.SQLSelectObj(reasonsObj);
                cbReasons2.Items.Clear();
                cbReasons2.DataSource = dtReasons;
                cbReasons2.DataValueField = "Change_Type";
                cbReasons2.DataTextField = "Change_Type";
                cbReasons2.DataBind();

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "openGenericDialog('#transferCheckPoint2' , 645 , 535 , 'Ventana de transbordo')", true);
                // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "setSelect()", true);
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void btCancelCH1_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "genericCloseDialog('#transferCheckPoint1')", true);
        }

        protected void btCancelCH2_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "genericCloseDialog('#transferCheckPoint2')", true);
        }

        protected void Grid_CheckPoint3_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                Grid_CheckPoint3.EditIndex = e.NewEditIndex;
                
                Label lbstopPoint = (Label)Grid_CheckPoint3.Rows[Grid_CheckPoint3.EditIndex].FindControl("cbCheck3Item") as Label;


                ViewState["checkpoint1EditingCH3"] = true;
                DataTable getDataSorting = null;
                DataTable getDataSearch = (DataTable)ViewState["searchResultsCH3"];
                Grid_CheckPoint3.EditIndex = e.NewEditIndex;

                if ((bool)ViewState["isSortingCH3"] == true)
                    getDataSorting = ((DataView)Session["ObjectsCH3"]).ToTable();

                if ((bool)ViewState["searchStatusCH3"] == true && getDataSearch.Rows.Count > 0 && (bool)ViewState["isSortingCH3"] == false)
                {
                    Grid_CheckPoint3.DataSource = getDataSearch;
                    Grid_CheckPoint3.DataBind();
                }
                if ((bool)ViewState["isSortingCH3"] == true && getDataSorting.Rows.Count > 0 && (bool)ViewState["searchStatusCH3"] == false)
                {
                    Grid_CheckPoint3.DataSource = getDataSorting;
                    Grid_CheckPoint3.DataBind();
                }
                if ((bool)ViewState["isSortingCH3"] == false && (bool)ViewState["searchStatusCH3"] == false)
                {
                    fillGridCheckpoint3();
                }
                TextBox tbHoraChk3 = (TextBox)Grid_CheckPoint3.Rows[Grid_CheckPoint3.EditIndex].FindControl("tbHoraCheckEdit3") as TextBox;

                tbHoraChk3.Focus();

                DropDownList cbCheck3Edit = (DropDownList)Grid_CheckPoint3.Rows[Grid_CheckPoint3.EditIndex].FindControl("cbCheck3Edit") as DropDownList;

                if (cbCheck3Edit.Items.Count > 0)
                {
                    if (lbstopPoint.Text != "--" || !string.IsNullOrEmpty(lbstopPoint.Text))
                    {
                        cbCheck3Edit.SelectedValue = lbstopPoint.Text.Trim();
                    }
                }

                foreach (GridViewRow row in Grid_CheckPoint3.Rows)
                {
                    if (row.RowIndex != e.NewEditIndex)
                    {
                        row.Enabled = false;
                    }
                }
                Grid_CheckPoint3.BottomPagerRow.Visible = false;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            finally
            {
                Grid_CheckPoint3.EditIndex = -1;
                tbSearchCH3.Text = string.Empty;

            }
        }

        protected void Grid_CheckPoint3_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                validateSession();
                Label lbID3 = (Label)Grid_CheckPoint3.Rows[e.RowIndex].FindControl("lbTripHD3") as Label;
                bool timeError = false;
                TextBox comment3 = (TextBox)Grid_CheckPoint3.Rows[e.RowIndex].FindControl("lbCommentEdit3") as TextBox;
                TextBox tbHoraCheck3 = (TextBox)Grid_CheckPoint3.Rows[e.RowIndex].FindControl("tbHoraCheckEdit3") as TextBox;
                TextBox tbNoPassengersCheck3 = (TextBox)Grid_CheckPoint3.Rows[e.RowIndex].FindControl("tbNoPassengersCheckEdit3") as TextBox;
                DropDownList cbCheck3Edit = (DropDownList)Grid_CheckPoint3.Rows[e.RowIndex].FindControl("cbCheck3Edit") as DropDownList;

                DateTime dtime = new DateTime();
                DateTime.TryParse(tbHoraCheck3.Text, out dtime);
                TimeSpan ts = new TimeSpan(dtime.Hour, dtime.Minute, dtime.Second);

                Trip_Detail tripDetail = new Trip_Detail();
                tripDetail.Trip_Start = ts;
                tripDetail.Psg_Init = Int32.Parse(tbNoPassengersCheck3.Text.Trim());
                tripDetail.Trip_Hrd_ID = Int32.Parse(lbID3.Text);
                tripDetail.Stop_point_ID = cbCheck3Edit.SelectedValue.ToString();
                tripDetail.Comment = comment3.Text;


                DataTable getTimeCH2 = GenericClass.SQLSelectObj(new Trip_Detail(), WhereClause: "Where Trip_Hrd_ID=" + tripDetail.Trip_Hrd_ID + " and Sequence=2");
                if (getTimeCH2.Rows.Count > 0)
                {
                    DateTime timeCH = new DateTime();
                    DateTime.TryParse(getTimeCH2.Rows[0]["Trip_Start"].ToString(), out timeCH);
                    TimeSpan tsCH = new TimeSpan(timeCH.Hour, timeCH.Minute, timeCH.Second);
                    if (ts <= tsCH)
                    {
                        timeError = true;
                    }
                }
                if (timeError == true)
                {
                    functions.ShowMessage(this, this.GetType(), msg_DailyOperation_CH3RowUpdating, MessageType.Warning);
                }
                else
                {

                    Dictionary<string, dynamic> dicUpdDtll = new Dictionary<string, dynamic>();
                    dicUpdDtll.Add("Trip_Start", "'" + tripDetail.Trip_Start + "'");
                    dicUpdDtll.Add("Psg_Init", "'" + tripDetail.Psg_Init + "'");
                    dicUpdDtll.Add("Trip_Hrd_ID", "'" + tripDetail.Trip_Hrd_ID + "'");
                    dicUpdDtll.Add("Stop_point_ID", "'" + tripDetail.Stop_point_ID + "'");
                    dicUpdDtll.Add("Comment", "'" + tripDetail.Comment + "'");

                    GenericClass.SQLUpdateObj(tripDetail, dicUpdDtll, "Where Trip_Hrd_ID=" + tripDetail.Trip_Hrd_ID + " and Sequence=3");

                    Dictionary<string, dynamic> dicUpdTripAllDetll = new Dictionary<string, dynamic>();
                    dicUpdTripAllDetll.Add("End_Point", true);
                    GenericClass.SQLUpdateObj(tripDetail, dicUpdTripAllDetll, "Where Trip_Hrd_ID=" + tripDetail.Trip_Hrd_ID);

                    functions.ShowMessage(this, this.GetType(), msg_DailyOperation_CH3RowUpdating2, MessageType.Success);
                    Grid_CheckPoint3.EditIndex = -1;
                    fillAllGrids();
                    ViewState["checkpoint1EditingCH3"] = false;
                    ViewState["searchStatusCH3"] = false;
                    ViewState["isSortingCH3"] = false;
                }

                Grid_CheckPoint3.BottomPagerRow.Visible = true;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void Grid_CheckPoint3_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Grid_CheckPoint3.EditIndex = -1;
            tbSearchCH3.Text = string.Empty;
            ViewState["checkpoint1EditingCH3"] = false;
            DataTable getDataSorting = null;
            DataTable getDataSearch = (DataTable)ViewState["searchResultsCH3"];

            if ((bool)ViewState["isSortingCH3"] == true)
                getDataSorting = ((DataView)Session["ObjectsCH3"]).ToTable();

            if ((bool)ViewState["searchStatusCH3"] == true && getDataSearch.Rows.Count > 0 && (bool)ViewState["isSortingCH3"] == false)
            {
                Grid_CheckPoint3.DataSource = getDataSearch;
                Grid_CheckPoint3.DataBind();
            }
            if ((bool)ViewState["isSortingCH3"] == true && getDataSorting.Rows.Count > 0 && (bool)ViewState["searchStatusCH3"] == false)
            {
                Grid_CheckPoint3.DataSource = getDataSorting;
                Grid_CheckPoint3.DataBind();
            }
            if ((bool)ViewState["isSortingCH3"] == false && (bool)ViewState["searchStatusCH3"] == false)
            {
                fillGridCheckpoint3();
            }
            Grid_CheckPoint3.BottomPagerRow.Visible = true;
        }

        protected void Grid_CheckPoint3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                    {
                        DropDownList cbcCheck3 = e.Row.FindControl("cbCheck3Edit") as DropDownList;
                        fillCombosCheckpoint2(cbcCheck3, "Checkpoint3");
                    }
                    else
                    {
                        TextBox tbTransbord = e.Row.FindControl("tbTransbordCH3") as TextBox;
                        Label cbCheck3Item = e.Row.FindControl("cbCheck3Item") as Label;
                        Label horaCheck3 = e.Row.FindControl("tbHoraCheck3") as Label;
                        Label npassengers3 = e.Row.FindControl("tbNoPassengersCheck3") as Label;
                        Label comment3 = e.Row.FindControl("lbComment3") as Label;
                        CheckBox end_Point3 = e.Row.FindControl("lbEndPoint3") as CheckBox;
                        if (string.IsNullOrEmpty(horaCheck3.Text)) { horaCheck3.Text = "--"; }
                        if (string.IsNullOrEmpty(npassengers3.Text)) { npassengers3.Text = "--"; }
                        if (string.IsNullOrEmpty(cbCheck3Item.Text)) { cbCheck3Item.Text = "--"; }
                        if (string.IsNullOrEmpty(cbCheck3Item.Text)) { comment3.Text = "--"; }

                        if (horaCheck3.Text != "--")
                        {
                            e.Row.CssClass = "highlightRowGreen";
                        }

                        if (end_Point3.Checked == true && !string.IsNullOrEmpty(tbTransbord.Text))
                        {
                            e.Row.Cells[8].Enabled = false;
                            e.Row.CssClass = "highlightRowYellow";
                            


                        }
                    }

                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }



        private bool validateTripIDSecuence2(int tripID, int sequence)
        {
            Trip_Detail obj = new Trip_Detail();
            DataTable dtCheck = GenericClass.SQLSelectObj(obj, WhereClause:
               "WHERE Trip_Hrd_ID=" + "'" + tripID + "'" + " AND Sequence=" + "'" + sequence + "'" + "  AND Trip_Start is not null AND Psg_Init is not null");
            if (dtCheck.Rows.Count > 0)
            { return true; }
            else
            { return false; }

        }

        protected void btSearchCH1_Click(object sender, EventArgs e)
        {

        }

        protected void btConfirmOperation_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable masterTripEmpty = GenericClass.SQLSelectObj(new Trip_Hrd(), WhereClause: "Where Master_Trip_ID ='" + getMasterTrip() + "'");
                if (masterTripEmpty.Rows.Count > 0)
                {

                    DataTable tripDetailEmpty = GenericClass.SQLSelectObj(new Trip_Detail(),
                        WhereClause: "  Where Trip_Hrd_ID in (select Trip_Hrd_ID from TRIP_HRD where Master_Trip_ID ='" + getMasterTrip() + "')"
                        + "And Trip_Start is null and Sequence = 1");

                    if (tripDetailEmpty.Rows.Count < 1)
                    {

                        Master_Trip mtrip = new Master_Trip();
                        mtrip.Master_Trip_ID = masterTripIDHidden.Value;

                        //Validate if Master_trip status is already  PENDINGTOAPPROVE or PENDINGTOCANCEL
                        DataTable getStatus = GenericClass.GetCustomData("Select Status from Master_Trip where Master_Trip_ID=" + "'" + mtrip.Master_Trip_ID + "'");
                        string status = getStatus.Rows[0]["Status"].ToString();

                        if (status != "PENDINGTOAPPROVE" && status != "PENDINGTOCANCEL")
                        {
                            Admin_Approve newApprove = new Admin_Approve();
                            if (!string.IsNullOrEmpty(mtrip.Master_Trip_ID))
                            {
                                if (usr.Rol_ID.Contains("ADMIN"))
                                {
                                    Dictionary<string, dynamic> dicUpdmTrip = new Dictionary<string, dynamic>();
                                    dicUpdmTrip.Add("Status", "'" + "CLOSED");
                                    dicUpdmTrip.Add("Admin_Close_Trip", true);
                                    GenericClass.SQLUpdateObj(mtrip, dicUpdmTrip, "Where Master_Trip_ID=" + "'" + mtrip.Master_Trip_ID + "'");
                                    functions.ShowMessage(this, this.GetType(), msg_DailyOperation_btConfirmOperation, MessageType.Success);
                                    cleanAlldata();
                                }
                                else
                                {
                                    newApprove.Activity_ID = 0;
                                    newApprove.Admin_Confirm = false;
                                    newApprove.Activity_Date = DateTime.Now;
                                    newApprove.User_ID = usr.User_ID;
                                    newApprove.Type = "UPDATE";
                                    newApprove.Module = mtrip.GetType().Name;
                                    newApprove.New_Values = "Status=" + "'" + "CLOSED" + "'" + ", Admin_Close_Trip=1";
                                    newApprove.Where_Clause = "Where Master_Trip_ID=" + "'" + mtrip.Master_Trip_ID + "'";
                                    newApprove.Rollback_action = "UPDATE";
                                    newApprove.Rollback_value = "Update MASTER_TRIP set Status=" + "'" + "OPEN" + "' WHERE Master_Trip_ID=" + "'" + mtrip.Master_Trip_ID + "'";
                                    newApprove.Comments = msg_DailyOperation_btConfirmOperation2 + " " + cbVendorAdm.SelectedItem.Text + ", " + msg_DailyOperation_shift + " " + cbShift.SelectedItem.Text;
                                    GenericClass.SQLInsertObj(newApprove);

                                    //Update master trip status to pending when this has been inserted in Admin Approve module
                                    Dictionary<string, dynamic> dicUpdmTrip = new Dictionary<string, dynamic>();
                                    dicUpdmTrip.Add("Status", "'" + "PENDINGAPPROVE");
                                    GenericClass.SQLUpdateObj(mtrip, dicUpdmTrip, "Where Master_Trip_ID=" + "'" + mtrip.Master_Trip_ID + "'");

                                    functions.ShowMessage(this, this.GetType(), msg_DailyOperation_btConfirmOperationOk, MessageType.Warning);
                                    cleanAlldata();
                                    SendEmail.sendEmailToAdmin(usr.User_ID.ToString(), newApprove);
                                    SendEmail.sendEmailToDistributionList(cbVendorAdm.SelectedValue.ToString(), "Daily_Operation_Email", newApprove, usr.User_ID.ToString());
                                    functions.ShowMessage(this, this.GetType(), msg_DailyOperation_btConfirmOperationEmail, MessageType.Success);
                                }
                            }
                            else
                            {
                                functions.ShowMessage(this, this.GetType(), msg_DailyOperation_reload, MessageType.Error);
                            }
                        }
                        else
                        {
                            functions.ShowMessage(this, this.GetType(), msg_DailyOperation_btConfirmOperation5, MessageType.Error);
                        }
                    }
                    else
                    {
                        functions.ShowMessage(this, this.GetType(), msg_DailyOperation_emptyDetail, MessageType.Error);

                    }

                }
                else
                {
                    functions.ShowMessage(this, this.GetType(), msg_DailyOperation_needCancel, MessageType.Error);
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }


        }

        private void cleanAlldata()
        {
            Grid_Checkpoint1.DataSource = null;
            Grid_Checkpoint1.DataBind();
            Grid_CheckPoint2.DataSource = null;
            Grid_CheckPoint2.DataBind();
            Grid_CheckPoint3.DataSource = null;
            Grid_CheckPoint3.DataBind();
            btLoadData.Enabled = true;
            cbVendorAdm.Enabled = true;
            cbShiftType.Enabled = true;
            cbShift.Enabled = false;
            btChange.Enabled = false;
            btLoadData.Enabled = false;
            btInactiveDetail.Enabled = false;
            btRevisionDetail.Enabled = false;
            btAlertDetail.Enabled = false;
            cbShift.Items.Clear();
            cbShiftType.SelectedIndex = 0;
            inactiveCount.Text = "0";
            revisionCount.Text = "0";
            AlertCount.Text = "0";
            btConfirmOperation.Enabled = false;
            btCancelOperation.Enabled = false;
            btSearchCH1.Enabled = false;
            btSearchCH2.Enabled = false;
            btSearchCH3.Enabled = false;
            lbDate.Text = string.Empty;
            lbVendorInfo.Text = string.Empty;
            lbShiftInfo.Text = string.Empty;
            ViewState["searchStatus"] = false;
            ViewState["isSorting"] = false;
            ViewState["checkpoint1Editing"] = false;
            ViewState["searchStatusCH2"] = false;
            ViewState["isSortingCH2"] = false;
            ViewState["checkpoint1EditingCH2"] = false;
            ViewState["searchStatusCH3"] = false;
            ViewState["isSortingCH3"] = false;
            ViewState["checkpoint1EditingCH3"] = false;
        }

        protected void btCancelOperation_Click(object sender, EventArgs e)
        {
            try
            {
                Admin_Approve newApprove = new Admin_Approve();
                Master_Trip mtrip = new Master_Trip();
                mtrip.Master_Trip_ID = masterTripIDHidden.Value;

                if (!string.IsNullOrEmpty(mtrip.Master_Trip_ID))
                {
                    if (usr.Rol_ID.Contains("ADMIN"))
                    {
                        Dictionary<string, dynamic> dicUpdmTrip = new Dictionary<string, dynamic>();
                        dicUpdmTrip.Add("Status", "'" + "CANCEL");
                        GenericClass.SQLUpdateObj(mtrip, dicUpdmTrip, "Where Master_Trip_ID=" + "'" + mtrip.Master_Trip_ID + "'");
                        cleanAlldata();
                        
                 
                        functions.ShowMessage(this, this.GetType(), msg_DailyOperation_btCancelOperation, MessageType.Success);
                        HtmlMeta meta = new HtmlMeta();
                        meta.HttpEquiv = "Refresh";
                        meta.Content = "5;url=Daily_Operation.aspx";
                        this.Page.Controls.Add(meta);
                    }
                    else
                    {
                        // Insert this activity in admin approve module when the user is not admin
                        newApprove.Activity_ID = 0;
                        newApprove.Admin_Confirm = false;
                        newApprove.Activity_Date = DateTime.Now;
                        newApprove.User_ID = usr.User_ID;
                        newApprove.Type = "UPDATE";
                        newApprove.Module = mtrip.GetType().Name;
                        newApprove.New_Values = "Status=" + "'" + "CANCEL" + "'";
                        newApprove.Where_Clause = "Where Master_Trip_ID=" + "'" + mtrip.Master_Trip_ID + "'";
                        newApprove.Rollback_action = "UPDATE";
                        newApprove.Rollback_value = "Update MASTER_TRIP set Status=" + "'" + "OPEN" + "' WHERE Master_Trip_ID=" + "'" + mtrip.Master_Trip_ID + "'";
                        newApprove.Comments = msg_DailyOperation_btCancelOperation2 + " " + cbVendorAdm.SelectedItem.Text + "," + msg_DailyOperation_shift + " " + cbShift.SelectedItem.Text;
                        GenericClass.SQLInsertObj(newApprove);
                        SendEmail.sendEmailToAdmin(usr.User_ID.ToString(), newApprove);

                        //Update master trip status to pending when this has been inserted in Admin Approve module
                        Dictionary<string, dynamic> dicUpdmTrip = new Dictionary<string, dynamic>();
                        dicUpdmTrip.Add("Status", "'" + "PENDINGCANCEL");
                        GenericClass.SQLUpdateObj(mtrip, dicUpdmTrip, "Where Master_Trip_ID=" + "'" + mtrip.Master_Trip_ID + "'");
                        cleanAlldata();
                        functions.ShowMessage(this, this.GetType(), msg_DailyOperation_btCancelOperation3, MessageType.Warning);
                        Response.Redirect(Request.RawUrl);
                    }
                }
                else
                {
                    functions.ShowMessage(this, this.GetType(), msg_DailyOperation_reload, MessageType.Error);
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btSearchCH1_Click1(object sender, EventArgs e)
        {
            DataTable dtSearch = new DataTable();
            try
            {
                Grid_Checkpoint1.EditIndex = -1;
                string searchTerm = tbSearchCH1.Text.ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    fillGridCheckpoint1(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue.ToString());
                    dtSearch.Clear();
                    ViewState["searchStatus"] = false;
                    ViewState["isSorting"] = false;
                }
                else
                {
                    ViewState["searchStatus"] = true;
                    ViewState["isSorting"] = false;
                    // search = true;
                    //always check if the viewstate exists before using it
                    if (ViewState["myViewState"] == null)
                        return;

                    //cast the viewstate as a datatable
                    DataTable dt = ViewState["myViewState"] as DataTable;

                    //make a clone of the datatable
                    dtSearch = dt.Clone();

                    //search the datatable for the correct fields
                    foreach (DataRow row in dt.Rows)
                    {
                        //add your own columns to be searched here
                        if (row["Bus_ID"].ToString().ToLower().Contains(searchTerm) || row["Driver"].ToString().ToLower().Contains(searchTerm) || row["Route"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtSearch.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    Grid_Checkpoint1.DataSource = dtSearch;
                    ViewState["searchResults"] = dtSearch;
                    Grid_Checkpoint1.DataBind();
                    //fillDropDownListAddNewTrip(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void btSearchCH2_Click(object sender, EventArgs e)
        {
            DataTable dtSearch = new DataTable();
            try
            {
                Grid_CheckPoint2.EditIndex = -1;
                string searchTerm = tbSearchCH2.Text.ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    fillGridCheckpoint2();
                    dtSearch.Clear();
                    ViewState["searchStatusCH2"] = false;
                    ViewState["isSortingCH2"] = false;
                }
                else
                {
                    ViewState["searchStatusCH2"] = true;
                    ViewState["isSortingCH2"] = false;
                    // search = true;
                    //always check if the viewstate exists before using it
                    if (ViewState["myViewState2"] == null)
                        return;

                    //cast the viewstate as a datatable
                    DataTable dt = ViewState["myViewState2"] as DataTable;

                    //make a clone of the datatable
                    dtSearch = dt.Clone();

                    //search the datatable for the correct fields
                    foreach (DataRow row in dt.Rows)
                    {
                        //add your own columns to be searched here
                        if (row["Bus_ID"].ToString().ToLower().Contains(searchTerm) || row["Name"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtSearch.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    Grid_CheckPoint2.DataSource = dtSearch;
                    ViewState["searchResultsCH2"] = dtSearch;
                    Grid_CheckPoint2.DataBind();
                    //  fillCombosCheckpoint2();
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btSearchCH3_Click(object sender, EventArgs e)
        {
            DataTable dtSearch = new DataTable();
            try
            {
                Grid_CheckPoint3.EditIndex = -1;
                string searchTerm = tbSearchCH3.Text.ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    fillGridCheckpoint3();
                    dtSearch.Clear();
                    ViewState["searchStatusCH3"] = false;
                    ViewState["isSortingCH3"] = false;
                }
                else
                {
                    ViewState["searchStatusCH3"] = true;
                    ViewState["isSortingCH3"] = false;
                    //   search = true;
                    //always check if the viewstate exists before using it
                    if (ViewState["myViewState3"] == null)
                        return;

                    //cast the viewstate as a datatable
                    DataTable dt = ViewState["myViewState3"] as DataTable;

                    //make a clone of the datatable
                    dtSearch = dt.Clone();

                    //search the datatable for the correct fields
                    foreach (DataRow row in dt.Rows)
                    {
                        //add your own columns to be searched here
                        if (row["Bus_ID"].ToString().ToLower().Contains(searchTerm) || row["Name"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtSearch.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    Grid_CheckPoint3.DataSource = dtSearch;
                    ViewState["searchResultsCH3"] = dtSearch;
                    Grid_CheckPoint3.DataBind();
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btChange_Click(object sender, EventArgs e)
        {
            cleanAlldata();
            btExcel.Enabled = false;
            disableOrEnableAddNewTripElements(false);
        }

        protected void cbShiftType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbShiftType.SelectedValue.ToString() == "NONE")
            {
                cbShift.Items.Clear();
                btLoadData.Enabled = false;
                cbShift.Enabled = false;
                btLoadData.Enabled = false;
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

            }

        }

        protected void btInactiveDetail_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "openGenericDialog('#InactiveDetails' , 1000 , 600 , 'Bus-Driver Inactive/Block')", true);


        }


        protected void LinkButtonInactive_Click1(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = searchInactive.Text.ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    FillInactiveBusDriver(cbShift.SelectedValue, cbVendorAdm.SelectedValue);
                    dtNewI.Clear();
                }
                else
                {
                    //  search = true;
                    //always check if the viewstate exists before using it
                    if (ViewState["myViewState4"] == null)
                        return;

                    //cast the viewstate as a datatable
                    DataTable dt = ViewState["myViewState4"] as DataTable;

                    //make a clone of the datatable
                    dtNewI = dt.Clone();

                    //search the datatable for the correct fields
                    foreach (DataRow row in dt.Rows)
                    {
                        //add your own columns to be searched here
                        if (row["Bus_ID"].ToString().ToLower().Contains(searchTerm) || row["DriverName"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtNewI.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    GridView_InactiveDetails.DataSource = dtNewI;
                    GridView_InactiveDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btRevisionDetail_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "openGenericDialog('#RevisionDetails' , 1000 , 600 , 'Bus-Driver under Revision')", true);
        }

        protected void btAlertDetail_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "openGenericDialog('#AlertDetails' , 1000 , 600 , 'Bus-Driver under Alert')", true);
        }


        protected void btSearchRevision_Click(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = searchRevision.Text.ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    FillInactiveBusDriver(cbShift.SelectedValue, cbVendorAdm.SelectedValue);
                    dtNewR.Clear();
                }
                else
                {
                    // search = true;
                    //always check if the viewstate exists before using it
                    if (ViewState["myViewState5"] == null)
                        return;

                    //cast the viewstate as a datatable
                    DataTable dt = ViewState["myViewState5"] as DataTable;

                    //make a clone of the datatable
                    dtNewR = dt.Clone();

                    //search the datatable for the correct fields
                    foreach (DataRow row in dt.Rows)
                    {
                        //add your own columns to be searched here
                        if (row["Bus_ID"].ToString().ToLower().Contains(searchTerm) || row["Driver"].ToString().ToLower().Contains(searchTerm)
                            || row["Description"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtNewR.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    GridView_Revision.DataSource = dtNewR;
                    GridView_Revision.DataBind();
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btSearchAlert_Click(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = searchAlert.Text.ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    FillInactiveBusDriver(cbShift.SelectedValue, cbVendorAdm.SelectedValue);
                    dtNewA.Clear();
                }
                else
                {
                    //  search = true;
                    //always check if the viewstate exists before using it
                    if (ViewState["myViewState6"] == null)
                        return;

                    //cast the viewstate as a datatable
                    DataTable dt = ViewState["myViewState6"] as DataTable;

                    //make a clone of the datatable
                    dtNewA = dt.Clone();

                    //search the datatable for the correct fields
                    foreach (DataRow row in dt.Rows)
                    {
                        //add your own columns to be searched here
                        if (row["Bus_ID"].ToString().ToLower().Contains(searchTerm) || row["Driver"].ToString().ToLower().Contains(searchTerm)
                            || row["Comments"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtNewA.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    GridView_Alerts.DataSource = dtNewA;
                    GridView_Alerts.DataBind();
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void GridView_InactiveDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridView_InactiveDetails.PageIndex = e.NewPageIndex;
                GridView_InactiveDetails.DataBind();
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void GridView_Revision_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridView_Revision.PageIndex = e.NewPageIndex;
                GridView_Revision.DataBind();
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void GridView_Alerts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridView_Alerts.PageIndex = e.NewPageIndex;
                GridView_Alerts.DataBind();
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void Grid_CheckPoint2_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                ViewState["isSortingCH2"] = true;
                ViewState["searchStatusCH2"] = false;
                Users usr = (Users)(Session["C_USER"]);
                string sortingDirection = string.Empty;
                if (directionCH2 == SortDirection.Ascending)
                {
                    directionCH2 = SortDirection.Descending;
                    sortingDirection = "Desc";
                }
                else
                {
                    directionCH2 = SortDirection.Ascending;
                    sortingDirection = "Asc";
                }
                Trip_Hrd tripH = new Trip_Hrd();
                DataTable dt = GenericClass.SQLSelectObj(tripH, mappingQueryName: "DO-Checkpoint2", WhereClause:
                           "WHERE [Master_Trip_ID]=" + "'" + masterTripIDHidden.Value + "'" + " AND [TRIP_DETAIL].[Sequence]=2");
                DataView sortedView = new DataView(dt);
                sortedView.Sort = e.SortExpression + " " + sortingDirection;
                Session["objectsCH2"] = sortedView;
                Grid_CheckPoint2.DataSource = sortedView;
                Grid_CheckPoint2.DataBind();
                //  fillCombosCheckpoint1(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }


        }

        public SortDirection directionCH2
        {
            get
            {
                if (ViewState["directionStateCH2"] == null)
                {
                    ViewState["directionStateCH2"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionStateCH2"];
            }
            set
            { ViewState["directionStateCH2"] = value; }
        }

        public SortDirection directionCH3
        {
            get
            {
                if (ViewState["directionStateCH3"] == null)
                {
                    ViewState["directionStateCH3"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionStateCH3"];
            }
            set
            { ViewState["directionStateCH3"] = value; }
        }

        protected void Grid_CheckPoint2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tbSearchCH2.Text = string.Empty;
            Grid_CheckPoint2.PageIndex = e.NewPageIndex;
            if ((bool)ViewState["searchStatusCH2"] == true)
            { Grid_Checkpoint1.DataSource = ViewState["searchResultsCH2"]; }
            if ((bool)ViewState["isSortingCH2"] == true)
            { Grid_Checkpoint1.DataSource = Session["ObjectsCH2"]; }
            Grid_CheckPoint2.DataBind();
            // fillCombosCheckpoint1(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
        }

        protected void Grid_CheckPoint3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tbSearchCH3.Text = string.Empty;
            Grid_CheckPoint3.PageIndex = e.NewPageIndex;
            if ((bool)ViewState["searchStatusCH3"] == true)
            { Grid_CheckPoint3.DataSource = ViewState["searchResultsCH3"]; }
            if ((bool)ViewState["isSortingCH3"] == true)
            { Grid_CheckPoint3.DataSource = Session["ObjectsCH3"]; }
            Grid_CheckPoint3.DataBind();
        }

        protected void Grid_CheckPoint3_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                tbSearchCH3.Text = string.Empty;
                ViewState["isSortingCH3"] = true;
                ViewState["searchStatusCH3"] = false;
                Users usr = (Users)(Session["C_USER"]);
                string sortingDirection = string.Empty;
                if (directionCH3 == SortDirection.Ascending)
                {
                    directionCH3 = SortDirection.Descending;
                    sortingDirection = "Desc";
                }
                else
                {
                    directionCH3 = SortDirection.Ascending;
                    sortingDirection = "Asc";
                }
                Trip_Hrd tripH3 = new Trip_Hrd();
                DataTable dt = GenericClass.SQLSelectObj(tripH3, mappingQueryName: "DO-Checkpoint3", WhereClause:
                           "WHERE [Master_Trip_ID]=" + "'" + masterTripIDHidden.Value + "'" + " AND [TRIP_DETAIL].[Sequence]=3");
                DataView sortedView = new DataView(dt);
                sortedView.Sort = e.SortExpression + " " + sortingDirection;
                Session["objectsCH3"] = sortedView;
                Grid_CheckPoint3.DataSource = sortedView;
                Grid_CheckPoint3.DataBind();
                //  fillCombosCheckpoint1(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void btRefreshCH1_Click(object sender, EventArgs e)
        {
            if (cbShift.SelectedValue != string.Empty)
            {
                fillGridCheckpoint1(cbShift.SelectedValue.ToString(), cbVendorAdm.SelectedValue);
                ViewState["searchStatus"] = false;
                ViewState["isSorting"] = false;
                ViewState["checkpoint1Editing"] = false;
                Grid_Checkpoint1.EditIndex = -1;
                functions.ShowMessage(this, this.GetType(), msg_DailyOperation_Refresh1, MessageType.Success);
            }
            else
                functions.ShowMessage(this, this.GetType(), msg_DailyOperation_selectShift, MessageType.Error);
        }

        protected void btRefreshCH2_Click(object sender, EventArgs e)
        {
            if (cbShift.SelectedValue != string.Empty)
            {
                fillGridCheckpoint2();
                ViewState["searchStatusCH2"] = false;
                ViewState["isSortingCH2"] = false;
                ViewState["checkpoint1EditingCH2"] = false;
                Grid_CheckPoint2.EditIndex = -1;
                functions.ShowMessage(this, this.GetType(), msg_DailyOperation_Refresh2, MessageType.Success);
            }
            else
                functions.ShowMessage(this, this.GetType(), msg_DailyOperation_selectShift, MessageType.Error);
        }

        protected void btRefreshCH3_Click(object sender, EventArgs e)
        {
            if (cbShift.SelectedValue != string.Empty)
            {
                fillGridCheckpoint3();
                ViewState["searchStatusCH3"] = false;
                ViewState["isSortingCH3"] = false;
                ViewState["checkpoint1EditingCH3"] = false;
                Grid_CheckPoint3.EditIndex = -1;
                functions.ShowMessage(this, this.GetType(), msg_DailyOperation_Refresh3, MessageType.Success);
            }
            else
                functions.ShowMessage(this, this.GetType(), msg_DailyOperation_selectShift, MessageType.Error);
        }

        protected void btHelp_Click(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("./Login.aspx");
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "openGenericDialog('#dialogHelp' , 700 , 600 , 'Ayuda - Operacion diaria');", true);
        }

    }
}