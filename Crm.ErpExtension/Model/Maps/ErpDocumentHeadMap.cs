namespace Crm.ErpExtension.Model.Maps
{
	using Crm.Library.BaseModel.Interfaces;

	using NHibernate.Mapping.ByCode;
	using NHibernate.Mapping.ByCode.Conformist;

	public class ErpDocumentHeadMap : SubclassMapping<ErpDocumentHead>, IDatabaseMapping
	{
		public ErpDocumentHeadMap()
		{
			Property(x => x.CompanyNo);
			Property(x => x.PaymentTerms);
			Property(x => x.PaymentMethod);
			Property(x => x.TermsOfDelivery);
			Property(x => x.DeliveryMethod);
			Property(x => x.ContactKey);
			Property(x => x.OrderNo);
			Property(x => x.OrderType);
			Property(x => x.OrderDate);
			Property(x => x.Commission);
			ManyToOne(x => x.Contact, map =>
			{
				map.Column("ContactKey");
				map.Fetch(FetchKind.Select);
				map.Lazy(LazyRelation.Proxy);
				map.Cascade(Cascade.None);
				map.Insert(false);
				map.Update(false);
			});
		}
	}
}