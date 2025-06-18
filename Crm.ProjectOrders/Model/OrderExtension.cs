namespace Crm.ProjectOrders.Model
{
	using System;

	using Crm.Library.BaseModel;
	using Crm.Library.BaseModel.Attributes;
	using Crm.Library.Rest;
	using Crm.Order.Model;
	using Crm.Project.Model;

	using Newtonsoft.Json;

	public class OrderExtension : EntityExtension<Order>
	{
		[Database(Column = "ProjectKey")]
		public virtual Guid? ProjectId { get; set; }
		
		[Database(ManyToOne = true)]
		[JsonIgnore]
		[Obsolete("load the project using the ProjectId where needed")]
		public virtual Project Project { get; set; }

		[Database(Ignore = true)]
		[Obsolete("load the project using the ProjectId where needed")]
		[NotReceived]
		public virtual string ProjectLegacyName
		{
			get { return Project != null ? Project.LegacyName : null; }
		}
	}
}