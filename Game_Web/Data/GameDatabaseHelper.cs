using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Security.Cryptography;

public class GameDatabaseHelper
{
    private string connectionString = "Server=localhost;Database=database;Uid=root;Pwd=root;";
    public List<Game> GetGames(string query)
    {
        DataTable dt = new DataTable();
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
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

                }
            }
        }
        return dt;
    }
}