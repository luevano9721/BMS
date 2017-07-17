using BusManagementSystem._01Catalogos;
using BusManagementSystem.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace BusManagementSystem.Class
{
    public class AddPrivilege
    {
        public AddPrivilege()
        {

        }
        public AddPrivilege(string turnOffOnButtons, Button add, Button reset, Button save, Button delete = null, Button excel = null)
        {
            this.add = add;

            this.reset = reset;

            this.save = save;

            this.delete = delete;

            this.excel = excel;

            this.add.Visible = turnOffOnButtons.ToString().Substring(0, 1) == "1";

            this.reset.Visible = turnOffOnButtons.ToString().Substring(1, 1) == "1";

            this.save.Visible = turnOffOnButtons.ToString().Substring(2, 1) == "1";

            if (delete != null)
            {
                this.delete.Visible = turnOffOnButtons.ToString().Substring(3, 1) == "1";
            }

            if (excel != null)
            {
                this.excel.Visible = turnOffOnButtons.ToString().Substring(4, 1) == "1";
            }



        }

        private DataTable dtPrivilege;

        public DataTable DtPrivilege
        {
            get { return dtPrivilege; }
            set { dtPrivilege = value; }
        }

        private Button add;

        public Button Add
        {
            get { return add; }
            set { add = value; }
        }

        private Button reset;

        public Button Reset
        {
            get { return reset; }
            set { reset = value; }
        }

        private Button save;

        public Button Save
        {
            get { return save; }
            set { save = value; }
        }

        private Button delete;

        public Button Delete
        {
            get { return delete; }
            set { delete = value; }
        }

        private Button excel;

        public Button Excel
        {
            get { return excel; }
            set { excel = value; }
        }

        /// <summary>
        /// Get privilege for page according to user
        /// </summary>
        /// <param name="pageTitle">Page name</param>
        /// <param name="usr">user</param>
        /// <returns>Table with query results</returns>
        public DataTable GetPrivilege(string pageTitle, Users usr)
        {
            DataTable dtGetPrigilege = new DataTable();
            try
            {
                dtGetPrigilege = GenericClass.SQLSelectObj(new Page_Privilege(), WhereClause: "Where User_ID='" + usr.User_ID
                    + "' And Page_ID IN (Select Page_ID from Pages Where UPPER(Page_Name) = Upper('" + pageTitle + "') And Is_Active=1)");
            }
            catch (Exception ex)
            {
                functions.InsertError(pageTitle, ex.StackTrace, ex.Message);
            }
            return dtGetPrigilege;
        }

        /// <summary>
        /// Apply privilege on page
        /// </summary>
        /// <param name="turnOffOnButtons">String to determine what button to make visible (insert,reset,save,delete,export)</param>
        /// <param name="grdPage">Grid to apply edit privilege</param>
        public void applyPrivilege(string turnOffOnButtons, GridView grdPage)
        {
            Reset.Visible = turnOffOnButtons.ToString().Substring(1, 1) == "1";

            Save.Visible = turnOffOnButtons.ToString().Substring(2, 1) == "1";

            try
            {
                if (dtPrivilege.Rows.Count > 0)
                {
                    IEnumerable<Int32> insertPrivilege =
                        dtPrivilege
                        .AsEnumerable()
                        .Where(row => row.Field<Int32>("Privilege_ID") == 1)
                        .Select(row => row.Field<Int32>("Privilege_ID"));

                    Add.Visible = insertPrivilege.Count() > 0 ? turnOffOnButtons.ToString().Substring(0, 1) == "1" : false;

                    IEnumerable<Int32> updatePrivilege =
                        dtPrivilege
                        .AsEnumerable()
                        .Where(row => row.Field<Int32>("Privilege_ID") == 2)
                        .Select(row => row.Field<Int32>("Privilege_ID"));

                    grdPage.Columns[0].Visible = updatePrivilege.Count() > 0 ? true : false;

                    if (grdPage.Columns.Count>1)
                    {
                        if (grdPage.Columns[1].HeaderText == string.Empty)
                        {
                            grdPage.Columns[1].Visible = updatePrivilege.Count() > 0 ? true : false;
                        }
                    }
                    

                    IEnumerable<Int32> deletePrivilege =
                        dtPrivilege
                        .AsEnumerable()
                        .Where(row => row.Field<Int32>("Privilege_ID") == 3)
                        .Select(row => row.Field<Int32>("Privilege_ID"));

                    if (Delete != null)
                    {
                        Delete.Visible = deletePrivilege.Count() > 0 ? turnOffOnButtons.ToString().Substring(3, 1) == "1" : false;

                    }

                    IEnumerable<Int32> exportPrivilege =
                        dtPrivilege
                        .AsEnumerable()
                        .Where(row => row.Field<Int32>("Privilege_ID") == 4)
                        .Select(row => row.Field<Int32>("Privilege_ID"));

                    if (Excel != null)
                    {
                        Excel.Visible = exportPrivilege.Count() > 0 ? turnOffOnButtons.ToString().Substring(4, 1) == "1" : false;
                    }



                }
            }
            catch (Exception ex)
            {
                functions.InsertError(grdPage.GetType().Name, ex.StackTrace, ex.Message);
            }
        }

    }
}