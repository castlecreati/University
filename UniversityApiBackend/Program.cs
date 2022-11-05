using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Conexion con SQL Server Express
const string CONNECTIONNAME = "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

// A�adir contexto --- A�adido logger para inspecci�n de las sentencias TSQL en terminal
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString)
.LogTo(Console.WriteLine,
					new[] { DbLoggerCategory.Database.Command.Name },
					LogLevel.Information));
// A�adimos al contenedor el servicio de querys
builder.Services.AddScoped<IQueryServices, Services>();

// Add services to the container.

builder.Services.AddControllers()
	.AddJsonOptions(opt =>
				opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
