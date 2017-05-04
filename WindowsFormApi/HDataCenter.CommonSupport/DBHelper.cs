using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace HDataCenter.CommonSupport
{
    public abstract class DBHelper
    {
        public  static string connectionString = string.Empty;
        protected abstract DbConnection CreateConn();      
        protected abstract DbParameter CreateParam(string content);
        protected abstract DbParameter CreateParam(byte[] fs);
        protected abstract DbCommand CreateCmd(string SQLString = null, DbConnection connection = null);
        protected abstract DbDataAdapter CreateAdapter(string SQLString = null, DbConnection connection = null);
        protected abstract DbDataAdapter CreateAdapter(DbCommand cmd);
       
        #region 公用方法
        public int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        public bool Exists(string strSql, params DbParameter[] cmdParms)
        {
            object obj = GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region  执行简单SQL语句

        public  bool Exists(string SQLString)
        {
            using (DbConnection connection = CreateConn())
            {
                using (DbCommand cmd = CreateCmd())
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    catch (SqlException e)
                    {
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public  int ExecuteSql(string SQLString)
        {
            using (DbConnection connection = CreateConn())
            {
                using (DbCommand cmd = CreateCmd())
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (SqlException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>  
        public  bool ExecuteSqlTran(ArrayList SQLStringList)
        {
            bool re = false;
            using (DbConnection connection = CreateConn())
            {
                connection.Open();
                DbCommand cmd = CreateCmd();
                cmd.Connection = connection;
                DbTransaction tx = connection.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    re = true;
                }
                catch (SqlException E)
                {
                    re = false;
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
            return re;
        }

        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public  int ExecuteSql(string SQLString, string content)
        {
            using (DbConnection connection = CreateConn())
            {
                DbCommand cmd = CreateCmd(SQLString, connection);
                DbParameter myParameter = CreateParam(content);
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (SqlException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

     


        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public  int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            using (DbConnection connection = CreateConn())
            {
                DbCommand cmd = CreateCmd(strSQL, connection);
                DbParameter myParameter = CreateParam(fs);

                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (SqlException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

      

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public  object GetSingle(string SQLString)
        {
            using (DbConnection connection = CreateConn())
            {
                using (DbCommand cmd = CreateCmd())
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (DbException e)
                    {
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回DbDataReader ( 注意：调用该方法后，一定要对DbDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>DbDataReader</returns>
        public  DbDataReader ExecuteReader(string strSQL)
        {
            DbConnection connection = CreateConn();
            DbCommand cmd = CreateCmd(strSQL, connection);
            try
            {
                connection.Open();
                DbDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }


        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public  DataSet Query(string SQLString)
        {
            using (DbConnection connection = CreateConn())
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    DbDataAdapter command = CreateAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                return ds;
            }
        }

     


        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public  int ExecuteSql(string SQLString, params DbParameter[] cmdParms)
        {
            using (DbConnection connection = CreateConn())
            {
                using (DbCommand cmd = CreateCmd())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (SqlException E)
                    {
                        throw new Exception(E.Message);
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        ///  执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="ProcedureList">存储过程的字典表（key为存储过程名称，value是该存储过程的List <DbParameter[]>，每一个DbParameter[] 代表一次存储过程调用 ）</param>
        public  void ExecuteProcedureTran(Dictionary<string, List<DbParameter[]>> ProcedureList)
        {
            using (DbConnection conn = CreateConn())
            {
                conn.Open();
                using (DbTransaction trans = conn.BeginTransaction())
                {
                    DbCommand cmd = CreateCmd();
                    try
                    {
                        //循环
                        foreach (string key in ProcedureList.Keys)
                        {
                            string cmdText = key;

                            List<DbParameter[]> cmdParms = ProcedureList[key];

                            foreach (DbParameter[] pms in cmdParms)
                            {
                                PrepareCommand(cmd, conn, trans, cmdText, pms, CommandType.StoredProcedure);
                                int val = cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }
                            trans.Commit();
                        }
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        public  void ExecuteProcedureTran(Dictionary<string, List<DbParameter[]>> ProcedureList, out string ECODE, out string EMSG)
        {
            ECODE = string.Empty; EMSG = string.Empty;
            using (DbConnection conn = CreateConn())
            {
                conn.Open();
                using (DbTransaction trans = conn.BeginTransaction())
                {
                    DbCommand cmd = CreateCmd();
                    try
                    {
                        //循环
                        foreach (string key in ProcedureList.Keys)
                        {
                            string cmdText = key;

                            List<DbParameter[]> cmdParms = ProcedureList[key];

                            foreach (DbParameter[] pms in cmdParms)
                            {

                                PrepareCommand(cmd, conn, trans, cmdText, pms, CommandType.StoredProcedure);
                                int val = cmd.ExecuteNonQuery();

                                if (IsError(pms, out ECODE, out EMSG))
                                {
                                    cmd.Parameters.Clear();
                                    trans.Rollback();
                                    return;
                                }

                                cmd.Parameters.Clear();
                            }
                            trans.Commit();
                        }
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        private  bool IsError(DbParameter[] pms, out string ECODE, out string EMSG)
        {
            ECODE = string.Empty; EMSG = string.Empty;

            foreach (DbParameter p in pms)
            {
                if (p.ParameterName == "DCODE")
                {
                    ECODE = p.Value.ToString2();
                }
                if (p.ParameterName == "EMSG")
                {
                    EMSG = p.Value.ToString2();
                }
            }

            if (ECODE == "-1")
                return true;

            return false;
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的DbParameter[]）</param>
        public  void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (DbConnection conn = CreateConn())
            {
                conn.Open();
                using (DbTransaction trans = conn.BeginTransaction())
                {
                    DbCommand cmd = CreateCmd();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            DbParameter[] cmdParms = (DbParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            trans.Commit();
                        }
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public  object GetSingle(string SQLString, params DbParameter[] cmdParms)
        {
            using (DbConnection connection = CreateConn())
            {
                using (DbCommand cmd = CreateCmd())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DbDataReader ( 注意：调用该方法后，一定要对DbDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>DbDataReader</returns>
        public  DbDataReader ExecuteReader(string SQLString, params DbParameter[] cmdParms)
        {
            DbConnection connection = CreateConn();
            DbCommand cmd = CreateCmd();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                DbDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public  DataSet Query(string SQLString, params DbParameter[] cmdParms)
        {
            using (DbConnection connection = CreateConn())
            {
                DbCommand cmd = CreateCmd();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (DbDataAdapter da = CreateAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                    return ds;
                }
            }
        }


        private  void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction trans, string cmdText, DbParameter[] cmdParms, CommandType cmdType = CommandType.Text)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (DbParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        #endregion

        #region 存储过程操作

        /// <summary>
        /// 执行存储过程 返回DbDataReader ( 注意：调用该方法后，一定要对DbDataReader进行Close )
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>DbDataReader</returns>
        public  DbDataReader RunProcedureReader(string storedProcName, IDataParameter[] parameters)
        {
            DbConnection connection = CreateConn();
            DbDataReader returnReader;
            connection.Open();
            DbCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return returnReader;
        }

        /// <summary>
        /// 执行存储过程，返回影响的行数  
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public  int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (DbConnection connection = CreateConn())
            {
                int result;
                connection.Open();
                DbCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;
                //Connection.Close();
                return result;
            }
        }
        /// <summary>
        /// 执行存储过程，什么值也不返回 
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        public  void RunProcedure(string storedProcName, DbParameter[] parameters)
        {
            using (DbConnection connection = CreateConn())
            {
                DbCommand cmd = BuildQueryCommand(connection, storedProcName, parameters);
                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行存储过程,返回数据集
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public  DataSet RunProcedureGetDataSet(string storedProcName, DbParameter[] parameters)
        {
            using (DbConnection connection = CreateConn())
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    connection.Open();
                    DbDataAdapter sqlDA = CreateAdapter();
                    sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);


                    sqlDA.Fill(dataSet);
                    connection.Close();
                    return dataSet;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 构建 DbCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>DbCommand</returns>
        private  DbCommand BuildQueryCommand(DbConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            DbCommand command = CreateCmd(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (DbParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            return command;
        }

        /// <summary>
        /// 执行存储过程，返回影响的行数  
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public  int RunProcedure_rowsAffected(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (DbConnection connection = CreateConn())
            {
                int result;
                connection.Open();
                DbCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;
                //Connection.Close();
                return result;
            }
        }

        /// <summary>
        /// 创建 DbCommand 对象实例(用来返回一个整数值) 
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>DbCommand 对象实例</returns>
        private  DbCommand BuildIntCommand(DbConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            return null;
            //DbCommand command = BuildQueryCommand(connection,storedProcName, parameters );
            //command.Parameters.Add( new DbParameter ("ReturnValue", Oracle.ManagedDataAccess.Types. .Int32, 4, ParameterDirection.ReturnValue,false,0,0,string.Empty,DataRowVersion.Default,null ));
            //return command;
        }

        #endregion 

    }
}
