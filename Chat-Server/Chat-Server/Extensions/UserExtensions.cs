using Chat_Server.Domain.Entities;
using Chat_Server.Helpers;
using Chat_Server.Response;

namespace Chat_Server.Extensions {
	public static class UserExtensions {
		// todo(v): некорректное имя метода. Перепутаны местами слова
		public static UserMessageResponse ToResponseMessage(this UserMessage userMessage) {
			var responseMessage = new UserMessageResponse {
				Id = userMessage.Id,
				Message = userMessage.Message,
				CreateAt = userMessage.CreatedAt,
				File = null,
				FileName = null
			};

			if (userMessage.HasFile) {
				responseMessage.FileName = FileHelper.GetFileName(userMessage.FilePath);
				responseMessage.File = FileHelper.ReadFile(userMessage.FilePath);
			}

			return responseMessage;
		}

		public static UserContactResponse ToUserContactResponse(this User user) {
			return new UserContactResponse {
				UserId = user.Id,
				UserName = user.Name
			};
		}

		public static BlockingResponse ToBlockingResponse(this User user) {
			return new BlockingResponse {
				UserId = user.Id,
				UserName = user.Name
			};
		}
	}
}