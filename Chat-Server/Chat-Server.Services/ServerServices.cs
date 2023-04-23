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
				PasswordHash = PasswordEncryptionHelper
			}

			 dBContext.Users.Add(user);
			await dBContext.SaveChangesAsync();
			return true;
		}
	}
}