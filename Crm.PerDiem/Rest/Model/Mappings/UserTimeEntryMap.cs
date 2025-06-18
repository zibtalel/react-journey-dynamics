namespace Crm.PerDiem.Rest.Model.Mappings
{
	using AutoMapper;

	using Crm.Library.AutoMapper;
	using Crm.PerDiem.Model;

	public class UserTimeEntryMap : IAutoMap
	{
		public virtual void CreateMap(IProfileExpression mapper)
		{
			mapper.CreateMap<UserTimeEntry, UserTimeEntryRest>()
				.IncludeBase<TimeEntry, TimeEntryRest>();
			mapper.CreateMap<UserTimeEntryRest, UserTimeEntry>()
				.IncludeBase<TimeEntryRest, TimeEntry>();
		}
	}
}
