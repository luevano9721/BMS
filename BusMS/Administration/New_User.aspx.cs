using BusManagementSystem._01Catalogos;
using BusManagementSystem.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace BusManagementSystem
{
    public partial class New_User : System.Web.UI.Page
    {
        List<Module_User> modulos = new List<Module_User>();
        List<Page_User> paginas = new List<Page_User>();
        List<Page_Privilege> privilegios = new List<Page_Privilege>();
        int actualPage;
        string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        string language = null;
        string msg_NewUser_AddedUser;
        string msg_NewUser_ConfirmPermissions;
        string msg_NewUse_NotHaveSelectedValues;
        string msg_NewUse_FielNotSelectValues;
        string msg_NewUse_PrivilegesUpdated;
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

            if (!this.IsPostBack)
            {
                fillRoleCombo();
                fillVendorCombo();
            }
            applyLanguage();
            messages_ChangeLanguage();
        }
        protected void messages_ChangeLanguage()
        {
            msg_NewUser_AddedUser = (string)GetGlobalResourceObject(language, "msg_NewUser_AddedUser");
            msg_NewUser_ConfirmPermissions = (string)GetGlobalResourceObject(language, "msg_NewUser_ConfirmPermissions");
            msg_NewUse_NotHaveSelectedValues = (string)GetGlobalResourceObject(language, "msg_NewUse_NotHaveSelectedValues");
            msg_NewUse_FielNotSelectValues = (string)GetGlobalResourceObject(language, "msg_NewUse_FielNotSelectValues");
            msg_NewUse_PrivilegesUpdated = (string)GetGlobalResourceObject(language, "msg_NewUse_PrivilegesUpdated");
            


        }
        private void applyLanguage()
        {
            Users usr = (Users)(Session["C_USER"]);
            functions func = new functions();
            language = Session["Language"] != null ? Session["Language"].ToString() : language = functions.GetLanguage(usr);
            func.languageTranslate(this.Master, language);
        }

        private void PopulateRootLevel(String rol)
        {
            try
            {
                DataTable dt = GenericClass.GetCustomData("SELECT [Rol_Module].[Rol_Module_ID],[Rol_Module].[Rol_ID],[Rol_Module].[Module_ID],[Module].[Module_Name]," +
            "(SELECT COUNT(*) FROM  [Rol_Module] WHERE [Rol_Module].[Rol_ID]=" + "'" + rol + "'" + ") childnodecount " +
            "FROM  [Rol_Module] LEFT JOIN  [Role] ON [Role].[Rol_ID]=[Rol_Module].[Rol_ID]" +
            "LEFT JOIN  [Module] ON [Module].[Module_ID]=[Rol_Module].[Module_ID]" +
            "WHERE [Role].[Rol_ID]=" + "'" + rol + "'");


                foreach (DataRow dr in dt.Rows)
                {
                    TreeNode tn = new TreeNode();
                    tn.Text = dr["Module_Name"].ToString();
                    tn.Value = dr["Module_ID"].ToString();
                    tn.Checked = true;
                    TreeView1.Nodes.Add(tn);

                    DataTable dt1 = GenericClass.GetCustomData("SELECT [Module_Page].[Page_ID] , [Pages].[Page_Name] " +
                    "FROM  [Module_Page] LEFT JOIN  [Pages] ON [Module_Page].[Page_ID]=[Pages].[Page_ID]" +
                    "WHERE [Module_Page].[Module_ID]=" + tn.Value);

                    foreach (DataRow dr2 in dt1.Rows)
                    {
                        TreeNode tn2 = new TreeNode();
                        tn2.Text = dr2["Page_Name"].ToString();
                        tn2.Value = dr2["Page_ID"].ToString();
                        tn2.Checked = true;
                        tn.ChildNodes.Add(tn2);

                        DataTable dtPrivileges = GenericClass.SQLSelectObj(new Privilege());
                        //string[] modules = ConfigurationManager.AppSettings["ModulesNoPermissions"].Split(',');
                        //foreach (string module in modules)
                        //{
                        //    if (tn.Text != module)
                        //    {
                        //        foreach (DataRow dr3 in dtPrivileges.Rows)
                        //        {
                        //            TreeNode tn3 = new TreeNode();
                        //            tn3.Text = dr3["Privilege_Name"].ToString();
                        //            tn3.Value = dr3["Privilege_ID"].ToString();
                        //            tn3.Checked = true;
                        //            tn2.ChildNodes.Add(tn3);
                        //        }
                        //    }
                        //}

                        if (tn.Text != "Activities" && tn.Text != "Reports" && tn.Text != "Blacklist" && tn.Text != "Administration")
                        {
                            foreach (DataRow dr3 in dtPrivileges.Rows)
                            {
                                TreeNode tn3 = new TreeNode();
                                tn3.Text = dr3["Privilege_Name"].ToString();
                                tn3.Value = dr3["Privilege_ID"].ToString();
                                tn3.Checked = true;
                                tn2.ChildNodes.Add(tn3);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        private void llenarCombos()
        {

        }

        private void fillVendorCombo()
        {
            try
            {
                //Fill vendor dropdownlist
                Vendor initialVendor = new Vendor();
                DataTable dtVendor = GenericClass.SQLSelectObj(initialVendor);
                cbVendor.Items.Clear();
                cbVendor.DataSource = dtVendor;
                cbVendor.DataValueField = "Vendor_ID";
                cbVendor.DataTextField = "Name";
                cbVendor.DataBind();
                ListItem all = new ListItem("Select a value", "NA");
                cbVendor.Items.Add(all);
                cbVendor.SelectedValue = "NA";
                cbVendor.Enabled = false;
            }

            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        private void fillRoleCombo()
        {
            try
            {
                //Fill roles dropdownlist
                Role role = new Role();
                DataTable dtRole = GenericClass.SQLSelectObj(role);
                cbRole.Items.Clear();
                ListItem i = new ListItem("Select a value", "NA");
                cbRole.Items.Add(i);
                cbRole.DataSource = dtRole;
                cbRole.DataValueField = "Rol_ID";
                cbRole.DataTextField = "Rol_ID";
                cbRole.DataBind();
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        private DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            try
            {

                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            sda.Fill(dt);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);

            }
            return dt;
        }

        private void getNodes()
        {

            TreeNodeCollection nodes = TreeView1.Nodes;

            foreach (TreeNode n in nodes)
            {
                if (n.Checked == false)
                {
                    Module_User moduloUser = new Module_User();
                    moduloUser.Module_ID = Int32.Parse(n.Value);
                    moduloUser.User_ID = labelUser.Text;
                    modulos.Add(moduloUser);

                }

                RecorrerNodos(n);
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
                        if (tn.Text != "Insert" && tn.Text != "Update" && tn.Text != "Delete" && tn.Text != "Export")
                        {
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
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void btAdd_Click(object sender, EventArgs e)
        {
            string vendor = cbVendor.SelectedValue.ToString();
            string role = cbRole.SelectedValue.ToString();

            if (role != "NA")
            {
                if (vendor != "NA")
                {
                    Users usuario = new Users();
                    Rol_User rs = new Rol_User();
                    try
                    {
                        DateTime dtBirtdate = new DateTime();
                        DateTime dtHireDate = new DateTime();
                        DateTime dtExpiration = new DateTime();
                        string[] formats = { "MM/dd/yyyy", "MM/dd/yy" };
                        usuario.User_ID = tbUsername.Text.Trim();
                        usuario.User_Name = tbName.Text.Trim();
                        usuario.Foxconn_ID = Int32.Parse(tbfoxconnID.Text.Trim());
                        usuario.Password = functions.GetSHA1(tbPassword.Text.Trim());
                        usuario.Department = tbDepartment.Text.Trim();

                        DateTime.TryParseExact(tbBirthdate.Text, formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtBirtdate);
                        usuario.Birthdate = dtBirtdate;
                        DateTime.TryParseExact(tbHireDate.Text, formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtHireDate);
                        usuario.Hiredate = dtHireDate;
                        DateTime.TryParseExact(tbExpiration.Text, formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtExpiration);
                        usuario.Expiration_Date = dtExpiration;

                        usuario.Address = tbAddress.Text.Trim();
                        usuario.Telephone = tbTelephone.Text.Trim();
                        usuario.Vendor_ID = cbVendor.SelectedValue.ToString();
                        usuario.Email = tbemail.Text.Trim();
                        usuario.Rol_ID = cbRole.SelectedValue.ToString();
                        usuario.Is_Active = Convert.ToBoolean(1);
                        usuario.Profile = cbProfile.SelectedValue.ToString();

                        rs.Rol_ID = cbRole.SelectedValue.ToString();
                        rs.User_ID = tbUsername.Text.Trim();

                        GenericClass.SQLInsertObj(usuario);
                        GenericClass.SQLInsertObj(rs);

                        PopulateRootLevel(usuario.Rol_ID);
                        TreeView1.CollapseAll();
                        lbl_NewUser_Info2.Text = "User ID " + usuario.User_ID + "-" + usuario.User_Name;
                        labelUser.Text = usuario.User_ID;
                        labelRole.Text = cbRole.SelectedValue.ToString();

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
                            lbrol.Text = "Rol: " + usuario.Rol_ID;
                        }
                        else
                        {
                            lbrol.Text = "Role: " + usuario.Rol_ID;
                        }



                        btSaveRol.Visible = true;
                        btnAdd.Enabled = false;
                        lbl_NewUser_Infor.Visible = false;
                        lbl_NewUser_Infor.Visible = false;
                        functions.ShowMessage(this, this.GetType(), msg_NewUser_AddedUser + " " + usuario.User_Name + " " + msg_NewUser_ConfirmPermissions, MessageType.Success);
                    }
                    catch (Exception ex)
                    {
                        functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);

                         functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
                    }
                }
                else
                    functions.ShowMessage(this, this.GetType(), msg_NewUse_NotHaveSelectedValues, MessageType.Error);

            }
            else
                functions.ShowMessage(this, this.GetType(), msg_NewUse_FielNotSelectValues, MessageType.Error);



        }


        protected void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btSaveRol_Click(object sender, EventArgs e)
        {

            getNodes();
            updatePermissions();
        }

        protected void updatePermissions()
        {
            try
            {

                //Update modulos
                foreach (var itemModulo in modulos)
                {
                    Module_User updateModule = new Module_User();
                    Dictionary<string, dynamic> updModule = new Dictionary<string, dynamic>();
                    updModule.Add("Is_Active", 0);

                    GenericClass.SQLUpdateObj(updateModule, updModule, WhereClause: "WHERE user_ID='" + itemModulo.User_ID + "' and Module_ID=" + itemModulo.Module_ID);

                }
                //Update paginas
                foreach (var itemPaginas in paginas)
                {

                    Page_User updatePageUser = new Page_User();
                    Dictionary<string, dynamic> updPageUser = new Dictionary<string, dynamic>();
                    updPageUser.Add("Is_Active", 0);

                    GenericClass.SQLUpdateObj(updatePageUser, updPageUser, WhereClause: "WHERE user_ID='" + itemPaginas.User_ID + "' and Page_ID=" + itemPaginas.Page_ID);

                }

                //Update privilegios
                foreach (var itemPrivilegios in privilegios)
                {
                    Page_Privilege updatePageUser = new Page_Privilege();
                    Dictionary<string, dynamic> updPageUser = new Dictionary<string, dynamic>();
                    updPageUser.Add("Is_Active", 0);

                    GenericClass.SQLUpdateObj(updatePageUser, updPageUser, WhereClause: "WHERE user_ID='" + itemPrivilegios.User_ID + "' and Page_ID=" + itemPrivilegios.Page_ID + "and  Privilege_ID=" + itemPrivilegios.Privilege_ID);

                }

                functions.ShowMessage(this, this.GetType(), msg_NewUse_PrivilegesUpdated, MessageType.Success);
                HtmlMeta meta = new HtmlMeta();
                meta.HttpEquiv = "Refresh";
                meta.Content = "5;url=View_Users.aspx";
                this.Page.Controls.Add(meta);

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void cbProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbProfile.SelectedValue == "Internal")
                {
                    cbVendor.Items.Clear();
                    ListItem all = new ListItem("ALL", "ALL");
                    cbVendor.Items.Add(all);
                    fillRoleCombo();

                }
                else
                {

                    fillVendorCombo();
                    cbVendor.Enabled = true;

                    ListItem admin = new ListItem("ADMIN", "ADMIN");
                    if (cbRole.Items.Contains(admin))
                    {
                        cbRole.Items.Remove(admin);
                    }
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btHelp_Click(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("./Login.aspx");
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Operación diaria')", true);
        }

    }
}