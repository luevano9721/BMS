using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Module_User
    {
        public Module_User()
        {

        }

        int module_User_ID;

        public int Module_User_ID
        {
            get { return module_User_ID; }
            set { module_User_ID = value; }
        }

        int module_ID;

        public int Module_ID
        {
            get { return module_ID; }
            set { module_ID = value; }
        }

        string user_ID;

        public string User_ID
        {
            get { return user_ID; }
            set { user_ID = value; }
        }

        Boolean is_Active;

        public Boolean Is_Active
        {
            get { return is_Active; }
            set { is_Active = value; }
        }


    }
}