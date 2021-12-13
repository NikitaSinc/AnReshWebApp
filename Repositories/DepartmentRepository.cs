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

        public async Task<IReadOnlyList<Department>> GetAllAsync()
        {
            using (var db = DBConnectionFactory.CreateConnection())
            {
                var result = await db.QueryAsync<Department>("SELECT * FROM Department");
                return result.ToList();
            }

        }

        internal async Task<IReadOnlyList<Department>> GetAllAsyncFiltered(DepartmentFilter filter)
        {
            using (var db = DBConnectionFactory.CreateConnection())
            {
                var result = await db.QueryAsync<Department>("SELECT * FROM Department "+ filter.departmentRow, filter.department);
                return result.ToList();
            }
        }

        public async Task<int> AddAsync(Department entity)
        {
            using (var db = DBConnectionFactory.CreateConnection())
            {
                int id = await db.QueryFirstAsync<int>("DECLARE @Insertedrows AS table(Id int); " +
                                                      "INSERT INTO Department(Name) " +
                                                      "OUTPUT Inserted.Id INTO @InsertedRows VALUES(@Name); " +
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