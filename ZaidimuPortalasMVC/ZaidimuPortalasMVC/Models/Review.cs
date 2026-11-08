namespace ZaidimuPortalasMVC.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int GameId { get; set; }
        public string GameName { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public int Rating { get; set; }
        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}