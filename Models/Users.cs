using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnReshWebApp.Models
{
    public class Users : BaseEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}