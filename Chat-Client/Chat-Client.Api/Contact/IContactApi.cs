using System.Threading.Tasks;

namespace Chat_Client.Api.Contact; 

public interface IContactApi {
	Task<string> AddUserContactRequestAsync(int userId, string token);
}