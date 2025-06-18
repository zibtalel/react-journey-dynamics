﻿namespace Crm.Rest.Model
{
	using System;

	using Crm.Library.Api.Attributes;
	using Crm.Library.BaseModel;
	using Crm.Library.Rest;
	using Crm.Library.Rest.Interfaces;
	using Crm.Library.Rest.Model;

	[RestrictedType(TypeRestriction.NoDelete)]
	[RestTypeFor(DomainType = typeof(Library.Model.User))]
	public class UserRest : RestEntity, IRestEntityWithExtensionValues
	{
		public string Id { get; set; }
		public SerializableDictionary<string, object> ExtensionValues { get; set; }
		[RestrictedField(Restriction.NotSortable)] public string Avatar { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string OpenIdIdentifier { get; set; }
		public string AdName { get; set; }
		public string Email { get; set; }
		[RestrictedField(Restriction.NotFilterable)] public string GeneralToken { get; set; }
		public string DefaultLanguageKey { get; set; }
		public string DefaultLocale { get; set; }
		public DateTime? LastLoginDate { get; set; }
		public string TimeZoneName { get; set; }
		public string Remark { get; set; }
		public string PersonnelId { get; set; }
		public DateTime? DischargeDate { get; set; }
		public bool Discharged { get; set; }
		[RestrictedField(Restriction.NotFilterable)] public string DropboxToken { get; set; }
		public string MasterRecordNo { get; set; }
		public string IdentificationNo { get; set; }
		public float? Latitude { get; set; }
		public float? Longitude { get; set; }
		public DateTime? LastStatusUpdate { get; set; }
		public string[] SkillKeys { get; set; }
		[NotReceived, ExplicitExpand] public PermissionSchemaRoleRest[] Roles { get; set; }
		[NotReceived, ExplicitExpand] public UsergroupRest[] UsergroupObjects { get; set; }
		[NotReceived, ExplicitExpand] public string[] Usergroups { get; set; }
		[NotReceived, ExplicitExpand] public Guid[] UsergroupIds { get; set; }
		[NotReceived, ExplicitExpand] public StationRest[] Stations { get; set; }
		[NotReceived, RestrictedField] public Guid[] StationIds { get; set; }
	}
}
