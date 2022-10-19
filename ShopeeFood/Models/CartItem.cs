using System.ComponentModel.DataAnnotations;

namespace ShopeeFood.Models
{
	public class CartItem
	{
		[Key]
		public string Id { get; set; }
		public int Quantity { get; set; }
		public DateTime CreatedDate { get; set; }
		public int ProductId { get; set; }
		public int UserId { get; set; }
		public virtual Product Product { get; set; }
		public virtual User User { get; set; }
	}
}
