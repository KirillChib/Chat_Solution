using Chat_Server.Context;
using Chat_Server.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_Server.Services
{
	// todo(v): в отдельную папку
	public class ServerServices : IServerServices
	{
		// todo(v): контекст стоит пересоздавать на каждый запрос
		private ChatDBContext dBContext = new ChatDBContext();

		public async Task<User> AuthorizationUserAsync(string log, byte[] hash)
		{
			var user = await Task.Run(() => dBContext.Users.FirstOrDefault(u => u.Login == log));
			if (user is null)
				return null;

			if (user.PasswordHash.SequenceEqual(hash))
				return user;
			else
				return null;
		}

		// todo(v): Task вместо Task<bool>
		public async Task<bool> CreateUserAsync(User user)
		{
			// todo(v): можно сделать в один запрос
			// todo(v): для проверки существования пользователя лучше сделать отдельный метод
			if (await Task.Run(() => dBContext.Users.Any(u => u.Login == user.Login)) ||
				await Task.Run(() => dBContext.Users.Any(u => u.Name == user.Name)))
			{
				return false;
			}

			dBContext.Users.Add(user);
			await dBContext.SaveChangesAsync();
			return true;
		}
	}
}