using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusManagementSystem._01Catalogos;
using BusManagementSystem.Catalogos;
using BusManagementSystem.Class;
using System.Data.SqlClient;
using System.Globalization;




namespace BusManagementSystem.Maintenance
{
    public partial class Maintenance_I : System.Web.UI.Page
    {
        string vendorFilter;

        public DataTable ReturnTable = null;

        private AddPrivilege privilege = new AddPrivilege();
        string language = null;
        string msg_MaintenanceI_ActivitySuccessfully;
        string msg_MaintenanceI_SelectV;
        string msg_MaintenanceI_SelectBus;
        string msg_MaintenanceI_SelectRoute;
        string msg_MaintenanceI_SelectShift;
        string msg_MaintenanceI_RevisionChecked;
        string msg_MaintenanceI_ActivityID;
        string msg_MaintenanceI_ExecutedSuccessfully;
        







        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

                //Load buttons to hide or unhide for privileges
                privilege = new AddPrivilege("10001", btAdd, btReset, btSave, btClose, btExcel);

           

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("~/Login.aspx");
                Session["GV_SESSION"] = null;
               
            }

            Users usr = (Users)(Session["C_USER"]);


                privilege.DtPrivilege = privilege.GetPrivilege(this.Page.Title, usr);


            if (usr.Profile != "INTERNAL")
            {
                vendorFilter = "Where Vendor_ID='" + usr.Vendor_ID + "'";
                Session["GV_SESSION"] = null;
            }
            if (!this.IsPostBack)
            {
                
                    DD_Grid_Vendor.SelectedIndex = 0;
                    DD_Grid_Status.Enabled = true;
                    DD_Grid_Vendor.Enabled = true;
                    GridView1.Visible = true;
                    ViewState["searchStatus"] = false;
                    ViewState["isSorting"] = false;
                    Fill_DD_v_Vendor();
                    PrivilegeAndProcessFlow("10001");
            }

            applyLanguage();
            messages_ChangeLanguage();

