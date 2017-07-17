using BusManagementSystem._01Catalogos;
using BusManagementSystem.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusManagementSystem
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        Users initialUser = new Users();
        string language = null;
        string msg_ChangePassword_UpdatePassword;
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
                    DataTable dtUsers = GenericClass.SQLSelectObj(initialUser);
                    dtUsers.Columns.Add("IdName", typeof(string), "User_Id +'-' + User_Name");
                    cbUsers.DataSource = dtUsers;
                    cbUsers.DataValueField = "User_Id";
                    cbUsers.DataTextField = "IdName";
                    cbUsers.DataBind();
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
        protected void btSelectUser_Click(object sender, EventArgs e)
        {
            try
            {
                string idUser = cbUsers.SelectedValue.ToString();
                DataTable dtUsers = GenericClass.SQLSelectObj(initialUser, WhereClause: "WHERE User_Id='" + idUser + "'");
                lbUserId.Text = dtUsers.Rows[0]["User_ID"].ToString();
                lbusername.Text = dtUsers.Rows[0]["User_Name"].ToString();
                //lbCurrentPassword.Text = dtUsers.Rows[0]["Password"].ToString();
               
                lbExpirationDate.Text = dtUsers.Rows[0]["Expiration_Date(M/D/Y)"].ToString();
                btPassword.Enabled = true;
                newPassword.Enabled = true;
                confirmPassword.Enabled = true;
                tbExpirationDate.Enabled = true;

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
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Cambiar Password de Usuario')", true);
        }
        protected void cbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            limpiar();
        }

        private void limpiar()
        {
            btSelectUser.Enabled = true;
            btPassword.Enabled = false;
            newPassword.Enabled = false;
            confirmPassword.Enabled = false;
            tbExpirationDate.Enabled = false;
            lbUserId.Text = string.Empty;
            lbusername.Text = string.Empty;
            lbExpirationDate.Text = string.Empty;
            //lbCurrentPassword.Text = string.Empty;
            tbExpirationDate.Text = string.Empty;
            checkPassExpiration.Checked = false;
        }
        protected void messages_ChangeLanguage()
        {
            msg_ChangePassword_UpdatePassword = (string)GetGlobalResourceObject(language, "msg_ChangePassword_UpdatePassword");
  
        }
        protected void btPassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (newPassword.Text.Trim() != string.Empty || confirmPassword.Text.Trim() != string.Empty)
                    {
                      
                        Users usuario = new Users();
                        Dictionary<string, dynamic> user = new Dictionary<string, dynamic>();
                        if (checkPassExpiration.Checked == true && tbExpirationDate.Text.Trim() != string.Empty)
                        {
                            DateTime dtExpiration = new DateTime();
                            string[] formats = { "MM/dd/yyyy", "MM/dd/yy" };
                            DateTime.TryParseExact(tbExpirationDate.Text, formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtExpiration);
                            usuario.Expiration_Date = dtExpiration;
                            user.Add("Expiration_Date", usuario.Expiration_Date);
                        }

                        usuario.User_ID = cbUsers.SelectedValue.ToString();
                        usuario.Password = functions.GetSHA1(newPassword.Text);
                        user.Add("Password", usuario.Password);
                        GenericClass.SQLUpdateObj(usuario, user, "Where User_ID='" + usuario.User_ID + "'");
                        functions.ShowMessage(this, this.GetType(), msg_ChangePassword_UpdatePassword + usuario.User_ID, MessageType.Success);
                        
                    }
                }
                limpiar();
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }


    }
}