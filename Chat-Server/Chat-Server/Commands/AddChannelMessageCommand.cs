using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Chat_Server.Domain.Entities;
using Chat_Server.Extensions;
using Chat_Server.Helpers;
using Chat_Server.Request;
using Chat_Server.Services.Channels;
using Chat_Server.Services.JWT;

namespace Chat_Server.Commands {
	public class AddChannelMessageCommand : AuthorizationCommand {
		private const string IdKey = "id";

		public override string Path => $@"/channels/(?<{IdKey}>\d+)/message";
		public override HttpMethod Method => HttpMethod.Post;

		private readonly IChannelServices _channelServices;

		public AddChannelMessageCommand(IJwtService jwtService,IChannelServices channelServices) : base(jwtService) {
			_channelServices = channelServices;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, CheckJwtResult result) {
			var match = Regex.Match(context.Request.Url.AbsolutePath, Path, RegexOptions.IgnoreCase);
			var channelId = int.Parse(match.Groups[IdKey].Value);

			if (!await _channelServices.ChannelUserExistAsync(result.UserId, channelId)) {
				await context.WriteResponseAsync(400, "Вы не подписаны на канал");
				return;
			}

			var requestBody = await context.GetRequestBodyAsync();
			var messageRequest = JsonSerializeHelper.Deserialize<ChannelMessageRequest>(requestBody);

			var message = messageRequest.ToChannelMessage(channelId, result.UserId);

			await _channelServices.AddChannelMessageAsync(message).ConfigureAwait(false);
			await context.WriteResponseAsync(200, "Ok");
		}
	}
}