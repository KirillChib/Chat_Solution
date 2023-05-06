using Chat_Server.Domain.Entities;
using Chat_Server.Extensions;
using Chat_Server.Helpers;
using Chat_Server.Request;
using Chat_Server.Services;
using Chat_Server.Services.Encryption;
using Chat_Server.Services.Users;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Chat_Server.Commands
{
	public class CreateUserCommand : ICommands
	{
		public string Path => @"/users";
		public HttpMethod Method => HttpMethod.Post;
		private IUserServices _userServices;
		private IEncryptionService _encryptionService;

		public CreateUserCommand(IUserServices userServices, IEncryptionService encryptionService)
		{
			_userServices = userServices;
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

			var tryCreated = await _userServices.CreateUserAsync(user).ConfigureAwait(false);

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