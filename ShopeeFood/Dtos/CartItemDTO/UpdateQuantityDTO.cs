using ShopeeFood.Models;
using System.ComponentModel.DataAnnotations;

namespace ShopeeFood.Dtos.CartItemDTO
{
    public class UpdateQuantityDTO
    {
        public int quantity { get; set; }
        public string CartId { get; set; }
    }
}
