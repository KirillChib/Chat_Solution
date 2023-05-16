using Chat_Client.Api.Helpers;
using Chat_Client.Api.Message;
using Chat_Client.Api.Request;
using Chat_Client.Api.Response;

namespace Chat_Client.Api.Extensions;

public static class MessageExtensions
{
	public static MessageView ChannelMessageToView(this ChannelMessageResponse channelMessage, string path)
	{
		var message = string.Concat(channelMessage.UserName, "\n", channelMessage.Message, "\n", channelMessage.CreateAt);

		string filePath;
		if(channelMessage.File != null)
		{
			filePath = FileHelper.SaveFile(path, channelMessage.FileName, channelMessage.File);
		}
		else
		{
			filePath = null;
		}

		return new MessageView
		{
			FullMessage = message,
			FilePath = filePath
		};
	}

	public static MessageView UserMessageToView(this MessageToUser userMessage, string path)
	{
		var message = string.Concat( userMessage.Message, "\n", userMessage.CreateAt);

		string filePath;
		if (userMessage.File != null)
		{
			filePath = FileHelper.SaveFile(path, userMessage.FileName, userMessage.File);
		}
		else
		{
			filePath = null;
		}

		return new MessageView
		{
			FullMessage = message,
			FilePath = filePath
		};
	}
}