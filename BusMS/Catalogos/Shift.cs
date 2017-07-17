using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Shift
    {
        private string shift_id;

        public string Shift_ID
        {
            get { return shift_id; }
            set { shift_id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private TimeSpan start_Time;

        public TimeSpan Start_Time
        {
            get { return start_Time; }
            set { start_Time = value; }
        }
        private Boolean special_Request;

        public Boolean Special_Request
        {
            get { return special_Request; }
            set { special_Request = value; }
        }
        private string invoice_GroupName;

        public string Invoice_GroupName
        {
            get { return invoice_GroupName; }
            set { invoice_GroupName = value; }
        }
        private Boolean exit_Shift;

        public Boolean Exit_Shift
        {
            get { return exit_Shift; }
            set { exit_Shift = value; }
        }
        private string special_Service;

        public string Special_Service
        {
            get { return special_Service; }
            set { special_Service = value; }
        }
    }
}