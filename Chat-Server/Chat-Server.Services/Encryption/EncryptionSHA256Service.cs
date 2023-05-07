using System.Security.Cryptography;
using System.Text;

namespace Chat_Server.Services.Encryption {
	public class EncryptionSHA256Service : IEncryptionService {
		public byte[] PasswordToHash(string pass) {
			using var mySha = SHA256.Create();
			var inputBytes = Encoding.UTF8.GetBytes(pass);
			var hashBytes = mySha.ComputeHash(inputBytes);
			return hashBytes;
		}
	}
}