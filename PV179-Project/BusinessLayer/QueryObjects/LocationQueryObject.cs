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
    public class LocationQueryObject : QueryObjectBase<Location, LocationDto, LocationFilterDto, IQuery<Location>>
    {
        public LocationQueryObject(IQuery<Location> query) : base(query)
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

            query = string.IsNullOrWhiteSpace(filter.Lat)
                    || string.IsNullOrWhiteSpace(filter.Long)
                    || string.IsNullOrWhiteSpace(filter.Maxdist)
                ? query
                : ((LocationQuery) query).FilterByDistance(double.Parse(filter.Lat),
                    double.Parse(filter.Long), double.Parse(filter.Maxdist));

            return query;
        }
    }
}
