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
        private string connectionString = "Server=127.0.0.1;Database=anreshprobation;Uid=root;Pwd=40544054nikita;";

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
                var result = await db.QueryAsync<Employee>("SELECT * FROM Department");
                return result.ToList();
            }

        }

        public async Task<int> AddAsync(Employee entity)
        {
            using (var db = new MySqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Department (Name) VALUES(@Name)";
                var result = await db.ExecuteAsync(sqlQuery, entity);
                return result;
            }

        }

        public async Task<int> UpdateAsync(Employee entity)
        {
            using (var db = new MySqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Department SET Name = @Name WHERE Id = @Id";
                var result = await db.ExecuteAsync(sqlQuery, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var db = new MySqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Department WHERE Id = @id";
                var result = await db.ExecuteAsync(sqlQuery, new { id });
                return result;
            }
        }

    }
}