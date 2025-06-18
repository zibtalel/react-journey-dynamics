namespace Crm.Configurator.Model
{
	using Crm.Article.Model;

	public class Variable : Article
	{
		public virtual ConfigurationBase ConfigurationBase { get; set; }
	}
}