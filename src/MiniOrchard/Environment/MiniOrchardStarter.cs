namespace MiniOrchard.Environment
{
	using System;
	using Autofac;
	using MiniOrchard.Caching;
	using MiniOrchard.Data;
	using MiniOrchard.Events;
	using MiniOrchard.Exceptions;
	using MiniOrchard.FileSystems.AppData;
	using MiniOrchard.Logging;
	using MiniOrchard.Mvc;
	using MiniOrchard.Security;
	using MiniOrchard.Security.Providers;
	using MiniOrchard.Services;

	public static class MiniOrchardStarter
	{
		public static IContainer CreateHostContainer(Action<ContainerBuilder> registrations)
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule(new MvcModule());
			builder.RegisterModule(new LoggingModule());
			builder.RegisterModule(new EventsModule());
			builder.RegisterModule(new CacheModule());
			builder.RegisterModule(new DataModule());

			builder.RegisterType<Authorizer>().As<IAuthorizer>();
			builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>();
			builder.RegisterType<RolesBasedAuthorizationService>().As<IAuthorizationService>();

			// a single default host implementation is needed for bootstrapping a web app domain
			builder.RegisterType<DefaultEventBus>().As<IEventBus>().SingleInstance();
			builder.RegisterType<DefaultCacheHolder>().As<ICacheHolder>().SingleInstance();
			builder.RegisterType<DefaultCacheContextAccessor>().As<ICacheContextAccessor>().SingleInstance();
			builder.RegisterType<DefaultParallelCacheContext>().As<IParallelCacheContext>().SingleInstance();
			builder.RegisterType<DefaultAsyncTokenProvider>().As<IAsyncTokenProvider>().SingleInstance();
			builder.RegisterType<DefaultHostEnvironment>().As<IHostEnvironment>().SingleInstance();
			builder.RegisterType<DefaultExceptionPolicy>().As<IExceptionPolicy>().SingleInstance();

			RegisterVolatileProvider<AppDataFolder, IAppDataFolder>(builder);
			RegisterVolatileProvider<Clock, IClock>(builder);

			registrations(builder);

			var container = builder.Build();

			//ControllerBuilder.Current.SetControllerFactory(new OrchardControllerFactory());
			//ViewEngines.Engines.Clear();
			//ViewEngines.Engines.Add(new ThemeAwareViewEngineShim());

			//var hostContainer = new DefaultOrchardHostContainer(container);
			////MvcServiceLocator.SetCurrent(hostContainer);
			//OrchardHostContainerRegistry.RegisterHostContainer(hostContainer);

			//// Register localized data annotations
			//ModelValidatorProviders.Providers.Add(new LocalizedModelValidatorProvider());

			return container;
		}

		private static void RegisterVolatileProvider<TRegister, TService>(ContainerBuilder builder) where TService : IVolatileProvider
		{
			builder.RegisterType<TRegister>()
				.As<TService>()
				.As<IVolatileProvider>()
				.SingleInstance();
		}
	}
}
