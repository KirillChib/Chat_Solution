﻿namespace Chat_Server.Domain.Entities;

public class ChannelUser
{
	public int ChannelId { get; set; }
	public int UserId { get; set; }

	public Channel Channel { get; set; }
	public User User { get; set; }
}