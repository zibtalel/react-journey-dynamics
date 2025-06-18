namespace Crm.Configurator.Model
{
	using Crm.Article.Model.Relationships;
	using Crm.Library.BaseModel;

	public class ArticleRelationshipExtension : EntityExtension<ArticleRelationship>
	{
		public virtual bool? IsDefault { get; set; }
		public virtual bool? IsRequired { get; set; }
		public virtual decimal? PurchasePrice { get; set; }
		public virtual decimal? SalesPrice { get; set; }
	}
}