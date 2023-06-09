﻿using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Chat_Server.Common.Extensions;
using Chat_Server.Extensions;

namespace Chat_Server;

// todo(v): часть кода написано в BSD стиле (фигурная скобка на следующей строке), а часть в K&R стиле (фигурная скобка на текущей строке). Нужно привести проект к одному виду
public class Server : IServer {
	private readonly ICommands[] _commands;
	private readonly object _lockObject = new object();
	private HttpListener _listener;

	public Server(ICommands[] commands) {
		_commands = commands;
	}

	public async Task StartAsync(string uri) {
		lock (_lockObject) {
			if (_listener != null)
				throw new InvalidOperationException("Server is started");

			_listener = new HttpListener();

			_listener.Prefixes.Add(uri);
			_listener.Start();
		}

		// todo(v): вывод в консоль не нужен. Можно либо удалить эту строку, либо добавить логи и писать в них
		Console.WriteLine("Server started");

		while (_listener.IsListening)
			await HandleNextRequestAsync().ConfigureAwait(false);
	}

	private async Task HandleNextRequestAsync() {
		var context = await _listener.GetContextAsync().ConfigureAwait(false);

		Task.Run(async () => {
			try {
				// todo(v): вывод в консоль не нужен. Можно либо удалить эту строку, либо добавить логи и писать в них
				Console.WriteLine($@"Request {context.Request.HttpMethod} {context.Request.Url}");

				var method = context.Request.HttpMethod;
				var path = context.Request.Url.AbsolutePath.TrimEnd('/');

				var command = _commands.SingleOrDefault(command =>
					command.Method.ToString() == method &&
					Regex.IsMatch(path, $"^{command.Path}$", RegexOptions.IgnoreCase));

				if (command == null) {
					await context.WriteResponseAsync(501, $"Not found command for path {path} with method {method}").ConfigureAwait(false);
					return;
				}

				await command.HandleRequestAsync(context).ConfigureAwait(false);
			}
			catch (Exception exception) {
				Console.WriteLine(exception);
				await context.WriteResponseAsync(500, exception.Message).ConfigureAwait(false);
			}
		}).FireAndForgetSafeAsync();
	}

	public void Dispose() {
		_listener?.Stop();
	}
}