﻿using System.IO;

namespace Chat_Client.Api.Helpers;

public static class FileHelper{
	public static string SaveFile(string path, string fileName, byte[] file)
	{
		var fullPath = Path.Combine(path, fileName);
		File.WriteAllBytes(fullPath, file);

		return fullPath;
	}

	public static string GetFileName(string path)
	{
		var fileInfo = new FileInfo(path);
		return fileInfo.Name;
	}

	public static byte[] ReadFile(string path)
	{
		var bytes = File.ReadAllBytes(path);
		return bytes;
	}
}