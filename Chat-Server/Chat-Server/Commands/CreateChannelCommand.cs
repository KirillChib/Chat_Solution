using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Chat_Server.Domain.Entities;
using Chat_Server.Extensions;
using Chat_Server.Helpers;
using Chat_Server.Request;
using Chat_Server.Services.Channels;
using Chat_Server.Services.JWT;

namespace Chat_Server.Commands;

public class CreateChannelCommand : AuthorizationCommand {
	public override string Path => @"/channels";
	public override HttpMethod Method => HttpMethod.Post;

	private readonly IChannelServices _channelServices;

	public CreateChannelCommand(IJwtService jwtService, IChannelServices channelServices) : base(jwtService) {
		_channelServices = channelServices;
	}

	protected override async Task HandleRequestInternalAsync(HttpListenerContext context, CheckJwtResult result) {
		var body = await context.GetRequestBodyAsync().ConfigureAwait(false);
		var request = JsonSerializeHelper.Deserialize<ChannelRequest>(body);

		if (await _channelServices.ChannelExistAsync(request.Name)) {
			await context.WriteResponseAsync(400, $"Канал с именем {request.Name} уже существует");
			return;
		}

		var channel = new Channel { Name = request.Name };

		await _channelServices.CreateChannelAsync(channel).ConfigureAwait(false);
		await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(channel));
	}
}