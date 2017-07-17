using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Service
    {
        public Service()
        {

        }

        string service_id;

        public string Service_ID
        {
            get { return service_id; }
            set { service_id = value; }
        }
        double min_distance;

        public double Min_Distance
        {
            get { return min_distance; }
            set { min_distance = value; }
        }
        double max_distance;

        public double Max_Distance
        {
            get { return max_distance; }
            set { max_distance = value; }
        }
    }
}