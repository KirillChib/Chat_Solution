using System.Collections.Generic;
using System.Threading.Tasks;
using Chat_Server.Domain.Entities;

namespace Chat_Server.Services.Contacts;

public interface IContactServices {
	Task<bool> ContactExist(UserContact contact);
	Task DeleteUserContactByIdAsync(UserContact contact);
	Task AddUserContactByIdAsync(UserContact contact);
	Task<ICollection<User>> GetUserContactsByIdAsync(int userId);
	Task<UserContact> GetUserContactByIdAsync(int userId, int userContactId);
}