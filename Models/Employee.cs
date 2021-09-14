using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnReshWebApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Full_name { get; set; }
        public int Department_id { get; set; }
        public int Salary { get; set; }
    }
}