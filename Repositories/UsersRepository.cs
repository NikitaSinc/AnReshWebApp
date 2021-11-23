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
    public interface IUsersRepository : IRepository<Users>
    {

    }
    public class UsersRepository : IUsersRepository
    {
        private readonly SqlConnection db = new SqlConnection(AppConfiguration.MSSQLConnection);

        public async Task<Users> LoginAsync(Users entity)
        {
            using (db)
            {
                var result = await db.QueryAsync<Users>("SELECT * FROM Users WHERE Login=@login and Password= @password", entity);
                return result.FirstOrDefault();
            }
        }
        public async Task<Users> GetByIdAsync(int id)
        {
            using (db)
            {
                var result = await db.QueryAsync<Users>("SELECT * FROM Users WHERE Id=@id", new { id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IReadOnlyList<Users>> GetAllAsync()
        {
            using (db)
            {
                var result = await db.QueryAsync<Users>("SELECT * FROM Users");
                return result.ToList();
            }
        }

        public async Task<int> AddAsync(Users entity)
        {
            using (db)
            {
                var result = await db.ExecuteAsync("INSERT INTO Users (Login, Password) VALUES(@Login, @Password)", entity);
                return result;
            }

        }

        public async Task<int> UpdateAsync(Users entity)
        {
            using (db)
            {
                var sqlQuery = "UPDATE Users SET Login = @Login, Password = @Password WHERE Id = @Id";
                var result = await db.ExecuteAsync(sqlQuery, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (db)
            {
                var sqlQuery = "DELETE FROM Users WHERE Id = @id";
                var result = await db.ExecuteAsync(sqlQuery, new { id });
                return result;
            }
        }
    }
}