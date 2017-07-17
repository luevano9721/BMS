using BusManagementSystem._01Catalogos;
using BusManagementSystem.Class;
using System;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusManagementSystem
{
    public partial class RecoverPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                string usr = Request.QueryString["tbUser"];
                string email = Request.QueryString["tbEmail"];
                Users bmsUser = new Users();
                DataTable dt = GenericClass.SQLSelectObj(bmsUser, WhereClause: "Where User_ID='" + usr + "' and Email='" + email + "'");

                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        bmsUser.User_ID = dt.Rows[0]["User_ID"].ToString();
                        Session["C_USER"] = bmsUser;
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                        functions.ShowMessage(this,this.GetType(), msg, MessageType.Error);
                    }
                    if (!string.IsNullOrWhiteSpace(usr) & !string.IsNullOrWhiteSpace(email))
                    {
                        if (SendEmail.RequestNewPassword(usr, email))
                        {
                            Response.Clear();
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                            Response.Redirect("./Login.aspx?email=Se le ha enviado un correo con las instrucciones para cambiar su contraseña", false);
                            Response.End();
                            Response.Flush();

                        }
                    }
                    else
                    {

                        Response.Clear();
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                        Response.Redirect("./Login.aspx?email=El usuario o la contraseña son incorrectos&Type=error", false);
                        Response.End();
                        Response.Flush();
                    }
                }
                else
                {
                    Response.Clear();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect("./Login.aspx?email=El usuario o la contraseña son incorrectos&Type=error", false);
                    Response.End();
                    Response.Flush();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", "");
                functions.ShowMessage(this,this.GetType(), msg, MessageType.Error);
            }
        }


    }
}