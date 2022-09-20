using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
	public class User: BaseEntity
	{
		[Required]
		[StringLength(100)]
		public string Name { get; set; } =string.Empty;

		[Required,StringLength(100)]
		public string LastName { get; set; } = string.Empty;

		[Required, EmailAddress]
		public string Email { get; set; } = string.Empty;

		[Required]
		public string Password { get; set; } = string.Empty;

		//public int BaseEntityForeignKey { get; set; }
		public Estudiante Estudiante { get; set; } = new Estudiante();

	}
}
