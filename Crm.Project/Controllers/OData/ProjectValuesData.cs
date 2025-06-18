namespace Crm.Project.Controllers.OData
{
	public class ProjectValuesData
	{
		public int? TotalCount { get; set; }
		public string Status { get; set; }
	}	
	public class ValueSumByCurrency
	{
		public string Currency { get; set; }
		public decimal? CurrencySum { get; set; }
		public string Status { get; set; }
	}
}
