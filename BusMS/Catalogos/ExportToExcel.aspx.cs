using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusManagementSystem._01Catalogos
{
    public partial class ExportToExcel : System.Web.UI.Page
    {

        #region Page
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string btn = Request.QueryString["btn"];
                if (!IsPostBack && Session["Excel"] != null)
                {

                    if (btn == "excel")
                    {

                        exportToExcel();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
                       

        #region Methods
        private void exportToExcel()
        {
            try
            {
                string today = DateTime.Now.ToString("MMddyyyyhhmmss");
                string strFileName = Session["name"].ToString() + today + ".csv";

                var dataTable = (DataTable)Session["Excel"];
                StringBuilder builder = new StringBuilder();
                List<string> columnNames = new List<string>();
                List<string> rows = new List<string>();

                foreach (DataColumn column in dataTable.Columns)
                {
                    columnNames.Add(column.ColumnName);
                }

                builder.Append(string.Join(",", columnNames.ToArray())).Append("\n");

                foreach (DataRow row in dataTable.Rows)
                {
                    List<string> currentRow = new List<string>();

                    foreach (DataColumn column in dataTable.Columns)
                    {
                        object item = row[column];

                        currentRow.Add(item.ToString());
                    }

                    rows.Add(string.Join(",", currentRow.ToArray()));
                }

                builder.Append(string.Join("\n", rows.ToArray()));

                Response.Clear();
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
                Response.Write(builder.ToString());
                Response.End();

                Response.Flush();
            }
            catch (Exception)
            {
                

            }
            finally
            {
                HttpContext.Current.ApplicationInstance.CompleteRequest();

            }
           



        }
        #endregion
      
      
        
       
    }
}