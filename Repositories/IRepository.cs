using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnReshWebApp.Models
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<BaseEntity> GetByIdAsync(int id);
        Task<IReadOnlyList<BaseEntity>> GetAllAsync();
        Task<int> AddAsync(BaseEntity entity);
        Task<int> UpdateAsync(BaseEntity entity);
        Task<int> DeleteAsync(int id);
    }
}
