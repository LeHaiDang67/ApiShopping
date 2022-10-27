using System.ComponentModel.DataAnnotations;

namespace ShopeeFood.Dtos.OrderDTO
{
	public class CreateOrderDTO
	{
		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
		public int TotalPrice { get; set; }
		[Required]
		public int UserId { get; set; }
		[Required]
		public List<int> ProductIds { get; set; }
	}
}
