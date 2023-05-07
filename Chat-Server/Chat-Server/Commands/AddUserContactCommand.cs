using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Chat_Server.Domain.Entities;
using Chat_Server.Extensions;
using Chat_Server.Helpers;
using Chat_Server.Request;
using Chat_Server.Services.Contacts;
using Chat_Server.Services.JWT;

namespace Chat_Server.Commands;

public class AddUserContactCommand : AuthorizationCommand {
	private const string IdKey = "id";

	public override string Path => $@"/contact/(?<{IdKey}>\d+)";
	public override HttpMethod Method => HttpMethod.Post;

	private readonly IContactServices _contactServices;

	public AddUserContactCommand(IJwtService jwtService, IContactServices contactServices) : base(jwtService) {
		_contactServices = contactServices;
	}

	protected override async Task HandleRequestInternalAsync(HttpListenerContext context, CheckJwtResult result) {
		var match = Regex.Match(context.Request.Url.AbsolutePath, Path, RegexOptions.IgnoreCase);
		var userContactId = int.Parse(match.Groups[IdKey].Value);

		var contact = new UserContact {
			UserId = result.UserId,
			ContactUserId = userContactId
		};

		if (await _contactServices.ContactExist(contact).ConfigureAwait(false))
		{
			await context.WriteResponseAsync(400, "В вашем списке уже есть этот контакт");
			return;
		}

		await _contactServices.AddUserContactByIdAsync(contact).ConfigureAwait(false);
		await context.WriteResponseAsync(200, "Ok");
	}
}