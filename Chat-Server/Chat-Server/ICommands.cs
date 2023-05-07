using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Chat_Server; 

public interface ICommands {
	string Path { get; }
	HttpMethod Method { get; }
	Task HandleRequestAsync(HttpListenerContext context);
}