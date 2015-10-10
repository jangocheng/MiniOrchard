using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace MiniOrchard.Data.Conventions
{
	public class SubclassConvention : ISubclassConvention
	{
		public void Apply(ISubclassInstance instance)
		{
			instance.DiscriminatorValue(instance.EntityType.Name);
		}
	}
}
