using System;
using System.Threading.Tasks;

namespace Chat_Server.Common.Extensions;

public static class TaskExtension {
	public static async void FireAndForgetSafeAsync(this Task task) {
		try {
			await task.ConfigureAwait(false);
		}
		catch (Exception) {
			// ignored
		}
	}
}