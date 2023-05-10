using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Chat_Server.Domain.Entities;
using Chat_Server.Extensions;
using Chat_Server.Helpers;
using Chat_Server.Services.Blockings;
using Chat_Server.Services.JWT;

namespace Chat_Server.Commands;

public class CreateBlockingCommand : AuthorizationCommand {
	public override string Path => @"/blocking";
	public override HttpMethod Method => HttpMethod.Post;

	private readonly IBlockingServices _blockingServices;

	public CreateBlockingCommand(IJwtService jwtService, IBlockingServices blockingServices) : base(jwtService) {
		_blockingServices = blockingServices;
	}
	protected override async Task HandleRequestInternalAsync(HttpListenerContext context, CheckJwtResult result) {
		var requestBody = await context.GetRequestBodyAsync().ConfigureAwait(false);
		var blocking = JsonSerializeHelper.Deserialize<Blocking>(requestBody);

		if (await _blockingServices.BlockingExistAsync(blocking).ConfigureAwait(false)) {
			await context.WriteResponseAsync(400, "Этот пользователь уже есть в блокировках").ConfigureAwait(false);
			return;
		}

		await _blockingServices.CreateBlockingAsync(blocking);
		await context.WriteResponseAsync(200, "Ok").ConfigureAwait(false);
	}
}