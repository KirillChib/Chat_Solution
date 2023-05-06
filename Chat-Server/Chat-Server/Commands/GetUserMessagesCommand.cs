using Chat_Server.Extensions;
using Chat_Server.Helpers;
using Chat_Server.Response;
using Chat_Server.Services.JWT;
using Chat_Server.Services.Messege;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Chat_Server.Commands
{
	public class GetUserMessagesCommand : AuthorizationCommand
	{
		private const string IdKey = "Id";

		public override string Path => $@"/messages/(?<{IdKey}>\d+)";
		public override HttpMethod Method => HttpMethod.Get;

		private IMessageService _messageServices;

		public GetUserMessagesCommand(IJwtService jwtService, IMessageService messageServices) : base(jwtService)
		{
			_messageServices = messageServices;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, CheckJwtResult result)
		{
			var match = Regex.Match(context.Request.Url.AbsolutePath, Path, RegexOptions.IgnoreCase);
			var id = int.Parse(match.Groups[IdKey].Value);

			var response = new List<UserMessageResponse>();

			var messages = await _messageServices.GetUserMessagesByIdAsync(result.UserId, id).ConfigureAwait(false);

			if (messages.Count > 0)
			{
				foreach (var message in messages)
				{
					response.Add(message.ToResponseMessage());
				}
			}

			await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
		}
	}
}