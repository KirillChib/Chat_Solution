using Chat_Server.Extensions;
using Chat_Server.Helpers;
using Chat_Server.Request;
using Chat_Server.Services;
using Chat_Server.Services.Encryption;
using Chat_Server.Services.JWT;
using Chat_Server.Services.Users;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Chat_Server.Commands
{
	public class AuthorizationUserCommand : ICommands
	{
		public string Path => @"/login";
		public HttpMethod Method => HttpMethod.Post;
		private IUserServices _userServices;
		private IEncryptionService _encryptionService;
		private IJwtService _jwtService;

		public AuthorizationUserCommand(IUserServices userServices,IEncryptionService encryptionService, IJwtService jwtService)
		{
			_userServices = userServices;
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

			var checkUser = await _userServices.AuthorizationUserAsync(user.Login,hash).ConfigureAwait(false);
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