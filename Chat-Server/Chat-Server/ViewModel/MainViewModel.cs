using Chat_Server.Context;
using Chat_Server.Domain.Entities;
using GalaSoft.MvvmLight;

namespace Chat_Server.ViewModel
{
	public class MainViewModel : ViewModelBase
	{
		private const string ServerUri = "http://127.0.0.1:8888/";

		public MainViewModel()
		{
			// todo(v): этот код стоит вызывать либо при событии Loaded, либо в App.cs
			CreateServer().StartAsync(ServerUri).ConfigureAwait(false);
		}

		private Iserver CreateServer()
		{
			return Locator.Current.Locate<Iserver>();
		}
	}
}