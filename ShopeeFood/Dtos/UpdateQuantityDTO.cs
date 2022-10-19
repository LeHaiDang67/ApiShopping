using ShopeeFood.Models;
using System.ComponentModel.DataAnnotations;

namespace ShopeeFood.Dtos
{
	public class UpdateQuantityDTO
	{
		public int quantity { get; set; }
		public string CartId { get; set; }
	}
}
