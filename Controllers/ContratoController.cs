using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_DotNet.Models;

namespace Inmobiliaria_DotNet.Controllers
{
	public class ContratoController : Controller
	{
		private readonly RepositorioContrato Repo;
		private readonly RepositorioInmueble repoInmueble;
		private readonly RepositorioInquilino repoInquilino;
		private readonly RepositorioPropietario repoPropietario;
		private readonly RepositorioPago repoPago;

		public ContratoController()
		{
			Repo = new RepositorioContrato();
			repoInmueble = new RepositorioInmueble();
			repoInquilino = new RepositorioInquilino();
			repoPropietario = new RepositorioPropietario();
			repoPago = new RepositorioPago();
		}
		// GET: Contrato
		public ActionResult Index()
		{
			var lista = Repo.ListarContratos();
			return View(lista);
		}

		public ActionResult VerVigentes()
		{
			var lista = Repo.ListarContratosVigentes();
			return View(lista);
		}

		// GET: Contrato/Details/5
		public ActionResult DetallesContrato(int id)
		{
			var contratoDetalles = Repo.BuscarContrato(id);
			return View(contratoDetalles);
		}

		// GET: Contrato/Create
		public ActionResult AltaContrato()
		{
			try
			{
				ViewBag.Inquilino = repoInquilino.ListarInquilinos();
				ViewBag.Inmueble = repoInmueble.ListarInmuebles();
				return View();
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		// POST: Contrato/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AltaContrato(Contrato contrato)
		{
			try
			{
				var result = Repo.AltaContrato(contrato);
				if (result == 0)
				{
						// If there is an overlap, add a model state error
						ModelState.AddModelError("", "Hay un contrato existente que se superpone en las fechas seleccionadas para esta propiedad.");
						ViewBag.Inquilino = repoInquilino.ListarInquilinos();
						ViewBag.Inmueble = repoInmueble.ListarInmuebles();
						return View(contrato);
				}
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				ViewBag.Inquilino = repoInquilino.ListarInquilinos();
				ViewBag.Inmueble = repoInmueble.ListarInmuebles();
				throw;
			}
		}

		// GET: Contrato/Edit/5
		public ActionResult EditarContrato(int id)
		{
			ViewBag.Inquilino = repoInquilino.ListarInquilinos();
			ViewBag.Inmueble = repoInmueble.ListarInmuebles();
			Contrato contratoEditar =  Repo.BuscarContrato(id);
			return View(contratoEditar);
		}
		// POST: Contrato/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditarContrato(int id, Contrato contrato)
		{
			try
			{
				contrato.idContrato = id;
				Repo.EditarContrato(contrato);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
			throw;
			//return View();
			}
		}

		// GET: Contrato/Delete/5
		[HttpGet]
		public ActionResult BorrarContrato(int id)
		{
			var contratoBorrar = Repo.BuscarContrato(id);
			return View(contratoBorrar);
		}

		// POST: Contrato/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult BorrarContrato(int id, IFormCollection collection)
		{
			try
			{
				// TODO: Add delete logic here
				Repo.BorrarContrato(id);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				throw;
				//return View();
			}
		}

		[HttpGet]
		public ActionResult VerPagosContrato(int id)
		{
			try
			{
				var pagos = repoPago.VerPagosContrato(id);
				return View(pagos);
			}
			catch
			{
				return View();
			}
		}

		// GET: Contrato/Edit/5
		[HttpGet]
		public ActionResult RenovarContrato(int id)
		{
			ViewBag.Inquilino = repoInquilino.ListarInquilinos();
			ViewBag.Inmueble = repoInmueble.ListarInmuebles();
			Contrato contratoRenovar =  Repo.BuscarContrato(id);
			contratoRenovar.FechaInicio = contratoRenovar.FechaFin.AddDays(1);
			contratoRenovar.FechaFin = contratoRenovar.FechaFin.AddYears(2);
			return View(contratoRenovar);
		}

		[HttpGet]
		public ActionResult RescindirContrato(int id)
		{
			ViewBag.Inquilino = repoInquilino.ListarInquilinos();
			ViewBag.Inmueble = repoInmueble.ListarInmuebles();
			Contrato contratoRescindir =  Repo.BuscarContrato(id);

			return View(contratoRescindir);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult RescindirContrato(int id, Contrato contrato)
		{
			try
			{
				Contrato contratoRescindir =  Repo.BuscarContrato(id);
				var pagos = repoPago.VerPagosContrato(id);

				contratoRescindir.idContrato = id;
				 var result = Repo.RescindirContrato(contratoRescindir, pagos);
				return RedirectToAction("MultaContrato", new {
				multa = result.Multa,
				idContrato = contratoRescindir.idContrato,
				pagosRealizados = result.PagosRealizados,
				pagosEsperados = result.PagosEsperados,
				pagosPendientes = result.PagosPendientes
        });
			}
			catch
			{
			throw;
			//return View();
			}
		}

    // Action that shows the penalty
		public ActionResult MultaContrato(int multa, int idContrato, int pagosRealizados, int pagosEsperados, bool pagosPendientes)
    {
			Contrato contrato = Repo.BuscarContrato(idContrato);
			ViewBag.Contrato = contrato;
			ViewBag.PagosRealizados = pagosRealizados;
			ViewBag.PagosEsperados = pagosEsperados;
			ViewBag.PagosPendientes = pagosPendientes;
			ViewBag.Multa = multa;

			return View();
    }
	}
}