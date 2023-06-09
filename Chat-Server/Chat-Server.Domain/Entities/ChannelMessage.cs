﻿using System;

namespace Chat_Server.Domain.Entities; 

public class ChannelMessage {
	public int Id { get; set; }
	public int UserFromId { get; set; }
	public int ChannelId { get; set; }
	public string Message { get; set; }
	public bool HasFile { get; set; }
	public string FilePath { get; set; }
	public DateTime CreatedAt { get; set; }

	public User UserFrom { get; set; }
	public Channel Channel { get; set; }
}