namespace MiniOrchard.Logging
{
	using System;

	public class NullLoggerFactory : ILoggerFactory
	{
		public ILogger CreateLogger(Type type)
		{
			return NullLogger.Instance;
		}
	}
}
