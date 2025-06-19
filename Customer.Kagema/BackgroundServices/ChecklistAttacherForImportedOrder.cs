
using System;
using System.Collections.Generic;
using System.Linq;
using Crm.DynamicForms.Model;
using Crm.DynamicForms.Model.Lookups;
using Crm.Library.Data.Domain.DataInterfaces;
using Crm.Library.Extensions;
using Crm.Library.Helper;
using Crm.Library.Logging;
using Crm.Service.Model;
using Quartz;
using Sms.Checklists.Model;

namespace Customer.Kagema.BackgroundServices
{
    public class ChecklistAttacherForImportedOrder : IJob
    {
        private readonly IRepositoryWithTypedId<ServiceOrderHead, Guid> serviceOrderHeadRepository;
        private readonly IRepositoryWithTypedId<ServiceOrderChecklist, Guid> serviceOrderChecklistRepository;
        private readonly IRepositoryWithTypedId<ChecklistInstallationTypeRelationship, Guid> checklistInstallationTypeRelationshipRepository;
        private readonly IRepositoryWithTypedId<DynamicForm, Guid> dynamicFormRepository;
        private readonly IRepositoryWithTypedId<Installation, Guid> installationRepository;
        private readonly Func<ServiceOrderChecklist> serviceOrderChecklistFactory;
        private readonly IAppSettingsProvider appSettingsProvider;
        private readonly ILog logger;

        public ChecklistAttacherForImportedOrder(
            IRepositoryWithTypedId<ServiceOrderHead, Guid> serviceOrderHeadRepository,
            IRepositoryWithTypedId<ServiceOrderChecklist, Guid> serviceOrderChecklistRepository,
            IRepositoryWithTypedId<ChecklistInstallationTypeRelationship, Guid> checklistInstallationTypeRelationshipRepository,
            IRepositoryWithTypedId<DynamicForm, Guid> dynamicFormRepository,
            IRepositoryWithTypedId<Installation, Guid> installationRepository,
            Func<ServiceOrderChecklist> serviceOrderChecklistFactory,
            IAppSettingsProvider appSettingsProvider,
            ILog logger)
        {
            this.serviceOrderHeadRepository = serviceOrderHeadRepository;
            this.serviceOrderChecklistRepository = serviceOrderChecklistRepository;
            this.checklistInstallationTypeRelationshipRepository = checklistInstallationTypeRelationshipRepository;
            this.dynamicFormRepository = dynamicFormRepository;
            this.installationRepository = installationRepository;
            this.serviceOrderChecklistFactory = serviceOrderChecklistFactory;
            this.appSettingsProvider = appSettingsProvider;
            this.logger = logger;
        }

