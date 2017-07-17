using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using BusManagementSystem.Class;
using System.Collections;
using IBatisNet.DataMapper;
using System.Data;
using System.Text;
using BusManagementSystem._01Catalogos;
using System.Reflection;
using System.Web.Services;
using BusManagementSystem.Catalogos;



namespace BusManagementSystem
{
    public enum MessageType { Success, Error, Info, Warning };
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Users usr = (Users)Session["C_USER"];
                if (!string.IsNullOrWhiteSpace(Request.QueryString["email"]))
                {
                    Uri emailResult = new Uri(Request.Url.ToString());
                    functions.ShowMessage(this,this.GetType(),HttpUtility.ParseQueryString(emailResult.Query).Get("email"),HttpUtility.ParseQueryString(emailResult.Query).Get("Type")=="error"?MessageType.Error: MessageType.Success);
                }
                

                if (Request.QueryString["user"] == "True")
                {
                    try
                    {

                        functions.ShowMessage(this, this.GetType(), "Usuario : " + usr.User_Name.Replace("\r\n", "") + " - Cambio de contraseña correcto", MessageType.Success);
                    }
                    catch (Exception)
                    {
                        functions.ShowMessage(this, this.GetType(), "Llamada no válida de url", MessageType.Error);
                    }
                }
                
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            Users initialUser = new Users();
            DataTable dtUser = GenericClass.SQLSelectObj(initialUser, WhereClause: "Where User_ID='" + UserIdText.Text.ToString() + "'");
            if (dtUser.Rows.Count > 0)
            {

                initialUser = new Users(dtUser.Rows[0]);
                if (!initialUser.Password_Expired)
                {
                    string sPassw;
                    sPassw = functions.GetSHA1(PasswordText.Text.ToString());
                    initialUser.Password = initialUser.Password.Replace("\r\n", "");
                    if (initialUser.Is_Active & !initialUser.Is_Block)
                    {
                        if (initialUser.Reset_Password == false)
                        {

                            if (initialUser.User_ID != string.Empty & initialUser.Password != string.Empty & initialUser.Password == sPassw.ToString())                          
                            {
                                UsersAndRoles usrAndRol = new UsersAndRoles(dtUser, PasswordText.Text.ToString());
                                Session["USER"] = initialUser.User_ID;
                                Session["C_USER"] = initialUser;
                                Session["UsersAndRoles"] = usrAndRol;                                
                                Response.Redirect("./MenuPortal.aspx");
                                
                                if (initialUser.Expiration_Date <= DateTime.Now.AddMonths(1))
                                {
                                    functions.ShowMessage(this, this.GetType(), "Su contraseña es un punto de expirar, póngase en contacto con su supervisor", MessageType.Warning);
                                }
                            }
                            else
                            {
                                functions.ShowMessage(this, this.GetType(), "¡Su usuario o contraseña son incorrectos!", MessageType.Error);
                            }

                        }
                        else
                        {
                            Session["Recover_User"] = initialUser;
                            Response.Redirect("./ResetPass.aspx");
                        }
                    }
                    else
                    {
                        functions.ShowMessage(this, this.GetType(), "Su usuario no está activo, póngase en contacto con su supervisor", MessageType.Error);
                    }
                }
                else
                {

                    functions.ShowMessage(this, this.GetType(), "Su contraseña ha expirado, póngase en contacto con su supervisor", MessageType.Error);
                }
            } 
            else
            {
                functions.ShowMessage(this, this.GetType(), "El usuario no existe, Intente de nuevo", MessageType.Error);
            }

        }


    }
}