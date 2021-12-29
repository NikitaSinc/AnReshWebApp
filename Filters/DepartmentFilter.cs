using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AnReshWebApp.Models;

namespace AnReshWebApp.Filters
{
    public class DepartmentFilter
    {
        public Department department = new Department();
        public string departmentRow;

        public void SetFilter(Department department)
        {
            this.department = department;
            GenerateSQLString();
        }

        private void GenerateSQLString()
        {
            if (department.Name != null)
            {
                departmentRow = " and Name like '%'+@Name+'%'";
            }
        }
    }
}