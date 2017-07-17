using BusManagementSystem._01Catalogos;
using BusManagementSystem.Class;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusManagementSystem._20Reportes
{
    public partial class R_ActivityLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                load_DailyOperationReport();
                ReportViewer1.AsyncRendering = true;
                ReportViewer1.SizeToReportContent = true;
                ReportViewer1.ZoomMode = ZoomMode.FullPage;
            }
            
        }
        private void load_DailyOperationReport()
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            Users usr = (Users)(Session["C_USER"]);

            try
            {
                string reportURL = ConfigurationManager.AppSettings["reportServerURL"];
                string reportUser = ConfigurationManager.AppSettings["reporUser"];
                string reportPassword = ConfigurationManager.AppSettings["reportUserPassword"];
                string reportDomain = ConfigurationManager.AppSettings["reportUserDomain"];
                string sReportPath_1 = ConfigurationManager.AppSettings["r_ActivityLog"];
                string reportEnviroment = ConfigurationManager.AppSettings["reportServerURL_Enviromnent"];

                string fullPath = reportEnviroment + sReportPath_1;

                /*Get all vendors */
                DataTable dtAllUsers = GenericClass.SQLSelectObj(new Users());
                /*turn datatable into string array*/
                string allUsers = String.Join(",", dtAllUsers.Rows.OfType<DataRow>().Select(R => R[dtAllUsers.Columns[0]]));
                string[] users = allUsers.Split(',');

                ReportParameter[] parameters = new ReportParameter[1];
                if (usr.Rol_ID.Contains("ADMIN"))
                    parameters[0] = new ReportParameter("User", users);
                else
                {
                    parameters[0] = new ReportParameter("User", usr.User_ID);
                    parameters[0].Visible = false;
                }



                ReportCredentials RC = new ReportCredentials(reportUser, reportPassword, reportDomain);
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                ReportViewer1.ServerReport.ReportServerCredentials = RC;
                ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportURL);
                ReportViewer1.ServerReport.ReportPath = fullPath;
                ReportViewer1.ServerReport.SetParameters(parameters);
                ReportViewer1.Visible = true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this,this.GetType(),msg, MessageType.Error);
            }
        }

        protected void btHelp_Click(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("./Login.aspx");
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Log de Actividades')", true);
        }
    }
}