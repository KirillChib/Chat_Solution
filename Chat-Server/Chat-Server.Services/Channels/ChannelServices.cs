using System.Threading.Tasks;
using Chat_Server.Context;
using Chat_Server.Domain.Entities;

namespace Chat_Server.Services.Channels;

public class ChannelServices : IChannelServices {
	public async Task CreateChannelAsync(Channel channel) {
		using var chatContext = new ChatDbContext();
		chatContext.Channels.Add(channel);

		await chatContext.SaveChangesAsync().ConfigureAwait(false);
	}
}