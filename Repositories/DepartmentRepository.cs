using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using AnReshWebApp.Services;
using AnReshWebApp.Filters;
using AnReshWebApp.Repositories;
using AnReshWebApp.Models.FilterEntity;

namespace AnReshWebApp.Models
{
    public class DepartmentRepository : GenericRepository<DepartmentFilterModel, Department>
    {
        public override IFilter<DepartmentFilterModel> Filter { get; set; }

        public DepartmentRepository(string tableName, AbstractDBFactory dBFactory, ISQLCommands commands) : base(tableName, dBFactory, commands)
        {
            TableName = tableName;
            DBFactory = dBFactory;
            Commands = commands;
        }

        public async override Task<IReadOnlyList<Department>> GetAllAsyncFiltred() //рекурсию
        {
            using (var db = DBFactory.CreateConnection())
            {
                string sqlGroup = "SELECT * FROM Department WHERE Pid in (SELECT Id from Department WHERE Pid<>0); ";
                string sqlSector = "SELECT * FROM Department WHERE Pid in (SELECT Id from Department WHERE Pid=0); ";
                string sqlDepartment = "SELECT * FROM Department WHERE Pid=0 "+ Filter.Row;
                string sql = sqlGroup + sqlSector + sqlDepartment ;
                using (var multi = await db.QueryMultipleAsync(sql, Filter.Model))
                {
                    var group = multi.Read<Department>();
                    var sector = multi.Read<Department>();
                    var department = multi.Read<Department>();
                    return department.Select(x => new Department {
                        Id = x.Id,
                        Pid = x.Pid,
                        Name = x.Name,
                        Childrens = sector.Select(y => new Department {
                            Id = y.Id,
                            Pid = y.Pid,
                            Name = y.Name,
                            Childrens = group.Select(z => new Department {
                                Id = z.Id,
                                Pid = z.Pid,
                                Name = z.Name,
                            }).Where(z => z.Pid == y.Id).ToList(),
                        }).Where(y => y.Pid == x.Id).ToList(),
                    }).ToList();
                }
            }
        }

        public async override Task<int> AddAsync(Department entity)
        {
            using (var db = DBFactory.CreateConnection())
            {
                var result = await db.QueryFirstAsync<int>("INSERT INTO Department(Pid,Name) VALUES(@Pid,@Name);" 
                                                           + Commands.ReturnInsertIdString, entity);
                return result;
            }
        }

        public async override Task<int> UpdateAsync(Department entity)
        {
            using (var db = DBFactory.CreateConnection())
            {
                var sqlQuery = "UPDATE Department SET Name = @Name WHERE Id = @Id";
                var result = await db.ExecuteAsync(sqlQuery, entity);
                return result;
            }
        }
    }
}