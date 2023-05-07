using System.Linq;
using System.Threading.Tasks;
using Chat_Server.Context;
using Chat_Server.Domain.Entities;

namespace Chat_Server.Services.Users; 

public class UserServices : IUserServices {
	public async Task<User> AuthorizationUserAsync(string log, byte[] hash) {
		using var dbContext = new ChatDbContext();
		var user = await Task.Run(() => dbContext.Users.FirstOrDefault(u => u.Login == log));
		if (user is null)
			return null;

		if (user.PasswordHash.SequenceEqual(hash))
			return user;

		return null;
	}

	public async Task<bool> CreateUserAsync(User user) {
		using var dbContext = new ChatDbContext();
		if (await UserExist(user.Login, user.Name))
			return false;

		dbContext.Users.Add(user);
		await dbContext.SaveChangesAsync().ConfigureAwait(false);
		return true;
	}

	public async Task<bool> UserExist(string login, string name) {
		using var dbContext = new ChatDbContext();
		if (await Task.Run(() => dbContext.Users.Any(u => u.Login == login || u.Name == name)))
			return true;
		return false;
	}
}