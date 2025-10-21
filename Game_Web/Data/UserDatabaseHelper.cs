using Game_Web.Data.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Reflection.Metadata;
using System.Security.Cryptography;

public class UserDatabaseHelper
{
    private string connectionString = "Server=localhost;Database=web_aplication;Uid=root;Pwd=francisco;";
    public List<User> GetUsers()
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var users = new List<User>();
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
                    var user = new User
                    {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("name"),
                        Email = reader.GetString("email"),
                        Password = reader.GetString("password"),
                        Picture = reader.GetString("picture"),
                    };
                    users.Add(user);

                }
            }
            return users;
        }
    }
}
