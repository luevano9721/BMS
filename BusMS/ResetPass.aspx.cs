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
    public partial class ResetPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["Recover_User"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                Users usr = (Users)(Session["Recover_User"]);
                if (usr.Password.Equals(functions.GetSHA1(currentPassword.Text)))
                {
                    if (newPassword.Text.Equals(confirmPassword.Text))
                    {
                        if (!currentPassword.Text.Equals(newPassword.Text))
                        {

                            
                            Dictionary<string, dynamic> user = new Dictionary<string, dynamic>();
                            user.Add("Password", functions.GetSHA1(confirmPassword.Text));
                            user.Add("Reset_Password", false);
                            usr.Password = functions.GetSHA1(newPassword.Text);
                            Session["C_USER"] = usr;
                            GenericClass.SQLUpdateObj(usr, user, "Where User_ID='" + usr.User_ID + "'");
                                                     
                            Response.Redirect("~/Login.aspx?user=True",false);
                        }
                        else
                        {
                            functions.ShowMessage(this, this.GetType(), "La nueva contraseña debe ser diferente a su contraseña actual", MessageType.Error);
                        }
                    } 
                }
                else
                {
                    functions.ShowMessage(this,this.GetType(),"La contraseña actual debe ser la misma que la enviada a su correo electrónico", MessageType.Error);
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

    }
}