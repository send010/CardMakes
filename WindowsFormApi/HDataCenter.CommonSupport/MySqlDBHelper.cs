using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Configuration;
using log4net;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace HDataCenter.CommonSupport
{
 /// <summary>
 /// Copyright (C) 2004-2008 LiTianPing 
 /// 数据访问基础类(基于Oracle)
 /// 可以用户可以修改满足自己项目的需要。
 /// </summary>
//public abstract class DbHelperOra
    public  class MySqlDBHelper:DBHelper
    {
        protected override DbConnection CreateConn()
        {
            return new MySqlConnection(connectionString);
        }
        protected override DbParameter CreateParam(string content)
        {
            return new MySqlParameter("@content", content);
        }
        protected override DbParameter CreateParam(byte[] fs)
        {
            return new MySqlParameter("@fs", fs);
        }

        protected override DbCommand CreateCmd(string SQLString = null, DbConnection connection = null)
        {
            if (string.IsNullOrEmpty(SQLString))
                return new MySqlCommand();

            MySqlConnection conn = connection as MySqlConnection;
            return new MySqlCommand(SQLString, conn);
        }

        protected override DbDataAdapter CreateAdapter(string SQLString = null, DbConnection connection = null)
        {
            if (string.IsNullOrEmpty(SQLString))
                return new MySqlDataAdapter();

            MySqlConnection conn = connection as MySqlConnection;
            return new MySqlDataAdapter(SQLString, conn);
        }

        protected override DbDataAdapter CreateAdapter(DbCommand cmd)
        {
            return new MySqlDataAdapter(cmd as MySqlCommand);
        }

    }
}
