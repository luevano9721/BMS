using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace BusManagementSystem.Class
{
    
    public class Connection
    {
        public SqlConnection con;
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public Connection()
        {
           
        }
        public SqlConnection connect()
        {
        
         SqlConnection con = new SqlConnection(connStr);
            try
            {
                con.Open();
                return con;
            }
            catch (SqlException)
            {
                con.Dispose();
                return null;
            }

        }

        public void close()
        {
            con.Close();
        }
       
    }
}