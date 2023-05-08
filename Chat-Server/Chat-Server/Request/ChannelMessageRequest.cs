using System;

namespace Chat_Server.Request; 

public class ChannelMessageRequest {
	public int UserId { get; set; }
	public string Message { get; set; }
	public byte[] File { get; set; }
	public DateTime CreateAt { get; set; }
}