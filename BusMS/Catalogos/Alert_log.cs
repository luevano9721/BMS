using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Alert_Log
    {
        public Alert_Log()
        {

        }

        int alert_log_id;

        public int Alert_log_ID
        {
            get { return alert_log_id; }
            set { alert_log_id = value; }
        }

        string code;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        DateTime alert_date;

        public DateTime Alert_Date
        {
            get { return alert_date; }
            set { alert_date = value; }
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

        string rissed_by;

        public string Rissed_By
        {
            get { return rissed_by; }
            set { rissed_by = value; }
        }
        string comments;

        public string Comments
        {
            get { return comments; }
            set { comments = value; }
        }
        private string priority;

        public string Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        private string closed_By;

        public string Closed_By
        {
            get { return closed_By; }
            set { closed_By = value; }
        }
        private DateTime last_Check;

        public DateTime Last_Check
        {
            get { return last_Check; }
            set { last_Check = value; }
        }
        private DateTime ticket_Close;

        public DateTime Ticket_Close
        {
            get { return ticket_Close; }
            set { ticket_Close = value; }
        }
        private string vendor_ID;

        public string Vendor_ID
        {
            get { return vendor_ID; }
            set { vendor_ID = value; }
        }
        private DateTime off_Date;

        public DateTime Off_Date
        {
            get { return off_Date; }
            set { off_Date = value; }
        }
        private string exec_On_Off;

        public string Exec_On_Off
        {
            get { return exec_On_Off; }
            set { exec_On_Off = value; }
        }
    }
}