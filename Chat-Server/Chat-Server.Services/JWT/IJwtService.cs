using Chat_Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server.Services.JWT
{
	public interface IJwtService
	{
		string GenerateToken(User user);
		CheckJWTResult CheckToken(string token);
	}
}
