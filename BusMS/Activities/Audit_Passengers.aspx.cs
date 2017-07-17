using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusManagementSystem._01Catalogos;
using BusManagementSystem.Class;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using BusManagementSystem.Catalogos;
using System.Globalization;
namespace BusManagementSystem.Activities
{
    public partial class Audit_Passengers : System.Web.UI.Page
    {  
        string vendorFilter;
        static bool search = false;
        static string searchValue;
        static bool onEditing;
        static string sequence;
        static string trip_ID;
        static DataTable dtNew = new DataTable();
        string msg_Audit_Updated;
        string msg_NoChanges;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("~/Login.aspx");
               
            }
            Users usr = (Users)(Session["C_USER"]);


            if (usr.Profile != "INTERNAL")
            {
                vendorFilter = "Where Vendor_ID='" + usr.Vendor_ID + "'";
               
            }

            functions func = new functions();
            String language = null;
            if (Session["Language"] != null)
            {
                language = Session["Language"].ToString();
            }
            else
            {
                language = functions.GetLanguage(usr);
            }
            func.languageTranslate(this.Master, language);
            
            if (!this.IsPostBack)
            {
                search = false ;
                fill_ddl_Vendor();
            }

            translateControls(language);
        }

        private void translateControls(string language)
        {
            msg_Audit_Updated = (string)GetGlobalResourceObject(language, "msg_Audit_Updated");
            
            msg_NoChanges=(string)GetGlobalResourceObject(language, "msg_NoChanges");

        }

        protected void fill_ddl_Vendor()
        {
            try
            {
                //ListItem li = new ListItem("ALL", "ALL");
                Users usr = (Users)(Session["C_USER"]);
                ddl_Shift.Enabled = false;
                if (usr.Profile.Equals("INTERNAL"))
                {

                    Vendor initialVendor = new Vendor();
                    DataTable dt = GenericClass.SQLSelectObj(initialVendor, WhereClause: string.IsNullOrWhiteSpace(vendorFilter) ? null : vendorFilter);
                    DataView dvVendor = new DataView(dt);
                    DataTable distincVendor = dvVendor.ToTable(true, "VENDOR_ID", "NAME");
                    ddl_VendorAdm.Items.Clear();
                    ddl_VendorAdm.DataSource = distincVendor;
                    ddl_VendorAdm.DataValueField = "VENDOR_ID";
                    ddl_VendorAdm.DataTextField = "NAME";
                    ddl_VendorAdm.DataBind();
                }
                else
                {
                    List<string> Parameters = new List<string>();
                    Parameters.Add("Vendor_ID");
                    Parameters.Add("Name");
                    DataTable dt = GenericClass.SQLSelectObj(new Vendor(), WhereClause: string.IsNullOrWhiteSpace(vendorFilter) ? null : vendorFilter);
                    ddl_VendorAdm.Items.Clear();
                    ddl_VendorAdm.DataSource = dt;
                    ddl_VendorAdm.DataValueField = "Vendor_ID";
                    ddl_VendorAdm.DataTextField = "Name";
                    ddl_VendorAdm.DataBind();
                }

                bt_Switch.Enabled = false;

            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            
        }

        protected void ddl_ShiftType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddl_ShiftType.SelectedValue.ToString() == "NONE")
                {
                    ddl_Shift.Items.Clear();
                    bt_Load_Data.Enabled = false;
                    btExcel.Enabled = false;
                    ddl_Shift.Enabled = false;
                }
                else
                {

                    bt_Load_Data.Enabled = true;
                    btExcel.Enabled = true;
                    Shift initialShift = new Shift();
                    DataTable dtGetShifts = GenericClass.SQLSelectObj(initialShift, WhereClause: "WHERE EXIT_SHIFT=" + ddl_ShiftType.SelectedValue.ToString());
                    ddl_Shift.Items.Clear();
                    ddl_Shift.DataSource = dtGetShifts;
                    ddl_Shift.DataValueField = "Shift_ID";
                    ddl_Shift.DataTextField = "Name";
                    ddl_Shift.DataBind();
                    ddl_Shift.Enabled = true;

                    bt_Load_Data.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
           

        }

        protected void bt_Load_Data_Click(object sender, EventArgs e)
        {
            try
            {
                tbSearchCH1.Text = null;
                btSearchCH1.Enabled = true;
                btExcel.Enabled = true;
                ddl_Shift.Enabled = false;
                ddl_ShiftType.Enabled = false;
                ddl_VendorAdm.Enabled = false;
                dtg_Audit_Passengers.DataSource = fillGridCheckpoint1(ddl_Shift.SelectedValue, ddl_VendorAdm.SelectedValue);
                dtg_Audit_Passengers.PageSize = 15;
                dtg_Audit_Passengers.DataBind();
                lbShiftInfo.Text = ddl_Shift.SelectedItem.Text;
                lbVendorInfo.Text = ddl_VendorAdm.SelectedItem.Text;
                bt_Switch.Enabled = true;
                bt_Load_Data.Enabled = false;

            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); 
                functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            
        }
        private DataTable fillGridCheckpoint1(string shift, string vendor)
        {
            List<String> customSelect = new List<string>();
            customSelect.Add("Trip_Hrd_ID");
 
            DataTable dtg_Grid1 = GenericClass.SQLSelectObj(new Trip_Hrd(),customSelect, mappingQueryName: "Audit",
                WhereClause: "where Master_Trip.Shift_ID= '" + shift +
                "' and SUBSTRING(Master_Trip.Master_Trip_ID,0,4) in ('" + vendor + "') and Status not in ('CANCEL' ,'PENDINGTOCANCEL')"+
                "And dtl.Psg_Audit is null "+
                "And dtl.Psg_Init is not null "+
                "Order by Trip_Hrd.Trip_date desc");
            ViewState["myViewState"] = dtg_Grid1;
            return dtg_Grid1;
           
        }

        protected void dtg_Audit_Passengers_SelectedIndexChanged(object sender, EventArgs e)
        {

         

        }

        protected void dtg_Audit_Passengers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (onEditing == false)
                {
                    dtg_Audit_Passengers.EditIndex = -1;
                    onEditing = true;
                }
                dtg_Audit_Passengers.PageIndex = e.NewPageIndex;
                dtg_Audit_Passengers.DataSource = fillGridCheckpoint1(ddl_Shift.SelectedValue, ddl_VendorAdm.SelectedValue); 
                dtg_Audit_Passengers.PageSize = 15;
                dtg_Audit_Passengers.DataBind();

            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            

        }

        protected void dtg_Audit_Passengers_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                if (onEditing == false)
                {
                    dtg_Audit_Passengers.EditIndex = -1;
                    onEditing = true;
                }
                Users usr = (Users)(Session["C_USER"]);
                string sortingDirection = string.Empty;
                if (direction == SortDirection.Ascending)
                {
                    direction = SortDirection.Descending;
                    sortingDirection = "Desc";
                }
                else
                {
                    direction = SortDirection.Ascending;
                    sortingDirection = "Asc";
                }
                if (search == true)
                {

                    DataView sortedView = new DataView(dtNew);
                    sortedView.Sort = e.SortExpression + " " + sortingDirection;
                    Session["auditObjects"] = sortedView;
                    dtg_Audit_Passengers.DataSource = sortedView;
                    dtg_Audit_Passengers.DataBind();

                }
                else
                {
                    DataTable dt = fillGridCheckpoint1(ddl_Shift.SelectedValue, ddl_VendorAdm.SelectedValue);
                    DataView sortedView = new DataView(dt);
                    sortedView.Sort = e.SortExpression + " " + sortingDirection;
                    Session["auditObjects"] = sortedView;
                    dtg_Audit_Passengers.DataSource = sortedView;
                    dtg_Audit_Passengers.DataBind();
                
                
                }
                
               

            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }


        protected void bt_Add_Number_Click(object sender, EventArgs e)
        {

          
        }

        protected void dtg_Audit_Passengers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                dtg_Audit_Passengers.EditIndex = e.NewEditIndex;
                Label lbl_Trip_Hdr_ID = dtg_Audit_Passengers.Rows[dtg_Audit_Passengers.EditIndex].FindControl("lbl_Trip_Hrd_ID") as Label;
                Label lbl_Sequence = dtg_Audit_Passengers.Rows[dtg_Audit_Passengers.EditIndex].FindControl("lbl_Sequence") as Label;
                trip_ID = lbl_Trip_Hdr_ID.Text;
                sequence = lbl_Sequence.Text;
                if (Session["auditObjects"] != null && search != true)
                {
                    dtg_Audit_Passengers.DataSource = fillGridCheckpoint1(ddl_Shift.SelectedValue, ddl_VendorAdm.SelectedValue);
                    dtg_Audit_Passengers.DataSource = Session["auditObjects"];
                    dtg_Audit_Passengers.PageSize = 15;
                    dtg_Audit_Passengers.DataBind();
                    onEditing = false;
                
                }
                else if (Session["auditObjects"] != null && search == true)
                {

                    dtg_Audit_Passengers.DataSource = dtNew;
                    dtg_Audit_Passengers.DataSource = Session["auditObjects"];
                    dtg_Audit_Passengers.PageSize = 15;
                    dtg_Audit_Passengers.DataBind();
                    onEditing = false;
                
                }
                else if (search == true)
                {

                    dtg_Audit_Passengers.DataSource = dtNew;
                    dtg_Audit_Passengers.PageSize = 15;
                    dtg_Audit_Passengers.DataBind();
                    onEditing = false;


                }
                else
                {
                    dtg_Audit_Passengers.DataSource = fillGridCheckpoint1(ddl_Shift.SelectedValue, ddl_VendorAdm.SelectedValue);
                    dtg_Audit_Passengers.PageSize = 15;
                    dtg_Audit_Passengers.DataBind();
                    onEditing = false;
                }

                dtg_Audit_Passengers.BottomPagerRow.Visible = false;

            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        
               
           
        }

        protected void dtg_Audit_Passengers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                if (search == false)
                {
                    dtg_Audit_Passengers.EditIndex = -1;
                    trip_ID = null;
                    sequence = null;
                    dtg_Audit_Passengers.DataSource = fillGridCheckpoint1(ddl_Shift.SelectedValue, ddl_VendorAdm.SelectedValue);
                    dtg_Audit_Passengers.PageSize = 15;
                    dtg_Audit_Passengers.DataBind();

                }
                else
                {
                    dtg_Audit_Passengers.EditIndex = -1;
                    trip_ID = null;
                    sequence = null;
                    dtg_Audit_Passengers.DataSource = dtNew;
                    dtg_Audit_Passengers.PageSize = 15;
                    dtg_Audit_Passengers.DataBind();
                }
                dtg_Audit_Passengers.BottomPagerRow.Visible = true;
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            
           
         

        }
  
        protected void dtg_Audit_Passengers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                onEditing = false;

                TextBox lbl_Trip_Hdr_ID = dtg_Audit_Passengers.Rows[dtg_Audit_Passengers.EditIndex].FindControl("txt_Audit") as TextBox;

                Label lbl_BusDriver = dtg_Audit_Passengers.Rows[dtg_Audit_Passengers.EditIndex].FindControl("lbl_Bus_Driver_ID") as Label;
               
                Trip_Detail trip_Detail_New = new Trip_Detail();
                
                trip_Detail_New.Psg_Audit = Convert.ToInt32(lbl_Trip_Hdr_ID.Text.Trim());

                Dictionary<string,dynamic> dicAudit= new Dictionary<string,dynamic>();

                dicAudit.Add("Psg_Audit",trip_Detail_New.Psg_Audit);


                GenericClass.SQLUpdateObj(new Trip_Detail(), dicAudit,
                        WhereClause: "where Trip_Hrd_ID = '" + trip_ID + "' and Sequence = " + Convert.ToInt32(sequence));


                if (search == false)
                {
                    refresh();
                }
                else
                {
                    refreshWithSearch();
                    dtg_Audit_Passengers.EditIndex = -1;
                    dtg_Audit_Passengers.DataSource = dtNew;
                    dtg_Audit_Passengers.PageSize = 15;
                    dtg_Audit_Passengers.DataBind();

                }
                dtg_Audit_Passengers.BottomPagerRow.Visible = true;

                functions.ShowMessage(this, this.GetType(), msg_Audit_Updated + ": "+lbl_BusDriver.Text, MessageType.Success);

            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
           

        }
        protected void refresh()
        {
            try
            {
                dtg_Audit_Passengers.DataSource = null;
                dtg_Audit_Passengers.DataBind();
                dtg_Audit_Passengers.EditIndex = -1;
                dtg_Audit_Passengers.DataSource = fillGridCheckpoint1(ddl_Shift.SelectedValue, ddl_VendorAdm.SelectedValue);
                dtg_Audit_Passengers.PageSize = 15;
                dtg_Audit_Passengers.DataBind();

            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
           
        
            
        
        }
        

        protected void bt_Switch_Click(object sender, EventArgs e)
        {
            try
            {
                tbSearchCH1.Text = null;
                ddl_ShiftType.SelectedIndex = 0;
                ddl_Shift.Items.Clear();
                dtg_Audit_Passengers.DataSource = null;
                dtg_Audit_Passengers.DataBind();
                ddl_VendorAdm.Enabled = true;
                ddl_ShiftType.Enabled = true;
                btSearchCH1.Enabled = false;
                lbShiftInfo.Text = null;
                lbVendorInfo.Text = null;
                bt_Load_Data.Enabled = false;
                bt_Switch.Enabled = false;
                btExcel.Enabled = false;

            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
           
        }
        public SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionState"];
            }
            set
            { ViewState["directionState"] = value; }
        }

        protected void btSearchCH1_Click(object sender, EventArgs e)
        {
            try 
            {
                Search();
            
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            
        }
        protected void Search()
        {
            try
            {
                string searchTerm = tbSearchCH1.Text.ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    refresh();
                    dtNew.Clear();
                    searchValue = null;
                    search = false;
                }
                else
                {
                    searchValue = tbSearchCH1.Text.ToLower();
                    search = true;
                    //always check if the viewstate exists before using it
                    if (ViewState["myViewState"] == null)
                        return;

                    //cast the viewstate as a datatable
                    DataTable dt = ViewState["myViewState"] as DataTable;

                    //make a clone of the datatable
                    dtNew = dt.Clone();

                    //search the datatable for the correct fields
                    foreach (DataRow row in dt.Rows)
                    {
                        //add your own columns to be searched here
                        if (row["Bus_ID"].ToString().ToLower().Contains(searchTerm) || row["Driver"].ToString().ToLower().Contains(searchTerm) || row["Bus_Driver_ID"].ToString().ToLower().Contains(searchTerm) || row["Route"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtNew.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    if (dtNew.Rows.Count > 15)
                    {
                        dtg_Audit_Passengers.DataSource = dtNew;
                        dtg_Audit_Passengers.PageSize = 15;
                        dtg_Audit_Passengers.DataBind();
                    }
                    else
                    {
                        dtg_Audit_Passengers.DataSource = dtNew;
                        dtg_Audit_Passengers.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }
        protected void refreshWithSearch()
        {
            try
            {
              
                    fillGridCheckpoint1(ddl_Shift.SelectedValue, ddl_VendorAdm.SelectedValue);
                    search = true;
                    //always check if the viewstate exists before using it
                     
                    if (ViewState["myViewState"] == null)
                        return;

                    //cast the viewstate as a datatable
                    DataTable dt = ViewState["myViewState"] as DataTable;

                    //make a clone of the datatable
                    dtNew = dt.Clone();

                    //search the datatable for the correct fields
                    foreach (DataRow row in dt.Rows)
                    {
                        //add your own columns to be searched here
                        if (row["Bus_ID"].ToString().ToLower().Contains(searchValue) || row["Driver"].ToString().ToLower().Contains(searchValue) || row["Bus_Driver_ID"].ToString().ToLower().Contains(searchValue) || row["Route"].ToString().ToLower().Contains(searchValue))
                        {
                            //when found copy the row to the cloned table
                            dtNew.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    if (dtNew.Rows.Count > 15)
                    {
                        dtg_Audit_Passengers.DataSource = dtNew;
                        dtg_Audit_Passengers.PageSize = 15;
                        dtg_Audit_Passengers.DataBind();
                    }
                    else
                    {
                        dtg_Audit_Passengers.DataSource = dtNew;
                        dtg_Audit_Passengers.DataBind();
                    }
                //}
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        
        
        
        }

        private void execute_query()
        {
            try
            {
                Session["Excel"] = null;
                Session["name"] = string.Empty;

                if (Session["C_USER"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                Users usr = (Users)(Session["C_USER"]);
                DataTable dtLoad = fillGridCheckpoint1(ddl_Shift.SelectedValue, ddl_VendorAdm.SelectedValue); 
                Session["Excel"] = dtLoad;
                Session["name"] = "AuditPassenger";
            }
            catch (Exception ex)
            {
               
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btExcel_Click(object sender, EventArgs e)
        {
            execute_query();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.Redirect("~/Catalogos/ExportToExcel.aspx?btn=excel", false);
        }

        protected void btHelp_Click(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("./Login.aspx");
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Auditar Pasajeros')", true);
        }

    }
}