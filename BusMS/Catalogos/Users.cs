using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Users
    {
        public Users()
        {
            this.user_ID = string.Empty;
            this.user_Name = string.Empty;
            this.password = string.Empty;
            this.email = string.Empty;
            this.foxconn_ID = new int();
            this.department = string.Empty;
            this.address = string.Empty;
            this.telephone = string.Empty;
            this.birthdate = new DateTime();
            this.hiredate = new DateTime();
            this.vendor_ID = string.Empty;
            this.expiration_Date = new DateTime();
            this.password_Expired = new Boolean();
            this.is_Active = new Boolean();
            this.is_Block = new Boolean();
            this.reset_Password = new Boolean();
            this.profile = string.Empty;
        }

        public Users(string user_ID,string user_name,string password,string email,int foxconn_id,string department,string address,
            string telephone,DateTime birthdate,DateTime hiredate,string vendor_id,DateTime expiration_date,Boolean password_expired,
            Boolean is_active, Boolean is_block, string profile)
        {
            this.user_ID = user_ID;
            this.user_Name =user_name;
            this.password = password;
            this.email = email;
            this.foxconn_ID = foxconn_id;
            this.department = department;
            this.address = address;
            this.telephone = telephone;
            this.birthdate = birthdate;
            this.hiredate = hiredate;
            this.vendor_ID = vendor_id;
            this.expiration_Date = expiration_date;
            this.password_Expired = password_expired;
            this.is_Active = is_active;
            this.is_Block = is_block;
            this.profile = profile;
        }
        public Users(DataRow dr)
        {
            string[] formats = { "MM/dd/yyyy", "MM/dd/yy" };
            DateTime dtBirtdate = new DateTime();
            DateTime dtHiredate = new DateTime();
            DateTime dtExpiration = new DateTime();
            this.User_ID = dr["User_ID"].ToString();
            this.User_Name = dr["User_Name"].ToString();
            this.Password = dr["Password"].ToString();
            this.Email = dr["Email"].ToString();
            this.Foxconn_ID = Convert.ToInt32(dr["Foxconn_ID"]);
            this.Department = dr["Department"].ToString();
            this.Address = dr["Address"].ToString();
            this.Telephone = dr["Telephone"].ToString();
            this.Profile = dr["Profile"].ToString();
            
            DateTime.TryParseExact(dr["Birthdate(M/D/Y)"].ToString(), formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtBirtdate);
            DateTime.TryParseExact(dr["Hiredate(M/D/Y)"].ToString(), formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtHiredate);
            DateTime.TryParseExact(dr["Expiration_Date(M/D/Y)"].ToString(), formats, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), System.Globalization.DateTimeStyles.None, out dtExpiration);
            //    Convert.ToDateTime(dr["Birthdate(M/D/Y)"]);
            this.Birthdate = dtBirtdate;
           // this.Hiredate = Convert.ToDateTime(dr["Hiredate(M/D/Y)"]);
            this.Hiredate = dtHiredate;
            this.Vendor_ID = dr["Vendor_ID"].ToString();
           // this.Expiration_Date = Convert.ToDateTime(dr["Expiration_Date(M/D/Y)"]);
            this.Expiration_Date = dtExpiration;
            this.Password_Expired = Convert.ToBoolean(dr["Password_Expired"]);
            this.Is_Active = Convert.ToBoolean(dr["Is_Active"]);
            this.Is_Block = Convert.ToBoolean(dr["Is_Block"]);
            this.Rol_ID = dr["Rol_ID"].ToString();
            this.Reset_Password = Convert.ToBoolean(dr["Reset_Password"].ToString());
        }

        string user_ID;

        public string User_ID
        {
            get { return user_ID; }
            set { user_ID = value; }
        }

        string user_Name;

        public string User_Name
        {
            get { return user_Name; }
            set { user_Name = value; }
        }

        string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        int foxconn_ID;

        public int Foxconn_ID
        {
            get { return foxconn_ID; }
            set { foxconn_ID = value; }
        }

        string department;

        public string Department
        {
            get { return department; }
            set { department = value; }
        }

        string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        string telephone;

        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }

        DateTime birthdate;

        public DateTime Birthdate
        {
            get { return birthdate; }
            set { birthdate = value; }
        }

        DateTime hiredate;

        public DateTime Hiredate
        {
            get { return hiredate; }
            set { hiredate = value; }
        }

        string vendor_ID;

        public string Vendor_ID
        {
            get { return vendor_ID; }
            set { vendor_ID = value; }
        }

        DateTime expiration_Date;

        public DateTime Expiration_Date
        {
            get { return expiration_Date; }
            set { expiration_Date = value; }
        }

        Boolean password_Expired;

        public Boolean Password_Expired
        {
            get { return password_Expired; }
            set { password_Expired = value; }
        }

        Boolean is_Active;

        public Boolean Is_Active
        {
            get { return is_Active; }
            set { is_Active = value; }
        }

        Boolean is_Block;

        public Boolean Is_Block
        {
            get { return is_Block; }
            set { is_Block = value; }
        }

        string rol_ID;

        public string Rol_ID
        {
            get { return rol_ID; }
            set { rol_ID = value; }
        }

        Boolean reset_Password;

        public Boolean Reset_Password
        {
            get { return reset_Password; }
            set { reset_Password = value; }
        }

        string profile;

        public string Profile
        {
            get { return profile; }
            set { profile = value; }
        }

    }
}