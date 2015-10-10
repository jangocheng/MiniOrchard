namespace MiniOrchard.Data.Providers
{
	using System;
	using FluentNHibernate.Cfg.Db;

	public class OracleDataServicesProvider : AbstractDataServicesProvider
	{
		private readonly string _dataFolder;
		private readonly string _connectionString;

		public OracleDataServicesProvider(string dataFolder, string connectionString)
		{
			_dataFolder = dataFolder;
			_connectionString = connectionString;
		}

		public static string ProviderName
		{
			get { return "Oracle"; }
		}

		public override IPersistenceConfigurer GetPersistenceConfigurer(bool createDatabase)
		{
			var persistence = OracleClientConfiguration.Oracle10;
			if (string.IsNullOrEmpty(_connectionString))
			{
				throw new ArgumentException("The connection string is empty");
			}
			persistence = persistence.ConnectionString(_connectionString);
			return persistence;
		}
	}
}
