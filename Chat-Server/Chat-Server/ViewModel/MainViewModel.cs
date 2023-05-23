using GalaSoft.MvvmLight;
using System.Windows.Input;
using Chat_Server.Common.Extensions;
using GalaSoft.MvvmLight.CommandWpf;

namespace Chat_Server.ViewModel;

// todo(v): проблемы табуляции в файле. Используй resharper
public class MainViewModel : ViewModelBase
{
	private const string ServerUri = "http://127.0.0.1:8888/";

	private string stutus;
	public string Stutus { get => stutus; set => Set(ref stutus,value); }
	

	private ICommand startCommand;
	public ICommand StartCommand => startCommand ??= new RelayCommand(Start);

	public MainViewModel()
	{	
	}

	private IServer CreateServer()
	{
		return Locator.Current.Locate<IServer>();
	}

	private async void Start()
	{
		Stutus = "Сервер запущен";
		CreateServer().StartAsync(ServerUri).FireAndForgetSafeAsync();
	}
}