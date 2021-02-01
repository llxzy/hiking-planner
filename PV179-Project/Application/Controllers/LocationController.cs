using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Models.LocationModels;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeInterfaces;
using AutoMapper;

namespace Application.Controllers
{
    public class LocationController : Controller
    {
        private ILocationFacade _locationFacade;
        private IMapper mapper = new Mapper(new MapperConfiguration(ApplicationMappingConfig.ConfigureMap));

        public LocationController(ILocationFacade facade)
        {
            _locationFacade = facade;
        }

        public IActionResult Index()
        {
            var loc = _locationFacade.ListAllSortedByVisit();
            return View(mapper.Map<List<LocationModel>>(loc));
            //return View(new List<LocationModel>() { SampleData.Everest, SampleData.AngelFalls });
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Location");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(LocationCreateModel location)
        {
            try
            {
                _locationFacade.Create(mapper.Map<LocationDto>(location));
            }
            catch(ArgumentException)
            {
                ModelState.AddModelError("", "Location not valid");
                //should work & return the same model??
                return View(location);
            }
            return RedirectToAction("Index", "Location");
        }
    }
}
