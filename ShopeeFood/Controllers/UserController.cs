using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopeeFood.Dtos.UserDTO;
using ShopeeFood.Interfaces;

namespace ShopeeFood.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize(AuthenticationSchemes = "Bearer")]
	public class UserController : Controller
	{
		private readonly IUser _iUser;
		public UserController(IUser iUser)
		{
			_iUser = iUser;
		}

		[HttpGet]
		public async Task<ActionResult> GetAll()
		{
			var users = _iUser.FindAll();
			return Ok( new{
				Success = true,
				Data = users,
				Message = "Success"
			});
;		}
		[HttpGet("{id}")]
		public async Task<ActionResult> GetById(int id)
		{
			var user = await _iUser.FindById(id);
			if(user == null)
			{
				return NotFound(new
				{
					Success = false,
					Message = "User is not found"
				});
			}
			return Ok(new
			{
				Success = true,
				Data = user,
				Message = "Success"
			});
		}

		[HttpPost]
		[Route("Create")]
		public async Task<ActionResult> CreateUser(CreateUserDTO requestData)
		{	
			if(requestData == null)
			{
				return BadRequest(new
				{
					Success = false,
					Message = "User is not found"
				});
			}
			//check User is exist
			var isUserExist = await _iUser.FindByName(requestData.UserName);
			if(isUserExist != null)
			{
				return BadRequest(new
				{
					Success = false,
					Message = "User is exist"
				});
			}
			_iUser.CreateItem(requestData);
			return Ok(new
			{
				Success = true,
				Message = "Success"
			});
		}

		[HttpPut]
		[Route("Update")]
		public async Task<ActionResult> UpdateUser(UpdateUserDTO requestData)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new
				{
					Success = false,
					Message = "The request is null"
				});
			}
			if (requestData == null)
			{
				return BadRequest(new
				{
					Success = false,
					Message = "The request is null"
				});
			}
			var userExists = await _iUser.FindById(requestData.Id);
			if(userExists == null)
			{
				return NotFound(new
				{
					Success = false,
					Message = "User is not exist"
				});
			}
			_iUser.UpdateItem(requestData);
			return Ok(new
			{
				Success = true,
				Message = "Success"
			});
		}

		[HttpDelete]
		[Route("Delete")]
		public async Task<ActionResult> DeleteUser(int Id)
		{
			var userExist = await _iUser.FindById(Id);
			if(userExist == null)
			{
				return NotFound(new
				{
					Success = false,
					Message = "User is not exist"
				});
			}
			_iUser.DeleteItem(userExist);
			return Ok(new
			{
				Success = true,
				Message = "Success"
			});
		}
	}
}
