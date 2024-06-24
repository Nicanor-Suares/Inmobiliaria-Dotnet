namespace Inmobiliaria_DotNet.Models;

    public class ControlContrato
    {
      public int Multa { get; set; }
      public int PagosRealizados { get; set; }
      public int PagosEsperados { get; set; }
      public bool PagosPendientes { get; set; }

      public ControlContrato(int multa, int pagosRealizados, int pagosEsperados, bool pagosPendientes)
      {
        Multa = multa;
        PagosRealizados = pagosRealizados;
        PagosEsperados = pagosEsperados;
        PagosPendientes = pagosPendientes;
      }

    }

