using BusManagementSystem._01Catalogos;
using BusManagementSystem.Catalogos;
using BusManagementSystem.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusManagementSystem
{
    public partial class MenuPortal : System.Web.UI.Page
    {
        Users usr;
        string profile;
        string language = null;
        string msg_MenuPortal_NoResults;
        string	msg_MenuPortal_Status;
        string	msg_MenuPortal_Date;
        string	msg_MenuPortal_Revision;
        string	msg_MenuPortal_Vendor;
        string	msg_MenuPortal_Bus_ID;

        protected void Page_Load(object sender, EventArgs e)
        {
            TreeView TreeView1 = (TreeView)Page.Master.FindControl("TreeView1");
            if (Session["USER"] == null)
            {
                Response.Redirect("./Login.aspx");
            }


            usr = (Users)(Session["C_USER"]);
            applyLanguage();
            messages_ChangeLanguage();
            

            profile = usr.Profile;
            createUserMenu();
            fillAlertsWidget();
            fillOperationWidget();
            fillPendingAct();
            fillRevWidget();
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
        private void createUserMenu()
        {
            try
            {
                Dictionary<string, string> pagesDty = new Dictionary<string, string>();
                DataTable dtgetMenuPages = GenericClass.GetCustomData("SELECT TOP 11 * FROM CAT_MENU_PORTAL");
                foreach (DataRow dr in dtgetMenuPages.Rows)
                {
                    if (language == "Esp")
                        pagesDty.Add(dr["Page_ID"].ToString(), dr["Html_Code_Esp"].ToString());
                    else
                        pagesDty.Add(dr["Page_ID"].ToString(), dr["Html_Code"].ToString());
                    
                }
                StringBuilder strMenuPages = new StringBuilder();
                Page_User pageUser = new Page_User();

                DataTable dtGetPages = GenericClass.SQLSelectObj(pageUser, WhereClause: "WHERE USER_ID= '" + usr.User_ID + "' AND IS_ACTIVE=1");
                if (dtGetPages.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtGetPages.Rows)
                    {
                        string pageValueID = dr["Page_ID"].ToString();
                        foreach (var item in pagesDty)
                        {
                            if (pageValueID == item.Key.ToString())
                            {
                                strMenuPages.Append(item.Value.ToString().ToString());
                            }
                        }
                    }
                    ltMenu.Text = strMenuPages.ToString();
                }
                else
                    ltMenu.Text = msg_MenuPortal_NoResults;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        protected void messages_ChangeLanguage()
        {
            msg_MenuPortal_NoResults = (string)GetGlobalResourceObject(language, "msg_MenuPortal_NoResults");
        	msg_MenuPortal_Status = (string)GetGlobalResourceObject(language, "msg_MenuPortal_Status");
        	msg_MenuPortal_Date = (string)GetGlobalResourceObject(language, "msg_MenuPortal_Date");
        	msg_MenuPortal_Revision = (string)GetGlobalResourceObject(language, "msg_MenuPortal_Revision");
        	msg_MenuPortal_Vendor = (string)GetGlobalResourceObject(language, "msg_MenuPortal_Vendor");
        	msg_MenuPortal_Bus_ID = (string)GetGlobalResourceObject(language, "msg_MenuPortal_Bus_ID");
        }
        private void fillPendingAct()
        {
            try
            {
                string whereCondition;
                if (profile == "INTERNAL")
                    whereCondition = "WHERE 1=1";
                else
                    whereCondition = "WHERE USER_ID=  '" + usr.User_ID + "'";
                int count = 0;
                StringBuilder strPendingAct = new StringBuilder();
                Admin_Approve pendingActObj = new Admin_Approve();
                DataTable dtGetData = GenericClass.SQLSelectObj(pendingActObj, WhereClause: whereCondition +" And Confirm_Date is null ", OrderByClause: "ORDER BY Activity_Date DESC");
                if (dtGetData.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtGetData.Rows)
                    {
                        count += 1;
                        if (count == 6)
                            break;

                        strPendingAct.AppendLine("<li class='clearfix'>");
                        strPendingAct.AppendLine(" <div class='txt'>");
                        strPendingAct.AppendLine("<i class='icon-edit'></i> ");
                        strPendingAct.AppendLine(dr["Type"].ToString() + "-" + dr["Module"].ToString() + "-" + dr["Comments"].ToString());
                        strPendingAct.AppendLine("<span class='date badge badge-info'>" + dr["Activity_Date(M/D/Y)"].ToString() + "</span>");
                        strPendingAct.AppendLine(" </div>");
                        strPendingAct.AppendLine("<div class='pull-right'>");
                        strPendingAct.AppendLine(" <div class='txt'>");
                        strPendingAct.AppendLine(" </div>");
                        strPendingAct.AppendLine("</li>");
                    }
                    ltPendingAct.Text = strPendingAct.ToString();
                }
                else
                    ltPendingAct.Text = msg_MenuPortal_NoResults;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        private void fillOperationWidget()
        {
            try
            {
                string whereCondition;
                if (profile == "INTERNAL")
                    whereCondition = "WHERE 1=1";
                else
                    whereCondition = "WHERE MASTER_TRIP.VENDOR_ID=  '" + usr.Vendor_ID + "'";
                StringBuilder strRecentOper = new StringBuilder();
                Master_Trip masterTripObj = new Master_Trip();
                DataTable dtGetOperations = GenericClass.SQLSelectObj(masterTripObj, mappingQueryName: "MP-getOperations", WhereClause: whereCondition, OrderByClause: "ORDER BY LOG_DATE DESC");
                if (dtGetOperations.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtGetOperations.Rows)
                    {
                        strRecentOper.AppendLine("<div class='new-update clearfix'> ");
                        strRecentOper.AppendLine("<i class='icon-gift'></i>");
                        strRecentOper.AppendLine("<span class='update-notice'>");
                        strRecentOper.AppendLine("<a title='' href='#'><strong>");
                        strRecentOper.AppendLine(dr["Name"].ToString() + "-" + dr["Name1"].ToString());
                        strRecentOper.AppendLine("</strong></a>");
                        strRecentOper.AppendLine("<span> " + msg_MenuPortal_Status + "<strong>" + dr["Status"].ToString() + "</strong>" + " "+ msg_MenuPortal_Date  + dr["Log_Date"].ToString() + " </span>");
                        strRecentOper.AppendLine("</span>");
                        strRecentOper.AppendLine("</div>");

                    }

                    ltRecentOp.Text = strRecentOper.ToString();
                }
                else
                    ltRecentOp.Text = msg_MenuPortal_NoResults;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        private void fillRevWidget()
        {
            try
            {
                string whereCondition;
                if (profile == "INTERNAL")
                    whereCondition = "WHERE 1=1";
                else
                    whereCondition = "WHERE legal_revision.VENDOR_ID=  '" + usr.Vendor_ID + "'";
                int revCount = 0;
                StringBuilder strRecentRev = new StringBuilder();
                Legal_Revision revObj = new Legal_Revision();
                DataTable dtGetRevisions = GenericClass.SQLSelectObj(revObj, mappingQueryName: "MTO-GetRevisions", WhereClause: whereCondition, OrderByClause: "ORDER BY Revision_Date DESC");
                if (dtGetRevisions.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtGetRevisions.Rows)
                    {
                        revCount += 1;
                        if (revCount == 6)
                            break;

                        strRecentRev.AppendLine("<div class='new-update clearfix'> ");
                        strRecentRev.AppendLine("<i class='icon-gift'></i>");
                        strRecentRev.AppendLine("<span class='update-notice'>");
                        strRecentRev.AppendLine("<a title='' href='#'><strong>");
                        strRecentRev.AppendLine(msg_MenuPortal_Revision +" " + dr["Legal_Revision_ID"].ToString() + "-" + msg_MenuPortal_Vendor +" " + dr["Proveedor"].ToString() + msg_MenuPortal_Bus_ID +" " + dr["Bus_ID"].ToString());
                        strRecentRev.AppendLine("</strong></a>");
                        strRecentRev.AppendLine("<span> " + msg_MenuPortal_Status + " <strong> " + dr["Status"].ToString() + "</strong>" +" "+ msg_MenuPortal_Date  + dr["Revision_Date(M/D/Y)"].ToString() + " </span>");
                        strRecentRev.AppendLine("</span>");
                        strRecentRev.AppendLine("</div>");
                    }

                    ltRecentRev.Text = strRecentRev.ToString();
                }
                else
                    ltRecentRev.Text = msg_MenuPortal_NoResults;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        private void fillAlertsWidget()
        {
            try
            {
                string whereCondition;
                if (profile == "INTERNAL")
                    whereCondition = "WHERE 1=1";
                else
                    whereCondition = "WHERE VENDOR_ID=  '" + usr.Vendor_ID + "'";
                int count = 0;
                StringBuilder strAlerts = new StringBuilder();
                Alert_Log iniAlert = new Alert_Log();
                DataTable dtGetAlerts = GenericClass.SQLSelectObj(iniAlert, WhereClause: whereCondition + " And UPPER(Status)='OPEN'", OrderByClause: "ORDER BY ALERT_DATE DESC");
                if (dtGetAlerts.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtGetAlerts.Rows)
                    {
                        count += 1;
                        if (count == 6)
                            break;
                        string priority = dr["Priority"].ToString();
                        switch (priority)
                        {
                            case "LOW": priority = "<span class='by label'> " + " " + dr["Priority"].ToString() + "</span>";
                                break;
                            case "MEDIUM": priority = "<span class='date badge badge-warning'> " + " " + dr["Priority"].ToString() + "</span>";
                                break;
                            case "HIGH": priority = "<span class='date badge badge-important'> " + " " + dr["Priority"].ToString() + "</span>";
                                break;
                            default: priority = "<span class='by label'> " + " " + dr["Priority"].ToString() + "</span>";
                                break;
                        }

                        strAlerts.AppendLine("<li class='clearfix'>");
                        strAlerts.AppendLine(" <div class='txt'>");
                        strAlerts.AppendLine("<i class='icon-warning-sign'></i> ");
                        strAlerts.AppendLine(dr["Bus_ID"].ToString() + " " + dr["Driver_ID"].ToString() + " " + dr["Comments"].ToString());
                        strAlerts.AppendLine(priority);
                        strAlerts.AppendLine(" </div>");
                        strAlerts.AppendLine("<div class='pull-right'>");
                        strAlerts.AppendLine(" <div class='txt'>");
                        strAlerts.AppendLine(" </div>");
                        strAlerts.AppendLine("</li>");
                    }
                    litTable.Text = strAlerts.ToString();
                }
                else
                    litTable.Text = msg_MenuPortal_NoResults;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
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



    }
}