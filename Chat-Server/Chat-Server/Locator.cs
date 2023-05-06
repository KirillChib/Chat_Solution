﻿using Chat_Server.Commands;
using Chat_Server.Services;
using Chat_Server.Services.Encryption;
using Chat_Server.Services.JWT;
using Chat_Server.Services.Messege;
using Chat_Server.Services.MessegeService;
using Grace.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Chat_Server
{
	public class Locator : ILocatorService
	{
		private readonly DependencyInjectionContainer _container;
		private static readonly Lazy<Locator> Lazy = new Lazy<Locator>(() => new Locator());

		public static Locator Current => Lazy.Value;

		private Locator()
		{
			_container = new DependencyInjectionContainer();
			_container.Configure(RegisterDependencies);
		}

		private static void RegisterDependencies(IExportRegistrationBlock registration)
		{
			RegisterSingleton<ICommand, CreateUserCommand>(registration);
			RegisterSingleton<ICommand, AuthorizationUserCommand>(registration);
			RegisterSingleton<ICommand, AddUserMessageCommand>(registration);
			RegisterSingleton<ICommand, GetUserMessagesCommand>(registration);
			RegisterSingleton<ICommand, GetNewUserMessagesCommand>(registration);
			//RegisterSingleton<ICommand, GetFriendsListCommand>(registration);
			//RegisterSingleton<ICommand, GetPrivateChatCommand>(registration);
			//RegisterSingleton<ICommand, GetPublicChatCommand>(registration);
			//RegisterSingleton<ICommand, GetUsersOnLineCommand>(registration);
			//RegisterSingleton<ICommand, LogInCommand>(registration);

			RegisterSingleton<Iserver, Server>(registration);
			RegisterSingleton<IUserServices, UserServices>(registration);
			RegisterSingleton<IMessageService, MessageService>(registration);
			RegisterSingleton<IEncryptionService, EncryptionSHA256Service>(registration);
			RegisterSingleton<IJwtService>(registration,
<<<<<<< .mine
			() => new JwtService(СonfigurationsTokens.Issuer, СonfigurationsTokens.SecretKey));
=======
				() => new JwtService(СonfigurationsTokens.Issuer, СonfigurationsTokens.SecretKey));
>>>>>>> .theirs
		}

		private static void RegisterSingleton<TFrom, TTo>(IExportRegistrationBlock registrationBlock) where TTo : TFrom
		{
			registrationBlock.Export<TTo>().As<TFrom>().Lifestyle.Singleton();
		}

		private static void RegisterSingleton<TFrom>(IExportRegistrationBlock registrationBlock, Func<TFrom> create)
		{
			registrationBlock.ExportFactory(create).As<TFrom>().Lifestyle.Singleton();
		}

		public object GetService(Type serviceType)
		{
			return _container.Locate(serviceType);
		}

		public bool CanLocate(Type type, ActivationStrategyFilter consider = null, object key = null)
		{
			return _container.CanLocate(type, consider, key);
		}

		public object Locate(Type type)
		{
			return _container.Locate(type);
		}

		public object LocateOrDefault(Type type, object defaultValue)
		{
			return _container.LocateOrDefault(type, defaultValue);
		}

		public T Locate<T>()
		{
			return _container.Locate<T>();
		}

		public T LocateOrDefault<T>(T defaultValue = default)
		{
			return _container.LocateOrDefault(defaultValue);
		}

		public List<object> LocateAll(Type type, object extraData = null, ActivationStrategyFilter consider = null,
			IComparer<object> comparer = null)
		{
			return _container.LocateAll(type, extraData, consider, comparer);
		}

		public List<T> LocateAll<T>(Type type = null, object extraData = null, ActivationStrategyFilter consider = null,
			IComparer<T> comparer = null)
		{
			return _container.LocateAll(type, extraData, consider, comparer);
		}

		public bool TryLocate<T>(out T value, object extraData = null, ActivationStrategyFilter consider = null,
			object withKey = null, bool isDynamic = false)
		{
			return _container.TryLocate(out value, extraData, consider, withKey, isDynamic);
		}

		public bool TryLocate(Type type, out object value, object extraData = null,
			ActivationStrategyFilter consider = null, object withKey = null, bool isDynamic = false)
		{
			return _container.TryLocate(type, out value, extraData, consider, withKey, isDynamic);
		}

		public object LocateByName(string name, object extraData = null, ActivationStrategyFilter consider = null)
		{
			return _container.LocateByName(name, extraData, consider);
		}

		public List<object> LocateAllByName(string name, object extraData = null,
			ActivationStrategyFilter consider = null)
		{
			return _container.LocateAllByName(name, extraData, consider);
		}

		public bool TryLocateByName(string name, out object value, object extraData = null,
			ActivationStrategyFilter consider = null)
		{
			return _container.TryLocateByName(name, out value, extraData, consider);
		}

		// ReSharper disable MethodOverloadWithOptionalParameter
		public object Locate(Type type, object extraData = null, ActivationStrategyFilter consider = null,
			object withKey = null, bool isDynamic = false)
		{
			return _container.Locate(type, extraData, consider, withKey, isDynamic);
		}

		public T Locate<T>(object extraData = null, ActivationStrategyFilter consider = null, object withKey = null,
			bool isDynamic = false)
		{
			return _container.Locate<T>(extraData, consider, withKey, isDynamic);
		}
	}
}