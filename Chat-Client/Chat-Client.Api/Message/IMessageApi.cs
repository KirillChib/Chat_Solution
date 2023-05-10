using System.Threading.Tasks;
using Chat_Client.Api.Request;

namespace Chat_Client.Api.Message;

public interface IMessageApi {
	Task SendMessageToUserRequestAsync(string token, MessageToUser message);
}