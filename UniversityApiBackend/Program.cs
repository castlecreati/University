using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 2. Conexion con SQL Server Express
const string CONNECTIONNAME = "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

// 3. Añadir contexto
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString)
.LogTo(Console.WriteLine,
					new[] { DbLoggerCategory.Database.Command.Name },
					LogLevel.Information));
// 7. Add services of JWT identification - creamos previamente una clase como servicio de AddJwtTokenServices
// TODO: (terminado)
builder.Services.AddJwtTokenServices(builder.Configuration);

//4. Add Custom Services

// Add services to the container.
builder.Services.AddControllers()
	.AddJsonOptions(opt =>
				opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// 8. Add authorization
builder.Services.AddAuthorization(options =>
	{
		options.AddPolicy("UserOnlyPolicy", policy => policy
				.RequireClaim("UserOnly", "User1"));
	});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// 9. TODO: Config Swagger to take care of Authorization of JWT
builder.Services.AddSwaggerGen(options =>
	{
		options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
		{
			// We define the security for the Authorization
			Name = "Authorization",
			Type = SecuritySchemeType.Http,
			Scheme = "Bearer",
			BearerFormat = "JWT",
			In = ParameterLocation.Header,
			Description = "JWT Authorization Bearer using Bearer Scheme"
		});
		options.AddSecurityRequirement(new OpenApiSecurityRequirement
		{
			{
				new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
					},
				new string[]{}
			}
		});
	});

// 5. CORS configuration
builder.Services.AddCors(options =>
	{
		options.AddPolicy(name: "CorsPolicy", builder =>
		{
			builder.AllowAnyOrigin();
			builder.AllowAnyMethod();
			builder.AllowAnyHeader();
		});

	});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// 6. Use Cors
app.UseCors("CorsPolicy");

app.Run();
