using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusManagementSystem.Class;
using BusManagementSystem._01Catalogos;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Data;
namespace BusManagementSystem
{
    public partial class User_Settings : System.Web.UI.Page
    {
        string language = null;
        string msg_UserSettings_User;
        string msg_UserSettings_ChangeCorrectly;
        string msg_UserSettings_ShouldBeDifferent;
        string msg_UserSettings_ChangeCorrectlyEmail;
        string msg_UserSettings_EmailShouldBeDifferent;
        string msg_UserSettings_Account;
        string msg_UserSettings_DeleteCorrectlyAccount;
        string msg_UserSettings_ColorHasBenChanged;
        string msg_UserSettings_LanguageHasBenChangedEN;
        string msg_UserSettings_LanguageHasBenChangedES;
        string msg_UserSettings_LanguageDefault;
        string msg_UserSettings_ThemeColorDefault;
        string msg_UserSettings_ThemeNLan;
        #region Page
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["C_USER"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }

                Users usr = (Users)(Session["C_USER"]);
                if (!this.IsPostBack)
                {
                    ddl_Fill();
                }
                
                applyLanguage();
                messages_ChangeLanguage();

            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", "");

                functions.ShowMessage(this.Page, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                if (Session["C_USER"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                Users usr = (Users)(Session["C_USER"]);
                currentEmail.Text = usr.Email;
                currentPassword.Text = usr.Password;
                currentPassword.Attributes.Add("value", usr.Password);

            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this.Page, this.GetType(), msg, MessageType.Error);
            }
        }
        #endregion


        #region button
        protected void btPassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["C_USER"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }

                if (newPassword.Text.Equals(confirmPassword.Text))
                {
                    if (!currentPassword.Text.Equals(functions.GetSHA1(newPassword.Text)))
                    {

                        Users usr = (Users)(Session["C_USER"]);
                        Dictionary<string, dynamic> user = new Dictionary<string, dynamic>();
                        user.Add("Password", functions.GetSHA1(confirmPassword.Text));
                        GenericClass.SQLUpdateObj(usr, user, "Where User_ID='" + usr.User_ID + "'");
                        functions.ShowMessage(this, this.GetType(), msg_UserSettings_User + usr.User_Name.Replace("\r\n", "") + msg_UserSettings_ChangeCorrectly, MessageType.Success);
                        usr.Password = functions.GetSHA1(newPassword.Text);
                        currentPassword.Text = usr.Password;
                        currentPassword.Attributes.Add("value", usr.Password);
                        Session["C_USER"] = usr;
                    }
                    else
                    {
                        functions.ShowMessage(this, this.GetType(), msg_UserSettings_ShouldBeDifferent, MessageType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this.Page, this.GetType(), msg, MessageType.Error);
            }

        }
        protected void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["C_USER"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }

                if (!currentEmail.Text.Equals(newEmail.Text))
                {

                    Users usr = (Users)(Session["C_USER"]);
                    Dictionary<string, dynamic> user = new Dictionary<string, dynamic>();
                    user.Add("Email", newEmail.Text);
                    GenericClass.SQLUpdateObj(usr, user, "Where User_ID='" + usr.User_ID + "'");
                    functions.ShowMessage(this, this.GetType(), msg_UserSettings_User + usr.User_Name.Replace("\r\n", "") + msg_UserSettings_ChangeCorrectlyEmail, MessageType.Success);
                    usr.Email = newEmail.Text;
                    currentEmail.Text = usr.Email;
                    newEmail.Text = "";
                    Session["C_USER"] = usr;
                }
                else
                {
                    functions.ShowMessage(this, this.GetType(), msg_UserSettings_EmailShouldBeDifferent, MessageType.Error);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this.Page, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void btDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "", true);

                if (Session["C_USER"] == null)
                {
                    Response.Redirect("~/Login.aspx");

                }

