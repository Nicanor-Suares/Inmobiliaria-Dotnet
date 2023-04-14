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

		public ContratoController()
		{
				Repo = new RepositorioContrato();
				repoInmueble = new RepositorioInmueble();
				repoInquilino = new RepositorioInquilino();
				repoPropietario = new RepositorioPropietario();
		}
		// GET: Contrato
		public ActionResult Index()
		{
				var lista = Repo.ListarContratos();
				return View(lista);
		}

		// GET: Contrato/Details/5
		public ActionResult Details(int id)
		{
				return View();
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
						// TODO: Add insert logic here
						Repo.AltaContrato(contrato);
						return RedirectToAction(nameof(Index));
				}
				catch
				{
						return View();
				}
		}

		// GET: Contrato/Edit/5
		public ActionResult EditarContrato(int id)
		{
				return View();
		}
		// POST: Contrato/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditarContrato(int id, Contrato contrato)
		{
				try
				{
						// TODO: Add update logic here
						contrato.idContrato = id;
						Repo.EditarContrato(contrato);
						return RedirectToAction(nameof(Index));
				}
				catch
				{
						return View();
				}
		}

		// GET: Contrato/Delete/5
		public ActionResult Delete(int id)
		{
				return View();
		}

		// POST: Contrato/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
				try
				{
						// TODO: Add delete logic here

						return RedirectToAction(nameof(Index));
				}
				catch
				{
						return View();
				}
		}
	}
}