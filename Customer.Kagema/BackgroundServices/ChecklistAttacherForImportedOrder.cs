namespace Customer.Kagema.BackgroundServices
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Crm.DynamicForms.Model;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Helper;
	using Crm.Library.Logging;
	using Crm.Lmobile.Model;
	using Crm.Lmobile.Model.Lookups;

	public class ChecklistAttacherForImportedOrder : BackgroundServiceBase
	{
		private readonly IRepository<ServiceOrderHead> serviceOrderRepository;
		private readonly IRepository<DynamicFormReference> dynamicFormReferenceRepository;
		private readonly IRepository<Checklist> checklistRepository;
		private readonly IRepository<DynamicForm> dynamicFormRepository;
		private readonly IAppSettingsProvider appSettingsProvider;
		private readonly ILogger logger;

		public ChecklistAttacherForImportedOrder(
			IRepository<ServiceOrderHead> serviceOrderRepository,
			IRepository<DynamicFormReference> dynamicFormReferenceRepository,
			IRepository<Checklist> checklistRepository,
			IRepository<DynamicForm> dynamicFormRepository,
			IAppSettingsProvider appSettingsProvider,
			ILogger logger)
		{
			this.serviceOrderRepository = serviceOrderRepository;
			this.dynamicFormReferenceRepository = dynamicFormReferenceRepository;
			this.checklistRepository = checklistRepository;
			this.dynamicFormRepository = dynamicFormRepository;
			this.appSettingsProvider = appSettingsProvider;
			this.logger = logger;
		}

		protected override void DoWork()
		{
			var newOrders = serviceOrderRepository.GetAll()
				.Where(x => x.StatusKey == ServiceOrderStatus.DispatchedKey && x.ChecklistsAttached != true)
				.ToList();

			foreach (var order in newOrders)
			{
				AttachChecklistsToOrder(order.ServiceOrderNumber, order);

				order.ChecklistsAttached = true;
				serviceOrderRepository.SaveOrUpdate(order);
			}

			if (appSettingsProvider.GetAppSetting("RefreshChecklistsOnFormUpdate", "true") == "true")
			{
				RefreshExistingChecklists();
			}
		}

		private void RefreshExistingChecklists()
		{
			try
			{
				var serviceOrders = serviceOrderRepository.GetAll()
					.Where(x => x.StatusKey != ServiceOrderStatus.FinishedKey && x.StatusKey != ServiceOrderStatus.CancelledKey)
					.ToList();

				foreach (var serviceOrder in serviceOrders)
				{
					AttachChecklistsToOrder(serviceOrder.ServiceOrderNumber, serviceOrder);
				}

				logger.Info($"Refreshed checklists for {serviceOrders.Count} service orders");
			}
			catch (Exception ex)
			{
				logger.Error($"Error refreshing existing checklists: {ex.Message}", ex);
			}
		}

		private void AttachChecklistsToOrder(string orderNumber, ServiceOrderHead serviceOrder = null)
		{
			var checklists = checklistRepository.GetAll()
				.Where(x => x.AppliesTo == ChecklistAppliesTo.ServiceOrder && x.IsActive)
				.ToList();

			if (!checklists.Any())
			{
				logger.Info($"No active checklists found for service orders.");
				return;
			}

			if (serviceOrder == null)
			{
				serviceOrder = serviceOrderRepository.GetAll()
					.FirstOrDefault(x => x.ServiceOrderNumber == orderNumber);

				if (serviceOrder == null)
				{
					logger.Warn($"Service order with number {orderNumber} not found.");
					return;
				}
			}

			if (serviceOrder != null)
			{
				var existingChecklists = dynamicFormReferenceRepository.GetAll()
					.Where(x => x.ReferenceKey == serviceOrder.ServiceOrderId.ToString())
					.ToList();

				if (existingChecklists.Any())
				{
					foreach (var existing in existingChecklists)
					{
						dynamicFormReferenceRepository.Delete(existing);
					}
				}
			}

			foreach (var checklist in checklists)
			{
				var dynamicForm = dynamicFormRepository.GetAll()
					.FirstOrDefault(x => x.Id == checklist.DynamicFormId);

				if (dynamicForm == null)
				{
					logger.Warn($"Dynamic form with id {checklist.DynamicFormId} not found for checklist {checklist.Id}.");
					continue;
				}

				var dynamicFormReference = new DynamicFormReference
				{
					DynamicFormId = dynamicForm.Id,
					ReferenceKey = serviceOrder.ServiceOrderId.ToString(),
					ReferenceType = "ServiceOrder",
					Name = dynamicForm.Name,
					Description = dynamicForm.Description,
					CreateDate = DateTime.UtcNow,
					CreateUser = "System",
					ModifyDate = DateTime.UtcNow,
					ModifyUser = "System"
				};

				dynamicFormReferenceRepository.SaveOrUpdate(dynamicFormReference);

				logger.Info($"Attached checklist {checklist.Id} to service order {orderNumber}.");
			}
		}
	}
}
