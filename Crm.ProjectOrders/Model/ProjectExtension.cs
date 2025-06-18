namespace Crm.ProjectOrders.Model
{
	using System;

	using Crm.Library.BaseModel;
	using Crm.Library.BaseModel.Attributes;
	using Crm.Order.Model;
	using Crm.Project.Model;

	using Newtonsoft.Json;

	public class ProjectExtension : EntityExtension<Project>
	{
		[UI(Hidden = true)]
		public virtual Guid? PreferredOfferId { get; set; }
		
		[Database(ManyToOne = true)]
		[JsonIgnore]
		[ReadOnlyExtensionProperty]
		public virtual Offer PreferredOffer { get; set; }
	}
}