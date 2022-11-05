using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Servicios
{
	public interface IQueryServices
	{
		//Obtener los usuarios según email
		public Task<ActionResult<IEnumerable<User>>> UsuariosPorEmail(string mail);
		public Task<ActionResult<IEnumerable<Estudiante>>> EstudiantesMayoresEdad();
	}
}
