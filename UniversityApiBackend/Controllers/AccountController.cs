using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Controllers
{
	[Route("Api/[Controller]/[Action]")]
	[ApiController]
	public class AccountController : Controller
	{
		private readonly JwtSettings _jwtSettings;

		public AccountController(JwtSettings jwtSettings)
		{
			_jwtSettings = jwtSettings;
		}

		// TODO: Change by real users in DB
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
				var Valid = Logins.Any(user => user.Name
								.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));
				if (Valid)
				{
					var user = Logins
						.FirstOrDefault(user => user.Name
						.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));
					Token = JwtHelpers.GetTokenKey(new UserTokens()
					{
						UserName = user.Name,
						EmailId = user.Email,
						Id = user.Id,
						GuidId = Guid.NewGuid()
					}, _jwtSettings);
				}
				else
				{
					return BadRequest("Wrong Password");
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
			return Ok(Logins);
		}
		
	}
}
