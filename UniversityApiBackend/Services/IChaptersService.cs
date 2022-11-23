using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
	public interface IChaptersService
	{
		//Obtener temario de un curso concreto
		Task<ActionResult<IEnumerable<Chapter>>> GetChapterOfID1();
	}
}
