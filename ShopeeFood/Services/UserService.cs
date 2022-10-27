using Microsoft.EntityFrameworkCore;
using ShopeeFood.Dtos;
using ShopeeFood.Dtos.AuthDTO;
using ShopeeFood.Dtos.UserDTO;
using ShopeeFood.Interfaces;
using ShopeeFood.Models;
using TodoApi.Constants;


namespace ShopeeFood.Services
{
	public class UserService : IUser
	{
		private readonly todoAPIContext _todoContext;
		public UserService(todoAPIContext todoContext)
		{
			_todoContext = todoContext;
		}

		public Task<List<User>> FindAll()
		{
			var users = _todoContext.Users.Include(c => c.Orders).ToListAsync();
			return users;
		}

		public Task<User> FindById(int id)
		{
			var user =  _todoContext.Users.FirstOrDefaultAsync(x => x.Id == id);
			if (user == null)
			{
				return null;
			}
			return user;
		}

		public Task<User> FindByName(string theName)
		{
			var item = _todoContext.Users.FirstOrDefaultAsync(m => m.UserName == theName);
			if (item == null)
			{
				return null;
			}
			return item;
		}

		public void CreateItem(CreateUserDTO requestData)
		{
			if (requestData == null)
			{
				return;
			}
			var userItem = new User();
			userItem.UserName = requestData.UserName;
			userItem.UserAge = requestData.UserAge;
			//get current day if createDate is null
			if (requestData.CreatedDate == null)
			{
				userItem.CreatedDate = DateTime.Now;
			}
			else
			{
				userItem.CreatedDate = requestData.CreatedDate;
			}
			userItem.Email = requestData.Email;
			userItem.PhoneNumber = requestData.PhoneNumber;
			userItem.Address = requestData.Address;
			userItem.Password = requestData.Password;
			if (requestData.Genre == null)
			{
				userItem.Genre = GenreConstant.Male;
			}
			else
			{
				userItem.Genre = requestData.Genre;
			}
			if (requestData.Role == null)
			{
				userItem.Genre = RoleConstant.USER;
			}
			else
			{
				userItem.Role = requestData.Role;
			}

			_todoContext.Users.Add(userItem);
			_todoContext.SaveChanges();
		}

		public void UpdateItem(UpdateUserDTO requestData)
		{
			var existUser = _todoContext.Users.FirstOrDefault(x => x.Id == requestData.Id);

			if (existUser is null)
			{
				return;
			};
			if ((requestData.UserName != string.Empty) & (requestData.UserName != "string"))
			{
				existUser.UserName = requestData.UserName;
			}
			if (requestData.UserAge > 0)
			{
				existUser.UserAge = requestData.UserAge;
			}
			if ((requestData.Email != string.Empty) & (requestData.Email != "string"))
			{
				existUser.Email = requestData.Email;
			}
			if ((requestData.PhoneNumber != string.Empty) & (requestData.PhoneNumber != "string"))
			{
				existUser.PhoneNumber = requestData.PhoneNumber;
			}
			if ((requestData.Address != string.Empty) & (requestData.Address != "string"))
			{
				existUser.Address = requestData.Address;
			}
			if (requestData.Genre > 0)
			{
				existUser.Genre = requestData.Genre;
			}
			if (requestData.Role > 0)
			{
				existUser.Role = requestData.Role;
			}
			if ((requestData.Password != string.Empty) & (requestData.Password != "string"))
			{
				existUser.Password = requestData.Password;
			}
			//_mapper.Map(requestData, existUser);
			_todoContext.ChangeTracker.Clear();
			_todoContext.Users.Update(existUser);
			_todoContext.SaveChanges();
		}

		public void DeleteItem(User userExist)
		{
			_todoContext.Users.Remove(userExist);
			_todoContext.SaveChanges();
		}

		public bool Register(RegisterRequestDTO requestData)
		{
			var isExistedItem = _todoContext.Users.FirstOrDefault(u => u.Email == requestData.Email);
			if (isExistedItem != null)
			{
				return false;
			}
			var userItem = new User()
			{
				UserName = requestData.Username,
				Email = requestData.Email,
				Address = string.Empty,
				Password = requestData.Password,
				Genre = GenreConstant.Male,
				UserAge = 0,
				RefreshToken = string.Empty,
				CreatedDate = DateTime.Now,
				RefreshTokenExpiryTime = DateTime.Now,
				PhoneNumber = string.Empty,
				Role = RoleConstant.USER
			};
			_todoContext.Users.Add(userItem);
			_todoContext.SaveChanges();
			return true;
		}

	}
}
