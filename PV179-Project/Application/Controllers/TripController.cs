using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Models;
using BusinessLayer.Facades.FacadeInterfaces;
using BusinessLayer.DataTransferObjects;
using Application.Models.TripModels;
using Application.Models.LocationModels;
using Application.Models.TripLocationModels;
using Application.Models.UserModels;
using Application.Models.ReviewModels;
using AutoMapper;

namespace Application.Controllers
{
    public class TripController : Controller
    {
        private ITripFacade _tripFacade;
        private IReviewFacade _reviewFacade;
        private IUserFacade _userFacade;
        private ILocationFacade _locationFacade;
        private IMapper mapper = new Mapper(new MapperConfiguration(ApplicationMappingConfig.ConfigureMap));

        public TripController(ITripFacade facade, IReviewFacade reviewFacade, IUserFacade userFacade, ILocationFacade locationFacade)
        {
            _tripFacade = facade;
            _reviewFacade = reviewFacade;
            _userFacade = userFacade;
            _locationFacade = locationFacade;
        }
        
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ListAllTrips()
        {
            var tripDtos = _tripFacade.GetAllTripsSorted();
            return View(mapper.Map<List<TripModel>>(tripDtos));
        }
        
        public async Task<IActionResult> TripDetail(int tripId)
        {
            var trip = await _tripFacade.GetTripByIdAsync(tripId);
            if (trip == null)
            {
                return RedirectToAction("Index", "Trip");
            }
            //todo return View(mapper for tripdto to tripmodel)
            //TODO fix mapping
            var reviews = _reviewFacade.ListReviewsByTrip(trip.Id, null);//@trip.Author.Id);
            var reviewModels = new List<ReviewModel>();
            foreach (var r in reviews)
            {
                //TODO DELETE LATER OR FACE CONSEQUENCES
                var jirik = _userFacade.GetAsync(26).Result;
                r.Author = jirik;
                reviewModels.Add(mapper.Map<ReviewModel>(r));
            }

            return View(new TripModel
            {
                // :'(
                Id = trip.Id,
                Title = trip.Title,
                Author = mapper.Map<UserModel>(trip.Author),
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
                Reviews = reviewModels
            });
            //return View(mapper.Map<TripModel>(trip));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTrip(string Id)
        {
            await _tripFacade.DeleteAsync(int.Parse(Id));
            return RedirectToAction("Profile", "User");
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TripCreateModel tripCreateModel)
        {
            //var tripDto = mapper.Map<TripDto>(tripCreateModel);
            
            var author = await _userFacade.GetAsync(int.Parse(User.Identity.Name));
            
            var tripDto = new TripDto()
            {
                Author = author,
                Title = tripCreateModel.Title,
                Description = tripCreateModel.Description,
                StartDate = tripCreateModel.StartDate,
                Done = tripCreateModel.Done,
                Participants = new List<UserTripDto>()
            };
            
            
            if (!string.IsNullOrEmpty(tripCreateModel.Participants))
            {
                foreach (var p in tripCreateModel.Participants.Split(','))
                {
                    var user = _userFacade.GetUserByMail(p.Trim());
                    if (user != null)
                    {
                        tripDto.Participants.Add(new UserTripDto()
                        {
                            User = user,
                            Trip = tripDto
                        });
                    }
                }
            }
            
            await _tripFacade.CreateAsync(tripDto);
            return RedirectToAction("Profile", "User");
        }

        public IActionResult ManageLocations(int tripId)
        {
            var locations = _locationFacade.ListAllAdded();
            return View(new TripAddLocationModel
            {
                TripId = tripId,
                Locations = mapper.Map<List<LocationModel>>(locations)
            });
        }


        public async Task<IActionResult> AddLocation(int tripId, int locationId)
        {
            var location = await _locationFacade.GetLocationByIdAsync(locationId);
            var trip = await _tripFacade.GetTripByIdAsync(tripId);
            await _tripFacade.AddTripLocationToTripAsync(location, trip);
            return View("ManageLocations", new TripAddLocationModel
                {
                    TripId = tripId,
                    Locations = mapper.Map<List<LocationModel>>(_locationFacade.ListAllAdded())
                });
        }
    }
}