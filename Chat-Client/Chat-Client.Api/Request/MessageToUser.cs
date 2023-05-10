using System;

namespace Chat_Client.Api.Request;

public class MessageToUser {
	public int UserToId { get; set; }
	public string Message { get; set; }
	public string FileName { get; set; }
	public byte[] File { get; set; }
	public DateTime CreateAt { get; set; }
}