using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server.Response
{
	public class UserMessageResponse
	{
		public int Id { get; set; }
		public string Message { get; set; }
		public string FileName { get; set; }
		public byte[] File { get; set; }
		public DateTime CreateAt { get; set; }
	}
}
