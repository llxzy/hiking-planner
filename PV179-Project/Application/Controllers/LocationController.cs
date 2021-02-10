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

        public IActionResult Index(string searchName, string searchType)
        {
            var locs = _locationFacade.ListAllSortedByVisit();

            if (!string.IsNullOrEmpty(searchName))
            {
                locs = locs
                    .Where(s => s.Name.ToLower().Contains(searchName.ToLower()))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(searchType))
            {
                locs = locs
                    .Where(s => (int)s.Type == int.Parse(searchType))
                    .ToList();
            }

            return View(mapper.Map<List<LocationModel>>(locs));
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
        public async Task<IActionResult> Create(LocationCreateModel location)
        {
            try
            {
                await _locationFacade.CreateAsync(mapper.Map<LocationDto>(location));
            }
            catch(ArgumentException)
            {
                ModelState.AddModelError("", "Location not valid");
                //should work & return the same model??
                return View(location);
            }
            return RedirectToAction("Index", "Location");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _locationFacade.DeleteAsync(id);
            }
            catch(Exception)
            {
                ModelState.AddModelError("", "Unable to delete locations.");
            }
            return RedirectToAction("Index", "Location");
        }
    }
}
