namespace Crm.Configurator.Model
{
	using System.Collections.Generic;

	using Crm.Article.Model;

	public class ConfigurationBase : Article
	{
		public virtual ICollection<ConfigurationRule> ConfigurationRules { get; set; }
		public virtual ICollection<Variable> Variables { get; set; }

		public ConfigurationBase()
		{
			ConfigurationRules = new List<ConfigurationRule>();
			Variables = new List<Variable>();
		}
	}
}