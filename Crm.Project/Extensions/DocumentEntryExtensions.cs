namespace Crm.Project.Extensions
{
	using Crm.Library.BaseModel;
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization.Interfaces;
	using Crm.Project.Model;
	using System.Linq;

	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.PermissionIntegration;

	public static class DocumentEntryExtensions
	{
		public static IQueryable<DocumentEntry> FilterByContactVisibility(this IQueryable<DocumentEntry> documentEntry, IAuthorizationManager authorizationManager, User user)
		{
			if (authorizationManager.IsAuthorizedForAction(user, PermissionGroup.Visibility, PermissionName.SkipCheck))
			{
				return documentEntry;
			}
			var userGroupIds = user.Usergroups.Select(ug => ug.Id).ToList();
			documentEntry = documentEntry.Where(x => x.Contact == null
					 || x.Contact.Visibility == Visibility.Everybody
					 || x.Contact.Visibility == Visibility.OnlyMe && x.Contact.CreateUser == user.Id
					 || x.Contact.Visibility == Visibility.UserGroups && x.Contact.VisibleToUsergroupIds.Any(ug => userGroupIds.Contains(ug))
					 || x.Contact.Visibility == Visibility.Users && x.Contact.VisibleToUserIds.Any(u => u == user.Id));
			return documentEntry;
		}
	}
}
