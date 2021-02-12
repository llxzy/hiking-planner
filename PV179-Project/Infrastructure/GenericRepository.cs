using System.Threading.Tasks;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private IUnitOfWorkProvider _unitOfWorkProvider;

        public GenericRepository(IUnitOfWorkProvider provider)
        {
            _unitOfWorkProvider = provider;
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            var _context = _unitOfWorkProvider.GetUnitOfWorkInstance().Context;
            return _context.Set<TEntity>().FindAsync(id).AsTask();
        }

        public async Task CreateAsync(TEntity entity)
        {
            var _context = _unitOfWorkProvider.GetUnitOfWorkInstance().Context;
            await _context.Set<TEntity>().AddAsync(entity);
            _context.Entry(entity).State = EntityState.Added;
        }
        
        public void Update(TEntity entity)
        {
            var _context = _unitOfWorkProvider.GetUnitOfWorkInstance().Context;
            _context.Entry(entity).State = EntityState.Modified;
            var e = _context.Entry(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var _context = _unitOfWorkProvider.GetUnitOfWorkInstance().Context;
            var e = await _context.Set<TEntity>().FindAsync(id);
            if (e != null)
            {
                _context.Set<TEntity>().Remove(e);
            }
        }
    }
}
