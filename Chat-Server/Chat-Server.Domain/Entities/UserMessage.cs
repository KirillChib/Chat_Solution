using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server.Domain.Entities
{
	public class UserMessage
	{
		public int Id { get; set; }
		public int UserFromId { get; set; }
		public int UserToId{get;set;}
		public string Message { get; set; }
		// todo(v): убрать = false;
		public bool HasFile { get; set; } = false;
		public string FilePath { get; set; }
		public DateTime CreatedAt { get; set; }

		public User UserFrom { get; set; }
		public User UserTo { get; set; }
	}
}
