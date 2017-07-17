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
using System.Configuration;
namespace BusManagementSystem.BlackList
{
    public partial class BlackList : System.Web.UI.Page
    {
        private bool SaveFlag;

        private AddPrivilege privilege = new AddPrivilege();

        String language = null;
        string msg_Blacklist_SelecVendor;
        string msg_Blacklist_SelecShift;
        string msg_Blacklist_SelecBusDriver;
        string msg_Blacklist_SelecDriver;
        string msg_Blacklist_SelecBus;
        string msg_Blacklist_CommstsEmpty;
        string msg_Blacklist_UpdateSuccess;
        string msg_Blacklist_InsertSuccess;
        string msg_BlackList_BusDriver;



        protected void Page_Init(object sender, EventArgs e)
        {

            if (Session["C_USER"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            //Load buttons to hide or unhide for privileges
            privilege = new AddPrivilege("10001", btAdd, btReset, btSave, null, btExcel);

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
            msg_BlackList_BusDriver = (string)GetGlobalResourceObject(language, "msg_BlackList_BusDriver");
            msg_Blacklist_SelecVendor = (string)GetGlobalResourceObject(language, "msg_Blacklist_SelecVendor");
            msg_Blacklist_SelecShift = (string)GetGlobalResourceObject(language, "msg_Blacklist_SelecShift");
            msg_Blacklist_SelecBusDriver = (string)GetGlobalResourceObject(language, "msg_Blacklist_SelecBusDriver");
            msg_Blacklist_SelecDriver = (string)GetGlobalResourceObject(language, "msg_Blacklist_SelecDriver");
            msg_Blacklist_SelecBus = (string)GetGlobalResourceObject(language, "msg_Blacklist_SelecBus");
            msg_Blacklist_CommstsEmpty = (string)GetGlobalResourceObject(language, "msg_Blacklist_CommstsEmpty");
            msg_Blacklist_UpdateSuccess = (string)GetGlobalResourceObject(language, "msg_Blacklist_UpdateSuccess");
            msg_Blacklist_InsertSuccess = (string)GetGlobalResourceObject(language, "msg_Blacklist_InsertSuccess");

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("~/Login.aspx");

            }

            Users usr = (Users)(Session["C_USER"]);

            if (!usr.Profile.ToUpper().Equals("INTERNAL"))
            {
                Response.Redirect("~/MenuPortal.aspx");
            }

            //Fill datatable with privileges
            privilege.DtPrivilege = privilege.GetPrivilege(this.Page.Title, usr);



            if (!this.IsPostBack)
            {

                //from language

                ViewState["UpdateNinsertflag"] = null;
                PrivilegeAndProcessFlow("00001");

                Fill_Grid_Vendor_DropDown();
                DD_Grid_Vendor.SelectedIndex = 0;

                ViewState["searchStatus"] = false;
                ViewState["isSorting"] = false;
            }

            applyLanguage();
            if (!string.IsNullOrEmpty(language))
                translateMessages(language);

            if ((bool)ViewState["searchStatus"] == false && (bool)ViewState["isSorting"] == false)
                Fill_Grid(null, null);
        }

        public void PrivilegeAndProcessFlow(string turnOnOffBNuttons)
        {
            privilege.applyPrivilege(turnOnOffBNuttons, GridViewBlackList);
            ProcessFlow();

        }

        protected void ProcessFlow()
        {
            if (this.btAdd.Visible == false && this.btSave.Visible == false)
            {


                btAdd.Visible = false;

                DD_Category.Visible = true;
                DD_Category.Enabled = true;
                DD_Dinamic1.Enabled = false;
                DD_Dinamic2.Enabled = false;
                DD_Dinamic2.Visible = false;
                btReset.Enabled = false;
                btReset.Visible = false;
                btSave.Enabled = false;
                btSave.Visible = false;
                DD_Vendor.Enabled = false;
                txt_Comments.Text = null;
                txt_Comments.Enabled = false;
                txt_Reason.Text = null;
                txt_Reason.Enabled = false;
                DD_Dinamic_Label1.Enabled = false;
                DD_Dinamic_Label1.Visible = false;
                DD_Dinamic_Label2.Enabled = false;

                DD_Dinamic_Label1.Text = null;
                DD_Dinamic_Label2.Text = null;
                DD_Grid_Vendor.Enabled = true;
                DD_Category_Grid.Enabled = true;

                form_Profilepicture.ImageUrl = "~/images/image-not-found.jpg";

                GridViewBlackList.Columns[0].Visible = true;

                privilege.applyPrivilege("00001", GridViewBlackList);

            }
            if (this.btAdd.Visible == true)
            {

                btAdd.Enabled = true;

                btAdd.Visible = true;
            }

            if (this.btSave.Visible == true)
            {
                if (txt_Reason.Text == string.Empty)
                {
                    DD_Vendor.Enabled = true;
                }
                else
                {
                    DD_Vendor.Enabled = false;
                }

                DD_Category.Enabled = false;
                btAdd.Enabled = false;
                btAdd.Visible = false;
                btReset.Enabled = true;
                btReset.Visible = true;
                btSave.Enabled = true;
                btSave.Visible = true;
                DD_Grid_Vendor.Enabled = false;
                DD_Category_Grid.Enabled = false;
                txt_Comments.Enabled = true;
                txt_Reason.Enabled = true;
                GridViewBlackList.Columns[0].Visible = false;
            }

        }

        protected void bt_Add_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["UpdateNinsertflag"] = true;
                Fill_Vendor_DropDwon();
                PrivilegeAndProcessFlow("01101");
                btSave.Enabled = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
                SaveFlag = false;
                GridViewBlackList.Enabled = false;
                tbSearchCH1.Enabled = false;
                btSearchCH1.Enabled = false;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void DD_Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
                if (DD_Category.SelectedValue != "Select")
                {
                    btSave.Enabled = false;
                    PrivilegeAndProcessFlow("10001");

                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }



        }


