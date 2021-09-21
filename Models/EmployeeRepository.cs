using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
        private string connectionString = "Server=127.0.0.1;Database=anreshprobation;Uid=anreshuser;Pwd=anreshuser;";

        public async Task<Employee> GetByIdAsync(int id)
        {
            using (var db = new MySqlConnection(connectionString))
            {
                var result = await db.QueryAsync<Employee>("SELECT * FROM Employee WHERE Id=@id", new { id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IReadOnlyList<Employee>> GetAllAsync()
        {
            using (var db = new MySqlConnection(connectionString))
            {
                var result = await db.QueryAsync<Employee>("SELECT * FROM Employee");
                return result.ToList();
            }

        }

        public async Task<int> AddAsync(Employee entity)
        {
            using (var db = new MySqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Employee (Full_name, Id_department, Salary) VALUES(@Full_name, @Id_department, @Salary)";
                var result = await db.ExecuteAsync(sqlQuery, entity);
                return result;
            }

        }

        public async Task<int> UpdateAsync(Employee entity)
        {
            using (var db = new MySqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Employee SET Full_name = @Full_name, Id_department=@Id_department, Salary=@Salary WHERE Id = @Id";
                var result = await db.ExecuteAsync(sqlQuery, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var db = new MySqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Employee WHERE Id = @id";
                var result = await db.ExecuteAsync(sqlQuery, new { id });
                return result;
            }
        }

    }
}