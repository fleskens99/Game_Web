using MySql.Data.MySqlClient;
using System;
using System.Data;

public class DatabaseHelper
{
    private string connectionString = "Server=localhost;Database=database;Uid=root;Pwd=root;";

    public void TestConnection()
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                Console.WriteLine("Connection successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection failed: " + ex.Message);
            }
        }
    }

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
            MySqlDataReader mySqlDataReader = new(cmd);
            using (MySqlDataReader reader = mySqlDataReader)
            {
                while (reader.Read())
                {

                }
            }
        }
        return dt;
    }
}