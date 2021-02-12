using System.Threading.Tasks;
using DataAccessLayer;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public DatabaseContext Context { get; set; }

        public UnitOfWork(DatabaseContext context)
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
