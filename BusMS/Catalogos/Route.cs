using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Route
    {
        public Route()
        {

        }

        int route_id;

        public int Route_ID
        {
            get { return route_id; }
            set { route_id = value; }
        }
        string org_id;

        public string Org_ID
        {
            get { return org_id; }
            set { org_id = value; }
        }

        string dest_id;

        public string Dest_ID
        {
            get { return dest_id; }
            set { dest_id = value; }
        }

        TimeSpan travel_time_est;

        public TimeSpan Travel_Time_Est
        {
            get { return travel_time_est; }
            set { travel_time_est = value; }
        }

        string service_id;

        public string Service_ID
        {
            get { return service_id; }
            set { service_id = value; }
        }

        int zone;

        public int Zone
        {
            get { return zone; }
            set { zone = value; }
        }

    }
}