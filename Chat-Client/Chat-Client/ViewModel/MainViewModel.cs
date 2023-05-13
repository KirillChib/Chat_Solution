using AsyncCommandLibrary;
using Chat_Client.Api;
using Chat_Client.Api.Blocking;
using Chat_Client.Api.Channel;
using Chat_Client.Api.Contact;
using Chat_Client.Api.Message;
using Chat_Client.Api.Response;
using Chat_Client.Api.User;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chat_Client.ViewModel
{
	public class MainViewModel : ViewModelBase
	{
		private const string BaseUri = "http://127.0.0.1:8888";
		private readonly IUserApi _userApi = new UserApi(BaseUri);
		private readonly IMessageApi _messageApi = new MessageApi(BaseUri);
		private readonly IContactApi _contactApi = new ContactApi(BaseUri);
		private readonly IChannelApi _channelApi = new ChannelApi(BaseUri);
		private readonly IBlockingApi _blockingApi = new BlockingApi(BaseUri);

		private string _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjUiLCJpc3MiOiJDaGF0LVNvbHV0aW9uIn0.fLFJ_dObDJZtbUyN7KeUpubYHDpwtQG6C_gBLpuhipU";
		private ObservableCollection<UserResponse> users;
		private ObservableCollection<UserContactResponse> contacts;
		private ObservableCollection<Channel> channels;

		private ICommand getUsersCommand;

		
		//public ICommand GetUsersCommand => getUsersCommand ??= new AsyncRelayCommand(GetUsersAsync);

		public MainViewModel()
		{
			GetUsersAsync();
			GetContactsAsync();
			GetChannelsAsync();
		}

		public ObservableCollection<UserResponse> Users { get => users; set =>Set(ref users,value); }
		public ObservableCollection<UserContactResponse> Contacts { get => contacts; set => Set(ref contacts,value); }
		public ObservableCollection<Channel> Channels { get => channels; set => Set(ref channels,value); }

		private async Task GetUsersAsync()
		{
			var users = await _userApi.GetAllUserRequest(_token).ConfigureAwait(false);
			Users = new ObservableCollection<UserResponse>(users);
		}

		private async Task GetContactsAsync()
		{
			var cntacts = await _contactApi.GetUserContactsRequestAsync(_token).ConfigureAwait(false);
			Contacts = new ObservableCollection<UserContactResponse>(cntacts);
		}

		private async Task GetChannelsAsync()
		{
			var channels = await _channelApi.GetUserChannelsAsync(_token).ConfigureAwait(false);
			Channels = new ObservableCollection<Channel>(channels);
		}
	}
}