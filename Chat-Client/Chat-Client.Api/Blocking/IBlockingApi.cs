using System.Collections.Generic;
using System.Threading.Tasks;
using Chat_Client.Api.Request;
using Chat_Client.Api.Response;

namespace Chat_Client.Api.Blocking; 

public interface IBlockingApi {
	Task<string> CreateBlockingRequestsAsync(string token, int userId);
	Task<ICollection<BlockingResponse>> GetBlockingsRequestAsync(string token);
	Task<string> DeleteBlockingRequestAsync(string token, int userId);
}