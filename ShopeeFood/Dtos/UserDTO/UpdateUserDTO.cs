using System.ComponentModel.DataAnnotations;

namespace ShopeeFood.Dtos.UserDTO
{
	public class UpdateUserDTO
	{
		[Required]
		public int Id { get; set; }
		public string UserName { get; set; } = string.Empty;
		public int UserAge { get; set; } 
		public string Email { get; set; } =	string.Empty ;
		public string PhoneNumber { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public int Genre { get; set; }
		public int Role { get; set; }
		public string Password { get; set; } = string.Empty;
	}
}
