using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 
namespace BusManagementSystem._01Catalogos
{
    public class Activities_Log
    {
        public Activities_Log()
        {

        }

        public Activities_Log(int activity_ID, DateTime activity_Date, string user_ID, string type, string module, string new_Values, string where_Clause)
        {
            this.activity_ID = activity_ID;
            this.activity_Date = activity_Date;
            this.user_ID = user_ID;
            this.type = type;
            this.module = module;
            this.new_Values = new_Values;
            this.where_Clause = where_Clause;
        }

        int activity_ID;

        public int Activity_ID
        {
            get { return activity_ID; }
            set { activity_ID = value; }
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
    }
}