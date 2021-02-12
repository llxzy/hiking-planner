using System;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkProvider : IDisposable
    {
        IUnitOfWork Create();
        IUnitOfWork GetUnitOfWorkInstance();
    }
}
