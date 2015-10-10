namespace MiniOrchard.Component
{
	using System.Collections.Generic;
	using MiniOrchard.Field;

	public interface IComponent
	{
		IEnumerable<IField> Fields { get; set; }
	}
}
