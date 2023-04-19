using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server.Commands
{
	public interface ICommand
	{
		string Path { get; }
		HttpMethod Method { get; }
		Task HandleRequestAsync(HttpListenerContext context);
	}
}
