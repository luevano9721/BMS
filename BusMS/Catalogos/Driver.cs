using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Driver
    {
        int driverID;

        public Driver()
        {

        }

        public Driver(int driverID, string name, DateTime birthdate, string licenseNo, DateTime licenseExp, string address, string telephone, string vendorID, bool isActive)
        {
            this.driverID = driverID;
            this.name = name;
            this.birthdate = birthdate;
            this.licenseNo = licenseNo;
            this.licenseExp = licenseExp;
            this.address = address;
            this.telephone = telephone;
            this.vendorID = vendorID;
            this.is_active = isActive;
        }

        public int Driver_ID
        {
            get { return driverID; }
            set { driverID = value; }
        }
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        DateTime birthdate;

        public DateTime Birthdate
        {
            get { return birthdate; }
            set { birthdate = value; }
        }

        string licenseNo;

        public string License_No
        {
            get { return licenseNo; }
            set { licenseNo = value; }
        }
        DateTime licenseExp;

        public DateTime License_Exp
        {
            get { return licenseExp; }
            set { licenseExp = value; }
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
        string vendorID;

        public string Vendor_ID
        {
            get { return vendorID; }
            set { vendorID = value; }
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
        DateTime hiring_date;

        public DateTime Hiring_Date
        {
            get { return hiring_date; }
            set { hiring_date = value; }
        }
    }
}