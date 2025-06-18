﻿namespace Crm.BusinessRules.RoleRules
{
	using Crm.Library.Validation;
	using Crm.Library.Validation.BaseRules;
	using Crm.Model.Extension;

	[Rule]
	public class DescriptionMaxLength : MaxLengthRule<PermissionSchemaRoleExtension>
	{
		public DescriptionMaxLength()
		{
			Init(r => r.Description, 255, "Description");
		}
	}
}
