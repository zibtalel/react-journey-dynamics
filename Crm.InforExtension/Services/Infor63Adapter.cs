namespace Crm.InforExtension.Services
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.Common;
	using System.Text;

	using Crm.InforExtension.Services.Interfaces;

	using LMobile.Data.Oracle;

	using log4net;

	public class Infor63Adapter : IInforAdapter
  {
    private readonly ILog logger;
    private readonly string connectionString;
		private readonly OracleDatabase database;

    //Constructor
		public Infor63Adapter(string connectionString, ILog logger)
    {
      this.connectionString = connectionString;
      this.logger = logger;
      database = new OracleDatabase(ConnectionString);
    }
    public string ConnectionString
    {
      get { return connectionString; }
    }
		public LMobile.Data.Database Database
		{
			get { return database; }
		}

		public bool InsertRecords(List<DbParameter> crmParam, List<DbParameter> eventsParam)
    {
      var crmSql = GenerateInsertStatement("USBookNewEventsCRM", crmParam);
      var eventsSql = GenerateInsertStatement("USBookNewEvents", eventsParam);

			logger.Debug(crmSql);
			logger.Debug(eventsSql);

      DbTransaction transaction = null;
      try
      {
        transaction = database.GetTransaction(IsolationLevel.ReadCommitted);
        var cmd = database.CreateCommand();
        cmd.CommandText = eventsSql;
        cmd.Transaction = transaction;
        database.ExecuteNonQuery(cmd, eventsParam.ToArray());

        cmd = database.CreateCommand();
        cmd.CommandText = crmSql;
        cmd.Transaction = transaction;
        database.ExecuteNonQuery(cmd, crmParam.ToArray());
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
					database.CreateParameter("sysf", Encoding.ASCII.GetBytes("4711")),
			    database.CreateParameter("ik", NewIk()),
			    database.CreateParameter("eader_createDate", DateTime.Now),
			    database.CreateParameter("eader_modifyDate", DateTime.Now),
			    database.CreateParameter("eader_createUser", "LMOBILE"),
			    database.CreateParameter("eader_modifyUser", "LMOBILE"),
			    database.CreateParameter("vqlheader_cid", NewCid()),
			    database.CreateParameter("id", id),
				};
    }
		public List<DbParameter> GetEventsParam(decimal id, int trac)
    {
      return new List<DbParameter>
				{
						database.CreateParameter("sysf", Encoding.ASCII.GetBytes("4711")),
			      database.CreateParameter("ik", NewIk()),
			      database.CreateParameter("createdate", DateTime.Now),
			      database.CreateParameter("modifydate", DateTime.Now),
			      database.CreateParameter("createuser", "LMOBILE"),
			      database.CreateParameter("modifyuser", "LMOBILE"),
			      database.CreateParameter("cid", NewCid()),
			      database.CreateParameter("id", id),
			      database.CreateParameter("status", 0),
			      database.CreateParameter("queueid", 0),
			      database.CreateParameter("transactionid", 0),
			      database.CreateParameter("bookingduration", 0),
			      database.CreateParameter("waitingduration", 0),
			      database.CreateParameter("errorcode", 0),
			      database.CreateParameter("errortext", String.Empty),
			      database.CreateParameter("hostclient", "L"),
			      database.CreateParameter("clientname", "Test"),
			      database.CreateParameter("clientsequence", 0),
			      database.CreateParameter("buchdatum", DateTime.Now),
			      database.CreateParameter("retrytime", DateTime.Now),
			      database.CreateParameter("retrycounter", 0),
			      database.CreateParameter("bezugid", 0),
			      database.CreateParameter("trac", trac)
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
        result = (decimal)database.ExecuteScalar(idSql);
      }
      catch
      {
        // most probably the sequence wasn't created yet, we create it with the max value and continue to
        const string maxSql = "SELECT MAX(id) FROM USBookNewEvents";
        var startDb = database.ExecuteScalar(maxSql);
        var startDec = startDb == DBNull.Value ? 42000 : (decimal)startDb;
        startDec = startDec < 42000 ? 42000 : startDec;

        var seqSql = String.Format("CREATE SEQUENCE \"INFOR\".\"LMOBILE_ID\" INCREMENT BY 1 START WITH " +
                                   "{0} MAXVALUE 1.0E27 MINVALUE {1} NOCYCLE " +
                                   "NOCACHE ORDER", startDec, 42000);

        database.ExecuteNonQuery(seqSql);
        return NewId();
      }
      return result;
    }
		public decimal NewIk()
    {
      const string sql = "SELECT infor.IK_SEQUENCE.NextVal AS MaxIK FROM dual";
      return (decimal)database.ExecuteScalar(sql);
    }
		public decimal NewCid()
    {
      const string sql = "SELECT infor.CID_SEQUENCE.NextVal AS CID FROM dual";
      return (decimal)database.ExecuteScalar(sql);
    }
  }
}
