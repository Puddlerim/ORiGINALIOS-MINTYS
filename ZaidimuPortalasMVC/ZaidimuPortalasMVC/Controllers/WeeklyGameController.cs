using Microsoft.AspNetCore.Mvc;
using ZaidimuPortalasMVC.Data;

namespace ZaidimuPortalasMVC.Controllers
{
    public class WeeklyGameController : Controller
    {
        public IActionResult Index()
        {
            var game = SampleData.Games.FirstOrDefault(g => g.Id == 2);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }
    }
}