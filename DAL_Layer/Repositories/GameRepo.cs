using DAL;
using Entities;
using MySql.Data.MySqlClient;
using Interfaces;


namespace Repos
{
    public class GameRepo : IGameRepo
    {
        public List<Game> GetGames()
        {
            List<Game> games = new List<Game>();

            using (MySqlConnection conn = new MySqlConnection(DatabaseConnectionString.ConnectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Database was not connected.", ex);
                }

                using (MySqlCommand cmd = new MySqlCommand("SELECT * From game", conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var game = new Game
                            {
                                Id = reader.GetInt32("ID"),
                                Name = reader.GetString("Name"),
                                Categorie = reader.IsDBNull(reader.GetOrdinal("Categorie")) ? string.Empty : reader.GetString("Categorie"),
                                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader.GetString("Description"),
                                Picture = reader.IsDBNull(reader.GetOrdinal("Picture")) ? string.Empty : reader.GetString("Picture"),
                            };
                            games.Add(game);
                        }
                    }

                }
                conn.Close();
                return games;

            }

        }

        public void AddGame(Game game)
        {
            if (game == null) throw new ArgumentNullException(nameof(game));

            using var conn = new MySqlConnection(DatabaseConnectionString.ConnectionString);
            conn.Open();


            var sql = "INSERT INTO game (Name, Categorie, Description, Picture) VALUES (@Name, @Categorie, @Description, @Picture)";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Name", game.Name ?? string.Empty);
                cmd.Parameters.AddWithValue("@Categorie", game.Categorie ?? string.Empty);
                cmd.Parameters.AddWithValue("@Description", game.Description ?? string.Empty);
                cmd.Parameters.AddWithValue("@Picture", game.Picture ?? string.Empty);

                cmd.ExecuteNonQuery();
            }
        }
    }
}

