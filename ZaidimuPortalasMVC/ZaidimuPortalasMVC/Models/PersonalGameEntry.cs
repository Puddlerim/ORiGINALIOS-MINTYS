
namespace ZaidimuPortalasMVC.Models
{
    public class PersonalGameEntry
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public int GameId { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.Now;
    }
}
