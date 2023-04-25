using Chat_Server.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server.Helpers
{
	public static  class GenerateTokenHelper
	{
		public static string GetToken(User user,string secretKey)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

			var header = new JwtHeader(credentials);

			var payload = new JwtPayload
		   {
			   { "id", user.Id},
			   { "login", user.Login},
				{"name",user.Name }
		   };

			var securityToken = new JwtSecurityToken(header, payload);
			var handler = new JwtSecurityTokenHandler();

			return handler.WriteToken(securityToken);

		}
	}
}
