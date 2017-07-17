using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusManagementSystem
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["APPID"] = null;
            Session["USER"] = null;
            Session["DESC"] = null;
            Session["C_USER"] = null;
            if (Session["Language"] != null)
            {
                Session["Language"] = null;
            }
            if (Session["ColorTheme"] != null)
            {
                Session["ColorTheme"] = null;
            }
            if (Session["IVA"] != null)
            {
                Session["IVA"] = null;
            }
            Response.Redirect("./Login.aspx");
        }
    }
}