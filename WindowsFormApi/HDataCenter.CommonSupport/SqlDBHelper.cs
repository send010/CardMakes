using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using log4net;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace HDataCenter.CommonSupport
{
    public class SqlDBHelper : DBHelper
    {
        protected override  DbConnection CreateConn()
        {
            return new SqlConnection(connectionString);
        }
        protected override DbParameter CreateParam(string content)
        {
            return new SqlParameter("@content", content);
        }
        protected override DbParameter CreateParam(byte[] fs)
        {
            return new SqlParameter("@fs", fs);
        }

        protected override DbCommand CreateCmd(string SQLString = null, DbConnection connection = null)
        {
            if (string.IsNullOrEmpty(SQLString))
                return new SqlCommand();

            SqlConnection conn = connection as SqlConnection;
            return new SqlCommand(SQLString, conn);
        }

        protected override DbDataAdapter CreateAdapter(string SQLString = null, DbConnection connection = null)
        {
            if (string.IsNullOrEmpty(SQLString))
                return new SqlDataAdapter();

            SqlConnection conn = connection as SqlConnection;
            return new SqlDataAdapter(SQLString, conn);
        }

        protected override DbDataAdapter CreateAdapter(DbCommand cmd)
        {
            return new SqlDataAdapter(cmd as SqlCommand);
        }

    }
}
