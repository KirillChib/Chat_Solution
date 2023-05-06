using Chat_Server.Domain.Entities;
using Chat_Server.Extensions;
using Chat_Server.Helpers;
using Chat_Server.Request;
using Chat_Server.Services;
using Chat_Server.Services.JWT;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Chat_Server.Services.MessegeService;

namespace Chat_Server.Commands
{
	public class AddUserMessageCommand : AuthorizationCommand
	{
		public override string Path => @"/messages";
		public override HttpMethod Method => HttpMethod.Post;

		private IMessageService _messageServices;	

		public AddUserMessageCommand(IJwtService jwtService, IMessageService messageServices) : base(jwtService)
		{
			_messageServices = messageServices;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, CheckJWTResult result)
		{
			var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
			var message = JsonSerializeHelper.Deserialize<MessageToUser>(requestBody);

			var userMessage = new UserMessage
			{
				UserFromId = result.UserId,
				UserToId = message.UserToId,
				Message = message.Message,
				CreatedAt = message.CreateAt
			};
			if (message.File is null)
			{
				userMessage.HasFile = false;
				userMessage.FilePath = null;
			}
			else
			{
				userMessage.HasFile = true;
				userMessage.FilePath = FileHelper.SaveFile(ConfigurationsFiles.Path, message.FileName, message.File);
			}

			await _messageServices.AddUserMessageAsync(userMessage).ConfigureAwait(false);
			await context.WriteResponseAsync(200, "Ok");
		}
	}
}