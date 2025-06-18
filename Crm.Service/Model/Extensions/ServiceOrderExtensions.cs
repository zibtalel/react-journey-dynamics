﻿namespace Crm.Service.Model.Extensions
{
	using System;
	using System.Linq;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Service.Model.Helpers;

	public static class ServiceOrderExtensions
	{
		public static bool HasCustomServiceLocationAddress(this ServiceOrderHead serviceOrderHead)
		{
			return !String.IsNullOrWhiteSpace(serviceOrderHead.Name1)
			       || !String.IsNullOrWhiteSpace(serviceOrderHead.Name2)
			       || !String.IsNullOrWhiteSpace(serviceOrderHead.Name3)
			       || !String.IsNullOrWhiteSpace(serviceOrderHead.Street)
			       || !String.IsNullOrWhiteSpace(serviceOrderHead.City)
			       || !String.IsNullOrWhiteSpace(serviceOrderHead.ZipCode)
			       || !String.IsNullOrWhiteSpace(serviceOrderHead.CountryKey);
		}

		public static void DynamicUpdateOrderStatus(this IServiceOrderStatusEvaluator serviceOrderStatusEvaluator, ServiceOrderHead serviceOrderHead)
		{
			var serviceOrderStatus = serviceOrderStatusEvaluator.Evaluate(serviceOrderHead);
			if (!serviceOrderStatus.Equals(serviceOrderHead.StatusKey, StringComparison.InvariantCultureIgnoreCase))
			{
				serviceOrderHead.StatusKey = serviceOrderStatus;
			}
		}

		public static ServiceOrderHead Get(this IRepositoryWithTypedId<ServiceOrderHead, Guid> serviceOrderRepository, string orderNo)
		{
			return serviceOrderRepository.GetAll().SingleOrDefault(x => x.OrderNo == orderNo);
		}
	}
}