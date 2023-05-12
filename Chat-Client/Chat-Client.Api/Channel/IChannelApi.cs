using System.Threading.Tasks;

namespace Chat_Client.Api.Channel; 

public interface IChannelApi {
	Task<string> CreateChannelRequestAsync(string token, string channelName);
}