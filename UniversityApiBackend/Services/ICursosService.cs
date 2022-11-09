using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
	public interface ICursosService
	{
		//Obtener todos los Cursos de una categoría concreta
		Task<ActionResult<IEnumerable<Curso>>> GetCursosOfAlgebra();

		//Obtener Cursos sin temarios
		Task<ActionResult<IEnumerable<Curso>>> GetAllCursosOfWhithoutChapters();
		
		//Obtener los Cursos de un Alumno
		Task<ActionResult<IEnumerable<Curso>>> GetCoursesOfMoncho();
	}
}
