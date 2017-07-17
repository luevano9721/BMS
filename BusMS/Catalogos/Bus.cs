using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
  public  class Bus
    {
        public Bus(/*Vendor*/)
        {

        }

        
        string bus_id;

        public string Bus_ID
        {
            get { return bus_id; }
            set
            {
                bus_id = value;
            }
        }
        string vendor_id;

        public string Vendor_ID
        {
            get { return vendor_id; }
            set { vendor_id = value; }
        }
        string vin;

        public string VIN
        {
            get { return vin; }
            set { vin = value; }
        }
        string capacity;

        public string Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }
        string model;

        public string Model
        {
            get { return model; }
            set { model = value; }
        }
        string make;

        public string Make
        {
            get { return make; }
            set { make = value; }
        }
        string line;

        public string Line
        {
            get { return line; }
            set { line = value; }
        }
        string license_Pl;

        public string License_Pl
        {
            get { return license_Pl; }
            set { license_Pl = value; }
        }
        string gps_id;

        public string GPS_ID
        {
            get { return gps_id; }
            set { gps_id = value;}
        }
        Boolean is_active;

        public Boolean Is_Active
        {
            get { return is_active; }
            set { is_active = value; }
        }
        Boolean is_block;

        public Boolean Is_Block
        {
            get { return is_block; }
            set { is_block = value; }
        }

    }
}