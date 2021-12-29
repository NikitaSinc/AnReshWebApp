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
        public static readonly string MSSQLConnection = ConfigurationManager.ConnectionStrings["MSSQLConnection"].ConnectionString;
        public static readonly string JWTSecretkey = ConfigurationManager.AppSettings.Get("secretJWT");
        public static readonly string HostURL = ConfigurationManager.AppSettings.Get("HostURL");
        public static readonly string ClientURL = ConfigurationManager.AppSettings.Get("ClientURL");
        public static readonly int JWTTimeout = 15;
        public static readonly string DescriptionFilePath = ConfigurationManager.AppSettings.Get("DescriptionFilePath");

    }
}