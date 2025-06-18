namespace Crm.ErpExtension.Model.Maps
{
	using Crm.Library.BaseModel.Interfaces;

	using NHibernate.Mapping.ByCode;
	using NHibernate.Mapping.ByCode.Conformist;

	public class CreditNoteMap : SubclassMapping<CreditNote>, IDatabaseMapping
	{
		public CreditNoteMap()
		{
			DiscriminatorValue("CreditNote");
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

			Property(x => x.CreditNoteNo);
			Property(x => x.CreditNoteDate);
			Property(x => x.InvoiceNo);
		}
	}
}
