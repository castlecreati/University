using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
	public class ChaptersService : IChaptersService
	{ 
		private readonly UniversityDBContext _context;

		public ChaptersService(UniversityDBContext context)
		{
			_context = context;
		}
	
		public async Task<ActionResult<IEnumerable<Chapter>>> GetChapterOfID1()
		{
			var chaptersOfId1Course = _context.Chapters
										.Include(ch => ch.Curso)
										.Where(c => c.CursoId == 1).ToListAsync();
			return await chaptersOfId1Course;
		}
	}
}
