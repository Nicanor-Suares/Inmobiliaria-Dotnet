using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_DotNet.Models;

namespace Inmobiliaria_DotNet.Controllers
{
	public class PagoController : Controller
	{
		private readonly RepositorioPago Repo;
		private readonly RepositorioInmueble repoInmueble;
		private readonly RepositorioContrato repoContrato;
		
		public PagoController()
		{
			Repo = new RepositorioPago();
			repoInmueble = new RepositorioInmueble();
			repoContrato = new RepositorioContrato();
		}

		// GET: Pago
		public ActionResult Index()
		{
			var lista = Repo.ListarPagos();
			return View(lista);
		}

		// GET: Pago/Details/5
		public ActionResult DetallesPago(int id)
		{
			var detallesPago = Repo.BuscarPago(id);
			return View(detallesPago);
		}

		// GET: Pago/Create
		public ActionResult AltaPago()
		{
			ViewBag.Contrato = repoContrato.ListarContratos();
			return View();
		}

		// POST: Pago/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AltaPago(Pago pago)
		{
			try
			{
				// TODO: Add insert logic here
				Repo.AltaPago(pago);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				throw;
				//return View();
			}
		}

		// GET: Pago/Edit/5
		public ActionResult EditarPago(int id)
		{
			ViewBag.Contrato = repoContrato.ListarContratos();
			Pago pagoEditar = Repo.BuscarPago(id);
			return View(pagoEditar);
		}

		// POST: Pago/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditarPago(int id, Pago pago)
		{
			try
			{
				// TODO: Add update logic here
				pago.IdPago = id;
				Repo.EditarPago(pago);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				throw;
				//return View();
			}
		}

		// GET: Pago/Delete/5
		public ActionResult BorrarPago(int id)
		{
			ViewBag.Contrato = repoContrato.BuscarContrato(id);
			var pagoBorrar = Repo.BuscarPago(id);
			return View(pagoBorrar);
		}

		// POST: Pago/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult BorrarPago(int id, IFormCollection collection)
		{
			try
			{
				// TODO: Add delete logic here
				Repo.BorrarPago(id);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				throw;
				//return View();
			}
		}
	}
}