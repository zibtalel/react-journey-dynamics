namespace Crm.Configurator.Rest.Model.Mappings
{
	using AutoMapper;

	using Crm.Configurator.Model;
	using Crm.Library.AutoMapper;
	using Crm.Model;
	using Crm.Rest.Model;

	public class ConfigurationBaseMap : IAutoMap
	{
		public virtual void CreateMap(IProfileExpression mapper)
		{
			mapper.CreateMap<ConfigurationBase, ConfigurationBaseRest>()
				.IncludeBase<Contact, ContactRest>();
			mapper.CreateMap<ConfigurationBaseRest, ConfigurationBase>()
				.IncludeBase<ContactRest, Contact>();
		}
	}
}
