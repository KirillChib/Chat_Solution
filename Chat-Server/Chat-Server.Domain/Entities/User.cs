using System.Collections.Generic;

namespace Chat_Server.Domain.Entities
{
	public class User {
		public int Id { get; set; }
		public string Login { get; set; }

		public string Name { set; get; }

		// todo(v): может быть хранить в base64?
		public byte[] PasswordHash { get; set; }

		public ICollection<UserMessage> UserMessagesFrom { get; set; }
		public ICollection<UserMessage> UserMessagesTo { get; set; }
		public ICollection<ChannelMessage> ChannelMessages { get; set; }
		public ICollection<ChannelUser> ChannelsUser { get; set; }
		public ICollection<UserContact> UserContacts { get; set; }
		public ICollection<UserContact> Contacts { get; set; }
		public ICollection<Blocking> BlockingUsersTo { get; set; }
		public ICollection<Blocking> BlockingUsersFrom { get; set; }
	}
}