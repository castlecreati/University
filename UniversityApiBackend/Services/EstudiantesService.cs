using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
	public class EstudiantesService : IEstudiantesService
	{
		private readonly UniversityDBContext _context;

		public EstudiantesService(UniversityDBContext context)
		{
			_context = context;
		}

		public async Task<ActionResult<IEnumerable<Estudiante>>> GetAllEstudiantesOfArte()
		{
			var estudiantesDeArte = _context.Estudiantes.Include(e => e.Cursos.Where(
									c => c.Nombre == "Arte")).ToListAsync();
									
			return await estudiantesDeArte;
		}
	

		public async Task<ActionResult<IEnumerable<Estudiante>>> GetAllEstudiantesWithoutCourses()
		{
			var estudiantesSinCursos = _context.Estudiantes.Where(e => e.Cursos.Count == 0).ToListAsync();

			return await estudiantesSinCursos;
		}

		
	}
}
