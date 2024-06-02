namespace Inmobiliaria_DotNet.Models;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


	public class Contrato
	{
		[Display(Name = "Código Contrato")]
		public int idContrato { get; set; }
		[Display(Name = "Fecha de Inicio")]
		public DateTime FechaInicio { get; set; }
		[Display(Name = "Fecha de Finalización")]
		public DateTime FechaFin { get; set; }
		[Display(Name = "Código Inmueble")]
		public int InmuebleId { get; set; }
		[ForeignKey(nameof(InmuebleId))]
		public Inmueble InmuebleContrato { get; set; }
		[Display(Name = "Código Inquilino")]
		public int InquilinoId { get; set; }
		[ForeignKey(nameof(InquilinoId))]
		[Display(Name = "Inquilino")]
		public Inquilino InquilinoContrato { get; set; }
		public bool Activo { get; set; }

		public Contrato(){
			this.idContrato = 0;
			FechaInicio = DateTime.Now;
			FechaFin = DateTime.Now;
			InmuebleId = 0;
			InquilinoId = 0;
			Activo = true;
		}
		public Contrato(int idContrato)
		{
			this.idContrato = idContrato;
			this.FechaInicio = DateTime.Now;
			this.FechaFin = DateTime.Now;
			this.InmuebleId = 0;
			this.InquilinoId = 0;
			this.Activo = true;
		}
		public Contrato(int idContrato, DateTime fechaInicio, DateTime fechaFin, int InmuebleId, int InquilinoId, bool activo)
		{
			this.idContrato = idContrato;
			FechaInicio = fechaInicio;
			FechaFin = fechaFin;
			InmuebleId = InmuebleId;
			InquilinoId = InquilinoId;
			Activo = activo;
		}
		public Contrato(DateTime fechaInicio, DateTime fechaFin, int InmuebleId, int InquilinoId, bool activo)
		{
			FechaInicio = fechaInicio;
			FechaFin = fechaFin;
			InmuebleId = InmuebleId;
			InquilinoId = InquilinoId;
			Activo = activo;
		}

		public override string ToString()
		{
			return $"Código: {idContrato} Inmueble: {InmuebleContrato.Direccion} Inquilino: {InquilinoContrato.Nombre} {InquilinoContrato.Apellido}";
		}

	}
