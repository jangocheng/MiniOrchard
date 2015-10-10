namespace MiniOrchard.Setting
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Reflection;
	using MiniOrchard.Logging;

	public abstract class Settings<T> : Settings where T : class
	{
		private ILogger _logger = NullLogger.Instance;
		private PropertyInfo[] _properties;
		private IEnumerable<PropertyInfo> Properties
		{
			get
			{
				return _properties ?? (_properties =
									   GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty));
			}
		}

		/// <summary>
		/// Updates this settings object, return true if there was an actual change
		/// </summary>
		public virtual bool UpdateSettings(T newSettings)
		{
			_logger.Error("Settings updated! " + GetType(), null);
			if (newSettings == null)
			{
				_logger.Error("Updated settings for " + typeof(T).Name + " were null, aborting.", null);
				return false;
			}

			bool changed = false;
			try
			{
				var toCopy = Properties;
				foreach (var prop in toCopy)
				{
					if (!prop.CanWrite) continue;

					var current = prop.GetValue(this, null);
					var newSetting = prop.GetValue(newSettings, null);

					// observables are meant to be updated at the member level and need specific love
					if (prop.PropertyType.IsGenericType &&
						prop.PropertyType.GetGenericTypeDefinition() == typeof(ObservableCollection<>))
					{
						// handle collections!
						// TODO: Decide on how collections (and subcollections) are handled
						// need to broadcast these changes or publish them via json APIs
					}
					else if (prop.CanWrite)
					{
						try
						{
							if (current == newSetting) continue; // nothing changed, NEXT
							prop.SetValue(this, newSetting, null);
							OnPropertyChanged(prop.Name);
							changed = true;
						}
						catch (Exception e)
						{
							_logger.Error("Error setting propery: " + prop.Name, e);
						}
					}
				}
			}
			catch (Exception e)
			{
				_logger.Error("Error updating settings for " + typeof(T).Name, e);
			}

			if (changed)
				TriggerChanged();
			return changed;
		}
	}

	public abstract class Settings : INotifyPropertyChanged, ISecurableSection
	{
		/// <summary>
		/// Whether this section is enabled (has servers, has connection, etc.)
		/// </summary>
		public abstract bool Enabled { get; }

		/// <summary>
		/// Semilcolon delimited list of security groups that can see this section, but not perform actions
		/// </summary>
		public string ViewGroups { get; set; }

		/// <summary>
		/// Semilcolon delimited list of security groups that can do anything in this section, including management actions
		/// </summary>
		public string AdminGroups { get; set; }

		/// <summary>
		/// Fired when anything on this settings object changes, once per reload and not on every property
		/// </summary>
		public event EventHandler Changed;

		protected void TriggerChanged()
		{
			var handler = Changed;
			if (handler != null) handler(this, EventArgs.Empty);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
