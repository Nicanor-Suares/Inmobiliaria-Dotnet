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

			public PropietariosController() {
					Repo = new RepositorioPropietario();
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
					try
					{
							// TODO: Add insert logic here
							Repo.AltaPropietario(propietario);
							return RedirectToAction(nameof(Index));
					}
					catch
					{
							return View();
					}
			}

			// GET: Propietarios/Edit/5
			[HttpGet]
			public ActionResult EditarPropietario(int id)
			{
					return View();
			}

			// POST: Propietarios/Edit/5
			[HttpPost]
			[ValidateAntiForgeryToken]
			public ActionResult EditarPropietario(int id, Propietario propietario)
			{
					try
					{
							// TODO: Add update logic here
							propietario.idPropietario = id;
							Repo.EditarPropietario(propietario);
							return RedirectToAction(nameof(Index));
					}
					catch
					{
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
					try
					{
							// TODO: Add delete logic here
							Repo.BorrarPropietario(id);
							return RedirectToAction(nameof(Index));
					}
					catch
					{
							return View();
					}
			}
	}
}