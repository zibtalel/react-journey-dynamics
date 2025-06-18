namespace Crm.ErpExtension.Model.Lookups
{
	using Crm.Library.Globalization.Lookup;

	public class ErpDocumentStatus : EntityLookup<string>, ILookupWithColor
	{
		[LookupProperty(Shared = true)]
		public virtual string Color { get; set; }
	}
	
	[Lookup("[LU].[ErpPaymentMethod]", "ErpPaymentMethodId")]
	public class ErpPaymentMethod : EntityLookup<string>
	{
	}
	
	[Lookup("[LU].[ErpDeliveryMethod]", "ErpDeliveryMethodId")]
	public class ErpDeliveryMethod : EntityLookup<string>
	{
	}
	
	[Lookup("[LU].[ErpDeliveryProhibitedReason]", "ErpDeliveryProhibitedReasonId")]
	public class ErpDeliveryProhibitedReason : EntityLookup<string>
	{
	}
	
	[Lookup("[LU].[ErpPartialDeliveryProhibitedReason]", "ErpPartialDeliveryProhibitedReasonId")]
	public class ErpPartialDeliveryProhibitedReason : EntityLookup<string>
	{
	}
	
	[Lookup("[LU].[ErpTermsOfDelivery]", "ErpTermsOfDeliveryId")]
	public class ErpTermsOfDelivery : EntityLookup<string>
	{
	}
	
	[Lookup("[LU].[ErpPaymentTerms]", "ErpPaymentTermsId")]
	public class ErpPaymentTerms : EntityLookup<string>
	{
	}
}