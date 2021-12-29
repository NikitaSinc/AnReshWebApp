using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using AnReshWebApp.Services;
using AnReshWebApp.Filters;

namespace AnReshWebApp.Models
{
    public interface IDepartmentRepository : IRepository<Department>
    {

    }

    public class DepartmentRepository : IDepartmentRepository
    {
        public async Task<Department> GetByIdAsync(int id)
        {
            using (var db = DBConnectionFactory.CreateConnection())
            {
                var result = await db.QueryAsync<Department>("SELECT * FROM Department WHERE Id=@id", new { id });
                return result.FirstOrDefault();
            }
        }
        public async Task<IReadOnlyList<Department>> GetDepartmentsOnlyAsync()
        {
            using (var db = DBConnectionFactory.CreateConnection())
            {
                string sql = "SELECT * FROM Department WHERE Pid=0 ";
                var result = await db.QueryAsync<Department>(sql);
                return result.ToList();
            }
        }

        public async Task<IReadOnlyList<Department>> GetAllAsync()
        {
            using (var db = DBConnectionFactory.CreateConnection())
            {
                string sqlGroup = "SELECT * FROM Department WHERE Pid in (SELECT Id from Department WHERE Pid<>0); ";
                string sqlSector = "SELECT * FROM Department WHERE Pid in (SELECT Id from Department WHERE Pid=0); ";
                string sqlDepartment = "SELECT * FROM Department WHERE Pid=0; ";
                string sql = sqlGroup + sqlSector + sqlDepartment;
                using (var multi = await db.QueryMultipleAsync(sql))
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

        internal async Task<IReadOnlyList<Department>> GetAllAsyncFiltered(DepartmentFilter filter)
        {
            using (var db = DBConnectionFactory.CreateConnection())
            {
                string sqlGroup = "SELECT * FROM Department WHERE Pid in (SELECT Id from Department WHERE Pid<>0); ";
                string sqlSector = "SELECT * FROM Department WHERE Pid in (SELECT Id from Department WHERE Pid=0); ";
                string sqlDepartment = "SELECT * FROM Department WHERE Pid=0 "+ filter.departmentRow;
                string sql = sqlGroup + sqlSector + sqlDepartment ;
                using (var multi = await db.QueryMultipleAsync(sql, filter.department))
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

        public async Task<int> AddAsync(Department entity)
        {
            using (var db = DBConnectionFactory.CreateConnection())
            {
                int id = await db.QueryFirstAsync<int>("DECLARE @Insertedrows AS table(Id int); " +
                                                      "INSERT INTO Department(Pid,Name) " +
                                                      "OUTPUT Inserted.Id INTO @InsertedRows VALUES(@Pid,@Name); " +
                                                      "SELECT Id From @Insertedrows", entity);
                return id;
            }

        }

        public async Task<int> UpdateAsync(Department entity)
        {
            using (var db = DBConnectionFactory.CreateConnection())
            {
                var sqlQuery = "UPDATE Department SET Name = @Name WHERE Id = @Id";
                var result = await db.ExecuteAsync(sqlQuery, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var db = DBConnectionFactory.CreateConnection())
            {
                var sqlQuery = "DELETE FROM Department WHERE Id = @id";
                var result = await db.ExecuteAsync(sqlQuery, new { id });
                return result;
            }
        }
    }
}