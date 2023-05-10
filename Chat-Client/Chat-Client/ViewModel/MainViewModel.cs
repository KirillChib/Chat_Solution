using Chat_Client.Api;
using Chat_Client.Api.User;
using GalaSoft.MvvmLight;

namespace Chat_Client.ViewModel
{
	public class MainViewModel : ViewModelBase
	{
		private const string BaseUri = "http://127.0.0.1:8888";
		private static readonly IUserApi UserApi = new UserApi(BaseUri);

		public MainViewModel()
		{
		}
	}
}