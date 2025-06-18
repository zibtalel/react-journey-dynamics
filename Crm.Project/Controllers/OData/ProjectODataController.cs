namespace Crm.Project.Controllers.OData
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Crm.Library.Api;
	using Crm.Library.Api.Attributes;
	using Crm.Library.Api.Controller;
	using Crm.Library.Api.Extensions;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Project.Model;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.OData.Formatter;

	[ControllerName("CrmProject_Project")]
	public class ProjectODataController : ODataControllerEx, IEntityApiController
	{
		private readonly IRepositoryWithTypedId<Project, Guid> projectRepository;
		public Type EntityType => typeof(Project);
		public ProjectODataController(IRepositoryWithTypedId<Project, Guid> projectRepository)
		{
			this.projectRepository = projectRepository;
		}
	
		[HttpPost]
		public virtual IActionResult ValuePerCategoryAndYear(ODataActionParameters parameters)
		{
			if (ModelState.ErrorCount > 0)
			{
				return ValidationProblem(ModelState);
			}
			var projectIds = parameters.GetValue<IEnumerable<Guid>>("ProjectIds");
			var chartData = projectRepository.GetAll()
				.Where(x => projectIds.Contains(x.Id))
				.Where(x => x.DueDate.HasValue)
				.Where(x => x.Value.HasValue)
				.GroupBy(k => new { k.CategoryKey, k.DueDate.Value.Year }, e => e.Value.Value, (key, elements) => new { key.CategoryKey, key.Year, ValueSum = elements.Sum() })
				.AsEnumerable()
				.Select(x => new ProjectValueChartData { d = x.CategoryKey, x = x.Year, y = x.ValueSum });
			return Ok(chartData);
		}

		[HttpGet]
		public virtual IActionResult CountOfProjectsByStatus(Guid ProductFamilyId, Guid ParentId)
		{
			var countProjectsGroupByStatus = projectRepository.GetAll()
				.Where(x => x.MasterProductFamilyKey == ProductFamilyId)
				.Where(x => x.ParentId == ParentId)
				.GroupBy(t => t.StatusKey)
				.AsEnumerable()
				.Select(g => new ProjectValuesData
				{
					TotalCount = g.Select(x => x).Count(),
					Status = g.Key,
				});

			return Ok(countProjectsGroupByStatus);
		}

		[HttpGet]
		public virtual IActionResult CurrencySumByStatusAndCurrencyKey(Guid ProductFamilyId, Guid ParentId)
		{
			var currencySums = projectRepository.GetAll()
				.Where(x => x.MasterProductFamilyKey == ProductFamilyId)
				.Where(x => x.ParentId == ParentId)
				.GroupBy(x => new { x.StatusKey, x.CurrencyKey }, e => e.Value, (key, element) => new { Status = key.StatusKey, Currency = key.CurrencyKey, Sum = element.Sum() })
				.AsEnumerable()
				.Select(x => new ValueSumByCurrency { Currency = x.Currency, CurrencySum = x.Sum, Status = x.Status })
				.Where(x => x.CurrencySum != null);

			return Ok(currencySums);
		}
		[HttpGet]
		public virtual IActionResult CurrencySumByStatus(Guid ParentId)
		{
			var currencySums = projectRepository.GetAll()
				.Where(x => x.ParentId == ParentId)
				.GroupBy(x => new { x.StatusKey, x.CurrencyKey }, e => e.Value, (key, element) => new { Status = key.StatusKey, Currency = key.CurrencyKey, Sum = element.Sum() })
				.AsEnumerable()
				.Select(x => new ValueSumByCurrency { Currency = x.Currency, CurrencySum = x.Sum, Status = x.Status })
				.Where(x => x.CurrencySum != null);

			return Ok(currencySums);
		}
		
		[HttpGet]
		public virtual IActionResult AllProjectsCurrencySum()
		{
			var currencySums = projectRepository.GetAll()
				.GroupBy(x => new { x.StatusKey, x.CurrencyKey }, e => e.Value, (key, element) => new { Status = key.StatusKey, Currency = key.CurrencyKey, Sum = element.Sum() })
				.AsEnumerable()
				.Select(x => new ValueSumByCurrency { Currency = x.Currency, CurrencySum = x.Sum, Status = x.Status })
				.Where(x => x.CurrencySum != null);

			return Ok(currencySums);
		}
	}
}
