﻿namespace Crm.Service.Rest.Model.Mappings
{
	using AutoMapper;

	using Crm.Library.AutoMapper;
	using Crm.Rest.Model;
	using Crm.Service.Model;

	public class ReplenishmentOrderMap : IAutoMap
	{
		public virtual void CreateMap(IProfileExpression mapper)
		{
			mapper.CreateMap<ReplenishmentOrder, DocumentGeneratorEntry>()
				.IncludeBase<object, DocumentGeneratorEntry>()
				.ForMember(x => x.ErrorMessage, m => m.MapFrom(x => x.SendingError))
				;
		}
	}
}
