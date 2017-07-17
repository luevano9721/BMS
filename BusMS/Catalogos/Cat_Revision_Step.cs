using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem.Catalogos
{
    public class Cat_Revision_Step
    {
        private int revision_Step_ID;

        public int Revision_Step_ID
        {
            get { return revision_Step_ID; }
            set { revision_Step_ID = value; }
        }
        private string revision_Type_ID;

        public string Revision_Type_ID
        {
            get { return revision_Type_ID; }
            set { revision_Type_ID = value; }
        }
        private string control_Name;

        public string Control_Name
        {
            get { return control_Name; }
            set { control_Name = value; }
        }
    }
}