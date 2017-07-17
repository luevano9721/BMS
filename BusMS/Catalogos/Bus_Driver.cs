using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Bus_Driver
    {
        public Bus_Driver()
        {

        }

        string bus_driver_id;

        public string Bus_Driver_ID
        {
            get { return bus_driver_id; }
            set { bus_driver_id = value; }
        }

        string vendor_id;

        public string Vendor_ID
        {
            get { return vendor_id; }
            set { vendor_id = value; }
        }

        string bus_id;

        public string Bus_ID
        {
            get { return bus_id; }
            set { bus_id = value; }
        }

        int driver_id;

        public int Driver_ID
        {
            get { return driver_id; }
            set { driver_id = value; }
        }

        Boolean is_active;

        public Boolean Is_Active
        {
            get { return is_active; }
            set { is_active = value; }
        }

        string shift_id;

        public string Shift_id
        {
            get { return shift_id; }
            set { shift_id = value; }
        }
    }
}