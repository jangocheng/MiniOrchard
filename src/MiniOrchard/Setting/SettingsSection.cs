namespace MiniOrchard.Setting
{
	using System.ComponentModel;
	using System.Configuration;

	public class SettingsSection : ConfigurationSection
	{
		[ConfigurationProperty("provider"), DefaultValue("JSONFile")]
		public string Provider { get { return this["provider"] as string; } }

		[ConfigurationProperty("name")]
		public string Name { get { return this["name"] as string; } }

		[ConfigurationProperty("path")]
		public string Path { get { return this["path"] as string; } }
	}
}
