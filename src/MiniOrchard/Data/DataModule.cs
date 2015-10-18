using System.Reflection;
using Autofac;
using MiniOrchard.Data.Providers;
using Module = Autofac.Module;

namespace MiniOrchard.Data
{
	public class DataModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<SessionLocator>().As<ISessionLocator>();
			builder.RegisterType<SessionFactoryHolder>().As<ISessionFactoryHolder>();
			builder.RegisterType<TransactionManager>().As<ITransactionManager>();
			builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();
		}

		protected override void AttachToComponentRegistration(Autofac.Core.IComponentRegistry componentRegistry, Autofac.Core.IComponentRegistration registration)
		{
			if (typeof(IDataServicesProvider).IsAssignableFrom(registration.Activator.LimitType))
			{
				var propertyInfo = registration.Activator.LimitType.GetProperty("ProviderName", BindingFlags.Static | BindingFlags.Public);
				if (propertyInfo != null)
				{
					registration.Metadata["ProviderName"] = propertyInfo.GetValue(null, null);
				}
			}
		}
	}
}