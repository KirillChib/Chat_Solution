using Chat_Server.Context;
using Chat_Server.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_Server.Services
{
	public class ServerServices : IServerServices
	{
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

		public async Task<bool> CreateUserAsync(User user)
		{
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