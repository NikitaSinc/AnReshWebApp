using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AnReshWebApp.Models;
using AnReshWebApp.Models.FilterEntity;

namespace AnReshWebApp.Filters
{
    public class EmployeeFilter : IFilter<EmployeeFilterModel>
    {
        public EmployeeFilterModel Model { get; set; }
        public string Row { get; set; }

        public EmployeeFilter(EmployeeFilterModel employee)
        {
            Model = employee;
            Row = "";
            GenerateSQLString();
        }

        private void GenerateSQLString()
        {
            string where = " WHERE";
            if (Model.Full_name != null)
            {
                if (DataBaseConfiguration.ChosenSQLConnection == DataBaseConfiguration.MSSQLConnection)
                {
                    Row += where + " Full_name like '%'+@Full_name+'%'";
                }
                else if (DataBaseConfiguration.ChosenSQLConnection == DataBaseConfiguration.MySQLConnection)
                {
                    Row += where + " Full_name like CONCAT('%', @Full_name, '%')";
                }
                where = " AND";
            }
            if (Model.Departments != null)
            {
                Row += where + " Id_department in @Departments";
                where = " AND";
            }
            if (Model.Skills != null)
            {
                Row += where + " Id in (SELECT id_employee FROM EmployeeSkills WHERE Id_skills in @Skills)";
            }
        }
    }
}