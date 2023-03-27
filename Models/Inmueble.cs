namespace Inmobiliaria_DotNet.Models;

public class Inmueble
{
	public int idInmueble { get; set; }
	public bool Disponibilidad { get; set; }
	public int Precio { get; set; }
	public int PropietarioInmueble { get; set; }
	public string Direccion { get; set; }

	public Inmueble()
	{
		this.idInmueble = 0;
		Disponibilidad = false;
		Precio = 0;
		PropietarioInmueble = 0;
		Direccion = "";
	}
	public Inmueble(int idInmueble)
	{
		this.idInmueble = idInmueble;
		Disponibilidad = true;
		Precio = 0;
		PropietarioInmueble = 0;
		Direccion = "";
	}

	public Inmueble(int idInmueble, bool disponibilidad, int precio, int propietarioInmueble, string direccion) {
		this.idInmueble = idInmueble;
		this.Disponibilidad = disponibilidad;
		this.Precio = precio;
		this.PropietarioInmueble = propietarioInmueble;
		this.Direccion = direccion;
	}

   public Inmueble(bool disponibilidad, int precio, int propietarioInmueble, string direccion) {
		this.Disponibilidad = disponibilidad;
		this.Precio = precio;
		this.PropietarioInmueble = propietarioInmueble;
		this.Direccion = "";
	}

}