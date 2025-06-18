namespace Sms.Einsatzplanung.Connector.Model;

using Crm.Article.Model;
using Crm.Library.BaseModel;

public class ArticleExtension : EntityExtension<Article>
{
	public virtual bool ExportToScheduler { get; set; }
}
