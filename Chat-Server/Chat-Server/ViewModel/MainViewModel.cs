using Chat_Server.Context;
using Chat_Server.Domain.Entities;
using GalaSoft.MvvmLight;

namespace Chat_Server.ViewModel
{
	public class MainViewModel : ViewModelBase
	{
		private ChatDBContext _context;
		public MainViewModel()
		{
			_context = new ChatDBContext();
			var user1 = new User() { Id = 1, Login = "2", Name = "3", PasswordHash = "4" };
			_context.Users.Add(user1);
			_context.SaveChanges();

			//var user = new User() { Login = null, Name = "3", PasswordHash = "4" };
			//_context.Users.Add(user);
			//_context.SaveChanges();
		}
	}
}