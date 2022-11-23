using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
	public interface IEstudiantesService
	{
		//Obtener todos los alumnos que no tienen cursos asociados
		Task<ActionResult<IEnumerable<Estudiante>>> GetAllEstudiantesWithoutCourses();

		//Obtener alumnos de un Curso concreto
		Task<ActionResult<IEnumerable<Estudiante>>> GetAllEstudiantesOfArte();
		

	}
}
