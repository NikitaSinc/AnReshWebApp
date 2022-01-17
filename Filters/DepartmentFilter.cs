using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AnReshWebApp.Models;
using AnReshWebApp.Models.FilterEntity;

namespace AnReshWebApp.Filters
{
    public class DepartmentFilter : IFilter<DepartmentFilterModel>
    {
        public  DepartmentFilterModel Model { get; set; }
        public string Row { get; set; }

        public DepartmentFilter(DepartmentFilterModel department)
        {
            Model = department;
            GenerateSQLString();
        }

        private void GenerateSQLString()
        {
            if (Model.Name != null)
            {
                if (DataBaseConfiguration.ChosenSQLConnection == DataBaseConfiguration.MSSQLConnection)
                {
                    Row = " and Name like '%'+@Name+'%'";
                }
                else if(DataBaseConfiguration.ChosenSQLConnection == DataBaseConfiguration.MySQLConnection)
                {
                    Row = " and Name like CONCAT('%', @Name, '%')";
                }   
            }
        }
    }
}