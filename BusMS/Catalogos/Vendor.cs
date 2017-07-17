using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Vendor
    {
        public Vendor()
        {

        }

        string vendor_id;

        public string Vendor_ID
        {
            get { return vendor_id; }
            set { vendor_id = value; }
        }
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
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

        string rfc;

        public string RFC
        {
            get { return rfc; }
            set { rfc = value; }
        }
        string contact_name;

        public string Contact_Name
        {
            get { return contact_name; }
            set { contact_name = value; }
        }
        string firm_Name;

        public string Firm_Name
        {
            get { return firm_Name; }
            set { firm_Name = value; }
        }

        string invoice_building;

        public string Invoice_building
        {
            get { return invoice_building; }
            set { invoice_building = value; }
        }
    }
}