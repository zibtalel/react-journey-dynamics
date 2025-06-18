namespace Crm.InforExtension.Services.Interfaces
{
	using System.Collections.Generic;
	using System.Data.Common;

	public interface IInforAdapter
	{
		bool InsertRecords(List<DbParameter> crmParam, List<DbParameter> eventsParam);
		List<DbParameter> GetCrmParam(decimal id);
		List<DbParameter> GetEventsParam(decimal id, int trac);

		decimal NewId();
		decimal NewIk();
		decimal NewCid();
		LMobile.Data.Database Database { get; }
	}
}