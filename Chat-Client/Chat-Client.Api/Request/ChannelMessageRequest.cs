using System;

namespace Chat_Client.Api.Request; 

public class ChannelMessageRequest {
	public string Message { get; set; }
	public byte[] File { get; set; }
	public string FileName { get; set; }
	public DateTime CreateAt { get; set; }
}