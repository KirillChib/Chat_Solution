using Chat_Server.Domain.Entities;
using Chat_Server.Extensions;
using Chat_Server.Helpers;
using Chat_Server.Request;
using Chat_Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server.Commands
{
	public class CreateUserCommand : ICommand
	{
		public string Path => @"/users";
		public HttpMethod Method => HttpMethod.Post;
		private IServerServices _serverServices;

		public CreateUserCommand(IServerServices serverServices)
		{
			this._serverServices = serverServices;
		}

		public async Task HandleRequestAsync(HttpListenerContext context)
		{
			var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
			if (!JsonSerializeHelper.TryDeserialize<RegistrationsUser>(requestBody, out var user))
			{
				await context.WriteResponseAsync(400, "Invalid request body content").ConfigureAwait(false);
				return;
			}


			var tryCreated = await _serverServices.CreateUserAsync(user.Login,user.Password,user.Name).ConfigureAwait(false);

			if(!tryCreated)
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
