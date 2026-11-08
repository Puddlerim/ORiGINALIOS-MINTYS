namespace ZaidimuPortalasMVC.Models
{
    public class Complaint
    {
        public int Id { get; set; }

        public int ReviewId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Reason { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}