﻿using BusManagementSystem._01Catalogos;
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
    public partial class R_Trip_Change : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                load_DailyOperationReport();
            }
            ReportViewer1.AsyncRendering = true;
            ReportViewer1.SizeToReportContent = true;
            ReportViewer1.ZoomMode = ZoomMode.FullPage;
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
                string reportEnviroment = ConfigurationManager.AppSettings["reportServerURL_Enviromnent"];
                string sReportPath_1 = ConfigurationManager.AppSettings["r_Trip_Change"];

                string fullPath = reportEnviroment + sReportPath_1;

                /*Get all vendors */
                DataTable dtAllVendors = GenericClass.SQLSelectObj(new Vendor());
                /*turn datatable into string array*/
                string allVendors = String.Join(",", dtAllVendors.Rows.OfType<DataRow>().Select(R => R[dtAllVendors.Columns[0]]));
                string[] vendors = allVendors.Split(',');

                ReportParameter[] parameters = new ReportParameter[1];
                if (usr.Vendor_ID.Equals("ALL"))
                    parameters[0] = new ReportParameter("VendorID", vendors);
                else
                {
                    parameters[0] = new ReportParameter("VendorID", usr.Vendor_ID);
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
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Reporte Cambios en operación diaria')", true);
        }
    }
}