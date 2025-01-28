using Steam_1.DAL;
using System.Text.Json.Serialization;

namespace Steam_1.Models
{
    public class Game
    {
        [JsonPropertyName("AppID")]
        public int AppID { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Release_date")]
        public string Release_date { get; set; }
        [JsonPropertyName("Price")]
        public double Price { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("Full_audio_languages")]
        public string Full_audio_languages { get; set; }

        [JsonPropertyName("Header_image")]
        public string Header_image { get; set; }
        [JsonPropertyName("Website")]
        public string Website { get; set; }
        [JsonPropertyName("Windows")]
        public string Windows { get; set; }
        [JsonPropertyName("Mac")]
        public string Mac { get; set; }
        [JsonPropertyName("Linux")]
        public string Linux { get; set; }
        [JsonPropertyName("Score_rank")]
        public int Score_rank { get; set; }
        [JsonPropertyName("Recommendations")]
        public string Recommendations { get; set; }
        [JsonPropertyName("Developers")]
        public string Developers { get; set; }
        [JsonPropertyName("Categories")]
        public string Categories { get; set; }
        [JsonPropertyName("Genres")]
        public string Genres { get; set; }
        [JsonPropertyName("Tags")]
        public string Tags { get; set; }
        [JsonPropertyName("Screenshots")]
        public string Screenshots { get; set; }

        [JsonPropertyName("numberOfPurchases")]
        public int numberOfPurchases { get; set; }

        public static List<Game> GamesList = new List<Game>();

        public Game() { }

        public Game(int appID, string name, string releaseDate, double price, string description, string fullAudioLanguages, string headerImage,
            string website, string windows, string mac, string linux, int scoreRank, string recommendations, string developers,
            string categories, string genres, string tags, string screenshots, int NumberOfPurchases)
        {
            AppID = appID;
            Name = name;
            Release_date = releaseDate;
            Price = price;
            Description = description;
            Full_audio_languages = fullAudioLanguages;
            Header_image = headerImage;
            Website = website;
            Windows = windows;
            Mac = mac;
            Linux = linux;
            Score_rank = scoreRank;
            Recommendations = recommendations;
            Developers = developers;
            Categories = categories;
            Genres = genres;
            Tags = tags;
            Screenshots = screenshots;
            numberOfPurchases = NumberOfPurchases;
        }

        static public List<Game> ReadGame()
        {
            DBServices dbs = new DBServices();

            return dbs.ReadGame();
        }

        static public List<Game> ReadMyGamesList(int id)
        {
            DBServices dbs = new DBServices();

            return dbs.ReadMyGamesList(id);
        }

        public List<Game> GetMyListGameByPrice(int id, double Price)
        {
            DBServices dbs = new DBServices();

            return dbs.GetMyListGameByPrice(id, Price);
        }

        public List<Game> GetMyListGameByRank(int id, int Score_Rank)
        {
            DBServices dbs = new DBServices();

            return dbs.GetMyListGameByRank(id, Score_Rank);
        }

        public bool SaveGameToMyList(int UserId, long GameId)
        {
            DBServices dbs = new DBServices();
            int result = dbs.SaveGameToMyList(UserId, GameId);
            return result > 0;
        }

        public bool DeleteFromMyList(int UserId, long GameId)
        {
            DBServices dbs = new DBServices();
            return dbs.DeleteFromMyList(UserId, GameId);
        }

        public static List<object> GetGameDetails()
        {
            DBServices dbs = new DBServices();
            return dbs.GetGameDetails(); // קריאה למתודה ב-DBServices
        }

    }
}

