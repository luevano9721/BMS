using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem.Catalogos
{
    public class Alert_Followup
    {
        int followup_id;

        public int Followup_id
        {
            get { return followup_id; }
            set { followup_id = value; }
        }
        int alert_log_id;

        public int Alert_log_id
        {
            get { return alert_log_id; }
            set { alert_log_id = value; }
        }
        DateTime comment_date;

        public DateTime Comment_date
        {
            get { return comment_date; }
            set { comment_date = value; }
        }
        string comment;

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }
        string user_id;

        public string User_id
        {
            get { return user_id; }
            set { user_id = value; }
        }
    }
}