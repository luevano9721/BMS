using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Trip_Hrd
    {
        public Trip_Hrd()
        {

        }

        int trip_hrd_id;

        public int Trip_Hrd_ID
        {
            get { return trip_hrd_id; }
            set { trip_hrd_id = value; }
        }
        string bus_driver_id;

        public string Bus_Driver_ID
        {
            get { return bus_driver_id; }
            set { bus_driver_id = value; }
        }

        int route_id;

        public int Route_ID
        {
            get { return route_id; }
            set { route_id = value; }
        }


        DateTime trip_date;

        public DateTime Trip_date
        {
            get { return trip_date; }
            set { trip_date = value; }
        }

        string comment;

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }
        string master_trip_id;

        public string Master_Trip_ID
        {
            get { return master_trip_id; }
            set { master_trip_id = value; }
        }
       

        TimeSpan check_point_time;

        public TimeSpan Check_Point_Time
        {
            get { return check_point_time; }
            set { check_point_time = value; }
        }


    }
}