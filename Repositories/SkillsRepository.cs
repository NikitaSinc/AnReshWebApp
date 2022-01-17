using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AnReshWebApp.Services;
using AnReshWebApp.Repositories;
using AnReshWebApp.Models.FilterEntity;

namespace AnReshWebApp.Models
{
    public class SkillsRepository : GenericRepository<BaseFilterEntity, Skills>
    {
        public SkillsRepository(string tableName, AbstractDBFactory dBFactory, ISQLCommands commands) : base(tableName, dBFactory, commands)
        {
            TableName = AppConfiguration.SkillTableName;
            DBFactory = dBFactory;
            Commands = commands;
        }

        public async override Task<int> AddAsync(Skills entity)
        {
            using (var db = DBFactory.CreateConnection())
            {
                int id = await db.QueryFirstAsync<int>("INSERT INTO Skills(Skill_name) " +
                                                       "VALUES(@Skill_name); " + Commands.ReturnInsertIdString, entity);
                                          
                return id;
            }

        }

        public async override Task<int> UpdateAsync(Skills entity)
        {
            using (var db = DBFactory.CreateConnection())
            {
                var sqlQuery = "UPDATE Skills SET Skill_name = @Skill_name WHERE Id = @Id";
                var result = await db.ExecuteAsync(sqlQuery, entity);
                return result;
            }
        }

    }
}