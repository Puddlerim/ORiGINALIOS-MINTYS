using Microsoft.AspNetCore.Mvc;
using ZaidimuPortalasMVC.Data;
using ZaidimuPortalasMVC.Models;

namespace ZaidimuPortalasMVC.Controllers
{
    // «control» Ataskaitų valdiklis (PA „Žaidimų duomenų ataskaitos peržiūra“, Redaktorius)
    public class ReportsController : Controller
    {
        // «boundary» Žaidimų duomenų ataskaitos puslapis
        public IActionResult Index()
        {
            var games = SampleData.Games;

            var report = new GameReportViewModel
            {
                VisoZaidimu = games.Count,
                SugeneravimoData = DateTime.Now,

                // Žaidimas gali būti keliose platformose, todėl išskleidžiame
                PagalPlatforma = BuildGroups(
                    games.SelectMany(g => g.Platformos.Select(p => (Raktas: p, Zaidimas: g))),
                    games.Count),

                PagalLeideja = BuildGroups(
                    games.Select(g => (Raktas: g.Leidejas, Zaidimas: g)),
                    games.Count),

                PagalKureja = BuildGroups(
                    games.Select(g => (Raktas: g.Kurejas, Zaidimas: g)),
                    games.Count)
            };

            return View(report);
        }

        // Sugrupuoja žaidimus pagal raktą ir apskaičiuoja kiekvienos grupės statistiką
        private static List<GameReportGroup> BuildGroups(
            IEnumerable<(string Raktas, Game Zaidimas)> items,
            int visoZaidimu)
        {
            return items
                .Where(i => !string.IsNullOrWhiteSpace(i.Raktas))
                .GroupBy(i => i.Raktas)
                .Select(grupe =>
                {
                    var zaidimai = grupe.Select(i => i.Zaidimas).Distinct().ToList();

                    return new GameReportGroup
                    {
                        Pavadinimas = grupe.Key,
                        ZaidimuSkaicius = zaidimai.Count,
                        Dalis = visoZaidimu == 0 ? 0 : 100.0 * zaidimai.Count / visoZaidimu,
                        SeniausiasIsleidimas = zaidimai.Min(z => z.IsleidimoData),
                        NaujausiasIsleidimas = zaidimai.Max(z => z.IsleidimoData),
                        Zaidimai = zaidimai.Select(z => z.Pavadinimas).OrderBy(p => p).ToList()
                    };
                })
                .OrderByDescending(g => g.ZaidimuSkaicius)
                .ThenBy(g => g.Pavadinimas)
                .ToList();
        }
    }
}
