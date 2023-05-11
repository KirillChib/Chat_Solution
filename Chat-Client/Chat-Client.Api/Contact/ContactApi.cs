using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Chat_Client.Api.Response;

namespace Chat_Client.Api.Contact;

public class ContactApi : ApiBase, IContactApi{
	public ContactApi(string baseUri) : base(baseUri) {
	}
	public async Task<string> AddUserContactRequestAsync(int userId, string token) {
		var response = await SendAsync(HttpMethod.Post, $@"/contact/{userId}", token).ConfigureAwait(false);
		return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
	}
	public async Task<ICollection<UserContactResponse>> GetUserContactsRequestAsync(string token) {
		return await SendAsync<ICollection<UserContactResponse>>(HttpMethod.Get, @"/contacts", token).ConfigureAwait(false);
	}
	public async Task<string> DeleteUserContactRequestAsync(int userId, string token) {
		var response = await SendAsync(HttpMethod.Delete, $@"/contacts/{userId}", token).ConfigureAwait(false);
		return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
	}
}