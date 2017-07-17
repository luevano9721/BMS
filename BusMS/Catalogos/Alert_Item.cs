using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class Alert_Item
    {
        public Alert_Item()
        {

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

    }
}