            if ((bool)ViewState["searchStatus"] == false && (bool)ViewState["isSorting"] == false)
                FillGrid();
    

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
        protected void messages_ChangeLanguage()
        {
            msg_MaintenanceI_ActivitySuccessfully = (string)GetGlobalResourceObject(language, "msg_MaintenanceI_ActivitySuccessfully");
            msg_MaintenanceI_SelectV = (string)GetGlobalResourceObject(language, "msg_MaintenanceI_SelectV");
            msg_MaintenanceI_SelectBus = (string)GetGlobalResourceObject(language, "msg_MaintenanceI_SelectBus");
            msg_MaintenanceI_SelectRoute = (string)GetGlobalResourceObject(language, "msg_MaintenanceI_SelectRoute");
            msg_MaintenanceI_SelectShift = (string)GetGlobalResourceObject(language, "msg_MaintenanceI_SelectShift");
            msg_MaintenanceI_RevisionChecked = (string)GetGlobalResourceObject(language, "msg_MaintenanceI_RevisionChecked");
            msg_MaintenanceI_ActivityID = (string)GetGlobalResourceObject(language, "msg_MaintenanceI_ActivityID");
            msg_MaintenanceI_ExecutedSuccessfully = (string)GetGlobalResourceObject(language, "msg_MaintenanceI_ExecutedSuccessfully");


        }
        protected void FillGrid()
        {
            try 
            {


                Session["GV_SESSION"] = GenerateTable(DD_Grid_Vendor.SelectedValue.ToString(), DD_Grid_Status.SelectedValue.ToString());

                GridView1.DataSource = (DataTable)Session["GV_SESSION"]; 

                GridView1.PageSize = 15;

                ViewState["backupData"] = (DataTable)Session["GV_SESSION"]; 

                GridView1.DataBind();

                
              

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            

        }


        protected void Fill_DD_v_Vendor()
        {
            try 
            {
                ListItem li = new ListItem("ALL", "ALL");
                Users usr = (Users)(Session["C_USER"]);

                if (usr.Profile.Equals("INTERNAL"))
                {

                    Vendor initialVendor = new Vendor();
                    DataTable dt = GenericClass.SQLSelectObj(initialVendor, WhereClause: string.IsNullOrWhiteSpace(vendorFilter) ? null : vendorFilter);
                    DataView dvVendor = new DataView(dt);
                    DataTable distincVendor = dvVendor.ToTable(true, "VENDOR_ID", "NAME");
                    DD_Grid_Vendor.Items.Clear();
                    DD_Grid_Vendor.DataSource = distincVendor;
                    DD_Grid_Vendor.DataValueField = "VENDOR_ID";
                    DD_Grid_Vendor.DataTextField = "NAME";
                    DD_Grid_Vendor.DataBind();

                    DD_Grid_Vendor.Items.Insert(0, li);
                    DD_Grid_Vendor.SelectedIndex = 0;
                }
                else
                {
                    List<string> Parameters = new List<string>();
                    Parameters.Add("Vendor_ID");
                    Parameters.Add("Name");
                    DataTable dt = GenericClass.SQLSelectObj(new Vendor(), WhereClause: string.IsNullOrWhiteSpace(vendorFilter) ? null : vendorFilter);
                    DD_Grid_Vendor.Items.Clear();
                    DD_Grid_Vendor.DataSource = dt;
                    DD_Grid_Vendor.DataValueField = "Vendor_ID";
                    DD_Grid_Vendor.DataTextField = "Name";
                    DD_Grid_Vendor.DataBind();
                }

                DD_Grid_Status.Items.Clear();
                List<string> Columns = new List<string>();
                Columns.Add("Status");
                DD_Grid_Status.DataSource = GenericClass.SQLSelectObj(new Legal_Revision(), mappingQueryName: "Status_Distinct", customSelect: Columns);
                DD_Grid_Status.DataValueField = "status";
                DD_Grid_Status.DataTextField = "STATUS";
                DD_Grid_Status.DataBind();
                DD_Grid_Status.Items.Insert(0, li);
                DD_Grid_Status.SelectedIndex = 0;
            
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            
           

        }

        protected void FillVendorDropDown()
        {
            try 
            {
                ListItem li = new ListItem("Select", "Select");
                Vendor initialVendor = new Vendor();
                DataTable dt = GenericClass.SQLSelectObj(initialVendor, WhereClause: string.IsNullOrWhiteSpace(vendorFilter) ? null : vendorFilter);
                DataView dvVendor = new DataView(dt);
                DataTable distincVendor = dvVendor.ToTable(true, "VENDOR_ID", "NAME");
                DD_Vendor.Items.Clear();
                DD_Vendor.DataSource = distincVendor;
                DD_Vendor.DataValueField = "VENDOR_ID";
                DD_Vendor.DataTextField = "NAME";
                DD_Vendor.DataBind();
                DD_Vendor.Items.Insert(0, li);
                DD_Vendor.SelectedIndex = 0;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
                DataTable dtShift = GenericClass.SQLSelectObj(new Shift(), mappingQueryName: "Catalog");
                DD_Shift.Items.Clear();
                DD_Shift.DataSource = dtShift;
                DD_Shift.DataValueField = "SHIFT_ID";
                DD_Shift.DataTextField = "NAME";
                DD_Shift.DataBind();
                DD_Shift.Items.Insert(0, li);
                DD_Shift.SelectedIndex = 0;
            
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

            
            
        }

        public void FillRouteDropDown()
        {
            try 
            {

                ListItem listSelect = new ListItem("Select", "Select");
                DataTable Shift_Type = GenericClass.SQLSelectObj(new Shift(), WhereClause: "where shift_ID = '" + DD_Shift.SelectedValue + "'");
                if (Shift_Type.Rows.Count == 0)
                {
                    
                }
                else
                {
                    bool Flag_Type = bool.Parse(Shift_Type.Rows[0]["Exit_Shift"].ToString());
                    DataTable DB_ROUTE = new DataTable();
                    List<string> Routes = new List<string>();
                    Routes.Add("Route_ID");
                    if (Flag_Type)
                    {
                        DB_ROUTE = GenericClass.SQLSelectObj(new Route(), WhereClause: "where SP.End_Point = 1", mappingQueryName: "Revision", customSelect: Routes);

                    }
                    else
                    {

                        DB_ROUTE = GenericClass.SQLSelectObj(new Route(), WhereClause: "where SP.End_Point = 0", mappingQueryName: "Revision", customSelect: Routes);
                    }

                    DD_Route.Items.Clear();
                    DD_Route.DataSource = DB_ROUTE;
                    DD_Route.DataValueField = "Route_ID";
                    DD_Route.DataTextField = "Name";
                    DD_Route.DataBind();
                    DD_Route.Enabled = true;
                    DD_Route.Items.Insert(0, listSelect);
                    DD_Route.SelectedIndex = 0;
                }
                
            
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        public void FillBusDriverIDDropDwon()
        {
            try
            {
                ListItem li = new ListItem("Select", "Select");
                List<string> Columns = new List<string>();
                Columns.Add("Bus_Driver_ID");
                DataTable Bus_ID = GenericClass.SQLSelectObj(new Bus_Driver(), WhereClause: "where Bus_Driver.Vendor_ID =  '" + DD_Vendor.SelectedValue + "' and Bus_Driver.Shift_ID = '" + DD_Shift.SelectedValue + "'", customSelect: Columns, mappingQueryName: "DST-Bus_Driver_ID");
                if (Bus_ID.Rows.Count <= 0)
                {
                    FillLikeEmpty();
                }
                else
                {

                    Bus_Driver_ID.Items.Clear();
                    Bus_Driver_ID.DataSource = Bus_ID;
                    Bus_Driver_ID.DataValueField = "Bus_Driver_ID";
                    Bus_Driver_ID.DataTextField = "Bus_Driver";
                    Bus_Driver_ID.DataBind();
                    Bus_Driver_ID.Enabled = true;
                    Bus_Driver_ID.Items.Insert(0, li);
                    Bus_Driver_ID.SelectedIndex = 0;


                }
                //btSave.Enabled = true;

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void Fill_TextBox()
        {
            try
            {
                List<string> columns = new List<string>();
                columns.Add("Bus_Driver_ID");
                DataTable DOp = GenericClass.GetCustomData("select datepart(ww, getdate()) as CurrentWeek");
                DataTable TB_BD_and_B = GenericClass.SQLSelectObj(new Bus_Driver(), mappingQueryName: "Legal_Revision_TextBox", WhereClause: "where Bus_Driver_ID = '" + Bus_Driver_ID.SelectedValue.ToString() + "'", customSelect: columns);
                if (TB_BD_and_B.Rows.Count != 0)
                {
                    TB_License.Text = TB_BD_and_B.Rows[0]["License_Driver"].ToString();
                    TB_SN.Text = TB_BD_and_B.Rows[0]["SN"].ToString();
                    TB_MY.Text = TB_BD_and_B.Rows[0]["MY"].ToString();
                    TB_Week.Text = DOp.Rows[0]["CurrentWeek"].ToString();
                    TB_Plaque.Text = TB_BD_and_B.Rows[0]["Plaque"].ToString();
                    CB_GPS.Enabled = true;
                    CB_I_As_Pce.Enabled = true;
                    CB_Lintern.Enabled = true;
                    CB_Radio.Enabled = true;
                    CB_Sfty_reflectors.Enabled = true;
                    CB_Travel_Map.Enabled = true;
                }
               
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        protected void FillLikeEmpty()
        {
            try
            {
                Bus_Driver_ID.DataTextField = "No data";
                TB_License.Text = "No data";
                TB_SN.Text = "No data";
                TB_MY.Text = "No data";
                TB_Week.Text = "No data";
                TB_Plaque.Text = "No data";
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }


        }

        public void Fill_Legal_Revision(String Legal_Revision_ID, Boolean Status)
        {
            try
            {
                ListItem listAll = new ListItem("ALL", "ALL");
                List<string> columns = new List<string>();
                columns.Add("Legal_Revision_ID");
                columns.Add("Revision_Date");

                Session["select"] = true;

                DataTable DT_Fill = GenericClass.SQLSelectObj(new Legal_Revision(), WhereClause: "Where Legal_Revision_ID = '" + Legal_Revision_ID + "'");

                DataTable DT_FILL_OTHER = GenericClass.SQLSelectObj(new Legal_Revision(), WhereClause: "where LEGAL_REVISION.Legal_Revision_ID = '" + Legal_Revision_ID + "'", mappingQueryName: "Legal_revision_ID__Fill_Other", customSelect: columns); //, WhereClause: " Where  [DRIVER].[Vendor_ID]= '" + usr.Vendor_id + "'");

                DataTable DT_DRIVER = new DataTable();
                DT_DRIVER.Clear();
                DT_DRIVER.Columns.Add("Driver_Name");
                DT_DRIVER.Columns.Add("Driver_Id");
                DataRow _R = DT_DRIVER.NewRow();
                _R["Driver_Name"] = DT_FILL_OTHER.Rows[0]["NAME_BUS"].ToString();
                _R["Driver_Id"] = DT_FILL_OTHER.Rows[0]["Bus_ID"].ToString();
                DT_DRIVER.Rows.Add(_R);
                Bus_Driver_ID.DataSource = DT_DRIVER;
                Bus_Driver_ID.DataValueField = "Driver_Id";
                Bus_Driver_ID.DataTextField = "Driver_Name";
                Bus_Driver_ID.DataBind();

                DataTable DT_Route = new DataTable();
                DT_Route.Clear();
                DT_Route.Columns.Add("Route");
                DT_Route.Columns.Add("Route_Id");
                DataRow _RR = DT_Route.NewRow();
                _RR["Route"] = DT_FILL_OTHER.Rows[0]["Route_Con"].ToString();
                _RR["Route_Id"] = DT_FILL_OTHER.Rows[0]["Route_ID"].ToString();
                DT_Route.Rows.Add(_RR);
                DD_Route.DataSource = DT_Route;
                DD_Route.DataValueField = "Route_Id";
                DD_Route.DataTextField = "Route";
                DD_Route.DataBind();

                DataTable DT_Shift = new DataTable();
                DT_Shift.Clear();
                DT_Shift.Columns.Add("SHIFT_NAME");
                DT_Shift.Columns.Add("SHIFT_ID");
                DataRow _S = DT_Shift.NewRow();
                _S["SHIFT_NAME"] = DT_FILL_OTHER.Rows[0]["Shift_Name"].ToString();
                _S["SHIFT_ID"] = DT_FILL_OTHER.Rows[0]["Shif_ID"].ToString();
                DT_Shift.Rows.Add(_S);
                DD_Shift.DataSource = DT_Shift;
                DD_Shift.DataValueField = "SHIFT_ID";
                DD_Shift.DataTextField = "SHIFT_NAME";
                DD_Shift.DataBind();

                DataTable DT_VENDOR = new DataTable();
                DT_VENDOR.Clear();
                DT_VENDOR.Columns.Add("VENDOR_ID");
                DT_VENDOR.Columns.Add("VENDOR_NAME");
                DataRow _V = DT_VENDOR.NewRow();
                _V["VENDOR_ID"] = DT_FILL_OTHER.Rows[0]["Vendor_ID"].ToString();
                _V["VENDOR_NAME"] = DT_FILL_OTHER.Rows[0]["V_NAME"].ToString();
                DT_VENDOR.Rows.Add(_V);
                DD_Vendor.DataSource = DT_VENDOR;
                DD_Vendor.DataValueField = "VENDOR_ID";
                DD_Vendor.DataTextField = "VENDOR_NAME";
                DD_Vendor.DataBind();

                TB_Plaque.Text = DT_FILL_OTHER.Rows[0]["License"].ToString();
                TB_SN.Text = DT_FILL_OTHER.Rows[0]["SN"].ToString();
                TB_MY.Text = DT_FILL_OTHER.Rows[0]["MY"].ToString();
                TB_License.Text = DT_FILL_OTHER.Rows[0]["License_D"].ToString();
                GregorianCalendar cal = new GregorianCalendar(GregorianCalendarTypes.Localized);

                TB_Week.Text = cal.GetWeekOfYear(Convert.ToDateTime(DT_Fill.Rows[0]["Revision_Date(M/D/Y)"].ToString()), CalendarWeekRule.FirstDay, DayOfWeek.Monday).ToString() ;

                CB_GPS.Checked = Convert.ToBoolean(DT_Fill.Rows[0]["GPS"]);
                CB_I_As_Pce.Checked = Convert.ToBoolean(DT_Fill.Rows[0]["PCI"]);
                CB_Lintern.Checked = Convert.ToBoolean(DT_Fill.Rows[0]["Lintern"]);
                CB_Radio.Checked = Convert.ToBoolean(DT_Fill.Rows[0]["Communication_Radio"]);
                CB_Sfty_reflectors.Checked = Convert.ToBoolean(DT_Fill.Rows[0]["Security_Reflectors"]);
                CB_Travel_Map.Checked = Convert.ToBoolean(DT_Fill.Rows[0]["Travel_Map"]);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);

                DD_Route.Enabled = false;
                Bus_Driver_ID.Enabled = false;
                DD_Shift.Enabled = false;
                DD_Vendor.Enabled = false;

                TB_License.Enabled = false;
                TB_SN.Enabled = false;
                TB_MY.Enabled = false;
                TB_Week.Enabled = false;
                TB_Plaque.Enabled = false;

                if (Status == true)
                {

                    PrivilegeAndProcessFlow("01111");
                }
                else
                {

                    PrivilegeAndProcessFlow("01101");
                }

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        protected void btAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DD_Vendor.Enabled = true;
                DD_Shift.Enabled = true;
                FillVendorDropDown();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
                PrivilegeAndProcessFlow("01101");
                //buttons_Steps("Add");
                //GridView1.Visible = false;
                Session["Add_Flag"] = true;
                Session["DropDownFlag"] = true;
                Session["Select"] = false;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
           

        }


        protected void btReset_Click(object sender, EventArgs e)
        {
            try
            {
                Session["GV_SESSION"] = GenerateTable(null, null);
                PrivilegeAndProcessFlow("10001");
               // Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", false);
                Session["Legal_Revision_BK"] = null;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }


        }


        protected void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (DD_Vendor.SelectedValue.Trim() != "Select" &&
                    Bus_Driver_ID.SelectedValue.Trim() != "Select" && 
                    DD_Route.SelectedValue.ToString() != "Select" && 
                    DD_Shift.SelectedValue.Trim() != "Select")
                {
                    if (Convert.ToBoolean(Session["Select"]) == true)
                    {
                        Session["TypeOfUpdate"] = "Save";
                        Update();
                        Session["Select"] = false;

                    }
                    else
                    {
                        Legal_Revision LR = new Legal_Revision();
                        LR.Revision_Date = DateTime.Now;
                        LR.Vendor_ID = DD_Vendor.SelectedValue.Trim();
                        LR.Route_ID = Convert.ToInt32(DD_Route.SelectedValue.ToString());
                        LR.Bus_Driver_ID = Bus_Driver_ID.SelectedValue.Trim();
                        LR.Shift_ID = DD_Shift.SelectedValue.Trim();
                        LR.Travel_Map = Convert.ToBoolean(CB_Travel_Map.Checked);
                        LR.Communication_Radio = Convert.ToBoolean(CB_Radio.Checked);
                        LR.GPS = Convert.ToBoolean(CB_GPS.Checked);
                        LR.PCI = Convert.ToBoolean(CB_I_As_Pce.Checked);
                        LR.Lintern = Convert.ToBoolean(CB_Lintern.Checked);
                        LR.Security_Reflectors = Convert.ToBoolean(CB_Sfty_reflectors.Checked);
                        LR.Status = "OPEN";
                        GenericClass.SQLInsertObj(LR);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", false);
                        GridView1.Enabled = true;

                        functions.ShowMessage(this, this.GetType(), msg_MaintenanceI_ActivitySuccessfully, MessageType.Success);
                    }
                    Session["GV_SESSION"] = GenerateTable(DD_Grid_Vendor.SelectedValue.ToString(), DD_Grid_Status.SelectedValue.ToString());
                    GridView1.DataSource = (DataTable)Session["GV_SESSION"];
                   
                    GridView1.DataBind();
                    PrivilegeAndProcessFlow("10001");
                }
                else
                {
            
                    if (DD_Vendor.SelectedValue.Trim() == "Select")
                    {

                        functions.ShowMessage(this, this.GetType(), msg_MaintenanceI_SelectV, MessageType.Error);
                    }
                    else if (Bus_Driver_ID.SelectedValue.Trim() == "Select")
                    {

                        functions.ShowMessage(this, this.GetType(), msg_MaintenanceI_SelectBus, MessageType.Error);
                    }
                    else if (DD_Route.SelectedValue.ToString() == "Select")
                    {

                        functions.ShowMessage(this, this.GetType(), msg_MaintenanceI_SelectRoute, MessageType.Error);
                    }
                    else if (DD_Shift.SelectedValue.Trim() == "Select")
                    {

                        functions.ShowMessage(this, this.GetType(), msg_MaintenanceI_SelectShift, MessageType.Error);
                    }


                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);

                }
                
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            
        }


        protected void DD_Vendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DD_Shift.Enabled = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
                Session["DropDownFlag"] = true;
                Session["Add_Flag"] = true;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            
        }

        protected void DD_Shift_SelectedIndexChanged(object sender, EventArgs e)
       {
           try
           {
               FillRouteDropDown();
               FillBusDriverIDDropDwon();
               Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
               Session["DropDownFlag"] = true;
               Session["Add_Flag"] = true;
               //if(DD_Grid_Vendor.SelectedValue.ToString() == "Select" && DD_Shift.SelectedValue.ToString() == "Select" && DD_Route.SelectedValue == "Select" && dd)
           }
           catch (Exception ex)
           {
               functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
           }
            
            
        }

        protected void Bus_Driver_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
                Fill_TextBox();
                Session["DropDownFlag"] = true;
                Session["Add_Flag"] = true;
                btSave.Enabled = true;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        


        protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvRow = GridView1.SelectedRow;
                if (HttpUtility.HtmlDecode(GridView1.Rows[gvRow.RowIndex].Cells[3].Text) == "CLOSE")
                {
                    Fill_Legal_Revision(HttpUtility.HtmlDecode(GridView1.Rows[gvRow.RowIndex].Cells[2].Text), false);

                }
                else
                {
                    Fill_Legal_Revision(HttpUtility.HtmlDecode(GridView1.Rows[gvRow.RowIndex].Cells[2].Text), true);
                }

                
                Session["Legal_Revision_BK"] = HttpUtility.HtmlDecode(GridView1.Rows[gvRow.RowIndex].Cells[2].Text);

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            
        }

        protected void btEdit_Click(object sender, EventArgs e)
        {
            PrivilegeAndProcessFlow("01111");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);

        }

        protected void btClose_Click(object sender, EventArgs e)
        {

            try
            {
                int revision_ID = Convert.ToInt32(Session["Legal_Revision_BK"].ToString());
                bool errors = validateRevisionSteps(revision_ID);

                if (!errors)
                {
                    Session["TypeOfUpdate"] = "Close";
                    Update();

                }

                else
                {

                    functions.ShowMessage(this, this.GetType(), msg_MaintenanceI_RevisionChecked, MessageType.Error);
                  

                }
                Session["GV_SESSION"] = GenerateTable(DD_Grid_Vendor.SelectedValue.ToString(), DD_Grid_Status.SelectedValue.ToString());
                GridView1.DataSource = (DataTable)Session["GV_SESSION"];
                
                GridView1.DataBind();
                PrivilegeAndProcessFlow("10001");

            }

            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);

            }
        }

        private bool validateRevisionSteps(int legalRevision)
        {

            bool error = false;
            List<string> columns = new List<string>();
            columns.Add("Is_Checked");

            DataTable getAllCheckboxes = GenericClass.SQLSelectObj(new Revision_Check(), WhereClause: "WHERE LEGAL_REVISION_ID =" + legalRevision +" And Is_Checked=0", customSelect: columns); 
            if (getAllCheckboxes.Rows.Count > 0)
            {
                //foreach (DataRow dr in getAllCheckboxes.Rows)
                //{

                //    bool is_Checked = Convert.ToBoolean(dr["Is_Checked"].ToString());

                //    if (is_Checked == false)
                //    {

                //        error = true;

                //    }

                error=true;
                //}
            }

            return error;

        }



        protected void Update()
        {
            try
            {
                Legal_Revision LR = new Legal_Revision();
                LR.Revision_Date = DateTime.Now;
                LR.Vendor_ID = DD_Vendor.SelectedValue.Trim();
                LR.Route_ID = Convert.ToInt32(DD_Route.SelectedValue.ToString());
                LR.Bus_Driver_ID = Bus_Driver_ID.SelectedValue.Trim();
                LR.Shift_ID = DD_Shift.SelectedValue.Trim();
                LR.Travel_Map = Convert.ToBoolean(CB_Travel_Map.Checked);
                LR.Communication_Radio = Convert.ToBoolean(CB_Radio.Checked);
                LR.GPS = Convert.ToBoolean(CB_GPS.Checked);
                LR.PCI = Convert.ToBoolean(CB_I_As_Pce.Checked);
                LR.Lintern = Convert.ToBoolean(CB_Lintern.Checked);
                LR.Security_Reflectors = Convert.ToBoolean(CB_Sfty_reflectors.Checked);

                DataTable DT_Current = GenericClass.SQLSelectObj(new Legal_Revision(), WhereClause: "Where Legal_Revision_ID = '" + Session["Legal_Revision_BK"].ToString() + "'");
                Legal_Revision Legal_Revision_Current = new Legal_Revision();
                Legal_Revision_Current.Revision_Date = Convert.ToDateTime(DT_Current.Rows[0]["Revision_Date(M/D/Y)"]);
                Legal_Revision_Current.Vendor_ID = DT_Current.Rows[0]["Vendor_ID"].ToString();
                Legal_Revision_Current.Route_ID = Convert.ToInt32(DT_Current.Rows[0]["Route_ID"]);
                Legal_Revision_Current.Bus_Driver_ID = DT_Current.Rows[0]["Bus_Driver_ID"].ToString();
                Legal_Revision_Current.Shift_ID = DT_Current.Rows[0]["Shift_ID"].ToString();
                Legal_Revision_Current.Travel_Map = Convert.ToBoolean(DT_Current.Rows[0]["Travel_Map"]);
                Legal_Revision_Current.Communication_Radio = Convert.ToBoolean(DT_Current.Rows[0]["Communication_Radio"]);
                Legal_Revision_Current.GPS = Convert.ToBoolean(DT_Current.Rows[0]["GPS"]);
                Legal_Revision_Current.PCI = Convert.ToBoolean(DT_Current.Rows[0]["PCI"]);
                Legal_Revision_Current.Lintern = Convert.ToBoolean(DT_Current.Rows[0]["Lintern"]);
                Legal_Revision_Current.Security_Reflectors = Convert.ToBoolean(DT_Current.Rows[0]["Security_Reflectors"]);
                Legal_Revision_Current.Status = DT_Current.Rows[0]["Status"].ToString();

                if (Session["TypeOfUpdate"].ToString() == "Save")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", false);
                    
                    LR.Status = "OPEN";
                    
                    GenericClass.SQLUpdateObj(new Legal_Revision(), adminApprove.compareObjects(Legal_Revision_Current, LR, ""), "Where Legal_Revision_ID= '" + Session["Legal_Revision_BK"].ToString() + "'");                    
                }
                else if (Session["TypeOfUpdate"].ToString() == "Close")
                {

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", false);
                    
                    LR.Status = "CLOSE";
                    
                    GenericClass.SQLUpdateObj(new Legal_Revision(), adminApprove.compareObjects(Legal_Revision_Current, LR, ""), "Where Legal_Revision_ID= '" + Session["Legal_Revision_BK"].ToString() + "'");
                    
                }

                
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            functions.ShowMessage(this, this.GetType(), msg_MaintenanceI_ActivityID + Session["Legal_Revision_BK"] + msg_MaintenanceI_ExecutedSuccessfully, MessageType.Success);
            Session["GV_SESSION"] = GenerateTable(DD_Grid_Vendor.SelectedValue.ToString(), DD_Grid_Status.SelectedValue.ToString());
            Session["Legal_Revision_BK"] = null;
            PrivilegeAndProcessFlow("10001");
        
        }

      

        protected void DD_Grid_Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            try 
            {
                Session["DropDownFlag"] = true;
                Session["Add_Flag"] = true;
                ViewState["backupData"] = 
                Session["GV_SESSION"] = GenerateTable(DD_Grid_Vendor.SelectedValue.ToString(), DD_Grid_Status.SelectedValue.ToString());
                Session["DD_Vendor"] = 
                GridView1.DataSource = (DataTable)Session["GV_SESSION"];

                GridView1.DataBind();
                tbSearchCH1.Text = string.Empty;

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
         
            
         
        }

        protected void DD_Grid_Vendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["DropDownFlag"] = true;
                Session["Add_Flag"] = true;
                ViewState["backupData"] = 
                Session["GV_SESSION"] = GenerateTable(DD_Grid_Vendor.SelectedValue.ToString(), DD_Grid_Status.SelectedValue.ToString());
                GridView1.DataSource = (DataTable)Session["GV_SESSION"];
                GridView1.DataBind();
                tbSearchCH1.Text = string.Empty;

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }


        public void PrivilegeAndProcessFlow(string turnOnOffBNuttons)
        {
            privilege.applyPrivilege(turnOnOffBNuttons, GridView1);
            ProcessFlow();

        }


        protected void ProcessFlow()
        {
            try
            {
                if (this.btAdd.Visible == true || (this.btAdd.Visible == false & this.btSave.Visible == false))
                {


                    btAdd.Enabled = true;
                    btAdd.Visible = true;
                    btClose.Enabled = false;
                    btClose.Visible = false;
                    btEdit.Enabled = false;
                    btEdit.Visible = false;
                    btReset.Enabled = false;
                    btReset.Visible = false;
                    btSave.Enabled = false;
                    btSave.Visible = false;

                    DD_Grid_Status.Enabled = true;
                    DD_Grid_Vendor.Enabled = true;
                    btSearchCH1.Enabled = true;
                    btExcel.Enabled = true;
                    tbSearchCH1.Enabled = true;

                    Clear_Items();
                    Disable_Items();

                    GridView1.Columns[0].Visible = true;
                    GridView1.Columns[1].Visible = true;

                    privilege.applyPrivilege("10001", GridView1);
                }
                if (this.btSave.Visible == true && btClose.Visible==false)
                {

                    btAdd.Enabled = false;
                    btAdd.Visible = false;
                    btEdit.Enabled = false;
                    btEdit.Visible = false;
                    btReset.Enabled = true;
                    btReset.Visible = true;
                    btSave.Enabled = false;
                    btSave.Visible = true;
                    DD_Grid_Status.Enabled = false;
                    DD_Grid_Vendor.Enabled = false;
                    btSearchCH1.Enabled = false;
                    btExcel.Enabled = false;
                    tbSearchCH1.Enabled = false;
                    GridView1.Columns[0].Visible = false;
                    GridView1.Columns[1].Visible = false;
                    

                    CB_GPS.Enabled = false;
                    CB_I_As_Pce.Enabled = false;
                    CB_Lintern.Enabled = false;
                    CB_Radio.Enabled = false;
                    CB_Sfty_reflectors.Enabled = false;
                    CB_Travel_Map.Enabled = false;

                }
                if (this.btSave.Visible == true && btClose.Visible == true)
                {

                    btEdit.Enabled = false;
                    btEdit.Visible = false;
                    btAdd.Enabled = false;
                    btAdd.Visible = false;
                    btSave.Enabled = true;
                    btSave.Visible = true;
                    btReset.Enabled = true;
                    btReset.Visible = true;
                    btClose.Enabled = true;
                    btClose.Visible = true;
                    DD_Grid_Status.Enabled = false;
                    DD_Grid_Vendor.Enabled = false;
                    btSearchCH1.Enabled = false;
                    btExcel.Enabled = false;
                    tbSearchCH1.Enabled = false;
                    GridView1.Columns[0].Visible = false;
                    GridView1.Columns[1].Visible = false;

                    CB_GPS.Enabled = true;
                    CB_I_As_Pce.Enabled = true;
                    CB_Lintern.Enabled = true;
                    CB_Radio.Enabled = true;
                    CB_Sfty_reflectors.Enabled = true;
                    CB_Travel_Map.Enabled = true;
                }


            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
          
        }
        protected void Clear_Items()
            {
                try
                {
                    GridView1.Enabled = true;
                    //FillGrid();
                    //modificar clear de los dd
                    DD_Route.Items.Clear();
                    Bus_Driver_ID.Items.Clear();
                    DD_Shift.Items.Clear();
                    DD_Vendor.Items.Clear();
                    TB_License.Text = null;
                    TB_SN.Text = null;
                    TB_MY.Text = null;
                    TB_Week.Text = null;
                    TB_Plaque.Text = null;
                    CB_GPS.Checked = false;
                    CB_I_As_Pce.Checked = false;
                    CB_Lintern.Checked = false;
                    CB_Radio.Checked = false;
                    CB_Sfty_reflectors.Checked = false;
                    CB_Travel_Map.Checked = false;
                }
                catch (Exception ex)
                {
                    functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
                }

            }

        protected void Disable_Items()
        {

            try
            {
                DD_Route.Enabled = false;
                Bus_Driver_ID.Enabled = false;
                DD_Shift.Enabled = false;
                DD_Vendor.Enabled = false;
                TB_License.Enabled = false;
                TB_SN.Enabled = false;
                TB_MY.Enabled = false;
                TB_Week.Enabled = false;
                TB_Plaque.Enabled = false;
                CB_GPS.Enabled = false;
                CB_I_As_Pce.Enabled = false;
                CB_Lintern.Enabled = false;
                CB_Radio.Enabled = false;
                CB_Sfty_reflectors.Enabled = false;
                CB_Travel_Map.Enabled = false;
            }
            catch(Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        
        }

        protected void IbtnConfirm_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton btView = (ImageButton)sender;
                string idToRedirect = Convert.ToString(btView.CommandArgument);
                Response.Clear();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Redirect(string.Format("~/Maintenance/EditRevision.aspx?Revision={0}", idToRedirect), false);
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ImageButton ViewBT = e.Row.FindControl("IbtnConfirm") as ImageButton;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[3].Text == "CLOSE")
                { 

                    ViewBT.Visible = false;

                }
            }
        }

