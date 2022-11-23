using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Controllers
{
	[Route("Api/[Controller]/[Action]")]
	[ApiController]
	public class AccountController : Controller
	{
		private readonly JwtSettings _jwtSettings;
		private readonly UniversityDBContext _context;

		public AccountController(JwtSettings jwtSettings, UniversityDBContext context)
		{
			_jwtSettings = jwtSettings;
			_context = context;
		}

		// Changed by real users in DB
		private List<User> Logins = new List<User>()
		{
			new User()
			{
				Id = 1,
				Email = "martin@imaginagroup.com",
				Name = "Admin",
				Password = "Admin"
			},
			new User()
			{
				Id = 2,
				Email = "pepe@imaginagroup.com",
				Name = "User1",
				Password = "pepe"
			}

		};

		[HttpPost]
		public IActionResult GetToken(UserLogings userLogin)
		{
			try
			{
				var Token = new UserTokens();


				var searchUser = _context.Users
					.FirstOrDefault(user => user.Name == userLogin.UserName);

				var Valid = searchUser.Password.Equals(userLogin.PassWord) &&
							searchUser != null;

				if (Valid)
				{
					//var user = Logins
					//	.FirstOrDefault(user => user.Name
					//	.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));
					Token = JwtHelpers.GetTokenKey(new UserTokens()
					{
						UserName = searchUser.Name,
						EmailId = searchUser.Email,
						Id = searchUser.Id,
						Role = ((int)searchUser.Role == 1) ? "Administrator" : "User",
						GuidId = Guid.NewGuid()
					}, _jwtSettings);
				}
				else
				{
					return BadRequest("Wrong Password or null User");
				}
				return Ok(Token);
			}
			catch (Exception ex)
			{

				throw new Exception("GetToken error", ex);
			}
		}
		[HttpGet]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
		public ActionResult GetUserList()
		{
			return Ok(_context.Users);
		}
		
	}
}
