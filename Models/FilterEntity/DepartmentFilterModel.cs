using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnReshWebApp.Models.FilterEntity
{
    public class DepartmentFilterModel : BaseFilterEntity
    {
        public List<Department> Childrens { get; set; }
        public int Pid { get; set; }
        public string Name { get; set; }
    }
}