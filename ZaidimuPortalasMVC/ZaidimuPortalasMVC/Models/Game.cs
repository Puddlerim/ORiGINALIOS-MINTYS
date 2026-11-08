namespace ZaidimuPortalasMVC.Models
{
    public class Game
    {
        public int Id { get; set; }

        public string Pavadinimas { get; set; }

        public string Aprasymas { get; set; }

        public DateTime IsleidimoData { get; set; }

        public string Kurejas { get; set; }

        public string Leidejas { get; set; }

        public string AmziausReitingas { get; set; }

        public string VirselioUrl { get; set; }

        public List<string> Zanrai { get; set; } = new();

        public List<string> Platformos { get; set; } = new();
    }
}