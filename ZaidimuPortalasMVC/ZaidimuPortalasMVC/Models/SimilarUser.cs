namespace ZaidimuPortalasMVC.Models;

public class SimilarUserViewModel
{
    public string Username { get; set; } = string.Empty;

    public int CommonGames { get; set; }

    public int TotalGames { get; set; }

    public double SimilarityPercentage { get; set; }
}