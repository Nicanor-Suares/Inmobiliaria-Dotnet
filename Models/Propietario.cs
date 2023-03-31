namespace Inmobiliaria_DotNet.Models;
public class Propietario
{
	public int idPropietario { get; set; }
	public string ? Nombre { get; set; }
	public string ? Apellido { get; set; }
	public string Dni { get; set; }
	public string ? Direccion { get; set; }
	public string ? Telefono { get; set; }

	public Propietario()
	{
		this.idPropietario = 0;
		Nombre = "";
		Apellido = "";
		Dni = "";
		Direccion = "";
		Telefono = "";
	}
	
	public Propietario(int idPropietario)
	{
		this.idPropietario = idPropietario;
		Nombre = "";
		Apellido = "";
		Dni = "";
		Direccion = "";
		Telefono = "";
	}

	public Propietario(int idPropietario, string nombre, string apellido, string dni, string direccion, string telefono) {
		this.idPropietario = idPropietario;
		this.Nombre = nombre;
		this.Apellido = apellido;
		this.Dni = dni;
		this.Direccion = direccion;
		this.Telefono = telefono;
	}

   public Propietario(string nombre, string apellido, string dni, string direccion, string telefono) {
		this.idPropietario = idPropietario;
		this.Nombre = nombre;
		this.Apellido = apellido;
		this.Dni = dni;
		this.Direccion = direccion;
		this.Telefono = telefono;
	}

  public override string ToString()
  {
    return $"{Nombre} {Apellido}";
  }


}