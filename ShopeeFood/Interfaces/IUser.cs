using ShopeeFood.Dtos;
using ShopeeFood.Dtos.AuthDTO;
using ShopeeFood.Dtos.UserDTO;
using ShopeeFood.Models;

namespace ShopeeFood.Interfaces
{
	public interface IUser
	{
		public Task<List<User>> FindAll();
		public Task<User> FindById(int id);
		public Task<User> FindByName(string theName);
		public void CreateItem(CreateUserDTO requestData);
		public void UpdateItem(UpdateUserDTO requestData);
		public void DeleteItem(User requestData);
		public bool Register(RegisterRequestDTO requestData);
	}
}
