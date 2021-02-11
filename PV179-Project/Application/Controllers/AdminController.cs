using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models.LocationModels;
using Application.Models.UserModels;
using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeInterfaces;
using DataAccessLayer.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Application.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserFacade _userFacade;
        private ILocationFacade _locationFacade;
        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(ApplicationMappingConfig.ConfigureMap));

        public AdminController(IUserFacade userFacade, ILocationFacade locationFacade)
        {
            _userFacade = userFacade;
            _locationFacade = locationFacade;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ChangeRights(int id, bool makeMod)
        {
            var user = await _userFacade.GetAsync(id);
            user.Role = makeMod ? UserRole.Moderator : UserRole.RegularUser;
            await _userFacade.UpdateAsync(user);
            //return RedirectToAction("Profile", "User");
            return View("../User/Profile", _mapper.Map<UserModel>(user));
        }


        public IActionResult ShowSubmittedLocations(string searchName, string searchType)
        {
            var locs = _locationFacade.ListAllSubmitted();

            if (!string.IsNullOrEmpty(searchName))
            {
                locs = locs
                    .Where(s => s.Name.ToLower().Contains(searchName.ToLower()))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(searchType))
            {
                locs = locs
                    .Where(s => (int) s.Type == int.Parse(searchType))
                    .ToList();
            }

            return View(_mapper.Map<List<LocationModel>>(locs));  
        }

        [HttpPost]
        public async Task<IActionResult> AcceptSubmission(int id)
        {
            try 
            {
                var loc = await _locationFacade.GetLocationByIdAsync(id);
                loc.PermanentlyAdded = true;
                await _locationFacade.UpdateAsync(_mapper.Map<LocationDto>(loc));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to accept submitted location.");
            }
            return RedirectToAction("ShowSubmittedLocations", "Admin");
        }
    }
}