                Users usr = (Users)(Session["C_USER"]);
                Rol_User rolUser = new Rol_User();
                GenericClass.SQLDeleteObj(rolUser, "Where User_ID='" + usr.User_ID + "'");
                GenericClass.SQLDeleteObj(usr, "Where User_ID='" + usr.User_ID + "'");
                functions.ShowMessage(this, this.GetType(), msg_UserSettings_Account + usr.User_Name.Replace("\r\n", "") + msg_UserSettings_DeleteCorrectlyAccount, MessageType.Success);
                HtmlMeta meta = new HtmlMeta();
                meta.HttpEquiv = "Refresh";
                meta.Content = "2;url=Logout.aspx";
                this.Page.Controls.Add(meta);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this.Page, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void btnSaveGlobal_Click(object sender, EventArgs e)
        {
            try
            {
                string msjThemeColor = null;
                string validateThemeColorMsj = validateThemeColor();
                msjThemeColor = validateThemeColorMsj;
                validateLanguage();
                Response.Redirect("~/User_Settings.aspx", false);

                functions.ShowMessage(this, this.GetType(), msjThemeColor, MessageType.Success);

            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this.Page, this.GetType(), msg, MessageType.Error);
            }

        }
        protected void btnSaveSession_Click(object sender, EventArgs e)
        {
            try
            {
                

                String color = txt_themeColorValue.Text;
               
                if (txtlanguage.SelectedValue == "EN")
                {
                    Session["Language"] = "Eng";
                    functions.ShowMessage(this, this.GetType(), msg_UserSettings_LanguageHasBenChangedEN, MessageType.Success);
                    functions func = new functions();
                    func.languageTranslate(this.Master, "Eng");
                }
                else
                {
                    Session["Language"] = "Esp";
                    functions.ShowMessage(this, this.GetType(), msg_UserSettings_LanguageHasBenChangedES, MessageType.Success);
                    functions func = new functions();
                    
                }
                if (color != "#1b346b")
                {
                    Session["ColorTheme"] = color;
                    functions.changeTheme(this.Page, this.GetType(), functions.addPound(color));
                    

                    functions.ShowMessage(this, this.GetType(), msg_UserSettings_ColorHasBenChanged, MessageType.Success);

                }
                Response.Redirect("~/User_Settings.aspx", false);
                

            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this.Page, this.GetType(), msg, MessageType.Error);
            }

        }
    
        protected void imb_DefaultSettings_Click(object sender, EventArgs e)
        {
            string msg = null;

            if (resetLanguage())
            {
                msg = msg_UserSettings_LanguageDefault;
            }
            if (resetThemeColor())
            {
                if (msg == null)
                {
                    msg = msg_UserSettings_ThemeColorDefault;
                }
                else
                {
                    msg = msg + msg_UserSettings_ThemeNLan;
                }
            }
            Session["Language"] = null;
            functions.ShowMessage(this, this.GetType(), msg, MessageType.Success);
            Response.Redirect("~/User_Settings.aspx", false);
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
            msg_UserSettings_User = (string)GetGlobalResourceObject(language, "msg_UserSettings_User");
            msg_UserSettings_ChangeCorrectly = (string)GetGlobalResourceObject(language, "msg_UserSettings_ChangeCorrectly");
            msg_UserSettings_ShouldBeDifferent = (string)GetGlobalResourceObject(language, "msg_UserSettings_ShouldBeDifferent");
            msg_UserSettings_ChangeCorrectlyEmail = (string)GetGlobalResourceObject(language, "msg_UserSettings_ChangeCorrectlyEmail");
            msg_UserSettings_EmailShouldBeDifferent = (string)GetGlobalResourceObject(language, "msg_UserSettings_EmailShouldBeDifferent");
            msg_UserSettings_Account = (string)GetGlobalResourceObject(language, "msg_UserSettings_Account");
            msg_UserSettings_DeleteCorrectlyAccount = (string)GetGlobalResourceObject(language, "msg_UserSettings_DeleteCorrectlyAccount");
            msg_UserSettings_ColorHasBenChanged = (string)GetGlobalResourceObject(language, "msg_UserSettings_ColorHasBenChanged");
            msg_UserSettings_LanguageHasBenChangedEN = (string)GetGlobalResourceObject(language, "msg_UserSettings_LanguageHasBenChangedEN");
            msg_UserSettings_LanguageHasBenChangedES = (string)GetGlobalResourceObject(language, "msg_UserSettings_LanguageHasBenChangedES");
            msg_UserSettings_LanguageDefault = (string)GetGlobalResourceObject(language, "msg_UserSettings_LanguageDefault");
            msg_UserSettings_ThemeColorDefault = (string)GetGlobalResourceObject(language, "msg_UserSettings_ThemeColorDefault");
            msg_UserSettings_ThemeNLan = (string)GetGlobalResourceObject(language, "msg_UserSettings_ThemeNLan");


        }
        #endregion


       

        #region Methods
        protected void validateLanguage()
        {
            if (Session["Language"] != null)
                Session["Language"] = null;
            Users usr = (Users)(Session["C_USER"]);
            if (txtlanguage.SelectedValue == "EN")
            {

                DataTable GetTLanguage = GenericClass.SQLSelectObj(new Global_Configuration(), WhereClause: "where Configuration_Name = 'Language' and User_ID = '" + usr.User_ID + "'");
                if (GetTLanguage.Rows.Count == 0)
                {
                    Global_Configuration GlobalConfiguration = new Global_Configuration();
                    GlobalConfiguration.User_ID = usr.User_ID;
                    GlobalConfiguration.Vendor_ID = usr.Vendor_ID;
                    GlobalConfiguration.Last_Change = DateTime.Now;
                    GlobalConfiguration.Configuration_Name = "Language";
                    GlobalConfiguration.Configuration_Value = "Eng";
                    GenericClass.SQLInsertObj(GlobalConfiguration);

                    //insert
                }
                else if (GetTLanguage.Rows[0]["Configuration_Value"].ToString() != "EN")
                {
                    Global_Configuration GlobalConfigurationCurrent = new Global_Configuration();
                    GlobalConfigurationCurrent.User_ID = usr.User_ID;
                    GlobalConfigurationCurrent.Vendor_ID = usr.Vendor_ID;
                    GlobalConfigurationCurrent.Last_Change = DateTime.Now;
                    GlobalConfigurationCurrent.Configuration_Name = "Language";
                    GlobalConfigurationCurrent.Configuration_Value = "EN";

                    Global_Configuration GlobalConfigurationOld = new Global_Configuration();
                    GlobalConfigurationOld.User_ID = usr.User_ID;
                    GlobalConfigurationOld.Vendor_ID = usr.Vendor_ID;
                    GlobalConfigurationOld.Last_Change = Convert.ToDateTime(GetTLanguage.Rows[0]["Last_Change(M/D/Y)"]);
                    GlobalConfigurationOld.Configuration_Name = "Language";
                    GlobalConfigurationOld.Configuration_Value = GetTLanguage.Rows[0]["Configuration_Value"].ToString().Trim();

                    GenericClass.SQLUpdateObj(new Global_Configuration(), adminApprove.compareObjects(GlobalConfigurationOld, GlobalConfigurationCurrent, ""), WhereClause: "where Configuration_Name = 'Language' and User_ID = '" + usr.User_ID + "'");


                    //update
                }
                
            }
            else
            {

                DataTable GetTLanguage = GenericClass.SQLSelectObj(new Global_Configuration(), WhereClause: "where Configuration_Name = 'Language' and User_ID = '" + usr.User_ID + "'");
                if (GetTLanguage.Rows.Count == 0)
                {
                    Global_Configuration GlobalConfiguration = new Global_Configuration();
                    GlobalConfiguration.User_ID = usr.User_ID;
                    GlobalConfiguration.Vendor_ID = usr.Vendor_ID;
                    GlobalConfiguration.Last_Change = DateTime.Now;
                    GlobalConfiguration.Configuration_Name = "Language";
                    GlobalConfiguration.Configuration_Value = "ES";
                    GenericClass.SQLInsertObj(GlobalConfiguration);

                    //insert
                }
                else if (GetTLanguage.Rows[0]["Configuration_Value"].ToString() != "ES")
                {
                    Global_Configuration GlobalConfigurationCurrent = new Global_Configuration();
                    GlobalConfigurationCurrent.User_ID = usr.User_ID;
                    GlobalConfigurationCurrent.Vendor_ID = usr.Vendor_ID;
                    GlobalConfigurationCurrent.Last_Change = DateTime.Now;
                    GlobalConfigurationCurrent.Configuration_Name = "Language";
                    GlobalConfigurationCurrent.Configuration_Value = "ES";

                    Global_Configuration GlobalConfigurationOld = new Global_Configuration();
                    GlobalConfigurationOld.User_ID = usr.User_ID;
                    GlobalConfigurationOld.Vendor_ID = usr.Vendor_ID;
                    GlobalConfigurationOld.Last_Change = Convert.ToDateTime(GetTLanguage.Rows[0]["Last_Change(M/D/Y)"]);
                    GlobalConfigurationOld.Configuration_Name = "Language";
                    GlobalConfigurationOld.Configuration_Value = GetTLanguage.Rows[0]["Configuration_Value"].ToString().Trim();

                    GenericClass.SQLUpdateObj(new Global_Configuration(), adminApprove.compareObjects(GlobalConfigurationOld, GlobalConfigurationCurrent, ""), WhereClause: "where Configuration_Name = 'Language' and User_ID = '" + usr.User_ID + "'");


                    //update
                }

            }


        }

        protected void btHelp_Click(object sender, EventArgs e)
        {
            if (Session["USER"] == null)
            {
                Response.Redirect("./Login.aspx");
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Uso General de BMS')", true);
        }

        protected string validateThemeColor()
        {
            string value = null;
            Users usr = (Users)(Session["C_USER"]);
            if (Session["ColorTheme"] != null)
                     Session["ColorTheme"] = null;
        

            if (txt_themeColorValue.Text != "#1b346b")
            {
                List<string> columnsOfTheTableGetTheme = new List<string>();
                columnsOfTheTableGetTheme.Add("Configuration_Value");

                DataTable GetThemeColor = GenericClass.SQLSelectObj(new Global_Configuration(), WhereClause: "where Configuration_Name = 'ColorTheme' and User_ID = '" + usr.User_ID + "'", customSelect: columnsOfTheTableGetTheme);
                String colorValue = txt_themeColorValue.Text;
                Global_Configuration GlobalConfigurationCurrent = new Global_Configuration();
                GlobalConfigurationCurrent.Configuration_Name = "ColorTheme";
                GlobalConfigurationCurrent.Configuration_Value = colorValue;
                GlobalConfigurationCurrent.Last_Change = DateTime.Now;
                GlobalConfigurationCurrent.User_ID = usr.User_ID;
                GlobalConfigurationCurrent.Vendor_ID = usr.Vendor_ID;

                if (GetThemeColor.Rows.Count != 0)
                {
                    DataTable GetGlobalConfigurationOld = GenericClass.SQLSelectObj(new Global_Configuration(), WhereClause: "where Configuration_Name = 'ColorTheme' and User_ID = '" + usr.User_ID + "'", customSelect: columnsOfTheTableGetTheme);
                    Global_Configuration GlobalConfigurationOld = new Global_Configuration();
                    GlobalConfigurationOld.Configuration_Name = "ColorTheme";
                    GlobalConfigurationOld.Configuration_Value = GetGlobalConfigurationOld.Rows[0]["Configuration_Value"].ToString();
                    GlobalConfigurationOld.Last_Change = DateTime.Now;
                    GlobalConfigurationOld.User_ID = usr.User_ID;
                    if (colorValue != GetGlobalConfigurationOld.Rows[0]["Configuration_Value"].ToString())
                    {
                        GenericClass.SQLUpdateObj(new Global_Configuration(), adminApprove.compareObjects(GlobalConfigurationOld, GlobalConfigurationCurrent, ""), WhereClause: "where Configuration_Name = 'ColorTheme' and User_ID = '" + usr.User_ID + "'");
                        value = "Update themecolor";
                    }

                }
                else
                {
                    GenericClass.SQLInsertObj(GlobalConfigurationCurrent);
                    value = "Create new theme color";
                }
                functions.changeTheme(this.Page, this.GetType(), colorValue);
                txt_themeColorValue.Text = null;
            }
            return value;


        }
        protected void ddl_Fill()
        {
            Users usr = (Users)(Session["C_USER"]);
            List<string> columnsOfTheTableGetTheme = new List<string>();
            columnsOfTheTableGetTheme.Add("Configuration_Value");
            DataTable GetThemeColor = GenericClass.SQLSelectObj(new Global_Configuration(), WhereClause: "where Configuration_Name = 'ColorTheme' and User_ID = '" + usr.User_ID + "'", customSelect: columnsOfTheTableGetTheme);
            if (GetThemeColor.Rows.Count != 0)
            {
                txt_themeColorValue.Text = functions.addPound(GetThemeColor.Rows[0]["Configuration_Value"].ToString());
            }
            else
            {
                txt_themeColorValue.Text = "#1b346b";
            }
            DataTable GetTLanguage = GenericClass.SQLSelectObj(new Global_Configuration(), WhereClause: "where Configuration_Name = 'Language' and User_ID = '" + usr.User_ID + "'");
            ListItem EN = new ListItem("EN", "EN");
            ListItem ES = new ListItem("ES", "ES");
             if (Session["Language"] != null)
            {
                if (Session["Language"].ToString() == "Esp")
                {
                    txtlanguage.Items.Insert(0, ES);
                    txtlanguage.Items.Insert(1, EN);
                }
                else
                {
                    txtlanguage.Items.Insert(0, EN);
                    txtlanguage.Items.Insert(1, ES);
                }
            
            }
            else if(GetTLanguage.Rows.Count != 0)
            {

                if (GetTLanguage.Rows[0]["Configuration_Value"].ToString() == "ES")
                {
                    txtlanguage.Items.Insert(0, ES);
                    txtlanguage.Items.Insert(1, EN);
                }
                else 
                {
                    txtlanguage.Items.Insert(0, EN);
                    txtlanguage.Items.Insert(1, ES);

                }

            }
             else
             {
                 txtlanguage.Items.Insert(0, ES);
                 txtlanguage.Items.Insert(1, EN);

             }


        }
        protected bool resetThemeColor()
        {

            bool returnValue = false;
            try
            {

                Users usr = (Users)(Session["C_USER"]);
                List<string> columnsOfTheTableGetTheme = new List<string>();
                columnsOfTheTableGetTheme.Add("Configuration_Value");
                DataTable GetThemeColor = GenericClass.SQLSelectObj(new Global_Configuration(), WhereClause: "where Configuration_Name = 'ColorTheme' and User_ID = '" + usr.User_ID + "'", customSelect: columnsOfTheTableGetTheme);
                if (Session["ColorTheme"] != null)
                {

                    Session["ColorTheme"] = null;


                    returnValue = true;
                }
                if (GetThemeColor.Rows.Count != 0)
                {
                    GenericClass.SQLDeleteObj(new Global_Configuration(), WhereClause: "where Configuration_Name = 'ColorTheme' and User_ID = '" + usr.User_ID + "'");
                    returnValue = true;

                }
               

            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this.Page, this.GetType(), msg, MessageType.Error);
            }
            return returnValue;

        }
        protected bool resetLanguage()
        {
            bool returnValue = false;
            Session["Language"] = null;
            Users usr = (Users)(Session["C_USER"]);
            try
            {   
                DataTable GetTLanguage = GenericClass.SQLSelectObj(new Global_Configuration(), WhereClause: "where Configuration_Name = 'Language' and User_ID = '" + usr.User_ID + "'");
                if (GetTLanguage.Rows.Count != 0)
                {

                    GenericClass.SQLDeleteObj(new Global_Configuration(), WhereClause: "where Configuration_Name = 'Language' and User_ID = '" + usr.User_ID + "'");
                    returnValue = true;
                }

            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this.Page, this.GetType(), msg, MessageType.Error);
            }

            return returnValue;
        }
        #endregion


        

        
        


       
  

     


    }
}