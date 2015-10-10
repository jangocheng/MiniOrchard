using System;
using System.Runtime.Serialization;

namespace MiniOrchard
{
	[Serializable]
	public class FrameworkCoreException : Exception
	{
		public FrameworkCoreException(string message)
			: base(message)
		{

		}

		public FrameworkCoreException(string message, Exception innerException)
			: base(message, innerException)
		{

		}

		protected FrameworkCoreException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}