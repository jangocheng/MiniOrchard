using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using MiniOrchard.Utility.Extensions;

namespace MiniOrchard.Data.Conventions
{
	/// <summary>
	/// 类中的主键使用Id命名，表中的主键使用表名+“_ID”的命名方式。
	/// 比如CostCenter中有public virtual long Id{get;set;}，对应表中的列COST_CENTER_ID
	/// </summary>
	public class HiLoIdConvention : IIdConvention
	{
		public const string NextHiValueColumnName = "VALUE";
		public const string NHibernateHiLoIdentityTableName = "HIBERNATE_UNIQUE_KEY";
		public const string TableColumnName = "TABLE_NAME";

		public void Apply(IIdentityInstance instance)
		{
			var tableName = instance.EntityType.Name.ToDatabaseName();
			instance.Column("ID");
			if (instance.Type == typeof(long))//接下来设置主键的生成方式为HiLo值方式
			{
				instance.GeneratedBy.HiLo(
					NHibernateHiLoIdentityTableName,
					NextHiValueColumnName,
					"1000000000",
					builder => builder.AddParam("where", string.Format("{0} = '{1}'", TableColumnName, tableName)));
			}
		}
	}
}
