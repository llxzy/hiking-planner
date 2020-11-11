using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.DataClasses;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;

namespace DataAccessLayer.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        Task<TEntity> GetByIdAsync(int id);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
        
    }
}
