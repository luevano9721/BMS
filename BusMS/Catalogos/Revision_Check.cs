using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem.Catalogos
{
    public class Revision_Check
    {
        private int revision_Check_ID;

        public int Revision_Check_ID
        {
            get { return revision_Check_ID; }
            set { revision_Check_ID = value; }
        }
        private int legal_Revision_ID;

        public int Legal_Revision_ID
        {
            get { return legal_Revision_ID; }
            set { legal_Revision_ID = value; }
        }
        private int revision_Step_ID;

        public int Revision_Step_ID
        {
            get { return revision_Step_ID; }
            set { revision_Step_ID = value; }
        }
        private Boolean is_Checked;

        public Boolean Is_Checked
        {
            get { return is_Checked; }
            set { is_Checked = value; }
        }
        private string revision_Type_ID;

        public string Revision_Type_ID
        {
            get { return revision_Type_ID; }
            set { revision_Type_ID = value; }
        }
    }
}