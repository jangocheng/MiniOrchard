using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using MiniOrchard.Utility.Extensions;

namespace MiniOrchard.Data.Conventions
{
	public class PropertyConvention : IPropertyConvention
	{
		public void Apply(IPropertyInstance instance)
		{
			instance.Column(instance.Name.ToDatabaseName());
		}
	}
}
