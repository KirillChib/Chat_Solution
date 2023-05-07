using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Chat_Server.Extensions;
using Chat_Server.Services.Contacts;
using Chat_Server.Services.JWT;

namespace Chat_Server.Commands {
	public class DeleteContactCommand : AuthorizationCommand {
		private const string IdKey = "id";

		public override string Path => $@"/contacts/(?<{IdKey}>\d+)";
		public override HttpMethod Method => HttpMethod.Delete;

		private readonly IContactServices _contactServices;

		public DeleteContactCommand(IJwtService jwtService, IContactServices contactServices) : base(jwtService) {
			_contactServices = contactServices;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, CheckJwtResult result) {
			var match = Regex.Match(context.Request.Url.AbsolutePath, Path, RegexOptions.IgnoreCase);
			var contactId = int.Parse(match.Groups[IdKey].Value);

			var user = await _contactServices.GetUserContactByIdAsync(result.UserId, contactId).ConfigureAwait(false);
			if (user is null) {
				await context.WriteResponseAsync(404, "Контакт не найден").ConfigureAwait(false);
				return;
			}

			await _contactServices.DeleteUserContactByIdAsync(user).ConfigureAwait(false);
			await context.WriteResponseAsync(200, "Ok");
		}
	}
}