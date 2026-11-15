using Microsoft.AspNetCore.Mvc;
using ZaidimuPortalasMVC.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ZaidimuPortalasMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ZaidimuPortalasMVC.Controllers
{
    public class PersonalListController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PersonalListController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [Authorize]
        public IActionResult Index()
        {
            // Gauname prisijungusio naudotojo ID
            var userId = _userManager.GetUserId(User);

            // Gauname šio naudotojo išsaugotų žaidimų ID
            var gameIds = _context.PersonalGameEntries
                .Where(x => x.UserId == userId)
                .Select(x => x.GameId)
                .ToList();

            // Pagal ID atrenkame žaidimus
            var games = SampleData.Games
                .Where(g => gameIds.Contains(g.Id))
                .ToList();

            return View(games);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int gameId)
        {
            // 1. Gauname prisijungusio naudotojo ID
            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return Challenge();
            }

            // 2. Patikriname, ar toks žaidimas egzistuoja
            var game = SampleData.Games
                .FirstOrDefault(g => g.Id == gameId);

            if (game == null)
            {
                return NotFound();
            }

            // 3. Patikriname, ar naudotojas jau turi šį žaidimą
            bool alreadyExists = _context.PersonalGameEntries
                .Any(x => x.UserId == userId && x.GameId == gameId);

            if (!alreadyExists)
            {
                // 4. Sukuriame naują asmeninio sąrašo įrašą
                var entry = new PersonalGameEntry
                {
                    UserId = userId,
                    GameId = gameId
                };

                // 5. Išsaugome duomenų bazėje
                _context.PersonalGameEntries.Add(entry);
                await _context.SaveChangesAsync();
            }

            // 6. Grįžtame į žaidimų katalogą
            return RedirectToAction("Index", "Games");
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int gameId)
        {
            // Gauname prisijungusio naudotojo ID
            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return Challenge();
            }

            // Surandame žaidimą naudotojo asmeniniame sąraše
            var entry = await _context.PersonalGameEntries
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    x.GameId == gameId);

            if (entry == null)
            {
                return NotFound();
            }

            // Pašaliname įrašą iš duomenų bazės
            _context.PersonalGameEntries.Remove(entry);
            await _context.SaveChangesAsync();

            // Grįžtame į asmeninį sąrašą
            return RedirectToAction("Edit");
        }



        public IActionResult Statistics()
        {
            var games = SampleData.Games
                .Where(g => g.Id == 1 || g.Id == 2)
                .ToList();

            return View(games);
        }


        [Authorize]
        public IActionResult Edit()
        {
            var userId = _userManager.GetUserId(User);

            var gameIds = _context.PersonalGameEntries
                .Where(x => x.UserId == userId)
                .Select(x => x.GameId)
                .ToList();

            var games = SampleData.Games
                .Where(g => gameIds.Contains(g.Id))
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