using System.Collections.Generic;

namespace Chat_Server.Domain.Entities; 

public class Channel {
	public int Id { get; set; }
	public string Name { get; set; }

	public ICollection<ChannelUser> ChannelUsers { get; set; }
	public ICollection<ChannelMessage> ChannelMessages { get; set; }
}