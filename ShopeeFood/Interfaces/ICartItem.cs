using ShopeeFood.Dtos;
using ShopeeFood.Dtos.CartItem;
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
