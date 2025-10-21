using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Security.Cryptography;
using Game_Web.Data.Entities;

public class RatingDatabaseHelper
{
    private string connectionString = "Server=localhost;Database=database;Uid=root;Pwd=root;";
    public List<Rating> GetGames()
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var ratings = new List<Rating>();

            try
            {
                conn.Open();
            }
            catch (MySqlException exception)
            {
                throw new Exception("Database was not connected.", exception);
            }

            MySqlCommand cmd = new MySqlCommand();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var rating = new Rating
                    {
                        Id = reader.GetInt32("id"),
                        UserId = reader.GetInt32("user_id"),
                        GameId = reader.GetInt32("game_id"),
                    };
                    ratings.Add(rating);

                }
            }
            return ratings;
        }
    }
}
