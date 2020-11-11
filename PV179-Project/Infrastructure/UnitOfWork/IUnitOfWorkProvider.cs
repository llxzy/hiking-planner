namespace DataAccessLayer.Infrastructure
{
    public interface IUnitOfWorkProvider
    {
        IUnitOfWork Create();
        IUnitOfWork GetUnitOfWorkInstance();
    }
}