using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Facades.FacadeInterfaces;
using System.Threading.Tasks;
using AutoMapper;

namespace Application.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewFacade _reviewFacade;
        private readonly IMapper mapper = new Mapper(new MapperConfiguration(ApplicationMappingConfig.ConfigureMap));

        public ReviewController(IReviewFacade facade)
        {
            _reviewFacade = facade;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, int tripId)
        {
            try
            {
                await _reviewFacade.Delete(id);
            }
            catch (Exception)
            {
            }
            return RedirectToAction("TripDetail", "Trip", new { id = tripId });
        }
    }
}
