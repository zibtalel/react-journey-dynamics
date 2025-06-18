namespace Sms.Einsatzplanung.Team.Model.Map
{
	using System;

	using Crm.Library.BaseModel.Mappings;

	using NHibernate.Mapping.ByCode;

	public class TeamDispatchUserMap : EntityClassMapping<TeamDispatchUser>
	{
		public TeamDispatchUserMap()
		{
			Schema("SMS");
			Table("TeamDispatchUser");

			Id(x => x.Id, map =>
			{
				map.Column("TeamDispatchUsersId");
				map.Generator(LMobile.Unicore.NHibernate.GuidCombGeneratorDef.Instance);
				map.UnsavedValue(Guid.Empty);
			});

			Property(x => x.DispatchId);
			ManyToOne(x => x.Dispatch, map =>
			{
				map.Column("DispatchId");
				map.Fetch(FetchKind.Select);
				map.Lazy(LazyRelation.Proxy);
				map.Cascade(Cascade.None);
				map.Insert(false);
				map.Update(false);
			});

			Property(x => x.Username);
			ManyToOne(x => x.User, map =>
			{
				map.Column("Username");
				map.Fetch(FetchKind.Select);
				map.Lazy(LazyRelation.Proxy);
				map.Cascade(Cascade.None);
				map.Insert(false);
				map.Update(false);
			});

			Property(x => x.IsNonTeamMember);
		}
	}
}