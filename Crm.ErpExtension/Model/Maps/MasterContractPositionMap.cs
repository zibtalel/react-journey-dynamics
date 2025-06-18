namespace Crm.ErpExtension.Model.Maps
{
	using Crm.Library.BaseModel.Interfaces;

	using NHibernate.Mapping.ByCode;
	using NHibernate.Mapping.ByCode.Conformist;

	public class MasterContractPositionMap : SubclassMapping<MasterContractPosition>, IDatabaseMapping
	{
		public MasterContractPositionMap()
		{
			DiscriminatorValue("MasterContractPosition");
			ManyToOne(x => x.Parent, map =>
			{
				map.Column("ParentKey");
				map.Fetch(FetchKind.Select);
				map.Lazy(LazyRelation.Proxy);
				map.Cascade(Cascade.None);
				map.Insert(false);
				map.Update(false);
			});

			Property(x => x.QuantityShipped);
			Property(x => x.RemainingQuantity);
		}
	}
}