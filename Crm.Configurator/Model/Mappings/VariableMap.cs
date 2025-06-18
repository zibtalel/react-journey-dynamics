namespace Crm.Configurator.Model.Mappings
{
	using NHibernate.Mapping.ByCode;
	using NHibernate.Mapping.ByCode.Conformist;

	public class VariableMap : SubclassMapping<Variable>
	{
		public VariableMap()
		{
			DiscriminatorValue("Variable");

			ManyToOne(x => x.ConfigurationBase, m =>
			{
				m.Column("ParentKey");
				m.Insert(false);
				m.Update(false);
				m.Fetch(FetchKind.Select);
				m.Lazy(LazyRelation.Proxy);
			});
		}
	}
}