using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusManagementSystem.Class;
using System.Web.UI.HtmlControls;
using BusManagementSystem._01Catalogos;
using BusManagementSystem.Catalogos;
using System.Configuration;

namespace BusManagementSystem
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        string language = null;
        public void setPanelInfoVisible(bool visible)
        {
           pnlInfo.Visible = visible;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(ConfigurationManager.AppSettings["BMSversion"].ToString() == null)
            {
            lbl_MasterPage_version.Text = "No version";
            }
            else
            {
            lbl_MasterPage_version.Text = ConfigurationManager.AppSettings["BMSversion"].ToString();
            }
            
             
            
            int i = 0;

            if (i == 0)
            {
                i = i + 1;
                TreeView TreeView1 = (TreeView)Page.Master.FindControl("TreeView1");
                applyLanguage();
                messages_ChangeLanguage();
                if (!IsPostBack)
                {
               

                    if (Session["APPID"] == null) { Session["APPID"] = "N/A"; }
                    if (Session["USER"] == null)
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                    Users usr = (Users)(Session["C_USER"]);            
                    
                    i = new int();

                    /*RAFBEL MENU.INI*/
                    if (Session["UsersAndRoles"] == null)
                    {
                        Response.Redirect("~/Login.aspx");
                    }                    
                    UsersAndRoles usrAndRol = (UsersAndRoles)(Session["UsersAndRoles"]);
                    string GetLanguage = functions.GetLanguage(usr); //Esp
                    if (Session["Language"] != null)                                 
                    {
                        GetLanguage = Session["Language"].ToString();
                    }
                   
                    /*RAFBEL MENU.END*/

                     foreach( Module module in usrAndRol.moduleList.FindAll(find => find.Module_Name.ToUpper() != "ADMINISTRATION" ).OrderBy(order => order.Module_Name ))
                     {
                        Module_User moduleUser =  (Module_User)usrAndRol.moduleUserList.Find(find => find.Module_ID == module.Module_ID 
                            && find.Is_Active == true );

                        if (moduleUser != null)
                        {
                            string moduleName = module.Module_Name.ToString(); 
                            if (GetLanguage.ToUpper().Trim() == "ESP")
                            {
                                moduleName = module.Module_Name_ESP.ToString();
                            }
                            TreeNode tn = new TreeNode(moduleName, "PARENT_NODO");

                            foreach (Module_Page modulePag in usrAndRol.modulePageList.FindAll(find => find.Module_ID == moduleUser.Module_ID))
                            {
                                Pages pag = (Pages)usrAndRol.pageList.Find(find => find.Page_ID == modulePag.Page_ID );
                                if (pag != null )
                                {
                                    Page_User pUser = usrAndRol.pageUserList.Find(find => find.Page_ID == pag.Page_ID
                                        && find.Is_Active == true);
                                    if (pUser != null)
                                    {
                                        string pageName = pag.Page_Name.ToString();
                                        if (GetLanguage.ToUpper().Trim() == "ESP")
                                        {
                                            pageName = pag.Page_Name_ESP.ToString();
                                        }
                                        TreeNode a = new TreeNode(pageName, pag.Page_URL);
                                        a.NavigateUrl = pag.Page_URL;
                                        tn.ChildNodes.Add(a);
                                    }
                                }
                                
                                                                
                            }
                            
                            TreeView1.Nodes.Add(tn);
                            TreeView1.CollapseAll();
                        }                        
                     }
                     foreach (Module module in usrAndRol.moduleList.FindAll(find => find.Module_Name.ToUpper() == "ADMINISTRATION").OrderBy(order => order.Module_Name))
                     {
                         Module_User moduleUser = (Module_User)usrAndRol.moduleUserList.Find(find => find.Module_ID == module.Module_ID
                             && find.Is_Active == true);

                         if (moduleUser != null)
                         {
                             string moduleName = module.Module_Name.ToString();
                             if (GetLanguage.ToUpper().Trim() == "ESP")
                             {
                                 moduleName = module.Module_Name_ESP.ToString();
                             }
                             TreeNode tn = new TreeNode(moduleName, "PARENT_NODO");

                             foreach (Module_Page modulePag in usrAndRol.modulePageList.FindAll(find => find.Module_ID == moduleUser.Module_ID))
                             {
                                 Pages pag = (Pages)usrAndRol.pageList.Find(find => find.Page_ID == modulePag.Page_ID);
                                 if (pag != null)
                                 {
                                     Page_User pUser = usrAndRol.pageUserList.Find(find => find.Page_ID == pag.Page_ID
                                         && find.Is_Active == true);
                                     if (pUser != null)
                                     {
                                         string pageName = pag.Page_Name.ToString();
                                         if (GetLanguage.ToUpper().Trim() == "ESP")
                                         {
                                             pageName = pag.Page_Name_ESP.ToString();
                                         }
                                         TreeNode a = new TreeNode(pageName, pag.Page_URL);
                                         a.NavigateUrl = pag.Page_URL;
                                         tn.ChildNodes.Add(a);
                                     }
                                 }
                             }

                             TreeView2Admin.Nodes.Add(tn);
                             TreeView2Admin.CollapseAll();
                         }
                     }

                     Module moduleAdmin = (Module)usrAndRol.moduleList.Find(find => find.Module_Name.ToUpper() == "ADMINISTRATION");
                     if (moduleAdmin != null)
                     {
                         Module_User moduleUser = (Module_User)usrAndRol.moduleUserList.Find(find => find.Module_ID == moduleAdmin.Module_ID
                             && find.Is_Active == true);
                         if (moduleUser == null)
                         {
                                 TreeNode tn = new TreeNode("Do not have privileges", "PARENT_NODO");
                                 TreeView2Admin.Nodes.Add(tn);
                         }
                     }


                     HelloUser.Text = (string)GetGlobalResourceObject(language, "lbl_MasterPage_HelloUser") + usr.User_ID;
                     HelloUserName.Text = (string)GetGlobalResourceObject(language, "lbl_MasterPage_HelloUserName") + usr.User_Name;
                }
            }
            themeColor();
            
        }
        protected void messages_ChangeLanguage()
        {
            lbl_MasterPage_Block.Text = (string)GetGlobalResourceObject(language, "lbl_MasterPage_Block");
            lbl_MasterPage_Footer.Text = (string)GetGlobalResourceObject(language, "lbl_MasterPage_Footer");
            lbl_MasterPage_Inactive.Text = (string)GetGlobalResourceObject(language, "lbl_MasterPage_Inactive");
            lbl_MasterPage_Logout.Text = (string)GetGlobalResourceObject(language, "lbl_MasterPage_Logout");
            lbl_MasterPage_SystemAdmin.Text = (string)GetGlobalResourceObject(language, "lbl_MasterPage_SystemAdmin");
            lbl_MasterPage_SystemOperation.Text = (string)GetGlobalResourceObject(language, "lbl_MasterPage_SystemOperation");
            lbl_MasterPage_Title.Text = (string)GetGlobalResourceObject(language, "lbl_MasterPage_Title");
            //lbl_MasterPage_UnderAlert.Text = (string)GetGlobalResourceObject(language, "lbl_MasterPage_UnderAlert");
            lbl_MasterPage_UserSettings.Text = (string)GetGlobalResourceObject(language, "lbl_MasterPage_UserSettings");

            
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
            
        }
        protected void themeColor()
        {
            try
            {
                List<string> ColumnsOfTable = new List<string>();
                ColumnsOfTable.Add("Configuration_Value");
                Users usr = (Users)(Session["C_USER"]);
                DataTable GetTheme = GenericClass.SQLSelectObj(new Global_Configuration(), WhereClause: "where Configuration_Name = 'ColorTheme' and User_ID = '" + usr.User_ID + "'", customSelect: ColumnsOfTable);
                if (GetTheme.Rows.Count > 0)
                {
                    if (GetTheme.Rows[0]["Configuration_Value"].ToString() != null && Session["ColorTheme"] == null)
                    {

                        functions.changeTheme(this.Page, this.GetType(), functions.addPound(GetTheme.Rows[0]["Configuration_Value"].ToString()));

                    }
                    else if (Session["ColorTheme"] != null)
                    {
                        functions.changeTheme(this.Page, this.GetType(), functions.addPound(Session["ColorTheme"].ToString()));
                    }
                }
                else if (Session["ColorTheme"] != null)
                {
                    functions.changeTheme(this.Page, this.GetType(), functions.addPound(Session["ColorTheme"].ToString()));
                
                }

            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
            }
            
        
        
        }

    }
}