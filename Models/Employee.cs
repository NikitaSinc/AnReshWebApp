using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AnReshWebApp.Models
{
    public class Employee : BaseEntity
    {
        public string Full_name { get; set; }
        public int Salary { get; set; }

        [ForeignKey("Id_department")]
        public int Id_department { get; set; }
    }
}