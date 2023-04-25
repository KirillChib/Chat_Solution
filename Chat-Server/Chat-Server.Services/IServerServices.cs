using Chat_Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server.Services
{
	public interface  IServerServices
	{
		 Task<bool> CreateUserAsync(string log,string pass,string name);
		Task<User> AuthorizationUserAsync(string log,string pass);
	}
}
