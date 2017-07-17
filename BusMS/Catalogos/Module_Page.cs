using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Module_Page
    {
        public Module_Page()
        {

        }

        int module_Page_ID;

        public int Module_Page_ID
        {
            get { return module_Page_ID; }
            set { module_Page_ID = value; }
        }

        int module_ID;

        public int Module_ID
        {
            get { return module_ID; }
            set { module_ID = value; }
        }

        int page_ID;

        public int Page_ID
        {
            get { return page_ID; }
            set { page_ID = value; }
        }
    }
}