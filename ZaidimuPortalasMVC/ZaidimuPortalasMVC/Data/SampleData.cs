using Microsoft.AspNetCore.Http.HttpResults;
using ZaidimuPortalasMVC.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ZaidimuPortalasMVC.Data
{
    public static class SampleData
    {
        public static List<Game> Games = new List<Game>
        {
            new Game
            {
                Id = 1,
                Pavadinimas = "The Witcher 3: Wild Hunt",
                Aprasymas = "An open-world RPG where players control Geralt of Rivia.",
                IsleidimoData = new DateTime(2015, 5, 19),
                Kurejas = "CD Projekt Red",
                Leidejas = "CD Projekt",
                AmziausReitingas = "PEGI 18",
                Zanrai = new List<string> { "RPG", "Action", "Adventure" },
                Platformos = new List<string> { "PC", "PlayStation", "Xbox", "Nintendo Switch" }
            },

            new Game
            {
                Id = 2,
                Pavadinimas = "Elden Ring",
                Aprasymas = "An action RPG set in a vast fantasy world.",
                IsleidimoData = new DateTime(2022, 2, 25),
                Kurejas = "FromSoftware",
                Leidejas = "Bandai Namco",
                AmziausReitingas = "PEGI 16",
                Zanrai = new List<string> { "RPG", "Action" },
                Platformos = new List<string> { "PC", "PlayStation", "Xbox" }
            },

            new Game
            {
                Id = 3,
                Pavadinimas = "Counter-Strike 2",
                Aprasymas = "A competitive multiplayer first-person shooter.",
                IsleidimoData = new DateTime(2023, 9, 27),
                Kurejas = "Valve",
                Leidejas = "Valve",
                AmziausReitingas = "PEGI 18",
                Zanrai = new List<string> { "FPS", "Shooter", "Multiplayer" },
                Platformos = new List<string> { "PC" }
            },

            new Game
            {
                Id = 4,
                Pavadinimas = "League of Legends",
                Aprasymas = "Degradacija",
                IsleidimoData = new DateTime(2023, 9, 27),
                Kurejas = "Riot Games",
                Leidejas = "Riot Games",
                AmziausReitingas = "PEGI 18",
                Zanrai = new List<string> { "FPS", "Shooter", "Multiplayer" },
                Platformos = new List<string> { "PC" }
            }
        };

        public static List<Review> Reviews = new List<Review>
        {
            new Review
            {
                Id = 1,
                GameId = 4,
                GameName = "League of Legends",
                Username = "Edas Milasius",
                Rating = 0,
                Text = "Kur fembojai?",
                CreatedAt = new DateTime(2026, 10, 20)
            },

            new Review
            {
                Id = 2,
                GameId = 1,
                GameName = "The Witcher 3: Wild Hunt",
                Username = "Gamer456",
                Rating = 9,
                Text = "One of the best RPG games I have ever played.",
                CreatedAt = new DateTime(2026, 10, 22)
            },

            new Review
            {
                Id = 3,
                GameId = 2,
                GameName = "Elden Ring",
                Username = "DarkSoul",
                Rating = 9,
                Text = "Challenging combat and a fantastic world to explore.",
                CreatedAt = new DateTime(2026, 10, 25)
            },

            new Review
            {
                Id = 1,
                GameId = 4,
                GameName = "League of Legends",
                Username = "Edas Milasius",
                Rating = 10,
                Text = "Nulauze paskyra, kaip istrinti komentara???",
                CreatedAt = new DateTime(2026, 10, 20)
            }
        };

        public static List<Complaint> Complaints = new List<Complaint>();

    }
}