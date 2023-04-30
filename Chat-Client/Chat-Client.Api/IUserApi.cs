using Chat_Client.Api.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.Api
{
	public interface IUserApi
	{
		 Task<string> RegistrationRequestAsync(UserRegistration newUser);
		Task<string> LoginRequestAsync(UserLogin user);
	}
}
