using System;
using System.Linq;
using DataAccessLayer.DataClasses;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query
{
    public class TripQuery : QueryBase<Trip>
    {
        public TripQuery(IUnitOfWorkProvider provider) : base(provider)
        {
            /*Queryable = Queryable
                .Include(t => t.Participants)
                .Include(t => t.Author)
                .Include(t => t.Reviews)
                .Include(t => t.TripLocations)
                .ThenInclude(tl => tl.AssociatedLocation);*/
        }

        public TripQuery FilterByStartDate(DateTime date)
        {
            Queryable = Queryable.Where(t => t.StartDate == date);
            return this;
        }
        
        public TripQuery FilterByDone(bool done)
        {
            Queryable = Queryable.Where(t => t.Done == done);
            return this;
        }
        
        public TripQuery FilterByAuthorId(int authorId)
        {
            Queryable = Queryable.Where(t => t.AuthorId == authorId);
            return this;
        }
        
        public TripQuery FilterByTitle(string title)
        {
            Queryable = Queryable.Where(t => t.Title == title);
            return this;
        }

        public TripQuery FilterByLocation(int locationId)
        {
            Queryable = Queryable
                .Where(t => t.TripLocations
                    .Any(tl => tl.AssociatedLocationId == locationId));
            return this;
        }
    }
}