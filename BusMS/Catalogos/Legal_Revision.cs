using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem.Catalogos
{
    public class Legal_Revision
    {
        private int legal_Revision_ID;

        public int Legal_Revision_ID
        {
            get { return legal_Revision_ID; }
            set { legal_Revision_ID = value; }
        }
        private DateTime revision_Date;

        public DateTime Revision_Date
        {
            get { return revision_Date; }
            set { revision_Date = value; }
        }

        private string vendor_Id;

        public string Vendor_ID
        {
            get { return vendor_Id; }
            set { vendor_Id = value; }
        }
        private int route_Id;

        public int Route_ID
        {
            get { return route_Id; }
            set { route_Id = value; }
        }
        private string bus_Driver_ID;

        public string Bus_Driver_ID
        {
            get { return bus_Driver_ID; }
            set { bus_Driver_ID = value; }
        }
        private string shift_Id;

        public string Shift_ID
        {
            get { return shift_Id; }
            set { shift_Id = value; }
        }
        private Boolean travel_Map;

        public Boolean Travel_Map
        {
            get { return travel_Map; }
            set { travel_Map = value; }
        }
        private Boolean communication_Radio;

        public Boolean Communication_Radio
        {
            get { return communication_Radio; }
            set { communication_Radio = value; }
        }

        private Boolean Gps;

        public Boolean GPS
        {
            get { return Gps; }
            set { Gps = value; }
        }
        private Boolean pci;

        public Boolean PCI
        {
            get { return pci; }
            set { pci = value; }
        }
        private Boolean lintern;

        public Boolean Lintern
        {
            get { return lintern; }
            set { lintern = value; }
        }
        private Boolean security_Reflectors;

        public Boolean Security_Reflectors
        {
            get { return security_Reflectors; }
            set { security_Reflectors = value; }
        }
        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        }
}