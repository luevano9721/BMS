using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem.Catalogos
{
    public class System_Error_Log
    {
        int log_ID;

        public int Log_ID
        {
            get { return log_ID; }
            set { log_ID = value; }
        }
        string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        string module;

        public string Module
        {
            get { return module; }
            set { module = value; }
        }
        string statement;

        public string Statement
        {
            get { return statement; }
            set { statement = value; }
        }



    }
}