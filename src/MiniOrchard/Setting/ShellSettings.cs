namespace MiniOrchard.Setting
{

	public class ShellSettings : Settings<SiteSettings>, IAfterLoadActions
	{
		/// <summary>
		/// The name pf the tenant
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The database provider
		/// </summary>
		public string DataProvider { get; set; }

		/// <summary>
		/// The database connection string
		/// </summary>
		public string DataConnectionString { get; set; }

		/// <summary>
		/// The data table prefix added to table names for this tenant
		/// </summary>
		public string DataTablePrefix { get; set; }

		/// <summary>
		/// The host name of the tenant
		/// </summary>
		public string RequestUrlHost { get; set; }

		public string RequestUrlPrefix { get; set; }

		/// <summary>
		/// The encryption algorithm used for encryption services
		/// </summary>
		public string EncryptionAlgorithm { get; set; }

		/// <summary>
		/// The encryption key used for encryption services
		/// </summary>
		public string EncryptionKey { get; set; }

		/// <summary>
		/// The hash algorithm used for encryption services
		/// </summary>
		public string HashAlgorithm { get; set; }

		/// <summary>
		/// The hash key used for encryption services
		/// </summary>
		public string HashKey { get; set; }

		/// <summary>
		/// List of available themes for this tenant
		/// </summary>
		public string[] Themes { get; set; }

		public override bool Enabled
		{
			get { return true; }
		}

		public void AfterLoad()
		{

		}
	}
}
