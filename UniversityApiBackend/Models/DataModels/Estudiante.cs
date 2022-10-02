using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UniversityApiBackend.Models.DataModels
{
	public class Estudiante: BaseEntity
	{
		[Required]
		public string Firstname { get; set; } = string.Empty;
		[Required]
		public string Lastname { get; set; } = string.Empty;
		[Required]
		public DateTime DOB { get; set; }
		public ICollection<Curso> Cursos { get; set; } = new List<Curso>();
		[Required]
		public int FKUserId { get; set; }
		[JsonIgnore]
		public virtual User? User { get; set; }
		
	}
}
