namespace Crm.Project.Services
{
	using Crm.Library.Api.Controller;
	using Crm.Library.AutoFac;
	using Crm.Library.BaseModel.Interfaces;
	using Crm.Library.Extensions;
	using Crm.Project.Model;
	using Microsoft.AspNetCore.OData.Query;
	using System.Linq;
	using System.Reflection;

	public class ODataQueryOrderByProjectStatus : IODataQueryFunction, IDependency
	{
		protected static MethodInfo FilterByVisibilityInfo = typeof(ODataQueryOrderByProjectStatus).GetMethod(nameof(OrderByProjectStatus), BindingFlags.Instance | BindingFlags.NonPublic);

		protected virtual IQueryable<Project> OrderByProjectStatus(IQueryable<Project> query, string keys)
		{
			var keyList = keys.Split(',').ToList();
			foreach (var key in keyList)
			{
				query = query.OrderByDescending(x => x.StatusKey == key);
			}
			return query;
		}
		public virtual IQueryable<T> Apply<T, TRest>(ODataQueryOptions<TRest> options, IQueryable<T> query)
			where T : class, IEntityWithId
			where TRest : class
		{
			if (typeof(Project).IsAssignableFrom(typeof(T)) == false)
			{
				return query;
			}
			const string parameterName = "orderByProjectStatus";
			var parameters = options.Request.Query;
			if (parameters.Keys.Contains(parameterName))
			{
				var keys = options.Request.GetQueryParameter("orderByProjectStatus");
				return (IQueryable<T>)FilterByVisibilityInfo.Invoke(this, new object[] { query, keys });
			}
			return query;
		}
	}
}
