using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades
{
    public abstract class FacadeBase
    {
        protected readonly IUnitOfWorkProvider _unitOfWorkProvider;

        protected FacadeBase(IUnitOfWorkProvider provider)
        {
            _unitOfWorkProvider = provider;
        }

        public void Dispose()
        {
            _unitOfWorkProvider.Dispose();
        }
    }
}
