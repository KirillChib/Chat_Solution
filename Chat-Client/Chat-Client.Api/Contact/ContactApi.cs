using System.Net.Http;
using System.Threading.Tasks;

namespace Chat_Client.Api.Contact;

public class ContactApi : ApiBase, IContactApi{
	public ContactApi(string baseUri) : base(baseUri) {
	}
	public async Task<string> AddUserContactRequestAsync(int userId, string token) {
		var response = await SendAsync(HttpMethod.Post, $@"/contact/{userId}", token).ConfigureAwait(false);
		return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
	}
}