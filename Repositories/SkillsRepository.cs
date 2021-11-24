using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AnReshWebApp.Models
{
    public interface ISkillsRepository : IRepository<Skills>
    {

    }

    public class SkillsRepository : ISkillsRepository
    {
        private SqlConnection db = new SqlConnection(AppConfiguration.MSSQLConnection);

        public async Task<Skills> GetByIdAsync(int id)
        {
            using (db)
            {
                var result = await db.QueryAsync<Skills>("SELECT * FROM Skills WHERE Id=@id", new { id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IReadOnlyList<Skills>> GetAllAsync()
        {
            using (db)
            {
                var result = await db.QueryAsync<Skills>("SELECT * FROM Skills");
                return result.ToList();
            }

        }

        public async Task<int> AddAsync(Skills entity)
        {
            using (db)
            {
                var sqlQuery = "INSERT INTO Skills (Skill_name) VALUES(@Skill_name)";
                var result = await db.ExecuteAsync(sqlQuery, entity);
                return result;
            }

        }

        public async Task<int> UpdateAsync(Skills entity)
        {
            using (db)
            {
                var sqlQuery = "UPDATE Skills SET Skill_name = @Skill_name WHERE Id = @Id";
                var result = await db.ExecuteAsync(sqlQuery, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (db)
            {
                var sqlQuery = "DELETE FROM Skills WHERE Id = @id";
                var result = await db.ExecuteAsync(sqlQuery, new { id });
                return result;
            }
        }

    }
}