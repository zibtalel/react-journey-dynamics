﻿using Microsoft.AspNetCore.Mvc;

namespace Crm.Service.Controllers
{
	using Crm.Library.Model;
	using Microsoft.AspNetCore.Authorization;

	[Authorize]
	public class ServiceContractAddressRelationshipController : Controller
	{
		[RequiredPermission(MainPlugin.PermissionName.EditAddressRelationship, Group = ServicePlugin.PermissionGroup.ServiceContract)]
		public virtual ActionResult EditTemplate()
		{
			return PartialView("../Relationship/EditTemplate");
		}
	}
}
