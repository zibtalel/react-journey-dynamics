namespace Crm.InforExtension.Services
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.Common;
	using System.Text;
	using System.Threading;

	using Crm.InforExtension.Extensions;
	using Crm.InforExtension.Services.Interfaces;
	using Crm.Library.Extensions.IIdentity;

	using LMobile.Data.Oracle;

	using log4net;

	using Microsoft.AspNetCore.Http;

	public class Infor71Adapter : IInforAdapter
	{
		private readonly ILog logger;
		private readonly string connectionString;
		protected Lazy<OracleDatabase> database;
		private readonly IHttpContextAccessor httpContextAccessor;

		//Constructor
		public Infor71Adapter(string connectionString, ILog logger, IHttpContextAccessor httpContextAccessor)
		{
			this.connectionString = connectionString;
			this.logger = logger;
			this.httpContextAccessor = httpContextAccessor;
			database = new Lazy<OracleDatabase>(() => new OracleDatabase(ConnectionString));
		}
		public string ConnectionString
		{
			get { return connectionString; }
		}
		public LMobile.Data.Database Database
		{
			get { return database.Value; }
		}

		public bool InsertRecords(List<DbParameter> crmParam, List<DbParameter> eventsParam)
		{
			var crmSql = GenerateInsertStatement("USBookNewEventsCRM", crmParam);
			var eventsSql = GenerateInsertStatement("USBookNewEvents", eventsParam);

			DbTransaction transaction = null;
			try
			{
				transaction = database.Value.GetTransaction(IsolationLevel.ReadCommitted);
				var cmd = database.Value.CreateCommand();
				cmd.CommandText = eventsSql;
				cmd.Transaction = transaction;
				database.Value.ExecuteNonQuery(cmd, eventsParam.ToArray());

				cmd = database.Value.CreateCommand();
				cmd.CommandText = crmSql;
				cmd.Transaction = transaction;
				database.Value.ExecuteNonQuery(cmd, crmParam.ToArray());
				transaction.Commit();

				return true;
			}
			catch (Exception ex)
			{
				if (transaction != null)
				{
					transaction.Rollback();
				}

				const string message = "Couldn't write data to USBookNewEventsCRM and USBookNewEvents.";
				logger.Error(message, ex);
				throw new Exception(ex.Message + " " + message);
			}
			finally
			{
				if (transaction != null)
				{
					transaction.Dispose();
				}
			}
		}
		public List<DbParameter> GetCrmParam(decimal id)
		{
			return new List<DbParameter>
				{
					database.Value.CreateParameter("sysf", Encoding.ASCII.GetBytes("4711")),
					database.Value.CreateParameter("ik", NewIk()),
					database.Value.CreateParameter("vqlheader_createDate", DateTime.Now),
					database.Value.CreateParameter("vqlheader_modifyDate", DateTime.Now),
					database.Value.CreateParameter("vqlheader_createUser", "LMOBILE"),
					database.Value.CreateParameter("vqlheader_modifyUser", "LMOBILE"),
					database.Value.CreateParameter("vqlheader_cid", NewCid()),
					database.Value.CreateParameter("id", id),
				};
		}
		public List<DbParameter> GetEventsParam(decimal id, int trac)
		{
			return new List<DbParameter>
				{
					database.Value.CreateParameter("sysf", Encoding.ASCII.GetBytes("4711")),
					database.Value.CreateParameter("ik", NewIk()),
					database.Value.CreateParameter("createdate", DateTime.Now),
					database.Value.CreateParameter("modifydate", DateTime.Now),
					database.Value.CreateParameter("createuser", "LMOBILE"),
					database.Value.CreateParameter("modifyuser", "LMOBILE"),
					database.Value.CreateParameter("cid", NewCid()),
					database.Value.CreateParameter("id", id),
					database.Value.CreateParameter("status", 0),
					database.Value.CreateParameter("transactionid", 0),
					database.Value.CreateParameter("bookingduration", 0),
					database.Value.CreateParameter("waitingduration", 0),
					database.Value.CreateParameter("errorcode", 0),
					database.Value.CreateParameter("errortext", String.Empty),
					database.Value.CreateParameter("clientname", httpContextAccessor.HttpContext != null
					                                       	? httpContextAccessor.HttpContext.User.Identity.GetUserName().NormalizeForInfor(16)
					                                       	: Thread.CurrentPrincipal.Identity.GetUserName().NormalizeForInfor(16)),
					database.Value.CreateParameter("postingdate", DateTime.Now),
					database.Value.CreateParameter("retrytime", DateTime.Now),
					database.Value.CreateParameter("retrycounter", 0),
					database.Value.CreateParameter("refid", 0),
					database.Value.CreateParameter("trac", trac)
				};
		}

		private static string GenerateInsertStatement(string tableName, IEnumerable<DbParameter> parameters)
		{
			var builder = new StringBuilder();
			builder.AppendFormat("INSERT INTO {0} (", tableName);

			foreach (DbParameter t in parameters)
			{
				builder.AppendFormat("{0},", t.ParameterName.Substring(1));
			}
			builder = builder.Remove(builder.Length - 1, 1);
			builder.Append(") VALUES (");

			foreach (DbParameter t in parameters)
			{
				builder.AppendFormat("{0},", t.ParameterName);
			}
			builder = builder.Remove(builder.Length - 1, 1);
			builder.Append(")");

			return builder.ToString();
		}

		public decimal NewId()
		{
			const string idSql = "SELECT infor.LMOBILE_ID.NextVal AS MaxID FROM dual";
			decimal result = 0;
			try
			{
				result = (decimal)database.Value.ExecuteScalar(idSql);
			}
			catch
			{
				// most probably the sequence wasn't created yet, we create it with the max value and continue to
				const string maxSql = "SELECT MAX(id) FROM USBookNewEvents";
				var startDb = database.Value.ExecuteScalar(maxSql);
				var startDec = startDb == DBNull.Value ? 42000 : (decimal)startDb;
				startDec = startDec < 42000 ? 42000 : startDec;

				var seqSql = String.Format("CREATE SEQUENCE \"INFOR\".\"LMOBILE_ID\" INCREMENT BY 1 START WITH " +
										   "{0} MAXVALUE 1.0E27 MINVALUE {1} NOCYCLE " +
										   "NOCACHE ORDER", startDec, 42000);

				database.Value.ExecuteNonQuery(seqSql);
				return NewId();
			}
			return result;
		}
		public decimal NewIk()
		{
			const string sql = "SELECT infor.IK_SEQUENCE.NextVal AS MaxIK FROM dual";
			return (decimal)database.Value.ExecuteScalar(sql);
		}
		public decimal NewCid()
		{
			const string sql = "SELECT infor.CID_SEQUENCE.NextVal AS CID FROM dual";
			return (decimal)database.Value.ExecuteScalar(sql);
		}
	}
}
