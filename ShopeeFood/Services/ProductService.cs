
using Microsoft.EntityFrameworkCore;
using ShopeeFood.Dtos;
using ShopeeFood.Interfaces;
using ShopeeFood.Models;

namespace ShopeeFood.Services
{
	public class ProductService : IProduct
	{
		private readonly todoAPIContext _todoContext;
		public ProductService(todoAPIContext todoContext)
		{
			_todoContext = todoContext;
		}

		public async Task<List<Product>> FindAll()
		{
			var products = await _todoContext.Products.ToListAsync();

			return products;
		}

		public async Task<Product> FindById(int id)
		{
			var product = await _todoContext.Products.FirstOrDefaultAsync(p => p.Id == id);
			if (product == null)
			{
				return null;
			}
			return product;
		}

		public async Task<Product> FindByName(string theName)
		{
			var item = await _todoContext.Products.FirstOrDefaultAsync(m => m.NameProduct == theName);
			if (item == null)
			{
				return null;
			}
			return item;
		}

		public void CreateItem(CreateProductDTO requestData)
		{
			var newItem = new Product();
			newItem.NameProduct = requestData.NameProduct;
			newItem.Image = requestData.Image;
			newItem.UnitPrice = requestData.UnitPrice;
			newItem.TypeProduct = requestData.TypeProduct;
			newItem.Detail = requestData.Detail;
			_todoContext.Products.Add(newItem);
			_todoContext.SaveChanges();
		}

		public async void UpdateItem(Product requestData)
		{
			_todoContext.ChangeTracker.Clear();
			var existProduct = await FindById(requestData.Id);
			if (existProduct == null)
			{
				return;
			}
			if(requestData.NameProduct != null)
			{
				existProduct.NameProduct = requestData.NameProduct;
			}
			if(requestData.Image != null)
			{
				existProduct.Image = requestData.Image;
			}
			if(requestData.TypeProduct > 0)
			{
				existProduct.TypeProduct = requestData.TypeProduct;
			}
			if(requestData.Detail != null)
			{
				existProduct.Detail = requestData.Detail;
			}
			_todoContext.Products.Update(existProduct);
			_todoContext.SaveChanges();
		}

		public async Task<bool> DeleteItem(int id)
		{
			var checkExistData = await FindById(id);
			if (checkExistData == null)
			{
				return false;
			}
			_todoContext.Products.Remove(checkExistData);
			_todoContext.SaveChanges();
			return true;
		}

	}
}
