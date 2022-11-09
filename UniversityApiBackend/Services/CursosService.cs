using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
	public class CursosService : ICursosService
	{
		private readonly UniversityDBContext _context;

		public CursosService(UniversityDBContext context)
		{
			_context = context;
		}
		public async Task<ActionResult<IEnumerable<Curso>>> GetAllCursosOfWhithoutChapters()
		{
			var cursosWithoutChapters = _context.Cursos
										.Where(c => c.Chapter.Chapters == null)
										.ToListAsync();
			return await cursosWithoutChapters;
		}

		public async Task<ActionResult<IEnumerable<Curso>>> GetCursosOfAlgebra()
		{
			var cursosDeAlgebra = _context.Cursos.Include(c => c.Categorias.Where(
									cat => cat.Nombre == "Álgebra")).ToListAsync();
			return await cursosDeAlgebra;
		}

		public async Task<ActionResult<IEnumerable<Curso>>> GetCoursesOfMoncho()
		{
			var cursosDeMoncho = _context.Cursos.Include(c => c.Estudiantes.Where(
									e => e.Firstname == "Moncho")).ToListAsync();
			return await cursosDeMoncho;
		}
	}
}
