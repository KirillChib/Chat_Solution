using Chat_Server.Extensions;
using Chat_Server.Helpers;
using Chat_Server.Request;
using Chat_Server.Services;
using Chat_Server.Services.Encryption;
using Chat_Server.Services.JWT;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Chat_Server.Commands
{
	public class AuthorizationUserCommand : ICommand
	{
		public string Path => @"/login";
		public string SecretKey { get; set; } = "TW9zaGVFcmV6UHJpdmF0ZUtleQ==";
		public HttpMethod Method => HttpMethod.Post;
		private IUserServices _serverServices;
		private IEncryptionService _encryptionService;
		private IJwtService _jwtService;

		public AuthorizationUserCommand(IUserServices serverServices,IEncryptionService encryptionService, IJwtService jwtService)
		{
			_serverServices = serverServices;
			_encryptionService = encryptionService;
			_jwtService = jwtService;
		}

		public async Task HandleRequestAsync(HttpListenerContext context)
		{
			var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
			if (!JsonSerializeHelper.TryDeserialize<AuthorizationUser>(requestBody, out var user))
			{
				await context.WriteResponseAsync(400, "Invalid request body content").ConfigureAwait(false);
				return;
			}

			var hash = _encryptionService.PasswordToHash(user.Password);

			var checkUser = await _serverServices.AuthorizationUserAsync(user.Login,hash).ConfigureAwait(false);
			if(checkUser is null)
			{
				await context.WriteResponseAsync(401, "Invalid username or password").ConfigureAwait(false);
				return;
			}

			var token = _jwtService.GenerateToken(checkUser);
			await context.WriteResponseAsync(200, token);
		}
	}
}