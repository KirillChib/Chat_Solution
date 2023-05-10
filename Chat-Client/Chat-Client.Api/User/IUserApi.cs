using System.Threading.Tasks;
using Chat_Client.Api.Request;

namespace Chat_Client.Api.User;

public interface IUserApi {
	Task<string> RegistrationRequestAsync(UserRegistration newUser);
	Task<string> LoginRequestAsync(UserLogin user);
}