using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AnReshWebApp.Services;

namespace AnReshWebApp.Models
{
    public interface ISkillsRepository : IRepository<Skills>
    {

    }

    public class SkillsRepository : ISkillsRepository
    {
        public async Task<Skills> GetByIdAsync(int id)
        {
            using (var db = DBConnectionFactory.CreateConnection())
            {
                var result = await db.QueryAsync<Skills>("SELECT * FROM Skills WHERE Id=@id", new { id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IReadOnlyList<Skills>> GetAllAsync()
        {
            using (var db = DBConnectionFactory.CreateConnection())
            {
                var result = await db.QueryAsync<Skills>("SELECT * FROM Skills");
                return result.ToList();
            }

        }

        public async Task<int> AddAsync(Skills entity)
        {
            using (var db = DBConnectionFactory.CreateConnection())
            {
                int id = await db.QueryFirstAsync<int>("DECLARE @Insertedrows AS table(Id int); " +
                                          "INSERT INTO Skills(Skill_name) " +
                                          "OUTPUT Inserted.Id INTO @InsertedRows VALUES(@Skill_name); " +
                                          "SELECT Id From @Insertedrows", entity);
                return id;
            }

        }

        public async Task<int> UpdateAsync(Skills entity)
        {
            using (var db = DBConnectionFactory.CreateConnection())
            {
                var sqlQuery = "UPDATE Skills SET Skill_name = @Skill_name WHERE Id = @Id";
                var result = await db.ExecuteAsync(sqlQuery, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var db = DBConnectionFactory.CreateConnection())
            {
                var sqlQuery = "DELETE FROM Skills WHERE Id = @id";
                var result = await db.ExecuteAsync(sqlQuery, new { id });
                return result;
            }
        }

    }
}