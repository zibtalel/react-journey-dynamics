namespace Crm.Configurator.Model
{
	using System;

	using Crm.Library.BaseModel;
	using Crm.Library.BaseModel.Attributes;
	using Crm.Order.Model;

	using Newtonsoft.Json;

    public class OrderItemExtension : EntityExtension<OrderItem>
	{
		public virtual Guid? VariableId { get; set; }

        [Database(ManyToOne = true)]
        [JsonIgnore]
        public virtual Variable Variable { get; set; }
	}
}