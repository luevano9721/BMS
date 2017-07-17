using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Data.SqlClient;
using BusManagementSystem._01Catalogos;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using BusManagementSystem.Class;
using BusManagementSystem.Catalogos;


namespace BusManagementSystem.Class
{


    public class GenericClass : System.Web.UI.Page
    {
        static string pattern = "[~#%&*{}?'\"]";
        static string valueToReplace = "";
        static Regex regEx = new Regex(pattern);


        /// <summary>
        /// formating values acording to its type 
        /// </summary>
        /// <param name="type">type of variable</param>
        /// <param name="prefixName">Table</param>
        /// <param name="Name">variable name</param>
        /// <returns> string formatted</returns>
        public static string caseFormat(string type, string prefixName, string Name)
        {
            switch (type)
            {
                case "DateTime":
                    return " convert(varchar(15)," + prefixName + Name + ",101) as '" + Name + "(M/D/Y)',";
                case "TimeSpan":
                    return " convert(varchar(5)," + prefixName + Name + ", 108) as " + Name + ",";
                default:
                    return prefixName + Name + " as " + Name + ",";
            }
        }
        /// <summary>
        /// Formating object properties to comment the action pendding of approval
        /// </summary>
        /// <param name="obj">class which properties are going to be formatted</param>
        /// <returns>insert statement to be executed </returns>
        static public string formatValues(Object obj)
        {

            StringBuilder Result = new StringBuilder();
            PropertyInfo[] ObjProp = obj.GetType().GetProperties();
            dynamic[] fields = obj.GetType()
            .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Select(f => f.GetValue(obj))
            .ToArray();

            for (int i = 0; i < fields.Length; i++)
            {


                if (ObjProp[i].Name.Contains("_ID") && fields[i].ToString() == "0")
                {
                    continue;
                }
                else
                {
                    if (ObjProp[i].PropertyType.Name.ToString() == "Boolean")
                    {
                        Result.Append(", ");
                        Result.Append(fields[i].ToString().ToUpper() == "TRUE" ? "1" : "0");
                    }

                    else
                    {
                        if (ObjProp[i].PropertyType.Name.ToString() == "Int32")
                        {
                            Result.Append(", " + fields[i]);
                        }
                        else
                        {

                            if (ObjProp[i].PropertyType.Name.ToString() == "DateTime")
                            {
                                Result.Append(", cast('" +
                                    Regex.Replace(regEx.Replace(fields[i].ToString(), valueToReplace), pattern, "") + "' as DateTime) ");
                            }
                            else
                            {
                                if (ObjProp[i].PropertyType.Name.ToString() == "TimeSpan")
                                {
                                    Result.Append(", cast('" +
                                        Regex.Replace(regEx.Replace(fields[i].ToString(), valueToReplace), pattern, "") + "' as time(5)) ");
                                }
                                else
                                {
                                    Result.Append(", '" + 
                                        Regex.Replace(regEx.Replace(fields[i].ToString(), valueToReplace), pattern, "") + "'");
                                }
                            }
                        }
                    }
                }
            }
            if (Result.Length <= 0)
            {
                throw new Exception("Property not found while giving format - Select Statement");
            }

            return Result.Remove(0, 1).ToString();
        }

