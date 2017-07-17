using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Fare
    {
        public Fare()
        {

        }

        int fare_id;

        public int Fare_ID
        {
            get { return fare_id; }
            set { fare_id = value; }
        }

        string vendor_id;

        public string Vendor_ID
        {
            get { return vendor_id; }
            set { vendor_id = value; }
        }

        string service_id;

        public string Service_ID
        {
            get { return service_id; }
            set { service_id = value; }
        }

        double unit_cost;

        public double Unit_Cost
        {
            get { return unit_cost; }
            set { unit_cost = value; }
        }
    }
}