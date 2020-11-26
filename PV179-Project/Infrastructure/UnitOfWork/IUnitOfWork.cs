using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public DbContext Context { get; set; }
        Task CommitAsync();
    }
}