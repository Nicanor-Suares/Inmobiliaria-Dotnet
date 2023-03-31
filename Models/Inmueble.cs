namespace Inmobiliaria_DotNet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

public class Inmueble
{
	public int idInmueble { get; set; }
	public bool Disponibilidad { get; set; }
	public int Precio { get; set; }
	
	public int PropietarioId { get; set; }
	[ForeignKey(nameof(PropietarioId))]
	public Propietario PropietarioInmueble { get; set; }
	public string Direccion { get; set; }

	public Inmueble()
	{
		this.idInmueble = 0;
		Disponibilidad = false;
		Precio = 0;
		PropietarioId = 0;
		PropietarioInmueble = null;
		Direccion = "";
	}
	public Inmueble(int idInmueble)
	{
		this.idInmueble = idInmueble;
		Disponibilidad = true;
		Precio = 0;
		PropietarioInmueble = null;
		Direccion = "";
	}

	public Inmueble(int idInmueble, int idPropietario, string direccion, int precio, bool disponibilidad, Propietario propietarioInmueble) {
		this.idInmueble = idInmueble;
		this.PropietarioId = idPropietario;
		this.Disponibilidad = disponibilidad;
		this.Precio = precio;
		this.PropietarioInmueble = propietarioInmueble;
		this.Direccion = direccion;
	}

   public Inmueble(bool disponibilidad, int precio, Propietario propietarioInmueble, string direccion) {
		this.Disponibilidad = disponibilidad;
		this.Precio = precio;
		this.PropietarioInmueble = propietarioInmueble;
		this.Direccion = "";
	}

}