using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat_Server.Domain.Entities;

namespace Chat_Server.Services.MessegeService
{
	public interface IMessageService
	{
		Task AddUserMessageAsync(UserMessage userMessage);
		Task<ICollection<UserMessage>> GetUserMessagesByIdAsync(int userFromId, int userToId);
		Task<ICollection<UserMessage>> GetNewUserMessagesByIdAsync(int userFromId, int userToId, DateTime date);
	}
}
