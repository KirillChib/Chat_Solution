using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chat_Server.Domain.Entities;

namespace Chat_Server.Services.Messege; 

public interface IMessageService {
	Task AddUserMessageAsync(UserMessage userMessage);
	Task<ICollection<UserMessage>> GetUserMessagesByIdAsync(int userFromId, int userToId);
	Task<ICollection<UserMessage>> GetNewUserMessagesByIdAsync(int userFromId, int userToId, DateTime date);
}