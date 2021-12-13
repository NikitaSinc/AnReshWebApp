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
                var result = await db.QueryAsync<Employee>("select * from Employee");
                var emploeeList = result.ToList();
                foreach (Employee employee in emploeeList)
                {
                    int id = employee.Id;
                    var skillsList = await db.QueryAsync<int>("select Id_skills from EmployeeSkills where Id_employee = @id", new {id});
                    employee.Skills = skillsList.ToList();
                }
                return emploeeList;
            }
        }

        public async Task<IReadOnlyList<Employee>> GetAllAsyncFiltered(EmployeeFilter filter)
        {
            using (var db = DBConnectionFactory.CreateConnection())
            {
                var result = await db.QueryAsync<Employee>("select * from Employee "+filter.empoloyeeRow, filter.employee);
                var employeeList = result.ToList();
                List<Employee> onRemoveEmployeeList = new List<Employee>();
                foreach (Employee employee in employeeList)
                {
                    int id = employee.Id;
                    var skillsList = await db.QueryAsync<int>("select Id_skills from EmployeeSkills where Id_employee = @id", new { id });
                    employee.Skills = skillsList.ToList();
                    bool check = true;
                    if (filter.employee.Skills != null)
                    {
                        foreach (int value in filter.employee.Skills)
                        {
                            if (skillsList.Contains(value) == false)
                            {
                                check = false;
                                break;
                            }
                        }
                  
                        if(check == false)
                        {
                            onRemoveEmployeeList.Add(employee);
                        }  
                    }
                }
                return employeeList.Except(onRemoveEmployeeList).ToList();
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