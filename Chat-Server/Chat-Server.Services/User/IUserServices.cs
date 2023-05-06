using Chat_Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server.Services
{
	public interface IUserServices
	{
		 Task<bool> CreateUserAsync(User user);
		Task<User> AuthorizationUserAsync(string log, byte[] hash);
	}
}
