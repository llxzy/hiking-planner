using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Facades.FacadeInterfaces;
using BusinessLayer.DataTransferObjects;
using Application.Models.TripModels;

namespace Application.Controllers
{
    public class TripController : Controller

    {
        private ITripFacade _tripFacade;

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ListAllTrips()
        {
            var user1 = new UserDto
            {
                Id = 787,
                Name = "Whatever",
                MailAddress = "aaa@bbbb.com"
            };

            var user2 = new UserDto
            {
                Id = 788987,
                Name = "Whatever2",
                MailAddress = "aa222a@bbbb.com"
            };

            var trip1 = new TripListDto
            {
                Title = "TRIP1 TITLE",
                Description = "trip1 desc",
                Done = true,
                Author = user1
            };

            var trip2 = new TripListDto
            {
                Title = "TRIP2 TITLE",
                Description = "trip2 desc afnaklgjlkdnl .... :)",
                Done = false,
                Author = user2
            };


            //var tripDtos = _tripFacade.GetAllTripsSorted();
            IEnumerable<TripListDto> tripDtos = new List<TripListDto> { trip1, trip2 };

            return View(tripDtos);
        }
    }
}
