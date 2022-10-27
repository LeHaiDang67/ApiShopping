using System.ComponentModel.DataAnnotations;

namespace ShopeeFood.Dtos.UserDTO
{
    public class CreateUserDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public int UserAge { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        public int Genre { get; set; }
        public int Role { get; set; }
        public string Password { get; set; }
    }
}
