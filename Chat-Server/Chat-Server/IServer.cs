using System;
using System.Threading.Tasks;

namespace Chat_Server; 

public interface IServer : IDisposable {
	Task StartAsync(string uri);
}