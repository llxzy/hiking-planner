using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using DataAccessLayer.DataClasses;
using Infrastructure.Query;

namespace BusinessLayer.QueryObjects
{
    public class TripQueryObject : QueryObjectBase<Trip, TripDto, TripFilterDto, IQuery<Trip>>
    {
        public TripQueryObject(IMapper mapper, IQuery<Trip> query) : base(mapper, query)
        {
            
        }

        public override IQuery<Trip> ApplyFilter(IQuery<Trip> query, TripFilterDto filter)
        {
            /*query = string.IsNullOrWhiteSpace(filter.StartDate)
                ? query
                : ((TripQuery) query).FilterByStartDate();
            */
            query = string.IsNullOrWhiteSpace(filter.Done)
                ? query
                : ((TripQuery) query).FilterByDone(bool.Parse(filter.Done));
            query = string.IsNullOrWhiteSpace(filter.AuthorId)
                ? query
                : ((TripQuery) query).FilterByAuthorId(int.Parse(filter.AuthorId));
            query = string.IsNullOrWhiteSpace(filter.Title)
                ? query
                : ((TripQuery) query).FilterByTitle(filter.Title);
            return query;
            
        }
    }
}