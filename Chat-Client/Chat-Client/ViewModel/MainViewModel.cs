using Chat_Client.Api.Blocking;
using Chat_Client.Api.Channel;
using Chat_Client.Api.Contact;
using Chat_Client.Api.Extensions;
using Chat_Client.Api.Helpers;
using Chat_Client.Api.Message;
using Chat_Client.Api.Request;
using Chat_Client.Api.Response;
using Chat_Client.Api.User;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Chat_Client.Commands;

namespace Chat_Client.ViewModel
{
	// todo(v): очень большой класс. Стоит разбить на независимые контролы
	public class MainViewModel : ViewModelBase
	{
		// todo(v): BaseUrl стоит хранить в классе с константами или в конфигурации
		private const string BaseUri = "http://127.0.0.1:8888";
		private const string _path = "D:\\Images";

		// todo(v): Используй DI контейнер
		private readonly IUserApi _userApi = new UserApi(BaseUri);
		private readonly IMessageApi _messageApi = new MessageApi(BaseUri);
		private readonly IContactApi _contactApi = new ContactApi(BaseUri);
		private readonly IChannelApi _channelApi = new ChannelApi(BaseUri);
		private readonly IBlockingApi _blockingApi = new BlockingApi(BaseUri);

		private string _token;

		// todo(v): в wpf нужно использовать классы Model (реализуемые паттерн наблюдателя) для хранения и отображения данных
		private ObservableCollection<UserResponse> users;
		private ObservableCollection<UserContactResponse> contacts;
		private ObservableCollection<ChannelResponse> channels;
		private ObservableCollection<MessageView> messagesView;
		private ObservableCollection<ChannelResponse> searchChannels;
		private ObservableCollection<BlockingResponse> blockingUsers;

		// todo(v): в wpf нужно использовать классы Model (реализуемые паттерн наблюдателя) для хранения и отображения данных
		private UserResponse selectedUser;
		private UserContactResponse selectedContact;
		private ChannelResponse selectedChannel;
		private ChannelResponse searcSelectedChannel;
		private BlockingResponse selectedBlocking;

		private string channelName;
		private string createChannelName;
		private string userName;
		private string logIn;
		private string password;
		private string name;
		private string status;
		// todo(v): лучше сделать как enum. Иначе есть возможность одновременно включить показ обоих контролов
		private Visibility visibilitySign;
		private Visibility visibilityChat;

		//UserCommands
		private ICommand getUsersCommand;
		private ICommand addContactCommand;
		private ICommand searchUsersCommand;

		//Channel Commands
		private ICommand getChannelMessagesCommand;
		private ICommand createChannelCommand;
		private ICommand searchChannelCommand;
		private ICommand subscribeChannelCommand;

		//Contact Commands
		private ICommand getUserMessagesCommand;
		private ICommand deleteContactCommand;

		//Message Commands
		private ICommand sendMessageCommand;
		private ICommand addMessageFileCommand;

		//Sign Commands
		private ICommand createUserCommand;
		private ICommand logInUserCommand;

		//Ignore Commands
		private ICommand addIgnoreUserCommand;
		private ICommand deleteIgnoreUserCommand;

		//UserCommands
		public ICommand GetUsersCommand => getUsersCommand ??= new AsyncRelayCommand(GetUsersAsync);//+
		public ICommand AddContactCommand => addContactCommand ??= new AsyncRelayCommand(AddContactAsync);//+
		public ICommand SearchUsersCommand => searchUsersCommand ??= new AsyncRelayCommand(SearchUserByNameAsync);//+

		//Channel Commands
		public ICommand GetChannelMessagesCommand => getChannelMessagesCommand ??= new AsyncRelayCommand(GetChannelMessagesById);//+
		public ICommand CreateChannelCommand => createChannelCommand ??= new AsyncRelayCommand(CreateChannelAsync);//+
		public ICommand SearcChannelCommand => searchChannelCommand ??= new AsyncRelayCommand(SearchChannelAsync);//+
		public ICommand SubscribeChannelCommand => subscribeChannelCommand ??= new AsyncRelayCommand(SubscribeChannelAsync);//+

		//Contact Commands
		public ICommand GetUserMessagesCommand => getUserMessagesCommand ??= new AsyncRelayCommand(GetUserMessageByIdAsync);//+
		public ICommand DeleteContactCommand => deleteContactCommand ??= new AsyncRelayCommand(DeleteContactAsync);//+

		//Message Commands
		public ICommand SendMessageCommand => sendMessageCommand ??= new AsyncRelayCommand(SendMessageAsync);//+
		public ICommand AddMessageFileCommand => addMessageFileCommand ??= new RelayCommand(OpenFile);//+

