using System;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.DataClasses;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        //private IUnitOfWork _uow;
        //private DbContext _context;
        private IUnitOfWorkProvider _unitOfWorkProvider;

        //provider????
        //public GenericRepository(IUnitOfWork uow)
        //{
        //    _uow = uow;
        //    _context = uow.Context;
        //}

        public GenericRepository(IUnitOfWorkProvider provider)
        {
            _unitOfWorkProvider = provider;
            //_context = _unitOfWorkProvider.GetUnitOfWorkInstance().Context;
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            var _context = _unitOfWorkProvider.GetUnitOfWorkInstance().Context;
            return _context.Set<TEntity>().FindAsync(id).AsTask();
        }

        public async Task CreateAsync(TEntity entity)
        {
            var _context = _unitOfWorkProvider.GetUnitOfWorkInstance().Context;
            _context.Set<TEntity>().Attach(entity);
            await _context.Set<TEntity>().AddAsync(entity);
            //TODO
            //.Entry(entity).State = EntityState.Detached;
        }

        public void Update(TEntity entity)
        {
            var _context = _unitOfWorkProvider.GetUnitOfWorkInstance().Context;
            //_context.Attach(entity);
            //_context.Update(entity);
            //_context.Entry(entity).CurrentValues.SetValues(entity);
            _context.Entry(entity).State = EntityState.Modified;
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
