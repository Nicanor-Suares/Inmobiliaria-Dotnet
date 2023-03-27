using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_DotNet.Models;

namespace Inmobiliaria_DotNet.Controllers
{
	public class InquilinoController : Controller
	{
		private readonly RepositorioInquilino Repo;

		public InquilinoController() {
			Repo = new RepositorioInquilino();
		}
		// GET: Inquilino
		public ActionResult Index()
		{
			var lista = Repo.ListarInquilinos();
			return View(lista);
		}

		// GET: Inquilino/Details/5
		public ActionResult DetallesInquilino(int id)
		{
			var inquiDetalles = Repo.BuscarInquilino(id);
			return View(inquiDetalles);
		}

		// GET: Inquilino/Create
		[HttpGet]
		public ActionResult AltaInquilino()
		{
			return View();
		}

		// POST: Inquilino/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AltaInquilino(Inquilino inquilino)
		{
			try
			{
				// TODO: Add insert logic here
				Repo.AltaInquilino(inquilino);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: Inquilino/Edit/5
		public ActionResult EditarInquilino(int id)
		{
			return View();
		}

		// POST: Inquilino/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditarInquilino(int id, Inquilino inquilino)
		{
			try
			{
				// TODO: Add update logic here
				inquilino.idInquilino = id;
				Repo.EditarInquilino(inquilino);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: Inquilino/Delete/5
		public ActionResult BorrarInquilino(int id)
		{
			var inquiBorrar = Repo.BuscarInquilino(id);
			return View(inquiBorrar);
		}

		// POST: Inquilino/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult BorrarInquilino(int id, IFormCollection collection)
		{
			try
			{
				// TODO: Add delete logic here
				Repo.BorrarInquilino(id);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}