		//Sign Commands
		public ICommand CreateUserCommand => createUserCommand ??= new AsyncRelayCommand(CreateUserAsync);//+
		public ICommand LogInUserCommand => logInUserCommand ??= new AsyncRelayCommand(AuthorizationUserAsync);//+

		//Ignore Commands
		public ICommand AddIgnoreUserCommand => addIgnoreUserCommand ??= new AsyncRelayCommand(AddBlockingUserAsync);
		public ICommand DeleteIgnoreUserCommand => deleteIgnoreUserCommand ??= new AsyncRelayCommand(DeleteBlockingUserAsync);

		public ObservableCollection<UserResponse> Users { get => users; set => Set(ref users, value); }
		public ObservableCollection<UserContactResponse> Contacts { get => contacts; set => Set(ref contacts, value); }
		public ObservableCollection<ChannelResponse> Channels { get => channels; set => Set(ref channels, value); }
		public ObservableCollection<MessageView> MessagesView { get => messagesView; set => Set(ref messagesView, value); }
		public ObservableCollection<ChannelResponse> SearchChannels { get => searchChannels; set => Set(ref searchChannels, value); }
		public ObservableCollection<BlockingResponse> BlockingUsers { get => blockingUsers; set => Set(ref blockingUsers,value); }

		public UserResponse SelectedUser { get => selectedUser; set => Set(ref selectedUser, value); }
		public UserContactResponse SelectedContact { get => selectedContact; set => Set(ref selectedContact, value); }
		public ChannelResponse SelectedChannel { get => selectedChannel; set => Set(ref selectedChannel, value); }
		public ChannelResponse SearcSelectedChannel { get => searcSelectedChannel; set => Set(ref searcSelectedChannel, value); }
		public BlockingResponse SelectedBlocking { get => selectedBlocking; set => Set(ref selectedBlocking,value); }

		public bool IsChannel { get; set; }
		public byte[] MessageFile { get; set; } = null; // todo(v): ненужная инициализация дефолтным значением
		public string FileName { get; set; } = null;
		public string Message { get; set; }
		public string LogIn { get => logIn; set => Set(ref logIn,value); } // todo(v): некорректные отступы
		public string Password { get => password; set => Set(ref password,value); }
		public string Name { get => name; set => Set(ref name,value); }
		public string Status { get => status; set => Set(ref status,value); }
		public Visibility VisibilitySign { get => visibilitySign; set => Set(ref visibilitySign,value); }
		public Visibility VisibilityChat { get => visibilityChat; set => Set(ref visibilityChat,value); }

		public string ChannelName { get => channelName; set => Set(ref channelName, value); }
		public string CreateChannelName { get => createChannelName; set => Set(ref createChannelName, value); }
		public string UserName { get => userName; set => Set(ref userName, value); }		

		public MainViewModel()
		{
			VisibilityChat = Visibility.Collapsed;
			VisibilitySign = Visibility.Visible;
		}

		private async Task GetUsersAsync()
		{
			var users = await _userApi.GetAllUserRequest(_token).ConfigureAwait(false);
			Users = new ObservableCollection<UserResponse>(users);
		}

		private async Task GetContactsAsync()
		{
			// todo(v): опечатка в слове cntacts
			var cntacts = await _contactApi.GetUserContactsRequestAsync(_token).ConfigureAwait(false);
			Contacts = new ObservableCollection<UserContactResponse>(cntacts);
		}

		private async Task GetChannelsAsync()
		{
			// todo(v): локальная переменная одноименна с полем
			var channels = await _channelApi.GetUserChannelsAsync(_token).ConfigureAwait(false);
			Channels = new ObservableCollection<ChannelResponse>(channels);
		}

		private void OpenFile()
		{
			using var fileDialog = new OpenFileDialog();
			fileDialog.Filter = "File (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";

			if (fileDialog.ShowDialog() == DialogResult.Cancel)
				return;

			var path = fileDialog.FileName;

			FileName = FileHelper.GetFileName(path);
			MessageFile = FileHelper.ReadFile(path);
		}

		private async Task SendMessageAsync()
		{
			if (IsChannel)
			{
				var message = MessageHelper.CreateChannelMessage(Message, MessageFile, FileName);
				await _channelApi.AddChannelMessageRequestAsync(message, _token, SelectedChannel.Id).ConfigureAwait(false);
				await GetChannelMessagesById().ConfigureAwait(false);
			}
			else
			{
				var message = MessageHelper.CreateUserMessage(selectedContact.UserId, Message, MessageFile, FileName);
				await _messageApi.SendMessageToUserRequestAsync(_token, message).ConfigureAwait(false);
				await GetUserMessageByIdAsync().ConfigureAwait(false);
			}

			MessageFile = null;
			FileName = null;
		}

