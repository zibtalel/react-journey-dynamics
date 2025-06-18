namespace Crm.Project.Rest.Model.Mappings
{
	using AutoMapper;

	using Crm.Library.AutoMapper;
	using Crm.Model;
	using Crm.Project.Model;
	using Crm.Rest.Model;

	public class PotentialRestMap : IAutoMap
	{
		public virtual void CreateMap(IProfileExpression mapper)
		{
			mapper.CreateMap<Potential, PotentialRest>()
				.IncludeBase<Contact, ContactRest>()
				.ForMember(t => t.Parent, m => m.MapFromProxy(x => x.Parent).As<Company>())
				.ForMember(t => t.ResponsibleUserUser, m => m.MapFrom(s => s.ResponsibleUserObject));

			mapper.CreateMap<PotentialRest, Potential>()
				.IncludeBase<ContactRest, Contact>();
		}
	}
}
