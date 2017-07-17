using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusManagementSystem._01Catalogos
{
    public class MappingColumns
    {
        
        public MappingColumns()
        {
            
        }

        string objectClass;

        public string ObjectClass
        {
            get { return objectClass; }
            set { objectClass = value; }
        }
        string replaceType;

        public string ReplaceType
        {
            get { return replaceType; }
            set { replaceType = value; }
        }

        string replaceText;

        public string ReplaceText
        {
            get { return replaceText; }
            set { replaceText = value; }
        }
       
    }
}