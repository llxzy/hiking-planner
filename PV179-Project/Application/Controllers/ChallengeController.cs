using System.Threading.Tasks;
using BusinessLayer.Facades.FacadeInterfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Models.ChallengeModels;

namespace Application.Controllers
{
    public class ChallengeController : Controller
    {
        private IChallengeFacade     _challengeFacade;
        private readonly IUserFacade _userFacade;
        
        public ChallengeController(IChallengeFacade facade, IUserFacade userFacade)
        {
            _challengeFacade = facade;
            _userFacade = userFacade;

        }
        
        [HttpGet]
        public IActionResult CreateChallenge()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateChallenge(ChallengeCreateModel challengeCreateModel)
        {
            await _challengeFacade.CreateAsync(challengeCreateModel.TripCount, 
                challengeCreateModel.UserId, 
                challengeCreateModel.Type);
            return RedirectToAction("Profile", "User");
        }
    }
}