using Chat_Server.Context;
using Chat_Server.Domain.Entities;
using Chat_Server.Services.Encryption;
using GalaSoft.MvvmLight;

namespace Chat_Server.ViewModel; 

public class MainViewModel : ViewModelBase {
	private const string ServerUri = "http://127.0.0.1:8888/";

	public MainViewModel() {

		// todo(v): ���� ��� ����� �������� ���� ��� ������� Loaded, ���� � App.cs
		CreateServer().StartAsync(ServerUri).ConfigureAwait(false);
	}

	private IServer CreateServer() {
		return Locator.Current.Locate<IServer>();
	}
}