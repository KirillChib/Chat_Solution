using System;
using System.Net.Http;
using System.Threading.Tasks;
using Chat_Client.Api.Request;

namespace Chat_Client.Api.Blocking; 

public class BlockingApi : ApiBase, IBlockingApi {
	public BlockingApi(string baseUri) : base(baseUri) {
	}
	public async Task<string> CreateBlockingRequestsAsync(BlockingRequest blocking, string token) {
		var response = await SendAsync(HttpMethod.Post, @"/blocking", token, blocking).ConfigureAwait(false);
		return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
	}
}