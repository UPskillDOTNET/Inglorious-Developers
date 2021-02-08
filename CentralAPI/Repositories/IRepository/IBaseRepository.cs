using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Repositories.IRepository
{
    public interface IBaseRepository<TEntity> where TEntity: class, new()
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> Find(string id);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);
    }
}
