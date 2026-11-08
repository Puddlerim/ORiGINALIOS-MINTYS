using Microsoft.AspNetCore.Mvc;

namespace ZaidimuPortalasMVC.Controllers
{
    public class PersonalListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Statistics()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult PlayTime()
        {
            return View();
        }
    }
}