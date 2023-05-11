using System.Threading.Tasks;
using Chat_Client.Api.Request;

namespace Chat_Client.Api.Blocking; 

public interface IBlockingApi {
	Task<string> CreateBlockingRequestsAsync(BlockingRequest blocking, string token);
}