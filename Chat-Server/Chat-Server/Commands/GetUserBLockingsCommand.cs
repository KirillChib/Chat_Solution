using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Chat_Server.Extensions;
using Chat_Server.Helpers;
using Chat_Server.Response;
using Chat_Server.Services.Blockings;
using Chat_Server.Services.JWT;

namespace Chat_Server.Commands;

public class GetUserBLockingsCommand : AuthorizationCommand {
	public override string Path => @"/blockings";
	public override HttpMethod Method => HttpMethod.Get;

	private readonly IBlockingServices _blockingServices;

	public GetUserBLockingsCommand(IJwtService jwtService, IBlockingServices blockingServices) : base(jwtService) {
		_blockingServices = blockingServices;
	}

	protected override async Task HandleRequestInternalAsync(HttpListenerContext context, CheckJwtResult result) {
		var response = new List<BlockingResponse>();

		var blockings = await _blockingServices.GetBlockingsByUserIdAsync(result.UserId).ConfigureAwait(false);

		// todo(v): можно переписать на linq
		foreach (var block in blockings) 
			response.Add(block.ToBlockingResponse());

		await context.WriteResponseAsync(200, JsonSerializeHelper.Serialize(response)).ConfigureAwait(false);
	}
}