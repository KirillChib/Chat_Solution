using Chat_Server.Domain.Entities;
using Chat_Server.Helpers;
using Chat_Server.Request;
using Chat_Server.Response;

namespace Chat_Server.Extensions;

public static class ChannelExtensions {
	public static ChannelMessage ToChannelMessage(this ChannelMessageRequest request, int channelId, int userId) {
		var message = new ChannelMessage {
			UserFromId = userId,
			ChannelId = channelId,
			Message = request.Message,
			CreatedAt = request.CreateAt
		};

		if (request.File is null) {
			message.HasFile = false;
			message.FilePath = null;
		}
		else {
			message.HasFile = true;
			message.FilePath = FileHelper.SaveFile(ConfigurationsFiles.Path, request.FileName, request.File);
		}

		return message;
	}

	public static ChannelMessageResponse ToChannelMessageResponse(this ChannelMessage message,string name) {
		var response = new ChannelMessageResponse {
			UserName = name,
			Message = message.Message,
			File = null,
			FileName = null,
			CreateAt = message.CreatedAt
		};

		if (message.HasFile) {
			response.File = FileHelper.ReadFile(message.FilePath);
			response.FileName = FileHelper.GetFileName(message.FilePath);
		}

		return response;
	}
}