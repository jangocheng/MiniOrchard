namespace MiniOrchard.Logging
{
	using System;

	public interface ILoggerFactory
	{
		ILogger CreateLogger(Type type);
	}
}
