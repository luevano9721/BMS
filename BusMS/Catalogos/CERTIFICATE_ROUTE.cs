using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem.Catalogos
{
    public class CERTIFICATE_ROUTE
    {
        int certify_Route_ID;

        public int Certify_Route_ID
        {
            get { return certify_Route_ID; }
            set { certify_Route_ID = value; }
        }
        int driver_ID;

        public int Driver_ID
        {
            get { return driver_ID; }
            set { driver_ID = value; }
        }
        int route_ID;

        public int Route_ID
        {
            get { return route_ID; }
            set { route_ID = value; }
        }

        DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
    }
}