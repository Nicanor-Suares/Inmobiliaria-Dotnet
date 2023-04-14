using MySql.Data.MySqlClient;

namespace Inmobiliaria_DotNet.Models;

public class RepositorioTipoInmueble {
	string connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;";

	public List<TipoInmueble> listarTiposInmuebles() 
	{
	 List<TipoInmueble> listaTipos = new List<TipoInmueble>(); 
	 using (MySqlConnection connection = new MySqlConnection(connectionString)) 
	 {
		string query = @"SELECT idTipo, tipo FROM tipo_inmueble";
		using (MySqlCommand command = new MySqlCommand(query, connection)) 
		{
		 connection.Open();
		 using (MySqlDataReader reader = command.ExecuteReader())
		 {       
			while (reader.Read()) {
				TipoInmueble tipoInmueble = new TipoInmueble
				{
					idTipo = reader.GetInt32(nameof(tipoInmueble.idTipo)),
					tipo = reader.GetString(nameof(tipoInmueble.tipo))
				};
				listaTipos.Add(tipoInmueble);
			}
		 }
		}
		 connection.Close();
	 }
	 return listaTipos;
	}

}