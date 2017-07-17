using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusManagementSystem.Class;
using BusManagementSystem._01Catalogos;
using System.Text;
using System.Web.UI.HtmlControls;

namespace BusManagementSystem.Administration
{
    public partial class GlobalConfigurations : System.Web.UI.Page
    {
        string language = null;
        string msg_GlobalConfigurations_Record;
        string msg_GlobalConfigurations_ContactITteam;
        string msg_GlobalConfigurations_DistributionList;
        string msg_GlobalConfigurations_NewIVA;
        string msg_GlobalConfigurations_UpdateIVA;
        string msg_GlobalConfigurations_IcorrectoORempty;
        
   


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            try
            {
                Users usr = (Users)(Session["C_USER"]);

                if (!usr.Rol_ID.Contains("ADMIN"))
                {
                    Response.Redirect("~/MenuPortal.aspx");
                }
                if (!this.IsPostBack)
                {


                   
                    fill_txtIva();
                    fillVendorCombo();
                    if (!usr.Rol_ID.Contains("ADMIN"))
                    {
                        ddlVendor.Enabled = false;
                        btnEmail.Enabled = false;
                    }

                }
                applyLanguage();
                messages_ChangeLanguage();

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);

                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", "");

                functions.ShowMessage(this.Page, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void messages_ChangeLanguage()
        {
       
            msg_GlobalConfigurations_Record = (string)GetGlobalResourceObject(language, "msg_GlobalConfigurations_Record");
            msg_GlobalConfigurations_ContactITteam = (string)GetGlobalResourceObject(language, "msg_GlobalConfigurations_ContactITteam");
            msg_GlobalConfigurations_DistributionList = (string)GetGlobalResourceObject(language, "msg_GlobalConfigurations_DistributionList");
            msg_GlobalConfigurations_NewIVA = (string)GetGlobalResourceObject(language, "msg_GlobalConfigurations_NewIVA");
            msg_GlobalConfigurations_UpdateIVA = (string)GetGlobalResourceObject(language, "msg_GlobalConfigurations_UpdateIVA");
            msg_GlobalConfigurations_IcorrectoORempty = (string)GetGlobalResourceObject(language, "msg_GlobalConfigurations_IcorrectoORempty");

         

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

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);

                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this.Page, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void fill_txtIva()
        {
            DataTable dt_GetIVAValue = GenericClass.SQLSelectObj(new Global_Configuration(), WhereClause: "where Configuration_Name = 'IVA'");
            if (dt_GetIVAValue.Rows.Count == 0)
            {
                txt_CurrentIVA.Text = "No data";
                txt_LastModification.Text = "No data";
            }
            else
            {
                txt_CurrentIVA.Text = dt_GetIVAValue.Rows[0]["Configuration_Value"].ToString() + " %";
                txt_LastModification.Text = dt_GetIVAValue.Rows[0]["Last_Change(M/D/Y)"].ToString();
            }
           

        }
        protected void btnEmail_Click(object sender, EventArgs e)
        {

            string vendor_ID = ddlVendor.SelectedValue.ToString();
            string emailsDaily = txtDailyOperation.Text;
            string emailsCatalog = txtCatalogs.Text;
            string emailsMaintenance = txtMaintenance.Text;
            string emailsAlerts = txtAlerts.Text;

            Dictionary<string, string> relatedItem = new Dictionary<string, string>();
            relatedItem.Add("Daily_Operation_Email", emailsDaily);
            relatedItem.Add("Alerts_Email", emailsAlerts);
            relatedItem.Add("Maintenance_Email", emailsMaintenance);
            relatedItem.Add("Catalogs_Email", emailsCatalog);

            try
            {
                foreach (var item in relatedItem)
                {
                    Global_Configuration globalObj = new Global_Configuration();
                    //Validate if not exists , then insert record
                    DataTable getGlobalC = GenericClass.SQLSelectObj(globalObj, WhereClause: "WHERE VENDOR_ID='" + vendor_ID + "' AND CONFIGURATION_NAME='" + item.Key + "'" +
                   "AND CONFIGURATION_CATEGORY='EMAIL'");
                    if (getGlobalC.Rows.Count == 0)
                    {
                        globalObj.Configuration_Name = item.Key;
                        globalObj.Configuration_Value = item.Value;
                        globalObj.Configuration_Category = "EMAIL";
                        globalObj.Last_Change = System.DateTime.Now;
                        globalObj.User_ID = null;
                        globalObj.Vendor_ID = vendor_ID;
                        GenericClass.SQLInsertObj(globalObj);
                    }
                    if (getGlobalC.Rows.Count == 1)
                    {
                        Dictionary<string, dynamic> globalUpdDtll = new Dictionary<string, dynamic>();
                        globalUpdDtll.Add("Last_Change", System.DateTime.Now);
                        globalUpdDtll.Add("Configuration_Value", "'" + item.Value + "'");

                        GenericClass.SQLUpdateObj(globalObj, globalUpdDtll, "WHERE CONFIGURATION_NAME= '" + item.Key + "' AND VENDOR_ID='" + vendor_ID + "' AND CONFIGURATION_CATEGORY='EMAIL'");

                    }
                    else
                    {
                        functions.ShowMessage(this, this.GetType(), msg_GlobalConfigurations_Record + item.Key + msg_GlobalConfigurations_ContactITteam, MessageType.Success);
                    }

                }

                functions.ShowMessage(this, this.GetType(), msg_GlobalConfigurations_DistributionList, MessageType.Success);
            }
            catch (Exception ex )
            {
               functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        private void fillVendorCombo()
        {
            btnEmail.Enabled = false;
            Vendor initialVendor = new Vendor();
            DataTable getVendors = GenericClass.SQLSelectObj(initialVendor, WhereClause: "");

            ddlVendor.Items.Clear();
            ddlVendor.DataSource = getVendors;
            ddlVendor.DataValueField = "VENDOR_ID";
            ddlVendor.DataTextField = "NAME";
            ddlVendor.DataBind();

            ListItem def = new ListItem();
            def.Text = "PLEASE SELECT";
            def.Value = "DEF";
            ddlVendor.Items.Add(def);

            ddlVendor.SelectedValue = "DEF";

        }

        protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAlerts.Text = string.Empty;
            txtCatalogs.Text = string.Empty;
            txtDailyOperation.Text = string.Empty;
            txtMaintenance.Text = string.Empty;

            DropDownList ddlVendor = (DropDownList)sender;
            if (ddlVendor.SelectedValue == "DEF")
            {
                btnEmail.Enabled = false;
                txtAlerts.Enabled = false;
                txtCatalogs.Enabled = false;
                txtDailyOperation.Enabled = false;
                txtMaintenance.Enabled = false;
            }
            else
            {
                btnEmail.Enabled = true;
                fillDistListEmailsFields(ddlVendor.SelectedValue);
                txtAlerts.Enabled = true;
                txtCatalogs.Enabled = true;
                txtDailyOperation.Enabled = true;
                txtMaintenance.Enabled = true;
            }

        }
        private void fillDistListEmailsFields(string vendorID)
        {
            Global_Configuration globalC = new Global_Configuration();
            DataTable getData = GenericClass.SQLSelectObj(globalC, WhereClause: "WHERE CONFIGURATION_CATEGORY='EMAIL' AND VENDOR_ID='" + vendorID + "'");

            //Assing values depending of selected Vendor
            if (getData.Rows.Count > 0)
            {
                txtDailyOperation.Text = getData.Rows[0]["Configuration_Value"].ToString();
                txtAlerts.Text = getData.Rows[1]["Configuration_Value"].ToString();
                txtMaintenance.Text = getData.Rows[2]["Configuration_Value"].ToString();
                txtCatalogs.Text = getData.Rows[3]["Configuration_Value"].ToString();
            }


        }

        protected void bt_SaveIVAValue_Click(object sender, EventArgs e)
        {
            string msg = validate_Iva();
            if (msg != null)
            {
                functions.ShowMessage(this, this.GetType(), msg, MessageType.Success);
                fill_txtIva();
                txt_IVAValue.Text = null;
            }
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
        protected string validate_Iva()
        {
            string value = null;
            if (txt_IVAValue.Text != null && txt_IVAValue.Text != "0" && txt_IVAValue.Text != "00")
            {
                Users usr = (Users)(Session["C_USER"]);
                Global_Configuration globalConfigurationOld = new Global_Configuration();
                DataTable dt_GetIVAValue = GenericClass.SQLSelectObj(new Global_Configuration(), WhereClause: "where Configuration_Name = 'IVA'");
                if (dt_GetIVAValue.Rows.Count == 0)
                {
                    globalConfigurationOld.Configuration_Name = "IVA";
                    globalConfigurationOld.Configuration_Value = txt_IVAValue.Text;
                    globalConfigurationOld.Last_Change = DateTime.Now;
                    globalConfigurationOld.Vendor_ID = usr.Vendor_ID;
                    globalConfigurationOld.User_ID = usr.User_ID;
                    GenericClass.SQLInsertObj(globalConfigurationOld);
                    value = msg_GlobalConfigurations_NewIVA + txt_IVAValue.Text + " %";
                }
                else
                {
                    globalConfigurationOld.Configuration_ID = Convert.ToInt32(dt_GetIVAValue.Rows[0]["Configuration_ID"]);
                    globalConfigurationOld.Configuration_Name = Convert.ToString(dt_GetIVAValue.Rows[0]["Configuration_Name"]);
                    globalConfigurationOld.Configuration_Value = dt_GetIVAValue.Rows[0]["Configuration_Value"].ToString();
                    globalConfigurationOld.Last_Change = Convert.ToDateTime(dt_GetIVAValue.Rows[0]["Last_Change(M/D/Y)"]);
                    globalConfigurationOld.Vendor_ID = usr.Vendor_ID;
                    globalConfigurationOld.User_ID = usr.User_ID;


                    Global_Configuration globalConfigurationCurrent = new Global_Configuration();
                    globalConfigurationCurrent.Configuration_Value = txt_IVAValue.Text;
                    globalConfigurationCurrent.Configuration_ID = Convert.ToInt32(dt_GetIVAValue.Rows[0]["Configuration_ID"]);
                    globalConfigurationCurrent.Configuration_Name = "IVA";
                    globalConfigurationCurrent.User_ID = usr.User_ID;
                    globalConfigurationCurrent.Vendor_ID = usr.Vendor_ID;
                    globalConfigurationCurrent.Last_Change = DateTime.Now;

                    if (dt_GetIVAValue.Rows[0]["Configuration_Value"].ToString() != txt_IVAValue.Text)
                    {
                        GenericClass.SQLUpdateObj(new Global_Configuration(), adminApprove.compareObjects(globalConfigurationOld, globalConfigurationCurrent, ""), WhereClause: "where Configuration_ID ='" + Convert.ToInt32(dt_GetIVAValue.Rows[0]["Configuration_ID"]) + "'");
                        value = msg_GlobalConfigurations_UpdateIVA + txt_IVAValue.Text + " %";
                    }

                }

            }
            else
            {
                functions.ShowMessage(this, this.GetType(), msg_GlobalConfigurations_IcorrectoORempty, MessageType.Error);
            }
            return value;
        }

        protected void btHelp_Click(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("./Login.aspx");
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Configuración global')", true);
        }
    }
}