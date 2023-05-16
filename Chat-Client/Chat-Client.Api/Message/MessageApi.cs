using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Chat_Client.Api.Request;
using Chat_Client.Api.Response;

namespace Chat_Client.Api.Message;

public class MessageApi : ApiBase, IMessageApi {
	public MessageApi(string baseUri) : base(baseUri) {
	}
	public async Task SendMessageToUserRequestAsync(string token, MessageToUser message) {
		await SendAsync(HttpMethod.Post, @"/messages", token, message);
	}
	public async Task<ICollection<MessageToUser>> GetUserMessagesRequestAsync(string token, int userId) {
		return await SendAsync<ICollection<MessageToUser>>(HttpMethod.Get, $@"/messages/{userId}", token).ConfigureAwait(false);
	}
	public async Task<ICollection<MessageFromUser>> GetNewUserMessageRequestAsync(string token, int userId) {
		return await SendAsync<ICollection<MessageFromUser>>(HttpMethod.Get, $@"/NewMessages/{userId}", token).ConfigureAwait(false);
	}
}