namespace Crm.Project.Rest.Model.Mappings
{
	using AutoMapper;

	using Crm.Library.AutoMapper;
	using Crm.Model;
	using Crm.Project.Model;
	using Crm.Rest.Model;

	public class ProjectRestMap : IAutoMap
	{
		public virtual void CreateMap(IProfileExpression mapper)
		{
			mapper.CreateMap<Project, ProjectRest>()
				.IncludeBase<Contact, ContactRest>()
				.ForMember(t => t.Competitor, m => m.MapFromProxy(x => x.Competitor).As<Company>())
				.ForMember(t => t.Parent, m => m.MapFromProxy(x => x.Parent).As<Company>())
				.ForMember(t => t.Potential, m => m.MapFromProxy(x => x.Potential))
				.ForMember(t => t.ResponsibleUserUser, m => m.MapFrom(s => s.ResponsibleUserObject));

			mapper.CreateMap<ProjectRest, Project>()
				.IncludeBase<ContactRest, Contact>();
		}
	}
}
