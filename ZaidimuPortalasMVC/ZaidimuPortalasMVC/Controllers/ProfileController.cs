using Microsoft.AspNetCore.Mvc;

namespace ZaidimuPortalasMVC.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}