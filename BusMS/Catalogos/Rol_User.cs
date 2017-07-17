using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Rol_User
    {
        public Rol_User()
        {

        }
        int rol_User_ID;

        public int Rol_User_ID
        {
            get { return rol_User_ID; }
            set { rol_User_ID = value; }
        }

        string rol_ID;

        public string Rol_ID
        {
            get { return rol_ID; }
            set { rol_ID = value; }
        }

        string user_ID;

        public string User_ID
        {
            get { return user_ID; }
            set { user_ID = value; }
        }

    }
}