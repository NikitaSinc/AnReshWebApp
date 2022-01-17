using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AnReshWebApp.Models.FilterEntity
{
    public class EmployeeFilterModel : BaseFilterEntity
    {
        public string Full_name { get; set; }
        public int Salary { get; set; }
        public List<int> Departments { get; set; }
        public List<int> Skills { get; set; }
    }
}