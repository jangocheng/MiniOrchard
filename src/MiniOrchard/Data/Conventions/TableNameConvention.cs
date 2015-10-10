using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace MiniOrchard.Data.Conventions
{
	public class TableNameConvention : IClassConvention
	{
		private readonly Dictionary<Type, RecordBlueprint> _descriptors;

		public TableNameConvention(IEnumerable<RecordBlueprint> descriptors)
		{
			_descriptors = descriptors.ToDictionary(d => d.Type);
		}

		public void Apply(IClassInstance instance)
		{
			RecordBlueprint desc;
			if (_descriptors.TryGetValue(instance.EntityType, out desc))
			{
				instance.Table(desc.TableName);
			}
		}
	}
}