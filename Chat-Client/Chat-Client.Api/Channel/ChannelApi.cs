using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Chat_Client.Api.Request;
using Chat_Client.Api.Response;

namespace Chat_Client.Api.Channel;

public class ChannelApi : ApiBase, IChannelApi{
	public ChannelApi(string baseUri) : base(baseUri) {
	}
	public async Task<ChannelResponse> CreateChannelRequestAsync(string token, ChannelRequest channel) {
		 return await SendAsync<ChannelResponse>(HttpMethod.Post, @"/channels", token, channel).ConfigureAwait(false);
	}
	public async Task<string> SubscribeChannelRequestAsync(string token, int channelId) {
		var response = await SendAsync(HttpMethod.Post, $@"/channels/{channelId}", token).ConfigureAwait(false);
		return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
	}
	public async Task<ICollection<ChannelResponse>> GetUserChannelsAsync(string token) {
		return await SendAsync<ICollection<ChannelResponse>>(HttpMethod.Get, @"/channels/my", token).ConfigureAwait(false);
	}
	public async Task<ICollection<ChannelResponse>> GetChannelByNameRequestAsync(string token, string channelName) {
		return await SendAsync<ICollection<ChannelResponse>>(HttpMethod.Get, $@"/channels?Name={channelName}", token).ConfigureAwait(false);
	}
	public async Task<string> AddChannelMessageRequestAsync(ChannelMessageRequest message, string token, int channelId) {
		var response = await SendAsync(HttpMethod.Post, $@"/channels/{channelId}/message", token, message).ConfigureAwait(false);
		return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
	}
	public async Task<ICollection<ChannelMessageResponse>> GetChannelMessagesRequestAsync(string token, int channelId) {
		return await SendAsync<ICollection<ChannelMessageResponse>>(HttpMethod.Get, $@"/channels/{channelId}/messages", token).ConfigureAwait(false);
	}
	public async Task<ICollection<ChannelMessageResponse>> GetNewChannelMessagesRequestAsync(string token, int channelId, DateTime date) {
		return await SendAsync<ICollection<ChannelMessageResponse>>(HttpMethod.Get, $@"/channels/{channelId}/messages/new?Date={date}", token).ConfigureAwait(false);
	}
}