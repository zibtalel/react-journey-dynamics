namespace Sms.Einsatzplanung.Team.Model
{
	using Crm.Library.BaseModel;
	using Crm.Library.Model;

	public class UserGroupExtension : EntityExtension<Usergroup>
	{
		public string MainResourceId { get; set; }
	}
}