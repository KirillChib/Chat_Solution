using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Chat_Server.Extensions;
using Chat_Server.Helpers;
using Chat_Server.Services.Channels;
using Chat_Server.Services.JWT;

namespace Chat_Server.Commands;

public class GetChannelByNameCommand : AuthorizationCommand {
	private const string QueryKey = "Name";

	public override string Path => @"/channels";
	public override HttpMethod Method => HttpMethod.Get;

	private readonly IChannelServices _channelServices;

	public GetChannelByNameCommand(IJwtService jwtService, IChannelServices channelServices) : base(jwtService) {
		_channelServices = channelServices;
	}

	protected override async Task HandleRequestInternalAsync(HttpListenerContext context, CheckJwtResult result) {
		var param = context.Request.QueryString[QueryKey];

		var channels = await _channelServices.GetChannelsByNameAsync(param).ConfigureAwait(false);
		if (channels.Count == 0) {
			await context.WriteResponseAsync(404, "Not found").ConfigureAwait(false);
		}

		await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(channels)).ConfigureAwait(false);
	}
}