using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Game_Web.Data.Entities;

namespace Game_Web.Data.Models;

public class GameModel
{
    // You can move this into configuration later; kept here for minimal changes.
    private readonly string connectionString = "Server=localhost;Database=database;Uid=root;Pwd=root;";

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
                Id = reader.GetInt32("id"),
                Name = reader.GetString("name"),
                Genre = reader.IsDBNull(reader.GetOrdinal("genre")) ? string.Empty : reader.GetString("genre"),
                Description = reader.IsDBNull(reader.GetOrdinal("description")) ? string.Empty : reader.GetString("description"),
                Picture = reader.IsDBNull(reader.GetOrdinal("picture")) ? string.Empty : reader.GetString("picture"),
            };
            games.Add(game);
        }

        return games;
    }

    public void AddGame(Game_Web.Data.Entities.GameModel game)
    {
        if (game == null) throw new ArgumentNullException(nameof(game));

        using var conn = new MySqlConnection(connectionString);
        conn.Open();

        // Fixed column names and parameter list to match entity
        var sql = "INSERT INTO Games (name, genre, description, picture) VALUES (@name, @genre, @description, @picture)";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@name", game.Name ?? string.Empty);
        cmd.Parameters.AddWithValue("@genre", game.Genre ?? string.Empty);
        cmd.Parameters.AddWithValue("@description", game.Description ?? string.Empty);
        cmd.Parameters.AddWithValue("@picture", game.Picture ?? string.Empty);

        cmd.ExecuteNonQuery();
    }
}