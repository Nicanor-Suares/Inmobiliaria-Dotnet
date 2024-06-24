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
				var query = @"INSERT INTO contrato (fechaInicio, fechaFin, monto, idInquilino, idInmueble, activo)
				VALUES (@FechaInicio, @FechaFin, @Monto, @InquilinoId, @InmuebleId, @Activo);
				SELECT LAST_INSERT_ID();";
				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@FechaInicio", contrato.FechaInicio);
					command.Parameters.AddWithValue("@FechaFin", contrato.FechaFin);
					command.Parameters.AddWithValue("@Monto", contrato.Monto);
					command.Parameters.AddWithValue("@InquilinoId", contrato.InquilinoId);
					command.Parameters.AddWithValue("@InmuebleId", contrato.InmuebleId);
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
				//FIX
				var query = @"UPDATE contrato SET fechaInicio = @FechaInicio, fechaFin = @FechaFin,
				monto = @Monto, activo = @Activo WHERE idContrato = @idContrato";
				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@fechaInicio", contrato.FechaInicio);
					command.Parameters.AddWithValue("@fechaFin", contrato.FechaFin);
					command.Parameters.AddWithValue("@monto", contrato.Monto);
					command.Parameters.AddWithValue("@activo", contrato.Activo);
					command.Parameters.AddWithValue("@idContrato", contrato.idContrato);
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
				var query = @"SELECT idContrato, fechaInicio, fechaFin, monto,
				idInquilino, idInmueble, activo FROM contrato
				WHERE idContrato = @idContrato";
				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@idContrato", idContrato);
					connection.Open();
					using (var reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							res = new Contrato
							{
								idContrato = reader.GetInt32(nameof(Contrato.idContrato)),
								FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio)),
								FechaFin = reader.GetDateTime(nameof(Contrato.FechaFin)),
								Monto = reader.GetInt32(nameof(Contrato.Monto)),
								InquilinoId = reader.GetInt32("idInquilino"),
								InmuebleId = reader.GetInt32("idInmueble"),
								Activo = reader.GetBoolean(nameof(Contrato.Activo))
							};
						}
					connection.Close();
					}
				}
				connection.Close();
			}
			return res;
		}

		public int BorrarContrato(int idContrato){
			int res = 0;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				var query = @"DELETE FROM contrato WHERE idContrato = @idContrato";
				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@idContrato", idContrato);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
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
				var query = @"SELECT idContrato, fechaInicio, fechaFin, monto,
				c.idInquilino, c.idInmueble, i.Nombre, i.apellido, inmu.propietarioId, inmu.direccion, activo, p.Nombre AS nombreProp, p.Apellido AS apellidoProp
				FROM contrato c
				INNER JOIN inquilino i ON c.idInquilino = i.idInquilino
				INNER JOIN inmueble inmu ON c.idInmueble = inmu.idInmueble
				INNER JOIN propietario p on inmu.propietarioId = p.idPropietario
				";
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
								Monto = reader.GetInt32(nameof(contrato.Monto)),
								InquilinoId = reader.GetInt32("idInquilino"),
								InmuebleId = reader.GetInt32("idInmueble"),
								Activo = reader.GetBoolean(nameof(contrato.Activo)),
								InquilinoContrato = new Inquilino
								{
									Nombre = reader.GetString(nameof(contrato.InquilinoContrato.Nombre)),
									Apellido = reader.GetString(nameof(contrato.InquilinoContrato.Apellido))
								},
								InmuebleContrato = new Inmueble
								{
									Direccion = reader.GetString(nameof(contrato.InmuebleContrato.Direccion)),
									PropietarioInmueble = new Propietario {
										Nombre = reader.GetString("nombreProp"),
										Apellido = reader.GetString("apellidoProp")
									}
								}
							};
							listaContratos.Add(contrato);
						}
					connection.Close();
					}
				}
			}
			return listaContratos;
		}

		public List<Contrato> ListarContratosVigentes()
		{
			List<Contrato> listaContratos = new List<Contrato>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				var query = @"SELECT idContrato, fechaInicio, fechaFin, monto,
				c.idInquilino, c.idInmueble, i.Nombre, i.apellido, inmu.propietarioId, inmu.direccion, activo, p.Nombre AS nombreProp, p.Apellido AS apellidoProp
				FROM contrato c
				INNER JOIN inquilino i ON c.idInquilino = i.idInquilino
				INNER JOIN inmueble inmu ON c.idInmueble = inmu.idInmueble
				INNER JOIN propietario p on inmu.propietarioId = p.idPropietario
				WHERE c.activo = 1
				";
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
								Monto = reader.GetInt32(nameof(contrato.Monto)),
								InquilinoId = reader.GetInt32("idInquilino"),
								InmuebleId = reader.GetInt32("idInmueble"),
								Activo = reader.GetBoolean(nameof(contrato.Activo)),
								InquilinoContrato = new Inquilino
								{
									Nombre = reader.GetString(nameof(contrato.InquilinoContrato.Nombre)),
									Apellido = reader.GetString(nameof(contrato.InquilinoContrato.Apellido))
								},
								InmuebleContrato = new Inmueble
								{
									Direccion = reader.GetString(nameof(contrato.InmuebleContrato.Direccion)),
									PropietarioInmueble = new Propietario {
										Nombre = reader.GetString("nombreProp"),
										Apellido = reader.GetString("apellidoProp")
									}
								}
							};
							listaContratos.Add(contrato);
						}
					connection.Close();
					}
				}
			}
			return listaContratos;
		}

		public List<Contrato> VerContratosInmueble(int idInmueble)
		{
			List<Contrato> listaContratos = new List<Contrato>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				var query = @"SELECT idContrato, fechaInicio, fechaFin, monto,
				c.idInquilino, c.idInmueble, i.Nombre, i.apellido, inmu.propietarioId, inmu.direccion, activo, p.Nombre AS nombreProp, p.Apellido AS apellidoProp
				FROM contrato c
				INNER JOIN inquilino i ON c.idInquilino = i.idInquilino
				INNER JOIN inmueble inmu ON c.idInmueble = inmu.idInmueble
				INNER JOIN propietario p on inmu.propietarioId = p.idPropietario
				WHERE c.idInmueble = @idInmueble
				";
				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@idInmueble", idInmueble);
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
								Monto = reader.GetInt32(nameof(contrato.Monto)),
								InquilinoId = reader.GetInt32("idInquilino"),
								InmuebleId = reader.GetInt32("idInmueble"),
								Activo = reader.GetBoolean(nameof(contrato.Activo)),
								InquilinoContrato = new Inquilino
								{
									Nombre = reader.GetString(nameof(contrato.InquilinoContrato.Nombre)),
									Apellido = reader.GetString(nameof(contrato.InquilinoContrato.Apellido))
								},
								InmuebleContrato = new Inmueble
								{
									Direccion = reader.GetString(nameof(contrato.InmuebleContrato.Direccion)),
									PropietarioInmueble = new Propietario {
										Nombre = reader.GetString("nombreProp"),
										Apellido = reader.GetString("apellidoProp")
									}
								}
							};
							listaContratos.Add(contrato);
						}
					connection.Close();
					}
				}
			}
			return listaContratos;
		}

		public ControlContrato RescindirContrato(Contrato contrato, List<Pago> pagos)
		{
    	// Calcular si ha pasado más de la mitad de la duración del contrato
			TimeSpan duracionContrato = contrato.FechaFin - contrato.FechaInicio;
    	TimeSpan mitadDuracion = TimeSpan.FromTicks(duracionContrato.Ticks / 2);
    	bool masDeLaMitad = DateTime.Now - contrato.FechaInicio > mitadDuracion;

    	// Calcular la multa
			int multa = 0;
			if(masDeLaMitad) {
				multa = contrato.Monto;
			} else {
				multa = contrato.Monto * 2;
			}

			int mesesContrato = (int)Math.Ceiling(duracionContrato.TotalDays / 30.436875); // Cantidad de meses aproximados en el contrato
			int pagosEsperados = mesesContrato;

			// Obtener cantidad de pagos hasta la fecha
			int pagosRealizados = pagos.Count;

			// Chequear si faltan pagos
			bool pagosPendientes = pagosRealizados < pagosEsperados;

			contrato.FechaFin = DateTime.Now;
			contrato.Activo = false;

			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				//FIX
				var query = @"UPDATE contrato SET fechaFin = @FechaFin,
										activo = @Activo WHERE idContrato = @idContrato";
				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@fechaFin", contrato.FechaFin);
					command.Parameters.AddWithValue("@activo", contrato.Activo);
					command.Parameters.AddWithValue("@idContrato", contrato.idContrato);
					connection.Open();
					command.ExecuteNonQuery();
					connection.Close();
				}
				connection.Close();
			}
			return new ControlContrato(multa, pagosRealizados, pagosEsperados, pagosPendientes);
		}

	}