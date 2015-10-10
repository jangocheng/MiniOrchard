namespace MiniOrchard.Setting.Providers
{
	using System;
	using System.Configuration;

	public abstract class SettingsProvider
	{
		public abstract string ProviderType { get; }

		public string Name { get; set; }
		public string Path { get; set; }

		// Accessors for built-in types
		public MailSettings Mail { get { return GetSettings<MailSettings>(); } }
		public PageSettings Page { get { return GetSettings<PageSettings>(); } }
		public SiteSettings Site { get { return GetSettings<SiteSettings>(); } }

		public abstract T GetSettings<T>() where T : Settings<T>, new();
		public abstract T SaveSettings<T>(T settings) where T : class, new();

		protected SettingsProvider(SettingsSection settings)
		{
			Name = settings.Name;
			Path = settings.Path;
		}

		private static SettingsProvider _current;

		public static SettingsProvider Current { get { return _current ?? (_current = GetCurrentProvider()); } }
		public static SettingsProvider GetCurrentProvider()
		{
			var section = ConfigurationManager.GetSection("Settings") as SettingsSection;
			if (section == null)
			{
				throw new ConfigurationErrorsException("No settings section found in the config");
			}
			var provider = section.Provider;
			if (!provider.EndsWith("SettingsProvider")) provider += "SettingsProvider";
			if (!provider.Contains(".")) provider = "TSMC.BOS.Core.Settings.SettingsProviders." + provider;

			var t = Type.GetType(provider, false);
			if (t == null)
			{
				throw new ConfigurationErrorsException(string.Format("Could not resolve type '{0}' ('{1}')", section.Provider, provider));
			}
			var p = (SettingsProvider)Activator.CreateInstance(t, section);
			return p;
		}
	}
}
