using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.Api.Request
{
	public class UserLogin
	{
		public string Login { get; set; }
		public string Password { get; set; }
	}
}
