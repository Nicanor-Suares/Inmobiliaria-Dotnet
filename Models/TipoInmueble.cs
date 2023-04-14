namespace Inmobiliaria_DotNet.Models;

public class TipoInmueble
{
  public int idTipo { get; set; }
  public string tipo { get; set; }

  public TipoInmueble() {
    this.idTipo = 0;
    this.tipo = "";
  }

  public TipoInmueble(int idTipo, string tipo) {
    this.idTipo = idTipo;
    this.tipo = tipo;
  }

  public override string ToString() {
    return $"{tipo}";
  }

}