using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}