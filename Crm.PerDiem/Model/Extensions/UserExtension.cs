namespace Crm.PerDiem.Model.Extensions
{
	using Crm.Library.BaseModel;
	using Crm.Library.Model;

	public class UserExtension : EntityExtension<User>
	{
		public int WorkingHoursPerDay { get; set; }
	}
}
