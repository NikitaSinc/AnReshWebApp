using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace AnReshWebApp.Models
{
    public interface IDepartmentRepository : IRepository<Department>
    {
       Task<List<string>> GetAllNamesAsync();
       Task<int> GetIdByNameAsync(string name);
    }

    public class DepartmentRepository : IDepartmentRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MSSQLConnection"].ConnectionString;

        public async Task<Department> GetByIdAsync(int id)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["MSSQLConnection"].ConnectionString))
            {
               var result = await db.QueryAsync<Department>("SELECT * FROM Department WHERE Id=@id", new { id });
               return result.FirstOrDefault();
            } 
        }

        public async Task<IReadOnlyList<Department>> GetAllAsync()
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["MSSQLConnection"].ConnectionString))
            {
                var result = await db.QueryAsync<Department>("SELECT * FROM Department");
                return result.ToList();
            }
           
        }

        public async Task<int> AddAsync(Department entity)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["MSSQLConnection"].ConnectionString))
            {
                var sqlQuery = "INSERT INTO Department (Name) VALUES(@Name)";
                var result = await db.ExecuteAsync(sqlQuery, entity);
                return result;
            }
            
        }

        public async Task<int> UpdateAsync(Department entity)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["MSSQLConnection"].ConnectionString))
            {
                var sqlQuery = "UPDATE Department SET Name = @Name WHERE Id = @Id";
                var result = await db.ExecuteAsync(sqlQuery, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["MSSQLConnection"].ConnectionString))
            {
                var sqlQuery = "DELETE FROM Department WHERE Id = @id";
                var result = await db.ExecuteAsync(sqlQuery, new { id });
                return result;
            }
        }

        public async Task<List<string>> GetAllNamesAsync()
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["MSSQLConnection"].ConnectionString))
            {
                var sqlQuery = "SELECT name FROM Department";
                var result = await db.QueryAsync<Department>(sqlQuery);
                List<string> stringResult = new List<string>();
                foreach (Department department in result)
                {
                    stringResult.Add(department.Name);
                }
                return stringResult;
            }
        }

        public async Task<int> GetIdByNameAsync(string name)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["MSSQLConnection"].ConnectionString))
            {
                var result = await db.QueryAsync<Department>("SELECT * FROM Department WHERE Name=@name", new { name });
                return result.FirstOrDefault().Id;
            }
        }
    }
}