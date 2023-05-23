using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Chat_Server.Context;
using Chat_Server.Domain.Entities;

namespace Chat_Server.Services.Blockings; 

public class BlockingServices : IBlockingServices {
	public async Task CreateBlockingAsync(Blocking blocking) {
		using var chatContext = new ChatDbContext();
		chatContext.Blockings.Add(blocking);

		await chatContext.SaveChangesAsync().ConfigureAwait(false);
	}
	public async Task<ICollection<User>> GetBlockingsByUserIdAsync(int userId) {
		using var chatContext = new ChatDbContext();
		return await chatContext.Blockings.Where(b => b.UserId == userId).Select(b => b.BlockingUser).ToListAsync().ConfigureAwait(false);
	}
	public async Task<bool> BlockingExistAsync(Blocking blocking) {
		using var chatContext = new ChatDbContext();
		return await chatContext.Blockings.AnyAsync(b => b.UserId == blocking.UserId && b.BlockingUserId == blocking.BlockingUserId).ConfigureAwait(false);
	}
	public async Task DeleteBlockingAsync(Blocking blocking) {
		using var chatContext = new ChatDbContext();
		// todo(v): Attach или Add?
		chatContext.Blockings.Attach(blocking);
		chatContext.Blockings.Remove(blocking);

		await chatContext.SaveChangesAsync().ConfigureAwait(false);
	}
}