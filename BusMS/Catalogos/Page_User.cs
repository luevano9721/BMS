using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Page_User
    {
        public Page_User()
        {

        }
        int page_User_ID;

        public int Page_User_ID
        {
            get { return page_User_ID; }
            set { page_User_ID = value; }
        }
        int page_ID;

        public int Page_ID
        {
            get { return page_ID; }
            set { page_ID = value; }
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