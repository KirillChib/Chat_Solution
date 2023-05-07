using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Chat_Server.Extensions;
using Chat_Server.Helpers;
using Chat_Server.Response;
using Chat_Server.Services.Contacts;
using Chat_Server.Services.JWT;

namespace Chat_Server.Commands;

public class GetUserContactsCommand : AuthorizationCommand {
	public override string Path => @"/contacts";
	public override HttpMethod Method => HttpMethod.Get;

	private readonly IContactServices _contactServices;

	public GetUserContactsCommand(IJwtService jwtService, IContactServices contactServices) : base(jwtService) {
		_contactServices = contactServices;
	}

	protected override async Task HandleRequestInternalAsync(HttpListenerContext context, CheckJwtResult result) {
		var contacts = await _contactServices.GetUserContactsByIdAsync(result.UserId).ConfigureAwait(false);

		var users = new List<UserContactResponse>();

		foreach (var contact in contacts)
			users.Add(contact.ToUserContactResponse());

		var response = JsonSerializeHelper.Serialize(users);

		await context.WriteResponseAsync(200, response);
	}
}