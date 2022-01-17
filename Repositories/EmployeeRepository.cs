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
using AnReshWebApp.Repositories;
using AnReshWebApp.Models.FilterEntity;

namespace AnReshWebApp.Models
{
    public class EmployeeRepository : GenericRepository<EmployeeFilterModel, Employee>
    {
        public override Paginator Paginator { get; set; }
        public override IFilter<EmployeeFilterModel> Filter { get; set; }

        public EmployeeRepository(string tableName, AbstractDBFactory dBFactory, ISQLCommands commands) : base(tableName, dBFactory, commands)
        {
            TableName = AppConfiguration.EmployeeTableName;
            DBFactory = dBFactory;
            Commands = commands;
        }

        public int GetAVGSalary(string row, EmployeeFilterModel employee = null)
        {
            using (var db = DBFactory.CreateConnection())
            {
                return db.QueryFirst<int>("select coalesce((select avg(Salary) from (" + row + ") as AVGSalary), 0)", employee);
            }
        }

        public int Count(string row, EmployeeFilterModel employee = null) 
        {
            using (var db = DBFactory.CreateConnection())
            {
                return db.QueryFirst<int>("select count(*) from ("+row+") as counter", employee);
            }
        }

        public async override Task<IReadOnlyList<Employee>> GetAllAsyncFiltred()
        {
            using (var db = DBFactory.CreateConnection())
            {
                string sqlEmployee = "select * from (select *, ROW_NUMBER() over(order by Id) as RowNum from Employee " + Filter.Row + ") as Numbers " ;
                Paginator.Paginate(Count(sqlEmployee, Filter.Model), GetAVGSalary(sqlEmployee, Filter.Model));
                string sqlEmployeeSkills = "; select * from EmployeeSkills ";
                string sql = sqlEmployee + Paginator.SQLRow() + sqlEmployeeSkills;
                using (var multi = await db.QueryMultipleAsync(sql, Filter.Model))
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

        public async override Task<int> AddAsync(Employee entity)
        {
            using (var db = DBFactory.CreateConnection())
            {
                entity.Id = await db.QueryFirstAsync<int>("INSERT INTO Employee(Full_name, Id_department, Salary) "+
                                                          "VALUES(@Full_name, @Id_department, @Salary);"+
                                                          Commands.ReturnInsertIdString, entity);
                                                          
                foreach (int skill in entity.Skills)
                {
                    await db.ExecuteAsync("INSERT INTO EmployeeSkills (Id_employee, Id_skills) VALUES(@id,@skill)", new { id = entity.Id, skill});
                };
                return entity.Id;
            }

        }

        public async override Task<int> UpdateAsync(Employee entity)
        {
            using (var db = DBFactory.CreateConnection())
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
    }
}