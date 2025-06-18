namespace Crm.ErpExtension.Model.Maps
{
	using Crm.Library.BaseModel.Interfaces;

	using NHibernate.Mapping.ByCode;
	using NHibernate.Mapping.ByCode.Conformist;

	public class InvoicePositionMap : SubclassMapping<InvoicePosition>, IDatabaseMapping
	{
		public InvoicePositionMap()
		{
			DiscriminatorValue("InvoicePosition");
			ManyToOne(x => x.Parent, map =>
			{
				map.Column("ParentKey");
				map.Fetch(FetchKind.Select);
				map.Lazy(LazyRelation.Proxy);
				map.Cascade(Cascade.None);
				map.Insert(false);
				map.Update(false);
			});
		}
	}
}