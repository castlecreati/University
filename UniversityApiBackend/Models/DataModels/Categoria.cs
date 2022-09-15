using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
	public class Categoria: BaseEntity
	{
		[Required]
		public string Nombre { get; set; } = string.Empty;
		public ICollection<Curso> Cursos { get; set; } = new List<Curso>();
	}
}
