using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
	public class UserLogings
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		public string PassWord { get; set; }
	}
}
