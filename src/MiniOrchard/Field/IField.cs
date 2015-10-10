namespace MiniOrchard.Field
{
	public interface IField
	{
		FieldPermission Permission { get; set; }
		string Name { get; set; }
	}
}
