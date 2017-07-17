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
    public partial class Edit_User : System.Web.UI.Page
    {
        Users usr;
        string usuario;
        string vendorID;
        bool changeRol = false;
        string language = null;
        string msg_EditUser_UpdateUser;
        string msg_EditUser_Successfully;
        string msg_EditUser_WaitForAdmin;
        string msg_EditUser_UpdatedPrivi;
        string msg_InvalidUser;
        List<Module_User> modulos = new List<Module_User>();
        List<Page_User> paginas = new List<Page_User>();
        List<Page_Privilege> privilegios = new List<Page_Privilege>();
        int actualPage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (Request.QueryString["User_ID"] == null)
            {
                Response.Redirect("View_Users.aspx");
            }


            try
            {
                usuario = Request.QueryString["User_ID"];

                usr = (Users)(Session["C_USER"]);


                if (!usr.Rol_ID.Contains("ADMIN"))
                {
                    Response.Redirect("~/MenuPortal.aspx");
                }
                Users initialUser = new Users();

                DataTable dtusers = GenericClass.SQLSelectObj(initialUser, WhereClause: "WHERE User_ID=" + "'" + usuario + "'");


                if (dtusers.Rows.Count>0)
                {


                    if (!IsPostBack)
                    {

                        
                        tbUsername.Text =HttpUtility.HtmlDecode(dtusers.Rows[0]["User_ID"].ToString());
                        
                        tbName.Text = HttpUtility.HtmlDecode(dtusers.Rows[0]["User_Name"].ToString());
                        
                        tbfoxconnID.Text = HttpUtility.HtmlDecode(dtusers.Rows[0]["Foxconn_ID"].ToString());
                        
                        tbDepartment.Text = HttpUtility.HtmlDecode(dtusers.Rows[0]["Department"].ToString());
                        
                        tbBirthdate.Text = HttpUtility.HtmlDecode(dtusers.Rows[0]["Birthdate(M/D/Y)"].ToString());
                        
                        tbHireDate.Text = HttpUtility.HtmlDecode(dtusers.Rows[0]["Hiredate(M/D/Y)"].ToString());
                        
                        tbAddress.Text = HttpUtility.HtmlDecode(dtusers.Rows[0]["Address"].ToString());
                        
                        tbTelephone.Text = HttpUtility.HtmlDecode(dtusers.Rows[0]["Telephone"].ToString());
                        
                        tbExpiration.Text = HttpUtility.HtmlDecode(dtusers.Rows[0]["Expiration_Date(M/D/Y)"].ToString());
                        
                        cbProfile.SelectedValue = HttpUtility.HtmlDecode(dtusers.Rows[0]["Profile"].ToString());

                        vendorID = HttpUtility.HtmlDecode(dtusers.Rows[0]["Vendor_ID"].ToString());
                        
                        llenarCbRoleIni(dtusers.Rows[0]["Rol_ID"].ToString());
                        
                        llenarCbVendorIni(dtusers.Rows[0]["Vendor_ID"].ToString());
                        
                        tbemail.Text = dtusers.Rows[0]["Email"].ToString();
                        
                        if (Convert.ToBoolean(dtusers.Rows[0]["Is_Active"]) == true)
                            checkEnabled.Checked = true;
                        if (Convert.ToBoolean(dtusers.Rows[0]["Is_Block"]) == true)
                            checkBlacklist.Checked = true;

                        PopulateRootLevel(usuario);
                        TreeView1.CollapseAll();
                    } 
                }
                else
                {

                    Response.Redirect("View_Users.aspx");
                    
                }
                applyLanguage();
                messages_ChangeLanguage();
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        private void applyLanguage()
        {
            Users usr = (Users)(Session["C_USER"]);
            functions func = new functions();
            language = Session["Language"] != null ? Session["Language"].ToString() : language = functions.GetLanguage(usr);
            func.languageTranslate(this.Master, language);
        }
        protected void messages_ChangeLanguage()
        {
            msg_EditUser_UpdateUser = (string)GetGlobalResourceObject(language, "msg_EditUser_UpdateUser");
            msg_EditUser_Successfully = (string)GetGlobalResourceObject(language, "msg_EditUser_Successfully");
            msg_EditUser_WaitForAdmin = (string)GetGlobalResourceObject(language, "msg_EditUser_WaitForAdmin");
            msg_EditUser_UpdatedPrivi = (string)GetGlobalResourceObject(language, "msg_EditUser_UpdatedPrivi");
            msg_InvalidUser = (string)GetGlobalResourceObject(language, "msg_InvalidUser");
        }
        private void llenarCbVendorIni(string vendor)
        {
            if (usr.Vendor_ID == "ALL")
            {
                if (vendor == "ALL" || cbRole.SelectedValue.ToString().Equals("ADMIN"))
                {
                    ListItem vendorAll = new ListItem(vendor);
                    cbVendor.Items.Add(vendorAll);
                }
                else
                {
                    Vendor initialVendor = new Vendor();
                    DataTable dtVendor = GenericClass.SQLSelectObj(initialVendor);
                    cbVendor.Items.Clear();
                    cbVendor.DataSource = dtVendor;
                    cbVendor.DataValueField = "Vendor_ID";
                    cbVendor.DataTextField = "Name";
                }

                cbVendor.SelectedValue = vendor;
                cbVendor.DataBind();
            }
            else
            {
                cbVendor.Items.Clear();
                ListItem current = new ListItem(vendor);
                cbVendor.Items.Add(current);
            }
        }

        private void llenarCbRoleIni(string role)
        {
            try
            {
                if (usr.Rol_ID.Contains("ADMIN"))
                {
                    Role rol = new Role();
                    DataTable dtRole = GenericClass.SQLSelectObj(rol);
                    cbRole.Items.Clear();
                    cbRole.DataSource = dtRole;
                    cbRole.DataValueField = "Rol_ID";
                    cbRole.DataTextField = "Rol_ID";
                    cbRole.SelectedValue = role;
                    cbRole.DataBind();
                }
                else
                {
                    cbRole.Items.Clear();
                    ListItem currentRole = new ListItem(role);
                    cbRole.Items.Add(currentRole);
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }


        private void PopulateRootLevel(string userid)
        {
            try
            {
                string GetLanguage = functions.GetLanguage(usr);
                if (Session["Language"] != null)
                {
                    GetLanguage = Session["Language"].ToString();
                }

                DataTable dt = GenericClass.GetCustomData("SELECT [Module_User].[Module_ID] ,[Module].[Module_Name] ,[Module_User].[Is_Active], [Module].[Module_Name_ESP] FROM [Module_User]" +
                "LEFT JOIN [Module] ON [Module_User].Module_ID=[Module].[Module_ID]" +
                "WHERE [Module_User].User_ID='" + usuario + "'");

                
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
                    if (Convert.ToBoolean(dr["Is_Active"]) == true) { tn.Checked = true; }
                    TreeView1.Nodes.Add(tn);

                    DataTable dt1 = GenericClass.GetCustomData("SELECT [Page_User].[Page_ID] ,[Pages].[Page_Name] ,[Page_User].[Is_Active],[Pages].[Page_Name_ESP] FROM [Page_User]" +
                    "LEFT JOIN [Pages] ON [Page_User].[Page_ID]=[Pages].[Page_ID]" +
                    "LEFT JOIN [Module_Page] ON [Page_User].[Page_ID]= [Module_Page].[Page_ID]" +
                    "WHERE [Page_User].[User_ID]='" + usuario + "'" + "and [Module_Page].[Module_ID]=" + tn.Value);

                    foreach (DataRow dr2 in dt1.Rows)
                    {
                        string pageName = dr2["Page_Name"].ToString();
                        if (GetLanguage.ToUpper().Trim() == "ESP")
                        {
                            pageName = dr2["Page_Name_ESP"].ToString();
                        }

                        TreeNode tn2 = new TreeNode();
                        tn2.Text = pageName;
                        tn2.Value = dr2["Page_ID"].ToString();
                        if (Convert.ToBoolean(dr2["Is_Active"]) == true) { tn2.Checked = true; }

                        tn.ChildNodes.Add(tn2);

                        DataTable dtPrivileges = GenericClass.GetCustomData("SELECT [Page_Privilege].[Privilege_ID], [Privilege].[Privilege_Name] , [Page_Privilege].[Is_Active]" +
                        "FROM  [Page_Privilege]" +
                        "LEFT JOIN [Privilege] ON [Page_Privilege].[Privilege_ID]= [Privilege].[Privilege_ID]" +
                        "WHERE [Page_Privilege].[User_ID]='" + usuario + "'" + "AND  [Page_Privilege].[Page_ID]=" + tn2.Value);

                        foreach (DataRow dr3 in dtPrivileges.Rows)
                        {
                            TreeNode tn3 = new TreeNode();
                            tn3.Text = dr3["Privilege_Name"].ToString();
                            tn3.Value = dr3["Privilege_ID"].ToString();
                            if (Convert.ToBoolean(dr3["Is_Active"]) == true) { tn3.Checked = true; }

                            tn2.ChildNodes.Add(tn3);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
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
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);

            }
            return dt;
        }
        private void getNodes()
        {

            TreeNodeCollection nodes = TreeView1.Nodes;

            foreach (TreeNode n in nodes)
            {
                Module_User moduloUser = new Module_User();
                moduloUser.Module_ID = Int32.Parse(n.Value);
                moduloUser.User_ID = usuario;
                moduloUser.Is_Active = n.Checked;
                modulos.Add(moduloUser);

                RecorrerNodos(n);
            }

        }

        private void RecorrerNodos(TreeNode treeNode)
        {
            try
            {

                foreach (TreeNode tn in treeNode.ChildNodes)
                {
                    if (tn.Text != "Insert" && tn.Text != "Update" && tn.Text != "Delete" && tn.Text != "Export")
                        
                    {//Si tiene hijos entonces se trata de una pagina
                        Page_User paginaUser = new Page_User();
                        paginaUser.Page_ID = Int32.Parse(tn.Value);
                        paginaUser.User_ID = usuario;
                        paginaUser.Is_Active = tn.Checked;
                        paginas.Add(paginaUser);
                        actualPage = Int32.Parse(tn.Value);

                    }
                    else
                    {
                       
                            Page_Privilege pagePrivilege = new Page_Privilege();
                            pagePrivilege.Page_ID = actualPage;
                            pagePrivilege.Privilege_ID = Int32.Parse(tn.Value);
                            pagePrivilege.User_ID = usuario;
                            pagePrivilege.Is_Active = tn.Checked;
                            privilegios.Add(pagePrivilege);
                        
                    }


                    RecorrerNodos(tn);
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void updatePermissions()
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                //Update modulos
                foreach (var itemModulo in modulos)
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE Module_User SET Is_Active=@Is_active WHERE user_ID=@user_ID and Module_ID=@Module_ID", con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@user_ID", itemModulo.User_ID);
                            cmd.Parameters.AddWithValue("@Module_ID", itemModulo.Module_ID);
                            cmd.Parameters.AddWithValue("@Is_active", itemModulo.Is_Active);
                            con.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                //Update paginas
                foreach (var itemPaginas in paginas)
                {

                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE Page_User SET Is_Active=@Is_active WHERE user_ID=@user_ID and Page_ID=@Page_ID", con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@user_ID", itemPaginas.User_ID);
                            cmd.Parameters.AddWithValue("@Page_ID", itemPaginas.Page_ID);
                            cmd.Parameters.AddWithValue("@Is_active", itemPaginas.Is_Active);
                            con.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }

                //Update privilegios
                foreach (var itemPrivilegios in privilegios)
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE Page_Privilege SET Is_Active=@Is_active WHERE user_ID=@user_ID and Page_ID=@Page_ID and  Privilege_ID=@Privilege_ID", con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@user_ID", itemPrivilegios.User_ID);
                            cmd.Parameters.AddWithValue("@Page_ID", itemPrivilegios.Page_ID);
                            cmd.Parameters.AddWithValue("@Privilege_ID", itemPrivilegios.Privilege_ID);
                            cmd.Parameters.AddWithValue("@Is_active", itemPrivilegios.Is_Active);
                            con.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            Users updateUser = new Users();
            Users initialUser = new Users();
            Rol_User rs = new Rol_User();

            try
            {
                getNodes();
                updatePermissions();
                string[] formats = { "MM/dd/yyyy", "MM/dd/yy" };
                //current user
                //Create and fill current bus object
                DateTime dtCurrentBirtdate = new DateTime();
                DateTime dtCurrentHireDate = new DateTime();
                DateTime dtCurrentExpiration = new DateTime();
                Admin_Approve newApprove = new Admin_Approve();
                DataTable getUser = GenericClass.SQLSelectObj(initialUser, WhereClause: "Where [Users].[User_ID]= '" + usuario + "'");
                Users currentUser = new Users();
                DateTime.TryParseExact(getUser.Rows[0]["Birthdate(M/D/Y)"].ToString(), formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtCurrentBirtdate);
                DateTime.TryParseExact(getUser.Rows[0]["Expiration_date(M/D/Y)"].ToString(), formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtCurrentExpiration);
                DateTime.TryParseExact(getUser.Rows[0]["Hiredate(M/D/Y)"].ToString(), formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtCurrentHireDate);

                currentUser.User_ID = getUser.Rows[0]["User_ID"].ToString();
                currentUser.User_Name = getUser.Rows[0]["User_Name"].ToString();
                currentUser.Foxconn_ID = Convert.ToInt32(getUser.Rows[0]["Foxconn_ID"]);
                currentUser.Department = getUser.Rows[0]["Department"].ToString();
                currentUser.Birthdate = dtCurrentBirtdate;
                currentUser.Hiredate = dtCurrentHireDate;
                currentUser.Expiration_Date = dtCurrentExpiration;
                currentUser.Rol_ID = getUser.Rows[0]["Rol_ID"].ToString();
                currentUser.Address = getUser.Rows[0]["Address"].ToString();
                currentUser.Telephone = getUser.Rows[0]["Telephone"].ToString();
                currentUser.Vendor_ID = getUser.Rows[0]["Vendor_ID"].ToString();
                currentUser.Email = getUser.Rows[0]["Email"].ToString();
                currentUser.Profile = getUser.Rows[0]["Profile"].ToString();
                currentUser.Is_Active = Convert.ToBoolean(getUser.Rows[0]["Is_Active"].ToString());
                currentUser.Is_Block = Convert.ToBoolean(getUser.Rows[0]["Is_Block"].ToString());



                DateTime dtBirtdate = new DateTime();
                DateTime dtHireDate = new DateTime();
                DateTime dtExpiration = new DateTime();
                updateUser.User_ID = usuario;
                updateUser.User_Name = tbName.Text.Trim();
                updateUser.Foxconn_ID = Int32.Parse(tbfoxconnID.Text.Trim());
                updateUser.Department = tbDepartment.Text.Trim();

                DateTime.TryParseExact(tbBirthdate.Text, formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtBirtdate);
                updateUser.Birthdate = dtBirtdate;
                DateTime.TryParseExact(tbHireDate.Text, formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtHireDate);
                updateUser.Hiredate = dtHireDate;
                DateTime.TryParseExact(tbExpiration.Text, formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtExpiration);
                updateUser.Expiration_Date = dtExpiration;

                updateUser.Address = tbAddress.Text.Trim();
                updateUser.Telephone = tbTelephone.Text.Trim();
                updateUser.Vendor_ID = cbVendor.SelectedValue.ToString();
                updateUser.Email = tbemail.Text.Trim();
                updateUser.Rol_ID = cbRole.SelectedValue.ToString();
                updateUser.Profile = cbProfile.SelectedValue.ToString();
                updateUser.Is_Active = checkEnabled.Checked;
                updateUser.Is_Block = checkBlacklist.Checked;

                if (currentUser.Rol_ID != cbRole.SelectedValue.ToString())
                {
                    changeRol = true;
                    updateUser.Rol_ID = cbRole.SelectedValue.ToString();
                    rs.Rol_ID = cbRole.SelectedValue.ToString();
                    rs.User_ID = currentUser.User_ID;
                }

                newApprove = adminApprove.compareObjects(currentUser, updateUser);


                if (newApprove.New_Values != "No values changed")
                {
                    if (usr.Rol_ID.Contains("ADMIN"))
                    {

                        GenericClass.SQLUpdateObj(updateUser, adminApprove.compareObjects(currentUser, updateUser, ""), "Where User_ID='" + updateUser.User_ID + "'");
                        if (changeRol == true)
                        {
                            Dictionary<string, dynamic> userRol = new Dictionary<string, dynamic>();
                            userRol.Add("Rol_ID", rs.Rol_ID);
                            GenericClass.SQLUpdateObj(rs, userRol, WhereClause: "Where User_ID='" + updateUser.User_ID + "'");
                        }

                        functions.ShowMessage(this, this.GetType(), msg_EditUser_UpdateUser + updateUser.User_ID + "-" + updateUser.User_Name + msg_EditUser_Successfully, MessageType.Success);

                    }
                    else
                    {
                        newApprove.Activity_ID = 0;
                        newApprove.Admin_Confirm = false;
                        newApprove.Activity_Date = DateTime.Now;
                        newApprove.User_ID = usr.User_ID;
                        newApprove.Type = "UPDATE";
                        newApprove.Module = updateUser.GetType().Name;
                        newApprove.Where_Clause = "Where Driver_ID='" + updateUser.User_ID + "'";
                        newApprove.Comments = "[User][" + updateUser.User_Name + "]<br>" + newApprove.Comments;

                        GenericClass.SQLInsertObj(newApprove);

                        functions.ShowMessage(this, this.GetType(), msg_EditUser_WaitForAdmin + currentUser.User_ID + "-" + currentUser.User_Name, MessageType.Warning);

                    }

                }

                else
                {
                    functions.ShowMessage(this, this.GetType(), msg_EditUser_UpdatedPrivi, MessageType.Info);
                }

                HtmlMeta meta = new HtmlMeta();
                meta.HttpEquiv = "Refresh";
                meta.Content = "2;url=View_Users.aspx";
                this.Page.Controls.Add(meta);

            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRole.SelectedValue.ToString().Equals("ADMIN")|| cbProfile.SelectedValue.ToString().Equals("INTERNAL"))
            {
                cbVendor.Items.Clear();
                ListItem n = new ListItem("ALL");
                cbVendor.Items.Add(n);

            }
            else
            {
                llenarCbVendorIni(vendorID);

            }

        }

        protected void cbProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbProfile.SelectedValue == "INTERNAL")
                {
                    cbVendor.Items.Clear();
                    ListItem all = new ListItem("ALL", "ALL");
                    cbVendor.Items.Add(all);
                    Role rol = new Role();
                    DataTable dtRole = GenericClass.SQLSelectObj(rol);
                    cbRole.Items.Clear();
                    cbRole.DataSource = dtRole;
                    cbRole.DataValueField = "Rol_ID";
                    cbRole.DataTextField = "Rol_ID";

                    cbRole.DataBind();
                    cbVendor.Enabled = false;

                }
                else
                {
                    //  Fill vendor dropdownlist
                    Vendor initialVendor = new Vendor();
                    DataTable dtVendor = GenericClass.SQLSelectObj(initialVendor);
                    cbVendor.Items.Clear();
                    cbVendor.DataSource = dtVendor;
                    cbVendor.DataValueField = "Vendor_ID";
                    cbVendor.DataTextField = "Name";
                    cbVendor.DataBind();
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
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void btHelp_Click(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("./Login.aspx");
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Editar usuario')", true);
        }



    }
}