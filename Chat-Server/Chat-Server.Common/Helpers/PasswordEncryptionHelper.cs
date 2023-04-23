using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server.Helpers
{
	public static class PasswordEncryptionHelper
	{
		public static byte[] ToHash(string pass)
		{
			using(var mySha = SHA256.Create())
			{
				byte[] inputBytes = Encoding.UTF8.GetBytes(pass);
				byte[] hashBytes = mySha.ComputeHash(inputBytes);
				return hashBytes;
			}
		}
	}
}
