using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat_Client.Api.Channel; 

public interface IChannelApi {
	Task<string> CreateChannelRequestAsync(string token, string channelName);
	Task<string> SubscribeChannelRequestAsync(string token, int channelId);
	Task<ICollection<Channel>> GetUserChannelsAsync(string token);
}