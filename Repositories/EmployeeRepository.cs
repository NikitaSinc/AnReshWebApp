using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AnReshWebApp.Models
{

    public interface IEmployeeRepository : IRepository<Employee>
    {
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private SqlConnection db = new SqlConnection(AppConfiguration.MSSQLConnection);
        public async Task<Employee> GetByIdAsync(int id)
        {
            using (db)
            {
                var result = await db.QueryAsync<Employee>("select * from Employee where Id=@Id", new { id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IReadOnlyList<Employee>> GetAllAsync()
        {
            using (db)
            {
                var result = await db.QueryAsync<Employee>("select * from Employee");
                return result.ToList();
            }
        }

        public async Task<int> AddAsync(Employee entity)
        {
            using (db)
            {
                var sqlQuery = "INSERT INTO Employee (Full_name, Id_department, Salary) VALUES(@Full_name, @Id_department, @Salary)";
                var result = await db.ExecuteAsync(sqlQuery, entity);
                return result;
            }

        }

        public async Task<int> UpdateAsync(Employee entity)
        {
            using (db)
            {
                var sqlQuery = "UPDATE Employee SET Full_name = @Full_name, Id_department=@Id_department, Salary=@Salary WHERE Id = @Id";
                var result = await db.ExecuteAsync(sqlQuery, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (db)
            {
                var sqlQuery = "DELETE FROM Employee WHERE Id = @id";
                var result = await db.ExecuteAsync(sqlQuery, new { id });
                return result;
            }
        }

    }
}