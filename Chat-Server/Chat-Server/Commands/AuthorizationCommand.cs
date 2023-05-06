using Chat_Server.Extensions;
using Chat_Server.Services.JWT;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Chat_Server.Commands
{
	public abstract class AuthorizationCommand : ICommands
	{
		private const string AuthorizationHeaderKey = "Authorization";

		public abstract string Path { get;}
		public abstract HttpMethod Method { get;}
		private readonly IJwtService _jwtService;

		protected AuthorizationCommand(IJwtService jwtService)
		{
			_jwtService = jwtService;
		}

		public async Task HandleRequestAsync(HttpListenerContext context)
		{
			var checkTokenResult = _jwtService.CheckToken(context.Request.Headers[AuthorizationHeaderKey]);
			if (checkTokenResult.IsFaulted)
			{
				await context.WriteResponseAsync(401, "Unauthorized").ConfigureAwait(false);
				return;
			}

			await HandleRequestInternalAsync(context,checkTokenResult).ConfigureAwait(false);
		}

		protected abstract Task HandleRequestInternalAsync(HttpListenerContext context,CheckJwtResult result);
	}
}