using System;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private IUnitOfWorkProvider _uowProvider;
        //private DbContext _context => _uowProvider.GetUnitOfWorkInstance().Context;
        private DbContext _context;

        public GenericRepository(IUnitOfWorkProvider uowProvider)
        {
            _uowProvider = uowProvider;
            _uowProvider.Create();
            _context = uowProvider.GetUnitOfWorkInstance().Context;
        }
        
        public Task<TEntity> GetByIdAsync(int id)
        {
            return _context.Set<TEntity>().FindAsync(id).AsTask();
        }

        public async Task CreateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
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
