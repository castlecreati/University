using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models;
using UniversityApiBackend.Models.DataModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace UniversityApiBackend.Servicios
{
	public class Services: IQueryServices
	{
		private readonly UniversityDBContext _context;

		public Services(UniversityDBContext context)
		{
			_context = context;
		}
		
		public async Task<ActionResult<IEnumerable<User>>> UsuariosPorEmail(string mail)
		{
			var usuariosPorEmail = await _context.Users
				.Where(u => u.Email == mail)
				.ToListAsync();
			
			return usuariosPorEmail;

		}

		public async Task<ActionResult<IEnumerable<Estudiante>>> EstudiantesMayoresEdad()
		{
			var anoActual = DateTime.Today.Year;
			var estudiantesAdultos = await _context.Estudiantes
				.Where(e => anoActual - e.DOB.Year > 18)
				.ToListAsync();
			return estudiantesAdultos;
		}

		public async Task<ActionResult<IEnumerable<Estudiante>>> AlumnosUnCursoOMas()
		{
			var alumnosUnCursoOMAs = await _context.Estudiantes
				.Where(e => e.Cursos.Count >= 1)
				.ToListAsync();
			return alumnosUnCursoOMAs;
		}

		public Task<ActionResult<IEnumerable<Curso>>> CursoNivel0MinimoUnAlumno()
		{
			throw new NotImplementedException();
		}

		public Task<ActionResult<IEnumerable<Curso>>> CursoNivel0CategoriaCalculo()
		{
			throw new NotImplementedException();
		}

		public async Task<ActionResult<IEnumerable<Curso>>> CursosSinAlumnos()
		{
			var cursosSinAlumnos = await _context.Cursos
				.Where(c => c.Estudiantes.Count == 0).ToListAsync();
			return cursosSinAlumnos;
		}
	}
}
