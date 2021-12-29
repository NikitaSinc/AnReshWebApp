using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AnReshWebApp.Services;
using AnReshWebApp.Filters;

namespace AnReshWebApp.Models
{

    public interface IEmployeeRepository : IRepository<Employee>
    {
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        public int GetAVGSalary(string row, EmployeeFilterModel employee = null)
        {
            using (var db = DBConnectionFactory.CreateConnection())
            {
                return db.QueryFirst<int>("select AVG(Salary) from (" + row + ") as AVGSalary", employee);
            }
        }

        public int Count(string row, EmployeeFilterModel employee = null) 
        { 
            using (var db = DBConnectionFactory.CreateConnection())
            {
               return db.QueryFirst<int>("select count(*) from ("+row+") as counter", employee);
            }
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            using (var db = DBConnectionFactory.CreateConnection())
            {
                var result = await db.QueryAsync<Employee>("select * from Employee where Id=@Id", new { id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IReadOnlyList<Employee>> GetAllAsync()
        {
            using (var db = DBConnectionFactory.CreateConnection())
            {
                string sql = "select* from Employee; select * from EmployeeSkills";
                using (var multi = await db.QueryMultipleAsync(sql))
                {
                    var employee = multi.Read<Employee>();
                    var skills = multi.Read<EmployeeSkills>();
                    return employee.Select(x => new Employee {
                        Id = x.Id,
                        Full_name = x.Full_name,
                        Id_department = x.Id_department,
                        Salary = x.Salary,
                        Skills = skills.Where(y => y.Id_employee == x.Id).Select(y => y.Id_skills).ToList(),
                    }).ToList();
                }
            }
        }

        public async Task<IReadOnlyList<Employee>> GetAllAsyncFiltered(EmployeeFilter filter, Paginator paginator)
        {
            using (var db = DBConnectionFactory.CreateConnection()) //unit of work
            {
                string sqlEmployee = "select * from (select *, ROW_NUMBER() over(order by Id) as RowNum from Employee " + filter.SQLRow + ") as Numbers " ;
                paginator.Paginate(Count(sqlEmployee, filter.employee), GetAVGSalary(sqlEmployee, filter.employee));
                string sqlEmployeeSkills = "; select * from EmployeeSkills ";
                string sql = sqlEmployee + paginator.SQLRow() + sqlEmployeeSkills;
                using (var multi = await db.QueryMultipleAsync(sql, filter.employee))
                {
                    var employee = multi.Read<Employee>();
                    var skills = multi.Read<EmployeeSkills>();
                    return employee.Select(x => new Employee {
                        Id = x.Id,
                        Full_name = x.Full_name,
                        Id_department = x.Id_department,
                        Salary = x.Salary,
                        Skills = skills.Where(y => y.Id_employee == x.Id).Select(y => y.Id_skills).ToList(),
                    }).ToList();
                }
            }
        }

        public async Task<int> AddAsync(Employee entity)
        {
            using (var db = DBConnectionFactory.CreateConnection())
            {
                entity.Id = await db.QueryFirstAsync<int>("DECLARE @Insertedrows AS table(Id int); "+
                                                          "INSERT INTO Employee(Full_name, Id_department, Salary) "+
                                                          "OUTPUT Inserted.Id INTO @InsertedRows VALUES(@Full_name, @Id_department, @Salary);"+
                                                          "SELECT Id From @Insertedrows", entity);
                
                foreach (int skill in entity.Skills)
                {
                    await db.ExecuteAsync("INSERT INTO EmployeeSkills (Id_employee, Id_skills) VALUES(@id,@skill)", new { id = entity.Id, skill});
                };
                return entity.Id;
            }

        }

        public async Task<int> UpdateAsync(Employee entity)
        {
            using (var db = DBConnectionFactory.CreateConnection())
            {
                await db.ExecuteAsync("DELETE FROM EmployeeSkills WHERE Id_employee = @id", entity);
                foreach(int skill in entity.Skills)
                {
                    await db.ExecuteAsync("INSERT INTO EmployeeSkills (Id_employee, Id_skills) VALUES(@id,@skill)", new {id = entity.Id, skill});
                };
                var sqlQuery = "UPDATE Employee SET Full_name = @Full_name, Id_department=@Id_department, Salary=@Salary WHERE Id = @Id";
                var result = await db.ExecuteAsync(sqlQuery, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var db = DBConnectionFactory.CreateConnection())
            {
                var sqlQuery = "DELETE FROM Employee WHERE Id = @id";
                var result = await db.ExecuteAsync(sqlQuery, new { id });
                return result;
            }
        }

    }
}