using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DataAccess
{
	public class UniversityDBContext: DbContext
	{
		public UniversityDBContext(DbContextOptions<UniversityDBContext> options)
			: base(options)
		{

		}
		//TODO: Add DbSets
		public DbSet<User>? Users { get; set; }
		public DbSet<Curso>? Cursos { get; set; }
		public DbSet<Categoria>? Categorias { get; set; }
		public DbSet<Estudiante>? Estudiantes { get; set; }
		public DbSet<Chapter>? Chapters { get; set; }

		//protected override void OnModelCreating(ModelBuilder modelBuilder)
		//{
		//	modelBuilder.Entity<BaseEntity>()
		//		.HasOne(b => b.User)
		//		.WithOne(i => i.BaseEntity)
		//		.HasForeignKey<User>(b => b.BaseEntityForeignKey);
		//}
	}
}
