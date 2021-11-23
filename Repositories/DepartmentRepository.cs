using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace AnReshWebApp.Models
{
    public interface IDepartmentRepository : IRepository<Department>
    {

    }

    public class DepartmentRepository : IDepartmentRepository
    {
        private SqlConnection db = new SqlConnection(AppConfiguration.MSSQLConnection);

        public async Task<Department> GetByIdAsync(int id)
        {
            using (db)
            {
               var result = await db.QueryAsync<Department>("SELECT * FROM Department WHERE Id=@id", new { id });
               return result.FirstOrDefault();
            } 
        }

        public async Task<IReadOnlyList<Department>> GetAllAsync()
        {
            using (db)
            {
                var result = await db.QueryAsync<Department>("SELECT * FROM Department");
                return result.ToList();
            }
           
        }

        public async Task<int> AddAsync(Department entity)
        {
            using (db)
            {
                var sqlQuery = "INSERT INTO Department (Name) VALUES(@Name)";
                var result = await db.ExecuteAsync(sqlQuery, entity);
                return result;
            }
            
        }

        public async Task<int> UpdateAsync(Department entity)
        {
            using (db)
            {
                var sqlQuery = "UPDATE Department SET Name = @Name WHERE Id = @Id";
                var result = await db.ExecuteAsync(sqlQuery, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (db)
            {
                var sqlQuery = "DELETE FROM Department WHERE Id = @id";
                var result = await db.ExecuteAsync(sqlQuery, new { id });
                return result;
            }
        }
        
    }
}