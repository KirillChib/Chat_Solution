using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chat_Server.Domain.Entities;

namespace Chat_Server.Services.Channels; 

public interface IChannelServices {
	Task CreateChannelAsync(Channel channel);
	Task<bool> ChannelExistAsync(string name);
	Task<ICollection<Channel>> GetChannelsByNameAsync(string name);
	Task<ICollection<Channel>> GetChannelsByUserIdAsync(int userId);
	Task AddChannelMessageAsync(ChannelMessage message);
	Task<ICollection<ChannelMessage>> GetChannelMessagesByChannelIdAsync(int channelId);
	Task<ICollection<ChannelMessage>> GetNewChannelMessagesAsync(int channelId, DateTime date);
	Task AddUserToChannelAsync(ChannelUser channelUser);
	Task DeleteUserFromChannelAsync(ChannelUser channelUser);
	Task<bool> ChannelUserExistAsync(int userId,int channelId);
	Task<string> GetChannelUserFromNameByIdAsync(int userId);
}