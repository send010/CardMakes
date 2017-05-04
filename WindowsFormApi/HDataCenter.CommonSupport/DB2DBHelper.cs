using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Configuration;
using log4net;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using IBM.Data.DB2;


namespace HDataCenter.CommonSupport
{
 /// <summary>
 /// Copyright (C) 2004-2008 LiTianPing 
 /// 数据访问基础类(基于Oracle)
 /// 可以用户可以修改满足自己项目的需要。
 /// </summary>
//public abstract class DbHelperOra
    public  class DB2DBHelper:DBHelper
    {
        protected override DbConnection CreateConn()
        {
            return new DB2Connection(connectionString);
        }
        protected override DbParameter CreateParam(string content)
        {
            return new DB2Parameter("@content", content);
        }
        protected override DbParameter CreateParam(byte[] fs)
        {
            return new DB2Parameter("@fs", fs);
        }

        protected override DbCommand CreateCmd(string SQLString = null, DbConnection connection = null)
        {
            if (string.IsNullOrEmpty(SQLString))
                return new DB2Command();

            DB2Connection conn = connection as DB2Connection;
            return new DB2Command(SQLString, conn);
        }

        protected override DbDataAdapter CreateAdapter(string SQLString = null, DbConnection connection = null)
        {
            if (string.IsNullOrEmpty(SQLString))
                return new DB2DataAdapter();

            DB2Connection conn = connection as DB2Connection;
            return new DB2DataAdapter(SQLString, conn);
        }

        protected override DbDataAdapter CreateAdapter(DbCommand cmd)
        {
            return new DB2DataAdapter(cmd as DB2Command);
        }

    }
}
