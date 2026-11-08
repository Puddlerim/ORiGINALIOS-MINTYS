using Microsoft.AspNetCore.Mvc;

namespace ZaidimuPortalasMVC.Controllers
{
    public class GameSyncController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}