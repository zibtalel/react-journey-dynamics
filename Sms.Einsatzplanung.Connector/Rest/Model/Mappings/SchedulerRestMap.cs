namespace Sms.Einsatzplanung.Connector.Rest.Model.Mappings
{
	using AutoMapper;

	using Crm.Library.AutoMapper;

	using Sms.Einsatzplanung.Connector.Model;

	public class SchedulerRestMap : IAutoMap
	{
		public virtual void CreateMap(IProfileExpression mapper)
		{
			mapper.CreateMap<Scheduler, SchedulerRest>()
				.ForMember(x => x.ManifestVersion, m => m.MapFrom(x => x.ManifestVersion.ToString(4)))
				;
		}
	}
}
