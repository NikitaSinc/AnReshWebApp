using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AnReshWebApp.Models
{
    public class EmployeeFilterModel : BaseEntity
    {
        public string Full_name { get; set; }
        public int Salary { get; set; }
        public List<int> Departments { get; set; }
        public List<int> Skills { get; set; }
    }
}