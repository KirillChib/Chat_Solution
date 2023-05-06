using Chat_Server.Domain.Entities;
using System.Threading.Tasks;

namespace Chat_Server.Services.Users
{
	public interface IUserServices
	{
		Task<bool> CreateUserAsync(User user);
		Task<User> AuthorizationUserAsync(string log, byte[] hash);
		Task<bool> UserExist(string login, string name);
	}
}