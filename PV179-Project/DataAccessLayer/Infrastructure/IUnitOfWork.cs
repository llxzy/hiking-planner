using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Infrastructure
{
    public interface IUnitOfWork
    {
        public DbContext Context { get; set; }
        Task CommitAsync();
    }
}