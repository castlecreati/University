using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UniversityApiBackend.Models.DataModels
{
	public class Chapter: BaseEntity
	{
		public int CursoId { get; set; }
		[JsonIgnore]
		public virtual Curso Curso { get; set; }

		[Required]
		public string Chapters { get; set; } = string.Empty;
		
	}
}
