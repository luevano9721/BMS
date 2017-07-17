using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Invoice
    {
        public Invoice()
        {

        }

        int invoice_id;

        public int Invoice_ID
        {
            get { return invoice_id; }
            set { invoice_id = value; }
        }
        int trip_hrd_id;

        public int Trip_Hrd_ID
        {
            get { return trip_hrd_id; }
            set { trip_hrd_id = value; }
        }

        double cost;

        public double Cost
        {
            get { return cost; }
            set { cost = value; }
        }
    }
}