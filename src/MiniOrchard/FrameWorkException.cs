using System;
using System.Runtime.Serialization;

namespace MiniOrchard
{
	[Serializable]
	public class FrameWorkException : ApplicationException
	{
		public FrameWorkException(string message)
			: base(message)
		{
		}

		public FrameWorkException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected FrameWorkException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}