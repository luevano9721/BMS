using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem.Catalogos
{
    public class Blacklist
    {
        int blackList_ID;

        public int BlackList_ID
        {
            get { return blackList_ID; }
            set { blackList_ID = value; }
        }

        DateTime blackList_Date;

        public DateTime BlackList_Date
        {
            get { return blackList_Date; }
            set { blackList_Date = value; }
        }

        string vendor_ID;

        public string Vendor_ID
        {
            get { return vendor_ID; }
            set { vendor_ID = value; }
        }

        string bus_ID;

        public string Bus_ID
        {
            get { return bus_ID; }
            set { bus_ID = value; }
        }

        string driver_ID;

        public string Driver_ID
        {
            get { return driver_ID; }
            set { driver_ID = value; }
        }

        string reason;

        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }

        string comments;

        public string Comments
        {
            get { return comments; }
            set { comments = value; }
        }
        

        
    }
    
}