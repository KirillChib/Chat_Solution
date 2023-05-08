using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Chat_Server.Domain.Entities;
using Chat_Server.Extensions;
using Chat_Server.Services.Channels;
using Chat_Server.Services.JWT;

namespace Chat_Server.Commands;

public class SubscribeToChannelCommand : AuthorizationCommand {
	private const string IdKey = "id";

	public override string Path => $@"/channels/(?<{IdKey}>\d+)";
	public override HttpMethod Method => HttpMethod.Post;

	private readonly IChannelServices _channelServices;

	public SubscribeToChannelCommand(IJwtService jwtService, IChannelServices channelServices) : base(jwtService) {
		_channelServices = channelServices;
	}

	protected override async Task HandleRequestInternalAsync(HttpListenerContext context, CheckJwtResult result) {
		var match = Regex.Match(context.Request.Url.AbsolutePath, Path, RegexOptions.IgnoreCase);
		var channelId = int.Parse(match.Groups[IdKey].Value);

		var userid = result.UserId;

		if (await _channelServices.ChannelUserExistAsync(userid, channelId).ConfigureAwait(false)) {
			await context.WriteResponseAsync(400, "Вы уже подписаны на этот канал ").ConfigureAwait(false);
			return;
		}

		var channelUser = new ChannelUser { UserId = userid, ChannelId = channelId };

		await _channelServices.AddUserToChannelAsync(channelUser).ConfigureAwait(false);
		await context.WriteResponseAsync(200, "Ok").ConfigureAwait(false);
	}
}