namespace Sms.Einsatzplanung.Connector.Rest.Mappings
{
	using AutoMapper;

	using Crm.Library.AutoMapper;

	using Sms.Einsatzplanung.Connector.Model;
	using Sms.Einsatzplanung.Connector.Rest.Model;

	public class AbsenceDispatchRestMap : IAutoMap
	{
		public virtual void CreateMap(IProfileExpression mapper)
		{
			mapper.CreateMap<AbsenceDispatchRest, AbsenceDispatch>()
				.ForMember(d => d.IsActive, m => m.Ignore());

			mapper.CreateMap<AbsenceDispatch, AbsenceDispatchRest>()
				.ForMember(d => d.IsActive, m => m.Ignore());
		}
	}
}
