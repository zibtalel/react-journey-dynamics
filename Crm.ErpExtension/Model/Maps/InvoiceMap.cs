namespace Crm.ErpExtension.Model.Maps
{
	using Crm.Library.BaseModel.Interfaces;

	using NHibernate.Mapping.ByCode;
	using NHibernate.Mapping.ByCode.Conformist;

	public class InvoiceMap : SubclassMapping<Invoice>, IDatabaseMapping
	{
		public InvoiceMap()
		{
			DiscriminatorValue("Invoice");
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

			Property(x => x.InvoiceNo);
			Property(x => x.InvoiceDate);
			Property(x => x.DunningLevel);
			Property(x => x.DueDate);
			Property(x => x.OutstandingBalance);
		}
	}
}
