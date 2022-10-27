using ShopeeFood.Models;

namespace ShopeeFood.Dtos.CartItemDTO
{
	public class CartItemsResponseDTO
	{
		public string id { get; set; }
		public int quantity { get; set; }
		public DateTime createdDate { get; set; }
		public int productId { get; set; }
		public string productName { get; set; }
		public string productImg { get; set; }
		public int unitPrice { get; set; }
		public int typeProduct { get; set; }
		public string detailProduct { get; set; }
		public int totalPrice { get; set; }
		public int userId { get; set; }
	}
}
