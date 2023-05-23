using System.IO;

namespace Chat_Server.Helpers {
	public static class FileHelper {
		public static string SaveFile(string path, string fileName, byte[] file) {
			var fullPath = Path.Combine(path, fileName);
			File.WriteAllBytes(fullPath, file);

			return fullPath;
		}

		// todo(v): этот метод можно заменить на Path.GetFileName
		public static string GetFileName(string path) {
			var fileInfo = new FileInfo(path);
			return fileInfo.Name;
		}

		// todo(v): лишний прокси-метод. Если бы текущий класс был бы не статическим, то в этом коде был бы смысл
		public static byte[] ReadFile(string path) {
			var bytes = File.ReadAllBytes(path);
			return bytes;
		}
	}
}