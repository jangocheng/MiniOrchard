namespace MiniOrchard.Security
{
	/// <summary>
	/// user name & password validate service
	/// </summary>
	public interface IMembershipService : IDependency
	{
		MembershipSettings GetSettings();

		IUser CreateUser(CreateUserParams createUserParams);
		IUser GetUser(string username);

		IUser ValidateUser(string userNameOrEmail, string password, string domain);
		void SetPassword(IUser user, string password);
	}
}
