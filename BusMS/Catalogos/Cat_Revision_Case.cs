using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem.Catalogos
{
    public class Cat_Revision_Case
    {
        private int revision_Case_ID;

        public int Revision_Case_ID
        {
            get { return revision_Case_ID; }
            set { revision_Case_ID = value; }
        }

        private string revision_Type_ID;

        public string Revision_Type_ID
        {
            get { return revision_Type_ID; }
            set { revision_Type_ID = value; }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private string severity;

        public string Severity
        {
            get { return severity; }
            set { severity = value; }
        }

        private string control_Name;

        public string Control_Name
        {
            get { return control_Name; }
            set { control_Name = value; }
        }
    }
}