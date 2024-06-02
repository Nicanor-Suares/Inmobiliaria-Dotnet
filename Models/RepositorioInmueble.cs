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
			string query = @"INSERT INTO inmueble (direccion, disponibilidad, precio, propietarioId, idTipo) 
			VALUES (@Direccion, @Disponibilidad, @Precio, @PropietarioId, @idTipo);
			SELECT LAST_INSERT_ID();";
			using (MySqlCommand command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@Direccion", inmueble.Direccion);
				command.Parameters.AddWithValue("@Disponibilidad", inmueble.Disponibilidad);
				command.Parameters.AddWithValue("@Precio", inmueble.Precio);
				command.Parameters.AddWithValue("@PropietarioId", inmueble.PropietarioId);
				command.Parameters.AddWithValue("@idTipo", inmueble.idTipo);
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
			precio = @Precio, propietarioId = @PropietarioId, idTipo = @idTipo 
			WHERE idInmueble = @idInmueble";
			using (MySqlCommand command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@Direccion", inmueble.Direccion);
				command.Parameters.AddWithValue("@Disponibilidad", inmueble.Disponibilidad);
				command.Parameters.AddWithValue("@Precio", inmueble.Precio);
				command.Parameters.AddWithValue("@PropietarioId", inmueble.PropietarioId);
				command.Parameters.AddWithValue("@idTipo", inmueble.idTipo);
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
			var query = @"SELECT idInmueble, propietarioId, i.direccion, precio, disponibilidad,
			p.Nombre, p.Apellido, ti.idTipo, ti.tipo
			FROM inmueble i INNER JOIN propietario p ON i.propietarioId = idPropietario 
			INNER JOIN tipo_inmueble ti ON i.idTipo = ti.idTipo
			WHERE idInmueble = @idInmueble;";
			using (MySqlCommand command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@idInmueble", idInmueble);
				connection.Open();
				using (MySqlDataReader reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						res = new Inmueble{
							idInmueble = reader.GetInt32(nameof(Inmueble.idInmueble)),
							PropietarioId = reader.GetInt32(nameof(Inmueble.PropietarioId)),
							Direccion = reader.GetString("Direccion"),
							Precio = reader.GetInt32(nameof(Inmueble.Precio)),
							Disponibilidad = reader.GetBoolean("Disponibilidad"),
							PropietarioInmueble = new Propietario{
								idPropietario = reader.GetInt32(nameof(Inmueble.PropietarioId)),
								Nombre = reader.GetString("Nombre"),
								Apellido = reader.GetString("Apellido"),
							},
							TipoInmueble = new TipoInmueble{
								idTipo = reader.GetInt32(nameof(TipoInmueble.idTipo)),
								tipo = reader.GetString(nameof(TipoInmueble.tipo))
							}
						};
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
			var query = @"SELECT idInmueble, propietarioId, i.direccion, precio, disponibilidad,
			p.Nombre, p.Apellido, ti.idTipo, ti.tipo 
			FROM inmueble i INNER JOIN propietario p ON i.propietarioId = idPropietario
			INNER JOIN tipo_inmueble ti ON i.idTipo = ti.idTipo;";

			using (MySqlCommand command = new MySqlCommand(query, connection))
			{
				connection.Open();
				using (MySqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						Inmueble inmueble = new Inmueble
						{
							idInmueble = reader.GetInt32(nameof(Inmueble.idInmueble)),
							PropietarioId = reader.GetInt32(nameof(Inmueble.PropietarioId)),
							Direccion = reader.GetString("Direccion"),
							Precio = reader.GetInt32(nameof(Inmueble.Precio)),
							Disponibilidad = reader.GetBoolean("Disponibilidad"),
							PropietarioInmueble = new Propietario{
								idPropietario = reader.GetInt32(nameof(Inmueble.PropietarioId)),
								Nombre = reader.GetString("Nombre"),
								Apellido = reader.GetString("Apellido"),
							},
							TipoInmueble = new TipoInmueble{
								idTipo = reader.GetInt32(nameof(TipoInmueble.idTipo)),
								tipo = reader.GetString(nameof(TipoInmueble.tipo))
							}
						};
						listaInmuebles.Add(inmueble);
					}
				}
			}
			connection.Close();
		}
		return listaInmuebles;
	}

	public List<Inmueble> VerPropiedades(int idPropietario)
	{
		List<Inmueble> listaInmuebles = new List<Inmueble>();
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			var query = @"SELECT idInmueble, i.direccion, precio, disponibilidad,
			ti.idTipo, ti.tipo
			FROM inmueble i	INNER JOIN tipo_inmueble ti ON i.idTipo = ti.idTipo
			WHERE i.propietarioId = @idPropietario;";

			using (MySqlCommand command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@idPropietario", idPropietario);
				connection.Open();
				using (MySqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						Inmueble inmueble = new Inmueble
						{
							idInmueble = reader.GetInt32(nameof(Inmueble.idInmueble)),
							Direccion = reader.GetString("Direccion"),
							Precio = reader.GetInt32(nameof(Inmueble.Precio)),
							Disponibilidad = reader.GetBoolean("Disponibilidad"),
							TipoInmueble = new TipoInmueble{
								idTipo = reader.GetInt32(nameof(TipoInmueble.idTipo)),
								tipo = reader.GetString(nameof(TipoInmueble.tipo))
							}
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