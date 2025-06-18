namespace Crm.Configurator.Model
{
	using Crm.Article.Model.Lookups;
	using Crm.Library.BaseModel;
	using Crm.Library.Globalization.Lookup;

	public class ArticleGroup01Extension : EntityExtension<ArticleGroup01>
	{
		[LookupProperty(Shared = true)]
		public virtual bool ShowInConfigurator { get; set; }
	}
}