﻿namespace Crm.Controllers
{
	using System.Collections.Generic;
	using System.Linq;
	using Crm.Library.Extensions;
	using Crm.Library.Globalization.Resource;
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization;
	using Crm.Library.Services.Interfaces;
	using Crm.Library.Signalr;
	using Crm.Library.Validation;

	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class UserAdminController : Controller
	{
		private readonly IUserService userService;
		private readonly IUsergroupService usergroupService;
		private readonly ISignalrProfiler signalrProfiler;
		private readonly IAuthenticationService authenticationService;
		private readonly IResourceManager resourceManager;
		private readonly IRuleValidationService ruleValidationService;

		[RequiredPermission(MainPlugin.PermissionName.SignalR, Group = PermissionGroup.UserAdmin)]
		public virtual ActionResult EditLogLevel() => PartialView();

		[RequiredPermission(MainPlugin.PermissionName.RefreshUserCache, Group = PermissionGroup.UserAdmin)]
		public virtual ActionResult RefreshUserCache()
		{
			userService.PurgeCache();
			usergroupService.PurgeCache();
			return RedirectToAction("Index", "UserList");
		}

		[RequiredPermission(MainPlugin.PermissionName.SignalR, Group = PermissionGroup.UserAdmin)]
		public virtual ActionResult JavaScriptCommand() => PartialView();

		[RequiredPermission(MainPlugin.PermissionName.SignalR, Group = PermissionGroup.UserAdmin)]
		public virtual ActionResult LogEntries() => PartialView();

		[HttpPost]
		[RequiredPermission(MainPlugin.PermissionName.ResetUserPassword, Group = PermissionGroup.UserAdmin)]
		public virtual ActionResult ResetPassword(string username, string password, string confirmPassword)
		{
			var user = userService.GetUser(username);
			var ruleViolations = new List<RuleViolation>();
			var messageList = new Dictionary<string, string>();

			if (user == null)
			{
				ruleViolations.Add(new RuleViolation("UserDoesNotExist"));
				messageList.Add("ERROR", resourceManager.GetTranslation("NoUserFound"));
				return Json(messageList.ToArray());
			}

			if (!password.Equals(confirmPassword))
			{
				ruleViolations.Add(new RuleViolation("PasswordsDoNotMatch"));
			}

			user.Password = password;
			ruleViolations.AddRange(ruleValidationService.GetRuleViolations(user));

			if (ruleViolations.Any())
			{
				foreach (var ruleViolation in ruleViolations)
				{
					if (ruleViolation.ErrorMessageKey.IsNullOrEmpty())
					{
						messageList.Add($"ERROR_{ruleViolation.RuleClass}", resourceManager.GetTranslation($"RuleViolation.{ruleViolation.RuleClass}").WithArgs(resourceManager.GetTranslation(ruleViolation.PropertyName)));
					}
					else
					{
						messageList.Add($"ERROR_{ruleViolation.RuleClass}", resourceManager.GetTranslation($"RuleViolation.{ruleViolation.ErrorMessageKey}"));
					}
				}
			}
			else
			{
				authenticationService.ResetPassword(user, password);
				messageList.Add("MESSAGE", resourceManager.GetTranslation("ResetPasswordSuccess"));
			}

			return Json(messageList.ToArray());
		}

		[RequiredPermission(MainPlugin.PermissionName.RequestLocalStorage, Group = PermissionGroup.UserAdmin)]
		public virtual ActionResult RequestLocalStorage(string username)
		{
			var user = userService.GetUser(username);

			if (user == null)
			{
				return Json(new { errorMessage = resourceManager.GetTranslation("UserDoesNotExist") });
			}

			signalrProfiler.RequestLocalStorage(user);

			return new EmptyResult();
		}
		[RequiredPermission(MainPlugin.PermissionName.RequestLocalDatabase, Group = PermissionGroup.UserAdmin)]
		public virtual ActionResult RequestLocalDatabase(string username)
		{
			var user = userService.GetUser(username);

			if (user == null)
			{
				return Json(new { errorMessage = resourceManager.GetTranslation("UserDoesNotExist") });
			}

			signalrProfiler.RequestLocalDatabase(user);

			return new EmptyResult();
		}
		[RequiredPermission(MainPlugin.PermissionName.SetLogLevel, Group = PermissionGroup.UserAdmin)]
		public virtual ActionResult SetLogLevel(string username, JavaScriptLogLevel logLevel)
		{
			var user = userService.GetUser(username);

			if (user == null)
			{
				return Json(new { errorMessage = resourceManager.GetTranslation("UserDoesNotExist") });
			}

			signalrProfiler.StartProfiling(user, logLevel);

			return new EmptyResult();
		}

		[RequiredPermission(MainPlugin.PermissionName.SendJavaScriptCommand, Group = PermissionGroup.UserAdmin)]
		public virtual ActionResult SendJavaScriptCommand(string username, string javascript)
		{
			var user = userService.GetUser(username);

			if (user == null)
			{
				return Json(new { errorMessage = resourceManager.GetTranslation("UserDoesNotExist") });
			}

			signalrProfiler.SendJavaScriptCommand(user, javascript);
			return Json(new { message = resourceManager.GetTranslation("JavaScriptCommandSent") });
		}

		[HttpGet]
		[RequiredPermission(MainPlugin.PermissionName.AssignSkill, Group = PermissionGroup.UserAdmin)]
		public virtual ActionResult GetSkillAssignedUser(string skillKey)
		{
			var allUsers = userService.GetUsers().Where(x => x.SkillKeys.Contains(skillKey)).Select(x => x.Id).ToList();

			return Json(allUsers);
		}

		// Constructor
		public UserAdminController(IUserService userService, IUsergroupService usergroupService, ISignalrProfiler signalrProfiler, IAuthenticationService authenticationService, IResourceManager resourceManager, IRuleValidationService ruleValidationService)
		{
			this.userService = userService;
			this.usergroupService = usergroupService;
			this.signalrProfiler = signalrProfiler;
			this.authenticationService = authenticationService;
			this.resourceManager = resourceManager;
			this.ruleValidationService = ruleValidationService;
		}
	}
}
