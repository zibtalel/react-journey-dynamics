namespace Crm.Project.Rest.Model.Mappings
{
	using AutoMapper;

	using Crm.Library.AutoMapper;
	using Crm.Library.EntityConfiguration;
	using Crm.Project.Model;
	using Crm.Project.Model.Configuration;

	public class ProjectMap : IAutoMap
	{
		public virtual void CreateMap(IProfileExpression mapper)
		{
			mapper.CreateMap<Project, TimelineEvent>()
				.ForMember(x => x.Id, m => m.MapFrom(x => x.Id))
				.ForMember(x => x.Summary, m => m.MapFrom(x => x.GetIcsSummary()))
				.ForMember(x => x.IsAllDay, m => m.MapFrom(x => true))
				.ForMember(x => x.Start, m => m.MapFrom(x => x.DueDate))
				;
		}
	}
}
