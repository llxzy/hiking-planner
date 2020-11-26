using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades
{
    public abstract class FacadeBase
    {
        protected IUnitOfWorkProvider unitOfWorkProvider;

        protected FacadeBase(IUnitOfWorkProvider provider)
        {
            unitOfWorkProvider = provider;
        }

        public void Dispose()
        {
            unitOfWorkProvider.Dispose();
        }

    }
}
