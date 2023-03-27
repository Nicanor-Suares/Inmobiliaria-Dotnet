using MySql.Data.MySqlClient;

namespace Inmobiliaria_DotNet.Models;

public class RepositorioInmueble
{
  string connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;";

  public int AltaInmueble(Inmueble inmueble)
  {
    int res = 0;
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
      string query = @"INSERT INTO inmueble (direccion, disponibilidad, precio, propietarioInmueble) 
      VALUES (@Direccion, @Disponibilidad, @Precio, @PropietarioInmueble);
      SELECT LAST_INSERT_ID();";
      using (MySqlCommand command = new MySqlCommand(query, connection))
      {
        command.Parameters.AddWithValue("@Direccion", inmueble.Direccion);
        command.Parameters.AddWithValue("@Disponibilidad", inmueble.Disponibilidad);
        command.Parameters.AddWithValue("@Precio", inmueble.Precio);
        command.Parameters.AddWithValue("@PropietarioInmueble", inmueble.PropietarioInmueble);
        connection.Open();
        res = Convert.ToInt32(command.ExecuteScalar());
        inmueble.idInmueble = res;
        connection.Close();
      }
    }
    return res;
  }

  public int EditarInmueble(Inmueble inmueble)
  {
    int res = 0;
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
      string query = @"UPDATE inmueble SET direccion = @Direccion, disponibilidad = @Disponibilidad, 
      precio = @Precio, propietarioInmueble = @PropietarioInmueble 
      WHERE idInmueble = @idInmueble";
      using (MySqlCommand command = new MySqlCommand(query, connection))
      {
        command.Parameters.AddWithValue("@Direccion", inmueble.Direccion);
        command.Parameters.AddWithValue("@Disponibilidad", inmueble.Disponibilidad);
        command.Parameters.AddWithValue("@Precio", inmueble.Precio);
        command.Parameters.AddWithValue("@PropietarioInmueble", inmueble.PropietarioInmueble);
        command.Parameters.AddWithValue("@idInmueble", inmueble.idInmueble);
        connection.Open();
        res = command.ExecuteNonQuery();
        connection.Close();
      }
        connection.Close();
    }
    return res;
  }

  public Inmueble BuscarInmueble(int idInmueble)
  {
    Inmueble res = null;
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
      var query = @"SELECT idInmueble, propietarioInmueble, direccion, precio, disponibilidad 
      FROM inmueble WHERE idInmueble = @idInmueble;";
      using (MySqlCommand command = new MySqlCommand(query, connection))
      {
        command.Parameters.AddWithValue("@idInmueble", idInmueble);
        connection.Open();
        using (MySqlDataReader reader = command.ExecuteReader())
        {
          if (reader.Read())
          {
            res = new Inmueble(
              Convert.ToInt32(reader["idInmueble"]),
              Convert.ToBoolean(reader["disponibilidad"]),
              Convert.ToInt32(reader["precio"]),
              Convert.ToInt32(reader["propietarioInmueble"]),
              reader["direccion"].ToString()
            );
          }
        }
      }
      connection.Close();
    }
    return res;
  }

  public int BorrarInmueble(int idInmueble)
  {
    int res = 0;
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
      var query = @"DELETE FROM inmueble WHERE idInmueble = @idInmueble;";
      using (MySqlCommand command = new MySqlCommand(query, connection))
      {
        command.Parameters.AddWithValue("@idInmueble", idInmueble);
        connection.Open();
        res = command.ExecuteNonQuery();
        connection.Close();
      }
        connection.Close();
    }
    return res;
  }

  public List<Inmueble> ListarInmuebles()
  {
    List<Inmueble> listaInmuebles = new List<Inmueble>();
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
      var query = @"SELECT idInmueble, propietarioInmueble, direccion, precio, disponibilidad 
      FROM inmueble";

      using (MySqlCommand command = new MySqlCommand(query, connection))
      {
        connection.Open();
        using (MySqlDataReader reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            Inmueble inmueble = new Inmueble
            {
              idInmueble = Convert.ToInt32(reader["idInmueble"]),
              PropietarioInmueble = Convert.ToInt32(reader["propietarioInmueble"]),
              Direccion = reader["direccion"].ToString(),
              Precio = Convert.ToInt32(reader["precio"]),
              Disponibilidad = Convert.ToBoolean(reader["disponibilidad"])
            };
            listaInmuebles.Add(inmueble);
          }
        }
      }
      connection.Close();
    }
    return listaInmuebles;
  }

}