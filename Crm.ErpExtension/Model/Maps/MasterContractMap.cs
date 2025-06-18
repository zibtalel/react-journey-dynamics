namespace Crm.ErpExtension.Model.Maps
{
	using Crm.Library.BaseModel.Interfaces;

	using NHibernate.Mapping.ByCode;
	using NHibernate.Mapping.ByCode.Conformist;

	public class MasterContractMap : SubclassMapping<MasterContract>, IDatabaseMapping
	{
		public MasterContractMap()
		{
			DiscriminatorValue("MasterContract");
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

			Property(x => x.QuantityShipped);
			Property(x => x.OrderConfirmationDate);
			Property(x => x.DueDate);
			Property(x => x.RemainingQuantity);
			Property(x => x.ItemNo);
			Property(x => x.Quantity);
		}
	}
}
