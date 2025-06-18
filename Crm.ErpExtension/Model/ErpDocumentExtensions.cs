namespace Crm.ErpExtension.Model
{
	using System.Linq;

	using Crm.Library.BaseModel;
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.Interfaces;
	using Crm.Library.Model.Authorization.PermissionIntegration;

	public static class ErpDocumentExtensions
	{
		public static IQueryable<ErpDocumentHead<TPosition>> FilterByVisibility<TPosition>(this IQueryable<ErpDocumentHead<TPosition>> erpDocuments, IAuthorizationManager authorizationManager, User user)
		where TPosition : ErpDocumentPosition
		{
			// Using any causes an exception due to NHibernate internals.
			// Should be fixed by NH 3.3.3GA but wasn't :(
			// http://nihbernate.jira.com/browse/NH-3241
			// ReSharper disable once UseMethodAny.2
			if (authorizationManager.IsAuthorizedForAction(user, PermissionGroup.Visibility, PermissionName.SkipCheck) || erpDocuments.Count() == 0)
			{
				return erpDocuments;
			}

			var userGroupIds = user.Usergroups.Select(ug => ug.Id).ToList();
			erpDocuments = erpDocuments.Where(c => (c.Contact != null && c.Contact.Visibility == Visibility.Everybody)
			                                       || (c.Contact != null && c.Contact.CreateUser == user.Id && c.Contact.Visibility == Visibility.OnlyMe)
			                                       || (c.Contact != null && c.Contact.Visibility == Visibility.Users && c.Contact.VisibleToUserIds.Any(u => u == user.Id))
			                                       || (c.Contact != null && c.Contact.Visibility == Visibility.UserGroups && c.Contact.VisibleToUsergroupIds.Any(u => userGroupIds.Contains(u))));

			return erpDocuments;
		}
		public static IQueryable<ErpDocumentPosition<THead>> FilterByVisibility<THead>(this IQueryable<ErpDocumentPosition<THead>> erpDocumentPositions, IAuthorizationManager authorizationManager, User user)
		where THead : ErpDocumentHead
		{
			// Using any causes an exception due to NHibernate internals.
			// Should be fixed by NH 3.3.3GA but wasn't :(
			// http://nihbernate.jira.com/browse/NH-3241
			// ReSharper disable once UseMethodAny.2
			if (authorizationManager.IsAuthorizedForAction(user, PermissionGroup.Visibility, PermissionName.SkipCheck) || erpDocumentPositions.Count() == 0)
			{
				return erpDocumentPositions;
			}

			var userGroupIds = user.Usergroups.Select(ug => ug.Id).ToList();
			erpDocumentPositions = erpDocumentPositions.Where(c => (c.Parent.Contact != null && c.Parent.Contact.Visibility == Visibility.Everybody)
			                                                       || (c.Parent.Contact != null && c.Parent.Contact.CreateUser == user.Id && c.Parent.Contact.Visibility == Visibility.OnlyMe)
			                                                       || (c.Parent.Contact != null && c.Parent.Contact.Visibility == Visibility.Users && c.Parent.Contact.VisibleToUserIds.Any(u => u == user.Id))
			                                                       || (c.Parent.Contact != null && c.Parent.Contact.Visibility == Visibility.UserGroups && c.Parent.Contact.VisibleToUsergroupIds.Any(u => userGroupIds.Contains(u))));

			return erpDocumentPositions;
		}
	}
}
