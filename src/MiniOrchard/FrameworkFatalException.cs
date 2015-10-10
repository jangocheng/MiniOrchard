using System;
using System.Runtime.Serialization;

namespace MiniOrchard
{
	[Serializable]
	public class FrameworkFatalException : Exception
	{
		public FrameworkFatalException(string message)
			: base(message)
		{
		}

		public FrameworkFatalException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected FrameworkFatalException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

	}
}
