using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
	public async Task<bool> ChannelExistAsync(string name) {
		using var chatContext = new ChatDbContext();
		return await chatContext.Channels.AnyAsync(c => c.Name == name).ConfigureAwait(false);
	}
	public async Task<ICollection<Channel>> GetChannelsByNameAsync(string name) {
		using var chatContext = new ChatDbContext();
		return await chatContext.Channels.Where(c => c.Name.StartsWith(name, StringComparison.Ordinal)).ToListAsync().ConfigureAwait(false);
	}
	public async Task<ICollection<Channel>> GetChannelsByUserIdAsync(int userId) {
		using var chatContext = new ChatDbContext();
		return await chatContext.ChannelsUsers.Where(cu => cu.UserId == userId).Select(cu => cu.Channel).ToListAsync().ConfigureAwait(false);
	}
	public async Task AddChannelMessageAsync(ChannelMessage message) {
		using var chatContext = new ChatDbContext();
		chatContext.ChannelMessages.Add(message);

		await chatContext.SaveChangesAsync();
	}
	public async Task<ICollection<ChannelMessage>> GetChannelMessagesByChannelIdAsync(int channelId) {
		using var chatContext = new ChatDbContext();
		return await chatContext.ChannelMessages
			.Where(cm => cm.ChannelId == channelId)
			.OrderBy(cm => cm.CreatedAt)
			.ToListAsync().ConfigureAwait(false);
	}
	public async Task<ICollection<ChannelMessage>> GetNewChannelMessagesAsync(int channelId, DateTime date) {
		using var chatContext = new ChatDbContext();
		return await chatContext.ChannelMessages
			.Where(cm => cm.CreatedAt > date)
			.OrderBy(cm => cm.CreatedAt)
			.ToListAsync().ConfigureAwait(false);
	}
	public async Task AddUserToChannelAsync(ChannelUser channelUser) {
		using var chatContext = new ChatDbContext();
		chatContext.ChannelsUsers.Add(channelUser);

		await chatContext.SaveChangesAsync();
	}
	public async Task DeleteUserFromChannelAsync(ChannelUser channelUser) {
		using var chatContext = new ChatDbContext();
		chatContext.ChannelsUsers.Attach(channelUser);
		chatContext.ChannelsUsers.Remove(channelUser);

		await chatContext.SaveChangesAsync();
	}
	public async Task<bool> ChannelUserExistAsync(int userId, int channelId) {
		using var chatContext = new ChatDbContext();
		return await chatContext.ChannelsUsers.AnyAsync(cu => cu.UserId == userId && cu.ChannelId == channelId).ConfigureAwait(false);
	}
}