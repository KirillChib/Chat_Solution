using Chat_Server.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Chat_Server.Services.JWT
{
	public class JwtService : IJwtService
	{
		private const string BearerPrefix = "Bearer ";

		private const string IdClaimKey = "Id";

		private readonly string _issuer;
		private readonly SecurityKey _securityKey;
		private readonly SigningCredentials _signingCredentials;

		public JwtService(string issuer, string secretKey)
		{
			_issuer = issuer;
			_securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
			_signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
		}

		public string GenerateToken(User user)
		{
			var claims = new[]
		{
			new Claim(IdClaimKey, user.Id.ToString()),
		};

			var jwtSecurityToken = new JwtSecurityToken(_issuer, null, claims, null, null, _signingCredentials);
			return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
		}

		public CheckJwtResult CheckToken(string token)
		{
			if (token?.StartsWith(BearerPrefix, StringComparison.OrdinalIgnoreCase) == true)
				token = token.Substring(BearerPrefix.Length);

			var parameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				ValidateIssuer = true,
				ValidIssuer = _issuer,
				ValidateAudience = false,
				ValidateLifetime = false,
				IssuerSigningKey = _securityKey
			};

			try
			{
				var claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(token, parameters, out var validatedToken);
				var userId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == IdClaimKey)?.Value;

				return new CheckJwtResult { UserId = int.Parse(userId)};
			}
			catch
			{
				return new CheckJwtResult { IsFaulted = true };
			}
		}
	}
}