using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnReshWebApp.Models
{
    public class Department : BaseEntity
    {
        public List<Department> Childrens { get; set; }
        public int Pid { get; set; }
        public string Name { get; set; }
    }
}