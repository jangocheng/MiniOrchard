using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using MiniOrchard.Utility.Extensions;

namespace MiniOrchard.Data.Conventions
{
	/// <summary>
	/// 对于一对多的关系，使用父方的类名作为属性名，表中使用父表的主键列名作为对应的外键列的列名。
	/// 比如一个班对应多个学生，在Class类中就有public virtual IList<Student> Students{get;set;}，
	/// 而在Student类中就必须使用Class作为属性名：public virtual Class Class{get;set;}
	/// </summary>
	public class CollectionConvention : ICollectionConvention
	{
		public void Apply(ICollectionInstance instance)
		{
			string colName = string.Empty;
			var entityType = instance.ChildType;
			var childType = instance.ChildType;
			if (entityType == childType)
			{
				colName = "PARENT_ID";
			}
			else
			{
				colName = entityType.Name.ToDatabaseName() + "_ID";
			}
			instance.Key.Column(colName);
			instance.Cascade.AllDeleteOrphan();
		}
	}

	/// <summary>
	/// 一对多的情况，需要设置“一”的一方的Collection和“多”的一方的Reference
	/// </summary>
	public class ReferenceConvention : IReferenceConvention
	{
		public void Apply(IManyToOneInstance instance)
		{
			string colName = string.Empty;
			var referenceType = instance.Class.GetUnderlyingSystemType();
			var entityType = instance.EntityType;
			var propertyName = instance.Property.Name;
			if (referenceType == entityType) // self association
			{
				colName = "PARENT_ID";
			}
			else
			{
				colName = propertyName.ToDatabaseName().ToUpper();
			}
			instance.Column(colName);
		}
	}
}
