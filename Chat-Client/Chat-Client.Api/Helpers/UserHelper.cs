using Chat_Client.Api.Request;

namespace Chat_Client.Api.Helpers;

	public static class UserHelper
{
	public static UserRegistration CreateNewUser(string logIn, string password, string name)
	{
		return new UserRegistration
		{
			Login = logIn,
			Password = password,
			Name = name
		};
	}

	public static UserLogin CreateUserLogIn(string logIn, string password)
	{
		return new UserLogin
		{
			Login = logIn,
			Password = password
		};
	}
}