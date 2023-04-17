using System.ComponentModel.DataAnnotations;
namespace Inmobiliaria_DotNet.Models;

public enum enRoles
{
	Empleado = 1,
	Administrador = 2,
}

public class Usuario
{
	public int idUsuario { get; set; }
	public string? nombre { get; set; }
	public string? apellido { get; set; }
	[EmailAddress]
	public string? email { get; set; }
	public string password { get; set; } = "";
	[Display(Name = "Confirmar contraseña")]
	public string confirmPassword { get; set; } = "";
	public string? avatar { get; set; }
	public IFormFile? AvatarFile { get; set; }
	public int rol { get; set; }
	public string RolNombre => rol > 0 ? ((enRoles)rol).ToString() : "";

	public static IDictionary<int, string> ObtenerRoles()
	{
		SortedDictionary<int, string> roles = new SortedDictionary<int, string>();
		Type tipoEnumRol = typeof(enRoles);
		foreach (var valor in Enum.GetValues(tipoEnumRol))
		{
			roles.Add((int)valor, Enum.GetName(tipoEnumRol, valor) ?? "");
		}
		return roles;
	}
}