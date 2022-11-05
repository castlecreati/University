using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using UniversityApiBackend.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Conexion con SQL Server Express
const string CONNECTIONNAME = "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

// Añadir contexto
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString)
.LogTo(Console.WriteLine,
					new[] { DbLoggerCategory.Database.Command.Name },
					LogLevel.Information));
// Add services to the container.

builder.Services.AddControllers()
	.AddJsonOptions(opt =>
				opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// CORS configuration
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

// Use Cors
app.UseCors("CorsPolicy");

app.Run();
