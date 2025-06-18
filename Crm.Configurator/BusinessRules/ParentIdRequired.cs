namespace Crm.Configurator.BusinessRules
{
	using Crm.Configurator.Model;
	using Crm.Library.Validation.BaseRules;

	public class ParentIdRequired : RequiredRule<Variable>
	{
		public ParentIdRequired()
		{
			Init(x => x.ParentId, "ConfigurationBase");
		}
	}
}