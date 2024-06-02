using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_DotNet.Models;

namespace Inmobiliaria_DotNet.Controllers
{
	public class PropietariosController : Controller
	{
		private readonly RepositorioPropietario Repo;
		private readonly RepositorioInmueble RepoInmu;

		public PropietariosController() {
			Repo = new RepositorioPropietario();
			RepoInmu = new RepositorioInmueble();
		}
		// GET: Propietarios
		public ActionResult Index()
		{
			var lista = Repo.ListarPropietarios();
			return View(lista);
		}

		// GET: Propietarios/Details/5
		public ActionResult DetallesPropietario(int id)
		{
			var propietario = Repo.BuscarPropietario(id);
			return View(propietario);
		}

		// GET: Propietarios/AltaPropietario
		[HttpGet]
		public ActionResult AltaPropietario()
		{
			return View();
		}

		// POST: Propietarios/AltaPropietario
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AltaPropietario(Propietario propietario)
		{
			try {
				Repo.AltaPropietario(propietario);
				return RedirectToAction(nameof(Index));
			}
			catch {
				return View();
			}
		}

		// GET: Propietarios/Edit/5
		[HttpGet]
		public ActionResult EditarPropietario(int id)
		{
			var propietarioEdit = Repo.BuscarPropietario(id);
			return View(propietarioEdit);
		}

		// POST: Propietarios/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditarPropietario(int id, Propietario propietario)
		{
			try {
				propietario.idPropietario = id;
				Repo.EditarPropietario(propietario);
				return RedirectToAction(nameof(Index));
			}
			catch {
				return View();
			}
		}

		// GET: Propietarios/Delete/5
		[HttpGet]
		public ActionResult BorrarPropietario(int id)
		{
			var propBorrar = Repo.BuscarPropietario(id);
			return View(propBorrar);
		}

		// POST: Propietarios/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult BorrarPropietario(int id, IFormCollection collection)
		{
			try {
				Repo.BorrarPropietario(id);
				return RedirectToAction(nameof(Index));
			}
			catch{
				return View();
			}
		}

		[HttpGet]
		public ActionResult VerPropiedades(int id, Propietario propietario)
		{
			try {
				var propiedades = RepoInmu.VerPropiedades(id);
				return View(propiedades);
			}
			catch	{
				return View();
			}
		}
	}
}