        /// <summary>
        /// Retrive String for the update statement
        /// </summary>
        /// <param name="obj">class object with the values to update</param>
        /// <param name="updateDictionary">Name and value of the fields to update, if null it update whole table</param>
        /// <returns></returns>
        static public StringBuilder formatValues(Object obj, Dictionary<String, dynamic> updateDictionary = null)
        {
            PropertyInfo[] ObjProp = obj.GetType().GetProperties();
            StringBuilder SelectObjProp = new StringBuilder();
            dynamic findValue;
            if (updateDictionary != null)
            {
                foreach (var item in ObjProp)
                {
                    if (updateDictionary.TryGetValue(item.Name.ToString(), out findValue))
                    {
                        Type typeValue = findValue.GetType();
                        string valueInArray = "";
                        if (typeValue.IsArray)
                        {
                            foreach (var valueArray in findValue)
                            {
                                valueInArray += valueArray;
                            }
                            findValue = valueInArray;
                        }
                        if (item.PropertyType.Name == "DateTime")
                        {
                            SelectObjProp.Append(item.Name + "= cast('" +
                                Regex.Replace(regEx.Replace(findValue.ToString(), valueToReplace), pattern, "") + "' as datetime),");
                        }
                        else
                        {
                            if (item.PropertyType.Name == "Boolean")
                            {
                                SelectObjProp.Append(item.Name + "= ");
                                SelectObjProp.Append(findValue.ToString().ToUpper() == "TRUE" ? "1" : "0");
                                SelectObjProp.Append(",");
                            }
                            else
                            {
                                if (item.PropertyType.Name == "Int32")
                                { SelectObjProp.Append(item.Name + "= " + findValue + ","); }
                                else
                                {
                                    if (item.PropertyType.Name.ToString() == "TimeSpan")
                                    {
                                        SelectObjProp.Append(item.Name + "= cast('" + 
                                            Regex.Replace(regEx.Replace(findValue.ToString(), valueToReplace), pattern, "") + "' as time(5)),");
                                    }
                                    else
                                        SelectObjProp.Append(item.Name + "= '" + 
                                            Regex.Replace(regEx.Replace(findValue.ToString(), valueToReplace), pattern, "") + "',");
                                }
                            }
                        }

                    }
                }
            }
            else
            {
                foreach (var item in ObjProp)
                {

                    if (item.PropertyType.Name == "DateTime")
                        SelectObjProp.Append(item.Name + "= cast('" + 
                            Regex.Replace(regEx.Replace(item.GetValue(obj).ToString(), valueToReplace), pattern, "") + "' as datetime),");
                    else
                    {
                        if (item.PropertyType.Name == "Boolean")
                        {
                            SelectObjProp.Append(item.Name + "= ");
                            SelectObjProp.Append(item.GetValue(obj).ToString().ToUpper() == "TRUE" ? "1" : "0");
                            SelectObjProp.Append(",");
                        }
                        else
                        {
                            if (item.PropertyType.Name == "Int32")
                            { SelectObjProp.Append(item.Name + "= " + item.GetValue(obj) + ","); }
                            else
                            {
                                if (item.PropertyType.Name.ToString() == "TimeSpan")
                                {
                                    SelectObjProp.Append(item.Name + "= cast('" + 
                                        Regex.Replace(regEx.Replace(item.GetValue(obj).ToString(), valueToReplace), pattern, "") + "' as time(8)), ");
                                }
                                else
                                    SelectObjProp.Append(item.Name + "= '" +
                                        Regex.Replace(regEx.Replace(item.GetValue(obj).ToString(), valueToReplace), pattern, "") + "',");
                            }
                        }

                    }
                }

            }
            if (SelectObjProp.Length <= 0)
            {
                throw new Exception("SYS: Property not found while giving format - Update Statement");
            }
            return SelectObjProp.Remove(SelectObjProp.Length - 1, 1);
        }

