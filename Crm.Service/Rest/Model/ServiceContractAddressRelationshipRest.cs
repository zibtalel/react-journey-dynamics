﻿namespace Crm.Service.Rest.Model
{
	using System;

	using Crm.Library.Api.Attributes;
	using Crm.Library.Rest;
	using Crm.Rest.Model;
	using Crm.Service.Model.Relationships;

	[RestTypeFor(DomainType = typeof(ServiceContractAddressRelationship))]
	public class ServiceContractAddressRelationshipRest : RestEntityWithExtensionValues
	{
		[ExplicitExpand]
		[NotReceived]
		public AddressRest Child { get; set; }
		public Guid ChildId { get; set; }
		public string Information { get; set; }
		[ExplicitExpand]
		[NotReceived]
		public ServiceContractRest Parent { get; set; }
		public Guid ParentId { get; set; }
		public string RelationshipTypeKey { get; set; }
	}
}
