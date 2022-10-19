

namespace ShopeeFood.Models
{
    public partial class Product
    {
        public Product()
        {
            Orders = new HashSet<Order>();
            CartItems = new HashSet<CartItem>();

		}

        public int Id { get; set; }
        public string NameProduct { get; set; }
        public string Image { get; set; }
        public int UnitPrice { get; set; }
        public int TypeProduct { get; set; }
        public string? Detail { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
		public virtual ICollection<CartItem> CartItems { get; set; }
	}
}
