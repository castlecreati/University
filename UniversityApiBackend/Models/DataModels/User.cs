using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
	public class User: BaseEntity
	{
		//public User()
		//{
		//	Estudiante = new Estudiante();
		//}
		[Required]
		[StringLength(100)]
		public string Name { get; set; } =string.Empty;

		[Required,StringLength(100)]
		public string LastName { get; set; } = string.Empty;

		[Required, EmailAddress]
		public string Email { get; set; } = string.Empty;

		[Required]
		public string Password { get; set; } = string.Empty;
		[Required]
		public Role Role { get; set; } = Role.User;

		public Estudiante? Estudiante { get; set; }
	}

	public enum Role
	{
		User,
		Administrator
	}
}

