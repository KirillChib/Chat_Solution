using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Chat_Client.Api.Request;
using Chat_Client.Api.Response;

namespace Chat_Client.Api.User; 


public class UserApi : ApiBase, IUserApi {
	public UserApi(string baseUri) : base(baseUri) {
	}

	public async Task<string> RegistrationRequestAsync(UserRegistration newUser) {
		var response = await SendAsync(HttpMethod.Post, "/users", null, newUser).ConfigureAwait(false);
		return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
	}

	public async Task<string> LoginRequestAsync(UserLogin user) {
		var response = await SendAsync(HttpMethod.Post, "/login", null, user).ConfigureAwait(false);
		return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
	}
	public async Task<ICollection<UserResponse>> GetAllUserRequest(string token) {
		return await SendAsync<ICollection<UserResponse>>(HttpMethod.Get, "/users", token).ConfigureAwait(false);
	}
}