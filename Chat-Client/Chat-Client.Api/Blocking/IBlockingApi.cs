using System.Collections.Generic;
using System.Threading.Tasks;
using Chat_Client.Api.Request;
using Chat_Client.Api.Response;

namespace Chat_Client.Api.Blocking; 

public interface IBlockingApi {
	Task<string> CreateBlockingRequestsAsync(BlockingRequest blocking, string token);
	Task<ICollection<BlockingResponse>> GetBlockingsRequestAsync(string token);
	Task<string> DeleteBlockingRequestAsync(BlockingRequest blocking, string token);
}