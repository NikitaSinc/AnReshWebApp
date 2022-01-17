using AnReshWebApp.Models.FilterEntity;
using AnReshWebApp.Repositories;
using AnReshWebApp.Services;
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
    public class UsersRepository : GenericRepository<BaseFilterEntity, Users>
    {
        public UsersRepository(string tableName, AbstractDBFactory dBFactory, ISQLCommands commands) : base(tableName, dBFactory, commands)
        {
            TableName = AppConfiguration.UserTableName;
            DBFactory = dBFactory;
            Commands = commands;
        }

        public async override Task<Users> LoginAsync(Users entity)
        {
            using (var db = DBFactory.CreateConnection())
            {
                var result = await db.QueryAsync<Users>("SELECT * FROM Users WHERE Login=@login and Password= @password", entity);
                return result.FirstOrDefault();
            }
        }

        public async override Task<int> AddAsync(Users entity)
        {
            using (var db = DBFactory.CreateConnection())
            {
                var result = await db.ExecuteAsync("INSERT INTO Users (Login, Password) VALUES(@Login, @Password)", entity);
                return result;
            }

        }

        public async override Task<int> UpdateAsync(Users entity)
        {
            using (var db = DBFactory.CreateConnection())
            {
                var sqlQuery = "UPDATE Users SET Login = @Login, Password = @Password WHERE Id = @Id";
                var result = await db.ExecuteAsync(sqlQuery, entity);
                return result;
            }
        }
    }
}