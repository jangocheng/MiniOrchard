namespace MiniOrchard.Data.Providers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using FluentNHibernate;
	using FluentNHibernate.Automapping;
	using FluentNHibernate.Automapping.Alterations;
	using FluentNHibernate.Cfg;
	using FluentNHibernate.Cfg.Db;
	using FluentNHibernate.Conventions;
	using FluentNHibernate.Conventions.Helpers;
	using MiniOrchard.Data.Conventions;
	using NHibernate.Cfg;

	public abstract class AbstractDataServicesProvider : IDataServicesProvider
	{
		public abstract IPersistenceConfigurer GetPersistenceConfigurer(bool createDatabase);

		public Configuration BuildConfiguration(SessionFactoryParameters parameters)
		{
			var database = GetPersistenceConfigurer(parameters.CreateDatabase);
			var persistenceModel = CreatePersistenceModel(parameters.RecordDescriptors);

			return Fluently.Configure()
				.Database(database)
				.Mappings(m => m.AutoMappings.Add(persistenceModel))
				.BuildConfiguration();
		}

		public static AutoPersistenceModel CreatePersistenceModel(IEnumerable<RecordBlueprint> recordDescriptors)
		{
			return AutoMap.Source(new TypeSource(recordDescriptors))
				// Ensure that namespaces of types are never auto-imported, so that 
				// identical type names from different namespaces can be mapped without ambiguity
				.Conventions.Setup(x => x.Add(AutoImport.Never()))
				//.Conventions.Setup(GetConventions())
				.Conventions.Add(new HiLoIdConvention())
				.Conventions.Add(new TableNameConvention(recordDescriptors))
				.Alterations(alt =>
				{
					foreach (var recordAssembly in recordDescriptors.Select(x => x.Type.Assembly).Distinct())
					{
						alt.Add(new AutoMappingOverrideAlteration(recordAssembly));
					}
					alt.AddFromAssemblyOf<DataModule>();
					//alt.Add(new ContentItemAlteration(recordDescriptors));
				});
			//.Conventions.AddFromAssemblyOf<DataModule>();
		}

		protected static Action<IConventionFinder> GetConventions()
		{
			return finder =>
			{
				//finder.Add<ClassNameConvention>();
				//finder.Add<HiLoIdConvention>();
				//finder.Add<CollectionConvention>();
				//finder.Add<ReferenceConvention>();
				//finder.Add<HasManyConvention>();
				//finder.Add<SubclassConvention>();
				//finder.Add<SubclassConvention>();
				//finder.Add<PropertyConvention>();
				//finder.Add<EnumConvention>();
				//finder.Add<HasManyToManyConvention>();
			};
		}

		class TypeSource : ITypeSource
		{
			private readonly IEnumerable<RecordBlueprint> _recordDescriptors;

			public TypeSource(IEnumerable<RecordBlueprint> recordDescriptors) { _recordDescriptors = recordDescriptors; }

			public IEnumerable<Type> GetTypes() { return _recordDescriptors.Select(descriptor => descriptor.Type); }


			public string GetIdentifier()
			{
				return Guid.NewGuid().ToString();
			}

			public void LogSource(FluentNHibernate.Diagnostics.IDiagnosticLogger logger)
			{

			}
		}
	}
}