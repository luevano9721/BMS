
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Alert
    {
        public Alert()
        {

        }

        string code;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        int alert_item_id;

        public int Alert_Item_ID
        {
            get { return alert_item_id; }
            set { alert_item_id = value; }
        }

        string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        string action;

        public string Action
        {
            get { return action; }
            set { action = value; }
        }

        int period;

        public int Period
        {
            get { return period; }
            set { period = value; }
        }

        string priority;

        public string Priority
        {
            get { return priority; }
            set { priority = value; }
        }


        DateTime create_date;

        public DateTime Create_Date
        {
            get { return create_date; }
            set { create_date = value; }
        }

        DateTime last_check;

        public DateTime Last_Check
        {
            get { return last_check; }
            set { last_check = value; }
        }

        
        Boolean is_active;

        public Boolean Is_Active
        {
            get { return is_active; }
            set { is_active = value; }
        }

        DateTime off_date;

        public DateTime Off_Date
        {
            get { return off_date; }
            set { off_date = value; }
        }

        string vendor_ID;

        public string Vendor_ID
        {
            get { return vendor_ID; }
            set { vendor_ID = value; }
        }
        string alert_Type;

        public string Alert_Type
        {
            get { return alert_Type; }
            set { alert_Type = value; }
        }

        private string exec_On_Insert;

        public string Exec_On_Insert
        {
            get { return exec_On_Insert; }
            set { exec_On_Insert = value; }
        }
        private string exec_On_Close;

        public string Exec_On_Close
        {
            get { return exec_On_Close; }
            set { exec_On_Close = value; }
        }

        private string exec_On_Off;

        public string Exec_On_Off
        {
            get { return exec_On_Off; }
            set { exec_On_Off = value; }
        }
    }
}