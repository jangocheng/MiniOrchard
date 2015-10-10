namespace MiniOrchard.Component
{
	using Castle.Core.Logging;

	public abstract class Component : IDependency
	{
		protected Component()
		{
			Logger = NullLogger.Instance;
			Permission = ComponentPermission.Readonly;
		}

		public ILogger Logger { get; set; }
		public ComponentPermission Permission { get; set; }
	}
}
