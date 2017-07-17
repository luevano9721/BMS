using BusManagementSystem._01Catalogos;
using BusManagementSystem.Catalogos;
using BusManagementSystem.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusManagementSystem.Maintenance
{
    public partial class EditRevision : System.Web.UI.Page
    {
        string language = null;
        string msg_EditRevisions_RevisionID;
        string msg_EditRevisions_ImgUpload;
        string msg_EditRevisions_ErrorCharacter;
        string msg_EditRevisions_SelectImage;
        string msg_EditRevisions_AlertsDeleted;
        string msg_EditRevisions_AlertCreated;
        string msg_EditRevisions_AlertCreatedSuc;
        string msg_EditRevisions_DataSaved;
        string msg_EditRevisions_ImageUploaded;
        string msg_EditRevisions_CharacterError;
        string msg_EditRevisions_SelectNcontinue;
        List<CheckBox> stepControls;
        List<CheckBox> level2Controls;
        List<CheckBox> level3Controls;

        protected void Page_Load(object sender, EventArgs e)
        {
           
           
            try
            {
               
                lbRevisionCaseID.Text = (string)ViewState["lbRevisionCaseID"];
                lbRevisionNoteID.Text = (string)ViewState["lbRevisionNoteID"];
                applyLanguage();
                messages_ChangeLanguage();
                string revisionID = Request.QueryString["Revision"];
                if (validateRevisionID(revisionID))
                {
                    hfLegalRevisionID.Value = revisionID;
                    if (!this.IsPostBack)
                    {
                         bindRepeaterHeader();
                        
                        //Initialize control list by level
                        stepControls = new List<CheckBox>();
                        level2Controls = new List<CheckBox>();
                        level3Controls = new List<CheckBox>();
                        // Get controls and save them in each list
                        GetControlList<CheckBox>(PanelControls.Controls, stepControls, "Item"); //22
                        GetControlList<CheckBox>(PanelControls.Controls, level2Controls, "Medium"); //25
                        GetControlList<CheckBox>(PanelControls.Controls, level3Controls, "High"); //19
                        // Set checkboxes with current value in database
                        getMethodsInspectionValues();
                        getStepValuesTypeMedium();
                        getStepValuesTypeHigh();
                    }
                    
                }
                else
                {
                    functions.ShowMessage(this, this.GetType(), msg_EditRevisions_RevisionID, MessageType.Error);
                    Repeater1.Visible = false;
                    PanelControls.Enabled = false;
                    btSaveData.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        private void applyLanguage()
        {
            Users usr = (Users)(Session["C_USER"]);
            functions func = new functions();
            language = Session["Language"] != null ? Session["Language"].ToString() : language = functions.GetLanguage(usr);
            func.languageTranslate(this.Master, language);
        }
        protected void messages_ChangeLanguage()
        {
            msg_EditRevisions_RevisionID = (string)GetGlobalResourceObject(language, "msg_EditRevisions_RevisionID");
            msg_EditRevisions_ImgUpload = (string)GetGlobalResourceObject(language, "msg_EditRevisions_ImgUpload");
            msg_EditRevisions_ErrorCharacter  = (string)GetGlobalResourceObject(language, "msg_EditRevisions_ErrorCharacter");
            msg_EditRevisions_SelectImage  = (string)GetGlobalResourceObject(language, "msg_EditRevisions_SelectImage");
            msg_EditRevisions_AlertsDeleted  = (string)GetGlobalResourceObject(language, "msg_EditRevisions_AlertsDeleted");
            msg_EditRevisions_AlertCreated  = (string)GetGlobalResourceObject(language, "msg_EditRevisions_AlertCreated");
            msg_EditRevisions_AlertCreatedSuc  = (string)GetGlobalResourceObject(language, "msg_EditRevisions_AlertCreatedSuc");
            msg_EditRevisions_DataSaved  = (string)GetGlobalResourceObject(language, "msg_EditRevisions_DataSaved");
            msg_EditRevisions_ImageUploaded  = (string)GetGlobalResourceObject(language, "msg_EditRevisions_ImageUploaded");
            msg_EditRevisions_CharacterError  = (string)GetGlobalResourceObject(language, "msg_EditRevisions_CharacterError");
            msg_EditRevisions_SelectNcontinue  = (string)GetGlobalResourceObject(language, "msg_EditRevisions_SelectNcontinue");

           
        
        }
        private bool validateRevisionID(string revisionID)
        {

            int revision_ID = Convert.ToInt32(revisionID);
            bool isValid = false;
            DataTable dtGetRevision = GenericClass.GetCustomData("SELECT TOP 1 Legal_Revision_ID from Legal_Revision where Status='OPEN' and Legal_Revision_ID=" + revision_ID);
            if (dtGetRevision.Rows.Count == 1)
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }
            return isValid;
        }

        //This method is to obtain all checkboxes by level (1= Item , 2= Medium , 3= High)
        private void GetControlList<T>(ControlCollection controlCollection, List<T> resultCollection, string level)
        where T : Control
        {
            try
            {
                foreach (Control control in controlCollection)
                {
                    if (control.GetType() == typeof(CheckBox)) //Validate if the control is  a chechbox
                    {
                        if (control.ID.Contains(level)) // If it is a chechbox and is
                            resultCollection.Add((T)control);
                    }
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        private int  getSelectedRevisionNoteID( int Legal_Revision_ID ,  string controlName)
        {
            int revisionNoteID = -1;
            try
            {
                
                DataTable dtGet = GenericClass.GetCustomData("SELECT TOP 1 REVISION_NOTE.Revision_Note_ID FROM LEGAL_REVISION" +
                " LEFT JOIN REVISION_NOTE ON LEGAL_REVISION.Legal_Revision_ID = REVISION_NOTE.Legal_Revision_ID" +
                " LEFT JOIN CAT_REVISION_CASE ON REVISION_NOTE.Revision_Case_ID = CAT_REVISION_CASE.Revision_Case_ID" +
                " WHERE LEGAL_REVISION.Legal_Revision_ID=" + Legal_Revision_ID + "and CAT_REVISION_CASE.Button_Name=" + "'" + controlName + "'");

                if (dtGet.Rows.Count > 0)
                {
                    revisionNoteID = Convert.ToInt32(dtGet.Rows[0]["Revision_Note_ID"].ToString());
                }             
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

            return revisionNoteID;        
        }



        private void getMethodsInspectionValues()
        {
            try
            {
                DataTable dtGetMethods = GenericClass.GetCustomData("select CAT_REVISION_STEP.Control_Name , REVISION_CHECK.is_checked from [REVISION_CHECK]" +
                " left join CAT_REVISION_STEP on [REVISION_CHECK].Revision_Step_ID=CAT_REVISION_STEP.Revision_Step_ID" +
                " where Legal_Revision_ID=" + hfLegalRevisionID.Value);
                foreach (DataRow row in dtGetMethods.Rows)
                {
                    foreach (CheckBox item in stepControls)
                    {
                        if (row["Control_Name"].ToString() == item.ID.ToString())
                        {
                            item.Checked = Convert.ToBoolean(row["is_checked"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        private void getStepValuesTypeMedium()
        {
            try
            {
                DataTable dtGetValuesTypeOne = GenericClass.GetCustomData("select CAT_REVISION_CASE.Control_name , REVISION_NOTE.is_checked from REVISION_NOTE" +
                 " left join CAT_REVISION_CASE on REVISION_NOTE.Revision_Case_ID=CAT_REVISION_CASE.Revision_Case_ID" +
                 " where CAT_REVISION_CASE.Severity='MEDIUM' and Legal_Revision_ID=" + hfLegalRevisionID.Value);
                foreach (DataRow row in dtGetValuesTypeOne.Rows)
                {
                    foreach (CheckBox item in level2Controls)
                    {
                        if (row["Control_Name"].ToString() == item.ID.ToString())
                        {
                            item.Checked = Convert.ToBoolean(row["is_checked"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        private void getStepValuesTypeHigh()
        {
            try
            {
                DataTable dtGetValuesTypeTwo = GenericClass.GetCustomData("select CAT_REVISION_CASE.Control_name , REVISION_NOTE.is_checked from REVISION_NOTE" +
                    " left join CAT_REVISION_CASE on REVISION_NOTE.Revision_Case_ID=CAT_REVISION_CASE.Revision_Case_ID" +
                    " where CAT_REVISION_CASE.Severity='HIGH' and Legal_Revision_ID=" + hfLegalRevisionID.Value);
                foreach (DataRow row in dtGetValuesTypeTwo.Rows)
                {
                    foreach (CheckBox item in level3Controls)
                    {
                        if (row["Control_Name"].ToString() == item.ID.ToString())
                        {
                            item.Checked = Convert.ToBoolean(row["is_checked"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        private void bindRepeaterHeader()
        {
            try
            {
                List<String> customFields = new List<string>();
                customFields.Add("Legal_Revision_ID");
                Legal_Revision RevisionObj = new Legal_Revision();
                DataTable dtGetHeader = GenericClass.SQLSelectObj(RevisionObj, customSelect: customFields, mappingQueryName: "MTO-GetRevisions", WhereClause: "Where Legal_Revision_ID=" + hfLegalRevisionID.Value);
                ViewState["Vendor"]=  dtGetHeader.Rows[0]["Proveedor"].ToString();
                Repeater1.DataSource = dtGetHeader;
                Repeater1.DataBind();
        
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    foreach (Control c in item.Controls)
                    {
                        if (c is Label)
                    {
                        try
                        {
                            string lenguageChange = (string)GetGlobalResourceObject(language, ((Label)c).ID);

                            ((Label)c).Text = lenguageChange == null ? ((Label)c).Text : lenguageChange;
                        }
                        catch
                        {

                        }
                    }
                    }
                   
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void chAlineacionMedium1Img_Click(object sender, ImageClickEventArgs e)
        {           
        }
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
        }

       

        protected void btAllUploadImage_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //validate if Revision_Note is true , then show openImageDialog
                ImageButton iBtn = (ImageButton)sender;
                string button_Name = iBtn.ID;
                int revisionCaseID = -1;
                DataTable getStatusRevisionN = GenericClass.GetCustomData("SELECT IS_CHECKED , REVISION_NOTE.REVISION_CASE_ID FROM REVISION_NOTE LEFT JOIN CAT_REVISION_CASE ON" +
                " REVISION_NOTE.REVISION_CASE_ID= CAT_REVISION_CASE.REVISION_CASE_ID WHERE CAT_REVISION_CASE.BUTTON_NAME=" + "'" + button_Name + "' AND REVISION_NOTE.LEGAL_REVISION_ID=" + hfLegalRevisionID.Value);
                bool is_checked;
                if (getStatusRevisionN.Rows.Count > 0)
                {
                    is_checked = Convert.ToBoolean(getStatusRevisionN.Rows[0]["IS_CHECKED"].ToString());
                    revisionCaseID = Convert.ToInt32(getStatusRevisionN.Rows[0]["REVISION_CASE_ID"].ToString());
                }
                else
                {
                    is_checked = false;
                }                
                if (is_checked)
                {
                    ImageButton selectedControl = (ImageButton)sender;
                    int legal_Revision_ID = Convert.ToInt32(hfLegalRevisionID.Value);
                    int revisionNoteID = getSelectedRevisionNoteID(legal_Revision_ID, selectedControl.ID);
                    if (revisionNoteID > 0)
                    {
                        openImageDialog(revisionCaseID, revisionNoteID);
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "noFoundDialog('" + (string)GetGlobalResourceObject(language, "lbl_EditRevison_NoFoundTitle") + "');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "noFoundDialog('" + (string)GetGlobalResourceObject(language, "lbl_EditRevison_NoFoundTitle") + "');", true);
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        private void openImageDialog(int revisionCaseID, int revisionNoteID)
        {
            try
            {
                DataTable rf = new DataTable();
                ViewState["lbRevisionCaseID"]= revisionCaseID.ToString();
                lbRevisionCaseID.Text = revisionCaseID.ToString();
                ViewState["lbRevisionNoteID"] = revisionNoteID.ToString();
                lbRevisionNoteID.Text = revisionNoteID.ToString();
                rf = GenericClass.GetCustomData("SELECT CAT_REVISION_CASE.DESCRIPTION , CAT_REVISION.NAME FROM CAT_REVISION_CASE LEFT JOIN CAT_REVISION ON CAT_REVISION_CASE.REVISION_TYPE_ID= CAT_REVISION.REVISION_TYPE_ID WHERE REVISION_CASE_ID=" + revisionCaseID);
                lbl_EditRevision_MetodoInspec.Text = rf.Rows[0]["Name"].ToString();
                lblEditRevision_Case.Text = rf.Rows[0]["Description"].ToString();
                FetchImage(revisionNoteID, "Before");
                FetchImage(revisionNoteID, "After");
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "uploadImageDialog('" + (string)GetGlobalResourceObject(language, "lbl_EditRevison_NoFoundTitle") + "');", true);                
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }
        
        protected void btUploadImage_Click(object sender, EventArgs e)
        {
            uploadImageFunction("Before", FileUpload1);
        }

        private void uploadImageFunction(string type , FileUpload fileUploadCtrl)
        {
            try
            {
                int RevisionNoteID = Convert.ToInt32(ViewState["lbRevisionNoteID"]);
                int RevisionCaseID = Convert.ToInt32(ViewState["lbRevisionCaseID"]);
                if (fileUploadCtrl.HasFile)
                {
                    string imgName = fileUploadCtrl.PostedFile.FileName;

                    if (!imgName.Contains("#"))
                    {
                        string[] arrayPaths = createServerPathsStrings(type);
                        createDirectorysOnServer(arrayPaths[1]);
                        if (!System.IO.File.Exists(arrayPaths[1] + imgName))
                        {
                            fileUploadCtrl.PostedFile.SaveAs(arrayPaths[1] + imgName);
                            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(constr))
                            {
                                con.Open();
                                SqlCommand com = new SqlCommand("INSERT INTO [REVISION_NOTE_IMAGE] (Revision_Note_ID ," + type + " , Image_Name, Short_Path) values (@idRev, @pathImage , @imageName , @shortPath)", con);
                                try
                                {
                                    com.Parameters.AddWithValue("@idRev", RevisionNoteID);
                                    com.Parameters.AddWithValue("@pathImage", arrayPaths[1] + imgName);
                                    com.Parameters.AddWithValue("@imageName", imgName);
                                    com.Parameters.AddWithValue("@shortPath", arrayPaths[0]);
                                    com.ExecuteNonQuery();
                                    openImageDialog(RevisionCaseID, RevisionNoteID);
                                    functions.ShowMessage(this, this.GetType(), msg_EditRevisions_ImgUpload, MessageType.Success);
                                }
                                catch (SqlException ex)
                                {
                                    functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
                                }
                            }
                        }
                        else
                        {
                            functions.ShowMessage(this, this.GetType(), "Ya existe un archivo con el mismo nombre", MessageType.Error);
                            openImageDialog(RevisionCaseID, RevisionNoteID);
                        }
                    }
                    else
                    {
                        functions.ShowMessage(this, this.GetType(), msg_EditRevisions_ErrorCharacter, MessageType.Error);

                        openImageDialog(RevisionCaseID, RevisionNoteID);
                    }
                }
                else
                {
                    functions.ShowMessage(this, this.GetType(), msg_EditRevisions_SelectImage, MessageType.Warning);
                    openImageDialog(RevisionCaseID, RevisionNoteID);
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        private string[] createServerPathsStrings(string type)
        {
            DateTime now = DateTime.Now;
            string vendor = (string)ViewState["Vendor"];
            string month = now.ToString("MMMM");
            string year = now.ToString("yyyy");
            string serverPath = ConfigurationManager.AppSettings["revisionImagesPath"];
            string longPath = serverPath + vendor + "\\" + year + "\\" + month + "\\" + type + "\\";
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

        protected void FetchImage(int revisionNoteID , string type)
        {
            try
            {
                if (type == "After" || type == "Before")
                {
                    string revisionImagesPathToDisplay = ConfigurationManager.AppSettings["revisionImagesPathToDisplay"];
                    DataTable getImages = new DataTable();
                    getImages = GenericClass.GetCustomData("SELECT " + type + " , Image_name , Short_Path FROM REVISION_NOTE_IMAGE WHERE Revision_Note_ID =" + revisionNoteID+ " and "+ type+" is not null");
                    foreach (DataRow row in getImages.Rows)
                    {
                        if (row[type].ToString() != string.Empty)
                        {
                            if (System.IO.File.Exists(row[type].ToString()))
                            {
                                Image img = new Image();
                                string short_Path = row["Short_Path"].ToString().Replace("\\", "/");
                                img.ImageUrl = revisionImagesPathToDisplay + short_Path +"/"+type+"/"+ row["Image_name"].ToString();
                                img.Width = 80;
                                img.Height = 80;
                                img.CssClass = "imageUpload";
                                img.ID = "IMG" + type + row["Image_Name"].ToString();
                                CheckBox a = new CheckBox();
                                a.ID = "#" + row["Image_Name"].ToString() + "#Checkbox" + type;

                                if (type == "Before")
                                {
                                    phImageContentAntes.Controls.Add(img);
                                    phImageContentAntes.Controls.Add(a);
                                }
                                else if (type == "After")
                                {
                                    phImageContentdespues.Controls.Add(img);
                                    phImageContentdespues.Controls.Add(a);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        [WebMethod]
        public static string deleteImg(string[] Param, int revisionID, int revisionNoteID)
        {
            string result = "Image was not deleted";           
            if (Param != null)
            {
                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;            
                foreach (string name in Param)
                {
                    new EditRevision().DeleteFileFromFolder(name, revisionNoteID);
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand("DELETE FROM [REVISION_NOTE_IMAGE] WHERE Revision_Note_ID=@ID and Image_Name=@ImageName", con);
                        com.Parameters.AddWithValue("@ImageName", name);
                        com.Parameters.AddWithValue("@ID", revisionNoteID);
                        com.ExecuteNonQuery();
                        
                        result = "Image deleted successfully";
                    }

                }
            }

            else
            {
                result = "An unexpected error occurred ";
            }
            return result;

        }

        protected void btDeleteImage_Click(object sender, EventArgs e)
        {
        }

        protected void btDeleteImage_Click1(object sender, EventArgs e)
        {
            int RevisionNoteID = Convert.ToInt32(ViewState["lbRevisionNoteID"]);
            int RevisionID = Convert.ToInt32(ViewState["lbRevisionCaseID"]);
            openImageDialog(RevisionID, RevisionNoteID);
        }

        protected void checkboxLevel2And3_CheckedChanged(object sender, EventArgs e)
        {
            //Get revision_case_id of control

            try
            {
                
                bool is_checked;
                string controlName;
                string controlNameParent = "";
                CheckBox ch = (CheckBox)sender;
                if (ch.Checked)
                {
                    controlName = ch.ID;
                    ControlNameAlert.Text = controlName;
                    tbAction.Text = string.Empty;
                    tbproposalDate.Text = string.Empty;
                    stepControls = new List<CheckBox>();
                    GetControlList<CheckBox>(PanelControls.Controls, stepControls, "Item"); //22

                    if (controlName.Contains("Medium"))
                    {
                        controlNameParent = controlName.Replace("Medium", "Item");
                        ViewState["typeAlert"] = "MEDIUM";
                    }

                    if (controlName.Contains("High"))
                    {
                        controlNameParent = controlName.Replace("High", "Item");
                        ViewState["typeAlert"] = "HIGH";
                    }


                    if (ch.Checked)
                    {
                        DataTable getRevisionCase = GenericClass.GetCustomData("SELECT REVISION_CASE_ID FROM CAT_REVISION_CASE WHERE CONTROL_NAME=" + "'" + controlName + "'");
                        hfRevisionCaseId.Value = getRevisionCase.Rows[0]["REVISION_CASE_ID"].ToString();
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "confirmAlertDialog('" + (string)GetGlobalResourceObject(language, "lbl_EditRevison_NoFoundTitle") + "');", true);
                        is_checked = true;

                    }
                    else
                    {
                        DataTable getStatusRevisionN = GenericClass.GetCustomData("SELECT REVISION_NOTE_ID FROM REVISION_NOTE LEFT JOIN CAT_REVISION_CASE ON " +
                        "REVISION_NOTE.REVISION_CASE_ID= CAT_REVISION_CASE.REVISION_CASE_ID WHERE  REVISION_NOTE.LEGAL_REVISION_ID=" + hfLegalRevisionID.Value + " AND CAT_REVISION_CASE.CONTROL_NAME=" + "'" + controlName + "'");
                        if (getStatusRevisionN.Rows.Count > 0)
                        {
                            Revision_Note revNote = new Revision_Note();
                            revNote.Revision_note_id = Convert.ToInt32(getStatusRevisionN.Rows[0]["REVISION_NOTE_ID"]);
                            revNote.Is_Checked = false;
                            Dictionary<string, dynamic> dicUpdRev = new Dictionary<string, dynamic>();
                            dicUpdRev.Add("Is_Checked", false);
                            GenericClass.SQLUpdateObj(revNote, dicUpdRev, "Where REVISION_NOTE_ID=" + revNote.Revision_note_id);

                            //Delete all alerts for this Revision Note
                            List<Alert_Log> alertList = getAlertRevisionNote(revNote.Revision_note_id);
                            foreach (Alert_Log alert in alertList)
                            {
                                GenericClass.SQLDeleteObj(alert, WhereClause: "WHERE Alert_Log_ID=" + alert.Alert_log_ID);
                            }
                            functions.ShowMessage(this, this.GetType(), msg_EditRevisions_AlertsDeleted, MessageType.Success);

                        }
                        is_checked = false;
                    }
                    foreach (CheckBox stepChecboxes in stepControls)
                    {
                        if (stepChecboxes.ID == controlNameParent)
                        {
                            stepChecboxes.Checked = is_checked;
                        }
                    }
                }
                

            }

            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

        private List<Alert_Log> getAlertRevisionNote(int Revision_note_id)
        {
            List<Alert_Log> alertList = new List<Alert_Log>();
            try
            {
                Alert_Log alert = new Alert_Log();
                string rissed_by = "REVNOTE:" + Revision_note_id;
                DataTable dtGetAlert = GenericClass.SQLSelectObj(alert, WhereClause: "WHERE RISSED_BY=" + "'" + rissed_by + "'");
                if (dtGetAlert.Rows.Count > 0)
                {
                    Alert_Log aLog = new Alert_Log();
                    foreach (DataRow dr in dtGetAlert.Rows)
                    {
                        alert.Alert_log_ID = Convert.ToInt32(dr["ALERT_LOG_ID"].ToString());
                        alertList.Add(alert);
                    }
                }
               
            }
       
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

            return alertList;
        }


        protected void btconfirmAlert_Click(object sender, EventArgs e)
        {            
            int revisionNoteID = getRevisionNoteID();
            DateTime proposalDate = new DateTime();
            try
            {
                DateTime.TryParseExact(tbproposalDate.Text, "MM/dd/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out proposalDate);
                if (revisionNoteID > 0)
                {
                    
                    Revision_Note revNote = new Revision_Note();
                    revNote.Revision_note_id = revisionNoteID;
                    revNote.Is_Checked = true;
                    revNote.Description = tbAction.Text;
                    revNote.Proposal_date = proposalDate;
                    Dictionary<string, dynamic> dicUpdRev = new Dictionary<string, dynamic>();
                    dicUpdRev.Add("Is_Checked", true);
                    dicUpdRev.Add("Description", revNote.Description);
                    GenericClass.SQLUpdateObj(revNote, dicUpdRev, "Where REVISION_NOTE_ID=" + revNote.Revision_note_id);
                    createAlert(revNote.Revision_note_id);
                    functions.ShowMessage(this, this.GetType(), msg_EditRevisions_AlertCreated, MessageType.Success);
                }
                else
                {
                    //Crear objeto RevisionNote
                    Revision_Note rNoteObj = new Revision_Note();
                    rNoteObj.Legal_revision_id = Convert.ToInt32(hfLegalRevisionID.Value);
                    rNoteObj.Revision_case_id = Convert.ToInt32(hfRevisionCaseId.Value);
                    rNoteObj.Is_Checked = true;
                    rNoteObj.Description = tbAction.Text;
                    rNoteObj.Proposal_date = proposalDate;
                    GenericClass.SQLInsertObj(rNoteObj);
                    revisionNoteID = getRevisionNoteID();
                    createAlert(revisionNoteID);
                    functions.ShowMessage(this, this.GetType(), msg_EditRevisions_AlertCreatedSuc, MessageType.Success);
                }

            }

            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
 
        }

        private int getRevisionNoteID()
        {
            int RevisionNoteID=-1;
            try
            {
                
                DataTable getStatusRevisionN = GenericClass.GetCustomData("SELECT REVISION_NOTE_ID FROM REVISION_NOTE LEFT JOIN CAT_REVISION_CASE ON " +
                "REVISION_NOTE.REVISION_CASE_ID= CAT_REVISION_CASE.REVISION_CASE_ID WHERE  REVISION_NOTE.LEGAL_REVISION_ID=" + hfLegalRevisionID.Value + " AND CAT_REVISION_CASE.Revision_Case_ID=" + hfRevisionCaseId.Value);
                if (getStatusRevisionN.Rows.Count > 0)
                {
                    RevisionNoteID = Convert.ToInt32(getStatusRevisionN.Rows[0]["REVISION_NOTE_ID"].ToString());                  
                }
                else
                { RevisionNoteID = -1; }                
            }

            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

            return RevisionNoteID;
        }

        private void createAlert(int revisionNoteID)
        {
            try
            {

                Alert_Log alert = new Alert_Log();
                DateTime dtProposalDate = new DateTime();
                string[] formats = { "MM/dd/yyyy", "MM/dd/yy" };
                DateTime.TryParseExact(tbproposalDate.Text, formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtProposalDate);
                
                DataTable getInformation = GenericClass.GetCustomData("SELECT BUS_DRIVER.Bus_ID , BUS_DRIVER.Driver_ID ,  LEGAL_REVISION.Vendor_ID FROM LEGAL_REVISION" +
                " LEFT JOIN BUS_DRIVER ON LEGAL_REVISION.Bus_Driver_ID = BUS_DRIVER.Bus_Driver_ID WHERE LEGAL_REVISION.LEGAL_REVISION_ID=" + hfLegalRevisionID.Value);

                DataTable getAlertCode = GenericClass.GetCustomData("SELECT CAT_REVISION_STEP.Alert_Code_Id FROM REVISION_NOTE" +
                " LEFT JOIN CAT_REVISION_CASE ON REVISION_NOTE.Revision_Case_ID = CAT_REVISION_CASE.Revision_Case_ID" +
                " LEFT JOIN CAT_REVISION_STEP ON CAT_REVISION_CASE.REVISION_STEP_ID = CAT_REVISION_STEP.Revision_Step_ID WHERE REVISION_NOTE_ID=" + revisionNoteID);


                alert.Code = getAlertCode.Rows[0]["Alert_Code_Id"].ToString();
                alert.Alert_Date = DateTime.Now; ;
                alert.Bus_ID = getInformation.Rows[0]["Bus_ID"].ToString();
                alert.Driver_ID = Convert.ToInt32(getInformation.Rows[0]["Driver_ID"].ToString());
                alert.Rissed_By = "REVNOTE:" + revisionNoteID;
                alert.Comments = tbAction.Text;
                alert.Status = "OPEN";
                alert.Closed_By = null;
                alert.Last_Check = dtProposalDate;
                alert.Ticket_Close = dtProposalDate;
                alert.Vendor_ID = getInformation.Rows[0]["Vendor_ID"].ToString();
                alert.Priority = (string)ViewState["typeAlert"];
                alert.Off_Date = dtProposalDate;
                if (alert.Priority.ToUpper().Equals("HIGH"))
                {
                    alert.Exec_On_Off = "Update Bus Set Is_Active=0 Where Bus_ID=#Bus";
                }
                else
                {
                    alert.Exec_On_Off = "";
                }
                

                GenericClass.SQLInsertObj(alert);
            }

            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btSaveData_Click(object sender, EventArgs e)
        {
            try
            {
                stepControls = new List<CheckBox>();
                GetControlList<CheckBox>(PanelControls.Controls, stepControls, "Item"); //22

                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                foreach (CheckBox ch in stepControls)
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand(" UPDATE REVISION_CHECK SET REVISION_CHECK.Is_Checked = @is_Checked" +
                        " FROM REVISION_CHECK LEFT JOIN CAT_REVISION_STEP ON REVISION_CHECK.REVISION_STEP_ID = CAT_REVISION_STEP.REVISION_STEP_ID" +
                        " WHERE REVISION_CHECK.LEGAL_REVISION_ID = @revisionID AND CAT_REVISION_STEP.CONTROL_NAME=@controlName", con);
                        try
                        {
                            com.Parameters.AddWithValue("@revisionID", hfLegalRevisionID.Value);
                            com.Parameters.AddWithValue("@controlName", ch.ID.ToString());
                            com.Parameters.AddWithValue("@is_Checked", ch.Checked);
                            com.ExecuteNonQuery();                            
                        }
                        catch (SqlException ex)
                        {
                            functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
                        }
                    }

                }

                functions.ShowMessage(this, this.GetType(), msg_EditRevisions_DataSaved, MessageType.Success);
            }

            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btCancelAlert_Click(object sender, EventArgs e)
        {
            string controlName = ControlNameAlert.Text;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "unCheckControl('"+ controlName + "');", true);
        }

        protected void btUploadImageAfter_Click(object sender, EventArgs e)
        {
            uploadImageFunction("After", FileUpload2);
        }

        protected void btDeleteImageAfter_Click(object sender, EventArgs e)
        {
            int RevisionNoteID = Convert.ToInt32(ViewState["lbRevisionNoteID"]);
            int RevisionCaseID = Convert.ToInt32(ViewState["lbRevisionCaseID"]);
            openImageDialog(RevisionCaseID, RevisionNoteID);
        }

        private void DeleteFileFromFolder(string StrFilename, int revisionNoteID)
        {

            DataTable getRecordPath = GenericClass.GetCustomData("SELECT * FROM [REVISION_NOTE_IMAGE] WHERE " +
            " IMAGE_NAME=" + "'" + StrFilename + "'" + " AND [Revision_Note_ID]=" + revisionNoteID);
            if (getRecordPath.Rows.Count > 0)
            {
                string strFileFullPath = getRecordPath.Rows[0]["Before"].ToString() == "" ? getRecordPath.Rows[0]["After"].ToString() : getRecordPath.Rows[0]["Before"].ToString();

                if (System.IO.File.Exists(strFileFullPath))
                {
                    System.IO.File.Delete(strFileFullPath);
                }
            }

        }


    }

}