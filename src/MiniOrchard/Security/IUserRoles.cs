namespace MiniOrchard.Security
{
	using System.Collections.Generic;
	using MiniOrchard.Content;

	public interface IUserRoles : IContent
	{
		IList<string> Roles { get; }
	}
}
