namespace Crm.Project.Controllers.OData
{
	using Crm.Library.Api;
	using Crm.Library.Api.Attributes;
	using Crm.Library.Api.Controller;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Project.Model;
	using Microsoft.AspNetCore.Mvc;

	using System;
	using System.Linq;

	public class PotentialTotalCount
	{
		public string Status { get; set; }
		public int TotalCount { get; set; }
	}

	[ControllerName("CrmProject_Potential")]
	public class PotentialODataController : ODataControllerEx, IEntityApiController
	{
		private readonly IRepositoryWithTypedId<Potential, Guid> potentialRepository;
		public Type EntityType => typeof(Potential);

		public PotentialODataController(IRepositoryWithTypedId<Potential, Guid> potentialRepository) 
		{
			this.potentialRepository = potentialRepository;
		}

		[HttpGet]
		public virtual IActionResult CountOfPotentialsByStatus(Guid ProductFamilyId, Guid ParentId)
		{
			var potentialsCountGroupByStatus = potentialRepository.GetAll()
				.Where(x => x.MasterProductFamilyKey == ProductFamilyId)
				.Where(x => x.ParentId == ParentId)
				.GroupBy(k => new { k.StatusKey }, e => e, (key, elements) => new { StatusKey = key.StatusKey, Count = elements.Count() })
				.AsEnumerable()
				.Select(x => new PotentialTotalCount { Status = x.StatusKey, TotalCount = x.Count });

			return Ok(potentialsCountGroupByStatus);
		}
		
	}
}
