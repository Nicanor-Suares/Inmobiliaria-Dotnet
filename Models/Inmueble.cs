namespace Inmobiliaria_DotNet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

public class Inmueble
{
	[Display(Name = "Código Inmueble")]
	public int idInmueble { get; set; }
	public bool Disponibilidad { get; set; }
	public int Precio { get; set; }
	public int PropietarioId { get; set; }
	[ForeignKey(nameof(PropietarioId))]
	[Display(Name = "Propietario del Inmueble")]
	public Propietario PropietarioInmueble { get; set; }
	public string Direccion { get; set; }
	public int idTipo { get; set; }
	[ForeignKey(nameof(idTipo))]
	public TipoInmueble TipoInmueble { get; set; }


	public Inmueble()
	{
		this.idInmueble = 0;
		Disponibilidad = false;
		Precio = 0;
		PropietarioId = 0;
		PropietarioInmueble = null;
		Direccion = "";
		idTipo = 0;
		TipoInmueble = null;
	}
	public Inmueble(int idInmueble)
	{
		this.idInmueble = idInmueble;
		Disponibilidad = true;
		Precio = 0;
		PropietarioInmueble = null;
		Direccion = "";
		idTipo = 0;
		TipoInmueble = null;
	}

	public Inmueble(int idInmueble, int idPropietario, string direccion, int precio, bool disponibilidad, Propietario propietarioInmueble, int idTipo, TipoInmueble TipoInmueble) {
		this.idInmueble = idInmueble;
		this.PropietarioId = idPropietario;
		this.Disponibilidad = disponibilidad;
		this.Precio = precio;
		this.PropietarioInmueble = propietarioInmueble;
		this.Direccion = direccion;
		this.idTipo = idTipo;
		this.TipoInmueble = TipoInmueble;
	}

   public Inmueble(bool disponibilidad, int precio, Propietario propietarioInmueble, string direccion, int idTipo, TipoInmueble TipoInmueble) {
		this.Disponibilidad = disponibilidad;
		this.Precio = precio;
		this.PropietarioInmueble = propietarioInmueble;
		this.Direccion = "";
		this.idTipo = 0;
		this.TipoInmueble = TipoInmueble;
	}

	public override string ToString()
	{
			return $"{Direccion}";
	}

}