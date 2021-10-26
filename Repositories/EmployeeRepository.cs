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
        public async Task<Employee> GetByIdAsync(int id)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["MSSQLConnection"].ConnectionString))
            {
                var result = await db.QueryAsync<Employee, Department, Employee>("select E.Id, E.Full_name, E.Salary, D.Name from Employee as E, Department as D where E.Id_department = D.Id and E.Id=@Id",
                     map: (employee, department) => { employee.Department = department; return employee; },new { id }, splitOn: "Name");
                return result.FirstOrDefault();
            }
        }

        public async Task<IReadOnlyList<Employee>> GetAllAsync()
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["MSSQLConnection"].ConnectionString))
            {
                var result = await db.QueryAsync<Employee, Department, Employee>("select E.Id, E.Full_name, E.Salary, D.Name, D.Id from Employee as E, Department as D where E.Id_department = D.Id", 
                    map :(employee,department)=> { employee.Department = department; return employee; }, splitOn: "Name");
                return result.ToList();
            }
        }

        public async Task<int> AddAsync(Employee entity)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["MSSQLConnection"].ConnectionString))
            {
                var sqlQuery = "INSERT INTO Employee (Full_name, Id_department, Salary) VALUES(@Full_name, @Id_department, @Salary)";
                var result = await db.ExecuteAsync(sqlQuery, entity);
                return result;
            }

        }

        public async Task<int> UpdateAsync(Employee entity)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["MSSQLConnection"].ConnectionString))
            {
                var sqlQuery = "UPDATE Employee SET Full_name = @Full_name, Id_department=@Id_department, Salary=@Salary WHERE Id = @Id";
                var result = await db.ExecuteAsync(sqlQuery, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["MSSQLConnection"].ConnectionString))
            {
                var sqlQuery = "DELETE FROM Employee WHERE Id = @id";
                var result = await db.ExecuteAsync(sqlQuery, new { id });
                return result;
            }
        }

    }
}