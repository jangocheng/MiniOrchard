using System;
using NHibernate.Cfg;

namespace MiniOrchard.Data
{
	public interface ISessionConfigurationCache : ISingletonDependency
	{
		Configuration GetConfiguration(Func<Configuration> builder);
	}
}