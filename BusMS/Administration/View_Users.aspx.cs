using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusManagementSystem.Class;
using System.Data;
using BusManagementSystem._01Catalogos;
using System.Web.Script.Services;
using System.Web.Services;

namespace BusManagementSystem
{
    public partial class View_Users : System.Web.UI.Page
    {
        Users initialUser = new Users();
        string language = null;
        string msg_ViewUsers_ItemDeleted;
        protected void Page_Init(object sender, EventArgs e)
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["C_USER"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            Users usr = (Users)(Session["C_USER"]);

            if (!usr.Rol_ID.Contains("ADMIN") )
            {
                Response.Redirect("~/MenuPortal.aspx");
            }

            if (!Page.IsPostBack)
            {

                ViewState["searchStatus"] = false;
                ViewState["isSorting"] = false;
            }
            applyLanguage();
            messages_ChangeLanguage();
            if ((bool)ViewState["searchStatus"] == false && (bool)ViewState["isSorting"] == false)
                populateGrid();
        }
        private void applyLanguage()
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
        protected void messages_ChangeLanguage()
        {
            msg_ViewUsers_ItemDeleted = (string)GetGlobalResourceObject(language, "msg_ViewUsers_ItemDeleted");

        }
        public int CheckModify;

        protected void btHelp_Click(object sender, EventArgs e)
        {
            if (Session["USER"] == null)
            {
                Response.Redirect("./Login.aspx");
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BMSHelp('Ayuda - Uso General de BMS')", true);
        }

        private void populateGrid()
        {
            try
            {
                DataTable getData = GenericClass.SQLSelectObj(initialUser);
                gridUsers.DataSource = getData;
                ViewState["backupData"] = getData;
                gridUsers.PageSize = 15;
                gridUsers.DataBind();
                Session["Excel"] = getData;
                Session["name"] = "View_Users";
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            populateGrid();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.Redirect("~/Catalogos/ExportToExcel.aspx?btn=excel", false);
        }

        protected void btEdit_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton lnkRowSelection = (ImageButton)sender;
            //Get the Recipe id from command argumen tof linkbutton
            string UserID = lnkRowSelection.CommandArgument.ToString();

            // pass Recipe idto another page via query string
            Response.Redirect(string.Format("Edit_User.aspx?User_id={0}", UserID), false);
        }

        protected void gridUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Users user = new Users();
                Rol_User rol = new Rol_User();
                Module_User modulo = new Module_User();
                Page_User pageUser = new Page_User();
                Page_Privilege pagePrivilege = new Page_Privilege();
                Label id = (Label)gridUsers.Rows[e.RowIndex].FindControl("User_ID") as Label;
                user.User_ID = id.Text;
                rol.User_ID = id.Text;
                modulo.User_ID = id.Text;
                pageUser.User_ID = id.Text;
                pagePrivilege.User_ID = id.Text;
                GenericClass.SQLDeleteObj(pagePrivilege, "Where User_ID='" + user.User_ID + "'");
                GenericClass.SQLDeleteObj(pageUser, "Where User_ID='" + user.User_ID + "'");
                GenericClass.SQLDeleteObj(modulo, "Where User_ID='" + user.User_ID + "'");
                GenericClass.SQLDeleteObj(rol, "Where User_ID='" + user.User_ID + "'");
                GenericClass.SQLDeleteObj(user, "Where User_ID='" + user.User_ID + "'");

                functions.ShowMessage(this, this.GetType(), msg_ViewUsers_ItemDeleted, MessageType.Success);
                populateGrid();
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void gridUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gridUsers_Sorting(object sender, GridViewSortEventArgs e)
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

                DataTable dt = GenericClass.SQLSelectObj(initialUser);
                DataView sortedView = new DataView(dt);
                sortedView.Sort = e.SortExpression + " " + sortingDirection;
                Session["objects"] = sortedView;
                gridUsers.DataSource = sortedView;
                gridUsers.DataBind();
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);

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


        protected void gridUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gridUsers.PageIndex = e.NewPageIndex;
                if ((bool)ViewState["searchStatus"] == true)
                { gridUsers.DataSource = ViewState["searchResults"]; }
                if ((bool)ViewState["isSorting"] == true)
                { gridUsers.DataSource = Session["Objects"]; }
                gridUsers.DataBind();
            }
            catch (Exception ex)
            {
                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void gridUsers_DataBound(object sender, EventArgs e)
        {

        }

        protected void gridUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Users usr = (Users)(Session["C_USER"]);
                    ImageButton imgEdit = (ImageButton)e.Row.FindControl("btEdit");
                    ImageButton imgDel = (ImageButton)e.Row.FindControl("btDelete");
                    if (imgEdit.CommandArgument == usr.User_ID)
                    { imgDel.Enabled = false; }


                    CheckBox c = (CheckBox)e.Row.FindControl("Is_Block");
                    CheckBox inactive = (CheckBox)e.Row.FindControl("Is_Active");

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
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }
        }

        protected void btSearchCH1_Click(object sender, EventArgs e)
        {
            search();
        }

        private void search()
        {
            try
            {
                DataTable dtNew = new DataTable();
                gridUsers.PageIndex = 0;
                string searchTerm = tbSearchCH1.Text.ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    populateGrid();
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
                        if (row["User_name"].ToString().ToLower().Contains(searchTerm) || row["User_ID"].ToString().ToLower().Contains(searchTerm) || row["Email"].ToString().ToLower().Contains(searchTerm) || row["Foxconn_ID"].ToString().ToLower().Contains(searchTerm))
                        {
                            //when found copy the row to the cloned table
                            dtNew.Rows.Add(row.ItemArray);
                        }
                    }

                    //rebind the grid
                    gridUsers.DataSource = dtNew;
                    ViewState["searchResults"] = dtNew;
                    gridUsers.DataBind();
                }
            }
            catch (Exception ex)
            {
                functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message);

                 functions.InsertError(this.Page.Title, ex.StackTrace, ex.Message); string msg = ex.Message.ToString().Replace("'", "").Replace("\r\n", ""); functions.ShowMessage(this, this.GetType(), msg, MessageType.Error);
            }

        }

    }
}