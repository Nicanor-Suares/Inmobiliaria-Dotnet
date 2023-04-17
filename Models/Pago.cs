using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_DotNet.Models;
	public class Pago
	{
		[Display(Name = "Código Pago")]
		public int IdPago { get; set; }
		[Display(Name = "Código Contrato")]
		public int IdContrato { get; set; }
		[ForeignKey(nameof(IdContrato))]
		public Contrato ContratoPago { get; set; }
		[Display(Name = "Fecha de pago")]
		public DateTime FechaPago { get; set; }
		public int Monto { get; set; }
		[Display(Name = "Pago número:")]
		public int nroPago { get; set; }

		public Pago(){
			IdPago = 0;
			IdContrato = 0;
			ContratoPago = null;
			FechaPago = DateTime.Now;
			Monto = 0;
			nroPago = 0;
		}

		public Pago(int idPago)
		{
			IdPago = idPago;
			IdContrato = 0;
			ContratoPago = null;
			FechaPago = DateTime.Now;
			Monto = 0;
			nroPago = 0;
		}

		public Pago(int idPago, int idContrato, Contrato contrato, DateTime fechaPago, int monto, int nroPago){
			this.IdPago = idPago;
			this.IdContrato = idContrato;
			this.ContratoPago = contrato;
			this.FechaPago = fechaPago;
			this.Monto = monto;
			this.nroPago = nroPago;
		}

		public Pago(int idContrato, Contrato contrato, DateTime fechaPago, int monto, int nroPago){
			this.IdContrato = idContrato;
			this.ContratoPago = contrato;
			this.FechaPago = fechaPago;
			this.Monto = monto;
			this.nroPago = nroPago;
		}

	}