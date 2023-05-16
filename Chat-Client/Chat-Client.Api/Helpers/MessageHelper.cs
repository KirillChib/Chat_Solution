using Chat_Client.Api.Request;
using System;

namespace Chat_Client.Api.Helpers;

public static class MessageHelper { 
	public static MessageToUser CreateUserMessage(int userId, string message, byte[] file, string fileName)
	{
		return new MessageToUser
		{
			UserToId = userId,
			Message = message,
			File = file,
			FileName = fileName,
			CreateAt = DateTime.Now
		};
	}

	public static ChannelMessageRequest CreateChannelMessage(string message, byte[] file, string fileName)
	{
		return new ChannelMessageRequest
		{
			Message = message,
			File = file,
			FileName = fileName,
			CreateAt = DateTime.Now
		};
	}
}