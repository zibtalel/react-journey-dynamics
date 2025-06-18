namespace Crm.PerDiem.Services
{
	using Crm.Library.Helper;
	using Crm.Library.Model;
	using Crm.Library.Services;
	using Crm.Library.Services.Interfaces;
	using Crm.PerDiem.Model.Extensions;

	public class WorkingHoursPerDayPluginUserSaveExtension : IPluginUserSaveExtension
	{
		private readonly IUserService userService;
		private readonly IAppSettingsProvider appSettingsProvider;
		public WorkingHoursPerDayPluginUserSaveExtension(IUserService userService, IAppSettingsProvider appSettingsProvider)
		{
			this.userService = userService;
			this.appSettingsProvider = appSettingsProvider;
		}

		public virtual void Save(User user)
		{
			var userExtension = user.GetExtension<UserExtension>();
			if (userExtension.WorkingHoursPerDay == 0)
			{
				userExtension.WorkingHoursPerDay = appSettingsProvider.GetValue(PerDiemPlugin.Settings.TimeEntry.DefaultWorkingHoursPerDay);
				userService.SaveUser(user);
			}
		}
	}
}
