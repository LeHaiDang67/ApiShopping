using Microsoft.EntityFrameworkCore;
using ShopeeFood.Dtos.OrderDTO;
using ShopeeFood.Interfaces;
using ShopeeFood.Models;

namespace ShopeeFood.Services
{
	public class OrderService: IOrder
	{
		private readonly todoAPIContext _todoContext;
		private readonly IUser _iUser;
		public OrderService(todoAPIContext todoContext, IUser iUser)
		{
			_todoContext = todoContext;
			_iUser = iUser;
		}
		public Task<List<Order>> FindAll()
		{
			var orderList = _todoContext.Orders.Include(i => i.User).ToListAsync();
			return orderList;
		}

		public async Task<Order> FindById(int id)
		{
			var order = _todoContext.Orders.FirstOrDefault(o => o.Id == id);
			return order;
		}

		public void CreateItem(CreateOrderDTO requestData)
		{
			// will post the product-id list at the requestData
			// then loop the list 
			// the 1st of loop : Create order -> CreateOrderProduct
			// the 2nd to the rest of life: Get the one that was created -> CreateOrderProduct
			var newOrder = new Order();
			var userExist = _iUser.FindById(requestData.UserId);
			if (userExist != null)
			{
				newOrder.UserId = requestData.UserId;
				newOrder.TotalPrice = requestData.TotalPrice;
				newOrder.CreatedDate = DateTime.Now;
				newOrder.UserName = userExist.Result.UserName;
				_todoContext.Orders.Add(newOrder);
				_todoContext.SaveChanges();
				foreach (var itemId in requestData.ProductIds)
				{
					CreateOrderProduct(newOrder.CreatedDate, itemId);
				};
				
			}
		}

		public void UpdateItem(UpdateOrderDTO requestData)
		{
			var orderExist = FindById(requestData.Id);
			if (orderExist != null)
			{
				var order = new Order();
				order.Id = requestData.Id;
				order.TotalPrice = requestData.TotalPrice;
				order.UserId = requestData.UserId;
				order.UserName = orderExist.Result.UserName;
				order.CreatedDate = orderExist.Result.CreatedDate;
				_todoContext.ChangeTracker.Clear();
				_todoContext.Orders.Update(order);
				_todoContext.SaveChanges();
			}
			
		}

		public void DeleteItem(Order requestData)
		{
			_todoContext.Orders.Remove(requestData);
			_todoContext.SaveChanges();
		}

		//Search order list by UserId
		public async Task<List<ListOrderByUserDTO>> FindOrderByUserId(int userId)
		{
			var orders = await _todoContext.Orders.Include(o => o.Products).Include(u =>
			u.User).Where(o => o.UserId == userId).Select(o => new ListOrderByUserDTO() {
				Id = o.Id,
				UserName = o.UserName,
				CreatedDate = o.CreatedDate,
				TotalPrice = o.TotalPrice,
				UserId = o.UserId,
				Products = o.Products.ToList()

			}).ToListAsync();
			return orders;
		}

		public async Task<Order> FindTheLastOrder()
		{
			var order = (Order)_todoContext.Orders.OrderByDescending(o => o.CreatedDate).Take(1);
			return order;
		}
		public void CreateOrderProduct(DateTime createTime, int productId)
		{
			//var result = FindTheLastOrder();
			var order = _todoContext.Orders.First(o => o.CreatedDate == createTime);
			var orderProduct = new OrderProduct()
			{
				OrderId = order.Id,
				ProductId = productId
			};
			_todoContext.OrderProducts.Add(orderProduct);
			//_todoContext.Orders.FromSqlRaw($"insert into ordersProduct(OrderId, ProductId) values((select top (1) Id from orders order by CreatedDate DESC), 3)");
			_todoContext.SaveChanges();
		}	
	}
}
