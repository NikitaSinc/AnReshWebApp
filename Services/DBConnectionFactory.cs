using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace AnReshWebApp.Services
{
    public class DBConnectionFactory
    {
       public static SqlConnection CreateConnection()
        {
            return new SqlConnection(AppConfiguration.MSSQLConnection);
        }
    }
}