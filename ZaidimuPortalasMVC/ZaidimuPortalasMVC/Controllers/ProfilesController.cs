using Microsoft.AspNetCore.Mvc;

namespace ZaidimuPortalasMVC.Controllers
{
    public class ProfilesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            ViewBag.Username = id;

            return View();
        }
    }
}