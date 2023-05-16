using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chat_Client.Api.Request;
using Chat_Client.Api.Response;

namespace Chat_Client.Api.Channel; 

public interface IChannelApi {
	Task<ChannelResponse> CreateChannelRequestAsync(string token, ChannelRequest channel);
	Task<string> SubscribeChannelRequestAsync(string token, int channelId);
	Task<ICollection<ChannelResponse>> GetUserChannelsAsync(string token);
	Task<ICollection<ChannelResponse>> GetChannelByNameRequestAsync(string token, string channelName);
	Task<string> AddChannelMessageRequestAsync(ChannelMessageRequest message, string token, int channelId);
	Task<ICollection<ChannelMessageResponse>> GetChannelMessagesRequestAsync(string token, int channelId);
	Task<ICollection<ChannelMessageResponse>> GetNewChannelMessagesRequestAsync(string token, int channelId, DateTime date);
}