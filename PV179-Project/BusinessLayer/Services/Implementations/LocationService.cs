﻿using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DataClasses;
using Infrastructure;
using AutoMapper;
using BusinessLayer.QueryObjects;
using System.Linq;
using BusinessLayer.Services.Interfaces;
using Infrastructure.Query;

namespace BusinessLayer.Services.Implementations
{
    class LocationService :
        CrudQueryServiceBase<Location, LocationDto, LocationFilterDto>,
        ILocationService
    {

        public LocationService(
            IRepository<Location> repository, 
            IMapper mapper,
            QueryObjectBase<Location, LocationDto, LocationFilterDto, IQuery<Location>> qob ) 
            : base(repository, mapper, qob) 
        {
        }

        public async Task AcceptSubmission(int locationId)
        {
            var loc = await GetAsync(locationId);
            loc.PermanentlyAdded = true;
            Update(loc);
        }

        public List<LocationDto> GetAllSubmitted(object range = null)
        {
            return QueryObject.ExecuteQuery(new LocationFilterDto { Added = "false" }).Items.ToList();
        }

        public List<LocationDto> ListAllSortedByName(string locationName)
        {
            var filter = new LocationFilterDto()
            {
                Name = locationName,
            };
            filter.SortAccordingTo = nameof(filter.Name);

            return QueryObject.ExecuteQuery(filter).Items.ToList();
        }

        public List<LocationDto> ListAllSortedByType(string locationName)
        {
            var filter = new LocationFilterDto()
            {
                Name = locationName,
            };
            filter.SortAccordingTo = nameof(filter.Type);

            return QueryObject.ExecuteQuery(filter).Items.ToList();
        }

        public List<LocationDto> ListAllSortedByVisit()
        {
            var filter = new LocationFilterDto
            {
                UseAscendingOrder = false
            };
            filter.SortAccordingTo = nameof(filter.VisitCount); 
            
            return QueryObject.ExecuteQuery(filter).Items.ToList();
        }
    }
}