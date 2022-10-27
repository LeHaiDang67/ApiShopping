using ShopeeFood.Models;

namespace ShopeeFood.Dtos.OrderDTO
{
	public class ListOrderByUserDTO
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public DateTime CreatedDate { get; set; }
		public int TotalPrice { get; set; }
		public int UserId { get; set; }
		public List<Product> Products { get; set; }
	}
}
