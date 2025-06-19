
namespace Customer.Kagema.BackgroundServices
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Helper;
	using Crm.Service.Model;
	using Main.BackgroundServices;
	using Sms.Checklists.Model;

	public class ChecklistAttacherForImportedOrder : BaseAgent
	{
		private readonly IRepositoryWithTypedId<ServiceOrderHead, Guid> serviceOrderHeadRepository;
		private readonly IRepositoryWithTypedId<Installation, Guid> installationRepository;
		private readonly IRepositoryWithTypedId<ServiceOrderChecklist, Guid> serviceOrderChecklistRepository;
		private readonly IRepositoryWithTypedId<ChecklistInstallationTypeRelationship, Guid> checklistInstallationTypeRelationshipRepository;
		private readonly Func<ServiceOrderChecklist> serviceOrderChecklistFactory;
		private readonly IAppSettingsProvider appSettingsProvider;

		public ChecklistAttacherForImportedOrder(
			IRepositoryWithTypedId<ServiceOrderHead, Guid> serviceOrderHeadRepository,
			IRepositoryWithTypedId<Installation, Guid> installationRepository,
			IRepositoryWithTypedId<ServiceOrderChecklist, Guid> serviceOrderChecklistRepository,
			IRepositoryWithTypedId<ChecklistInstallationTypeRelationship, Guid> checklistInstallationTypeRelationshipRepository,
			Func<ServiceOrderChecklist> serviceOrderChecklistFactory,
			IAppSettingsProvider appSettingsProvider)
		{
			this.serviceOrderHeadRepository = serviceOrderHeadRepository;
			this.installationRepository = installationRepository;
			this.serviceOrderChecklistRepository = serviceOrderChecklistRepository;
			this.checklistInstallationTypeRelationshipRepository = checklistInstallationTypeRelationshipRepository;
			this.serviceOrderChecklistFactory = serviceOrderChecklistFactory;
			this.appSettingsProvider = appSettingsProvider;
		}

		protected override void DoWork()
		{
			var importedOrders = serviceOrderHeadRepository.GetAll().Where(x => x.ExternalKey != null && x.InstallationId != null).Take(50).ToList();
			foreach (var order in importedOrders)
			{
				AttachChecklistsToOrder(order, order.InstallationId.Value);
			}

			var refreshEnabled = appSettingsProvider.GetValue("Customer.Kagema/RefreshChecklistsOnFormUpdate", true);
			if (refreshEnabled)
			{
				RefreshExistingChecklists();
			}
		}

		protected virtual void AttachChecklistsToOrder(ServiceOrderHead serviceOrderHead, Guid installationId)
		{
			var installation = installationRepository.Get(installationId);
			if (installation == null)
				return;

			var checklistRelationships = GetChecklistsToAttach(installation.InstallationTypeKey, serviceOrderHead.TypeKey);
			foreach (ChecklistInstallationTypeRelationship checklistRelationship in checklistRelationships)
			{
				var existingChecklist = serviceOrderChecklistRepository.GetAll()
					.FirstOrDefault(x => x.ReferenceKey == serviceOrderHead.Id && x.DynamicFormKey == checklistRelationship.DynamicFormKey);

				if (existingChecklist == null)
				{
					var serviceOrderChecklist = serviceOrderChecklistFactory();
					serviceOrderChecklist.RequiredForServiceOrderCompletion = checklistRelationship.RequiredForServiceOrderCompletion;
					serviceOrderChecklist.SendToCustomer = checklistRelationship.SendToCustomer;
					serviceOrderChecklist.ServiceOrder = serviceOrderHead;
					serviceOrderChecklist.DynamicFormKey = checklistRelationship.DynamicFormKey;
					serviceOrderChecklist.DynamicForm = checklistRelationship.DynamicForm;
					serviceOrderChecklist.ReferenceKey = serviceOrderHead.Id;
					serviceOrderChecklistRepository.SaveOrUpdate(serviceOrderChecklist);
				}
			}
		}

		protected virtual void RefreshExistingChecklists()
		{
			var existingChecklists = serviceOrderChecklistRepository.GetAll().Take(100).ToList();
			foreach (var checklist in existingChecklists)
			{
				var relationship = checklistInstallationTypeRelationshipRepository.GetAll()
					.FirstOrDefault(x => x.DynamicFormKey == checklist.DynamicFormKey);
				
				if (relationship != null)
				{
					checklist.RequiredForServiceOrderCompletion = relationship.RequiredForServiceOrderCompletion;
					checklist.SendToCustomer = relationship.SendToCustomer;
					serviceOrderChecklistRepository.SaveOrUpdate(checklist);
				}
			}
		}

		protected virtual List<ChecklistInstallationTypeRelationship> GetChecklistsToAttach(string installationType, string serviceOrderType)
		{
			var relationships = checklistInstallationTypeRelationshipRepository.GetAll().Where(x => x.InstallationTypeKey == installationType && x.ServiceOrderTypeKey == serviceOrderType).ToList();
			return relationships;
		}
	}
}
