namespace Chat_Server.Services.Encryption; 

public interface IEncryptionService {
	byte[] PasswordToHash(string pass);
}