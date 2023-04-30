using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server.Domain.Entities
{
	public class Channel
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<ChannelUser> ChannelUsers { get; set; }
		public ICollection<ChannelMessage> ChannelMessages { get; set; }
	}
}
