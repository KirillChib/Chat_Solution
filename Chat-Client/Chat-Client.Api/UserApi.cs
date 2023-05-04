using Chat_Client.Api.Request;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Chat_Client.Api
{
	// todo(v): в отдельную папку
	public class UserApi : ApiBase,IUserApi
	{
		public UserApi(string baseUri) : base(baseUri)
		{
		}

		public async Task<string> RegistrationRequestAsync(UserRegistration newUser)
		{
			var response = await SendAsync(HttpMethod.Post, "/users", null, newUser).ConfigureAwait(false);
			return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
		}

		public async Task<string> LoginRequestAsync(UserLogin user)
		{
			var response = await SendAsync(HttpMethod.Post, "/login", null, user).ConfigureAwait(false);
			return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
		}
	}
}