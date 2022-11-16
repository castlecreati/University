using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Helpers
{
	static class JwtHelpers
	{
		public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, Guid Id)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim("id", userAccounts.Id.ToString()),
				new Claim(ClaimTypes.Name, userAccounts.UserName),
				new Claim(ClaimTypes.Email, userAccounts.EmailId),
				new Claim(ClaimTypes.NameIdentifier, userAccounts.Id.ToString()),
				new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1)
												.ToString("MMM ddd dd yyyy HH:mm:ss tt")),
				new Claim(ClaimTypes.Role, userAccounts.Role)
			};
			//if (userAccounts.UserName == "Admin")
			//{
			//	claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
			//}
			//else if (userAccounts.UserName == "User 1")
			//{
			//	claims.Add(new Claim(ClaimTypes.Role, "User"));
			//	claims.Add(new Claim("UserOnly", "User 1"));
			//}
			return claims;
		}

		public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid Id)
		{
			Id = Guid.NewGuid();
			return GetClaims(userAccounts, Id);
		}

		public static UserTokens GetTokenKey(UserTokens model, JwtSettings jwtSettings)
		{
			try
			{
				var userToken = new UserTokens();
				if (userToken == null)
				{
					throw new ArgumentNullException(nameof(model));
				}
				// Obtain SECRET KEY
				var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigninKey);

				Guid Id;

				// Expires in 1 day
				DateTime expireTime = DateTime.UtcNow.AddDays(1);

				//Validity
				userToken.Validity = expireTime.TimeOfDay; // Validity es TimeSpan

				//Generate our JWT
				var jwToken = new JwtSecurityToken(
					issuer: jwtSettings.ValidIssuer,
					audience: jwtSettings.ValidAudience,
					claims: GetClaims(model, out Id),
					notBefore: new DateTimeOffset(DateTime.Now).DateTime,
					expires: new DateTimeOffset(expireTime).DateTime,
					signingCredentials: new SigningCredentials(
						new SymmetricSecurityKey(key),
						SecurityAlgorithms.HmacSha256));

				userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwToken);
				userToken.UserName = model.UserName;
				userToken.Id = model.Id;
				//userToken.Role = model.Role;
				userToken.GuidId = model.GuidId;

				return userToken;
			}
			catch (Exception exception)
			{
				throw new Exception("Error generando el jwt", exception);
							}
		}
	}
}
