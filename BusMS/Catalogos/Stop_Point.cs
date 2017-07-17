using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Stop_Point
    {
        public Stop_Point()
        {

        }

        string stop_id;

        public string Stop_ID
        {
            get { return stop_id; }
            set { stop_id = value; }
        }

        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        string coordinates;

        public string Coordinates
        {
            get { return coordinates; }
            set { coordinates = value; }
        }

        Boolean middle_Point;

        public Boolean Middle_Point
        {
            get { return middle_Point; }
            set { middle_Point = value; }
        }
        Boolean end_Point;

        public Boolean End_Point
        {
            get { return end_Point; }
            set { end_Point = value; }
        }
    }
}