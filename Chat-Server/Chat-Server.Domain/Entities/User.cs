using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server.Domain.Entities
{
	public  class User
	{
		public int Id { get; set; }
		public string Login { get; set; }
		public string Name { set; get; }
		public byte[] PasswordHash { get; set; }

		public ICollection<UserMessage> UserMessagesFrom { get; set; }
		public ICollection<UserMessage> UserMessagesTo { get; set; }
		public ICollection<ChannelMessage> ChannelMessages { get; set; }
		public ICollection<ChannelUser> ChannelsUser { get; set; }
	}
}
