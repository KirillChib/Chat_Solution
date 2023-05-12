using System;

namespace Chat_Client.Api.Response;

public class ChannelMessageResponse {
	public string UserName { get; set; }
	public string Message { get; set; }
	public byte[] File { get; set; }
	public string FileName { get; set; }
	public DateTime CreateAt { get; set; }
}