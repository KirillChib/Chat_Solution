using System.Collections.Generic;
using System.Threading.Tasks;
using Chat_Client.Api.Request;
using Chat_Client.Api.Response;

namespace Chat_Client.Api.Channel; 

public interface IChannelApi {
	Task<string> CreateChannelRequestAsync(string token, string channelName);
	Task<string> SubscribeChannelRequestAsync(string token, int channelId);
	Task<ICollection<Channel>> GetUserChannelsAsync(string token);
	Task<ICollection<Channel>> GetChannelByNameRequestAsync(string token, string channelName);
	Task<string> AddChannelMessageRequestAsync(ChannelMessageRequest message, string token, int channelId);
	Task<ICollection<ChannelMessageResponse>> GetChannelMessagesRequestAsync(string token, int channelId);
}