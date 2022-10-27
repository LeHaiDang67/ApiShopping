using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShopeeFood.Dtos.CartItemDTO;
using ShopeeFood.Interfaces;
using ShopeeFood.Models;

namespace ShopeeFood.Controllers
{
    [ApiController]
	[Route("client/[controller]")]
	[Authorize(AuthenticationSchemes = "Bearer")]
	public class ShoppingCartController : Controller
	{
		public const string CartSessionKey = "CartId";
		private readonly todoAPIContext _todoAPIContext;
		private readonly ICartItem _iCartItem;
		public ShoppingCartController(todoAPIContext todoContext, ICartItem iCartItem)
		{
			_todoAPIContext = todoContext;
			_iCartItem = iCartItem;
		}
		[HttpPost]
		public async Task<ActionResult> GetCartList(int userId)
		{
			var existUser = await _todoAPIContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
			if (existUser == null)
			{
				return NotFound(new
				{
					Success = false,
					Message = "user is not exist"
				});
			}
			var cartList = _iCartItem.GetCartItems(userId);
			var responseData = new List<CartItemsResponseDTO>();
			foreach (var cartItem in cartList)
			{
				responseData.Add(new CartItemsResponseDTO()
				{
					id = cartItem.Id,
					createdDate = cartItem.CreatedDate,
					productId = cartItem.ProductId,
					productName = cartItem.Product.NameProduct,
					detailProduct = cartItem.Product.Detail,
					productImg = cartItem.Product.Image,
					typeProduct = cartItem.Product.TypeProduct,
					unitPrice = cartItem.Product.UnitPrice,
					quantity = cartItem.Quantity,
					totalPrice = (cartItem.Quantity * cartItem.Product.UnitPrice),
					userId = cartItem.UserId
				});
			}
			return Ok(new
			{
				Success = true,
				Data = responseData,
				Message = "Success"
			});
		}

		[HttpPost]
		[Route("AddToCart")]
		public async Task<ActionResult> AddToCart(AddToCartDTO request)
		{
			var product = await _todoAPIContext.Products.FirstOrDefaultAsync(u => u.Id == request.ProductId);
			if (product == null)
			{
				return NotFound(new
				{
					Success = false,
					Message = "Product is not exist"
				});
			}
			var existUser = await _todoAPIContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
			if (existUser == null)
			{
				return NotFound(new
				{
					Success = false,
					Message = "user is not exist"
				});
			}
			_iCartItem.AddCartItems(request);
			return Ok(new
			{
				Success = true,
				Message = "Success"
			});
		}

		[HttpPut]
		[Route("EditQuantity")]
		public async Task<IActionResult> EditQuantity(EditQuantityRequestDTO request)
		{
			var checkCartItem = _iCartItem.EditQuantity(request);
			if (checkCartItem == false)
			{
				return NotFound(new
				{
					Success = false,
					Message = "CartItem is not exist"
				});
			}
			return Ok( new
			{
				Success = checkCartItem,
				Message = "Success"
			});
		}

		[HttpDelete]
		[Route("DeleteCartItem")]
		public async Task<ActionResult> DeleteCartItem(string cartItemId)
		{
			if(cartItemId == null)
			{
				return BadRequest(new
				{
					Success = false,
					Message = "Id is missed"
				});
			}
			var checkCartItem = _iCartItem.ClearCart(cartItemId);
			if(checkCartItem == false)
			{
				return NotFound(new
				{
					Success = false,
					Message = "Not found"
				});
			}
			return Ok(new
			{
				Success = checkCartItem,
				Message = "Success"
			});
		}
	}
}
