using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Chat_Client.Api.Request;
using Chat_Client.Api.Response;

namespace Chat_Client.Api.Blocking;

public class BlockingApi : ApiBase, IBlockingApi {
	public BlockingApi(string baseUri) : base(baseUri) {
	}
	public async Task<string> CreateBlockingRequestsAsync(string token, int userId) {
		var response = await SendAsync(HttpMethod.Post, @"/blocking", token).ConfigureAwait(false);
		return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
	}
	public async Task<ICollection<BlockingResponse>> GetBlockingsRequestAsync(string token) {
		return await SendAsync<ICollection<BlockingResponse>>(HttpMethod.Get, @"/blockings", token).ConfigureAwait(false);
	}
	public async Task<string> DeleteBlockingRequestAsync(string token, int userId) {
		var response = await SendAsync(HttpMethod.Delete, $@"/blocking/{userId}", token).ConfigureAwait(false);
		return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
	}
}