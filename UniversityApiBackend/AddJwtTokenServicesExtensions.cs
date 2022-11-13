using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend
{
	public static class AddJwtTokenServicesExtensions
	{
		public static void AddJwtTokenServices(this IServiceCollection Services, IConfiguration configuration)
		{
			// Add JWT Settings
			var bindJwtSettings = new JwtSettings();
			configuration.Bind("JsonWebTokenKeys", bindJwtSettings);

			//Add Singleton of JwtSettings
			Services.AddSingleton(bindJwtSettings);

			//Concatenamos los servicios aunque podrían ir separados.
			Services.AddAuthentication(options =>
				{
					//para autenticar usuarios
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					//para comprobar usuarios
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

				}).AddJwtBearer(options =>
				{
					//Hacemos unas comprobaciones sobre la Jwtbearer, por defecto
					options.RequireHttpsMetadata = false;
					options.SaveToken = true;
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigninKey,
						IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigninKey)),
						ValidateIssuer = bindJwtSettings.ValidateIssuer,
						ValidIssuer = bindJwtSettings.ValidIssuer,
						ValidAudience = bindJwtSettings.ValidAudience,
						ValidateAudience = bindJwtSettings.ValidateAudience,
						RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
						ValidateLifetime = bindJwtSettings.ValidateLifetime,
						ClockSkew = TimeSpan.FromDays(1)
					};

				});
		}
	}
}
