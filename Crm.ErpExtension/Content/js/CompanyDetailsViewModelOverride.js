(function() {
    var baseViewModel = window.Main.ViewModels.CompanyDetailsViewModel;
    window.Main.ViewModels.CompanyDetailsViewModel = function() {
        var viewModel = this;
        baseViewModel.apply(this, arguments);
        if (window.Crm.ErpExtension.Settings.PaymentMethodIsLookup && window.AuthorizationManager.hasPermission("WebAPI::ErpPaymentMethod")){
            viewModel.lookups.ErpPaymentMethod = { $tableName: "CrmErpExtension_ErpPaymentMethod" }
        }
        if (window.Crm.ErpExtension.Settings.DeliveryMethodIsLookup && window.AuthorizationManager.hasPermission("WebAPI::ErpDeliveryMethod")){
            viewModel.lookups.ErpDeliveryMethod = { $tableName: "CrmErpExtension_ErpDeliveryMethod" }
        }
        if (window.Crm.ErpExtension.Settings.ErpDeliveryProhibitedReasonIsLookup && window.AuthorizationManager.hasPermission("WebAPI::ErpDeliveryProhibitedReason")){
            viewModel.lookups.ErpDeliveryProhibitedReason = { $tableName: "CrmErpExtension_ErpDeliveryProhibitedReason" }
        }
        if (window.Crm.ErpExtension.Settings.ErpPartialDeliveryProhibitedReasonIsLookup && window.AuthorizationManager.hasPermission("WebAPI::ErpPartialDeliveryProhibitedReason")){
            viewModel.lookups.ErpPartialDeliveryProhibitedReason = { $tableName: "CrmErpExtension_ErpPartialDeliveryProhibitedReason" }
        }
        if (window.Crm.ErpExtension.Settings.PaymentTermsIsLookup && window.AuthorizationManager.hasPermission("WebAPI::ErpPaymentTerms")){
            viewModel.lookups.ErpPaymentTerms = { $tableName: "CrmErpExtension_ErpPaymentTerms" }
        }
        if (window.Crm.ErpExtension.Settings.TermsOfDeliveryIsLookup && window.AuthorizationManager.hasPermission("WebAPI::ErpTermsOfDelivery")){
            viewModel.lookups.ErpTermsOfDelivery = { $tableName: "CrmErpExtension_ErpTermsOfDelivery" }
        }
        viewModel.lookups.currencies = { $tableName: "Main_Currency" }
        
    };
    window.Main.ViewModels.CompanyDetailsViewModel.prototype = baseViewModel.prototype;
})();