namespace MiniOrchard.Data.Providers
{
	using System.Collections.Generic;

	public class SessionFactoryParameters : DataServiceParameters
	{
		public IEnumerable<RecordBlueprint> RecordDescriptors { get; set; }
		public bool CreateDatabase { get; set; }
	}
}
