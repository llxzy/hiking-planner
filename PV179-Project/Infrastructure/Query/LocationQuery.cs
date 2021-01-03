using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.DataClasses;
using DataAccessLayer.Enums;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

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

        public LocationQuery FilterByDistance(double latitude, double longitude, double maxdist)
        {
            Queryable = Queryable
                .Where(location
                    =>  Math.Abs(location.Long - longitude) > 0.01 
                        || Math.Abs(location.Lat - latitude) > 0.01 
                    && DistanceBetween(
                        latitude,
                        longitude,
                        location.Lat,
                        location.Long
                        ) < maxdist);
            return this;
        }

        private double ToRadians(double n)
        {
            return n * (Math.PI / 180);
        }

        private double DistanceBetween(double lat1, double long1, double lat2, double long2)
        {
            const int EARTH_RADIUS = 6371;
            var dLat = ToRadians(lat2 - lat1);
            var dLong = ToRadians(long2 - long1);
            var rLat1 = ToRadians(lat1);
            var rLat2 = ToRadians(lat2);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Sin(dLong / 2) * Math.Sin(dLong / 2) *
                    Math.Cos(rLat1) * Math.Cos(rLat2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return EARTH_RADIUS * c;
        }
    }
}
