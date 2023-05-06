using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server
{
	// todo(v): переименовать
	public interface IServer : IDisposable
	{
		Task StartAsync(string uri);
	}
}
