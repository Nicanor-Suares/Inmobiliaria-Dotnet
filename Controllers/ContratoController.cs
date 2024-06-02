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
				Repo.AltaContrato(contrato);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				ViewBag.Inquilino = repoInquilino.ListarInquilinos();
				ViewBag.Inmueble = repoInmueble.ListarInmuebles();
				throw;
				//return View();
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
	}
}