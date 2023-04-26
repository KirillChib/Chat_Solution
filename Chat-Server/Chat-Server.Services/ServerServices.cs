using Chat_Server.Context;
using Chat_Server.Domain.Entities;
using Chat_Server.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_Server.Services
{
	public class ServerServices : IServerServices
	{
		private ChatDBContext dBContext = new ChatDBContext();

		public async Task<User> AuthorizationUserAsync(string log, string pass)
		{
			 var user = await Task.Run(() => dBContext.Users.FirstOrDefault(u => u.Login == log));
			if (user is null)
				return null;

			var hash = PasswordEncryptionHelper.ToHash(pass);
			if (user.PasswordHash.SequenceEqual(hash))
				return user;
			else
				return null;
		}
		//user.PasswordHash.SequenceEquals(hash)
		public async Task<bool> CreateUserAsync(string log, string pass, string name)
		{
			if(await Task.Run(() => dBContext.Users.Any(u => u.Login == log)) ||
				await Task.Run(() => dBContext.Users.Any(u => u.Name == name)))
			{
				return false;
			}

			var user = new User
			{
				Login = log,
				PasswordHash = PasswordEncryptionHelper.ToHash(pass),
				Name = name
			};

			 dBContext.Users.Add(user);
			await dBContext.SaveChangesAsync();
			return true;
		}
	}
}