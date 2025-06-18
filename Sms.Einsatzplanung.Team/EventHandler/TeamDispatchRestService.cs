using System;
using System.Collections.Generic;
using System.Linq;

namespace Sms.Einsatzplanung.Team.EventHandler
{
	using Crm.Library.AutoFac;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Model;
	using Crm.Library.Services.Interfaces;
	using Crm.Service.Model;
	using Crm.Service.Services;
	using Crm.Service.Services.Interfaces;

	using Microsoft.AspNetCore.Http;

	using Sms.Einsatzplanung.Team.Model;

	public class TeamDispatchRestService : DispatchRestService, IReplaceRegistration<IDispatchRestService>, IDispatchRestService
	{
		protected const string TeamMembersKey = "TeamMembers";
		protected const string NonTeamMembersKey = "NonTeamMembers";
		private readonly IUserService userService;
		private readonly IRepositoryWithTypedId<TeamDispatchUser, Guid> teamDispatchUserRepository;
		private readonly Func<TeamDispatchUser> teamDispatchUserFactory;
		private readonly IHttpContextAccessor httpContextAccessor;
		protected virtual IEnumerable<User> TeamMembers => GetUsers(TeamMembersKey);
		protected virtual IEnumerable<User> NonTeamMembers => GetUsers(NonTeamMembersKey);
		protected virtual bool IsTeamDispatch => httpContextAccessor.HttpContext.Request.Headers[TeamMembersKey].Count > 0 && httpContextAccessor.HttpContext.Request.Headers[NonTeamMembersKey].Count > 0;
		public TeamDispatchRestService(IRepositoryWithTypedId<ServiceOrderDispatch, Guid> dispatchRepository, IUserService userService, IRepositoryWithTypedId<TeamDispatchUser, Guid> teamDispatchUserRepository, Func<TeamDispatchUser> teamDispatchUserFactory, IHttpContextAccessor httpContextAccessor)
			: base(dispatchRepository)
		{
			this.userService = userService;
			this.teamDispatchUserRepository = teamDispatchUserRepository;
			this.teamDispatchUserFactory = teamDispatchUserFactory;
			this.httpContextAccessor = httpContextAccessor;
		}
		protected virtual IEnumerable<User> GetUsers(string key)
		{
			var ids = httpContextAccessor.HttpContext.Request.Headers[key].ToArray() ?? new string[0];
			return ids.Select(x => userService.GetUser(x)).Where(x => x != null);
		}
		protected virtual TeamDispatchUser GetTeamDispatchUser(Guid dispatchId, string username, bool isNonTeamMember)
		{
			var user = teamDispatchUserFactory();
			user.DispatchId = dispatchId;
			user.Username = username;
			user.IsNonTeamMember = isNonTeamMember;
			return user;
		}
		protected virtual void Update(Guid dispatchId, Dictionary<string, TeamDispatchUser> dispatchUsers, User teamMember, bool isNonTeamMember)
		{
			TeamDispatchUser dispatchUser;
			if (dispatchUsers.TryGetValue(teamMember.Id, out dispatchUser) == false)
			{
				dispatchUser = GetTeamDispatchUser(dispatchId, teamMember.Id, isNonTeamMember);
			}
			dispatchUser.IsNonTeamMember = isNonTeamMember;
			teamDispatchUserRepository.SaveOrUpdate(dispatchUser);
		}
		public override void CreateDispatch(ServiceOrderDispatch dispatch)
		{
			base.CreateDispatch(dispatch);
			if (!IsTeamDispatch)
			{
				return;
			}
			var dispatchId = dispatch.Id;
			foreach (var teamMember in TeamMembers)
			{
				teamDispatchUserRepository.SaveOrUpdate(GetTeamDispatchUser(dispatchId, teamMember.Id, false));
			}
			foreach (var nonTeamMember in NonTeamMembers)
			{
				teamDispatchUserRepository.SaveOrUpdate(GetTeamDispatchUser(dispatchId, nonTeamMember.Id, true));
			}
		}
		public override void UpdateDispatch(ServiceOrderDispatch dispatch)
		{
			base.UpdateDispatch(dispatch);
			if (!IsTeamDispatch)
			{
				return;
			}
			var dispatchId = dispatch.Id;
			var dispatchUsers = teamDispatchUserRepository.GetAll().Where(x => x.DispatchId == dispatchId).ToDictionary(x => x.Username);
			var handledIds = new HashSet<string>();
			foreach (var teamMember in TeamMembers)
			{
				Update(dispatchId, dispatchUsers, teamMember, false);
				handledIds.Add(teamMember.Id);
			}
			foreach (var nonTeamMember in NonTeamMembers)
			{
				Update(dispatchId, dispatchUsers, nonTeamMember, true);
				handledIds.Add(nonTeamMember.Id);
			}
			foreach (var remove in dispatchUsers.Where(x => handledIds.Contains(x.Key) == false))
			{
				teamDispatchUserRepository.Delete(remove.Value);
			}
		}
		public override void DeleteDispatch(ServiceOrderDispatch dispatch)
		{
			if (IsTeamDispatch)
			{
				foreach (var remove in teamDispatchUserRepository.GetAll().Where(x => x.DispatchId == dispatch.Id).ToList())
				{
					teamDispatchUserRepository.Delete(remove);
				}
			}
			base.DeleteDispatch(dispatch);
		}
	}
}
