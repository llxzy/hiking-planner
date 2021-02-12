using System.Linq;
using DataAccessLayer.DataClasses;
using Infrastructure.UnitOfWork;

namespace Infrastructure.Query
{
    public class UserQuery : QueryBase<User>
    {
        public UserQuery(IUnitOfWorkProvider provider) : base(provider) { }

        public UserQuery FilterByName(string userName)
        {
            Queryable = Queryable.Where(u => u.Name == userName);
            return this;
        }

        public UserQuery FilterByMail(string mailAddress)
        {
            Queryable = Queryable.Where(u => u.MailAddress == mailAddress);
            return this;
        }
    }
}
