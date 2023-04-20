using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_DotNet.Models;
public class Inquilino
{
	[Display(Name = "Código Inquilino")]
	public int idInquilino { get; set; }
	public string ? Nombre { get; set; }
	public string ? Apellido { get; set; }
	public string ? Dni { get; set; }
	public string ? Telefono { get; set; }

	public Inquilino(){
		idInquilino = 0;
		Nombre = null;
		Apellido = null;
		Dni = null;
		Telefono = null;
	}
	public Inquilino(int idInquilino)
	{
		this.idInquilino = idInquilino;
		Nombre = "";
		Apellido = "";
		Dni = "";
		Telefono = "";
	}

	public Inquilino(int idInquilino, string nombre, string apellido, string dni, string telefono) {
		this.idInquilino = idInquilino;
		this.Nombre = nombre;
		this.Apellido = apellido;
		this.Dni = dni;
		this.Telefono = telefono;
	}

	public Inquilino(string nombre, string apellido, string dni, string telefono) {
		this.idInquilino = idInquilino;
		this.Nombre = nombre;
		this.Apellido = apellido;
		this.Dni = dni;
		this.Telefono = telefono;
	}
	
		public override string ToString()
		{
				return $"{Apellido}, {Nombre}";
		}
}