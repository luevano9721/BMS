using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Global_Configuration
    {
        int configuration_ID;

        public int Configuration_ID
        {
            get { return configuration_ID; }
            set { configuration_ID = value; }
        }
        string configuration_Name;

        public string Configuration_Name
        {
            get { return configuration_Name; }
            set { configuration_Name = value; }
        }

        string configuration_Value;

        public string Configuration_Value
        {
            get { return configuration_Value; }
            set { configuration_Value = value; }
        }

        string configuration_Category;

        public string Configuration_Category
        {
            get { return configuration_Category; }
            set { configuration_Category = value; }
        }

        string user_ID;

        public string User_ID
        {
            get { return user_ID; }
            set { user_ID = value; }
        }
        DateTime last_Change;

        public DateTime Last_Change
        {
            get { return last_Change; }
            set { last_Change = value; }
        }
        string vendor_ID;

        public string Vendor_ID
        {
            get { return vendor_ID; }
            set { vendor_ID = value; }
        }
    }
}