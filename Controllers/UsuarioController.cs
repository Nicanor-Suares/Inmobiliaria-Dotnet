using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Inmobiliaria_DotNet.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Inmobiliaria_DotNet.Controllers
{
	public class UsuarioController : Controller
	{
		private IConfiguration configuration;
		private IWebHostEnvironment environment;
		private string hashSalt = "";
		private RepositorioUsuario Repo;
    private RepositorioDB repoDB;

		public UsuarioController(IConfiguration configuration, IWebHostEnvironment environment)
		{
			this.configuration = configuration;
			this.environment = environment;
			this.hashSalt = configuration["Salt"] ?? "";
			this.Repo = new RepositorioUsuario();
      this.repoDB = new RepositorioDB();
		}

		// GET: Usuarios
		[Authorize(Policy = "Administrador")]
		public ActionResult Index()
		{
			var usuarios = Repo.ListarUsuarios();
			return View(usuarios);
		}

		// GET: Usuarios/Create
    [HttpGet]
		//[Authorize(Policy = "Administrador")]
		public ActionResult AltaUsuario()
		{
      try
      {
       	ViewBag.Roles = Usuario.ObtenerRoles();
			  return View(); 
      }
      catch (System.Exception)
      {
        throw;
      }
		}

		// POST: Usuarios/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Policy = "Administrador")]
		public async Task<ActionResult> AltaUsuario(Usuario usuario)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Roles = Usuario.ObtenerRoles();
				return View();
			}
			try
			{
				// Comprobar que el email ingresado no existe en la BD
				var usuarioDB = Repo.BuscarUsuarioPorEmail(usuario.email);
				if (usuarioDB.idUsuario > 0)
				{
					//ModelState.AddModelError("Email", "El email ingresado ya existe");
					Console.WriteLine("El email ingresado ya existe");
          //ViewBag.Error = "El email ingresado ya existe";
					//ViewBag.Roles = Usuario.ObtenerRoles();
					return View();
				}

				// Comprobar que las contraseñas sean iguales
				if (usuario.password != usuario.confirmPassword)
				{
					// ModelState.AddModelError("ConfirmPassword", "Las contraseñas no coinciden");
					// ViewBag.Error = "Las contraseñas no coinciden";
					// ViewBag.Roles = Usuario.ObtenerRoles();
          Console.WriteLine("Las contraseñas no coinciden");
					return View();
				}

				string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
					password: usuario.password,
					salt: System.Text.Encoding.ASCII.GetBytes(hashSalt),
					prf: KeyDerivationPrf.HMACSHA1,
					iterationCount: 10000,
					numBytesRequested: 256 / 8
				));
				usuario.password = hashed;

				usuario.rol = User.IsInRole("Administrador") ? usuario.rol : (int)enRoles.Empleado;

				int res = Repo.AltaUsuario(usuario);
				Repo.ModificarContraseña(usuario);
        
				if (usuario.AvatarFile != null && usuario.idUsuario > 0)
				{
          Database db = repoDB.GetDatabase();
          // Instanciar objeto que contiene funciones de subida de foto de perfil
          var cloud = new RepositorioCloud(db.connection_string, db.container);
					string fileName = $"avatar_{usuario.idUsuario + Path.GetExtension(usuario.AvatarFile.FileName)}";
          var imageUrl = await cloud.SubirAvatarAsync(fileName, usuario.AvatarFile);
          usuario.avatar = imageUrl;
					Repo.ModificarAvatar(usuario);
				}
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				// ViewBag.Roles = Usuario.ObtenerRoles();
				// return View();
        throw;
			}
		}

		// GET: Usuarios/Perfil
		[Authorize]
		public ActionResult Perfil()
		{
			ViewData["Title"] = "Mi perfil";
			ViewBag.Roles = Usuario.ObtenerRoles();
			var idsuario = Convert.ToInt32(User.FindFirst("idUsuario")?.Value);
			var usuario = Repo.BuscarUsuarioPorId(idsuario);
			TempData["Perfil"] = true;
			return View("EditarUsuario", usuario);
		}

		// GET: Usuarios/Edit/5
		[Authorize(Policy = "Administrador")]
		public ActionResult EditarUsuario(int id)
		{
			TempData["Perfil"] = false;
			ViewBag.Roles = Usuario.ObtenerRoles();
			var usuario = Repo.BuscarUsuarioPorId(id);
			return View(usuario);
		}

		// POST: Usuarios/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<ActionResult> EditarUsuario(int id, Usuario usuario)
		{

			// Contraseña no es obligatorio en edicion
			// if (ModelState.ContainsKey("Password")) ModelState.Remove("Password");
			// if (ModelState.ContainsKey("ConfirmPassword")) ModelState.Remove("ConfirmPassword");

			var usuarioDB = Repo.BuscarUsuarioPorId(usuario.idUsuario);

			if (!ModelState.IsValid)
			{
				ViewBag.Roles = Usuario.ObtenerRoles();
				return View(usuarioDB);
			}

			try
			{
				// Comprobar que el email ingresado no existe en la BD
				var validarEmail = Repo.BuscarUsuarioPorEmail(usuario.email);
				if (validarEmail.idUsuario > 0 && validarEmail.idUsuario != usuario.idUsuario)
				{
					// ModelState.AddModelError("Email", "El email ingresado ya existe");
					// ViewBag.Error = "Email no puede coincidir con otro usuario";
					// ViewBag.Roles = Usuario.ObtenerRoles();
          Console.WriteLine("El email ingresado ya existe");
					return View(usuarioDB);
				}

				var modoEdicionDePerfil = TempData.ContainsKey("Perfil") && (bool)TempData.Peek("Perfil") == true;

				if (usuario.password != null)
				{
					if (usuario.confirmPassword == null)
					{
						// ModelState.AddModelError("ConfirmPassword", "No puede ser vacia");
						// ViewBag.Error = "Confirmacion vacia";
						// ViewBag.Roles = Usuario.ObtenerRoles();
            Console.WriteLine("Confirmacion vacia");
						return View(usuarioDB);
					}

					if (usuario.password != usuario.confirmPassword)
					{
						// ModelState.AddModelError("ConfirmPassword", "Las contraseñas no coinciden");
						// ViewBag.Error = "Las contraseñas no coinciden";
						// ViewBag.Roles = Usuario.ObtenerRoles();
          Console.WriteLine("Las contraseñas no coinciden");
						return View(usuarioDB);
					}

					string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
						password: usuario.password,
						salt: System.Text.Encoding.ASCII.GetBytes(hashSalt),
						prf: KeyDerivationPrf.HMACSHA1,
						iterationCount: 10000,
						numBytesRequested: 256 / 8
					));
					usuario.password = hashed;

					Repo.ModificarContraseña(usuario);
				}

				usuario.rol = User.IsInRole("Administrador") ? usuario.rol : (int)enRoles.Empleado;

				int res = Repo.ModificarUsuario(usuario);
				if (usuario.AvatarFile != null && usuario.idUsuario > 0)
				{
          Database db = repoDB.GetDatabase();
          // Instanciar objeto que contiene funciones de subida de foto de perfil
          var cloud = new RepositorioCloud(db.connection_string, db.container);

					string fileName = $"avatar_{usuario.idUsuario + Path.GetExtension(usuario.AvatarFile.FileName)}";
          var imageUrl = await cloud.SubirAvatarAsync(fileName, usuario.AvatarFile);
					Repo.ModificarAvatar(usuario);

					string wwwPath = environment.WebRootPath;
					string path = Path.Combine(wwwPath, "Uploads");
					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(path);
					}
					Repo.ModificarAvatar(usuario);
				}

				// Si estan editanto, modificar el claim
				if (modoEdicionDePerfil)
				{
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, usuario.email ?? usuarioDB.email),
						new Claim("FullName", $"{usuario.nombre ?? usuarioDB.nombre} {usuario.apellido ?? usuarioDB.apellido}"),
						new Claim(ClaimTypes.Role, usuario.RolNombre),
						new Claim("idUsuario", usuario.idUsuario.ToString()),
						new Claim("Avatar", usuario.avatar ?? usuarioDB.avatar ?? "")
					};

					var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

					await HttpContext.SignInAsync(
						CookieAuthenticationDefaults.AuthenticationScheme,
						new ClaimsPrincipal(claimsIdentity)
					);
				}

				if (modoEdicionDePerfil)
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					return RedirectToAction(nameof(Index));
				}

			}
			catch
			{
				throw;
			}
		}

		// GET: Usuarios/Delete/5
		[Authorize(Policy = "Administrador")]
		public ActionResult BorrarUsuario(int id)
		{
			var usuario = Repo.BuscarUsuarioPorId(id);
			return View(usuario);
		}

		// POST: Usuarios/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Policy = "Administrador")]
		public async Task<ActionResult> BorrarUsuarioAsync(int id, Usuario usuario)
		{
			try
			{
				var user = Repo.BuscarUsuarioPorId(id);
				
				// Borrar la foto si existe
        if (user.avatar != null){          
          Database db = repoDB.GetDatabase();
          var cloud = new RepositorioCloud(db.connection_string, db.container);
          await cloud.BorrarAvatar(user.avatar);
        }

				Repo.Eliminar(id);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				var user = Repo.BuscarUsuarioPorId(id);
				return View(user);
			}
		}

		// GET: Usuarios/Login
		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
      try
      {
       	TempData["returnUrl"] = returnUrl;
			  return View();
      }
      catch (System.Exception)
      {
        throw;
      }
		}

		// POST: Usuarios/Login
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginView login)
		{
			try
			{
				var returnUrl = String.IsNullOrEmpty(TempData["returnUrl"] as string) ? "/Home" : TempData["returnUrl"]?.ToString();
				if (ModelState.IsValid)
				{
					string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
						password: login.Password,
						salt: System.Text.Encoding.ASCII.GetBytes(hashSalt),
						prf: KeyDerivationPrf.HMACSHA1,
						iterationCount: 10000,
						numBytesRequested: 256 / 8
					));

					var usuario = Repo.BuscarUsuarioPorEmail(login.Email);

					// Salir temprano por si el usuario no existe o la contraseña es incorrecta
					if (usuario == null || usuario.password != hashed)
					{
						//ModelState.AddModelError("", "Usuario o contraseña incorrectos");
						Console.WriteLine("Usuario o contraseña incorrectos");
            TempData["returnUrl"] = returnUrl;
						return View();
					}

					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, usuario.email),
						new Claim("FullName", $"{usuario.nombre} {usuario.apellido}"),
						new Claim(ClaimTypes.Role, usuario.RolNombre),
						new Claim("IdUsuario", usuario.idUsuario.ToString()),
						new Claim("Avatar", usuario.avatar ?? "")
					};

					var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

					await HttpContext.SignInAsync(
						CookieAuthenticationDefaults.AuthenticationScheme,
						new ClaimsPrincipal(claimsIdentity)
					);

					TempData.Remove("returnUrl");
					return Redirect(returnUrl);
				}

				TempData["returnUrl"] = returnUrl;
				return View();
			}
			catch
			{
				throw;
			}
		}

		// Get: /Salir
		[Route("salir", Name = "logout")]
		public async Task<ActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home");
		}
	}
}