using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Chat_Server.Context;
using Chat_Server.Domain.Entities;

namespace Chat_Server.Services.Contacts;

public class ContactServices : IContactServices {
	public async Task AddUserContactByIdAsync(UserContact contact) {
		using var chatContext = new ChatDbContext();
		chatContext.UsersContacts.Add(contact);

		await chatContext.SaveChangesAsync();
	}

	public async Task<ICollection<User>> GetUserContactsByIdAsync(int userId) {
		using var chatContext = new ChatDbContext();
		return await chatContext.UsersContacts.Where(uc => uc.UserId == userId).Select(uc => uc.ContactUser).ToListAsync().ConfigureAwait(false);
	}

	public async Task<UserContact> GetUserContactByIdAsync(int userId, int userContactId) {
		using var chatContext = new ChatDbContext();
		return await chatContext.UsersContacts.FirstAsync(uc => uc.UserId == userId && uc.ContactUserId == userContactId).ConfigureAwait(false);
	}

	public async Task DeleteUserContactByIdAsync(UserContact contact) {
		using var chatContext = new ChatDbContext();
		chatContext.UsersContacts.Attach(contact);
		chatContext.UsersContacts.Remove(contact);

		await chatContext.SaveChangesAsync();
	}

	public async Task<bool> ContactExist(UserContact contact) {
		using var chatContext = new ChatDbContext();
		return await chatContext.UsersContacts.AnyAsync(uc => uc.UserId == contact.UserId && uc.ContactUserId == contact.ContactUserId).ConfigureAwait(false);
	}
}