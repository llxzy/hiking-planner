namespace DataAccessLayer.Infrastructure
{
    public interface IQuery
    {
        IQuery Where(IPredicate p);
        // add another parameter without name indicating the collectioin it is to be called over?
        // https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.where?view=netcore-3.1 like here
        // takes an enumerable
        IQuery SortBy(string accordingTo, bool ascendingOrder);
        IQuery Page(int pageToFetch, int pageSize);
        QueryResult ExecuteAsync();
    }
}