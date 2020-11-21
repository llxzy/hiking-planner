using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.QueryDTOs;
using BusinessLayer.QueryObjects;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.DataClasses;
using Infrastructure;
using Infrastructure.Query;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Implementations
{
    class TripLocationService :
        CrudQueryServiceBase<TripLocation, TripLocationDto, FilterDtoBase>,
        ITripLocationService
    {
        public TripLocationService(IRepository<TripLocation> repository,
            IMapper mapper, QueryObjectBase<TripLocation, TripLocationDto, FilterDtoBase, IQuery<TripLocation>> qob)
            : base(repository, mapper, qob)
        {
        }

        public async Task AddArrivalTime(int tripLocationId, DateTime time)
        {
            var tripLocDto = await GetAsync(tripLocationId);
            tripLocDto.ArrivalTime = time;
            Update(tripLocDto);
        }
    }
}
