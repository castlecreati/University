using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Servicios
{
	public interface IQueryServices
	{
		//Obtener los usuarios según email
		public Task<ActionResult<IEnumerable<User>>> UsuariosPorEmail(string mail);
		public Task<ActionResult<IEnumerable<Estudiante>>> EstudiantesMayoresEdad();
		public Task<ActionResult<IEnumerable<Estudiante>>> AlumnosUnCursoOMas();

		//Buscar cursos de un nivel determinado que al menos tengan un alumno inscrito
		public Task<ActionResult<IEnumerable<Curso>>> CursoNivel0MinimoUnAlumno();

		//Buscar cursos de un nivel determinado que sean de una categoría determinada
		public Task<ActionResult<IEnumerable<Curso>>> CursoNivel0CategoriaCalculo();
		//Buscar cursos sin alumnos
		public Task<ActionResult<IEnumerable<Curso>>> CursosSinAlumnos();
	}
}