		private async Task GetUserMessageByIdAsync()
		{
			IsChannel = false;

			var messages = await _messageApi.GetUserMessagesRequestAsync(_token, SelectedContact.UserId).ConfigureAwait(false);
			var view = new ObservableCollection<MessageView>();

			foreach (var message in messages)
				view.Add(message.UserMessageToView(_path));

			MessagesView = new ObservableCollection<MessageView>(view);
		}

		private async Task GetChannelMessagesById()
		{
			IsChannel = true;

			var messages = await _channelApi.GetChannelMessagesRequestAsync(_token, SelectedChannel.Id).ConfigureAwait(false);
			MessagesView = new ObservableCollection<MessageView>();

			foreach (var message in messages)
				MessagesView.Add(message.ChannelMessageToView(_path));
		}

		private async Task SubscribeChannelAsync()
		{
			await _channelApi.SubscribeChannelRequestAsync(_token, SearcSelectedChannel.Id).ConfigureAwait(false);

			// todo(v): опечатка в слове chennels
			var chennels = await _channelApi.GetUserChannelsAsync(_token).ConfigureAwait(false);

			System.Windows.MessageBox.Show($"Вы подписались на {SearcSelectedChannel.Name}");

			Channels = new ObservableCollection<ChannelResponse>(chennels);
		}

		private async Task SearchChannelAsync()
		{
			var channels = await _channelApi.GetChannelByNameRequestAsync(_token, ChannelName).ConfigureAwait(false);

			SearchChannels = new ObservableCollection<ChannelResponse>(channels);
		}

		private async Task SearchUserByNameAsync()
		{
			var users = await Task.Run(() => Users.Where(u => u.UserName.StartsWith(UserName)));
			Users = new ObservableCollection<UserResponse>(users);
		}

		private async Task AddContactAsync()
		{
			await _contactApi.AddUserContactRequestAsync(SelectedUser.UserId, _token).ConfigureAwait(false);

			System.Windows.MessageBox.Show($"Добавлен контакт {SelectedUser.UserName}");

			await GetContactsAsync().ConfigureAwait(false);
		}

		private async Task DeleteContactAsync()
		{
			await _contactApi.DeleteUserContactRequestAsync(SelectedContact.UserId, _token).ConfigureAwait(false);

			System.Windows.MessageBox.Show($"Контакт {SelectedContact.UserName} удален");

			await GetContactsAsync().ConfigureAwait(false);
		}

		private async Task CreateChannelAsync()
		{
			var channel = new ChannelRequest
			{
				Name = CreateChannelName
			};

			var subscribe = await _channelApi.CreateChannelRequestAsync(_token, channel).ConfigureAwait(false);
			await _channelApi.SubscribeChannelRequestAsync(_token, subscribe.Id).ConfigureAwait(false);

			await GetChannelsAsync().ConfigureAwait(false);
		}

		private async Task CreateUserAsync()
		{
			if(string.IsNullOrWhiteSpace(LogIn) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Name))
			{
				Status = "Не все поля заполнены";
				return;
			}

			var user = UserHelper.CreateNewUser(LogIn, Password, Name);
			Status = await _userApi.RegistrationRequestAsync(user).ConfigureAwait(false);
		}

		private async Task AuthorizationUserAsync()
		{
			if (string.IsNullOrWhiteSpace(LogIn) || string.IsNullOrWhiteSpace(Password))
			{
				Status = "Не все поля заполнены";
				return;
			}

			var userLogin = UserHelper.CreateUserLogIn(LogIn, Password);
			_token = await _userApi.LoginRequestAsync(userLogin).ConfigureAwait(false);

			if(_token == "Invalid request body content" || _token == "Invalid username or password")
			{
				Status = "Проверьте вводимые даные"; // todo(v): опечатка
				return;
			}

			await GetUsersAsync();
			await GetContactsAsync();
			await GetChannelsAsync();
			await GetUserBlockingsAsync();

			VisibilitySign = Visibility.Collapsed;
			VisibilityChat = Visibility.Visible;
		}

		private async Task GetUserBlockingsAsync()
		{
			var blockings = await _blockingApi.GetBlockingsRequestAsync(_token).ConfigureAwait(false);
			BlockingUsers = new ObservableCollection<BlockingResponse>(blockings);
		}

		private async Task AddBlockingUserAsync()
		{
			await _blockingApi.CreateBlockingRequestsAsync(_token, SelectedContact.UserId).ConfigureAwait(false);

			System.Windows.MessageBox.Show($"Контакт {SelectedContact.UserName} добавлен в ЧС");

			await GetUserBlockingsAsync().ConfigureAwait(false);
		}

		private async Task DeleteBlockingUserAsync()
		{
			await _blockingApi.DeleteBlockingRequestAsync(_token, SelectedBlocking.UserId).ConfigureAwait(false);

			System.Windows.MessageBox.Show($"Контакт {SelectedBlocking.UserName} удален из ЧС");

			await GetUserBlockingsAsync();
		}
	}
}