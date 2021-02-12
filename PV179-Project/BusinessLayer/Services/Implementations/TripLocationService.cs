using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.QueryDTOs;
using BusinessLayer.QueryObjects;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.DataClasses;
using Infrastructure;
using Infrastructure.Query;

namespace BusinessLayer.Services.Implementations
{
    public class TripLocationService :
        CrudQueryServiceBase<TripLocation, TripLocationDto, FilterDtoBase>,
        ITripLocationService
    {
        public TripLocationService(IRepository<TripLocation> repository,
            QueryObjectBase<TripLocation, TripLocationDto, FilterDtoBase, IQuery<TripLocation>> qob)
            : base(repository, qob) { }
    }
}
