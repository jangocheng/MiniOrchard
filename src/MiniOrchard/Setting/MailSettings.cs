namespace MiniOrchard.Setting
{
	using System.Collections.ObjectModel;
	using System.Linq;

	public class MailSettings : Settings<MailSettings>, IAfterLoadActions
	{
		public MailSettings()
		{
			Settings = new ObservableCollection<MailSetting>();
		}

		public string MailServer { get; set; }
		public string TemplateDir { get; set; }
		public string HostName { get; set; }
		public ObservableCollection<MailSetting> Settings { get; set; }

		public override bool Enabled
		{
			get { return Settings.Any(); }
		}

		public void AfterLoad()
		{
			Settings.ForEach(c => c.AfterLoad());
		}
	}

	public class MailSetting : IAfterLoadActions, ISettingsCollectionItem<MailSetting>
	{
		public string Name { get; set; }
		public string MailFrom { get; set; }
		public ObservableCollection<string> MailTo { get; set; }
		public ObservableCollection<string> CcTo { get; set; }
		public string Subject { get; set; }
		public string TemplateName { get; set; }

		public void AfterLoad()
		{

		}

		public bool Equals(MailSetting obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((MailSetting)obj);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((MailSetting)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int hashCode = 0;
				foreach (var n in MailTo)
					hashCode = (hashCode * 397) ^ n.GetHashCode();
				foreach (var n in CcTo)
					hashCode = (hashCode * 397) ^ n.GetHashCode();
				hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);

				return hashCode;
			}
		}
	}
}
