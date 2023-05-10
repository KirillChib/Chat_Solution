using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Chat_Client.Api.Request;

namespace Chat_Client.Api.Message;

public class MessageApi : ApiBase, IMessageApi {
	public MessageApi(string baseUri) : base(baseUri) {
	}
	public async Task SendMessageToUserRequestAsync(string token, MessageToUser message) {
		await SendAsync(HttpMethod.Get, @"/messages", token, message);
	}
	public async Task<ICollection<MessageToUser>> GetUserMessagesRequestAsync(string token, int userId) {
		return await SendAsync<ICollection<MessageToUser>>(HttpMethod.Get, $@"/messages/{userId}", token).ConfigureAwait(false);
	}
}