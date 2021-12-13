using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AnReshWebApp.Models;

namespace AnReshWebApp.Filters
{
    public class EmployeeFilter
    {
        public Employee employee = new Employee();
        public string empoloyeeRow;

        public void SetFilter(Employee employee)
        {
            this.employee = employee;
            GenerateSQLString();
        }

        private void GenerateSQLString()
        {
            string where = " WHERE";
            if (employee.Full_name != null)
            {
                empoloyeeRow += where+ " CHARINDEX(@Full_name, Full_name)>0";
                where = " AND";
            }
            if (employee.Id_department != 0)
            {
                empoloyeeRow += where + " Id_department = @Id_department";
                where = " AND";
            }
        }
    }
}