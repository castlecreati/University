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
	}
}
