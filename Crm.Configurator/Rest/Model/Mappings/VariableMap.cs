namespace Crm.Configurator.Rest.Model.Mappings
{
	using AutoMapper;

	using Crm.Configurator.Model;
	using Crm.Library.AutoMapper;
	using Crm.Model;
	using Crm.Rest.Model;

	public class VariableMap : IAutoMap
	{
		public virtual void CreateMap(IProfileExpression mapper)
		{
			mapper.CreateMap<Variable, VariableRest>()
				.IncludeBase<Contact, ContactRest>();
			mapper.CreateMap<VariableRest, Variable>()
				.IncludeBase<ContactRest, Contact>();
		}
	}
}
