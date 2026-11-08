using Microsoft.AspNetCore.Mvc;
using ZaidimuPortalasMVC.Data;

namespace ZaidimuPortalasMVC.Controllers
{
    public class PersonalListController : Controller
    {
        public IActionResult Index()
        {
            var games = SampleData.Games
                .Where(g => g.Id == 1 || g.Id == 2)
                .ToList();

            return View(games);
        }

        public IActionResult Statistics()
        {
            var games = SampleData.Games
                .Where(g => g.Id == 1 || g.Id == 2)
                .ToList();

            return View(games);
        }

        public IActionResult Edit()
        {
            var games = SampleData.Games
                .Where(g => g.Id == 1 || g.Id == 2)
                .ToList();

            return View(games);
        }

        public IActionResult PlayTime(int id)
        {
            var game = SampleData.Games.FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }
    }
}