namespace MiniOrchard.Security
{
	using System.Collections.Generic;

	public interface IRole
	{
		string Name { get; set; }
		IList<IRolesPermissions> RolesPermissions { get; set; }
	}
}
