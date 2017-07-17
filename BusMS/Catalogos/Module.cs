using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Module
    {
        public Module()
        {

        }
        int module_ID;

        public int Module_ID
        {
            get { return module_ID; }
            set { module_ID = value; }
        }

        string module_Name;

        public string Module_Name
        {
            get { return module_Name; }
            set { module_Name = value; }
        }

        string module_Name_ESP;

        public string Module_Name_ESP
        {
            get { return module_Name_ESP; }
            set { module_Name_ESP = value; }
        }

    }
}