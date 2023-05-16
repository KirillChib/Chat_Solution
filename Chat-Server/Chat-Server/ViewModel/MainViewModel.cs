using AsyncCommandLibrary;
using GalaSoft.MvvmLight;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chat_Server.ViewModel;

public class MainViewModel : ViewModelBase
{
	private const string ServerUri = "http://127.0.0.1:8888/";

	private string stutus;
	public string Stutus { get => stutus; set => Set(ref stutus,value); }
	

	private ICommand startCommand;
	public ICommand StartCommand => startCommand ??= new AsyncRelayCommand(StartAsync);

	public MainViewModel()
	{	
	}

	private IServer CreateServer()
	{
		return Locator.Current.Locate<IServer>();
	}

	private async Task StartAsync()
	{
		Stutus = "Сервер запущен";
		await CreateServer().StartAsync(ServerUri).ConfigureAwait(false);
	}
}