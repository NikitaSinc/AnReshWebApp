using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnReshWebApp.Filters
{

    public class Paginator
    {
        public int LowIndex;
        public int TopIndex;
        public int RowsPerPage;
        public decimal LastPage;
        public int RowsCount;
        public int AVGSalary;

        public string SQLRow()
        {
            return " where RowNum >= "+ LowIndex +" and RowNum < " + TopIndex;
        }

        public void SetPageSize(int pageNumber, int rowsPerPage)
        {
            RowsPerPage = rowsPerPage;
            TopIndex = pageNumber * rowsPerPage + 1;
            LowIndex = TopIndex - rowsPerPage;
        }
        public void Paginate(int length, int salary)
        {
            AVGSalary = salary;
            RowsCount = length;
            decimal dec = length;
            dec = dec / RowsPerPage;
            LastPage = Math.Ceiling(dec);
            if (LastPage == 0)
            {
                LastPage = 1;
            }
            
        }
    }
}