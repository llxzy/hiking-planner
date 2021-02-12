using System;
using System.Threading.Tasks;
using DataAccessLayer;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public DatabaseContext Context { get; set; }
        Task CommitAsync();
    }
}
