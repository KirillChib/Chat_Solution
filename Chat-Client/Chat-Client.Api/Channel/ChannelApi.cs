using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Chat_Client.Api.Request;

namespace Chat_Client.Api.Channel;

public class ChannelApi : ApiBase, IChannelApi{
	public ChannelApi(string baseUri) : base(baseUri) {
	}
	public async Task<string> CreateChannelRequestAsync(string token, string channelName) {
		var response = await SendAsync(HttpMethod.Post, @"/channels", token).ConfigureAwait(false);
		return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
	}
	public async Task<string> SubscribeChannelRequestAsync(string token, int channelId) {
		var response = await SendAsync(HttpMethod.Post, $@"/channels/{channelId}", token).ConfigureAwait(false);
		return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
	}
	public async Task<ICollection<Channel>> GetUserChannelsAsync(string token) {
		return await SendAsync<ICollection<Channel>>(HttpMethod.Get, @"/channels/my", token).ConfigureAwait(false);
	}
	public async Task<ICollection<Channel>> GetChannelByNameRequestAsync(string token, string channelName) {
		return await SendAsync<ICollection<Channel>>(HttpMethod.Get, $@"/channels?Name={channelName}", token).ConfigureAwait(false);
	}
	public async Task<string> AddChannelMessageRequestAsync(ChannelMessageRequest message, string token, int channelId) {
		var response = await SendAsync(HttpMethod.Post, $@"/channels/{channelId}/message", token, message).ConfigureAwait(false);
		return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
	}
}