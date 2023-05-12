using System.Net.Http;
using System.Threading.Tasks;

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
}