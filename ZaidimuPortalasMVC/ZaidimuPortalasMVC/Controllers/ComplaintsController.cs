using Microsoft.AspNetCore.Mvc;
using ZaidimuPortalasMVC.Data;
using ZaidimuPortalasMVC.Models;

namespace ZaidimuPortalasMVC.Controllers
{
    // «control» Skundų valdiklis (PA „Skundų peržiūra“, Administratorius)
    public class ComplaintsController : Controller
    {
        // «boundary» Skundų sąrašas: rodo neperžiūrėtus skundus
        public IActionResult Index()
        {
            var complaints = ComplaintSampleData.GetUnreviewed();

            ViewBag.Reviews = SampleData.Reviews;

            return View(complaints);
        }

        // «boundary» Skundų peržiūros langas: skundo ir atsiliepimo informacija
        public IActionResult Details(int id)
        {
            var complaint = ComplaintSampleData.GetComplaint(id);

            if (complaint == null)
            {
                return NotFound();
            }

            ViewBag.Review = SampleData.Reviews.FirstOrDefault(r => r.Id == complaint.ReviewId);
            ViewBag.Decision = ComplaintSampleData.GetDecision(id);

            return View(complaint);
        }

        // Skundas pagrįstas: atsiliepimas ištrinamas, būsena „Patvirtintas“
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Approve(int id, string? sprendimoKomentaras)
        {
            var complaint = ComplaintSampleData.GetComplaint(id);

            if (complaint == null)
            {
                return NotFound();
            }

            var review = SampleData.Reviews.FirstOrDefault(r => r.Id == complaint.ReviewId);
            if (review != null)
            {
                SampleData.Reviews.Remove(review);
            }

            ComplaintSampleData.Decide(id, IrasoBusena.Patvirtintas, sprendimoKomentaras);

            TempData["Success"] = "Skundas patvirtintas, atsiliepimas ištrintas.";

            return RedirectToAction("Index");
        }

        // Skundas nepagrįstas: būsena „Atmestas“
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reject(int id, string? sprendimoKomentaras)
        {
            var complaint = ComplaintSampleData.GetComplaint(id);

            if (complaint == null)
            {
                return NotFound();
            }

            ComplaintSampleData.Decide(id, IrasoBusena.Atmestas, sprendimoKomentaras);

            TempData["Success"] = "Skundas atmestas.";

            return RedirectToAction("Index");
        }
    }
}
