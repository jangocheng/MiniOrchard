using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using MiniOrchard;

namespace MiniOrchard.Data.Providers
{
	public interface IDataServicesProvider : ITransientDependency
	{
		Configuration BuildConfiguration(SessionFactoryParameters sessionFactoryParameters);
		IPersistenceConfigurer GetPersistenceConfigurer(bool createDatabase);
	}
}
