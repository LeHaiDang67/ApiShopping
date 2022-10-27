using ShopeeFood.Dtos.CartItemDTO;
using ShopeeFood.Models;

namespace ShopeeFood.Interfaces
{
    public interface ICartItem
	{
		public void AddCartItems(AddToCartDTO request);
		public List<CartItem> GetCartItems(int userId);
		public bool EditQuantity(EditQuantityRequestDTO request);
		public bool ClearCart(string cartItemId);
	}
}
