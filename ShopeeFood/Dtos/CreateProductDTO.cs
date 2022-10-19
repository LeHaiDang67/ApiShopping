using System.ComponentModel.DataAnnotations;

namespace ShopeeFood.Dtos
{
	public class CreateProductDTO
	{
		[Required]
		public string NameProduct { get; set; }
		public string Image { get; set; } = "";
		public int UnitPrice { get; set; } = 0;
		public int TypeProduct { get; set; } = 0;
		public string Detail { get; set; } = "";
	}
}
