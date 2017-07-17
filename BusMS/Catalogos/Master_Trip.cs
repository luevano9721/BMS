using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Master_Trip
    {
        public Master_Trip()
        {

        }

        string master_trip_id;

        public string Master_Trip_ID
        {
            get { return master_trip_id; }
            set { master_trip_id = value; }
        }

        string shift_id;

        public string Shift_ID
        {
            get { return shift_id; }
            set { shift_id = value; }
        }

        DateTime log_date;

        public DateTime Log_Date
        {
            get { return log_date; }
            set { log_date = value; }
        }
        string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        string vendor_id;

        public string Vendor_ID
        {
            get { return vendor_id; }
            set { vendor_id = value; }
        }
        Boolean auto_close_trip;

        public Boolean Auto_Close_Trip
        {
            get { return auto_close_trip; }
            set { auto_close_trip = value; }
        }

        Boolean admin_close_trip;

        public Boolean Admin_Close_Trip
        {
            get { return admin_close_trip; }
            set { admin_close_trip = value; }
        }
    }
}