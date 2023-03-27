using MySql.Data.MySqlClient;

namespace Inmobiliaria_DotNet.Models;
	public class RepositorioPropietario
	{
	string connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;";

	public int AltaPropietario(Propietario propietario)
	{
		int res = 0;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			string query = @"INSERT INTO propietario (nombre, apellido, dni, direccion, telefono) 
			VALUES (@Nombre, @Apellido, @Dni, @Direccion, @Telefono);
			SELECT LAST_INSERT_ID();";
			using (MySqlCommand command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@Nombre", propietario.Nombre);
				command.Parameters.AddWithValue("@Apellido", propietario.Apellido);
				command.Parameters.AddWithValue("@Dni", propietario.Dni);
				command.Parameters.AddWithValue("@Direccion", propietario.Direccion);
				command.Parameters.AddWithValue("@Telefono", propietario.Telefono);
				connection.Open();
				res = Convert.ToInt32(command.ExecuteScalar());
				propietario.idPropietario = res;
				connection.Close();
			}
		}
		return res;
}

	public int EditarPropietario (Propietario propietario)
	{
		int res = 0;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
		string query = @"UPDATE propietario SET nombre = @Nombre, apellido = @Apellido, 
		dni = @Dni, direccion = @Direccion, telefono = @Telefono 
		WHERE idPropietario = @idPropietario";
		using (MySqlCommand command = new MySqlCommand(query, connection))
		{
			command.Parameters.AddWithValue("@Nombre", propietario.Nombre);
			command.Parameters.AddWithValue("@Apellido", propietario.Apellido);
			command.Parameters.AddWithValue("@Dni", propietario.Dni);
			command.Parameters.AddWithValue("@Direccion", propietario.Direccion);
			command.Parameters.AddWithValue("@Telefono", propietario.Telefono);
			command.Parameters.AddWithValue("@idPropietario", propietario.idPropietario);
			connection.Open();
			res = command.ExecuteNonQuery();
			connection.Close();
		}
			connection.Close();
		}
		return res;
	}

	public Propietario BuscarPropietario(int idPropietario)
	{
		Propietario res = null;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
		var query = @"SELECT idPropietario, nombre, apellido, dni, direccion, telefono 
		FROM propietario WHERE idPropietario = @idPropietario";
		using (var command = new MySqlCommand(query, connection))
		{
			command.Parameters.AddWithValue("@idPropietario", idPropietario);
			connection.Open();
			using (var reader = command.ExecuteReader())
			{
				if (reader.Read())
				{
					res = new Propietario(
					Convert.ToInt32(reader["idPropietario"]),
					reader["nombre"].ToString(),
					reader["apellido"].ToString(),
					reader["dni"].ToString(),
					reader["direccion"].ToString(),
					reader["telefono"].ToString()
					);
				}
			}
			connection.Close();
		}
		}
		return res;
	}

	public int BorrarPropietario(int idPropietario)
	{
		int res = 0;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
		var query = @"DELETE FROM propietario WHERE idPropietario = @idPropietario;";
		using (MySqlCommand command = new MySqlCommand(query, connection))
		{
			command.Parameters.AddWithValue("@idPropietario", idPropietario);
			connection.Open();
			res = command.ExecuteNonQuery();
			connection.Close();
		}
		connection.Close();
		}
		return res;
	}

	public List<Propietario> ListarPropietarios()
	{
		List<Propietario> listaPropietarios = new List<Propietario>();
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
		var query = @"SELECT idPropietario, nombre, apellido, dni, direccion, telefono 
		FROM propietario";

		using (var command = new MySqlCommand(query, connection))
		{
			connection.Open();
			using (var reader = command.ExecuteReader())
			{
			while (reader.Read())
			{
				Propietario propietario = new Propietario
				{
					idPropietario = reader.GetInt32(nameof(propietario.idPropietario)),
					Nombre = reader.GetString(nameof(propietario.Nombre)),
					Apellido = reader.GetString(nameof(propietario.Apellido)),
					Dni = reader.GetString(nameof(propietario.Dni)),
					Direccion = reader.GetString(nameof(propietario.Direccion)),
					Telefono = reader.GetString(nameof(propietario.Telefono))
				};
				listaPropietarios.Add(propietario);  
			}
			}
		}
		connection.Close();
		}
		return listaPropietarios;
	}

}