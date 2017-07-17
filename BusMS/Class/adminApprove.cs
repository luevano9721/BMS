using BusManagementSystem._01Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BusManagementSystem.Class
{
    public class adminApprove
    {
        /// <summary>
        /// Compare two objects to find differences and register those changes in the comments
        /// </summary>
        /// <param name="oldObj">Values from database</param>
        /// <param name="newObj">Values from user</param>
        /// <returns>Admin_approve object to be inserted in database</returns>
        public static Admin_Approve compareObjects(object oldObj, object newObj)
        {
            Admin_Approve obj = new Admin_Approve();
            StringBuilder newValuesString = new StringBuilder();
            StringBuilder oldValuesString = new StringBuilder();
            StringBuilder comments = new StringBuilder();
            comments.Append("Se han solicitado cambiar los siguientes valores: <br>");
            Dictionary<string, dynamic> dicoldValues = new Dictionary<string, dynamic>();
            Dictionary<string, dynamic> dicnewValues = new Dictionary<string, dynamic>();
            
            foreach (var property in oldObj.GetType().GetProperties())
            {
                var oldValue = property.GetValue(oldObj);
                if (oldValue == null) { oldValue = 0; }
                var newValue = property.GetValue(newObj);
                if (newValue == null) { newValue = 0; }
                if (!oldValue.Equals(newValue))
                {
                    dicoldValues.Add(property.Name, oldValue);
                    dicnewValues.Add(property.Name, newValue);

                     comments.Append( " [" + property.Name + ": " + "Valor anterior: " + oldValue.ToString() + " Valor nuevo: " + newValue.ToString()+"]<br>");
                }
            }
            if (dicnewValues.Count > 0)
            {
                oldValuesString = GenericClass.formatValues(oldObj, dicoldValues);
                newValuesString = GenericClass.formatValues(newObj, dicnewValues);
            }
            if (string.IsNullOrEmpty(newValuesString.ToString())) { newValuesString.Append("No values changed"); }
            obj.Comments = comments.ToString();
            obj.Old_Values = oldValuesString.ToString();
            obj.New_Values = newValuesString.ToString();
            return obj;
        }

        /// <summary>
        /// Compare two objects to find differences
        /// </summary>
        /// <param name="oldObj">Values from database</param>
        /// <param name="newObj">Values from user</param>
        /// <param name="dif">flag to identify you only want a dictionary with the diferences</param>
        /// <returns>Dictionary with the diferences</returns>
        public static Dictionary<string, dynamic> compareObjects(object oldObj, object newObj,string dif)
        {
            Dictionary<string, dynamic> dicnewValues = new Dictionary<string, dynamic>();

            foreach (var property in oldObj.GetType().GetProperties())
            {
                var oldValue = property.GetValue(oldObj);

                if (oldValue == null) {

                    if (property.PropertyType.Name.ToString() == "String")
                    {
                        oldValue = DBNull.Value;
                    }
                    else
                    {
                        oldValue = 0;
                    }
                    
                }
                var newValue = property.GetValue(newObj);

                if (newValue == null) {

                    if (property.PropertyType.Name.ToString() == "String")
                    {
                        newValue = DBNull.Value;
                    }
                    else
                    {
                        newValue = 0;
                    }
                }
                if (!oldValue.Equals(newValue))
                {

                    dicnewValues.Add(property.Name, newValue);
                }
            }
            return dicnewValues;
        }
    }
}