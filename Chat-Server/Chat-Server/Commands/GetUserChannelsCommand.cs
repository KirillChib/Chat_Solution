using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Chat_Server.Extensions;
using Chat_Server.Helpers;
using Chat_Server.Services.Channels;
using Chat_Server.Services.JWT;

namespace Chat_Server.Commands;

public class GetUserChannelsCommand : AuthorizationCommand {
	public override string Path => @"/channels/my";
	public override HttpMethod Method => HttpMethod.Get;

	private readonly IChannelServices _channelServices;

	public GetUserChannelsCommand(IJwtService jwtService, IChannelServices channelServices) : base(jwtService) {
		_channelServices = channelServices;
	}

	protected override async Task HandleRequestInternalAsync(HttpListenerContext context, CheckJwtResult result) {
		var channels = await _channelServices.GetChannelsByUserIdAsync(result.UserId).ConfigureAwait(false);
		if (channels.Count == 0) {
			await context.WriteResponseAsync(404, "Вы не подписаны ни на один канал").ConfigureAwait(false);
			return;
		}

		await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(channels)).ConfigureAwait(false);
	}
}