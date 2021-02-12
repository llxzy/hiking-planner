using System;
using System.Threading;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWorkProvider : IUnitOfWorkProvider
    {
        private readonly Func<DatabaseContext>   _contextFactory;
        private readonly AsyncLocal<IUnitOfWork> _unitOfWorkLocal = new AsyncLocal<IUnitOfWork>();

        public UnitOfWorkProvider(Func<DatabaseContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        
        public IUnitOfWork Create()
        {
            _unitOfWorkLocal.Value = new UnitOfWork(_contextFactory.Invoke());
            return _unitOfWorkLocal.Value;
        }

        public void Dispose()
        {
            _unitOfWorkLocal.Value?.Dispose();
            _unitOfWorkLocal.Value = null;
        }

        public IUnitOfWork GetUnitOfWorkInstance()
        {
            return _unitOfWorkLocal.Value ?? throw new InvalidOperationException("Not created.");
        }
    }
}