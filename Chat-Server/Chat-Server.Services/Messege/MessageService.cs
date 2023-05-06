using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Chat_Server.Context;
using Chat_Server.Domain.Entities;
using Chat_Server.Services.MessegeService;

namespace Chat_Server.Services.Messege;

public class MessageService: IMessageService
{
	public async Task AddUserMessageAsync(UserMessage userMessage)
	{
		using var chatContext = new ChatContext();
		chatContext.UserMessages.Add(userMessage);
		await chatContext.SaveChangesAsync().ConfigureAwait(false);
	}

	public async Task<ICollection<UserMessage>> GetUserMessagesByIdAsync(int userFromId, int userToId)
	{
		using var chatContext = new ChatContext();
		return await  chatContext.UserMessages
			.Where(um => (um.UserFromId == userFromId && um.UserToId == userToId) || (um.UserFromId == userToId && um.UserToId == userFromId))
			.OrderBy(um => um.CreatedAt).ToListAsync().ConfigureAwait(false);
	}

	public async Task<ICollection<UserMessage>> GetNewUserMessagesByIdAsync(int userFromId, int userToId, DateTime date)
	{
		using var chatContext = new ChatContext();
		return await chatContext.UserMessages
			.Where(um => um.UserFromId == userFromId && um.UserToId == userToId && um.CreatedAt > date)
			.OrderBy(um => um.CreatedAt).ToListAsync().ConfigureAwait(false);
	}
}