using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Chat_Server.Extensions;
using Chat_Server.Helpers;
using Chat_Server.Response;
using Chat_Server.Services.JWT;
using Chat_Server.Services.Users;

namespace Chat_Server.Commands {
	public class GetAllUsersCommand : AuthorizationCommand {
		public override string Path => @"/users";
		public override HttpMethod Method => HttpMethod.Get;

		private readonly IUserServices _userServices;

		public GetAllUsersCommand(IJwtService jwtService, IUserServices userServices) : base(jwtService) {
			_userServices = userServices;
		}

		protected override async Task HandleRequestInternalAsync(HttpListenerContext context, CheckJwtResult result) {
			var users = await _userServices.GetAllUsersAsync().ConfigureAwait(false);
			var usersResponse = new List<UserContactResponse>();

			foreach (var user in users)
				usersResponse.Add(user.ToUserContactResponse());

			var response = JsonSerializeHelper.Serialize(usersResponse);

			await context.WriteResponseAsync(200, response).ConfigureAwait(false);
		}
	}
}