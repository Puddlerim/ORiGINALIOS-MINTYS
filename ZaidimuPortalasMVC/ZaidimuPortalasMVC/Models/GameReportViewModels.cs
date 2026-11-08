namespace ZaidimuPortalasMVC.Models
{
    // Vienos grupės (platformos, leidėjo ar kūrėjo) statistika
    public class GameReportGroup
    {
        public string Pavadinimas { get; set; } = string.Empty;

        public int ZaidimuSkaicius { get; set; }

        // Dalis nuo visų sistemos žaidimų, %
        public double Dalis { get; set; }

        public DateTime SeniausiasIsleidimas { get; set; }

        public DateTime NaujausiasIsleidimas { get; set; }

        public List<string> Zaidimai { get; set; } = new();
    }

    // Žaidimų duomenų ataskaita: visi sistemos žaidimai, sugrupuoti
    public class GameReportViewModel
    {
        public int VisoZaidimu { get; set; }

        public DateTime SugeneravimoData { get; set; }

        public List<GameReportGroup> PagalPlatforma { get; set; } = new();

        public List<GameReportGroup> PagalLeideja { get; set; } = new();

        public List<GameReportGroup> PagalKureja { get; set; } = new();
    }
}
