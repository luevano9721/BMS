using BusManagementSystem._01Catalogos;
using BusManagementSystem.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace BusManagementSystem.Administration
{
    public partial class New_Rol : System.Web.UI.Page
    {
        List<Rol_Module> rol_module = new List<Rol_Module>();
        List<Module_User> module = new List<Module_User>();
        List<Module_User> modulos = new List<Module_User>();
        List<Page_User> paginas = new List<Page_User>();
        List<Page_Privilege> privilegios = new List<Page_Privilege>();
        int actualPage;
        string language = null;
        string msg_NewRol_PrivilegesUpdatedSuccrssfully;
        string msg_NewRol_AddedRole;
        string msg_NewRol_ConfirmPrivileges;
        string msg_NewRol_RemoveRole;
        string msg_NewRol_Successfully;
        string msg_NewRol_TheRole;
        string msg_NewRol_Have;
        string msg_NewRol_CannotRemove;
        static string pattern = "[~#%&*{}?'\"]";
        static string valueToReplace = "";
        Regex regEx = new Regex(pattern);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            Users usr = (Users)(Session["C_USER"]);

            if (!usr.Rol_ID.Contains("ADMIN"))
            {
                Response.Redirect("~/MenuPortal.aspx");
            }
            DataTable dt = GenericClass.SQLSelectObj(new Role());

            GridView_roles.DataSource = dt;
            GridView_roles.DataBind();
            applyLanguage();
            messages_ChangeLanguage();
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
            msg_NewRol_PrivilegesUpdatedSuccrssfully = (string)GetGlobalResourceObject(language, "msg_NewRol_PrivilegesUpdatedSuccrssfully");
            msg_NewRol_AddedRole = (string)GetGlobalResourceObject(language, "msg_NewRol_AddedRole");
            msg_NewRol_ConfirmPrivileges = (string)GetGlobalResourceObject(language, "msg_NewRol_ConfirmPrivileges");
            msg_NewRol_RemoveRole = (string)GetGlobalResourceObject(language, "msg_NewRol_RemoveRole");
            msg_NewRol_Successfully = (string)GetGlobalResourceObject(language, "msg_NewRol_Successfully");
            msg_NewRol_TheRole = (string)GetGlobalResourceObject(language, "msg_NewRol_TheRole");
            msg_NewRol_Have = (string)GetGlobalResourceObject(language, "msg_NewRol_Have");
            msg_NewRol_CannotRemove = (string)GetGlobalResourceObject(language, "msg_NewRol_CannotRemove");
          

        }
        private void PopulateRootLevelEdith()
        {
            try
            {
                Users usr = (Users)(Session["C_USER"]);
                string GetLanguage = functions.GetLanguage(usr);
                if (Session["Language"] != null)
                {
                    GetLanguage = Session["Language"].ToString();
                }

                TreeView1.Nodes.Clear();
                DataTable dt = GenericClass.SQLSelectObj(new Module());
                DataTable dt_rolModule = GenericClass.SQLSelectObj(new Rol_Module(), WhereClause: " WHERE [Rol_ID] = '" + tbRolID.Text.ToString() + "'");

                foreach (DataRow dr in dt.Rows)
                {
                    string moduleName = dr["Module_Name"].ToString();
                    if (GetLanguage.ToUpper().Trim() == "ESP")
                    {
                        moduleName = dr["Module_Name_ESP"].ToString();
                    }

                    TreeNode tn = new TreeNode();
                    tn.Text = moduleName;
                    tn.Value = dr["Module_ID"].ToString();

                    tn.Checked = false;
                    foreach (DataRow drEdith in dt_rolModule.Rows)
                    {
                        if (dr["Module_ID"].ToString() == drEdith["Module_ID"].ToString())
                        {
                            tn.Checked = true;
                            break;
                        }
                    }
                    TreeView1.Nodes.Add(tn);
                }
            }
            catch (Exception ex)
            {
               

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        private void PopulateRootLevel()
        {
            try
            {
                DataTable dt = GenericClass.SQLSelectObj(new Module());

                foreach (DataRow dr in dt.Rows)
                {
                    TreeNode tn = new TreeNode();
                    tn.Text = dr["Module_Name"].ToString();
                    tn.Value = dr["Module_ID"].ToString();
                    tn.Checked = true;
                    TreeView1.Nodes.Add(tn);

                }
            }
            catch (Exception ex)
            {
               

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }



        protected void btSaveRol_Click(object sender, EventArgs e)
        {
            try
            {
                getNodes();
                updatePermissions();


                menu("100");
                TreeView1.Nodes.Clear();

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
                if (Language == "Esp")
                {
                    labelRole.Text = "Editar rol de permisos: " + tbRolID.Text;
                }
                else
                {
                    labelRole.Text = "Edit Role Permisions: " + tbRolID.Text;
                }



                labelRole.Visible = true;
                lbl_NewRol_Info.Visible = true;
                lbl_NewRol_Info2.Visible = true;
                btSaveRolPermissions.Visible = false;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void updatePermissions()
        {
            try
            {
                DataTable dt_AllModules = GenericClass.SQLSelectObj(new Module());
                List<string> ModForDel = new List<string>();

                foreach (DataRow dr in dt_AllModules.Rows)
                {
                    Rol_Module rm = rol_module.Find(find => find.Module_ID.ToString() == dr["Module_ID"].ToString());

                    if (rm == null)
                    {
                        ModForDel.Add(dr["Module_ID"].ToString());
                    }
                }

                foreach (string md in ModForDel)
                {
                    int modulo = 0;
                    int.TryParse(md, out modulo);
                    GenericClass.SQLDeleteObj(new Rol_Module(), "WHERE Rol_ID='" +Regex.Replace(regEx.Replace(tbRolID.Text, valueToReplace), pattern, "")+ "' and Module_ID = " + modulo);
                }


                //Insert modulos
                foreach (var itemModulo in rol_module)
                {


                    DataTable dt_CheckIsExist = GenericClass.SQLSelectObj(new Rol_Module(), WhereClause: "where rol_ID = '" + itemModulo.Rol_ID +
                        "' and  Module_ID=" + itemModulo.Module_ID);

                    if (dt_CheckIsExist.Rows.Count <= 0)
                    {
                        Rol_Module insertRolModule = new Rol_Module();
                        insertRolModule.Rol_ID = itemModulo.Rol_ID;
                        insertRolModule.Module_ID = itemModulo.Module_ID;
                        GenericClass.SQLInsertObj(insertRolModule);
                    }

                }
                functions.ShowMessage(this, this.GetType(), msg_NewRol_PrivilegesUpdatedSuccrssfully, MessageType.Success);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void getNodes()
        {

            TreeNodeCollection nodes = TreeView1.Nodes;
            foreach (TreeNode n in nodes)
            {
                if (n.Checked == true)
                {
                    Rol_Module rolMod = new Rol_Module();
                    rolMod.Module_ID = Int32.Parse(n.Value);
                    rolMod.Rol_ID = tbRolID.Text.ToString();
                    rol_module.Add(rolMod);
                    RecorrerNodos(n);
                }
            }
        }

        private void RecorrerNodos(TreeNode treeNode)
        {
            try
            {

                foreach (TreeNode tn in treeNode.ChildNodes)
                {

                    if (tn.Checked == false)
                    {
                        if (tn.ChildNodes.Count > 0)
                        {//Si tiene hijos entonces se trata de una pagina
                            Page_User paginaUser = new Page_User();
                            paginaUser.Page_ID = Int32.Parse(tn.Value);
                            paginaUser.User_ID = labelUser.Text;
                            paginas.Add(paginaUser);
                            actualPage = Int32.Parse(tn.Value);

                        }
                        else
                        {
                            //Si no tiene hijos entonces es un privilegio
                            Page_Privilege pagePrivilege = new Page_Privilege();
                            pagePrivilege.Page_ID = actualPage;
                            pagePrivilege.Privilege_ID = Int32.Parse(tn.Value);
                            pagePrivilege.User_ID = labelUser.Text;
                            privilegios.Add(pagePrivilege);
                        }
                    }

                    RecorrerNodos(tn);
                }
            }
            catch (Exception ex)
            {
 

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void GridView_roles_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = GridView_roles.SelectedRow;

            tbRolID.Text = HttpUtility.HtmlDecode(GridView_roles.Rows[gvRow.RowIndex].Cells[1].Text);
            try
            {
                menu("0011");
                PopulateRootLevelEdith();

                string rolText = Regex.Replace(regEx.Replace(tbRolID.Text, valueToReplace), pattern, "");

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
                if (Language == "Esp")
                {
                    labelRole.Text = "Editar rol de permisos: " + rolText;
                }
                else
                {
                    labelRole.Text = "Edit Role Permisions: " + rolText;
                }



                labelRole.Visible = true;
                lbl_NewRol_Info.Visible = false;
                lbl_NewRol_Info2.Visible = false;
            }
            catch (Exception ex)
            {
                

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void GridView_buses_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void GridView_roles_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridView_roles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_roles.PageIndex = e.NewPageIndex;
            GridView_roles.DataBind();
        }

        protected void GridView_roles_DataBound(object sender, EventArgs e)
        {

        }

        protected void btSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Role rolObj = new Role();
                rolObj.Rol_ID =HttpUtility.HtmlDecode(tbRolID.Text.ToString());
                GenericClass.SQLInsertObj(rolObj);
                functions.ShowMessage(this, this.GetType(), msg_NewRol_AddedRole + rolObj.Rol_ID.ToString() + msg_NewRol_ConfirmPrivileges, MessageType.Success);
                PopulateRootLevel();
                String Language = null;
                Users usr = (Users)(Session["C_USER"]);
                functions func = new functions();
                string rolText = Regex.Replace(regEx.Replace(tbRolID.Text, valueToReplace), pattern, "");
                if (Session["Language"] != null)
                {
                    Language = Session["Language"].ToString();
                }
                else
                {
                    Language = functions.GetLanguage(usr);
                }
                if (Language == "Esp")
                {
                    labelRole.Text = "Editar rol de permisos: " + rolText;
                }
                else
                {
                    labelRole.Text = "Edit Role Permisions: " + rolText;
                }
                labelRole.Visible = true;
                btSaveRolPermissions.Visible = true;
                lbl_NewRol_Info.Visible = false;
                lbl_NewRol_Info2.Visible = false;

                menu("0011");
            }
            catch (Exception ex)
            {

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btAdd_Click(object sender, EventArgs e)
        {
            menu("0110");
            lbl_NewRol_Info.Visible = true;
            lbl_NewRol_Info2.Visible = true;
        }

        /// <summary>
        /// Add,Reset,Edit,Save,Delete
        /// </summary>
        /// <param name="binary"></param>
        private void menu(string binary)
        {
            this.btAdd.Visible = binary.ToString().Substring(0, 1) == "1";
            this.btSave.Visible = binary.ToString().Substring(1, 1) == "1";
            this.btReset.Visible = binary.ToString().Substring(2, 1) == "1";
            this.btDelete.Visible = binary.ToString().Substring(3, 1) == "1";

            if (this.btAdd.Visible == false
                && this.btSave.Visible == true)
            {
                tbRolID.Text = string.Empty;
                tbRolID.Enabled = true;
                tbRolID.Focus();
            }
            if (this.btAdd.Visible == true
                && this.btSave.Visible == false)
            {
                tbRolID.Text = string.Empty;
                tbRolID.Enabled = false;
            }
            if (this.btAdd.Visible == false
                && this.btSave.Visible == false)
            {
                btSaveRolPermissions.Visible = true;
                tbRolID.Enabled = false;
            }

        }



        protected void btReset_Click(object sender, EventArgs e)
        {
            menu("1000");
            TreeView1.Nodes.Clear();
            labelRole.Visible = false;
            lbl_NewRol_Info.Visible = true;
            lbl_NewRol_Info2.Visible = true;
            btSaveRolPermissions.Visible = false;
        }

        protected void btSaveRolPermissions_Click(object sender, EventArgs e)
        {
            try
            {
                getNodes();
                updatePermissions();


                menu("1000");
                TreeView1.Nodes.Clear();
                labelRole.Visible = false;
                lbl_NewRol_Info.Visible = true;
                lbl_NewRol_Info2.Visible = true;
                btSaveRolPermissions.Visible = false;
            }
            catch (Exception ex)
            {


                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "", true);           

            Role rolObj = new Role();
            string role = Regex.Replace(regEx.Replace(tbRolID.Text, valueToReplace), pattern, "");
            rolObj.Rol_ID = role;
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
            if (Language == "Esp")
            {
                labelRole.Text = "Rol eliminado: " + tbRolID.Text;
            }
            else
            {
                labelRole.Text = "Removed Role: " + tbRolID.Text;
            }


            try
            {
                DataTable dtExitsUser = GenericClass.SQLSelectObj(new Rol_User(), WhereClause: "WHERE [Rol_ID] = '" + role + "'");


                if (dtExitsUser.Rows.Count < 1)
                {

                    GenericClass.SQLDeleteObj(rolObj, "Where Rol_ID='" + rolObj.Rol_ID + "'");

                    GridView_roles.DataSource = GenericClass.SQLSelectObj(new Role());
                    GridView_roles.DataBind();

                    functions.ShowMessage(this, this.GetType(), msg_NewRol_RemoveRole+" " + rolObj.Rol_ID.ToString() + msg_NewRol_Successfully, MessageType.Success);

                }
                else
                {
                    functions.ShowMessage(this, this.GetType(), msg_NewRol_TheRole +" "+ rolObj.Rol_ID.ToString() +" "+ msg_NewRol_Have +" "+ dtExitsUser.Rows.Count.ToString() +" "+ msg_NewRol_CannotRemove, MessageType.Info);
                    labelRole.Visible = false;
                    lbl_NewRol_Info.Visible = true;
                    lbl_NewRol_Info2.Visible = true;
                }
                TreeView1.Nodes.Clear();
                menu("1000");
                btSaveRolPermissions.Visible = false;
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); 
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btHelp_Click(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("./Login.aspx");
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Roles')", true);
        }




    }
}