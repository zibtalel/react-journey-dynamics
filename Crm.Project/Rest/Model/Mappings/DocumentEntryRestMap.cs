namespace Crm.Project.Rest.Model.Mappings {
	using AutoMapper;

	using Crm.Library.AutoMapper;
	using Crm.Project.Model;

	public class DocumentEntryRestMap : IAutoMap {
		public virtual void CreateMap(IProfileExpression mapper) {
			mapper.CreateMap<DocumentEntry, DocumentEntryRest>()
				.ForMember(x => x.IsActive, m => m.Ignore());
		}
	}
}
