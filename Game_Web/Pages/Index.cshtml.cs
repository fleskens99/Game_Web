using Game_Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace Game_Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {


        }
        private string connectionString = "Server=localhost;Database=database;Uid=root;Pwd=root;";
        public List<Game> GetGames(string query, List<Game> Gamedt)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                var games = new List<Game>();
                try
                {
                    conn.Open();
                }
                catch (MySqlException exception)
                {
                    throw new Exception("Database was not connected.", exception);
                }

                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var game = new Game
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            Genre = reader.GetString("genre"),
                            Description = reader.GetString("description"),
                            Picture = reader.GetString("picture"),
                        };
                        games.Add(game);
                    }
                }
                return games;
            }
        }
        public void AddGame(Game game)
        {
            using var conn = new MySqlConnection(connectionString);
            conn.Open();

            var cmd = new MySqlCommand("INSERT INTO Games (Title, Genre, ReleaseDate) VALUES (@title, @genre, @releaseDate)", conn);
            cmd.Parameters.AddWithValue("@title", game.Name);
            cmd.Parameters.AddWithValue("@genre", game.Genre);
            cmd.Parameters.AddWithValue("@description", game.Description);
            cmd.Parameters.AddWithValue("@picture", game.Picture);

            cmd.ExecuteNonQuery();
        }
    }
}
