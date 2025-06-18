namespace Crm.ErpExtension
{
	using System;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Menu;

	public class ErpExtensionMenuRegistrar : IMenuRegistrar<MaterialMainMenu>
	{
		public virtual void Initialize(MenuProvider<MaterialMainMenu> menuProvider)
		{
			menuProvider.Register(null, "ErpDocuments", iconClass: "zmdi zmdi-collection-text", priority: Int32.MaxValue - 90);
			menuProvider.Register("ErpDocuments", "Quotations", priority: 500, url: "~/Crm.ErpExtension/QuoteList/IndexTemplate");
			menuProvider.AddPermission("ErpDocuments", "Quotations", ErpPlugin.PermissionGroup.QuoteList, PermissionName.View);
			menuProvider.Register("ErpDocuments", "SalesOrders", priority: 510, url: "~/Crm.ErpExtension/SalesOrderList/IndexTemplate");
			menuProvider.AddPermission("ErpDocuments", "SalesOrders", ErpPlugin.PermissionGroup.SalesOrderList, PermissionName.View);
			menuProvider.Register("ErpDocuments", "Invoices", priority: 520, url: "~/Crm.ErpExtension/InvoiceList/IndexTemplate");
			menuProvider.AddPermission("ErpDocuments", "Invoices", ErpPlugin.PermissionGroup.InvoiceList, PermissionName.View);
			menuProvider.Register("ErpDocuments", "DeliveryNotes", priority: 530, url: "~/Crm.ErpExtension/DeliveryNoteList/IndexTemplate");
			menuProvider.AddPermission("ErpDocuments", "DeliveryNotes", ErpPlugin.PermissionGroup.DeliveryNoteList, PermissionName.View);
			menuProvider.Register("ErpDocuments", "CreditNotes", priority: 540, url: "~/Crm.ErpExtension/CreditNoteList/IndexTemplate");
			menuProvider.AddPermission("ErpDocuments", "CreditNotes", ErpPlugin.PermissionGroup.CreditNoteList, PermissionName.View);
			menuProvider.Register("ErpDocuments", "MasterContracts", priority: 550, url: "~/Crm.ErpExtension/MasterContractList/IndexTemplate");
			menuProvider.AddPermission("ErpDocuments", "MasterContracts", ErpPlugin.PermissionGroup.MasterContractList, PermissionName.View);
		}
	}
}
