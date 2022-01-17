using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AnReshWebApp;
using System.Configuration;

namespace AnReshWebApp
{
    public static class AppConfiguration
    {
        public static readonly string DescriptionFilePath = ConfigurationManager.AppSettings.Get("DescriptionFilePath");

        public static readonly string DepartmentTableName = ConfigurationManager.AppSettings.Get("DepartmentTableName");
        public static readonly string EmployeeTableName = ConfigurationManager.AppSettings.Get("EmployeeTableName");
        public static readonly string SkillTableName = ConfigurationManager.AppSettings.Get("SkillTableName");
        public static readonly string UserTableName = ConfigurationManager.AppSettings.Get("UserTableName");
    }

    public static class JWTConfiguration
    {
        public static readonly string JWTSecretkey = ConfigurationManager.AppSettings.Get("secretJWT");
        public static readonly string HostURL = ConfigurationManager.AppSettings.Get("HostURL");
        public static readonly string ClientURL = ConfigurationManager.AppSettings.Get("ClientURL");
        public static readonly int JWTTimeout = 15;
    }

    public static class DataBaseConfiguration
    {
        public static readonly string MSSQLConnection = ConfigurationManager.ConnectionStrings["MSSQLConnection"].ConnectionString;
        public static readonly string MySQLConnection = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
        public static readonly string ChosenSQLConnection = MSSQLConnection;
    }
}