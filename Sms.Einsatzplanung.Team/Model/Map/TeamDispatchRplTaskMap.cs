namespace Sms.Einsatzplanung.Team.Model.Map
{
	using NHibernate.Mapping.ByCode.Conformist;

	public class TeamDispatchRplTaskMap : SubclassMapping<RplTeamDispatch>
	{
		public TeamDispatchRplTaskMap()
		{
			DiscriminatorValue("TeamDispatch");

			Property(x => x.TeamId);
		}
	}
}
