using System;
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
            await _userFacade.Update(user);
            //return RedirectToAction("Profile", "User");
            return View("../User/Profile", _mapper.Map<UserModel>(user));
        }

        [HttpGet]
        public IActionResult ShowSubmittedLocations()
        {
            var locs = _locationFacade.ListAllSubmitted();
            return View(_mapper.Map<List<LocationModel>>(locs));
        }

        //[HttpPost]
        //public async Task<IActionResult> SubmitLocations(List<LocationModel> locsToAdd)
        //{
        //    foreach(var loc in locsToAdd)
        //    {
        //        loc.PermanentlyAdded = true;
        //        await _locationFacade.Update(_mapper.Map<LocationDto>(loc));
        //    }
        //    return RedirectToAction("Index", "Admin");
        //}

        [HttpPost]
        public async Task<IActionResult> AcceptSubmission(int id)
        {
            var loc = await _locationFacade.GetLocationById(id);
            try 
            {
                loc.PermanentlyAdded = true;
                await _locationFacade.Update(_mapper.Map<LocationDto>(loc));
            }
            catch (Exception)
            {
                //??
            }
            return RedirectToAction("ShowSubmittedLocations", "Admin");
        }
    }
}