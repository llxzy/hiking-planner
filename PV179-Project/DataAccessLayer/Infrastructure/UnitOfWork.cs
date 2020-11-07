using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public DbContext Context { get; set; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        public Task CommitAsync()
        {
            return Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}