using Microsoft.AspNetCore.Mvc;
using ShopeeFood.Dtos.OrderDTO;
using ShopeeFood.Interfaces;

namespace ShopeeFood.Controllers
{
	[ApiController]
	[Route("api/[controller]")]

	public class OrderController : Controller
	{
		private readonly IOrder _iOrder;
		private readonly IUser _iUser;
		public OrderController(IOrder iOrder, IUser iUser)
		{
			_iOrder = iOrder;
			_iUser = iUser;
		}
		[HttpGet]
		public async Task<ActionResult> GetAll()
		{
			var orders = _iOrder.FindAll();
			return Ok(new
			{
				Success = true,
				Data = orders,
				Message = "Success"
			});
		}

		[HttpGet]
		[Route("GetByUser")]
		public async Task<ActionResult> GetByUserId(int userId)
		{
			var orders = _iOrder.FindOrderByUserId(userId);
			//var response = new ListOrderByUser()
			//{
			//	Id = orders.Id,
			//	CreatedDate = DateTime.Now,
			//};


			return Ok(new
			{
				Success = true,
				Data = orders.Result,
				Messaga = "Success"
			});
		}

		[HttpPost]
		[Route("Create")]
		public async Task<ActionResult> CreatOrder(CreateOrderDTO requestData)
		{
			_iOrder.CreateItem(requestData);

			return Ok(new
			{
				Success = true,
				Message = "Success"
			});
		}
		[HttpPut]
		[Route("Update")]
		public async Task<ActionResult> UpdateOrder(UpdateOrderDTO requestData)
		{
			var checkOrderExist = await _iOrder.FindById(requestData.Id);
			if (checkOrderExist == null)
			{
				return NotFound(new
				{
					Success = false,
					Message = "Order is not exist"
				});
			}
			var checkUserExist = await _iUser.FindById(requestData.UserId);
			if (checkUserExist == null)
			{
				return NotFound(new
				{
					Success = false,
					Message = "UserId is not exist"
				});
			}
			_iOrder.UpdateItem(requestData);
			return Ok(new
			{
				Success = true,
				Message = "Success"
			});
		}

		[HttpDelete]
		[Route("Delete")]
		public async Task<ActionResult> DeleteOrder(int id)
		{
			var checkOrderExist = await _iOrder.FindById(id);
			if (checkOrderExist == null)
			{
				return NotFound(new
				{
					Success = false,
					Message = "Order is not exist"
				});
			}
			_iOrder.DeleteItem(checkOrderExist);
			return Ok(new
			{
				Seccess = true,
				Message = "Success"
			});
		}

	}
}
