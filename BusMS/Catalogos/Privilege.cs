using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Privilege
    {
        public Privilege()
        {

        }
        int privilege_ID;

        public int Privilege_ID
        {
            get { return privilege_ID; }
            set { privilege_ID = value; }
        }

        string privilege_Name;

        public string Privilege_Name
        {
            get { return privilege_Name; }
            set { privilege_Name = value; }
        }

    }
}