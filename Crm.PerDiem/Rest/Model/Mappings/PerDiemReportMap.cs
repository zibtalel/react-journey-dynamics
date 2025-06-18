namespace Crm.PerDiem.Rest.Model.Mappings;

using AutoMapper;

using Crm.Library.AutoMapper;
using Crm.PerDiem.Model;
using Crm.Rest.Model;

public class PerDiemReportMap : IAutoMap
{
	public virtual void CreateMap(IProfileExpression mapper)
	{
		mapper.CreateMap<PerDiemReport, DocumentGeneratorEntry>()
			.IncludeBase<object, DocumentGeneratorEntry>()
			.ForMember(x => x.ErrorMessage, m => m.MapFrom(x => x.SendingError))
			;
	}
}
