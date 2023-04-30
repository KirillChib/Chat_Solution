using Chat_Client.Api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.Api
{
	public abstract class ApiBase: IDisposable
	{
		private const string AuthorizationHeaderKey = "Authorization";

		private readonly string _baseUri;
		private readonly HttpClient _httpClient;

		protected ApiBase(string baseUri)
		{
			_baseUri = baseUri;
			_httpClient = new HttpClient();
		}

		protected async Task<HttpResponseMessage> SendAsync(
			HttpMethod method,
			string path,
			string token,
			object body = null)
		{
			var uri = _baseUri + path;

			var request = new HttpRequestMessage(method, uri);
			if (!string.IsNullOrEmpty(token))
				request.Headers.Add(AuthorizationHeaderKey, $"Bearer {token}");
			if (body != null)
				request.Content = new StringContent(JsonSerializeHelper.Serialize(body));

			var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
			//response.EnsureSuccessStatusCode();

			return response;
		}

		protected async Task<TResult> SendAsync<TResult>(
			HttpMethod method,
			string path,
			string token,
			object body = null)
		{
			var response = await SendAsync(method, path, token, body).ConfigureAwait(false);

			var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			return JsonSerializeHelper.Deserialize<TResult>(responseBody);
		}

		public void Dispose()
		{
			_httpClient.Dispose();
		}
	}
}
