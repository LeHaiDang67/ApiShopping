using Microsoft.AspNetCore.Mvc;
using ShopeeFood.Dtos;
using ShopeeFood.Dtos.Product;
using ShopeeFood.Interfaces;
using ShopeeFood.Models;

namespace ShopeeFood.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : Controller
	{
		private readonly IProduct _iProduct;
		public ProductController(IProduct iProduct)
		{
			_iProduct = iProduct;
		}

		[HttpGet]
		public async Task<ActionResult> GetAll()
		{
			var listItem = await _iProduct.FindAll();
			if (listItem == null)
			{
				return NotFound(new
				{
					Success = false,
					Message = "Not found"
				});
			}
			return Ok(new
			{
				Success = true,
				Data = listItem,
				Message = "Success"
			});
		}
		[HttpPost]
		[Route("id")]
		public async Task<ActionResult> GetById(int id)
		{
			var product = await _iProduct.FindById(id);
			if (product == null)
			{
				return NotFound(new
				{
					Success = false,
					Message = "Product is not exist"
				});
			}
			return Ok(new
			{
				Success = true,
				Data = product,
				Message = "Success"
			});
		}

		[HttpPost]
		[Route("Search")]
		public async Task<ActionResult> SearchProduct(SearchProductDTO request)
		{
			if(request == null)
			{
				return BadRequest(new
				{
					Success = false,
					Message = "The request is null"
				});
			}
			if(request.ProductName != "")
			{
				var searchData = _iProduct.SearchProduct(request.ProductName);
				return Ok(new
				{
					Success = true,
					Data = searchData.Result,
					Message = "Success"
				});
			} else
			{
				var listItem = await _iProduct.FindAll();
				return Ok(new
				{
					Success = true,
					Data = listItem,
					Message = "Success"
				});
			}
			
		}

		[HttpPost]
		[Route("Create")]
		public async Task<ActionResult> CreateProduct(CreateProductDTO requestData)
		{
			if (requestData == null)
			{
				return BadRequest(new
				{
					Success = false,
					Message = "Bad request"
				});
			}
			var checkExistItem = await _iProduct.FindByName(requestData.NameProduct);
			if (checkExistItem != null)
			{
				return BadRequest(new
				{
					Success = false,
					Message = "Product is exist"
				});
			}
			_iProduct.CreateItem(requestData);
			return Ok(new
			{
				Success = true,
				Message = "Success"
			});
		}

		[HttpPut]
		[Route("Update")]
		public async Task<ActionResult> UpdateProduct(Product product)
		{
			if (product == null)
			{
				return BadRequest(new
				{
					Success = false,
					Message = "Bad request"
				});
			}
			var checkExistItem = await _iProduct.FindById(product.Id);
			if (checkExistItem == null)
			{
				return NotFound(new
				{
					Success = false,
					Message = "Product is not exist"
				});
			}
			_iProduct.UpdateItem(product);
			return Ok(new
			{
				Success = true,
				Message = "Success"
			});
		}

		[HttpDelete]
		[Route("Delete")]
		public async Task<ActionResult> DeleteProduct(int id)
		{
			if (id == 0 || id == null)
			{
				return BadRequest(new
				{
					Success = false,
					Message = "Id field is missed"
				});
			}
			var checkExis = await _iProduct.DeleteItem(id);
			if (checkExis)
			{
				return Ok(new
				{
					Success = true,
					Message = "Success"
				});
			}
			else
			{
				return NotFound(new
				{
					Success = false,
					Message = "Product is not exist"
				});
			}

		}
	}
}
