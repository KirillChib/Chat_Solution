using System.Collections.Generic;
using System.Threading.Tasks;
using Chat_Client.Api.Response;

namespace Chat_Client.Api.Contact; 

public interface IContactApi {
	Task<string> AddUserContactRequestAsync(int userId, string token);
	Task<ICollection<UserContactResponse>> GetUserContactsRequestAsync(string token);
	Task<string> DeleteUserContactRequestAsync(int userId, string token);
}