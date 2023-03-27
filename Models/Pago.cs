namespace Inmobiliaria_DotNet.Models;
  public class Pago
  {
    public int IdPago { get; set; }
    public int IdContrato { get; set; }
    public DateTime FechaPago { get; set; }
    public int Monto { get; set; }

    public Pago(int idPago)
    {
      IdPago = idPago;
      IdContrato = 0;
      FechaPago = DateTime.Now;
      Monto = 0;
    }

    public Pago(int idPago, int idContrato, DateTime fechaPago, int monto){
      this.IdPago = idPago;
      this.IdContrato = idContrato;
      this.FechaPago = fechaPago;
      this.Monto = monto;
    }

    public Pago(int idContrato, DateTime fechaPago, int monto){
      this.IdContrato = idContrato;
      this.FechaPago = fechaPago;
      this.Monto = monto;
    }

  }