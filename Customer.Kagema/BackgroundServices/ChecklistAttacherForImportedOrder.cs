
using System;
using System.Collections.Generic;
using System.Linq;
using Crm.DynamicForms.Model;
using Crm.Library.Data.Domain.DataInterfaces;
using Crm.Library.Extensions;
using Crm.Library.Helper;
using Crm.Service.Model;
using Sms.Checklists.Model;

namespace Customer.Kagema.BackgroundServices
{
    public class ChecklistAttacherForImportedOrder : BackgroundServiceBase
    {
        private readonly IRepositoryWithTypedId<ServiceOrderHead, Guid> serviceOrderHeadRepository;
        private readonly IRepositoryWithTypedId<Installation, Guid> installationRepository;
        private readonly IRepositoryWithTypedId<ServiceOrderChecklist, Guid> serviceOrderChecklistRepository;
        private readonly IRepositoryWithTypedId<ChecklistInstallationTypeRelationship, Guid> checklistInstallationTypeRelationshipRepository;
        private readonly IRepositoryWithTypedId<DynamicForm, Guid> dynamicFormRepository;
        private readonly Func<ServiceOrderChecklist> serviceOrderChecklistFactory;
        private readonly IAppSettingsProvider appSettingsProvider;
        private DateTime lastDynamicFormCheck = DateTime.MinValue;

        public ChecklistAttacherForImportedOrder(
            IRepositoryWithTypedId<ServiceOrderHead, Guid> serviceOrderHeadRepository,
            IRepositoryWithTypedId<Installation, Guid> installationRepository,
            IRepositoryWithTypedId<ServiceOrderChecklist, Guid> serviceOrderChecklistRepository,
            IRepositoryWithTypedId<ChecklistInstallationTypeRelationship, Guid> checklistInstallationTypeRelationshipRepository,
            IRepositoryWithTypedId<DynamicForm, Guid> dynamicFormRepository,
            Func<ServiceOrderChecklist> serviceOrderChecklistFactory,
            IAppSettingsProvider appSettingsProvider)
        {
            this.serviceOrderHeadRepository = serviceOrderHeadRepository;
            this.installationRepository = installationRepository;
            this.serviceOrderChecklistRepository = serviceOrderChecklistRepository;
            this.checklistInstallationTypeRelationshipRepository = checklistInstallationTypeRelationshipRepository;
            this.dynamicFormRepository = dynamicFormRepository;
            this.serviceOrderChecklistFactory = serviceOrderChecklistFactory;
            this.appSettingsProvider = appSettingsProvider;
        }

        protected override void DoWork()
        {
            var orders = serviceOrderHeadRepository.GetAll().Where(x => x.ImportedFrom.IsNotNullOrEmpty()).ToList();
            foreach (var order in orders)
            {
                AttachChecklistsToOrder(order);
            }
            RefreshExistingChecklists();
        }

        private void RefreshExistingChecklists()
        {
            var refreshEnabled = appSettingsProvider.GetValue("RefreshChecklistsOnFormUpdate", "true").ToLower() == "true";
            if (!refreshEnabled) return;

            var currentTime = DateTime.UtcNow;
            var recentlyUpdatedForms = dynamicFormRepository.GetAll()
                .Where(x => x.ModifiedDate > lastDynamicFormCheck && x.Status == "Released")
                .ToList();

            if (recentlyUpdatedForms.Any())
            {
                foreach (var form in recentlyUpdatedForms)
                {
                    var affectedChecklists = serviceOrderChecklistRepository.GetAll()
                        .Where(x => x.DynamicFormKey == form.Id)
                        .ToList();

                    foreach (var checklist in affectedChecklists)
                    {
                        var relationships = checklistInstallationTypeRelationshipRepository.GetAll()
                            .Where(x => x.DynamicFormKey == form.Id)
                            .ToList();

                        var matchingRelationship = relationships.FirstOrDefault(r => 
                            IsChecklistMatchingRelationship(checklist, r));

                        if (matchingRelationship != null)
                        {
                            checklist.RequiredForServiceOrderCompletion = matchingRelationship.RequiredForServiceOrderCompletion;
                            checklist.SendToCustomer = matchingRelationship.SendToCustomer;
                            serviceOrderChecklistRepository.SaveOrUpdate(checklist);
                        }
                    }
                }
            }
            lastDynamicFormCheck = currentTime;
        }

        private bool IsChecklistMatchingRelationship(ServiceOrderChecklist checklist, ChecklistInstallationTypeRelationship relationship)
        {
            if (checklist.ServiceOrder?.InstallationId.HasValue == true)
            {
                var installation = installationRepository.Get(checklist.ServiceOrder.InstallationId.Value);
                return installation?.InstallationTypeKey == relationship.InstallationTypeKey &&
                       checklist.ServiceOrder.TypeKey == relationship.ServiceOrderTypeKey;
            }
            if (checklist.ServiceOrderTime?.InstallationId.HasValue == true)
            {
                var installation = installationRepository.Get(checklist.ServiceOrderTime.InstallationId.Value);
                return installation?.InstallationTypeKey == relationship.InstallationTypeKey &&
                       checklist.ServiceOrder?.TypeKey == relationship.ServiceOrderTypeKey;
            }
            return false;
        }

        private void AttachChecklistsToOrder(ServiceOrderHead serviceOrderHead)
        {
            if (serviceOrderHead.InstallationId.HasValue)
            {
                var installation = installationRepository.Get(serviceOrderHead.InstallationId.Value);
                if (installation == null) return;

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
        }

        protected virtual List<ChecklistInstallationTypeRelationship> GetChecklistsToAttach(string installationType, string serviceOrderType)
        {
            var relationships = checklistInstallationTypeRelationshipRepository.GetAll().Where(x => x.InstallationTypeKey == installationType && x.ServiceOrderTypeKey == serviceOrderType).ToList();
            return relationships;
        }
    }
}
