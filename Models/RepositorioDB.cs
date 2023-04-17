using MySql.Data.MySqlClient;

namespace Inmobiliaria_DotNet.Models;

public class RepositorioDB {

	string connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;";

  public RepositorioDB(){}
  public Database GetDatabase(){
    Database res = null;
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
      connection.Open();
      string query = @"SELECT id, container, access_key, connection_string FROM db_config WHERE id = 1";
      using (MySqlCommand command = new MySqlCommand(query, connection))
      {
        using (MySqlDataReader reader = command.ExecuteReader())
        {
          if (reader.Read())
          {
            res = new Database {
              id = Convert.ToInt32(reader["id"]),
              container = reader["container"].ToString(),
              access_key = reader["access_key"].ToString(),
              connection_string = reader["connection_string"].ToString()
            };
          }
        }
      }
      connection.Close();
    }
    return res;
  }

}