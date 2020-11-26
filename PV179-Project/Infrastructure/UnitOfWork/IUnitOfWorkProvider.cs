namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkProvider : System.IDisposable
    {
        IUnitOfWork Create();
        IUnitOfWork GetUnitOfWorkInstance();
    }
}