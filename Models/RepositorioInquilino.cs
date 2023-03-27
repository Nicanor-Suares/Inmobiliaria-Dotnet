using MySql.Data.MySqlClient;

namespace Inmobiliaria_DotNet.Models;

public class RepositorioInquilino
{
	string connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;";

	public int AltaInquilino(Inquilino inquilino)
	{
		int res = 0;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			string query = @"INSERT INTO inquilino (nombre, apellido, dni, telefono) 
			VALUES (@Nombre, @Apellido, @Dni, @Telefono);
			SELECT LAST_INSERT_ID();";
			using (MySqlCommand command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@Nombre", inquilino.Nombre);
				command.Parameters.AddWithValue("@Apellido", inquilino.Apellido);
				command.Parameters.AddWithValue("@Dni", inquilino.Dni);
				command.Parameters.AddWithValue("@Telefono", inquilino.Telefono);
				connection.Open();
				res = Convert.ToInt32(command.ExecuteScalar());
				inquilino.idInquilino = res;
				connection.Close();
			}
		}
		return res;
	}

	public int EditarInquilino (Inquilino inquilino)
	{
		int res = 0;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
		string query = @"UPDATE inquilino SET nombre = @Nombre, apellido = @Apellido, 
		dni = @Dni, telefono = @Telefono 
		WHERE idInquilino = @idInquilino";
		using (MySqlCommand command = new MySqlCommand(query, connection))
		{
			command.Parameters.AddWithValue("@Nombre", inquilino.Nombre);
			command.Parameters.AddWithValue("@Apellido", inquilino.Apellido);
			command.Parameters.AddWithValue("@Dni", inquilino.Dni);
			command.Parameters.AddWithValue("@Telefono", inquilino.Telefono);
			command.Parameters.AddWithValue("@idInquilino", inquilino.idInquilino);
			connection.Open();
			res = command.ExecuteNonQuery();
			connection.Close();
		}
			connection.Close();
		}
		return res;
	}

	public Inquilino BuscarInquilino (int idInquilino)
	{
		Inquilino res = null;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			string query = @"SELECT idInquilino, nombre, apellido, dni, telefono 
			FROM inquilino WHERE idInquilino = @idInquilino";
			using (MySqlCommand command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@idInquilino", idInquilino);
				connection.Open();
				using (MySqlDataReader reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						res = new Inquilino
						(
							Convert.ToInt32(reader["idInquilino"]),
							reader["nombre"].ToString(),
							reader["apellido"].ToString(),
							reader["dni"].ToString(),
							reader["telefono"].ToString()
						);
					}
				}
				connection.Close();
			}
		}
		return res;
	}

	public int BorrarInquilino(int idInquilino)
	{
		int res = 0;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			string query = @"DELETE FROM inquilino WHERE idInquilino = @idInquilino";
			using (MySqlCommand command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@idInquilino", idInquilino);
				connection.Open();
				res = command.ExecuteNonQuery();
				connection.Close();
			}
			connection.Close();
		}
		return res;
	}

	public List<Inquilino> ListarInquilinos() 
	{
		List<Inquilino> inquilinos = new List<Inquilino>();
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			string query = @"SELECT idInquilino, nombre, apellido, dni, telefono FROM inquilino";
			using (MySqlCommand command = new MySqlCommand(query, connection))
			{
				connection.Open();
				using (MySqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read()){            
						Inquilino inquilino = new Inquilino
						{
							idInquilino = reader.GetInt32(nameof(Inquilino.idInquilino)),
							Nombre = reader.GetString(nameof(Inquilino.Nombre)),
							Apellido = reader.GetString(nameof(Inquilino.Apellido)),
							Dni = reader.GetString(nameof(Inquilino.Dni)),
							Telefono = reader.IsDBNull(reader.GetOrdinal(nameof(Inquilino.Telefono))) ? null : reader.GetString(nameof(Inquilino.Telefono))
						};
					inquilinos.Add(inquilino);
					}
				}
			}
			connection.Close();
		}
		return inquilinos;
	}

}