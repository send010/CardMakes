using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Configuration;
using log4net;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;


namespace HDataCenter.CommonSupport
{
 /// <summary>
 /// Copyright (C) 2004-2008 LiTianPing 
 /// 数据访问基础类(基于Oracle)
 /// 可以用户可以修改满足自己项目的需要。
 /// </summary>
//public abstract class DbHelperOra
    public  class OracleDBHelper:DBHelper
    {
        protected override DbConnection CreateConn()
        {
            return new OracleConnection(connectionString);
        }
        protected override DbParameter CreateParam(string content)
        {
            return new OracleParameter("@content", content);
        }
        protected override DbParameter CreateParam(byte[] fs)
        {
            return new OracleParameter("@fs", fs);
        }

        protected override DbCommand CreateCmd(string SQLString = null, DbConnection connection = null)
        {
            if (string.IsNullOrEmpty(SQLString))
                return new OracleCommand();

            OracleConnection conn = connection as OracleConnection;
            return new OracleCommand(SQLString, conn);
        }

        protected override DbDataAdapter CreateAdapter(string SQLString = null, DbConnection connection = null)
        {
            if (string.IsNullOrEmpty(SQLString))
                return new OracleDataAdapter();

            OracleConnection conn = connection as OracleConnection;
            return new OracleDataAdapter(SQLString, conn);
        }

        protected override DbDataAdapter CreateAdapter(DbCommand cmd)
        {
            return new OracleDataAdapter(cmd as OracleCommand);
        }

    }
}
