using System;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.DataClasses;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private IUnitOfWorkProvider _unitOfWorkProvider;

        public GenericRepository(IUnitOfWorkProvider provider)
        {
            _unitOfWorkProvider = provider;
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            /*
            var _context = _unitOfWorkProvider.GetUnitOfWorkInstance().Context;
            return _context.Set<TEntity>().FindAsync(id).AsTask();
            */
            
            var _context = _unitOfWorkProvider.GetUnitOfWorkInstance().Context;
            var query = _context.Set<TEntity>().AsQueryable();
            
            var navigations = _context.Model.FindEntityType(typeof(TEntity))
                .GetDerivedTypesInclusive()
                .SelectMany(type => type.GetNavigations())
                .Distinct();

            foreach (var navigation in navigations)
            {
                query = query.Include(navigation.Name);
            }
            return query.FirstOrDefaultAsync(e => e.Id == id);
            
        }

        public async Task CreateAsync(TEntity entity)
        {
            var _context = _unitOfWorkProvider.GetUnitOfWorkInstance().Context;
            _context.Set<TEntity>().Attach(entity);
            await _context.Set<TEntity>().AddAsync(entity);
        }
        
        public void Update(TEntity entity)
        {
            var _context = _unitOfWorkProvider.GetUnitOfWorkInstance().Context;
            //_context.Attach(entity);
            //_context.Update(entity);
            //_context.Entry(entity).CurrentValues.SetValues(entity);
            _context.Entry(entity).State = EntityState.Modified;
            var trips = _context.Set<Trip>();
            var tls = _context.Set<TripLocation>();
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
