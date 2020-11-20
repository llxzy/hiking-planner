using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.DataClasses;
using Infrastructure;
using Infrastructure.Query;

namespace BusinessLayer.Services.Implementations
{
    public class TripService : CrudQueryServiceBase<Trip, TripDto, TripFilterDto>, ITripService
    {
        public TripService(IRepository<Trip> repository, IMapper mapper, QueryObjectBase<Trip, TripDto, TripFilterDto, IQuery<Trip>> qob) : base(repository, mapper, qob)
        {
        }

        public List<TripDto> GetTripsByLocation(int locationId)
        {
            throw new System.NotImplementedException();
        }

        public List<TripDto> SortByNewest()
        {
            throw new System.NotImplementedException();
        }

        public bool AddParticipant(int participantId, int tripId)
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveParticipant(int participantId, int tripId)
        {
            throw new System.NotImplementedException();
        }
    }
}