using MiniOrchard.Security.Permissions;

namespace MiniOrchard.Security
{
	/// <summary>
	/// Entry-point for configured authorization scheme. Role-based system
	/// provided by default. 
	/// </summary>
	public interface IAuthorizationService : IDependency
	{
		void CheckAccess(Permission permission, IUser user);
		bool TryCheckAccess(Permission permission, IUser user);
	}
}