        /// <summary>
        /// create string with columns to select from a query
        /// </summary>
        /// <param name="obj">Class</param>
        /// <param name="list">Custom columns to select</param>
        /// <returns>String with columns formated</returns>
        static public StringBuilder formatProperty(Object obj, List<string> list = null, string queryName = "Catalog")
        {


            PropertyInfo[] ObjProp = obj.GetType().GetProperties();

            DataTable TableResult = obj.GetType().Name != "MappingColumns" ? SQLSelectObj(new MappingColumns(), 
                WhereClause: "Where ObjectClass='" + obj.GetType().Name + "' And Upper(ReplaceType)<> 'JOIN'" + " And QueryName='" + queryName + "'") : null;

            StringBuilder SelectObjProp = new StringBuilder();
            String prefixName = obj.GetType().Name + ".";

            foreach (var item in ObjProp)
            {
                DataRow[] columnInMapp = TableResult == null ? null : TableResult.Select("ReplaceType='" + item.Name + "'");

                if (list != null)
                {
                    //dynamic columnInList = list.Select(column => item.Name == column);
                    string[] columnInList = list.Where(column => item.Name == column).ToArray();
                    if (columnInList.Length > 0)
                    {

                        if (columnInMapp != null)
                        {

                            if (columnInMapp.Length > 0)
                                SelectObjProp.Append(columnInMapp[0][2].ToString() + ",");
                            else
                            {
                                SelectObjProp.Append(caseFormat(item.PropertyType.Name, prefixName, item.Name));

                            }
                        }

                        else
                        {
                            SelectObjProp.Append(caseFormat(item.PropertyType.Name, prefixName, item.Name));

                        }
                    }
                }
                else
                {
                    if (columnInMapp != null)
                    {
                        if (columnInMapp.Length > 0)
                            SelectObjProp.Append(columnInMapp[0][2].ToString() + ",");
                        else
                        {
                            SelectObjProp.Append(caseFormat(item.PropertyType.Name, prefixName, item.Name));

                        }
                    }

                    else
                    {
                        SelectObjProp.Append(caseFormat(item.PropertyType.Name, prefixName, item.Name));
                    }

                }
            }
            if (SelectObjProp.Length <= 0)
            {
                throw new Exception("Property not found while giving format - Datatable Process");
            }
            return SelectObjProp.Remove(SelectObjProp.Length - 1, 1);

        }

        /// <summary>
        /// Create from clause according to table MappingColumns in database
        /// </summary>
        /// <param name="obj">Class to work with</param>
        /// <param name="queryName">Name of the query in table MappingColumns</param>
        /// <returns>From clause formated</returns>
        static public String formatFromClause(Object obj, string queryName = "Catalog")
        {
            DataTable tableResult = new DataTable();
            String fromClause = obj.GetType().Name;
            if (fromClause != "MappingColumns")
            {
                tableResult = SQLSelectObj(new MappingColumns(), WhereClause: "Where ObjectClass='" + fromClause + 
                    "' And Upper(ReplaceType)= 'JOIN'" + " And QueryName='" + queryName + "'");
                foreach (DataRow rowItem in tableResult.Rows)
                {

                    fromClause += " " + rowItem[2].ToString();
                }
            }

            return fromClause;
        }

        /// <summary>
        ///  Create Datatable according to class received    
        /// </summary>
        /// <param name="obj"> class made base on database unit</param>
        /// <returns>Datatable with or without rows</returns>
        /// 
        static public DataTable CreateEmptyDataTable(Object obj)
        {
            DataTable dt = new DataTable();

            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                dt.Columns.Add(new DataColumn(info.Name, info.PropertyType));
            }

