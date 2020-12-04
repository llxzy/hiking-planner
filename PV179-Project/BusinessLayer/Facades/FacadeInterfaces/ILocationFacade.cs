﻿using BusinessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Facades.FacadeInterfaces
{
    interface ILocationFacade : IDisposable
    {
        Task<LocationDto> GetLocationById(int id);

        Task Create(LocationDto locationDto);

        Task Update(LocationDto locationDto);

        Task Delete(int id);

        List<LocationDto> ListAllSortedByName(string locationName);

        List<LocationDto> ListAllSortedByType(string locationName);

        List<LocationDto> ListAllSortedByVisit();

        void CheckLocationValidity(LocationDto locationDto);
    }
}
