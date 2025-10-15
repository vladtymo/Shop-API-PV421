using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data.Entities;

namespace DataAccess.Repositories
{
    public interface IGenericRepository<T> where T : class, BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync(int? pageNumber = null, int pageSize = 10);
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
