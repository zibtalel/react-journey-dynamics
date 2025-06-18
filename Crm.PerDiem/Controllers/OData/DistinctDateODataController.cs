namespace Crm.PerDiem.Controllers.OData
{
	using System;
	using System.Linq;
	using System.Linq.Expressions;

	using AutoMapper.Extensions.ExpressionMapping;

	using Crm.Library.Api;
	using Crm.Library.Api.Controller;
	using Crm.Library.Api.Mapping;
	using Crm.Library.BaseModel.Interfaces;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Rest;

	using Microsoft.AspNetCore.OData.Query;
	using Microsoft.AspNetCore.Mvc;

	public abstract class DistinctDateODataController<T, TRest> : ODataControllerEx, IEntityApiController
		where T : class, IEntityWithId
		where TRest : class, IRestEntity
	{
		protected readonly IRepository<T> repository;
		protected readonly IODataMapper mapper;
		public Type EntityType => typeof(T);
		protected DistinctDateODataController(IRepository<T> repository, IODataMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}
		protected virtual IActionResult GetDistinctDates(ODataQueryOptions<TRest> options, Expression<Func<TRest, DateTime>> expression)
		{
			var settings = new ODataQuerySettings { EnableConstantParameterization = false, HandleNullPropagation = HandleNullPropagationOption.False };
			var query = (IQueryable<TRest>)repository.GetAll().UseAsDataSource(mapper).For<TRest>();
			query = (IQueryable<TRest>)options.ApplyTo(query, settings, AllowedQueryOptions.All & ~(AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Skip | AllowedQueryOptions.Top));
			var result = query.Select(expression).Distinct().AsEnumerable();
			return Ok(result);
		}
	}
}
