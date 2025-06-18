namespace Crm.Configurator.Model
{
	using System;

    using Crm.Library.BaseModel;
    using Crm.Library.BaseModel.Attributes;
    using Crm.Order.Model;

    using Newtonsoft.Json;

    public class OrderExtension : EntityExtension<Order>
	{
		public virtual Guid? ConfigurationBaseId { get; set; }

        [Database(ManyToOne = true)]
        [JsonIgnore]
        public virtual ConfigurationBase ConfigurationBase { get; set; }
    }
}