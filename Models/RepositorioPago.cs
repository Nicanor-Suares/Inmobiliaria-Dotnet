using MySql.Data.MySqlClient;

namespace Inmobiliaria_DotNet.Models;

public class RepositorioPago
{
	string connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;";

	public int AltaPago(Pago pago)
	{
		int res = 0;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			String query = @"INSERT INTO pago
			(idContrato, fechaPago, monto, nroPago)
			VALUES (@IdContrato, @fechaPago, @monto, @nroPago);
			SELECT LAST_INSERT_ID();";

			using (var command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@idContrato", pago.IdContrato);
				command.Parameters.AddWithValue("@fechaPago", pago.FechaPago);
				command.Parameters.AddWithValue("@monto", pago.Monto);
				command.Parameters.AddWithValue("@nroPago", pago.nroPago);
				connection.Open();
				res = Convert.ToInt32(command.ExecuteScalar());
				pago.IdPago = res;
				connection.Close();
			}
      connection.Close();
		}
		return res;
	}

	public int BorrarPago(int idPago)
	{
		int res = 0;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			var query = @"DELETE FROM pago WHERE idPago = @idPago;";
			using (var command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@idPago", idPago);
				connection.Open();
				res = command.ExecuteNonQuery();
				connection.Close();
			}
      connection.Close();
		}
		return res;
	}

	public int EditarPago(Pago pago)
	{
		int res = 0;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			var query = @"UPDATE pago
			SET idContrato = @idContrato, fechaPago = @fechaPago, monto = @monto, nroPago = @nroPago
			WHERE idPago = @idPago;";

			using (var command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@idPago", pago.IdPago);
				command.Parameters.AddWithValue("@idContrato", pago.IdContrato);
				command.Parameters.AddWithValue("@fechaPago", pago.FechaPago);
				command.Parameters.AddWithValue("@monto", pago.Monto);
				command.Parameters.AddWithValue("@nroPago", pago.nroPago);
				connection.Open();
				res = command.ExecuteNonQuery();
				connection.Close();
			}
      connection.Close();
		}
		return res;
	}

	public List<Pago> ListarPagos()
	{
		List<Pago> listaPagos = new List<Pago>();
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			var query = @"SELECT idPago, p.idContrato, fechaPago, p.monto AS montoPago, nroPago, i.direccion, inq.nombre, inq.apellido
			FROM pago p
			INNER JOIN contrato c ON p.idContrato = c.idContrato
			INNER JOIN inmueble i ON c.idInmueble = i.idInmueble
			INNER JOIN inquilino inq ON c.idInquilino = inq.idInquilino;";

			using (var command = new MySqlCommand(query, connection))
			{
				connection.Open();
				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					listaPagos.Add(new Pago
					{
						IdPago = Convert.ToInt32(reader["idPago"]),
						IdContrato = Convert.ToInt32(reader["idContrato"]),
						ContratoPago = new Contrato
						{
							InmuebleContrato = new Inmueble
							{
								Direccion = reader.GetString("direccion"),
							},
							InquilinoContrato = new Inquilino
							{
								Nombre = reader.GetString("nombre"),
								Apellido = reader.GetString("apellido"),
							}
						},
						FechaPago = Convert.ToDateTime(reader["fechaPago"]),
						Monto = Convert.ToInt32(reader["montoPago"]),
						nroPago = Convert.ToInt32(reader["nroPago"]),
					});
				}
			}
			connection.Close();
		}
		return listaPagos;
	}
	public Pago BuscarPago(int id)
	{
		Pago res = null;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			var query = @"SELECT idPago, p.idContrato, fechaPago, p.monto AS montoPago, nroPago, i.direccion, i.precio, inq.nombre, inq.apellido
			FROM pago p
			INNER JOIN contrato c ON p.idContrato    = c.idContrato
			INNER JOIN inmueble i ON c.idInmueble    = i.idInmueble
			INNER JOIN inquilino inq ON c.idInquilino = inq.idInquilino
			WHERE idPago = @id;";

			using (var command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@id", id);
				connection.Open();
				using (var reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						res = new Pago
						{
							IdPago = Convert.ToInt32(reader["idPago"]),
							IdContrato = Convert.ToInt32(reader["idContrato"]),
							ContratoPago = new Contrato
							{
								idContrato = Convert.ToInt32(reader["idContrato"]),
								InmuebleContrato = new Inmueble
								{
									Direccion = reader.GetString("direccion"),
                  Precio = Convert.ToInt32(reader["precio"]),
								},
								InquilinoContrato = new Inquilino
								{
									Nombre = reader.GetString("nombre"),
									Apellido = reader.GetString("apellido"),
								}
							},
							FechaPago = Convert.ToDateTime(reader["fechaPago"]),
							Monto = Convert.ToInt32(reader["montoPago"]),
							nroPago = Convert.ToInt32(reader["nroPago"]),
						};
					}
				}
			}
			connection.Close();
		}
		return res;
	}

	public int ObtenerUltimoPago(int idContrato)
	{
		int res = 0;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			var query = @"SELECT COALESCE(MAX(nroPago), 0) AS ultimo_pago
			FROM pago
			WHERE idContrato = @idContrato;";

			using (var command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@idContrato", idContrato);
				connection.Open();
				using (var reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						res = reader.GetInt32("ultimo_pago");
					}
				}
				connection.Close();
			}
		}
		return res;
	}

	public List<Pago> VerPagosContrato(int idContrato)
	{
		List<Pago> listaPagos = new List<Pago>();
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			var query = @"SELECT idPago, p.idContrato, fechaPago, p.monto AS montoPago, nroPago, i.direccion, inq.nombre, inq.apellido
			FROM pago p
			INNER JOIN contrato c ON p.idContrato = c.idContrato
			INNER JOIN inmueble i ON c.idInmueble = i.idInmueble
			INNER JOIN inquilino inq ON c.idInquilino = inq.idInquilino
			WHERE p.idContrato = @idContrato;";

			using (var command = new MySqlCommand(query, connection))
			{
				connection.Open();
				command.Parameters.AddWithValue("@idContrato", idContrato);
				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					listaPagos.Add(new Pago
					{
						IdPago = Convert.ToInt32(reader["idPago"]),
						IdContrato = Convert.ToInt32(reader["idContrato"]),
						ContratoPago = new Contrato
						{
							InmuebleContrato = new Inmueble
							{
								Direccion = reader.GetString("direccion"),
							},
							InquilinoContrato = new Inquilino
							{
								Nombre = reader.GetString("nombre"),
								Apellido = reader.GetString("apellido"),
							}
						},
						FechaPago = Convert.ToDateTime(reader["fechaPago"]),
						Monto = Convert.ToInt32(reader["montoPago"]),
						nroPago = Convert.ToInt32(reader["nroPago"]),
					});
				}
			}
			connection.Close();
		}
		return listaPagos;
	}
}