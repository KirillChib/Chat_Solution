using System;
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

public class GetNewChannelMessagesCommand : AuthorizationCommand {
	private const string IdKey = "id";
	private const string QueryKey = "Date";

	public override string Path => $@"/channels/(?<{IdKey}>\d+)/messages/new";
	public override HttpMethod Method => HttpMethod.Get;

	private readonly IChannelServices _channelServices;

	public GetNewChannelMessagesCommand(IJwtService jwtService, IChannelServices channelServices) : base(jwtService) {
		_channelServices = channelServices;
	}

	protected override async Task HandleRequestInternalAsync(HttpListenerContext context, CheckJwtResult result) {
		var match = Regex.Match(context.Request.Url.AbsolutePath, Path, RegexOptions.IgnoreCase);
		var channelId = int.Parse(match.Groups[IdKey].Value);

		var param = context.Request.QueryString[QueryKey];
		var date = DateTime.Parse(param);

		var messages = await _channelServices.GetNewChannelMessagesAsync(channelId, date).ConfigureAwait(false);
		if (messages.Count == 0) {
			await context.WriteResponseAsync(404, "Not founds");
			return;
		}

		var response = new List<ChannelMessageResponse>();

		foreach (var message in messages) {
			var name = await _channelServices.GetChannelUserFromNameByIdAsync(message.UserFromId).ConfigureAwait(false);
			message.ToChannelMessageResponse(name);
		}

		await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
	}
}