        protected void Fill_Dinamyc1(DataTable DT)
        {
            try
            {
                if (DD_Category.SelectedValue.ToString() == "DRIVER")
                {
                    ListItem li = new ListItem("Select", "Select");
                    DD_Dinamic1.Enabled = true;
                    DD_Dinamic1.Visible = true;
                    DD_Dinamic1.Items.Clear();
                    DD_Dinamic1.DataSource = DT;
                    DD_Dinamic1.DataValueField = "Driver_ID";
                    DD_Dinamic1.DataTextField = "Name";
                    DD_Dinamic1.DataBind();
                    DD_Dinamic1.Items.Insert(0, li);
                    DD_Dinamic1.SelectedIndex = 0;
                    DD_Dinamic_Label1.Visible = true;

                    DD_Dinamic_Label1.Text = languaje("lbl_BlackList_DinamicTag2");
                }
                else if (DD_Category.SelectedValue.ToString() == "BUS")
                {
                    ListItem li = new ListItem("Select", "Select");
                    DD_Dinamic1.Enabled = true;
                    DD_Dinamic1.Visible = true;
                    DD_Dinamic1.Items.Clear();
                    DD_Dinamic1.DataSource = DT;
                    DD_Dinamic1.DataValueField = "Bus_ID";
                    DD_Dinamic1.DataTextField = "Bus_ID";
                    DD_Dinamic1.DataBind();
                    DD_Dinamic1.Items.Insert(0, li);
                    DD_Dinamic1.SelectedIndex = 0;
                    DD_Dinamic_Label1.Visible = true;
                    DD_Dinamic_Label1.Text = languaje("lbl_BlackList_DinamicTag");


                }
                else
                {

                    //error
                }


                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected string languaje(string Key)
        {
            string value;
            String Language = null;
            Users usr = (Users)(Session["C_USER"]);
            functions func = new functions();
            if (Session["Language"] != null)
            {
                Language = Session["Language"].ToString();
            }
            else
            {
                Language = functions.GetLanguage(usr);
            }


            value = (string)GetGlobalResourceObject(Language, Key);
            return value;
        }



        protected void Fill_Vendor_DropDwon()
        {
            try
            {
                List<string> Columns = new List<string>();
                Columns.Add("Name");
                Columns.Add("Vendor_ID");
                ListItem li = new ListItem("Select", "Select");
                DD_Vendor.Items.Clear();
                DataTable dt = GenericClass.SQLSelectObj(new Vendor(), customSelect: Columns, mappingQueryName: "Name_distinct");
                DD_Vendor.DataSource = dt;
                DD_Vendor.DataValueField = "Vendor_ID";
                DD_Vendor.DataTextField = "Name";
                DD_Vendor.DataBind();
                DD_Vendor.Items.Insert(0, li);
                DD_Vendor.Enabled = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        protected void Fill_Grid_Vendor_DropDown()
        {
            try
            {
                List<string> Columns = new List<string>();
                Columns.Add("Name");
                Columns.Add("Vendor_ID");
                ListItem li = new ListItem("Select", "Select");
                DD_Grid_Vendor.Items.Clear();
                DataTable dt = GenericClass.SQLSelectObj(new Vendor(), customSelect: Columns, mappingQueryName: "Name_distinct");
                DD_Grid_Vendor.DataSource = dt;
                DD_Grid_Vendor.DataValueField = "Vendor_ID";
                DD_Grid_Vendor.DataTextField = "Name";
                DD_Grid_Vendor.DataBind();
                DD_Grid_Vendor.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void Fill_Shift_DropDown()
        {
            try
            {
                List<string> columns = new List<string>();
                columns.Add("Shift_ID");
                columns.Add("Name");
                ListItem li = new ListItem("Select", "Select");
                DD_Dinamic1.Enabled = true;
                DD_Dinamic1.Visible = true;
                DataTable dtShift = GenericClass.SQLSelectObj(new Shift(), mappingQueryName: "Catalog", customSelect: columns);
                if (dtShift.Rows.Count != 0)
                {
                    DD_Dinamic1.Items.Clear();
                    DD_Dinamic1.DataSource = dtShift;
                    DD_Dinamic1.DataValueField = "SHIFT_ID";
                    DD_Dinamic1.DataTextField = "NAME";
                    DD_Dinamic1.DataBind();
                    DD_Dinamic1.Items.Insert(0, li);
                    DD_Dinamic1.SelectedIndex = 0;
                    DD_Dinamic_Label1.Visible = true;
                    DD_Dinamic_Label1.Text = languaje("lbl_BlackList_DinamicTag3");
                }

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }


        protected DataTable Return_Table(string category, string vendor, string shift)
        {
            DataTable DT_Return = new DataTable();
            try
            {

                if (category == "DRIVER")
                {
                    List<string> Columns = new List<string>();
                    Columns.Add("Driver_ID");
                    Columns.Add("Name");
                    DT_Return = GenericClass.SQLSelectObj(new Driver(), customSelect: Columns, WhereClause: "Where Vendor_ID ='" + vendor + "'");
                }
                else if (category == "BUS")
                {
                    List<string> Columns = new List<string>();
                    Columns.Add("Bus_ID");
                    Columns.Add("Bus_ID");
                    DT_Return = GenericClass.SQLSelectObj(new Bus(), customSelect: Columns, WhereClause: "Where Vendor_ID ='" + vendor + "'");
                }
                else if (category == "BUS_DRIVER")
                {
                    List<string> Columns = new List<string>();
                    Columns.Add("");
                    DT_Return = GenericClass.SQLSelectObj(new Bus_Driver(), WhereClause: "where Bus_Driver.Vendor_ID =  '" + vendor
                        + "' and Bus_Driver.Shift_ID = '" + shift + "'", mappingQueryName: "DST-Bus_Driver_ID");
                }

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            return DT_Return;

        }

        protected void DD_Vendor_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                btSave.Enabled = false;
                if (DD_Category.SelectedValue.ToString() == "BUS_DRIVER")
                {

                    Fill_Shift_DropDown();

                    if (DD_Dinamic2.Enabled == true && DD_Dinamic1.Enabled == true)
                    {
                        DD_Dinamic_Label1.Visible = false;
                        DD_Dinamic_Label1.Text = string.Empty;
                        DD_Dinamic1.Visible = false;
                        DD_Dinamic1.Enabled = false;
                        DD_Dinamic1.Items.Clear();
                        DD_Dinamic_Label2.Visible = false;
                        DD_Dinamic_Label2.Text = string.Empty;
                        DD_Dinamic2.Visible = false;
                        DD_Dinamic2.Enabled = false;
                        DD_Dinamic2.Items.Clear();
                    }
                }
                else
                {
                    Fill_Dinamyc1(Return_Table(DD_Category.SelectedValue.ToString(), DD_Vendor.SelectedValue.ToString(), null));
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void DD_Dinamic2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DD_Dinamic2.SelectedValue.ToString() != "Select" && DD_Dinamic1.SelectedValue.ToString() != "Select" && DD_Vendor.SelectedValue.ToString() != "Select")
                {
                    btSave.Enabled = true;
                    int positionDriver = DD_Dinamic2.SelectedValue.IndexOf("-");

                    string driverID = DD_Dinamic2.SelectedValue.Substring(positionDriver + 1, DD_Dinamic2.SelectedValue.Length - (3 + positionDriver));

                    profilePicture(driverID);
                }
                else
                {
                    btSave.Enabled = false;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }


        }

        protected void DD_Dinamic1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DD_Dinamic1.SelectedValue.ToString() != "Select" || DD_Dinamic2.SelectedValue.ToString() != "")
                {
                    if (DD_Category.SelectedValue.ToString() != "BUS_DRIVER")
                    {
                        btSave.Enabled = true;
                        if (DD_Category.SelectedValue.ToString() == "DRIVER")
                        {
                            profilePicture(DD_Dinamic1.SelectedValue);
                        }
                    }
                }
                else
                {
                    btSave.Enabled = false;
                }
                if (DD_Category.SelectedValue.ToString() == "BUS_DRIVER")
                {

                    List<string> columns = new List<string>();
                    columns.Add("Shift_ID");
                    columns.Add("Name");
                    ListItem li = new ListItem("Select", "Select");
                    if (DD_Dinamic2.Enabled == false && DD_Dinamic2.Items.Count == 0)
                    {
                        DD_Dinamic2.Enabled = true;
                        DD_Dinamic2.Visible = true;
                        DD_Dinamic2.Items.Clear();
                        DD_Dinamic2.DataSource = Return_Table(DD_Category.SelectedValue.ToString(), DD_Vendor.SelectedValue.ToString(), DD_Dinamic1.SelectedValue.ToString());
                        DD_Dinamic2.DataValueField = "Bus_Driver_ID";
                        DD_Dinamic2.DataTextField = "bus_driver";
                        DD_Dinamic2.DataBind();
                        DD_Dinamic2.Items.Insert(0, li);
                        DD_Dinamic2.SelectedIndex = 0;
                        txt_Reason.Enabled = true;
                        txt_Comments.Enabled = true;
                        DD_Dinamic_Label2.Visible = true;
                        DD_Dinamic_Label2.Text = msg_BlackList_BusDriver;



                    }
                    else
                    {
                        btSave.Enabled = false;
                        DD_Dinamic_Label2.Visible = false;
                        DD_Dinamic_Label2.Text = string.Empty;
                        DD_Dinamic2.Visible = false;
                        DD_Dinamic2.Enabled = false;
                        DD_Dinamic2.Items.Clear();

                    }





                }

                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void bt_Reset_Click(object sender, EventArgs e)
        {
            try
            {
                Clear_Items();
                Fill_Grid(null, null);
                PrivilegeAndProcessFlow("00001");
                SaveFlag = false;
                btSearchCH1.Enabled = true;
                tbSearchCH1.Enabled = true;
                GridViewBlackList.Enabled = true;
                btReset.Enabled = false;
                txt_Comments.Enabled = false;
                txt_Reason.Enabled = false;
                lbl_BlackList_BlackList_ID.Text = "0";
                lbl_BlackList_BlackList_ID.Visible = false;
                lbl_BlackList_Form_vendor.Visible = false;

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        protected void Clear_Items()
        {
            try
            {

                DD_Category.SelectedIndex = 0;
                DD_Vendor.Items.Clear();
                DD_Dinamic1.Items.Clear();
                DD_Dinamic2.Items.Clear();
                DD_Category_Grid.SelectedIndex = -1;
                DD_Grid_Vendor.SelectedIndex = -1;
                txt_Comments.Text = null;
                txt_Reason.Text = null;


            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void bt_Save_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {

                    SaveFlag = true;
                    if (DD_Category.SelectedValue.ToString() == "BUS_DRIVER")
                    {
                        if (DD_Vendor.SelectedValue.ToString() == "Select")
                        {
                            functions.ShowMessage(this, this.GetType(), msg_Blacklist_SelecVendor, MessageType.Error);
                            SaveFlag = false;
                        }
                        else if (DD_Dinamic1.SelectedValue.ToString() == "Select")
                        {
                            functions.ShowMessage(this, this.GetType(), msg_Blacklist_SelecShift, MessageType.Error);
                            SaveFlag = false;
                        }
                        else if (DD_Dinamic2.SelectedValue.ToString() == "Select")
                        {
                            functions.ShowMessage(this, this.GetType(), msg_Blacklist_SelecBusDriver, MessageType.Error);
                            SaveFlag = false;
                        }

                    }
                    else if (DD_Category.SelectedValue.ToString() == "DRIVER")
                    {
                        if (DD_Vendor.SelectedValue.ToString() == "Select")
                        {
                            functions.ShowMessage(this, this.GetType(), msg_Blacklist_SelecVendor, MessageType.Error);
                            SaveFlag = false;
                        }
                        else if (DD_Dinamic1.SelectedValue.ToString() == "Select")
                        {

                            functions.ShowMessage(this, this.GetType(), msg_Blacklist_SelecDriver, MessageType.Error);
                            SaveFlag = false;
                        }

                    }
                    else if (DD_Category.SelectedValue.ToString() == "BUS")
                    {
                        if (DD_Vendor.SelectedValue.ToString() == "Select")
                        {
                            functions.ShowMessage(this, this.GetType(), msg_Blacklist_SelecVendor, MessageType.Error);
                            SaveFlag = false;
                        }
                        else if (DD_Dinamic1.SelectedValue.ToString() == "Select")
                        {
                            functions.ShowMessage(this, this.GetType(), msg_Blacklist_SelecBus, MessageType.Error);
                            SaveFlag = false;
                        }


                    }
                    if (Convert.ToBoolean(ViewState["UpdateNinsertflag"]) == true & SaveFlag)
                    {

                        Insert_Into_BlackList();
                        ViewState["UpdateNinsertflag"] = null;
                        lbl_BlackList_BlackList_ID.Text = string.Empty;
                    }
                    else if (Convert.ToBoolean(ViewState["UpdateNinsertflag"]) == false & SaveFlag)
                    {

                        Update();
                        ViewState["UpdateNinsertflag"] = null;
                        lbl_BlackList_BlackList_ID.Text = string.Empty;
                    }
                    if (SaveFlag == false)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);

                    }
                    else
                    {
                        PrivilegeAndProcessFlow("00001");

                    }

                }
                catch (Exception ex)
                {
                    functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
                }
            }
            else
            {
                functions.ShowMessage(this, this.GetType(), msg_Blacklist_CommstsEmpty, MessageType.Error);

            }

            

        }
        protected void Update()
        {
            try
            {
                Blacklist BlackList_Old = new Blacklist();
                DataTable DT_BlackList_Old = GenericClass.SQLSelectObj(new Blacklist(), WhereClause: "where BlackList_ID =" + lbl_BlackList_BlackList_ID.Text);
                //BlackList_Old.BlackList_ID = Convert.ToInt32(DT_BlackList_Old.Rows[0]["BlackList_ID"]);
                BlackList_Old.BlackList_Date = Convert.ToDateTime(DT_BlackList_Old.Rows[0]["BlackList_Date(M/D/Y)"]);
                //BlackList_Old.Bus_ID = Convert.ToString(DT_BlackList_Old.Rows[0]["Bus_ID"]);
                //BlackList_Old.Driver_ID = Convert.ToString(DT_BlackList_Old.Rows[0]["Driver_ID"]);
                //BlackList_Old.Vendor_ID = Convert.ToString(DT_BlackList_Old.Rows[0]["Vendor_ID"]);
                BlackList_Old.Reason = Convert.ToString(DT_BlackList_Old.Rows[0]["Reason"]);
                BlackList_Old.Comments = Convert.ToString(DT_BlackList_Old.Rows[0]["Comments"]);

                Blacklist BlackList_Current = new Blacklist();
                //BlackList_Current.Bus_ID = Convert.ToString(DT_BlackList_Old.Rows[0]["Bus_ID"]);
                //BlackList_Current.Driver_ID = Convert.ToString(DT_BlackList_Old.Rows[0]["Driver_ID"]);
                //BlackList_Current.Vendor_ID = Convert.ToString(DT_BlackList_Old.Rows[0]["Vendor_ID"]);
                //BlackList_Current.BlackList_ID = Convert.ToInt32(DT_BlackList_Old.Rows[0]["BlackList_ID"]);
                BlackList_Current.BlackList_Date = DateTime.Now;

                BlackList_Current.Comments = txt_Comments.Text;
                BlackList_Current.Reason = txt_Reason.Text;
                GenericClass.SQLUpdateObj(new Blacklist(), adminApprove.compareObjects(BlackList_Old, BlackList_Current, ""), "Where BlackList_ID = " + lbl_BlackList_BlackList_ID.Text);

                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", false);
                Clear_Items();
                Fill_Grid(null, null);
                lbl_BlackList_BlackList_ID.Text = "0";
                lbl_BlackList_BlackList_ID.Visible = false;
                lbl_BlackList_Form_vendor.Visible = false;


                functions.ShowMessage(this, this.GetType(), msg_Blacklist_UpdateSuccess, MessageType.Success);
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        protected void Insert_Into_BlackList()
        {
            try
            {
                Blacklist BL = new Blacklist();
                if (DD_Category.SelectedValue.ToString() == "BUS_DRIVER")
                {
                    List<string> Columns = new List<string>();
                    Columns.Add("Bus_ID");
                    Columns.Add("Driver_ID");
                    BL.BlackList_Date = DateTime.Now;
                    BL.Vendor_ID = DD_Vendor.SelectedValue.ToString();
                    DataTable Bus_And_Driver = GenericClass.SQLSelectObj(new Bus_Driver(), WhereClause: "Where Bus_Driver_ID = '" + DD_Dinamic2.SelectedValue.ToString() + "'", customSelect: Columns);
                    BL.Bus_ID = Bus_And_Driver.Rows[0]["Bus_ID"].ToString();
                    BL.Driver_ID = Bus_And_Driver.Rows[0]["Driver_ID"].ToString();
                    BL.Comments = txt_Comments.Text;
                    BL.Reason = txt_Reason.Text;

                }
                else
                {
                    BL.BlackList_Date = DateTime.Now;
                    BL.Vendor_ID = DD_Vendor.SelectedValue.ToString();
                    BL.Comments = txt_Comments.Text;
                    BL.Reason = txt_Reason.Text;
                    if (DD_Category.SelectedValue.ToString() == "BUS")
                    {
                        BL.Bus_ID = DD_Dinamic1.SelectedValue.ToString();
                    }
                    else if (DD_Category.SelectedValue.ToString() == "DRIVER")
                    {
                        BL.Driver_ID = DD_Dinamic1.SelectedValue.ToString();
                    }

                }
                GenericClass.SQLInsertObj(BL);
                PrivilegeAndProcessFlow("00001");
                Clear_Items();
                Fill_Grid(null, null);
                functions.ShowMessage(this, this.GetType(), msg_Blacklist_InsertSuccess, MessageType.Success);
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        protected String Change(string Variable)
        {
            try
            {
                if (Variable == "Select")
                {
                    Variable = null;
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

            return Variable;
        }

        protected DataTable Fill_Grid(string category, string vendor)
        {
            DataTable DT_Retunr = new DataTable();
            try
            {


                category = Change(category);
                vendor = Change(vendor);
                if (category == null && vendor == null)
                {

                    DT_Retunr = GenericClass.GetCustomData("select  B.BlackList_ID , convert(varchar(10), B.BlackList_Date,101) BlackList_Date, V.Name Vendor, B.Bus_ID, B.Driver_ID,Driver.Name Driver, B.Reason, B.Comments "
                        + "from  BLACKLIST B inner join VENDOR V on B.Vendor_ID = V.Vendor_ID "
                        + "Left Join Driver on Driver.Driver_ID=B.Driver_ID"
                        + "  order by B.BlackList_ID desc");

                }
                else if (category == null && vendor != null)
                {

                    DT_Retunr = GenericClass.GetCustomData("select  B.BlackList_ID ,convert(varchar(10), B.BlackList_Date,101) BlackList_Date, V.Name Vendor, B.Bus_ID, B.Driver_ID,Driver.Name Driver, B.Reason, B.Comments "
                        + "from  BLACKLIST B inner join VENDOR V on B.Vendor_ID = V.Vendor_ID "
                        + "Left Join Driver on Driver.Driver_ID=B.Driver_ID"
                        + " where  V.Vendor_ID = '" + vendor + "'"
                        + " order by B.BlackList_ID desc");

                }
                else if (vendor == null && category != null)
                {

                    DT_Retunr = GenericClass.GetCustomData("select  B.BlackList_ID , convert(varchar(10), B.BlackList_Date,101) BlackList_Date, V.Name Vendor, B.Bus_ID, B.Driver_ID,Driver.Name Driver, B.Reason, B.Comments"
                        + " from  BLACKLIST B inner join VENDOR V on B.Vendor_ID = V.Vendor_ID "
                        + "Left Join Driver on Driver.Driver_ID=B.Driver_ID"
                        + " where" + category
                        + " order by B.BlackList_ID desc");


                }
                else if (category != null && vendor != null)
                {

                    DT_Retunr = GenericClass.GetCustomData("select  B.BlackList_ID , convert(varchar(10), B.BlackList_Date,101) BlackList_Date, V.Name Vendor, B.Bus_ID, B.Driver_ID,Driver.Name Driver, B.Reason, B.Comments"
                        + " from  BLACKLIST B inner join VENDOR V on B.Vendor_ID = V.Vendor_ID "
                        + "Left Join Driver on Driver.Driver_ID=B.Driver_ID"
                        + " where" + category + " and V.Vendor_ID = '" + vendor + "'"
                        + " order by B.BlackList_ID desc");

                }
                GridViewBlackList.DataSource = DT_Retunr;
                GridViewBlackList.PageSize = 15;
                ViewState["backupData"] = DT_Retunr;
                GridViewBlackList.DataBind();

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }



            return DT_Retunr;
        }

        protected void GridViewBlackList_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridViewBlackList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //ViewState["backupData"]
            try
            {


                GridViewBlackList.PageIndex = e.NewPageIndex;
                if ((bool)ViewState["searchStatus"] == true)
                { GridViewBlackList.DataSource = ViewState["searchResults"]; }
                if ((bool)ViewState["isSorting"] == true)
                { GridViewBlackList.DataSource = Session["Objects_BlackList"]; }
                GridViewBlackList.DataBind();
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void DD_Category_Grid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string category = Change_Query();
                if (DD_Grid_Vendor.SelectedValue.ToString() == "Select")
                {
                    Fill_Grid(category, null);

                }
                else if (DD_Grid_Vendor.SelectedValue.ToString() == "Select" && DD_Category_Grid.SelectedValue.ToString() == "Select")
                {
                    Fill_Grid(null, null);
                }
                else
                {
                    Fill_Grid(category, DD_Grid_Vendor.SelectedValue.ToString());

                }
                tbSearchCH1.Text = string.Empty;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }


        }
        protected string Change_Query()
        {
            string category = null;
            try
            {
                if (DD_Category_Grid.SelectedValue.ToString() == "BUS_DRIVER")
                {
                    category = " B.Bus_ID is not null and B.Driver_ID is not null";

                }
                else if (DD_Category_Grid.SelectedValue.ToString() == "BUS")
                {
                    category = " B.Bus_ID is not null and B.Driver_ID is null";
                }
                else if (DD_Category_Grid.SelectedValue.ToString() == "DRIVER")
                {
                    category = " B.Driver_ID is not null and B.Bus_ID is null";
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

            return category;

        }

        protected void DD_Grid_Vendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DD_Category_Grid.SelectedValue.ToString() == "Select" && DD_Grid_Vendor.SelectedValue.ToString() != "Select")
                {
                    Fill_Grid(null, DD_Grid_Vendor.SelectedValue.ToString());
                }
                else if (DD_Grid_Vendor.SelectedValue.ToString() == "Select" && DD_Category_Grid.SelectedValue.ToString() == "Select")
                {
                    Fill_Grid(null, null);
                }
                else if (DD_Grid_Vendor.SelectedValue.ToString() == "Select" && DD_Category_Grid.SelectedValue.ToString() != "Select")
                {
                    Fill_Grid(Change_Query(), null);
                }
                else
                {
                    Fill_Grid(Change_Query(), DD_Grid_Vendor.SelectedValue.ToString());
                }
                tbSearchCH1.Text = string.Empty;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }


        }

        protected void GridViewBlackList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvRow = GridViewBlackList.SelectedRow;
                Fill_Formulario(Convert.ToInt32(GridViewBlackList.Rows[gvRow.RowIndex].Cells[1].Text));
                lbl_BlackList_BlackList_ID.Visible = false;
                lbl_BlackList_Form_vendor.Visible = true;
                PrivilegeAndProcessFlow("01101");
                GridViewBlackList.Rows[0].Visible = false;
                GridViewBlackList.Enabled = false;
                tbSearchCH1.Enabled = false;
                btSearchCH1.Enabled = false;

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }


        }
        protected void Fill_Formulario(int BlackListid)
        {
            try
            {
                DataTable dt = GenericClass.SQLSelectObj(new Blacklist(), WhereClause: "where BlackList_ID = " + BlackListid);
                string a = dt.Rows[0]["Driver_ID"].ToString();
                string b = dt.Rows[0]["Bus_ID"].ToString();
                lbl_BlackList_BlackList_ID.Text = Convert.ToString(BlackListid);
                if (dt.Rows[0]["Bus_ID"].ToString() != null && dt.Rows[0]["Driver_ID"].ToString() == string.Empty)
                {

                    List<string> columns = new List<string>();
                    columns.Add("Name");
                    columns.Add("Vendor_ID");
                    DataTable dt_vendor = GenericClass.SQLSelectObj(new Vendor(), WhereClause: "where Vendor_ID = '" + dt.Rows[0]["Vendor_ID"].ToString() + "'", customSelect: columns);
                    DD_Vendor.DataSource = dt_vendor;
                    DD_Vendor.DataValueField = "Vendor_ID";
                    DD_Vendor.DataTextField = "Name";
                    DD_Vendor.DataBind();
                    DD_Dinamic1.DataSource = dt;
                    DD_Dinamic1.DataValueField = "Bus_ID";
                    DD_Dinamic1.DataTextField = "Bus_ID";
                    DD_Dinamic1.DataBind();
                    DD_Dinamic_Label1.Text = languaje("lbl_BlackList_DinamicTag1");
                    DD_Dinamic_Label1.Visible = true;
                    txt_Comments.Text = dt.Rows[0]["Comments"].ToString();
                    txt_Reason.Text = dt.Rows[0]["Reason"].ToString();


                }
                else if (dt.Rows[0]["Driver_ID"].ToString() != null && dt.Rows[0]["Bus_ID"].ToString() == string.Empty)
                {
                    List<string> columnsVendor = new List<string>();
                    columnsVendor.Add("Name");
                    columnsVendor.Add("Vendor_ID");
                    List<string> columnsDriver = new List<string>();
                    columnsDriver.Add("Name");
                    columnsDriver.Add("Driver_ID");

                    DataTable dt_vendor = GenericClass.SQLSelectObj(new Vendor(), WhereClause: "where Vendor_ID = '" + dt.Rows[0]["Vendor_ID"].ToString() + "'", customSelect: columnsVendor);
                    DD_Vendor.DataSource = dt_vendor;
                    DD_Vendor.DataValueField = "Vendor_ID";
                    DD_Vendor.DataTextField = "Name";
                    DD_Vendor.DataBind();
                    DataTable dt_Driver = GenericClass.SQLSelectObj(new Driver(), WhereClause: "where Driver_ID = '" + dt.Rows[0]["Driver_ID"].ToString() + "'", customSelect: columnsDriver);

                    DD_Dinamic1.DataSource = dt_Driver;
                    DD_Dinamic1.DataValueField = "Driver_ID";
                    DD_Dinamic1.DataTextField = "Name";
                    DD_Dinamic1.DataBind();
                    DD_Dinamic_Label1.Text = languaje("lbl_BlackList_DinamicTag2");
                    DD_Dinamic_Label1.Visible = true;
                    txt_Comments.Text = dt.Rows[0]["Comments"].ToString();
                    txt_Reason.Text = dt.Rows[0]["Reason"].ToString();

                    profilePicture(dt.Rows[0]["Driver_ID"].ToString());



                }
                else if (dt.Rows[0]["Bus_ID"].ToString() != null && dt.Rows[0]["Driver_ID"] != null)
                {
                    List<string> columns = new List<string>();
                    columns.Add("Name");
                    columns.Add("Vendor_ID");
                    List<string> columnsDriver = new List<string>();
                    columnsDriver.Add("Name");
                    columnsDriver.Add("Driver_ID");
                    DataTable dt_vendor = GenericClass.SQLSelectObj(new Vendor(), WhereClause: "where Vendor_ID = '" + dt.Rows[0]["Vendor_ID"].ToString() + "'", customSelect: columns);
                    DD_Vendor.DataSource = dt_vendor;
                    DD_Vendor.DataValueField = "Vendor_ID";
                    DD_Vendor.DataTextField = "Name";
                    DD_Vendor.DataBind();

                    DataTable dt_Driver = GenericClass.SQLSelectObj(new Driver(), WhereClause: "where Driver_ID = '" + dt.Rows[0]["Driver_ID"].ToString() + "'", customSelect: columnsDriver);
                    DataTable DT_Bus_Driver = new DataTable();
                    DT_Bus_Driver.Clear();
                    DT_Bus_Driver.Columns.Add("Bus_Driver");
                    DataRow _R = DT_Bus_Driver.NewRow();
                    _R["Bus_Driver"] = dt.Rows[0]["Bus_ID"].ToString() + "/" + dt_Driver.Rows[0]["Name"].ToString();
                    DT_Bus_Driver.Rows.Add(_R);
                    DD_Dinamic1.DataSource = DT_Bus_Driver;
                    DD_Dinamic1.DataValueField = "Bus_Driver";
                    DD_Dinamic1.DataTextField = "Bus_Driver";
                    DD_Dinamic1.DataBind();
                    DD_Dinamic_Label1.Text = languaje("lbl_BlackList_DinamicTag3");
                    DD_Dinamic_Label1.Visible = true;
                    txt_Comments.Text = dt.Rows[0]["Comments"].ToString();
                    txt_Reason.Text = dt.Rows[0]["Reason"].ToString();

                    profilePicture(dt.Rows[0]["Driver_ID"].ToString());

                }

                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);


                SaveFlag = true;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }


        }

        protected void GridViewBlackList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 5)
            {
                e.Row.Cells[5].Visible = false;
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
        protected void GridViewBlackList_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                ViewState["isSorting"] = true;
                ViewState["searchStatus"] = false;
                string vendor = Change(DD_Grid_Vendor.SelectedValue.ToString());
                string category = Change_Query();
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

                //DataTable dt = Fill_Grid(category, vendor);
                DataView sortedView = new DataView(ViewState["backupData"] as DataTable);
                sortedView.Sort = e.SortExpression + " " + sortingDirection;
                Session["Objects_BlackList"] = sortedView;
                GridViewBlackList.DataSource = sortedView;

                GridViewBlackList.DataBind();

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
                Session["Excel"] = null;
                Session["name"] = string.Empty;

                if (Session["C_USER"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                Users usr = (Users)(Session["C_USER"]);
                DataTable dtLoad = Fill_Grid(Change_Query(), DD_Grid_Vendor.SelectedValue.ToString());
                Session["Excel"] = dtLoad;
                Session["name"] = "BlackList";
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

        private void search()
        {
            try
            {
                DataTable dtNew = new DataTable();
                GridViewBlackList.PageIndex = 0;
                string searchTerm = tbSearchCH1.Text.ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {

                    Fill_Grid(Change_Query(), DD_Grid_Vendor.SelectedValue.ToString());
                    dtNew.Clear();
                    ViewState["searchStatus"] = false;
                    ViewState["isSorting"] = false;
                    ViewState["searchResults"] = null;

                }
                else
                {
                    ViewState["searchStatus"] = true;
                    ViewState["isSorting"] = false;
                    //always check if the viewstate exists before using it
                    if (ViewState["backupData"] == null)
                        ViewState["backupData"] = Fill_Grid(null, null);

                    //cast the viewstate as a datatable
                    DataTable dt = ViewState["backupData"] as DataTable;

                    //make a clone of the datatable
                    dtNew = dt.Clone();

                    //search the datatable for the correct fields
                    foreach (DataRow row in dt.Rows)
                    {
                        //add your own columns to be searched here
                        if (row["Bus_ID"].ToString().ToLower().Contains(searchTerm) || row["Driver"].ToString().ToLower().Contains(searchTerm) || row["Reason"].ToString().ToLower().Contains(searchTerm) || row["Comments"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtNew.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    GridViewBlackList.DataSource = dtNew;
                    ViewState["searchResults"] = dtNew;
                    GridViewBlackList.DataBind();
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
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Lista negra')", true);
        }


        void profilePicture(string driverID)
        {
            try
            {
                string path = "~/images/image-not-found.jpg";

                DataTable dt_ProfilePicture = GenericClass.SQLSelectObj(new Driver_Documents(), WhereClause: "where Driver_ID = " + driverID + "  and Category_file = 'ProfilePicture'");
                if (dt_ProfilePicture.Rows.Count > 0)
                {
                    path = ConfigurationManager.AppSettings["profilePictureImagesPathToDisplay"].ToString() + dt_ProfilePicture.Rows[0]["Short_Path"].ToString() + dt_ProfilePicture.Rows[0]["File_Name"].ToString();
                }

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "ShowInfoToEdit();", true);
                form_Profilepicture.ImageUrl = path;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }



    }
}