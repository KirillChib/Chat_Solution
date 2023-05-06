using Chat_Server.Extensions;
using Chat_Server.Helpers;
using Chat_Server.Response;
using Chat_Server.Services.JWT;
using Chat_Server.Services.Messege;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chat_Server.Commands
{
	public class GetNewUserMessagesCommand : AuthorizationCommand
	{
		private const string IdKey = "Id";
		private const string DateQueryKey = "Date";

		public override string Path => $@"/NewMessages/(?<{IdKey}>\d+)";
		public override HttpMethod Method => HttpMethod.Get;

		private readonly IMessageService _messageService;

		public GetNewUserMessagesCommand(IMessageService messageService, IJwtService jwtService) : base(jwtService)
		{
			_messageService = messageService;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, CheckJwtResult result)
		{
			var match = Regex.Match(context.Request.Url.AbsolutePath, Path, RegexOptions.IgnoreCase);
			var id = int.Parse(match.Groups[IdKey].Value);

			var date = DateTime.Parse(context.Request.QueryString[DateQueryKey]);

			var response = new List<UserMessageResponse>();

			var messages = await _messageService.GetNewUserMessagesByIdAsync(result.UserId, id, date);

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