using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server.Domain.Entities
{
	public  class ChannelMessage
	{
		public int Id { get; set; }
		public int UserFromId { get; set; }
		public int ChannelId { get; set; }
		public string Message { get; set; }
		public bool HasFile { get; set; } = false;
		public string FilePath { get; set; }
		public DateTime CreatedAt { get; set; }

		public User UserFrom { get; set; }
		public Channel Channel { get; set; }
	}
}
