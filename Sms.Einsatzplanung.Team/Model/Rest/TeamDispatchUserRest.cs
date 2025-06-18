namespace Sms.Einsatzplanung.Team.Model.Rest
{
	using System;

	using Crm.Library.Api.Attributes;
	using Crm.Library.Rest;

	[RestrictedType]
	[RestTypeFor(DomainType = typeof(TeamDispatchUser))]
	public class TeamDispatchUserRest : RestEntity
	{
		public Guid Id { get; set; }
		public Guid DispatchId { get; set; }
		public string Username { get; set; }
		public bool IsNonTeamMember { get; set; }
	}
}