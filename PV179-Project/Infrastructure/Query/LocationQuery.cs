﻿using System.Linq;
using DataAccessLayer.DataClasses;
using DataAccessLayer.Enums;
using Infrastructure.UnitOfWork;

namespace Infrastructure.Query
{
    public class LocationQuery : QueryBase<Location>
    {
        public LocationQuery(IUnitOfWorkProvider provider) : base(provider) { }

        public LocationQuery FilterByName(string locationName)
        {
            Queryable = Queryable.Where(l => l.Name == locationName);
            return this;
        }

        public LocationQuery FilterByType(LocationType type)
        {
            Queryable = Queryable.Where(l => l.Type == type);
            return this;
        }

        public LocationQuery FilterByAddedStatus(bool added)
        {
            Queryable = Queryable.Where(l => l.PermanentlyAdded == added);
            return this;
        }
    }
}