        protected void GridView1_PageIndexChanged(object sender, EventArgs e)
        {
            
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
                Session["Excel"] = null;
                Session["name"] = string.Empty;

                if (Session["C_USER"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                Users usr = (Users)(Session["C_USER"]);
                DataTable dtLoad = GenericClass.SQLSelectObj(new Revision(), mappingQueryName: "Catalog");
                Session["Excel"] = dtLoad;
                Session["name"] = "Service";
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;          
            if ((bool)ViewState["searchStatus"] == true)
            { GridView1.DataSource = ViewState["searchResults"]; }
            if ((bool)ViewState["isSorting"] == true)
            { GridView1.DataSource = Session["Objects"]; }
            GridView1.DataBind();
        }
        protected DataTable GenerateTable (String Vendor, String Status)
        {

            if (Vendor == null && Status == null || Vendor == "ALL" && Status == "ALL" || Vendor == "" && Status == "")
            {
                Users usr = (Users)(Session["C_USER"]);
                 ReturnTable = GenericClass.GetCustomData(
                    vendorFilter == null ? "select  LR.Legal_Revision_ID, LR.Status as Status ,V.Name as Vendor_Name ,LR.Revision_Date as  Revision_Date , D.Name + '/' + BD.Bus_ID as Driver_and_Bus,"
                + "SF.Name Shift  from LEGAL_REVISION LR inner join VENDOR V on LR.Vendor_ID = V.Vendor_ID inner join BUS_DRIVER BD on LR.Bus_Driver_ID = BD.Bus_Driver_ID  inner join DRIVER D on BD.Driver_ID = D.Driver_ID inner join SHIFT SF on LR.Shift_ID = SF.Shift_ID  ORDER BY LR.Legal_Revision_ID DESC "
                : "select  LR.Legal_Revision_ID, LR.Status as Status ,V.Name as Vendor_Name ,LR.Revision_Date as  Revision_Date , D.Name + '/' + BD.Bus_ID as Driver_and_Bus,"
                + "SF.Name  from LEGAL_REVISION LR inner join VENDOR V on LR.Vendor_ID = V.Vendor_ID inner join BUS_DRIVER BD on LR.Bus_Driver_ID = "
                + " BD.Bus_Driver_ID  inner join DRIVER D on BD.Driver_ID = D.Driver_ID inner join SHIFT SF on LR.Shift_ID = SF.Shift_ID  Where V.[Vendor_ID]= '" + usr.Vendor_ID + "' order by  LR.Legal_Revision_ID desc");
               
            }
            else if(Vendor != "ALL" && Status == "ALL")
            {
                List<string> columns = new List<string>();
                columns.Add("Legal_Revision_ID");
                ReturnTable = GenericClass.SQLSelectObj(new Legal_Revision(), mappingQueryName: "Legal_Revision_ID_GenerateTable", WhereClause: "where V.Vendor_ID ='" + Vendor + "'", customSelect: columns, OrderByClause: " order by  Legal_Revision.Legal_Revision_ID desc");
            }   
            
            else if (Vendor == "ALL" && Status != "ALL")
            {
                List<string> columns = new List<string>();
                columns.Add("Legal_Revision_ID");
                ReturnTable = GenericClass.SQLSelectObj(new Legal_Revision(), mappingQueryName: "Legal_Revision_ID_GenerateTable", WhereClause: "where LEGAL_REVISION.Status ='" + Status + "'", customSelect: columns, OrderByClause: " order by  Legal_Revision.Legal_Revision_ID desc");
            }
            else if(Vendor != "ALL" && Status != "ALL") 
            {
                List<string> columns = new List<string>();
                columns.Add("Legal_Revision_ID");
                ReturnTable = GenericClass.SQLSelectObj(new Legal_Revision(), mappingQueryName: "Legal_Revision_ID_GenerateTable_Diferent_Vendor", WhereClause: "where LEGAL_REVISION.Status ='" + Status + "' and V.Vendor_ID = '" + Vendor + "'", customSelect: columns, OrderByClause: " order by  Legal_Revision.Legal_Revision_ID desc");
            }

            

            return ReturnTable;
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
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
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
                DataTable dt = GenerateTable(DD_Grid_Vendor.SelectedValue.ToString(), DD_Grid_Status.SelectedValue.ToString());
                DataView sortedView = new DataView(dt);
                sortedView.Sort = e.SortExpression + " " + sortingDirection;
                Session["objects"] = sortedView;
                GridView1.DataSource = sortedView;
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btSearchCH1_Click(object sender, EventArgs e)
        {
            search();
        }

        private void search()
        {
            try
            {
                DataTable dtNew = new DataTable();
                GridView1.PageIndex = 0;
                string searchTerm = tbSearchCH1.Text.ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    FillGrid();
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
                        if (row["Driver_and_bus"].ToString().ToLower().Contains(searchTerm) || row["Shift"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtNew.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    GridView1.DataSource = dtNew;
                    ViewState["searchResults"] = dtNew;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void btHelp_Click(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("./Login.aspx");
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Revisiones')", true);
        }
        
    }
}