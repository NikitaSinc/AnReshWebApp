using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AnReshWebApp.Models;

namespace AnReshWebApp.Filters
{
    public class EmployeeFilter
    {
        public EmployeeFilterModel employee = new EmployeeFilterModel();
        public string SQLRow="";

        public void SetFilter(EmployeeFilterModel employee)
        {
            this.employee = employee;
            GenerateSQLString();
        }

        private void GenerateSQLString()
        {
            string where = " WHERE";
            if (employee.Full_name != null)
            {
                SQLRow += where + " Full_name like '%'+@Full_name+'%'";
                where = " AND";
            }
            if (employee.Departments != null)
            {
                SQLRow += where + " Id_department in @Departments";
                where = " AND";
            }
            if (employee.Skills != null)
            {
                SQLRow += where + " Id in (SELECT id_employee FROM EmployeeSkills WHERE Id_skills in @Skills)";
            }
        }
    }
}