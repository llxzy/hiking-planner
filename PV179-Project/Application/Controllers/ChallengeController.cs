using BusinessLayer.Facades.FacadeInterfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Models.ChallengeModels;

namespace Application.Controllers
{
    public class ChallengeController : Controller
    {
        private IChallengeFacade _challengeFacade;
        
        public ChallengeController(IChallengeFacade facade)
        {
            _challengeFacade = facade;
        }
        // GET
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult CreateChallenge()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateChallenge(ChallengeCreateModel challengeCreateModel)
        {
            _challengeFacade.Create(challengeCreateModel);
            return RedirectToAction("Profile", "User");
        }
    }
}