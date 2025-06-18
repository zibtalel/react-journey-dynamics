namespace Crm.PerDiem.Rest.Model.Mappings
{
	using System;

	using AutoMapper;

	using Crm.Library.AutoMapper;
	using Crm.PerDiem.Model;

	public class TimeEntryRestMap : IAutoMap
	{
		public virtual void CreateMap(IProfileExpression mapper)
		{
			mapper.CreateMap<TimeEntry, TimeEntryRest>()
				.ForMember(x => x.Duration, m => m.MapFrom(x => x.DurationInMinutes.HasValue ? TimeSpan.FromMinutes(x.DurationInMinutes.Value) : (TimeSpan?)null));
			mapper.CreateMap<TimeEntryRest, TimeEntry>()
				.ForMember(x => x.DurationInMinutes, m => m.MapFrom(x => x.Duration.HasValue ? (int)x.Duration.Value.TotalMinutes : (int?)null));
		}
	}
}
