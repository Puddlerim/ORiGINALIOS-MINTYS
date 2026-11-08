using Microsoft.AspNetCore.Mvc;
using ZaidimuPortalasMVC.Data;
using ZaidimuPortalasMVC.Models;

namespace ZaidimuPortalasMVC.Controllers
{
    public class GamesController : Controller
    {
        // Displays all games
        public IActionResult Index()
        {
            var games = SampleData.Games;

            return View(games);
        }

        // Displays information about one specific game
        public IActionResult Details(int id)
        {
            var game = SampleData.Games.FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            var reviews = SampleData.Reviews
                .Where(r => r.GameId == id)
                .ToList();

            ViewBag.Reviews = reviews;

            return View(game);
        }

        // Opens the review creation page
        [HttpGet]
        public IActionResult CreateReview(int id)
        {
            var game = SampleData.Games.FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            var review = new Review
            {
                GameId = game.Id,
                GameName = game.Pavadinimas
            };

            return View(review);
        }


        // Saves the new review
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateReview(Review review)
        {
            var game = SampleData.Games.FirstOrDefault(g => g.Id == review.GameId);

            if (game == null)
            {
                return NotFound();
            }

            if (review.Rating < 1 || review.Rating > 10)
            {
                ModelState.AddModelError("Rating", "Įvertinimas turi būti nuo 1 iki 10.");
                return View(review);
            }

            review.Id = SampleData.Reviews.Max(r => r.Id) + 1;
            review.GameName = game.Pavadinimas;
            review.CreatedAt = DateTime.Now;

            SampleData.Reviews.Add(review);

            return RedirectToAction("Details", new { id = game.Id });
        }

        // Opens the review editing page
        [HttpGet]
        public IActionResult EditReview(int id)
        {
            var review = SampleData.Reviews.FirstOrDefault(r => r.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }


        // Saves the edited review
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditReview(Review updatedReview)
        {
            var review = SampleData.Reviews.FirstOrDefault(r => r.Id == updatedReview.Id);

            if (review == null)
            {
                return NotFound();
            }

            if (updatedReview.Rating < 1 || updatedReview.Rating > 10)
            {
                ModelState.AddModelError("Rating", "Įvertinimas turi būti nuo 1 iki 10.");
                return View(updatedReview);
            }

            review.Username = updatedReview.Username;
            review.Rating = updatedReview.Rating;
            review.Text = updatedReview.Text;

            return RedirectToAction("Details", new { id = review.GameId });
        }

        // Opens the report page
        [HttpGet]
        public IActionResult ReportReview(int id)
        {
            var review = SampleData.Reviews.FirstOrDefault(r => r.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            var complaint = new Complaint
            {
                ReviewId = review.Id
            };

            return View(complaint);
        }


        // Saves the report
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReportReview(Complaint complaint)
        {
            var review = SampleData.Reviews.FirstOrDefault(r => r.Id == complaint.ReviewId);

            if (review == null)
            {
                return NotFound();
            }

            complaint.Id = SampleData.Complaints.Count + 1;
            complaint.CreatedAt = DateTime.Now;

            SampleData.Complaints.Add(complaint);

            return RedirectToAction("Details", new { id = review.GameId });
        }

        

        public IActionResult Recommendations()
        {
            var recommendations = SampleData.Games
                .Where(g => g.Id == 4 || g.Id == 4)
                .ToList();

            return View(recommendations);
        }

        [HttpGet]
        public IActionResult EditGame(int id)
        {
            var game = SampleData.Games.FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditGame(
    Game updatedGame,
    string Zanrai,
    string Platformos)
        {
            var game = SampleData.Games.FirstOrDefault(g => g.Id == updatedGame.Id);

            if (game == null)
            {
                return NotFound();
            }

            game.Pavadinimas = updatedGame.Pavadinimas;
            game.Aprasymas = updatedGame.Aprasymas;
            game.IsleidimoData = updatedGame.IsleidimoData;
            game.Kurejas = updatedGame.Kurejas;
            game.Leidejas = updatedGame.Leidejas;
            game.AmziausReitingas = updatedGame.AmziausReitingas;

            game.Zanrai = Zanrai
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(z => z.Trim())
                .ToList();

            game.Platformos = Platformos
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim())
                .ToList();

            return RedirectToAction("Details", new { id = game.Id });
        }

    }
}