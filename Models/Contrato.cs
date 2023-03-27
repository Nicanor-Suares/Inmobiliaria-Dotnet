namespace Inmobiliaria_DotNet.Models;
  public class Contrato
  {
    public int idContrato { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public int IdInmueble { get; set; }
    public int IdInquilino { get; set; }
    public int IdPropietario { get; set; }
    public bool Activo { get; set; }

    public Contrato(){
      this.idContrato = 0;
      FechaInicio = DateTime.Now;
      FechaFin = DateTime.Now;
      IdInmueble = 0;
      IdInquilino = 0;
      IdPropietario = 0;
      Activo = true;
    }
    public Contrato(int idContrato)
    {
      this.idContrato = idContrato;
      this.FechaInicio = DateTime.Now;
      this.FechaFin = DateTime.Now;
      this.IdInmueble = 0;
      this.IdInquilino = 0;
      this.IdPropietario = 0;
      this.Activo = true;
    }
    public Contrato(int idContrato, DateTime fechaInicio, DateTime fechaFin, int idInmueble, int idInquilino, int idPropietario, bool activo)
    {
      this.idContrato = idContrato;
      FechaInicio = fechaInicio;
      FechaFin = fechaFin;
      IdInmueble = idInmueble;
      IdInquilino = idInquilino;
      IdPropietario = idPropietario;
      Activo = activo;
    }
    public Contrato(DateTime fechaInicio, DateTime fechaFin, int idInmueble, int idInquilino, int idPropietario, bool activo)
    {
      FechaInicio = fechaInicio;
      FechaFin = fechaFin;
      IdInmueble = idInmueble;
      IdInquilino = idInquilino;
      IdPropietario = idPropietario;
      Activo = activo;
    }

  }
    