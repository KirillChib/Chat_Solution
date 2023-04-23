using Chat_Server.Context;
using Chat_Server.Domain.Entities;
using System.Threading.Tasks;

namespace Chat_Server.Services
{
	public class ServerServices : IServerServices
	{
		private ChatDBContext dBContext = new ChatDBContext();
		
		public async Task CreateUserAsync(User user)
		{
			var currentsUsers = await dBContext.Users.Select()
		}
	}
}