using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server.Services.Encryption
{
	public  interface IEncryptionService
	{
		byte[] PasswordToHash(string pass);
	}
}
