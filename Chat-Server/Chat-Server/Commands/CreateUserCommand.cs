using Chat_Server.Domain.Entities;
using Chat_Server.Extensions;
using Chat_Server.Helpers;
using Chat_Server.Request;
using Chat_Server.Services;
using Chat_Server.Services.Encryption;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Chat_Server.Commands
{
	public class CreateUserCommand : ICommand
	{
		public string Path => @"/users";
		public HttpMethod Method => HttpMethod.Post;
		private IServerServices _serverServices;
		private IEncryptionService _encryptionService;

		public CreateUserCommand(IServerServices serverServices, IEncryptionService encryptionService)
		{
			_serverServices = serverServices;
			_encryptionService = encryptionService;
		}

		public async Task HandleRequestAsync(HttpListenerContext context)
		{
			var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
			if (!JsonSerializeHelper.TryDeserialize<RegistrationsUser>(requestBody, out var registrationUser))
			{
				await context.WriteResponseAsync(400, "Invalid request body content").ConfigureAwait(false);
				return;
			}

			var user = new User
			{
				Login = registrationUser.Login,
				PasswordHash = _encryptionService.PasswordToHash(registrationUser.Password),
				Name = registrationUser.Name
			};

			var tryCreated = await _serverServices.CreateUserAsync(user).ConfigureAwait(false);

			if (!tryCreated)
			{
				await context.WriteResponseAsync(400, "Такой логин или имя уже используется");
			}
			else
			{
				await context.WriteResponseAsync(200, "Учетная запись создана");
			}
		}
	}
}