            dynamic[] fields = obj.GetType()
            .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Select(f => f.GetValue(obj))
            .ToArray();

            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i] != null)
                {
                    if (fields[i].ToString() == DateTime.MinValue.ToString())
                    {
                        fields[i] = DBNull.Value;
                    }
                    if (fields[i].GetType().Name == "String")
                    {
                        if (!(obj.GetType().Name == "Admin_Approve" | fields[i].Contains("=")))
                        {
                            fields[i] = Regex.Replace(regEx.Replace(fields[i].ToString(), valueToReplace), pattern, "");
                        }
                    } 
                }
            }

            if (fields.Length > 0)
            {
                
                dt.Rows.Add(fields);
            }

            return dt;
        }

        /// <summary>
        /// Call Procedure to build select statement and return query result
        /// </summary>
        /// <param name="obj">Class</param>
        /// <param name="customSelect">List of columns you want to select</param>
        /// <param name="WhereClause">Custom Where Clause</param>
        /// <param name="GroupByClause">Custom Groupby Clause</param>
        /// <param name="OrderByClause">Custom Orderby Clause</param>
        /// <returns>DataTable</returns>
        static public DataTable SQLSelectObj(Object obj, List<string> customSelect = null, string WhereClause = " ", string GroupByClause = " ", string OrderByClause = " ", string mappingQueryName = null)
        {
            DataTable TableResult = new DataTable();

            Connection conex = new Connection();

            Users usr = new Users();

            using (var connection = conex.connect())
            {
                if (HttpContext.Current.Session["C_USER"] != null && HttpContext.Current.Session["C_USER"].ToString()!="") 
                {
                    usr = (Users)HttpContext.Current.Session["C_USER"];
                }
                

                using (var command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    
                    command.CommandText = "BMS_DYNAMIC_SELECT";

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@List", formatProperty(obj, customSelect, queryName: mappingQueryName == null
                        ? null : mappingQueryName).ToString());

                    command.Parameters.AddWithValue("@From", formatFromClause(obj, mappingQueryName == null ? null : mappingQueryName).ToString());

                    command.Parameters.AddWithValue("@WhereClause", WhereClause);

                    command.Parameters.AddWithValue("@GroupByClause", GroupByClause);

                    command.Parameters.AddWithValue("@OrderBYClause", OrderByClause);

                    try
                    {
                        TableResult.Load(command.ExecuteReader(), LoadOption.OverwriteChanges);
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {

                        ManageSQLException(ex, usr);


                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        command.Dispose();

                        connection.Close();

                        connection.Dispose();
                    }
                }
            }

            return TableResult;
        }

        /// <summary>
        /// Insert all columns from a class to database table
        /// </summary>
        /// <param name="obj">class</param>
        static public void SQLInsertObj(Object obj)
        {

            DataTable InsertRows = CreateEmptyDataTable(obj);

            Connection PrincipalConn = new Connection();

            SqlConnection cnn = PrincipalConn.connect();

            SqlTransaction transaction = cnn.BeginTransaction();

            Users usr = new Users();

            using (SqlBulkCopy sbc = new SqlBulkCopy(cnn, SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.CheckConstraints, transaction))
            {
                if (HttpContext.Current.Session["C_USER"] != null && HttpContext.Current.Session["C_USER"].ToString()  != "")
                {
                    usr = (Users)HttpContext.Current.Session["C_USER"];
                }

                try
                {
                    foreach (DataColumn col in InsertRows.Columns)
                    {
                        var Map = new SqlBulkCopyColumnMapping(col.ColumnName, col.ColumnName);
                    }

                    sbc.BulkCopyTimeout = 600;

                    sbc.DestinationTableName = obj.GetType().Name;

                    sbc.WriteToServer(InsertRows);

                    transaction.Commit();

                    if (sbc.DestinationTableName != "Activities_Log" & sbc.DestinationTableName != "Admin_Approve")
                    {
                        
                        Dictionary<string, dynamic> dicFormat = new Dictionary<string, dynamic>();

                        for (int i = 0; i < InsertRows.Columns.Count; i++)

                            dicFormat.Add(InsertRows.Columns[i].ColumnName, InsertRows.Rows.Cast<DataRow>().Select(k => k[InsertRows.Columns[i]]).ToArray());

                        GenericClass.SQLInsertObj(new Activities_Log(0, DateTime.Now, usr.User_ID.ToString(), "INSERT", 
                            obj.GetType().Name, formatValues(obj, dicFormat).ToString(), ""));

                    }
                }
                catch (System.Data.SqlClient.SqlException ex)
                {

                    ManageSQLException(ex, usr);


                }
                catch (Exception e)
                {

                    throw e;

                }
                finally
                {
                    sbc.Close();

                    transaction.Dispose();

                    PrincipalConn.connect().Close();
                }
            }

        }


        /// <summary>
        /// Sql query to insert table
        /// </summary>
        /// <param name="className">Class Name</param>
        /// <param name="adminList">list of values to be inserted</param>
        /// <param name="header">Property info of the class</param>
        /// <returns>Datatable with result</returns>
        static public DataTable SQLInsertObj(string className, string adminList, PropertyInfo[] header)
        {
            DataTable TableResult = new DataTable();

            Connection conex = new Connection();

            string[] splitResult = adminList.Split(',');

            string resultList = "";

            int j = 0;

            for (int i = 0; i < header.Length; i++)
            {
                if (header[i].Name.Contains("_ID") && splitResult.Length < header.Length && i == 0)
                {
                    continue;
                }
                resultList += header[i].Name + "=" + splitResult[j] + ", ";
                j++;
            }

            Users usr = new Users();

            using (var connection = conex.connect())
            {
                if (HttpContext.Current.Session["C_USER"] != null && HttpContext.Current.Session["C_USER"].ToString() != "")
                {
                    usr = (Users)HttpContext.Current.Session["C_USER"];
                }

                using (var command = connection.CreateCommand())
                {
                    command.Connection = connection;

                    command.CommandText = "BMS_DYNAMIC_INSERT";

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@List", adminList);

                    command.Parameters.AddWithValue("@TableName", className);

                    try
                    {
                        TableResult.Load(command.ExecuteReader(), LoadOption.OverwriteChanges);

                        if (className != "Activities_Log" & className != "Admin_Approve")
                        {
                           
                            GenericClass.SQLInsertObj(new Activities_Log(0, DateTime.Now, usr.User_ID.ToString(), "INSERT",
                                className, resultList, ""));

                        }
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {

                        ManageSQLException(ex, usr);


                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        command.Dispose();

                        connection.Close();

                        connection.Dispose();
                    }
                }
            }

            return TableResult;
        }

        /// <summary>
        /// Sql query to update table
        /// </summary>
        /// <param name="obj">Class with table columns as properties</param>
        /// <param name="customUpdateProperties">Property and value to be update</param>
        /// <param name="WhereClause">Where clause to filter update</param>
        /// <returns>Datatable with result</returns>
        static public DataTable SQLUpdateObj(Object obj, Dictionary<String, dynamic> customUpdateProperties = null, string WhereClause = " ", string adminList = "")
        {
            DataTable TableResult = new DataTable();

            Connection conex = new Connection();

            Users usr = new Users();

            string formatList = "";

            if (string.IsNullOrWhiteSpace(adminList))
            {
                formatList = formatValues(obj, customUpdateProperties == null ? null : customUpdateProperties).ToString();
            }

            using (var connection = conex.connect())
            {
                if (HttpContext.Current.Session["C_USER"] != null && HttpContext.Current.Session["C_USER"].ToString() != "")
                {
                    usr = (Users)HttpContext.Current.Session["C_USER"];
                }

                using (var command = connection.CreateCommand())
                {
                    command.Connection = connection;

                    command.CommandText = "BMS_DYNAMIC_UPDATE";

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@List", string.IsNullOrWhiteSpace(adminList) ? formatList : adminList);

                    command.Parameters.AddWithValue("@TableName", obj.GetType().Name);

                    command.Parameters.AddWithValue("@WhereClause", WhereClause);

                    try
                    {
                        TableResult.Load(command.ExecuteReader(), LoadOption.OverwriteChanges);

                        if (obj.GetType().Name != "Activities_Log")
                        {
                           
                            GenericClass.SQLInsertObj(new Activities_Log(0, DateTime.Now, usr.User_ID.ToString(), "UPDATE",
                                obj.GetType().Name, string.IsNullOrWhiteSpace(adminList) ? formatList : adminList, WhereClause));

                        }
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {

                        ManageSQLException(ex, usr);


                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        command.Dispose();

                        connection.Close();

                        connection.Dispose();
                    }
                }
            }

            return TableResult;
        }

        /// <summary>
        /// Sql query to delete table
        /// </summary>
        /// <param name="obj">Class with table columns as properties</param>
        /// <param name="WhereClause">Where clause to determine what to delete</param>
        /// <returns>Datatable with result</returns>
        static public DataTable SQLDeleteObj(Object obj, string WhereClause)
        {
            DataTable TableResult = new DataTable();
            
            Connection conex = new Connection();


            Users usr = new Users();

            using (var connection = conex.connect())
            {
                if (HttpContext.Current.Session["C_USER"] != null && HttpContext.Current.Session["C_USER"].ToString() != "")
                {
                    usr = (Users)HttpContext.Current.Session["C_USER"];
                }

                using (var command = connection.CreateCommand())
                {
                    command.Connection = connection;

                    command.CommandText = "BMS_DYNAMIC_DELETE";

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TableName", obj.GetType().Name);

                    command.Parameters.AddWithValue("@WhereClause", WhereClause);


                    try
                    {
                        TableResult.Load(command.ExecuteReader(), LoadOption.OverwriteChanges);

                        if (obj.GetType().Name != "Activities_Log")
                        {
                            GenericClass.SQLInsertObj(new Activities_Log(0, DateTime.Now, usr.User_ID.ToString(), "DELETE", 
                                obj.GetType().Name, "", WhereClause));

                        }
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {

                        ManageSQLException(ex, usr);


                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        command.Dispose();
                     
                        connection.Close();
                        
                        connection.Dispose();
                    }
                }
            }

            return TableResult;
        }


        /// <summary>
        /// Insert values into alert_Log table
        /// </summary>
        /// <param name="alertLog">Object with values to insert</param>
        /// <returns></returns>
        static public DataTable InsertToAlertLog(object alertLog)
        {
            DataTable TableResult = new DataTable();
            
            Connection conex = new Connection();

            Users usr = new Users();
           
            using (var connection = conex.connect())
            {
                if (HttpContext.Current.Session["C_USER"] != null && HttpContext.Current.Session["C_USER"].ToString() != "")
                {
                    usr = (Users)HttpContext.Current.Session["C_USER"];
                }

                using (var command = connection.CreateCommand())
                {
                    command.Connection = connection;

                    command.CommandText = "BMS_ALERTLOG_INSERT";

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AlertLog", ((Alert_Log)alertLog).Alert_log_ID);

                    command.Parameters.AddWithValue("@Code", ((Alert_Log)alertLog).Code.ToString().Trim());
                    
                    command.Parameters.AddWithValue("@Off_Date", ((Alert_Log)alertLog).Off_Date);
                    
                    command.Parameters.AddWithValue("@Bus_ID", ((Alert_Log)alertLog).Bus_ID.ToString().Trim());
                    
                    command.Parameters.AddWithValue("@Rissed_By", ((Alert_Log)alertLog).Rissed_By.ToString().Trim());
                    
                    command.Parameters.AddWithValue("@Comments", ((Alert_Log)alertLog).Comments.ToString().Trim());
                    
                    command.Parameters.AddWithValue("@Priority", ((Alert_Log)alertLog).Priority.ToString().Trim());
                    
                    command.Parameters.AddWithValue("@Vendor_ID", ((Alert_Log)alertLog).Vendor_ID.ToString().Trim());
                    
                    try
                    {
                        TableResult.Load(command.ExecuteReader(), LoadOption.OverwriteChanges);                        

                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {

                        ManageSQLException(ex, usr);


                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        command.Dispose();

                        connection.Close();
                        
                        connection.Dispose();
                    }
                }
            }
            return TableResult;
        }

        /// <summary>
        /// Call Procedure to insert dialy schedule to Trip_hrd
        /// </summary>
        /// <param name="shift">ShiftID</param>

        static public DataTable InsertToHRD(string shift, string vendor)
        {
            DataTable TableResult = new DataTable();

            Connection conex = new Connection();

            Users usr = new Users();
            
            using (var connection = conex.connect())
            {
                if (HttpContext.Current.Session["C_USER"] != null && HttpContext.Current.Session["C_USER"].ToString() != "")
                {
                    usr = (Users)HttpContext.Current.Session["C_USER"];
                }

                using (var command = connection.CreateCommand())
                {
                    
                    command.Connection = connection;
                    
                    command.CommandText = "BMS_INSERT_DST_TO_HRD";
                    
                    command.CommandType = CommandType.StoredProcedure;
                    
                    command.Parameters.AddWithValue("@Shift", shift);
                    
                    command.Parameters.AddWithValue("@Vendor", vendor);
                    
                    try
                    {
                        TableResult.Load(command.ExecuteReader(), LoadOption.OverwriteChanges);

                        
                        GenericClass.SQLInsertObj(new Activities_Log(0, DateTime.Now, usr.User_ID.ToString(), "INSERT", "DAILY OPERATION",
                            "New Operation created for " + shift + " Vendor: " + vendor, ""));

                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {

                        ManageSQLException(ex, usr);


                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        command.Dispose();
                       
                        connection.Close();
                        
                        connection.Dispose();
                    }
                }
            }
            return TableResult;
        }
        /// <summary>
        /// Execute custom query
        /// </summary>
        /// <param name="query">string with the sql sentense to be executed</param>
        /// <returns>DataTable</returns>
        static public DataTable GetCustomData(string query)
        {
            DataTable TableResult = new DataTable();

            Connection conex = new Connection();

            Users usr = new Users();

            if (HttpContext.Current.Session["C_USER"] != null && HttpContext.Current.Session["C_USER"].ToString() != "")
            {
                usr = (Users)HttpContext.Current.Session["C_USER"];
            }

            try
            {                

                using (SqlConnection con = conex.connect())
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            try
                            {

                                cmd.CommandType = CommandType.Text;
                                
                                cmd.Connection = con;
                                
                                sda.SelectCommand = cmd;
                                
                                sda.Fill(TableResult);
                            }

                            finally
                            {
                                cmd.Dispose();
                                
                                con.Close();
                                
                                con.Dispose();
                            }
                        }
                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {

                ManageSQLException(ex, usr);


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return TableResult;
        }

        /// <summary>
        /// Detect when a error is from a raiserror in trigger and send the text in the correct language
        /// </summary>
        /// <param name="sqlException">Exception catched</param>
        /// <param name="usr"> User logged in</param>
        static public void ManageSQLException(System.Data.SqlClient.SqlException sqlException, Users usr)
        {
            int errorNumber;

            string message;

            int maxNoErrorLength = 4;

            string subsMessage = sqlException.Message;

            string removeTransacEnd = sqlException.Message;
            //33
            if (sqlException.Message.Length>4)
            {
                subsMessage = sqlException.Message.Remove(4);
                Regex.Replace(regEx.Replace(subsMessage, valueToReplace), pattern, "");
                removeTransacEnd = subsMessage.Replace("\r\n", "");
            }


            if (removeTransacEnd.Length < maxNoErrorLength)
            {
                try
                {

                    errorNumber = Convert.ToInt32(removeTransacEnd);

                }
                catch (Exception)
                {
                    
                    throw sqlException;
                }
                message = GetTriggerError(errorNumber, usr);

                throw new DataException(message, sqlException);
            }
            else
            {
                throw sqlException;
            }
        }

        /// <summary>
        /// Get text from trigger error, depending on language configuration of user
        /// </summary>
        /// <param name="nError">Number or error to look in table</param>
        /// <param name="us">User logged in</param>
        /// <returns></returns>
        static public string GetTriggerError(int nError, Users us)
        {
            string languageConfig = getUserLanguage(us);

            string errorMessage = "NA";

            DataTable dtError = SQLSelectObj(new Trigger_Errors(), WhereClause: "Where Trigger_Error_Id=" + nError);

            if (languageConfig == "EN")
            {
                errorMessage = dtError.Rows[0]["Error_English"].ToString();
            }
            else
            {
                errorMessage = dtError.Rows[0]["Error_Spanish"].ToString();
            }

            return errorMessage;
        }

        /// <summary>
        /// Get Language set by user
        /// </summary>
        /// <param name="usr">User to look up in configuration table</param>
        /// <returns>If no configuration found returns ES for spanish</returns>
        static public string getUserLanguage(Users usr)
        {
            string userLanguage = "ES";

            if (usr != null)
            {
                DataTable dtLanguage = SQLSelectObj(new Global_Configuration(), WhereClause: "Where configuration_Name='Language' and User_ID='"
                    + usr.User_ID + "'");

                if (dtLanguage.Rows.Count > 0)
                {
                    userLanguage = dtLanguage.Rows[0]["Configuration_Value"].ToString();
                } 
            }

            return userLanguage;
        }

    }
}