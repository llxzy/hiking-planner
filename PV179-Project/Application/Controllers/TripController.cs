using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Application.Models.LocationModels;
using Application.Models.TripModels;
using Application.Models.UserModels;
using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeInterfaces;
using DataAccessLayer.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class TripController : Controller
    {
        private ITripFacade     _tripFacade;
        private IReviewFacade   _reviewFacade;
        private IUserFacade     _userFacade;
        private ILocationFacade _locationFacade;
        private IMapper         mapper = new Mapper(new MapperConfiguration(ApplicationMappingConfig.ConfigureMap));

        public TripController(ITripFacade facade, IReviewFacade reviewFacade, IUserFacade userFacade, ILocationFacade locationFacade)
        {
            _tripFacade     = facade;
            _reviewFacade   = reviewFacade;
            _userFacade     = userFacade;
            _locationFacade = locationFacade;
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
                return RedirectToAction("ListAllTrips", "Trip");
            }
            return View(mapper.Map<TripModel>(trip));
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
            //var author = await _userFacade.GetAsync(int.Parse(User.Identity.Name));
            var tripDto = new TripDto()
            {
                AuthorId = int.Parse(User.Identity.Name),
                Title = tripCreateModel.Title,
                Description = tripCreateModel.Description,
                StartDate = tripCreateModel.StartDate,
                Done = tripCreateModel.Done,
                Participants = new List<UserTripDto>()
            };
            await _tripFacade.CreateAsync(tripDto);
            return RedirectToAction("Profile", "User");
        }

        public IActionResult ManageLocations(int tripId, string searchName, string searchType)
        {
            var locations = _locationFacade.ListAllAdded();

            var locs = mapper.Map<List<LocationModel>>(locations
                .Where(l => l.Trips.All(tl => tl.AssociatedTrip.Id != tripId)));
            if (!string.IsNullOrEmpty(searchName))
            {
                locs = locs.Where(l => l.Name.ToLower().Contains(searchName.ToLower().Trim())).ToList();
            }
            if (!string.IsNullOrEmpty(searchType))
            {
                locs = locs.Where(l => (int)l.Type == int.Parse(searchType)).ToList();
            }
            
            return View(new TripAddLocationModel
            {
                TripId = tripId,
                Locations = locs
            });
        }

        public async Task<IActionResult> AddLocation(int tripId, int locationId)
        {
            await _tripFacade.AddTripLocationToTripAsync(locationId, tripId);
            var locs = _locationFacade.ListAllAdded();
            return View("ManageLocations", new TripAddLocationModel
                {
                    TripId = tripId,
                    Locations = mapper.Map<List<LocationModel>>(locs
                        .Where(l => l.Trips.All(tl => tl.AssociatedTrip.Id != tripId)))
                });
        }
        
        public IActionResult ManageParticipants(int tripId, string searchName, string searchMail)
        {
            var users = _userFacade.GetAllUsers();

            var userList = users
                .Where(u => u.Trips
                    .All(t => t.TripId != tripId) && u.Id != int.Parse(User.Identity.Name))
                .Where(u => u.Role != UserRole.Administrator);
            if (!string.IsNullOrEmpty(searchName))
            {
                userList = userList.Where(u => u.Name.ToLower().Contains(searchName.ToLower().Trim()));
            }
            if (!string.IsNullOrEmpty(searchMail))
            {
                userList = userList.Where(u => u.MailAddress.Equals(searchMail.Trim()));
            }
            
            return View(new UserTripAddModel
            {
                TripId = tripId,
                Users = mapper.Map<List<UserShowModel>>(userList)
            });
        }
        
        public async Task<IActionResult> AddParticipant(int tripId, int participantId)
        {
            var userTripDto = new UserTripDto
            {
                UserId = participantId,
                TripId = tripId
            };
            await _tripFacade.CreateUserTripAsync(userTripDto);
            return View("ManageParticipants", new UserTripAddModel
            {
                TripId = tripId,
                Users = mapper.Map<List<UserShowModel>>(_userFacade.GetAllUsers()
                    .Where(u => u.Trips
                        .All(t => t.TripId != tripId) && u.Id != int.Parse(User.Identity.Name))
                    .Where(u => u.Role != UserRole.Administrator))
            });
        }

        public async Task<IActionResult> FinishTrip(int tripId)
        {
            var trip = await _tripFacade.GetTripByIdAsync(tripId);
            trip.Done = true;
            foreach (var loc in trip.TripLocations)
            {
                loc.AssociatedLocation.VisitCount++;
                await _locationFacade.UpdateAsync(loc.AssociatedLocation);
            }
            await _tripFacade.UpdateAsync(trip);
            return RedirectToAction("TripDetail", "Trip", new {tripId});
        }
    }
}