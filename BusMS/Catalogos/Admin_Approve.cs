using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Admin_Approve
    {
        public Admin_Approve()
        {
            this.admin_Confirm = false;
            this.confirm_Date = DateTime.MinValue;
            this.admin_user = "";
        }

        public Admin_Approve(int activity_ID, DateTime activity_Date, string user_ID, string type, string module, string old_Values, string new_Values, string where_Clause, string comments)
        {
            this.activity_ID = activity_ID;
            this.activity_Date = activity_Date;
            this.user_ID = user_ID;
            this.type = type;
            this.module = module;
            this.old_Values = old_Values;
            this.new_Values = new_Values;
            this.where_Clause = where_Clause;
            this.admin_Confirm = false;
            this.confirm_Date = DateTime.MinValue;
            this.comments = comments;
            this.admin_user = "";
            this.rollback_action = null;
            this.Rollback_value = null;
        }

        int activity_ID;

        public int Activity_ID
        {
            get { return activity_ID; }
            set { activity_ID = value; }
        }
        Boolean admin_Confirm;

        public Boolean Admin_Confirm
        {
            get { return admin_Confirm; }
            set { admin_Confirm = value; }
        }
        DateTime confirm_Date;

        public DateTime Confirm_Date
        {
            get { return confirm_Date; }
            set { confirm_Date = value; }
        }

        DateTime activity_Date;

        public DateTime Activity_Date
        {
            get { return activity_Date; }
            set { activity_Date = value; }
        }
        string user_ID;

        public string User_ID
        {
            get { return user_ID; }
            set { user_ID = value; }
        }
        string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        string module;

        public string Module
        {
            get { return module; }
            set { module = value; }
        }
        string old_Values;

        public string Old_Values
        {
            get { return old_Values; }
            set { old_Values = value; }
        }

        string new_Values;

        public string New_Values
        {
            get { return new_Values; }
            set { new_Values = value; }
        }
        string where_Clause;

        public string Where_Clause
        {
            get { return where_Clause; }
            set { where_Clause = value; }
        }
        string comments;

        public string Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        string admin_user;

        public string Admin_User
        {
            get { return admin_user; }
            set { admin_user = value; }
        }

        string rollback_action;

        public string Rollback_action
        {
            get { return rollback_action; }
            set { rollback_action = value; }
        }
        string rollback_value;

        public string Rollback_value
        {
            get { return rollback_value; }
            set { rollback_value = value; }
        }



    }
}