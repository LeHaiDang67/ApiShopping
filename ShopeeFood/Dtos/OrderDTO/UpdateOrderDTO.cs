using System.ComponentModel.DataAnnotations;

namespace ShopeeFood.Dtos.OrderDTO
{
    public class UpdateOrderDTO
    {
		[Required]
		public int Id { get; set; }
		[Required]
		public int TotalPrice { get; set; }
		[Required]
		public int UserId { get; set; }
	}
}
