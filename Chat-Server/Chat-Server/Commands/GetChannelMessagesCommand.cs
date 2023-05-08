using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Chat_Server.Extensions;
using Chat_Server.Helpers;
using Chat_Server.Response;
using Chat_Server.Services.Channels;
using Chat_Server.Services.JWT;

namespace Chat_Server.Commands;

public class GetChannelMessagesCommand : AuthorizationCommand {
	private const string IdKey = "id";

	public override string Path => $@"/channels/(?<{IdKey}>\d+)/messages";
	public override HttpMethod Method => HttpMethod.Get;

	private readonly IChannelServices _channelServices;

	public GetChannelMessagesCommand(IJwtService jwtService, IChannelServices channelServices) : base(jwtService) {
		_channelServices = channelServices;
	}

	protected override async Task HandleRequestInternalAsync(HttpListenerContext context, CheckJwtResult result) {
		var match = Regex.Match(context.Request.Url.AbsolutePath, Path, RegexOptions.IgnoreCase);
		var channelId = int.Parse(match.Groups[IdKey].Value);

		if (!await _channelServices.ChannelUserExistAsync(result.UserId, channelId).ConfigureAwait(false)) {
			await context.WriteResponseAsync(400, "Вы не подписаны на канал");
			return;
		}

		var response = new List<ChannelMessageResponse>();
		var messages = await _channelServices.GetChannelMessagesByChannelIdAsync(channelId).ConfigureAwait(false);

		foreach (var message in messages) {
			var userName = await _channelServices.GetChannelUserFromNameByIdAsync(message.UserFromId).ConfigureAwait(false);
			response.Add(message.ToChannelMessageResponse(userName));
		}


		await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
	}
}