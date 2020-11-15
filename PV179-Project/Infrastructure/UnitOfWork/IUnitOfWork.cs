using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        public DbContext Context { get; set; }
        Task CommitAsync();
    }
}