using System;
using System.Threading.Tasks;

namespace Chat_Server; 

// todo(v): переименовать
public interface IServer : IDisposable {
	Task StartAsync(string uri);
}