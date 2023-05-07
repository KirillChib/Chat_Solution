using Chat_Server.Domain.Entities;

namespace Chat_Server.Services.JWT; 

public interface IJwtService {
	string GenerateToken(User user);
	CheckJwtResult CheckToken(string token);
}