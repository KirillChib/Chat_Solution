using System.Collections.Generic;
using System.Threading.Tasks;
using Chat_Server.Domain.Entities;

namespace Chat_Server.Services.Blockings;

public interface IBlockingServices {
	Task CreateBlockingAsync(Blocking blocking);
	Task<ICollection<User>> GetBlockingsByUserIdAsync(int userId);
	Task<bool> BlockingExistAsync(Blocking blocking);
	Task DeleteBlockingAsync(Blocking blocking);
}