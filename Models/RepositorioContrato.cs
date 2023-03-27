using MySql.Data.MySqlClient;

namespace Inmobiliaria_DotNet.Models;
	public class RepositorioContrato
	{
		string connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;";

		public int AltaContrato(Contrato contrato)
		{
			int res = 0;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				var query = @"INSERT INTO contrato (fechaInicio, fechaFin, idInquilino, idPropietario, idInmueble, activo)
				VALUES (@FechaInicio, @FechaFin, @IdInquilino, @IdPropietario, @IdInmueble, @Activo);
				SELECT LAST_INSERT_ID();";
				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@FechaInicio", contrato.FechaInicio);
					command.Parameters.AddWithValue("@FechaFin", contrato.FechaFin);
					command.Parameters.AddWithValue("@IdInquilino", contrato.IdInquilino);
					command.Parameters.AddWithValue("@IdPropietario", contrato.IdPropietario);
					command.Parameters.AddWithValue("@IdInmueble", contrato.IdInmueble);
					command.Parameters.AddWithValue("@Activo", contrato.Activo);
					connection.Open();
					res = Convert.ToInt32(command.ExecuteScalar());
					connection.Close();
				}
				connection.Close();
			}
			return res;
		}

		public int EditarContrato(Contrato contrato)
		{
			int res = 0;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				var query = @"UPDATE contrato SET fechaInicio = @FechaInicio, fechaFin = @FechaFin, 
				idInquilino = @IdInquilino, idPropietario = @IdPropietario, idInmueble = @IdInmueble, 
				activo = @Activo WHERE idContrato = @IdContrato";
				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@fechaInicio", contrato.FechaInicio);
					command.Parameters.AddWithValue("@fechaFin", contrato.FechaFin);
					command.Parameters.AddWithValue("@idInquilino", contrato.IdInquilino);
					command.Parameters.AddWithValue("@idPropietario", contrato.IdPropietario);
					command.Parameters.AddWithValue("@idInmueble", contrato.IdInmueble);
					command.Parameters.AddWithValue("@activo", contrato.Activo);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
				connection.Close();
			}
			return res;
		}

		public Contrato BuscarContrato(int idContrato)
		{
			Contrato res = null;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				var query = @"SELECT idContrato, fechaInicio, fechaFin, 
				idInquilino, idPropietario, idInmueble, activo FROM contrato 
				WHERE idContrato = @IdContrato";
				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@IdContrato", 1);
					connection.Open();
					using (var reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							res = new Contrato(
								Convert.ToInt32(reader["idContrato"]),
								Convert.ToDateTime(reader["fechaInicio"]),
								Convert.ToDateTime(reader["fechaFin"]),
								Convert.ToInt32(reader["idInmueble"]),
								Convert.ToInt32(reader["idInquilino"]),
								Convert.ToInt32(reader["idPropietario"]),
								Convert.ToBoolean(reader["activo"])
							);
						}
					connection.Close();
					}
				}
				connection.Close();
			}
			return res;
		}

		public List<Contrato> ListarContratos()
		{
			List<Contrato> listaContratos = new List<Contrato>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				var query = @"SELECT idContrato, fechaInicio, fechaFin, 
				idInquilino, idPropietario, idInmueble, activo FROM contrato";
				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					connection.Open();
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Contrato contrato = new Contrato
							{
								idContrato = reader.GetInt32(nameof(contrato.idContrato)),
								FechaInicio = reader.GetDateTime(nameof(contrato.FechaInicio)),
								FechaFin = reader.GetDateTime(nameof(contrato.FechaFin)),
								IdInquilino = reader.GetInt32(nameof(contrato.IdInquilino)),
								IdPropietario = reader.GetInt32(nameof(contrato.IdPropietario)),
								IdInmueble = reader.GetInt32(nameof(contrato.IdInmueble)),
								Activo = reader.GetBoolean(nameof(contrato.Activo))
							};
							listaContratos.Add(contrato);
						}
					connection.Close();
					}
				}
			}
			return listaContratos;
		}


	}