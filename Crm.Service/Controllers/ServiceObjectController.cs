﻿using Microsoft.AspNetCore.Mvc;

namespace Crm.Service.Controllers
{
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization;
	using Crm.Service.Model;
	using Microsoft.AspNetCore.Authorization;
	using PermissionGroup = ServicePlugin.PermissionGroup;

	[Authorize]
	public class ServiceObjectController : Controller
	{
		[RequiredPermission(PermissionName.Edit, Group = PermissionGroup.ServiceObject)]
		public virtual ActionResult AddInstallation()
		{
			return PartialView();
		}
		
		[RequiredPermission(PermissionName.Create, Group = PermissionGroup.ServiceObject)]
		public virtual ActionResult CreateTemplate()
		{
			return PartialView();
		}

		[RenderAction("ServiceObjectDetailsTopMenu")]
		public virtual ActionResult EditVisibilityTopMenu()
		{
			return PartialView();
		}

		[RenderAction("ServiceObjectDetailsMaterialTab", Priority = 100)]
		public virtual ActionResult DetailsTab()
		{
			return PartialView();
		}

		[RenderAction("ServiceObjectDetailsMaterialTabHeader", Priority = 100)]
		public virtual ActionResult DetailsTabHeader()
		{
			return PartialView();
		}

		[RenderAction("ServiceObjectDetailsTopMenu")]
		[RequiredPermission(MainPlugin.PermissionName.CreateAddress, Group = PermissionGroup.ServiceObject)]
		public virtual ActionResult TopMenuAddAddress()
		{
			return PartialView();
		}

		[RenderAction("ServiceObjectDetailsMaterialTab", Priority = 90)]
		[RequiredPermission(ServicePlugin.PermissionName.InstallationsTab, Group = PermissionGroup.ServiceObject)]
		public virtual ActionResult InstallationsTab()
		{
			return PartialView();
		}

		[RenderAction("ServiceObjectDetailsMaterialTabHeader", Priority = 90)]
		[RequiredPermission(ServicePlugin.PermissionName.InstallationsTab, Group = PermissionGroup.ServiceObject)]
		public virtual ActionResult InstallationsTabHeader()
		{
			return PartialView();
		}

		[RenderAction("InstallationItemTemplateActions")]
		[RequiredPermission(PermissionName.Edit, Group = PermissionGroup.ServiceObject)]
		public virtual ActionResult InstallationItemTemplateActionRemoveInstallation()
		{
			return PartialView();
		}

		[RenderAction("ServiceObjectDetailsMaterialTab", Priority = 80)]
		[RequiredPermission(MainPlugin.PermissionName.NotesTab, Group = PermissionGroup.ServiceObject)]
		public virtual ActionResult NotesTab()
		{
			return PartialView("MaterialNotesTab");
		}

		[RenderAction("ServiceObjectDetailsMaterialTabHeader", Priority = 80)]
		[RequiredPermission(MainPlugin.PermissionName.NotesTab, Group = PermissionGroup.ServiceObject)]
		public virtual ActionResult NotesTabHeader()
		{
			return PartialView("MaterialNotesTabHeader");
		}

		[RenderAction("ServiceObjectDetailsMaterialTab", Priority = 70)]
		[RequiredPermission(MainPlugin.PermissionName.StaffTab, Group = PermissionGroup.ServiceObject)]
		public virtual ActionResult PersonsTab()
		{
			return PartialView();
		}

		[RenderAction("ServiceObjectDetailsMaterialTabHeader", Priority = 70)]
		[RequiredPermission(MainPlugin.PermissionName.StaffTab, Group = PermissionGroup.ServiceObject)]
		public virtual ActionResult PersonsTabHeader()
		{
			return PartialView();
		}
		[RequiredPermission(PermissionName.Read, Group = PermissionGroup.ServiceObject)]
		public virtual ActionResult DetailsTemplate()
		{
			return PartialView();
		}

		[RenderAction("MaterialServiceObjectSidebarExtensions", Priority = 50)]
		public virtual ActionResult DropboxBlock()
		{
			return PartialView("ContactDetailsDropboxBlock", typeof(ServiceObject));
		}
	}
}
