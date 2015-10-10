using System;
using System.Runtime.Serialization;

namespace MiniOrchard.Security
{
	[Serializable]
	public class FrameworkSecurityException : FrameworkCoreException
	{
		public FrameworkSecurityException(string message) : base(message) { }
		public FrameworkSecurityException(string message, Exception innerException) : base(message, innerException) { }
		protected FrameworkSecurityException(SerializationInfo info, StreamingContext context) : base(info, context) { }

		public string PermissionName { get; set; }
		public IUser User { get; set; }
	}
}
