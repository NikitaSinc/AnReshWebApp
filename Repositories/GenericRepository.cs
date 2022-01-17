using AnReshWebApp.Filters;
using AnReshWebApp.Models;
using AnReshWebApp.Models.FilterEntity;
using AnReshWebApp.Services;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AnReshWebApp.Repositories
{
    public abstract class GenericRepository<F,T> : IRepository<F,T> where T: BaseEntity where F: BaseFilterEntity
     {
        public virtual Paginator Paginator { get; set; }
        public virtual IFilter<F> Filter { get; set; }

        protected string TableName;
        protected AbstractDBFactory DBFactory;
        protected ISQLCommands Commands;

        public GenericRepository(string tableName, AbstractDBFactory dBFactory, ISQLCommands commands)
        {
            TableName = tableName;
            DBFactory = dBFactory;
            Commands = commands;
        }
        public virtual Task<int> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<int> DeleteAsync(int id)
        {
            using (var db = DBFactory.CreateConnection())
            {
                var sqlQuery = "DELETE FROM "+TableName+" WHERE Id = @id";
                var result = await db.ExecuteAsync(sqlQuery, new { id });
                return result;
            }
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync()
        {
            using (var db = DBFactory.CreateConnection())
            {
                var result = await db.QueryAsync<T>("SELECT * FROM " + TableName);
                return result.ToList();
            }
        }

        public virtual Task<IReadOnlyList<T>> GetAllAsyncFiltred()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            using (var db = DBFactory.CreateConnection())
            {
                var result = await db.QueryAsync<T>("SELECT * FROM " + TableName + " WHERE Id=@id", new { id });
                return result.FirstOrDefault();
            }
        }

        public virtual Task<int> UpdateAsync(T entity)
        {
             throw new NotImplementedException();
        }

        public virtual Task<Users> LoginAsync(T entity)
        {
            throw new NotImplementedException();
        }

    }
}