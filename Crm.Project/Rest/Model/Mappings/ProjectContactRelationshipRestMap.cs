namespace Crm.Project.Rest.Model.Mappings
{
	using AutoMapper;

	using Crm.Library.AutoMapper;
	using Crm.Project.Model.Relationships;

	public class ProjectContactRelationshipRestMap : IAutoMap
	{
		public virtual void CreateMap(IProfileExpression mapper)
		{
			mapper.CreateMap<ProjectContactRelationship, ProjectContactRelationshipRest>()
				.ForMember(x => x.Project, m => m.MapFrom(x => x.Parent));
		}
	}
}
