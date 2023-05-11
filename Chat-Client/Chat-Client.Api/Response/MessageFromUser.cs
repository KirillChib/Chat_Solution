using System;

namespace Chat_Client.Api.Response;

public class MessageFromUser {
	public int UserFromId { get; set; }
	public string Message { get; set; }
	public string FileName { get; set; }
	public byte[] File { get; set; }
	public DateTime CreateAt { get; set; }
}