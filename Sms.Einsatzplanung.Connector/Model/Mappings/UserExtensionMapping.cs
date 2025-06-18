namespace Sms.Einsatzplanung.Connector.Model.Mappings
{
	using Crm.Library.Model;

	using LMobile.Unicore.NHibernate;

	using NHibernate.Mapping.ByCode;
	using NHibernate.Mapping.ByCode.Conformist;

	public class UserExtensionMapping : ComponentMapping<UserExtension>, INHibernateMappingExtension<User, UserExtension>
	{
		public UserExtensionMapping()
		{
			Property(x => x.PublicHolidayRegionKey);
			Property(x => x.HomeAddressId);
			ManyToOne(x => x.HomeAddress,
			m =>
			{
				m.Column("HomeAddressId");
				m.Fetch(FetchKind.Select);
				m.Update(false);
				m.Insert(false);
				m.Cascade(Cascade.None);
			});
		}
	}
}
