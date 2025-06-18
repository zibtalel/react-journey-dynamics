namespace Crm.ErpExtension.Model.Maps
{
	using Crm.Library.BaseModel.Interfaces;

	using NHibernate.Mapping.ByCode;
	using NHibernate.Mapping.ByCode.Conformist;

	public class QuoteMap : SubclassMapping<Quote>, IDatabaseMapping
	{
		public QuoteMap()
		{
			DiscriminatorValue("Quote");
			Set(x => x.Positions, m =>
			{
				m.Key(k =>
				{
					k.Column("ParentKey");
					k.NotNullable(true);
				});
				m.Inverse(true);
				m.Lazy(CollectionLazy.Lazy);
				m.Cascade(Cascade.Remove);
			}, action => action.OneToMany());

			Property(x => x.QuoteNo);
			Property(x => x.QuoteDate);
			Property(x => x.DocumentDate11);
			Property(x => x.DueDate);
		}
	}
}
