using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server.Helpers
{
	public static class SaveFileHelper
	{
		public static string SaveFile(string path,string fileName, byte[] file)
		{
			var fullPath = Path.Combine(path, fileName);
			File.WriteAllBytes(fullPath, file);

			return fullPath;
		}
	}
}
