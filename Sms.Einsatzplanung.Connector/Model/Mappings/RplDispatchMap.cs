namespace Sms.Einsatzplanung.Connector.Model.Mappings
{
	using System;

	using Crm.Library.BaseModel.Mappings;

	using NHibernate.Mapping.ByCode;
	using NHibernate.Type;

	public class RplDispatchMap : EntityClassMapping<RplDispatch>
	{
		public RplDispatchMap()
		{
			Schema("RPL");
			Table("Dispatch");
			Discriminator(x => x.Column("Type"));

			Id(x => x.Id, m =>
			{
				m.Column("InternalId");
				m.Generator(LMobile.Unicore.NHibernate.GuidCombGeneratorDef.Instance);
				m.UnsavedValue(Guid.Empty);
			});

			Version(x => x.Version, m =>
			{
				m.Generated(VersionGeneration.Never);
				m.UnsavedValue(0);
				m.Type(new Int32Type());
			});

			Property(x => x.Start);
			Property(x => x.Stop);
			Property(x => x.Fix);
			Property(x => x.Person, m => m.Column("ResourceKey"));
			Property(x => x.IsActive);
			Property(x => x.OrderKey, map => map.Column("DispatchOrderKey"));
			Property(x => x.Released, map => map.Column("DispatchReleased"));
			Property(x => x.Closed, map => map.Column("DispatchClosed"));
			Property(x => x.TechnicianInformation, map => map.Column("DispatchTechnicianInformation"));
			Property(x => x.InternalInformation);

			ManyToOne(x => x.Dispatch, m =>
			{
				m.Column("LegacyId");
				m.Fetch(FetchKind.Select);
				m.Lazy(LazyRelation.Proxy);
				m.NotFound(NotFoundMode.Ignore);
			});

		}
	}
}