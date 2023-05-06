using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chat_Server.Extensions
{
	public static class TaskExtension
	{
		public static async void FireAndForgetSafeAsync(this Task task)
		{
			try
			{
				await task.ConfigureAwait(false);
			}
			catch (Exception)
			{
				// ignored
			}
		}
	}
}
