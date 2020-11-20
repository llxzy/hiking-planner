using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using DataAccessLayer.DataClasses;
using DataAccessLayer.Enums;
using Infrastructure.Query;

namespace BusinessLayer.QueryObjects
{
    class LocationQueryObject : QueryObjectBase<Location, LocationDto, LocationFilterDto, IQuery<Location>>
    {
        public LocationQueryObject(IMapper mapper, IQuery<Location> query) : base(mapper, query)
        {
        }

        public override IQuery<Location> ApplyFilter(IQuery<Location> query, LocationFilterDto filter)
        {
            query = string.IsNullOrWhiteSpace(filter.Name)
                ? query
                : ((LocationQuery)query).FilterByName(filter.Name);

            query = string.IsNullOrWhiteSpace(filter.Type)
                ? query
                : ((LocationQuery)query).FilterByType(Enum.Parse<LocationType>(filter.Type));

            query = string.IsNullOrWhiteSpace(filter.Added)
                ? query
                : ((LocationQuery) query).FilterByAddedStatus(bool.Parse(filter.Added));

            return query;
        }
    }
}
