namespace Crm.Configurator.Model
{
	using System;
	using System.Collections.Generic;

	using Crm.Article.Model;
	using Crm.Library.BaseModel;
	using Crm.Library.BaseModel.Interfaces;

	public class ConfigurationRule : EntityBase<Guid>, ISoftDelete
	{
		public virtual Guid ConfigurationBaseId { get; set; }
		public virtual ICollection<Article> VariableValues { get; set; }
		public virtual ICollection<Article> AffectedVariableValues { get; set; }
		public virtual string Validation { get; set; }

		public ConfigurationRule()
		{
			VariableValues = new List<Article>();
			AffectedVariableValues = new List<Article>();
		}
	}
}
