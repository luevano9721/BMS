using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem.Catalogos
{
    public class Revision
    {
        int revision_Type_ID;

        public int Revision_Type_ID
        {
            get { return revision_Type_ID; }
            set { revision_Type_ID = value; }
        }
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string category;

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

    }
}