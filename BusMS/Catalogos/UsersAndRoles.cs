using BusManagementSystem._01Catalogos;
using BusManagementSystem.Class;
using IBatisNet.DataMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BusManagementSystem.Catalogos
{
    public class UsersAndRoles : Users
    {

        #region Fields
        

        
        public List<Module_User> moduleUserList = new List<Module_User>();
        public List<Page_User> pageUserList = new List<Page_User>();       
        
        /// <summary>
        /// Privilege_ID	Privilege_Name
        //          1	Insert
        //          2	Update
        //          3   Delete
        //          4   Export        
        /// </summary>
        public List<Page_Privilege> pagePrivilegeList = new List<Page_Privilege>();
        
        public List<Pages> pageList = new List<Pages>();
        public List<Module> moduleList = new List<Module>();

        public List<Module_Page> modulePageList = new List<Module_Page>();


        #endregion

        public UsersAndRoles()
            : base()
        {
        }
        public UsersAndRoles(DataTable dtUser,string pwd)
            : base()
        {
            try
            {
                base.User_ID = dtUser.Rows[0]["User_ID"].ToString();
                base.User_Name = dtUser.Rows[0]["User_Name"].ToString();
                base.Rol_ID = dtUser.Rows[0]["Rol_ID"].ToString();
                base.Password = dtUser.Rows[0]["Password"].ToString();
                base.Email = dtUser.Rows[0]["Email"].ToString();
                base.Foxconn_ID = Convert.ToInt32(dtUser.Rows[0]["Foxconn_ID"]);
                base.Department = dtUser.Rows[0]["Department"].ToString();
                base.Address = dtUser.Rows[0]["Address"].ToString();
                base.Telephone = dtUser.Rows[0]["Telephone"].ToString();
                base.Vendor_ID = dtUser.Rows[0]["Vendor_ID"].ToString();
                base.Password_Expired = Convert.ToBoolean(dtUser.Rows[0]["Password_Expired"]);
                base.Is_Active = Convert.ToBoolean(dtUser.Rows[0]["Is_Active"]);
                base.Is_Block = Convert.ToBoolean(dtUser.Rows[0]["Is_Block"]);

                string sPassw;
                sPassw = functions.GetSHA1(pwd);
                base.Password = base.Password.Substring(0, base.Password.Length - 2);

                get_modules_user();
                get_page_user();
                get_page_priviles();
                get_pages();
                get_modules();
                get_modules_pages();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }
        

        #region Methods

        /// <summary>
        /// Get modules
        /// </summary>
        private void get_modules_pages()
        {
            try
            {
                Module_Page initialModulesPages = new Module_Page();
                DataTable dtModulesPage = GenericClass.SQLSelectObj(initialModulesPages);

                foreach (DataRow dr in dtModulesPage.Rows)
                {
                    Module_Page modulePag = new Module_Page();
                    int modulePageID = 0;
                    int moduleID = 0;
                    int pageID = 0;

                    int.TryParse(dr["Module_Page_ID"].ToString(), out modulePageID);
                    int.TryParse(dr["Module_ID"].ToString(), out moduleID);
                    int.TryParse(dr["Page_ID"].ToString(), out pageID);

                    modulePag.Module_Page_ID = modulePageID;
                    modulePag.Module_ID = moduleID;
                    modulePag.Page_ID = pageID;

                    modulePageList.Add(modulePag);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Get modules
        /// </summary>
        private void get_modules()
        {
            try
            {
                Module initialModules = new Module();
                DataTable dtModules = GenericClass.SQLSelectObj(initialModules);

                foreach (DataRow dr in dtModules.Rows)
                {
                    Module module = new Module();
                    int moduleID = 0;
                    int.TryParse(dr["Module_ID"].ToString(), out moduleID);

                    module.Module_ID = moduleID;
                    module.Module_Name = dr["Module_Name"].ToString();
                    module.Module_Name_ESP =  dr["Module_Name_ESP"].ToString();
                    moduleList.Add(module);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Get pages
        /// </summary>
        private void get_pages()
        {
            try
            {
                Pages initialPages = new Pages();                
                DataTable dtPages = GenericClass.SQLSelectObj(initialPages);
                
                foreach (DataRow dr in dtPages.Rows)
                {
                    Pages pag = new Pages();
                    int pageID = 0;
                    int.TryParse(dr["Page_ID"].ToString(), out pageID);

                    pag.Page_ID = pageID;
                    pag.Page_Name = dr["Page_Name"].ToString();
                    pag.Page_Name_ESP = dr["Page_Name_ESP"].ToString();
                    pag.Page_URL = dr["Page_URL"].ToString();

                    pageList.Add(pag);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Get modules by user
        /// </summary>
        private void get_modules_user()
        {
            try
            {
                Module_User initialModuleUser = new Module_User();
                DataTable dtModuleUser = GenericClass.SQLSelectObj(initialModuleUser, WhereClause: "Where User_ID='" + base.User_ID + "'");

                foreach (DataRow dr in dtModuleUser.Rows)
                {
                    Module_User mu = new Module_User();
                    int moduleUserID = 0;
                    int moduleID = 0;                   
                    bool isActive = true;

                    int.TryParse(dr["Module_User_ID"].ToString(), out moduleUserID);
                    int.TryParse(dr["Module_ID"].ToString(), out moduleID);
                    bool.TryParse(dr["Is_Active"].ToString(), out isActive);

                    mu.Module_User_ID = moduleUserID;
                    mu.Module_ID = moduleID;
                    mu.User_ID = dr["User_ID"].ToString();
                    mu.Is_Active = isActive;

                    moduleUserList.Add(mu);                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Get pages by module
        /// </summary>
        private void get_page_user()
        {
            try
            {
                Page_User initialPageUser = new Page_User();
                DataTable dtPageUser = GenericClass.SQLSelectObj(initialPageUser, WhereClause: "Where User_ID='" + base.User_ID + "'");

                foreach (DataRow dr in dtPageUser.Rows)
                {
                    Page_User pu = new Page_User();

                    int pageUserID = 0;
                    int pageID = 0;
                    bool isActive = true;

                    int.TryParse(dr["Page_User_ID"].ToString(), out pageUserID);
                    int.TryParse(dr["Page_ID"].ToString(), out pageID);                   
                    bool.TryParse(dr["Is_Active"].ToString(), out isActive);

                    pu.Page_User_ID = pageUserID;
                    pu.Page_ID = pageID;
                    pu.User_ID = dr["User_ID"].ToString();
                    pu.Is_Active = isActive;

                    pageUserList.Add(pu);
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Privilege_ID	Privilege_Name
        //          1	Insert
        //          2	Update
        //          3   Delete
        //          4   Export
        /// </summary>
        private void get_page_priviles()
        {
            try
            {
                Page_Privilege initialPagePrivilege = new Page_Privilege();
                DataTable dtPagePrivilege = GenericClass.SQLSelectObj(initialPagePrivilege, WhereClause: "Where User_ID='" + base.User_ID + "'");

                foreach (DataRow dr in dtPagePrivilege.Rows)
                {
                    Page_Privilege pp = new Page_Privilege();
                    int pageID = 0;
                    int privilegeID = 0;
                    bool isActive = true;

                    //int.TryParse(dr["Page_Privilege_ID"].ToString(), out pagePrivilegeID);
                    int.TryParse(dr["Page_ID"].ToString(), out pageID);
                    int.TryParse(dr["Privilege_ID"].ToString(), out privilegeID);
                    bool.TryParse(dr["Is_Active"].ToString(), out isActive);

                    pp.Page_ID = pageID;
                    pp.Privilege_ID = privilegeID;
                    pp.Is_Active = isActive;

                    pagePrivilegeList.Add(pp);
                    
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        #endregion
    }
}