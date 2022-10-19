using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShopeeFood.Dtos;
using ShopeeFood.Dtos.CartItem;
using ShopeeFood.Interfaces;
using ShopeeFood.Models;

namespace ShopeeFood.Services
{
	public class CartItemService:ICartItem
	{
		private readonly todoAPIContext _todoAPIContext;
		//public string ShoppingCartId { get; set; }
		public const string CartSessionKey = "CartId";
		public CartItemService(todoAPIContext todoAPIContext)
		{
			_todoAPIContext = todoAPIContext;
		}

		public List<CartItem> GetCartItems(int userId)
		{
			var cartItem = _todoAPIContext.CartItems.Include(c => c.Product).Where(x => x.UserId == userId).ToList();
			return cartItem;
		}

		public void AddCartItems(AddToCartDTO request)
		{
			var existCartItem = _todoAPIContext.CartItems.FirstOrDefault(c => c.UserId == request.UserId && c.ProductId == request.ProductId);
			if(existCartItem != null)
			{
				existCartItem.Quantity = request.Quantity;
				_todoAPIContext.CartItems.Update(existCartItem);
				_todoAPIContext.SaveChanges();
			} else
			{
				var newItem = new CartItem() { 
					Id = Guid.NewGuid().ToString(),
					Quantity = request.Quantity,
					CreatedDate = DateTime.Now,
					ProductId = request.ProductId,
					UserId = request.UserId
				};
				_todoAPIContext.CartItems.Add(newItem);
				_todoAPIContext.SaveChanges();
			}
			
		}

		public bool EditQuantity(EditQuantityRequestDTO request)
		{
			var existCartItem = _todoAPIContext.CartItems.FirstOrDefault(c => c.Id == request.id);
			if(existCartItem == null)
			{
				return false;
			}
			if(request.quantity > 0)
			{
				existCartItem.Quantity = request.quantity;
				_todoAPIContext.Update(existCartItem);
				_todoAPIContext.SaveChanges();
			} else
			{
				_todoAPIContext.Remove(existCartItem);
				_todoAPIContext.SaveChanges();
			}
			return true;
		}

		public bool ClearCart(string cartItemId)
		{
			var existCartItem = _todoAPIContext.CartItems.FirstOrDefault(c => c.Id == cartItemId);
			if (existCartItem == null)
			{
				return false;
			}
			_todoAPIContext.Remove(existCartItem);
			_todoAPIContext.SaveChanges();
			return true;
		}
	}
}
