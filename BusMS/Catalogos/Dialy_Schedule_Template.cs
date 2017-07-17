using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Dialy_Schedule_Template
    {
        public Dialy_Schedule_Template()
        {

        }

        int dst_id;

        public int DST_ID
        {
            get { return dst_id; }
            set { dst_id = value; }
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


        TimeSpan check_point_time;

        public TimeSpan Check_Point_Time
        {
            get { return check_point_time; }
            set { check_point_time = value; }
        }
        string shift_ID;

        public string Shift_ID
        {
            get { return shift_ID; }
            set { shift_ID = value; }
        }

    }
}