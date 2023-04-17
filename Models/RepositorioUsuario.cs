using Inmobiliaria_DotNet.Models;
using MySql.Data.MySqlClient;

public class RepositorioUsuario
{
	string connectionString = "Server=localhost;Database=inmobiliaria;User=root;Password=;";

	public RepositorioUsuario() {}

	public int AltaUsuario(Usuario usuario)
	{
		int res = 0;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			String query = @"INSERT INTO usuario
			(nombre, apellido, email, password, avatar, rol)
			VALUES (@nombre, @apellido, @email, @password, @avatar, @rol);
			SELECT LAST_INSERT_ID();";

			using (var command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@nombre", usuario.nombre);
				command.Parameters.AddWithValue("@apellido", usuario.apellido);
				command.Parameters.AddWithValue("@email", usuario.email);
        command.Parameters.AddWithValue("@password", usuario.password);
				if (String.IsNullOrEmpty(usuario.avatar))
					command.Parameters.AddWithValue("@avatar", DBNull.Value);
				else
					command.Parameters.AddWithValue("@avatar", usuario.avatar);
				command.Parameters.AddWithValue("@rol", usuario.rol);

				connection.Open();
				res = Convert.ToInt32(command.ExecuteScalar());
				usuario.idUsuario = res;
				connection.Close();
			}
		}
		return res;
	}

	public int Eliminar(int id)
	{
		int res = 0;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			var query = @"DELETE FROM usuarios WHERE idUsuario = @id;";

			using (var command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@id", id);
				connection.Open();
				res = command.ExecuteNonQuery();
				connection.Close();
			}
		}
		return res;
	}

	public int Modificar(Usuario usuario)
	{
		int res = 0;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			String query = @"UPDATE usuarios
			SET nombre = @nombre, apellido = @apellido, email = @email, rol = @rol
			WHERE idUsuario = @id;";

			using (var command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@nombre", usuario.nombre);
				command.Parameters.AddWithValue("@apellido", usuario.apellido);
				command.Parameters.AddWithValue("@email", usuario.email);
				command.Parameters.AddWithValue("@rol", usuario.rol);
				command.Parameters.AddWithValue("@id", usuario.idUsuario);

				connection.Open();
				res = command.ExecuteNonQuery();
				connection.Close();
			}
		}
		return res;
	}

	public int ModificarContraseña(Usuario usuario)
	{
		int res = 0;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			String query = @"UPDATE usuario
			SET password = @password
			WHERE idUsuario = @id;";

			using (var command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@password", usuario.password);
				command.Parameters.AddWithValue("@id", usuario.idUsuario);

				connection.Open();
				res = command.ExecuteNonQuery();
				connection.Close();
			}
		}
		return res;
	}

	public int ModificarAvatar(Usuario usuario)
	{
		int res = 0;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			String query = @"UPDATE usuario
			SET avatar = @avatar
			WHERE idUsuario = @id;";

			using (var command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@avatar", usuario.avatar);
				command.Parameters.AddWithValue("@id", usuario.idUsuario);

				connection.Open();
				res = command.ExecuteNonQuery();
				connection.Close();
			}
		}
		return res;
	}

	public IList<Usuario> ListarUsuarios()
	{
		List<Usuario> listaUsuarios = new List<Usuario>();
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			var query = @"SELECT idUsuario, nombre, apellido, email, avatar, rol
			FROM usuario;";

			using (var command = new MySqlCommand(query, connection))
			{
				connection.Open();
				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var usuario = new Usuario
						{
							idUsuario = reader.GetInt32("idUsuario"),
							nombre = reader.GetString("nombre"),
							apellido = reader.GetString("apellido"),
							email = reader.GetString("email"),
							avatar = !reader.IsDBNull(reader.GetOrdinal("avatar")) ? reader.GetString("avatar") : null,
							rol = reader.GetInt32("rol")
						};
						listaUsuarios.Add(usuario);
					}
				}
				connection.Close();
			}
		}
		return listaUsuarios;
	}

	public Usuario BuscarUsuarioPorId(int id)
	{
		Usuario usuario = new Usuario();
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			var query = @"SELECT idUsuario, nombre, apellido, email, avatar, rol
			FROM usuario
			WHERE idUsuario = @id;";

			using (var command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@id", id);
				connection.Open();
				using (var reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						usuario.idUsuario = reader.GetInt32("idUsuario");
						usuario.nombre = reader.GetString("nombre");
						usuario.apellido = reader.GetString("apellido");
						usuario.email = reader.GetString("email");
						usuario.avatar = reader.IsDBNull(reader.GetOrdinal("avatar")) ? null : reader.GetString("avatar");
						usuario.rol = reader.GetInt32("rol");
					}
				}
				connection.Close();
			}
		}
		return usuario;
	}

	public Usuario BuscarUsuarioPorEmail(string email)
	{
		Usuario usuario = new Usuario();
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			var query = @"SELECT idUsuario, nombre, apellido, email, password, avatar, rol
			FROM usuario
			WHERE email = @email;";

			using (var command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@email", email);
				connection.Open();
				using (var reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						usuario.idUsuario = reader.GetInt32("idUsuario");
						usuario.nombre = reader.GetString("nombre");
						usuario.apellido = reader.GetString("apellido");
						usuario.email = reader.GetString("email");
						usuario.password = reader.GetString("password");
						usuario.avatar = reader.IsDBNull(reader.GetOrdinal("avatar")) ? null : reader.GetString("avatar");
						usuario.rol = reader.GetInt32("rol");
					}
				}
				connection.Close();
			}
		}
		return usuario;
	}
}