using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_DotNet.Models;

namespace Inmobiliaria_DotNet.Controllers
{
	public class InmuebleController : Controller
	{
		private readonly RepositorioInmueble Repo;
		private readonly RepositorioPropietario repoPropietario;
		private readonly RepositorioTipoInmueble repoTipoInmueble;
		private readonly RepositorioContrato repoContratos;

		public InmuebleController()
		{
			Repo = new RepositorioInmueble();
			repoPropietario = new RepositorioPropietario();
			repoTipoInmueble = new RepositorioTipoInmueble();
			repoContratos = new RepositorioContrato();
		}
		// GET: Inmueble
		public ActionResult Index()
		{
			var lista = Repo.ListarInmuebles();
			return View(lista);
		}

		// GET: Inmueble/Details/5
		public ActionResult DetallesInmueble(int id)
		{
			var inmuDetalles = Repo.BuscarInmueble(id);
			return View(inmuDetalles);
		}

		// GET: Inmueble/Create
		public ActionResult AltaInmueble()
		{
			try
			{
				ViewBag.Propietario = repoPropietario.ListarPropietarios();
				ViewBag.TipoInmueble = repoTipoInmueble.listarTiposInmuebles();
				return View();
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		// POST: Inmueble/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AltaInmueble(Inmueble inmueble)
		{
			try
			{
				// TODO: Add insert logic here
				Repo.AltaInmueble(inmueble);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: Inmueble/Edit/5
		public ActionResult EditarInmueble(int id)
		{
			try
			{
				ViewBag.Propietario = repoPropietario.ListarPropietarios();
				ViewBag.TipoInmueble = repoTipoInmueble.listarTiposInmuebles();
				Inmueble inmuEditar = Repo.BuscarInmueble(id);
				return View(inmuEditar);
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		// POST: Inmueble/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditarInmueble(int id, Inmueble inmueble)
		{
			try
			{
				// TODO: Add update logic here
				inmueble.idInmueble = id;
				Repo.EditarInmueble(inmueble);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: Inmueble/Delete/5
		public ActionResult BorrarInmueble(int id)
		{
			var inmuBorrar = Repo.BuscarInmueble(id);
			return View(inmuBorrar);
		}

		// POST: Inmueble/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult BorrarInmueble(int id, IFormCollection collection)
		{
			try
			{
				// TODO: Add delete logic here
				Repo.BorrarInmueble(id);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				throw;
				//return View();
			}
		}

		[HttpGet]
		public ActionResult VerContratosInmueble(int id)
		{
			try
			{
				var contratos = repoContratos.VerContratosInmueble(id);
				return View(contratos);
			}
			catch
			{
				return View();
			}
		}

		// Action to display the date selection form
    public ActionResult BuscarPorFecha()
    {
        return View();
    }

		// GET: Inmueble/BuscarPorFecha
		public ActionResult MostrarDisponibles(DateTime? fechaInicio, DateTime? fechaFin)
		{
			try
			{
				List<Inmueble> inmueblesDisponibles = new List<Inmueble>();
				if (fechaInicio.HasValue && fechaFin.HasValue)
				{
					inmueblesDisponibles = Repo.BuscarPorFecha(fechaInicio.Value, fechaFin.Value);
					ViewBag.StartDate = fechaInicio.Value;
					ViewBag.EndDate = fechaFin.Value;
				}

				return View(inmueblesDisponibles);
			}
			catch (Exception ex)
			{
				throw;
			}
		}

	}
}