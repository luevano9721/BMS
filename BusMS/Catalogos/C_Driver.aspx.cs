using BusManagementSystem.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusManagementSystem.Catalogos;
using BusManagementSystem._01Catalogos;
using System.IO;
using System.Configuration;


namespace BusManagementSystem._01Catalogos
{
    public partial class C_Driver : System.Web.UI.Page
    {
        #region Private Variables

        private Driver initialDriver = new Driver();

        private string vendorFilter;

        private Users usr;

        private DateTime dtBirtdate = new DateTime();

        private DateTime dtLicenseExpo = new DateTime();

        private DateTime dtHireDate = new DateTime();

        private string[] formats = { "MM/dd/yyyy", "MM/dd/yy" };

        private DataTable dtNew = new DataTable();

        private AddPrivilege privilege = new AddPrivilege();
        string language = null;
        string msg_Cdriver_NoSelected;
        string msg_Cdriver_ExtensionDoc;
        string msg_Cdriver_UploadOK;
        string msg_Cdriver_ExistsFile;
        string msg_Cdriver_FileSize;
        string msg_Cdriver_NoFile;
        string msg_Cdriver_FileDeleted;
        string msg_Cdriver_CertiExists;
        string msg_Cdriver_Deleted;
        string msg_Cdriver_Waitting;
        string msg_Cdriver_NoDrivers;
        string msg_Cdriver_NoPermission;
        string msg_Cdriver_Added;
        string msg_Cdriver_ErrorBL;
        string msg_Cdriver_Update;
        string msg_Cdriver_BLInsert;
        string msg_CdriverBLDeleted;
        string msg_Cdriver_NoChanges;
        string msg_Cdriver_ExtensionPicture;
        #endregion

