using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Pages
    {
        public Pages()
        {

        }

        int page_ID;

        public int Page_ID
        {
            get { return page_ID; }
            set { page_ID = value; }
        }

        string page_Name;

        public string Page_Name
        {
            get { return page_Name; }
            set { page_Name = value; }
        }

        string page_URL;

        public string Page_URL
        {
            get { return page_URL; }
            set { page_URL = value; }
        }

        string page_Name_ESP;

        public string Page_Name_ESP
        {
            get { return page_Name_ESP; }
            set { page_Name_ESP = value; }
        }
    }
}