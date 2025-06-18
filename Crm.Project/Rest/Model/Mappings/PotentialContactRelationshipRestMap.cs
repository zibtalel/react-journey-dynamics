namespace Crm.Project.Rest.Model.Mappings
{
	using AutoMapper;

	using Crm.Library.AutoMapper;
	using Crm.Project.Model.Relationships;

	public class PotentialContactRelationshipRestMap : IAutoMap
	{
		public virtual void CreateMap(IProfileExpression mapper)
		{
			mapper.CreateMap<PotentialContactRelationship, PotentialContactRelationshipRest>()
				.ForMember(x => x.Potential, m => m.MapFrom(x => x.Parent));
		}
	}
}
