using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.QueryDTOs;
using DataAccessLayer.DataClasses;
using Infrastructure.Query;

namespace BusinessLayer.QueryObjects
{
    public class TripLocationQueryObject : QueryObjectBase<TripLocation, TripLocationDto, FilterDtoBase, IQuery<TripLocation>>
    {
        public TripLocationQueryObject(IQuery<TripLocation> query) : base(query) { }
    }
}
