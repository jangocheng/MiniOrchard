using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using MiniOrchard.Utility.Extensions;

namespace MiniOrchard.Data.Conventions
{
	public class HasManyToManyConvention : IHasManyToManyConvention
	{
		public void Apply(IManyToManyCollectionInstance instance)
		{
			var entityDatabaseName = instance.EntityType.Name.ToDatabaseName();
			var childDatabaseName = instance.ChildType.Name.ToDatabaseName();
			var name = GetTableName(entityDatabaseName, childDatabaseName);//对两个表名进行排序，然后连接组成中间表名

			instance.Table(name);
			instance.Key.Column(entityDatabaseName + "_ID");
			instance.Relationship.Column(childDatabaseName + "_ID");
		}

		private string GetTableName(string a, string b)
		{
			var r = System.String.CompareOrdinal(a, b);
			if (r > 0)
			{
				return string.Format("{0}_{1}", b, a);
			}
			else
			{
				return string.Format("{0}_{1}", a, b);
			}
		}
	}
}
