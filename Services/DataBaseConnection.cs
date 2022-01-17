using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace AnReshWebApp.Services
{
    public interface AbstractDBFactory
    {
       System.Data.IDbConnection CreateConnection();
    }
    public class MSSQLFactory : AbstractDBFactory
    {
       public System.Data.IDbConnection CreateConnection()
       {
            return new SqlConnection(DataBaseConfiguration.ChosenSQLConnection);
       }
    }

    public class MySQLFactory : AbstractDBFactory
    {
        public System.Data.IDbConnection CreateConnection()
        {
            return new MySqlConnection(DataBaseConfiguration.ChosenSQLConnection);
        }
    }

    public interface ISQLCommands
    {
        string ReturnInsertIdString { get;}
    }

    public class MySQLCommands : ISQLCommands
    {
        public string ReturnInsertIdString { get;}

        public MySQLCommands()
        {
            ReturnInsertIdString = "SELECT last_insert_id()";
        }

       
    }

    public class MSSQLCommands : ISQLCommands
    {
        public string ReturnInsertIdString { get; }


        public MSSQLCommands()
        {
            ReturnInsertIdString = "SELECT SCOPE_IDENTITY()";
        }
    }
}