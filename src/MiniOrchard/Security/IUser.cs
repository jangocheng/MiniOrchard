namespace MiniOrchard.Security
{
	using MiniOrchard.Content;

	/// <summary>
	/// Interface provided by the "User" model. 
	/// </summary>
	public interface IUser : IContent
	{
		string UserName { get; }
		string Email { get; }
	}
}
