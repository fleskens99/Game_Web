using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Game_Web.Data.Entities;

namespace Game_Web.Data.Models;

public class GameModel
{
    private readonly string connectionString = "Server=localhost;Database=web_aplication;User=root;Password=francisco;";

    public List<Game_Web.Data.Entities.GameModel> GetGames(string query)
    {
        var games = new List<Game_Web.Data.Entities.GameModel>();

        using var conn = new MySqlConnection(connectionString);
        try
        {
            conn.Open();
        }
        catch (MySqlException ex)
        {
            throw new Exception("Database was not connected.", ex);
        }

        using var cmd = new MySqlCommand(query, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var game = new Game_Web.Data.Entities.GameModel
            {
                Id = reader.GetInt32("ID"),
                Name = reader.GetString("Name"),
                Categorie = reader.IsDBNull(reader.GetOrdinal("Categorie")) ? string.Empty : reader.GetString("Categorie"),
                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader.GetString("Description"),
                Picture = reader.IsDBNull(reader.GetOrdinal("Picture")) ? string.Empty : reader.GetString("Picture"),
            };
            games.Add(game);
        }
        conn.Close();
        return games;  
    }

    public void AddGame(Game_Web.Data.Entities.GameModel game)
    {
        if (game == null) throw new ArgumentNullException(nameof(game));

        using var conn = new MySqlConnection(connectionString);
        conn.Open();

 
        var sql = "INSERT INTO game (Name, Categorie, Description, Picture) VALUES (@Name, @Categorie, @Description, @Picture)";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Name", game.Name ?? string.Empty);
        cmd.Parameters.AddWithValue("@Categorie", game.Categorie ?? string.Empty);
        cmd.Parameters.AddWithValue("@Description", game.Description ?? string.Empty);
        cmd.Parameters.AddWithValue("@Picture", game.Picture ?? string.Empty);

        cmd.ExecuteNonQuery();
        conn.Close();
    }
}