        public virtual void Execute(IJobExecutionContext context)
        {
            try
            {
                // Attach checklists to newly imported orders (existing functionality)
                AttachChecklistsToImportedOrders();
                
                // Refresh existing checklists when dynamic forms are updated (new functionality)
                if (appSettingsProvider.GetValue("Customer.Kagema/RefreshChecklistsOnFormUpdate", true))
                {
                    RefreshChecklistsWithUpdatedForms();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error in ChecklistAttacherForImportedOrder", ex);
                throw;
            }
        }

        private void AttachChecklistsToImportedOrders()
        {
            // Get service orders that need checklist attachment (imported orders without checklists)
            var ordersNeedingChecklists = serviceOrderHeadRepository.GetAll()
                .Where(x => x.InstallationId.HasValue && 
                           !x.ServiceOrderTemplateId.HasValue &&
                           !serviceOrderChecklistRepository.GetAll().Any(c => c.ServiceOrder.Id == x.Id))
                .ToList();

            foreach (var order in ordersNeedingChecklists)
            {
                try
                {
                    AttachChecklistsToOrder(order);
                }
                catch (Exception ex)
                {
                    logger.Error($"Error attaching checklists to order {order.Id}", ex);
                }
            }
        }

        private void RefreshChecklistsWithUpdatedForms()
        {
            // Get all released dynamic forms that might have been updated
            var releasedForms = dynamicFormRepository.GetAll()
                .Where(x => x.Languages.Any(l => l.StatusKey == DynamicFormStatus.ReleasedKey))
                .ToList();

            foreach (var form in releasedForms)
            {
                try
                {
                    RefreshChecklistsForForm(form);
                }
                catch (Exception ex)
                {
                    logger.Error($"Error refreshing checklists for form {form.Id}", ex);
                }
            }
        }

        private void RefreshChecklistsForForm(DynamicForm form)
        {
            // Get all service order checklists using this form
            var checklistsToRefresh = serviceOrderChecklistRepository.GetAll()
                .Where(x => x.DynamicFormKey == form.Id)
                .ToList();

            foreach (var checklist in checklistsToRefresh)
            {
                try
                {
                    // Check if the form relationship still exists and is valid
                    var serviceOrder = checklist.ServiceOrder;
                    if (serviceOrder?.InstallationId == null) continue;

                    var installation = installationRepository.Get(serviceOrder.InstallationId.Value);
                    if (installation == null) continue;

                    var relationship = checklistInstallationTypeRelationshipRepository.GetAll()
                        .FirstOrDefault(x => x.InstallationTypeKey == installation.InstallationTypeKey &&
                                           x.ServiceOrderTypeKey == serviceOrder.TypeKey &&
                                           x.DynamicFormKey == form.Id);

                    if (relationship != null)
                    {
                        // Update checklist properties from the relationship
                        checklist.RequiredForServiceOrderCompletion = relationship.RequiredForServiceOrderCompletion;
                        checklist.SendToCustomer = relationship.SendToCustomer;
                        checklist.DynamicForm = relationship.DynamicForm;
                        
                        serviceOrderChecklistRepository.SaveOrUpdate(checklist);
                        
                        logger.Info($"Refreshed checklist {checklist.Id} for form {form.Id}");
                    }
                    else
                    {
                        // Relationship no longer exists, remove the checklist
                        serviceOrderChecklistRepository.Delete(checklist);
                        logger.Info($"Removed obsolete checklist {checklist.Id} for form {form.Id}");
                    }
                }
                catch (Exception ex)
                {
                    logger.Error($"Error refreshing individual checklist {checklist.Id}", ex);
                }
            }
        }

        private void AttachChecklistsToOrder(ServiceOrderHead serviceOrderHead)
        {
            if (!serviceOrderHead.InstallationId.HasValue) return;

            var installation = installationRepository.Get(serviceOrderHead.InstallationId.Value);
            if (installation == null) return;

            var checklistRelationships = GetChecklistsToAttach(installation.InstallationTypeKey, serviceOrderHead.TypeKey);
            
            foreach (var checklistRelationship in checklistRelationships)
            {
                var serviceOrderChecklist = serviceOrderChecklistFactory();
                serviceOrderChecklist.RequiredForServiceOrderCompletion = checklistRelationship.RequiredForServiceOrderCompletion;
                serviceOrderChecklist.SendToCustomer = checklistRelationship.SendToCustomer;
                serviceOrderChecklist.ServiceOrder = serviceOrderHead;
                serviceOrderChecklist.DynamicFormKey = checklistRelationship.DynamicFormKey;
                serviceOrderChecklist.DynamicForm = checklistRelationship.DynamicForm;
                serviceOrderChecklist.ReferenceKey = serviceOrderHead.Id;
                
                serviceOrderChecklistRepository.SaveOrUpdate(serviceOrderChecklist);
                
                logger.Info($"Attached checklist {checklistRelationship.DynamicForm?.GetTitle()} to order {serviceOrderHead.LegacyName}");
            }
        }

        private List<ChecklistInstallationTypeRelationship> GetChecklistsToAttach(string installationType, string serviceOrderType)
        {
            return checklistInstallationTypeRelationshipRepository.GetAll()
                .Where(x => x.InstallationTypeKey == installationType && x.ServiceOrderTypeKey == serviceOrderType)
                .ToList();
        }
    }
}
