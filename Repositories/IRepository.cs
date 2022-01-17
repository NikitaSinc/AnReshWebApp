using AnReshWebApp.Filters;
using AnReshWebApp.Models.FilterEntity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnReshWebApp.Models
{
    public interface IRepository<F,T> where T : BaseEntity where F : BaseFilterEntity
    {
        Paginator Paginator { get; set;}
        IFilter<F> Filter { get; set; }

        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsyncFiltred();
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);
    }
}
