using System.Threading.Tasks;
using Chat_Server.Domain.Entities;

namespace Chat_Server.Services.Channels; 

public interface IChannelServices {
	Task CreateChannelAsync(Channel channel);
}