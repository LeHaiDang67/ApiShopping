using ShopeeFood.Dtos;
using ShopeeFood.Models;

namespace ShopeeFood.Interfaces
{
	public interface IProduct 
	{
		public Task<List<Product>> FindAll();
		public Task<Product> FindById(int id);
		public Task<Product> FindByName(string theName);
		public void CreateItem(CreateProductDTO requestData);
		public void UpdateItem(Product requestData);
		public Task<bool> DeleteItem(int id);
		public Task<List<Product>> SearchProduct(string titleProduct);
	}
}
