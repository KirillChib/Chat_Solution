using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server.Services.Encryption
{
	public class EncryptionSHA256Service : IEncryptionService
	{
		public byte[] PasswordToHash(string pass)
		{
			using (var mySha = SHA256.Create())
			{
				byte[] inputBytes = Encoding.UTF8.GetBytes(pass);
				byte[] hashBytes = mySha.ComputeHash(inputBytes);
				return hashBytes;
			}
		}
	}
}
