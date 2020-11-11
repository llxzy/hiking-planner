using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccessLayer.Infrastructure
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private IUnitOfWorkProvider _uowProvider;
        private DbContext _context => _uowProvider.GetUnitOfWorkInstance().Context;

        public GenericRepository(IUnitOfWorkProvider uowProvider)
        {
            _uowProvider = uowProvider;
            var x = _context.Set<TEntity>();
        }
        
        public Task<TEntity> GetByIdAsync(int id)
        {
            return _context.Set<TEntity>().FindAsync(id).AsTask();
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return _context.Set<TEntity>().ToListAsync();
        }

        public async Task CreateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return null; //TODO likely might not work or be bad practice
        }

        public async Task DeleteAsync(int id)
        {
            var e = await _context.Set<TEntity>().FindAsync(id);
            if (e != null)
            {
                _context.Set<TEntity>().Remove(e);
            }
        }
    }
}