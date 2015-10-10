namespace MiniOrchard.Setting
{
	using System.Collections.ObjectModel;
	using System.Linq;

	public class ScheduleSettings : Settings<ScheduleSettings>, IAfterLoadActions
	{
		public ObservableCollection<JobTriggerSetting> Triggers { get; set; }

		public override bool Enabled
		{
			get { return Triggers.Any(); }
		}

		public void AfterLoad()
		{
			//to-do
		}

		public class JobTriggerSetting : ISettingsCollectionItem<JobTriggerSetting>, IAfterLoadActions
		{
			public const string DefaultGroup = "DEFAULT";

			public JobTriggerSetting()
			{
				Group = DefaultGroup;
			}

			public string Group { get; set; }
			public string Name { get; set; }
			public string JobName { get; set; }
			public string Identity { get; set; }
			public string Cron { get; set; }

			public bool Equals(JobTriggerSetting obj)
			{
				if (ReferenceEquals(null, obj)) return false;
				if (ReferenceEquals(this, obj)) return true;
				if (obj.GetType() != this.GetType()) return false;
				return Equals((JobTriggerSetting)obj);
			}

			public override bool Equals(object obj)
			{
				if (ReferenceEquals(null, obj)) return false;
				if (ReferenceEquals(this, obj)) return true;
				if (obj.GetType() != this.GetType()) return false;
				return Equals((JobTriggerSetting)obj);
			}

			public void AfterLoad()
			{
				//to-do
			}

			public override int GetHashCode()
			{
				unchecked
				{
					int hashCode = 0;
					hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
					hashCode = (hashCode * 397) ^ (Group != null ? Group.GetHashCode() : 0);
					hashCode = (hashCode * 397) ^ (JobName != null ? JobName.GetHashCode() : 0);
					hashCode = (hashCode * 397) ^ (Identity != null ? Identity.GetHashCode() : 0);
					hashCode = (hashCode * 397) ^ (Cron != null ? Cron.GetHashCode() : 0);

					return hashCode;
				}
			}
		}
	}
}
