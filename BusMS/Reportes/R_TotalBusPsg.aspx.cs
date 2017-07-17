using BusManagementSystem._01Catalogos;
using BusManagementSystem.Class;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace BusManagementSystem.Reportes
{
    public partial class R_TotalBusPsg : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                loadReportData();
            }
            ReportViewer1.AsyncRendering = false;
            ReportViewer1.SizeToReportContent = true;
            ReportViewer1.ZoomMode = ZoomMode.FullPage;
        }

        protected void btHelp_Click(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("./Login.aspx");
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Camiones')", true);
        }

        private void loadReportData() {
           Users usr = (Users)(Session["C_USER"]);

            try
            {
                /*Get all vendors */
                DataTable dtAllVendors = GenericClass.SQLSelectObj(new Vendor());
                /*turn datatable into string array*/
                string allVendors = String.Join(",", dtAllVendors.Rows.OfType<DataRow>().Select(R => R[dtAllVendors.Columns[0]]));
                string[] vendors = allVendors.Split(',');

               
                ReportParameter[] parameters = new ReportParameter[2];
                if (Session["IVA"] != null)
                {
                    parameters[0] = new ReportParameter("IVA", Session["IVA"].ToString());
                    parameters[0].Visible = false;
                }
                else
                {
                    Global_Configuration globalConfigurationOld = new Global_Configuration();
                    DataTable dt_GetIVAValue = GenericClass.SQLSelectObj(new Global_Configuration(), WhereClause: "where Configuration_Name = 'IVA'");
                 
                    parameters[0] = new ReportParameter("IVA",  dt_GetIVAValue.Rows[0]["Configuration_Value"].ToString());
                    parameters[0].Visible = false;
                }
                if (usr.Vendor_ID.Equals("ALL"))
                    parameters[1] = new ReportParameter("paramVendor", vendors);
                else
                {
                    parameters[1] = new ReportParameter("paramVendor", usr.Vendor_ID);
                    parameters[1].Visible = false;
                }

                string reportURL = ConfigurationManager.AppSettings["reportServerURL"];
                string reportPath = ConfigurationManager.AppSettings["reportTotalsPath"];
                string reportUser = ConfigurationManager.AppSettings["reporUser"];
                string reportPassword = ConfigurationManager.AppSettings["reportUserPassword"];
                string reportEnviroment = ConfigurationManager.AppSettings["reportServerURL_Enviromnent"];
                string reportDomain = ConfigurationManager.AppSettings["reportUserDomain"];

                string fullPath = reportEnviroment + reportPath;

                ReportCredentials reportCredentialsObj = new ReportCredentials(reportUser, reportPassword, reportDomain);
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                ReportViewer1.ServerReport.ReportServerCredentials = reportCredentialsObj;
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
       
    }
}