        #region Page

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                if (Session["C_USER"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }

                //Load buttons to hide or unhide for privileges
                privilege = new AddPrivilege("10001", btAdd, btReset, btSave, btDelete, btExcel);

                Users usr = (Users)(Session["C_USER"]);

                vendorFilter = usr.Vendor_ID.Equals("ALL") ? "" : "Where [Vendor_ID]= '" + usr.Vendor_ID + "'";

                Vendor initialVendor = new Vendor();
                DataTable dt = GenericClass.SQLSelectObj(initialVendor, WhereClause: string.IsNullOrWhiteSpace(vendorFilter) ? null : vendorFilter);
                DataView dvVendor = new DataView(dt);
                DataTable distincVendor = dvVendor.ToTable(true, "VENDOR_ID", "NAME");


                if (usr.Vendor_ID.Equals("ALL"))
                {
                    cbVendorAdm.Enabled = true;
                }
                else
                {
                    cbVendorAdm.Enabled = false;

                }
                cbVendorAdm.Items.Clear();
                cbVendor2.Items.Clear();


                cbVendorAdm.DataSource = distincVendor;
                cbVendorAdm.DataValueField = "VENDOR_ID";
                cbVendorAdm.DataTextField = "NAME";
                cbVendorAdm.DataBind();

                cbVendor2.DataSource = distincVendor;
                cbVendor2.DataValueField = "VENDOR_ID";
                cbVendor2.DataTextField = "NAME";
                cbVendor2.DataBind();



                if (usr.Vendor_ID.Equals("ALL"))
                {
                    ListItem li = new ListItem("ALL", "ALL");
                    cbVendorAdm.Items.Insert(0, li);
                    cbVendor2.Items.Insert(0, li);
                }

                cbVendorAdm.SelectedIndex = 0;

                cbVendor2.SelectedValue = cbVendorAdm.SelectedValue;

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            usr = (Users)(Session["C_USER"]);

            if (usr == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            privilege.DtPrivilege = privilege.GetPrivilege(this.Page.Title, usr);

            if (!IsPostBack)
            {
                //Set buttons to initial mode
                PrivilegeAndProcessFlow("10001");

                ViewState["searchStatus"] = false;

                ViewState["isSorting"] = false;

                ViewState["searchStatusRoutes"] = false;

                ViewState["isSorting_Routes"] = false;

                ViewState["searchStatusCertificates"] = false;

                ViewState["isSorting_Add_Certificates"] = false;
            }

            if ((bool)ViewState["searchStatus"] == false && (bool)ViewState["isSorting"] == false)
                fillDriverGrid();

            if ((bool)ViewState["searchStatusRoutes"] == false && (bool)ViewState["isSorting_Routes"] == false)
               refresh_Grid_Routes();

            if ((bool)ViewState["searchStatusCertificates"] == false && (bool)ViewState["isSorting_Add_Certificates"] == false)
                refresh_Grid_Add_Certificates();

            applyLanguage();

            if (!string.IsNullOrEmpty(language))
                translateMessages(language);
        }
        
        #endregion


        #region button

        protected void btShowUploads_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbDriverID.Text))
            {
                fillGridDocuments();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showUploadDocDialog();", true);
            }
            else
                functions.ShowMessage(this, this.GetType(), msg_Cdriver_NoSelected, MessageType.Error);
        }

        protected void btUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    if (FileUpload1.PostedFile.ContentLength < 5000000)
                    {
                        if (!validateFile())
                        {
                            functions.ShowMessage(this, this.GetType(), msg_Cdriver_ExtensionDoc, MessageType.Error);
                            fillGridDocuments();
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showUploadDocDialog();", true);
                        }

                        else
                        {
                            ViewState["Path"] = "Documents";
                            string[] path = createServerPathsStrings();


                            string fileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                            Driver_Documents docObj = new Driver_Documents();
                            docObj.File_Path = path[1];
                            docObj.Short_Path = path[0];
                            docObj.Driver_ID = Convert.ToInt32(tbDriverID.Text);
                            docObj.Upload_by = usr.User_ID;
                            docObj.Upload_date = System.DateTime.Now;
                            docObj.Category_file = cbDocType.SelectedValue.ToString();
                            docObj.File_Name = fileName;
                            createDirectorysOnServer(path[1]);
                            FileUpload1.PostedFile.SaveAs(path[1] + fileName);
                            GenericClass.SQLInsertObj(docObj);
                            functions.ShowMessage(this, this.GetType(), msg_Cdriver_UploadOK, MessageType.Success);
                            fillGridDocuments();
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showUploadDocDialog();", true);

                        }
                    }
                    else
                    {
                        functions.ShowMessage(this, this.GetType(), msg_Cdriver_FileSize, MessageType.Error);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showUploadDocDialog();", true);
                    }

                }
                else
                {
                    functions.ShowMessage(this, this.GetType(), msg_Cdriver_NoFile, MessageType.Error);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showUploadDocDialog();", true);
                }

            }

            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            //Server.MapPath("/")
            try
            {
                
                string driverDocuments_ID = (sender as Button).CommandArgument;
                DataTable dt_DriverDocuments = GenericClass.SQLSelectObj(new Driver_Documents(), WhereClause:"where Driver_Documents_ID =" + driverDocuments_ID);
                Response.ContentType = ContentType;
                string serverPath = dt_DriverDocuments.Rows[0]["File_Path"].ToString() + dt_DriverDocuments.Rows[0]["File_Name"].ToString();
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + dt_DriverDocuments.Rows[0]["File_Name"].ToString());
                Response.WriteFile(serverPath);
                Response.Flush();
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string docID = (sender as Button).CommandArgument;
                Driver_Documents docObj = new Driver_Documents();
                DataTable dtGetFilePath = GenericClass.SQLSelectObj(docObj, WhereClause: "WHERE DRIVER_DOCUMENTS_ID=" + docID);
                if (dtGetFilePath.Rows.Count > 0)
                {
                    string filePath = dtGetFilePath.Rows[0]["FILE_PATH"].ToString();
                    string serverPath = dtGetFilePath.Rows[0]["FILE_PATH"].ToString() + dtGetFilePath.Rows[0]["FILE_NAME"].ToString();
                    File.Delete(serverPath);
                    GenericClass.SQLDeleteObj(docObj, WhereClause: "WHERE DRIVER_DOCUMENTS_ID=" + docID);
                    functions.ShowMessage(this, this.GetType(), msg_Cdriver_FileDeleted, MessageType.Success);
                    fillGridDocuments();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showUploadDocDialog();", true);
                }
            }

            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }


        }

        protected void img_Add_certificates_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton imb_Add_Certificate = (sender) as ImageButton;
                string Certify_Route_ID = imb_Add_Certificate.CommandArgument;
                GenericClass.SQLDeleteObj(new CERTIFICATE_ROUTE(), WhereClause: "where Certify_Route_ID = " + Certify_Route_ID);
                refresh_Grid_Add_Certificates();
                refresh_Grid_Routes();
            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }


        }

        protected void img_Routes_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton imb_Add_Certificate = (sender) as ImageButton;
                string Route_ID = imb_Add_Certificate.CommandArgument;
                DataTable dt_Route_Validation = GenericClass.SQLSelectObj(new CERTIFICATE_ROUTE(), WhereClause: "where CERTIFICATE_ROUTE.Route_ID  =" + Route_ID + " and  CERTIFICATE_ROUTE.Driver_ID = " + Convert.ToInt32(tbDriverID.Text.Trim()));
                if (dt_Route_Validation.Rows.Count == 0)
                {

                    CERTIFICATE_ROUTE Certificate_Route = new CERTIFICATE_ROUTE();
                    Certificate_Route.Date = DateTime.Now;
                    Certificate_Route.Driver_ID = Convert.ToInt32(tbDriverID.Text.Trim());
                    Certificate_Route.Route_ID = Convert.ToInt32(Route_ID);
                    GenericClass.SQLInsertObj(Certificate_Route);

                }
                else
                {
                    functions.ShowMessage(this, this.GetType(), msg_Cdriver_CertiExists, MessageType.Warning);

                }

                refresh_Grid_Add_Certificates();
                refresh_Grid_Routes();
            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btn_Search_certificates_Click(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = txt_Search_certificates.Text.ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    refresh_Grid_Add_Certificates();
                    dtNew.Clear();
                    ViewState["searchStatusCertificates"] = false;

                    ViewState["isSorting_Add_Certificates"] = false;

                }
                else
                {
                    ViewState["searchStatusCertificates"] = true;

                    ViewState["isSorting_Add_Certificates"] = false;

                    //always check if the viewstate exists before using it
                    if (ViewState["dtg_Add_Certificates"] == null)
                        return;

                    //cast the viewstate as a datatable
                    DataTable dt = ViewState["dtg_Add_Certificates"] as DataTable;

                    //make a clone of the datatable
                    dtNew = dt.Clone();

                    //search the datatable for the correct fields
                    foreach (DataRow row in dt.Rows)
                    {
                        //add your own columns to be searched here
                        if (row["Route"].ToString().ToLower().Contains(searchTerm) || row["Date"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtNew.Rows.Add(row.ItemArray);
                        }
                    }
                    ViewState["searchResultCertificates"] = dtNew;
                    dtg_Add_Certificates.DataSource = dtNew;
                    dtg_Add_Certificates.DataBind();
                    //rebind the grid
                    //.DataSource = dtNew;
                    //dtg_Audit_Passengers.DataBind();


                }

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void bt_Search_Click(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = txt_Search.Text.ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    refresh_Grid_Routes();
                    dtNew.Clear();
                    ViewState["searchStatusRoutes"] = false;

                    ViewState["isSorting_Routes"] = false;

                }
                else
                {
                    ViewState["searchStatusRoutes"] = true;

                    ViewState["isSorting_Routes"] = false;

                    if (ViewState["dtg_Routes"] == null)
                        return;

                    //cast the viewstate as a datatable

                    DataTable dt = ViewState["dtg_Routes"] as DataTable;

                    //make a clone of the datatable
                    dtNew = dt.Clone();

                    //search the datatable for the correct fields
                    foreach (DataRow row in dt.Rows)
                    {
                        //add your own columns to be searched here
                        if (row["Route"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtNew.Rows.Add(row.ItemArray);
                        }
                    }
                    ViewState["searchResultsRoutes"] = dtNew;
                    dtg_Routes.DataSource = dtNew;
                    dtg_Routes.DataBind();
                    //rebind the grid



                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btCertificate_Click(object sender, EventArgs e)
        {
            try
            {
                

                List<string> Columns = new List<string>();
                Columns.Add("Route_ID");
                Columns.Add("Route");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "openWindow('certificateWindow', 'Certificates' , 700 , 700)", true);
                DataTable dt_Fill_Grid1 = GenericClass.SQLSelectObj(new CERTIFICATE_ROUTE(), mappingQueryName: "CERTIFICATE", WhereClause: "where CERTIFICATE_ROUTE.Driver_ID = " + Convert.ToInt32(tbDriverID.Text.Trim()));

                lbl_driver_Name_Text.Text = tbName.Text;
                lbl_Driver_ID_Window.Text = tbDriverID.Text;
                refresh_Grid_Routes();

                dtg_Routes.PageIndex = 0;
                tbSearchCH1.Text = string.Empty;

                bt_Search.Enabled = true;
                btn_Search_certificates.Enabled = true;

                if (dt_Fill_Grid1.Rows.Count == 0)
                {
                    //lbl_WithEmpty.Visible = true;
                    dtg_Add_Certificates.DataSource = null;
                    dtg_Add_Certificates.DataBind();
                    ViewState["dtg_Add_Certificates"] = null;

                }
                else
                {

                    ViewState["dtg_Add_Certificates"] = dt_Fill_Grid1;
                    dtg_Add_Certificates.DataSource = dt_Fill_Grid1;
                    dtg_Add_Certificates.DataBind();

                }
            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }


        }        

        protected void btSearchCH1_Click(object sender, EventArgs e)
        {
            search();
        }

        protected void btClose_Click(object sender, EventArgs e)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "closeDialog();", true);
        }

        protected void btAdd_Click(object sender, EventArgs e)
        {

            PrivilegeAndProcessFlow("01101");
            tbName.Focus();
            cleanSearchAndSorting();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
        }

        protected void btReset_Click(object sender, EventArgs e)
        {

            PrivilegeAndProcessFlow("10001");
            cbVendorAdm.Enabled = true;
            cleanSearchAndSorting();
            ViewState["isPictureChange"] = false;
            fillDriverGrid();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "HideInfoEdit();", true);
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            usr = (Users)(Session["C_USER"]);
            if (usr == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                if (tbDriverID.Text.ToString() == string.Empty)
                    saveDriver();
                else
                    updateDriver();

                PrivilegeAndProcessFlow("10001");
                fillDriverGrid();
            }
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
           // ClientScript.RegisterStartupScript(this.GetType(), "alert", "", true);
            try
            {
                if (Session["C_USER"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                Users usr = (Users)(Session["C_USER"]);

                int driverID = 0;
                int.TryParse(tbDriverID.Text, out driverID);

                Driver deleteDriver = new Driver();
                deleteDriver.Driver_ID = driverID;
                deleteDriver.Name = tbName.Text;
                if (usr.Rol_ID.Contains("ADMIN"))
                {
                    GenericClass.SQLDeleteObj(initialDriver, "Where Driver_ID=" + deleteDriver.Driver_ID);
                    functions.ShowMessage(this, this.GetType(), msg_Cdriver_Deleted + " " + tbDriverID.Text.Trim(), MessageType.Success);

                }
                else
                {
                    Admin_Approve confirmInsert = new Admin_Approve(0, DateTime.Now, usr.User_ID.ToString(), "DELETE", deleteDriver.GetType().Name, "", "", "Where Driver_ID=" + deleteDriver.Driver_ID, "El usuario ha solicitado eliminar el siguiente conductor : " + deleteDriver.Name);
                    GenericClass.SQLInsertObj(confirmInsert);
                    SendEmail.sendEmailToAdmin(usr.User_ID.ToString(), confirmInsert);
                    DataTable getVendor = GenericClass.GetCustomData("SELECT VENDOR_ID FROM DRIVER WHERE DRIVER_ID=" + deleteDriver.Driver_ID);
                    string Vendor_ID = getVendor.Rows[0]["VENDOR_ID"].ToString();
                    SendEmail.sendEmailToDistributionList(Vendor_ID, "Catalogs_Email", confirmInsert, usr.User_ID.ToString());
                    functions.ShowMessage(this, this.GetType(), msg_Cdriver_Waitting + " " + deleteDriver.Name, MessageType.Warning);
                }

                PrivilegeAndProcessFlow("10001");
                GridView_Drivers.DataSource = generalDataTable(cbVendorAdm.SelectedValue);
                GridView_Drivers.DataBind();
                cleanSearchAndSorting();

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btDelete_Click1(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "", true);
        }

        private void cleanSearchAndSorting()
        {
            ViewState["searchStatus"] = false;
            ViewState["isSorting"] = false;
            tbSearchCH1.Text = string.Empty;

        }

        protected void GridView_Drivers_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = GridView_Drivers.SelectedRow;
            try
            {
                btProfilePicture.Enabled = true;
                btProfilePicture.Visible = true;
                tbDriverID.Text = HttpUtility.HtmlDecode(GridView_Drivers.Rows[gvRow.RowIndex].Cells[1].Text);
                
                tbName.Text = HttpUtility.HtmlDecode(GridView_Drivers.Rows[gvRow.RowIndex].Cells[2].Text);
                
                tbBirthdate.Text = HttpUtility.HtmlDecode(GridView_Drivers.Rows[gvRow.RowIndex].Cells[3].Text);
                
                tbLicenseNo.Text = HttpUtility.HtmlDecode(GridView_Drivers.Rows[gvRow.RowIndex].Cells[4].Text);
                
                tbHireDate.Text = HttpUtility.HtmlDecode(GridView_Drivers.Rows[gvRow.RowIndex].Cells[11].Text);
                
                tbLicenseExp.Text = HttpUtility.HtmlDecode(GridView_Drivers.Rows[gvRow.RowIndex].Cells[5].Text);
                
                tbAddress.Text = HttpUtility.HtmlDecode(GridView_Drivers.Rows[gvRow.RowIndex].Cells[6].Text);
                
                tbTelephone.Text = HttpUtility.HtmlDecode(GridView_Drivers.Rows[gvRow.RowIndex].Cells[7].Text);

                functions selectFunction = new functions();

                selectFunction.SelectDropdownList(cbVendor2, GridView_Drivers.Rows[gvRow.RowIndex].Cells[8].Text.ToString().Trim());

                CheckBox cbEnable = (CheckBox)GridView_Drivers.Rows[gvRow.RowIndex].Cells[9].Controls[0];

                CheckBox cbBlacklist = (CheckBox)GridView_Drivers.Rows[gvRow.RowIndex].Cells[10].Controls[0];

                cbActiveItem.Checked = cbEnable.Checked;
                cbBlackListItem.Checked = cbBlacklist.Checked;
                PrivilegeAndProcessFlow("01111");
                tbName.Focus();

                cbVendorAdm.Enabled = false;
                profilePicture();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
            }

            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void btExcel_Click(object sender, EventArgs e)
        {
            execute_query();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Download", "GotoDownloadPage();", true);
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
            //Response.Redirect("./ExportToExcel.aspx?btn=excel", false);

        }

        protected void btHelp_Click(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("./Login.aspx");
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Conductor')", true);
        }


        #endregion


        #region GridView

        protected void dtg_Routes_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {

                ViewState["isSorting_Routes"] = true;

                ViewState["searchStatusRoutes"] = false;

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

                DataTable dt = refresh_Grid_Routes();
                DataView sortedView = new DataView(dt);
                sortedView.Sort = e.SortExpression + " " + sortingDirection;
                Session["dtg_Routes"] = sortedView;
                dtg_Routes.DataSource = sortedView;
                dtg_Routes.DataBind();
            }
            catch (Exception ex)
            {
                
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void dtg_Routes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            dtg_Routes.PageIndex = e.NewPageIndex;
            if (Convert.ToBoolean(ViewState["isSorting_Routes"]))
            {
                dtg_Routes.DataSource = Session["dtg_Routes"];
            }
            if ((bool)ViewState["searchStatusRoutes"]==true)
            {
                dtg_Routes.DataSource = ViewState["searchResultsRoutes"] as DataTable;
            }

            dtg_Routes.DataBind();


        }

        protected void dtg_Add_Certificates_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            try
            {
                dtg_Add_Certificates.PageIndex = e.NewPageIndex;
                if (Convert.ToBoolean(ViewState["isSorting_Add_Certificates"]))
                {
                    dtg_Add_Certificates.DataSource = Session["dtg_Add_Certificates"];
                }
                if ((bool)ViewState["searchStatusCertificates"] == true)
                {
                    dtg_Add_Certificates.DataSource = ViewState["searchResultCertificates"] as DataTable;
                }

                dtg_Add_Certificates.DataBind();

            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void dtg_Add_Certificates_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {

                ViewState["isSorting_Add_Certificates"] = true;

                ViewState["searchResultCertificates"] = false;

                
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

                DataTable dt= refresh_Grid_Add_Certificates();
                DataView sortedView = new DataView(dt);
                sortedView.Sort = e.SortExpression + " " + sortingDirection;
                Session["dtg_Add_Certificates"] = sortedView;
                dtg_Add_Certificates.DataSource = sortedView;
                dtg_Add_Certificates.DataBind();
            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }        

        private void fillDriverGrid()
        {
            try
            {
                DataTable getData = generalDataTable(cbVendorAdm.SelectedValue);
                if (getData.Rows.Count > 0)
                {
                    GridView_Drivers.DataSource = getData;
                    ViewState["backupData"] = getData;

                    GridView_Drivers.PageSize = 15;
                    GridView_Drivers.DataBind();


                }
                else
                {
                    functions.ShowMessage(this, this.GetType(), msg_Cdriver_NoDrivers + " ", MessageType.Warning);
                }
            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void GridView_Drivers_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox c = (CheckBox)e.Row.Cells[10].Controls[0];
                CheckBox inactive = (CheckBox)e.Row.Cells[9].Controls[0];
                // Check the XXXX column - if empty, the YYYY needs highlighting!
                if (c.Checked == true)
                {
                    e.Row.CssClass = "highlightRow"; // ...so highlight it
                }
                if (inactive.Checked == false & c.Checked == false)
                {
                    e.Row.CssClass = "highlightInactiveRow"; // ...so highlight it
                }
            }
        }

        protected void GridView_Drivers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_Drivers.PageIndex = e.NewPageIndex;
            if ((bool)ViewState["searchStatus"] == true)
            { GridView_Drivers.DataSource = ViewState["searchResults"]; }
            if ((bool)ViewState["isSorting"] == true)
            { GridView_Drivers.DataSource = Session["Objects"]; }
            GridView_Drivers.DataBind();
        }

        protected void GridView_Drivers_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                ViewState["isSorting"] = true;
                ViewState["searchStatus"] = false;
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

                DataTable dt = generalDataTable(cbVendorAdm.SelectedValue);
                DataView sortedView = new DataView(dt);
                sortedView.Sort = e.SortExpression + " " + sortingDirection;
                Session["objects"] = sortedView;
                GridView_Drivers.DataSource = sortedView;
                GridView_Drivers.DataBind();

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void GridView_Drivers_DataBound(object sender, EventArgs e)
        {
            functions.verifyIfBlock(GridView_Drivers, this, 10);
            functions.verifyIfInactive(GridView_Drivers, this, 9);
        }

        protected void btProfilePicture_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable dt_ProfilePicture = GenericClass.SQLSelectObj(new Driver_Documents(), WhereClause: "where Driver_ID = " + Convert.ToInt32(tbDriverID.Text.Trim()) + "  and Category_file = 'ProfilePicture'");
                if (dt_ProfilePicture.Rows.Count == 0)
                {

                    profilePictureDialog.ImageUrl = "~/images/image-not-found.jpg";
                    FileUpload2.Enabled = true;
                }
                else
                {
                    btUploadProfilePicture.Visible = false;
                    FileUpload2.Enabled = false;
                    string serverPath = ConfigurationManager.AppSettings["profilePictureImagesPathToDisplay"].ToString();
                    string shortPath = dt_ProfilePicture.Rows[0]["Short_Path"].ToString().Replace("\\", "/");
                    string fileName = dt_ProfilePicture.Rows[0]["File_Name"].ToString();
                    profilePictureDialog.ImageUrl = serverPath + shortPath + fileName;

                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showUpdateProfilePicture();", true);
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btUploadProfilePicture_Click(object sender, EventArgs e)
        {
            try
            {
                Users usr = (Users)(Session["C_USER"]);
                bool pictureChange = false;
                if (!validatePicture())
                {
                    functions.ShowMessage(this, this.GetType(), msg_Cdriver_ExtensionPicture, MessageType.Error);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showUpdateProfilePicture();", true);

                }
                else
                {
                    if (ViewState["isPictureChange"] != null)
                        pictureChange = Convert.ToBoolean(ViewState["isPictureChange"]);
                    if (this.FileUpload2.HasFile)
                    {
                        if (pictureChange)
                        {
                            Driver_Documents objetDriverDocuments_Current = new Driver_Documents();
                            Driver_Documents objetDriverDocuments_New = new Driver_Documents();
                            DataTable dt_ProfilePicture = GenericClass.SQLSelectObj(new Driver_Documents(), WhereClause: "where Driver_ID = " + Convert.ToInt32(tbDriverID.Text.Trim()) + "  and Category_file = 'ProfilePicture'");
                            objetDriverDocuments_Current.Driver_Documents_ID = Convert.ToInt32(dt_ProfilePicture.Rows[0]["Driver_Documents_ID"]);
                            objetDriverDocuments_Current.Category_file = dt_ProfilePicture.Rows[0]["Category_file"].ToString();
                            objetDriverDocuments_Current.Driver_ID = Convert.ToInt32(dt_ProfilePicture.Rows[0]["Driver_ID"]);
                            objetDriverDocuments_Current.File_Name = dt_ProfilePicture.Rows[0]["File_Name"].ToString();
                            objetDriverDocuments_Current.File_Path = dt_ProfilePicture.Rows[0]["File_Path"].ToString();
                            objetDriverDocuments_Current.Short_Path = dt_ProfilePicture.Rows[0]["Short_Path"].ToString();
                            objetDriverDocuments_Current.Upload_by = dt_ProfilePicture.Rows[0]["Upload_by"].ToString();
                            objetDriverDocuments_Current.Upload_date = Convert.ToDateTime(dt_ProfilePicture.Rows[0]["Upload_date(M/D/Y)"].ToString());

                            string Currentpath = objetDriverDocuments_Current.File_Path + objetDriverDocuments_Current.File_Name;
                            string fileName = System.IO.Path.GetFileName(FileUpload2.PostedFile.FileName);
                            ViewState["Path"] = "ProfilePicture";
                            string[] Paths = createServerPathsStrings();
                            string newPath = Paths[1];
                            if (System.IO.File.Exists(Currentpath))
                            {
                                System.IO.File.Delete(Currentpath);
                                createDirectorysOnServer(newPath);
                                FileUpload2.PostedFile.SaveAs(newPath + fileName);
                            }
                            objetDriverDocuments_New.Driver_Documents_ID = Convert.ToInt32(dt_ProfilePicture.Rows[0]["Driver_Documents_ID"]);
                            objetDriverDocuments_New.Category_file = dt_ProfilePicture.Rows[0]["Category_file"].ToString();
                            objetDriverDocuments_New.Driver_ID = Convert.ToInt32(dt_ProfilePicture.Rows[0]["Driver_ID"]);
                            objetDriverDocuments_New.File_Name = fileName;
                            objetDriverDocuments_New.File_Path = newPath;
                            objetDriverDocuments_New.Upload_by = usr.User_ID;
                            objetDriverDocuments_New.Upload_date = DateTime.Now;
                            objetDriverDocuments_New.Short_Path = Paths[0];
                            GenericClass.SQLUpdateObj(objetDriverDocuments_Current, adminApprove.compareObjects(objetDriverDocuments_Current, objetDriverDocuments_New, ""), "Where Driver_Documents_ID=" + Convert.ToInt32(dt_ProfilePicture.Rows[0]["Driver_Documents_ID"]));
                        }
                        else
                        {
                            Driver_Documents Documents = new Driver_Documents();
                            string fileName = System.IO.Path.GetFileName(FileUpload2.PostedFile.FileName);
                            ViewState["Path"] = "ProfilePicture";
                            string[] paths = createServerPathsStrings();


                            Documents.Category_file = "ProfilePicture";
                            Documents.Driver_ID = Convert.ToInt32(tbDriverID.Text);
                            Documents.File_Name = fileName;
                            Documents.Upload_by = usr.User_ID;
                            Documents.Upload_date = DateTime.Now;
                            Documents.File_Path = paths[1];
                            Documents.Short_Path = paths[0];
                            GenericClass.SQLInsertObj(Documents);
                            createDirectorysOnServer(paths[1]);
                            FileUpload2.PostedFile.SaveAs(paths[1] + fileName);

                        }
                        ViewState["Path"] = null;

                    }

                    profilePicture();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showUpdateProfilePicture();", true);
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        protected void btChangePicture_Click(object sender, EventArgs e)
        {
            FileUpload2.Enabled = true;
            btDeleteProfilePicture.Visible = false;
            btUploadProfilePicture.Visible = true;
            btChangePicture.Visible = false;
            ViewState["isPictureChange"] = true;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showUpdateProfilePicture();", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);

        }

        protected void btDeleteProfilePicture_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dt_ProfilePicture = GenericClass.SQLSelectObj(new Driver_Documents(), WhereClause: "where Driver_ID = " + Convert.ToInt32(tbDriverID.Text.Trim()) + "  and Category_file = 'ProfilePicture'");
                Driver_Documents Driver_Documents_Objets = new Driver_Documents();

                string Currentpath = dt_ProfilePicture.Rows[0]["File_Path"].ToString() + dt_ProfilePicture.Rows[0]["File_Name"].ToString();
                if (System.IO.File.Exists(Currentpath))
                {
                    System.IO.File.Delete(Currentpath);
                }
                GenericClass.SQLDeleteObj(Driver_Documents_Objets, WhereClause: "where Driver_ID = " + Convert.ToInt32(tbDriverID.Text.Trim()) + "  and Category_file = 'ProfilePicture'");
                FileUpload2.Enabled = true;
                profilePicture();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showUpdateProfilePicture();", true);
            }
            catch (Exception ex)
            {

                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }


        #endregion

        #region ComboBox
        protected void cbActiveItem_CheckedChanged(object sender, EventArgs e)
        {
            if (cbActiveItem.Checked)
            {
                cbBlackListItem.Checked = false;
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
        }

        protected void cbVendorAdm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridView_Documents.PageIndex = 0;

                cleanSearchAndSorting();
                GridView_Drivers.DataSource = generalDataTable(cbVendorAdm.SelectedValue);
                GridView_Drivers.PageSize = 15;
                GridView_Drivers.DataBind();
                
                PrivilegeAndProcessFlow("10001");
                cbVendor2.SelectedValue = cbVendorAdm.SelectedValue == "ALL" ? cbVendorAdm.Items[1].Value : cbVendorAdm.SelectedValue;
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void cbBlackListItem_CheckedChanged(object sender, EventArgs e)
        {
            Users usr = (Users)(Session["C_USER"]);
            if (cbBlackListItem.Checked)
            {
                cbActiveItem.Checked = false;
            }

            if (usr.Rol_ID.Contains("ADMIN"))
            {
                CheckBox cbBlackList = (CheckBox)sender;
                if (cbBlackList.Checked == true)
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showBlacklistDialog();", true);
                }
                else
                {
                    tbReasons.Text = string.Empty;
                    tbComments.Text = string.Empty;
                }
            }
            else
            {
                functions.ShowMessage(this, this.GetType(), msg_Cdriver_NoPermission + " ", MessageType.Warning);
                CheckBox cb = (CheckBox)sender;
                if (cb.Checked == true)
                    cb.Checked = false;
                else
                    cb.Checked = true;
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ShowInfoToEdit();", true);
        }
        #endregion

        #region Methods

        private void applyLanguage()
        {
            try
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
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        private void translateMessages(string language)
        {

            msg_Cdriver_NoSelected = (string)GetGlobalResourceObject(language, "msg_Cdriver_NoSelected");
            msg_Cdriver_ExtensionDoc = (string)GetGlobalResourceObject(language, "msg_Cdriver_ExtensionDoc");
            msg_Cdriver_UploadOK = (string)GetGlobalResourceObject(language, "msg_Cdriver_UploadOK");
            msg_Cdriver_ExistsFile = (string)GetGlobalResourceObject(language, "msg_Cdriver_ExistsFile");
            msg_Cdriver_FileSize = (string)GetGlobalResourceObject(language, "msg_Cdriver_FileSize");
            msg_Cdriver_NoFile = (string)GetGlobalResourceObject(language, "msg_Cdriver_NoFile");
            msg_Cdriver_FileDeleted = (string)GetGlobalResourceObject(language, "msg_Cdriver_FileDeleted");
            msg_Cdriver_CertiExists = (string)GetGlobalResourceObject(language, "msg_Cdriver_CertiExists");
            msg_Cdriver_Deleted = (string)GetGlobalResourceObject(language, "msg_Cdriver_Deleted");
            msg_Cdriver_Waitting = (string)GetGlobalResourceObject(language, "msg_Cdriver_Waitting");
            msg_Cdriver_NoDrivers = (string)GetGlobalResourceObject(language, "msg_Cdriver_NoDrivers");
            msg_Cdriver_NoPermission = (string)GetGlobalResourceObject(language, "msg_Cdriver_NoPermission");
            msg_Cdriver_Added = (string)GetGlobalResourceObject(language, "msg_Cdriver_Added");
            msg_Cdriver_ErrorBL = (string)GetGlobalResourceObject(language, "msg_Cdriver_ErrorBL");
            msg_Cdriver_Update = (string)GetGlobalResourceObject(language, "msg_Cdriver_Update");
            msg_Cdriver_BLInsert = (string)GetGlobalResourceObject(language, "msg_Cdriver_BLInsert");
            msg_CdriverBLDeleted = (string)GetGlobalResourceObject(language, "msg_CdriverBLDeleted");
            msg_Cdriver_NoChanges = (string)GetGlobalResourceObject(language, "msg_Cdriver_NoChanges");
            msg_Cdriver_ExtensionPicture = (string)GetGlobalResourceObject(language, "msg_Cdriver_ExtensionPicture");
        }

        protected DataTable refresh_Grid_Add_Certificates()
        {
            DataTable dt_Fill_Grid = new DataTable();
            try
            {

                if (tbDriverID.Text!=string.Empty)
                {
                    dt_Fill_Grid = GenericClass.SQLSelectObj(new CERTIFICATE_ROUTE(), mappingQueryName: "CERTIFICATE", WhereClause: "where CERTIFICATE_ROUTE.Driver_ID = " + Convert.ToInt32(tbDriverID.Text.Trim()));

                    ViewState["dtg_Add_Certificates"] = dt_Fill_Grid;
                    dtg_Add_Certificates.DataSource = dt_Fill_Grid;
                    dtg_Add_Certificates.DataBind(); 
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            return dt_Fill_Grid;

        }

        protected DataTable refresh_Grid_Routes()
        {
            DataTable dt_Routes = new DataTable();
            try
            {
                if (tbDriverID.Text!=string.Empty)
                {

                    List<string> Columns = new List<string>();
                    Columns.Add("Route_ID");
                    Columns.Add("Route");
                    dt_Routes = GenericClass.SQLSelectObj(new Route(), mappingQueryName: "addRoute",
                        WhereClause: "where Route.Dest_ID in (select stop_ID from stop_point where end_Point=1)"
                        + " And Route_ID not in (select Route_ID from certificate_route where Driver_ID = " + Convert.ToInt32(tbDriverID.Text) + ")",
                        customSelect: Columns);
                    ViewState["dtg_Routes"] = dt_Routes;

                    dtg_Routes.DataSource = dt_Routes;

                    dtg_Routes.DataBind(); 
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
            return dt_Routes;
        }

        private void search()
        {
            try
            {
                GridView_Drivers.PageIndex = 0;
                string searchTerm = tbSearchCH1.Text.ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    fillDriverGrid();
                    dtNew.Clear();
                    ViewState["searchStatus"] = false;
                    ViewState["isSorting"] = false;
                }
                else
                {
                    ViewState["searchStatus"] = true;
                    ViewState["isSorting"] = false;
                    //always check if the viewstate exists before using it
                    if (ViewState["backupData"] == null)
                        return;

                    //cast the viewstate as a datatable
                    DataTable dt = ViewState["backupData"] as DataTable;

                    //make a clone of the datatable
                    dtNew = dt.Clone();

                    //search the datatable for the correct fields
                    foreach (DataRow row in dt.Rows)
                    {
                        //add your own columns to be searched here
                        if (row["Driver_ID"].ToString().ToLower().Contains(searchTerm) || row["Name"].ToString().ToLower().Contains(searchTerm) || row["License_No"].ToString().ToLower().Contains(searchTerm) || row["Address"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtNew.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    GridView_Drivers.DataSource = dtNew;
                    ViewState["searchResults"] = dtNew;
                    GridView_Drivers.DataBind();
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        public DataTable generalDataTable(string vendor = "")
        {
            DataTable dtReturn = new DataTable();
            try
            {

                string vendorFilter = usr.Vendor_ID.Equals("ALL") ? "" : "Where [Driver].[Vendor_ID]= '" + usr.Vendor_ID + "'";
                string vendorFilterAdmin = vendor.Equals("ALL") ? "" : "Where [Driver].[Vendor_ID]= '" + vendor + "'";

                dtReturn = GenericClass.SQLSelectObj(initialDriver, mappingQueryName: "Catalog", 
                    WhereClause: vendorFilter.Equals("") ? vendorFilterAdmin.Equals("") ? null : vendorFilterAdmin : vendorFilter,
                    OrderByClause: "order by driver.Is_Active desc, driver.Is_Block");
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

            return dtReturn;
        }

        private void ProcessFlow()
        {

            if (this.btAdd.Visible == true || (this.btAdd.Visible == false & this.btSave.Visible == false))
            {
                tbName.Enabled = false;

                tbBirthdate.Enabled = false;

                tbLicenseNo.Enabled = false;

                tbLicenseExp.Enabled = false;

                tbAddress.Enabled = false;

                tbTelephone.Enabled = false;

                cbVendor2.Enabled = false;

                cbActiveItem.Enabled = false;

                cbBlackListItem.Enabled = false;

                tbHireDate.Enabled = false;

                btShowUploads.Visible = false;

                btCertificate.Visible = false;

                tbDriverID.Text = string.Empty;

                tbName.Text = string.Empty;

                tbBirthdate.Text = string.Empty;

                tbLicenseNo.Text = string.Empty;

                tbLicenseExp.Text = string.Empty;

                tbAddress.Text = string.Empty;

                tbTelephone.Text = string.Empty;

                tbHireDate.Text = string.Empty;

                cbActiveItem.Checked = false;

                cbBlackListItem.Checked = false;

                GridView_Drivers.Columns[0].Visible = true;

                privilege.applyPrivilege("10001", GridView_Drivers);

                btSearchCH1.Enabled = true;

                tbSearchCH1.Enabled = true;

                btExcel.Enabled = true;

                cbVendorAdm.Enabled = true;

                btProfilePicture.Visible = false;

                form_Profilepicture.ImageUrl = "~/images/image-not-found.jpg";
            }
            if (this.btSave.Visible == true || this.btDelete.Visible == true)
            {

                tbName.Enabled = true;

                tbBirthdate.Enabled = true;

                tbLicenseNo.Enabled = true;

                tbLicenseExp.Enabled = true;

                tbAddress.Enabled = true;

                tbTelephone.Enabled = true;

                cbVendor2.Enabled = true;

                cbActiveItem.Enabled = true;

                if (tbDriverID.Text == string.Empty)
                {

                    btCertificate.Visible = false;

                    btShowUploads.Visible = false;

                    btProfilePicture.Visible = false;

                    cbActiveItem.Checked = true;
                }
                else
                {
                    btCertificate.Visible = true;

                    btShowUploads.Visible = true;

                    btProfilePicture.Visible = true;
                }


                if (usr.Rol_ID.Contains("ADMIN"))
                {
                    cbBlackListItem.Enabled = true;
                }
                tbHireDate.Enabled = true;

                GridView_Drivers.Columns[0].Visible = false;

                btSearchCH1.Enabled = false;

                tbSearchCH1.Enabled = false;

                btExcel.Enabled = false;

                cbVendorAdm.Enabled = false;


            }
        }

        private void saveDriver()
        {
            try
            {
                dtBirtdate = new DateTime();
                dtLicenseExpo = new DateTime();
                dtHireDate = new DateTime();

                DateTime.TryParseExact(tbBirthdate.Text, formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtBirtdate);
                DateTime.TryParseExact(tbLicenseExp.Text, formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtLicenseExpo);
                DateTime.TryParseExact(tbHireDate.Text, formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtHireDate);

                Driver addDriver = new Driver();
                addDriver.Name = tbName.Text.Trim();
                addDriver.Birthdate = dtBirtdate;
                addDriver.License_No = tbLicenseNo.Text.Trim();
                addDriver.License_Exp = dtLicenseExpo;
                addDriver.Address = tbAddress.Text.Trim();
                addDriver.Telephone = tbTelephone.Text.ToString();
                addDriver.Vendor_ID = cbVendor2.SelectedValue.ToString();
                addDriver.Is_Active = cbActiveItem.Checked;
                addDriver.Is_Block = cbBlackListItem.Checked;
                addDriver.Hiring_Date = dtHireDate;

                if (usr.Rol_ID.Contains("ADMIN"))
                {
                    GenericClass.SQLInsertObj(addDriver);
                    functions.ShowMessage(this, this.GetType(), msg_Cdriver_Added + " " + addDriver.Name, MessageType.Success);
                    //Validate if cbBllackListItem is checked. If it is checked , then save  correspondig data in Blacklist object 
                    if (cbBlackListItem.Checked == true)
                    {
                        //Get driver ID 
                        Blacklist blObj = new Blacklist();
                        DataTable getSavedDriver = GenericClass.SQLSelectObj(addDriver, WhereClause: "WHERE NAME='" + addDriver.Name + "' AND VENDOR_ID='" + addDriver.Vendor_ID + "' AND TELEPHONE='" + addDriver.Telephone + "'");
                        if (getSavedDriver.Rows.Count > 0)
                        {
                            blObj.Driver_ID = getSavedDriver.Rows[0]["Driver_ID"].ToString();
                            blObj.Vendor_ID = cbVendor2.SelectedValue.ToString();
                            blObj.BlackList_Date = System.DateTime.Now;
                            blObj.Reason = tbReasons.Text;
                            blObj.Comments = tbComments.Text;
                            GenericClass.SQLInsertObj(blObj);
                            functions.ShowMessage(this, this.GetType(), "Driver was added to blacklist correctly", MessageType.Success);
                        }
                        else
                        { functions.ShowMessage(this, this.GetType(), msg_Cdriver_ErrorBL + " ", MessageType.Error); }
                        cleanSearchAndSorting();
                    }
                }
                else
                {
                    Admin_Approve confirmInsert = new Admin_Approve(0, DateTime.Now, usr.User_ID.ToString(), "INSERT", addDriver.GetType().Name, "", GenericClass.formatValues(addDriver), "", "Un nuevo conductor ha sido agregado: " + addDriver.Name);
                    GenericClass.SQLInsertObj(confirmInsert);
                    SendEmail.sendEmailToAdmin(usr.User_ID.ToString(), confirmInsert);
                    SendEmail.sendEmailToDistributionList(addDriver.Vendor_ID, "Catalogs_Email", confirmInsert, usr.User_ID.ToString());
                    cleanSearchAndSorting();
                    functions.ShowMessage(this, this.GetType(), msg_Cdriver_Waitting + " " + addDriver.Name, MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }


        }

        private void updateDriver()
        {
            try
            {
                cleanSearchAndSorting();
                dtBirtdate = new DateTime();
                dtLicenseExpo = new DateTime();
                dtHireDate = new DateTime();
                int driverID = 0;
                int.TryParse(tbDriverID.Text, out driverID);

                DateTime.TryParseExact(tbBirthdate.Text, formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtBirtdate);
                DateTime.TryParseExact(tbLicenseExp.Text, formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtLicenseExpo);
                DateTime.TryParseExact(tbHireDate.Text, formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtHireDate);


                Driver updateDriver = new Driver();
                updateDriver.Driver_ID = driverID;
                updateDriver.Name = tbName.Text.Trim();
                updateDriver.Birthdate = dtBirtdate;
                updateDriver.License_No = tbLicenseNo.Text.Trim();
                updateDriver.License_Exp = dtLicenseExpo;
                updateDriver.Address = tbAddress.Text.Trim();
                updateDriver.Telephone = tbTelephone.Text.ToString();
                updateDriver.Vendor_ID = cbVendor2.SelectedValue.ToString();
                updateDriver.Is_Active = cbActiveItem.Checked;
                updateDriver.Is_Block = cbBlackListItem.Checked;
                updateDriver.Hiring_Date = dtHireDate;
                Admin_Approve newApprove = new Admin_Approve();
                DataTable getDriver = GenericClass.SQLSelectObj(initialDriver, mappingQueryName: "getDriver", WhereClause: " Where  [DRIVER].[Driver_ID]='" + driverID + "'");
                Driver currentDriver = new Driver();
                DateTime.TryParseExact(getDriver.Rows[0]["Birthdate(M/D/Y)"].ToString(), formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtBirtdate);
                DateTime.TryParseExact(getDriver.Rows[0]["License_Exp(M/D/Y)"].ToString(), formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtLicenseExpo);
                DateTime.TryParseExact(getDriver.Rows[0]["Hiring_Date(M/D/Y)"].ToString(), formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtHireDate);

                currentDriver.Driver_ID = Int32.Parse(getDriver.Rows[0]["Driver_ID"].ToString());
                currentDriver.Name = getDriver.Rows[0]["Name"].ToString();
                currentDriver.Birthdate = dtBirtdate;
                currentDriver.License_No = getDriver.Rows[0]["License_No"].ToString();
                currentDriver.License_Exp = dtLicenseExpo;
                currentDriver.Address = getDriver.Rows[0]["Address"].ToString();
                currentDriver.Telephone = getDriver.Rows[0]["Telephone"].ToString();
                currentDriver.Vendor_ID = getDriver.Rows[0]["Vendor_ID"].ToString();
                currentDriver.Is_Active = Convert.ToBoolean(getDriver.Rows[0]["Is_Active"].ToString());
                currentDriver.Is_Block = Convert.ToBoolean(getDriver.Rows[0]["Is_Block"].ToString());
                currentDriver.Hiring_Date = dtHireDate;

                newApprove = adminApprove.compareObjects(currentDriver, updateDriver);
                if (newApprove.New_Values != "No values changed")
                {
                    if (usr.Rol_ID.Contains("ADMIN"))
                    {
                        Blacklist blObjUpd = new Blacklist();

                        //Get current value of Blacklist field in database
                        if (currentDriver.Is_Block != updateDriver.Is_Block)
                        {
                            DataTable getBlacklistRecord = GenericClass.SQLSelectObj(blObjUpd, WhereClause: "WHERE DRIVER_ID='" + tbDriverID.Text.Trim() + "'");

                            if (updateDriver.Is_Block == true)
                            {
                                if (getBlacklistRecord.Rows.Count <= 0)
                                {
                                    Blacklist blObj = new Blacklist();
                                    blObj.Driver_ID = currentDriver.Driver_ID.ToString();
                                    blObj.BlackList_Date = System.DateTime.Now;
                                    blObj.Vendor_ID = currentDriver.Vendor_ID;
                                    blObj.Reason = tbReasons.Text;
                                    blObj.Comments = tbComments.Text;
                                    GenericClass.SQLInsertObj(blObj);

                                    functions.ShowMessage(this, this.GetType(), msg_Cdriver_BLInsert + " ", MessageType.Success);
                                }
                            }
                            else
                            {
                                if (getBlacklistRecord.Rows.Count > 0)
                                {
                                    Blacklist blObj = new Blacklist();
                                    blObj.Driver_ID = currentDriver.Driver_ID.ToString();
                                    GenericClass.SQLDeleteObj(blObj, WhereClause: "WHERE DRIVER_ID= '" + blObj.Driver_ID + "'");
                                    functions.ShowMessage(this, this.GetType(), msg_CdriverBLDeleted, MessageType.Success);
                                }
                            }

                        }


                        GenericClass.SQLUpdateObj(updateDriver, adminApprove.compareObjects(currentDriver, updateDriver, ""), "Where Driver_ID=" + updateDriver.Driver_ID);
                        functions.ShowMessage(this, this.GetType(), msg_Cdriver_Update + " " + updateDriver.Driver_ID + "-" + updateDriver.Name, MessageType.Success);

                    }
                    else
                    {
                        newApprove.Activity_ID = 0;
                        newApprove.Admin_Confirm = false;
                        newApprove.Activity_Date = DateTime.Now;
                        newApprove.User_ID = usr.User_ID;
                        newApprove.Type = "UPDATE";
                        newApprove.Module = updateDriver.GetType().Name;
                        newApprove.Where_Clause = "Where [DRIVER].[Driver_ID]='" + driverID + "'";
                        newApprove.Comments = "[Driver][" + updateDriver.Name + "]<br>" + newApprove.Comments;
                        GenericClass.SQLInsertObj(newApprove);
                        SendEmail.sendEmailToAdmin(usr.User_ID.ToString(), newApprove);
                        SendEmail.sendEmailToDistributionList(currentDriver.Vendor_ID, "Catalogs_Email", newApprove, usr.User_ID.ToString());
                        functions.ShowMessage(this, this.GetType(), msg_Cdriver_Waitting + " " + currentDriver.Driver_ID + "-" + currentDriver.Name, MessageType.Warning);

                    }
                }

                else
                {
                    functions.ShowMessage(this, this.GetType(), msg_Cdriver_NoChanges, MessageType.Info);
                }
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
                DataTable dtLoad = generalDataTable(cbVendorAdm.SelectedValue);
                Session["Excel"] = dtLoad;
                Session["name"] = "Driver";
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        public void PrivilegeAndProcessFlow(string turnOnOffBNuttons)
        {
            privilege.applyPrivilege(turnOnOffBNuttons, GridView_Drivers);
            ProcessFlow();

        }

        private bool validateFile()
        {
            string[] validFileTypes = { "gif", "png", "jpg", "jpeg", "doc", "docx", "xls", "xlsx", "pdf" };
            string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
            bool isValidFile = false;
            for (int i = 0; i < validFileTypes.Length; i++)
            {
                if (ext == "." + validFileTypes[i])
                {
                    isValidFile = true;
                    break;
                }
            }
            return isValidFile;
        }

        private bool validatePicture()
        {
            string[] validFileTypes = { "gif", "png", "jpg", "jpeg" };
            string ext = System.IO.Path.GetExtension(FileUpload2.PostedFile.FileName);
            bool isValidFile = false;
            for (int i = 0; i < validFileTypes.Length; i++)
            {
                if (ext == "." + validFileTypes[i])
                {
                    isValidFile = true;
                    break;
                }
            }
            return isValidFile;
        }

        private bool validateNameOrFileExists(string fileName)
        {
            bool result = false;
            try
            {

                Driver_Documents docObj = new Driver_Documents();
                DataTable dtGetFileNames = GenericClass.SQLSelectObj(docObj, WhereClause: "WHERE FILE_NAME='" + fileName + "'");
                if (dtGetFileNames.Rows.Count > 0)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

            return result;


        }

        private void fillGridDocuments()
        {
            try
            {
                int driverID = Convert.ToInt32(tbDriverID.Text.Trim());
                Driver_Documents docObj = new Driver_Documents();
                DataTable dtGetData = GenericClass.SQLSelectObj(docObj, WhereClause: "WHERE DRIVER_ID=" + driverID);
                GridView_Documents.DataSource = dtGetData;
                GridView_Documents.DataBind();
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

     void profilePicture()
        {
            try
            {
                DataTable dt_ProfilePicture = GenericClass.SQLSelectObj(new Driver_Documents(), WhereClause: "where Driver_ID = " + Convert.ToInt32(tbDriverID.Text.Trim()) + "  and Category_file = 'ProfilePicture'");
                if (dt_ProfilePicture.Rows.Count == 0)
                {
                    string path = "~/images/image-not-found.jpg";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "formProfilePictureChange();", true);
                    profilePictureDialog.ImageUrl = path;
                    btUploadProfilePicture.Visible = true;
                    btDeleteProfilePicture.Visible = false;
                    btChangePicture.Visible = false;

                }
                else
                {
                    FileUpload2.Enabled = false;
                    btUploadProfilePicture.Visible = false;
                    btDeleteProfilePicture.Visible = true;
                    btChangePicture.Visible = true;
                    string path = ConfigurationManager.AppSettings["profilePictureImagesPathToDisplay"].ToString() + dt_ProfilePicture.Rows[0]["Short_Path"].ToString() + dt_ProfilePicture.Rows[0]["File_Name"].ToString();
                    form_Profilepicture.ImageUrl = path;
                    profilePictureDialog.ImageUrl = path;
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        private string[] createServerPathsStrings()
        {
            DateTime now = DateTime.Now;
            DataTable dt_Driver = GenericClass.SQLSelectObj(new Driver(), mappingQueryName: "Driver_name", WhereClause: "Where Driver.Driver_ID =" + tbDriverID.Text);
            //string vendor = (string)ViewState["Vendor"];
            string vendor = dt_Driver.Rows[0]["Name"].ToString();
            string month = now.ToString("MMMM");
            string year = now.ToString("yyyy");
            string serverPath = null;
            if (ViewState["Path"].ToString() == "ProfilePicture")
            {
                serverPath = ConfigurationManager.AppSettings["profilePicture_ImagesPath"];
            }
            else
            {
                serverPath = ConfigurationManager.AppSettings["DriversDocuments_DocumentsPath"];
            }
            string longPath = serverPath + vendor + "\\" + year + "\\" + month + "\\";
            string shortPath = vendor + "\\" + year + "\\" + month + "\\";
            string[] arrayPaths = { shortPath, longPath };
            return arrayPaths;
        }

        private void createDirectorysOnServer(string longPath)
        {
            if (!Directory.Exists(longPath))
            {
                Directory.CreateDirectory(longPath);
            }
        }


        #endregion










    }
}