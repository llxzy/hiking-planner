using BusinessLayer.Facades.FacadeInterfaces;
using Microsoft.AspNetCore.Mvc;

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
        
        public IActionResult CreateChallenge()
        {
            return View();
        }
    }
}