

namespace ShopeeFood.Models
{
    public class Product
    {
        public Product()
        {
            Orders = new List<Order>();
            CartItems = new List<CartItem>();

		}

        public int Id { get; set; }
        public string NameProduct { get; set; }
        public string Image { get; set; }
        public int UnitPrice { get; set; }
        public int TypeProduct { get; set; }
        public string? Detail { get; set; }
        public List<Order> Orders { get; set; }
		public List<CartItem> CartItems { get; set; }
	}
}
