using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Facades.FacadeInterfaces;
using BusinessLayer.DataTransferObjects;
using Application.Models.TripModels;
using Application.Models.LocationModels;
using Application.Models.TripLocationModels;
using Application.Models.UserModels;

namespace Application.Controllers
{
    public class TripController : Controller
    {
        private ITripFacade _tripFacade;

        public TripController(ITripFacade facade)
        {
            _tripFacade = facade;
        }
        
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ListAllTrips()
        {
            var user1 = new UserModel
            {
                //Id = 787,
                Name = "Whatever",
                MailAddress = "aaa@bbbb.com"
            };

            var user2 = new UserModel
            {
                //Id = 788987,
                Name = "Whatever2",
                MailAddress = "aa222a@bbbb.com"
            };

            var trip1 = new TripModel
            {
                Title = "TRIP1 TITLE",
                Description = "trip1 desc",
                Done = true,
                Author = user1,
                TripLocations = new List<TripLocationModel>()
            };

            var trip2 = new TripModel
            {
                Title = "TRIP2 TITLE",
                Description = "trip2 desc afnaklgjlkdnl .... :)",
                Done = false,
                Author = user2,
                TripLocations = new List<TripLocationModel>()
            };
            var galapagy = new LocationModel
            {
                Name = "Galapagy",
                Type = DataAccessLayer.Enums.LocationType.Waterfall,
                Lat = 0,
                Long = 2
            };

            var everest = new LocationModel
            {
                Name = "Mt Everest", 
                Type = DataAccessLayer.Enums.LocationType.Mountain,
                Lat = 5545445,
                Long = 667
            };

            var tripLocation1 = new TripLocationModel
            {
                AssociatedTrip = trip1,
                AssociatedLocation = galapagy,
                ArrivalTime = DateTime.Today,
            };

            var tripLocation2 = new TripLocationModel
            {
                AssociatedTrip = trip2,
                AssociatedLocation = everest,
                ArrivalTime = DateTime.Today,
            };

            trip1.TripLocations.Add(tripLocation1);
            trip2.TripLocations.Add(tripLocation2);

            //var tripDtos = _tripFacade.GetAllTripsSorted();
            IEnumerable<TripModel> tripDtos = new List<TripModel> { trip1, trip2 };

            return View(tripDtos);
        }
        
        [HttpPost]
        public async Task<IActionResult> TripDetail(int tripId)
        {
            var trip = await _tripFacade.GetTripByIdAsync(tripId);
            if (trip == null)
            {
                return RedirectToAction("Index", "Trip");
            }
            //todo return View(mapper for tripdto to tripmodel)
            return View(new TripModel
            {
                // :(
                Title = trip.Title,
                Author = new UserModel{Name = "trip.Author.Name"},
                Description = trip.Description,
                Done = trip.Done,
                StartDate = trip.StartDate,
                TripLocations = trip.TripLocations
                    .Select(a => 
                        new TripLocationModel
                        {
                            AssociatedLocation = new LocationModel {Name = a.AssociatedLocation.Name},
                            ArrivalTime = a.ArrivalTime
                        })
                    .ToList(),
            });
        }
        
    }
}
