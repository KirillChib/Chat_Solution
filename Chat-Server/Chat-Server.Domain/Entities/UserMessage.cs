﻿using System;

namespace Chat_Server.Domain.Entities; 

public class UserMessage {
	public int Id { get; set; }
	public int UserFromId { get; set; }
	public int UserToId { get; set; }
	public string Message { get; set; }
	public bool HasFile { get; set; }
	public string FilePath { get; set; }
	public DateTime CreatedAt { get; set; }

	public User UserFrom { get; set; }
	public User UserTo { get; set; }
}