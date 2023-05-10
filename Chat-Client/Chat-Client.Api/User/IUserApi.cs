using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chat_Client.Api.Request;
using Chat_Client.Api.Response;

namespace Chat_Client.Api.User;

public interface IUserApi {
	Task<string> RegistrationRequestAsync(UserRegistration newUser);
	Task<string> LoginRequestAsync(UserLogin user);
	Task<ICollection<UserResponse>> GetAllUserRequest(string token);
}