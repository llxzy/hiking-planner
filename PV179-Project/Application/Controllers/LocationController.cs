using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Models.LocationModels;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeInterfaces;

namespace Application.Controllers
{
    public class LocationController : Controller
    {
        private ILocationFacade _locationFacade;

        public LocationController(ILocationFacade facade)
        {
            _locationFacade = facade;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(LocationCreateModel locationCreateModel)
        {
            LocationDto location = new LocationDto
            {
                Name = locationCreateModel.Name,
                Type = locationCreateModel.Type,
                Lat = locationCreateModel.Lat,
                Long = locationCreateModel.Long,
                //PermanentlyAdded = false
            };
            //_locationFacade.Create(location);

            return new ContentResult() { Content = location.Name + location.Type };
        }
    }
}
