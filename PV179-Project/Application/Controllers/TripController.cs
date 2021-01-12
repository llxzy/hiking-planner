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
        
        [HttpPost]
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
            await _tripFacade.Delete(int.Parse(Id));
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
            var tripLocation = new TripLocationDto()
            {
                AssociatedLocation = await _locationFacade.GetLocationById(tripCreateModel.LocationId)
            };
            var tripLocations = new List<TripLocationDto>() { tripLocation };

            if (string.IsNullOrEmpty(tripCreateModel.Participants))
            {
                tripCreateModel.Participants = string.Empty;
            }
            
            var participants = new List<UserTripModel>();
            var author = await _userFacade.GetAsync(int.Parse(User.Identity.Name));
            var tripDto = new TripDto()
            {
                Author = author,
                Title = tripCreateModel.Title,
                Description = tripCreateModel.Description,
                StartDate = tripCreateModel.StartDate,
                Done = tripCreateModel.Done,
                TripLocations = tripLocations,
                //Participants = participants
            };
            
            foreach (var p in tripCreateModel.Participants.Split(','))
            {
                var user = _userFacade.GetUserByMail(p.Trim());
                if (user != null)
                {
                    participants.Add(mapper.Map<UserTripModel>(new UserTripDto()
                    {
                        User = user,
                        Trip = tripDto
                    }));
                }
            }
            var tripModel = new TripModel()
            {
                Author = mapper.Map<UserModel>(author),
                Title = tripCreateModel.Title,
                Description = tripCreateModel.Description,
                StartDate = tripCreateModel.StartDate,
                Done = tripCreateModel.Done,
                TripLocations = mapper.Map<List<TripLocationModel>>(tripLocations),
                Participants = participants
            };

            await _tripFacade.Create(tripDto);

            return RedirectToAction("Profile", "User");
        }

        public IActionResult DisplayAddLocation(string searchText)
        {
            var model = _locationFacade.ListAllSortedByName(searchText);
            return PartialView("SearchResults", model);
        }
    }
}