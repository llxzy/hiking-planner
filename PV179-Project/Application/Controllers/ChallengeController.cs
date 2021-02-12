using Application.Models.ChallengeModels;
using BusinessLayer.Facades.FacadeInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application.Controllers
{
    public class ChallengeController : Controller
    {
        private readonly IChallengeFacade _challengeFacade;
        
        public ChallengeController(IChallengeFacade facade)
        {
            _challengeFacade = facade;
        }
        
        [HttpGet]
        public IActionResult CreateChallenge()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateChallenge(ChallengeCreateModel challengeCreateModel)
        {
            await _challengeFacade.CreateAsync(
                challengeCreateModel.TripCount, 
                challengeCreateModel.UserId, 
                challengeCreateModel.Type);

            return RedirectToAction("Profile", "User");
        }
    }
}