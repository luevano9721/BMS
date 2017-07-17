using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Trip_Change
    {
        public Trip_Change()
        {

        }

        int trip_change_id;

        public int Trip_Change_ID
        {
            get { return trip_change_id; }
            set { trip_change_id = value; }
        }
        int trip_hrd_id;

        public int Trip_Hrd_ID
        {
            get { return trip_hrd_id; }
            set { trip_hrd_id = value; }
        }
        string vendor_id;

        public string Vendor_ID
        {
            get { return vendor_id; }
            set { vendor_id = value; }
        }
        int prev_driver;

        public int Prev_Driver
        {
            get { return prev_driver; }
            set { prev_driver = value; }
        }
        int new_driver;

        public int New_Driver
        {
            get { return new_driver; }
            set { new_driver = value; }
        }

        string prev_bus;

        public string Prev_Bus
        {
            get { return prev_bus; }
            set { prev_bus = value; }
        }
        string new_bus;

        public string New_Bus
        {
            get { return new_bus; }
            set { new_bus = value; }
        }
        string comments;

        public string Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        string change_Type;

        public string Change_Type
        {
            get { return change_Type; }
            set { change_Type = value; }
        }

        DateTime change_Date;

        public DateTime Change_Date
        {
            get { return change_Date; }
            set { change_Date = value; }
        }
    }
}