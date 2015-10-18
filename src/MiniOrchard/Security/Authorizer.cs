using MiniOrchard.Content;
using MiniOrchard.Security.Permissions;

namespace MiniOrchard.Security
{
	/// <summary>
	/// Authorization services for the current user
	/// </summary>
	public interface IAuthorizer : IDependency
	{
		/// <summary>
		/// Authorize the current user against a permission
		/// </summary>
		/// <param name="permission">A permission to authorize against</param>
		bool Authorize(Permission permission);

		/// <summary>
		/// Authorize the current user against a permission; if authorization fails, the specified
		/// message will be displayed
		/// </summary>
		/// <param name="permission">A permission to authorize against</param>
		/// <param name="message">A localized message to display if authorization fails</param>
		bool Authorize(Permission permission, string message);

		/// <summary>
		/// Authorize the current user against a permission for a specified content item;
		/// </summary>
		/// <param name="permission">A permission to authorize against</param>
		/// <param name="content">A content item the permission will be checked for</param>
		bool Authorize(Permission permission, IContent content);

		/// <summary>
		/// Authorize the current user against a permission for a specified content item;
		/// if authorization fails, the specified message will be displayed
		/// </summary>
		/// <param name="permission">A permission to authorize against</param>
		/// <param name="content">A content item the permission will be checked for</param>
		/// <param name="message">A localized message to display if authorization fails</param>
		bool Authorize(Permission permission, IContent content, string message);

	}

	public class Authorizer : IAuthorizer
	{
		private readonly IAuthorizationService _authorizationService;
		IUser _user;
		//private readonly INotifier _notifier;

		public Authorizer(
			//IAuthorizationService authorizationService
			//INotifier notifier,
			)
		{
			//_authorizationService = authorizationService;
			//_user = user;
			//_notifier = notifier;
		}


		public bool Authorize(Permission permission)
		{
			return Authorize(permission, null, null);
		}

		public bool Authorize(Permission permission, string message)
		{
			return Authorize(permission, null, message);
		}

		public bool Authorize(Permission permission, IContent content)
		{
			return Authorize(permission, content, null);
		}

		public bool Authorize(Permission permission, IContent content, string message)
		{
			if (_authorizationService == null)
				return false;

			if (_authorizationService.TryCheckAccess(permission, _user, content))
				return true;

			if (message != null)
			{
				//if (_user == null)
				//{
				//    //notifier error
				//    _notifier.Error(T("{0}. Anonymous users do not have {1} permission.",
				//                      message, permission.Name));
				//}
				//else
				//{
				//    //notifier error
				//    _notifier.Error(T("{0}. Current user, {2}, does not have {1} permission.",
				//                      message, permission.Name, _user.UserName));
				//}
			}

			return false;
		}
	}
}
