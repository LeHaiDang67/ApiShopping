using ShopeeFood.Dtos.OrderDTO;
using ShopeeFood.Models;

namespace ShopeeFood.Interfaces
{
	public interface IOrder
	{
		public Task<List<Order>> FindAll();
		public Task<Order> FindById(int id);
		public void CreateItem(CreateOrderDTO requestData);
		public void UpdateItem(UpdateOrderDTO requestData);
		public void DeleteItem(Order requestData);
		public Task<List<ListOrderByUserDTO>> FindOrderByUserId(int userId);
	}
}
