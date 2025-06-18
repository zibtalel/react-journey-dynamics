namespace Crm.ErpExtension.Model.Maps
{
	using Crm.Library.BaseModel.Interfaces;

	using NHibernate.Mapping.ByCode;
	using NHibernate.Mapping.ByCode.Conformist;

	public class DeliveryNoteMap : SubclassMapping<DeliveryNote>, IDatabaseMapping
	{
		public DeliveryNoteMap()
		{
			DiscriminatorValue("DeliveryNote");
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

			Property(x => x.DeliveryNoteNo, map => map.Column("DeliverNoteNo"));
			Property(x => x.DeliveryNoteDate);
		}
	}
}
