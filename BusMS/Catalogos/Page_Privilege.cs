using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Page_Privilege
    {
        public Page_Privilege()
        {
               
        }


        int page_ID;

        public int Page_ID
        {
            get { return page_ID; }
            set { page_ID = value; }
        }

        int privilege_ID;

        public int Privilege_ID
        {
            get { return privilege_ID; }
            set { privilege_ID = value; }
        }

        Boolean is_Active;

        public Boolean Is_Active
        {
            get { return is_Active; }
            set { is_Active = value; }
        }

        string user_ID;

        public string User_ID
        {
            get { return user_ID; }
            set { user_ID = value; }
        }


    }
}