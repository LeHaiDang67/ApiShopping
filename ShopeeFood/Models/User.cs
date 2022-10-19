using System;
using System.Collections.Generic;

namespace ShopeeFood.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            CartItems = new HashSet<CartItem>();
		}

        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public int UserAge { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int Genre { get; set; }
        public int Role { get; set; }
        public string Password { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime RefreshTokenExpiryTime { get; set; }
		public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
