

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
namespace WinAtt
{
    public sealed class SqlLinker
    {
        private string _connString;
        internal StringBuilder builder;
        internal SqlConnection connection;
        public Exception ErrException;
        public string ErrMessage;
        private Hashtable htGlobalData;
        internal SqlTransaction transaction;

        public SqlLinker()
        {
            this.connection = null;
            this.transaction = null;
            this.builder = null;
            this.htGlobalData = null;
        }

        public SqlLinker(string i_ConnString)
        {
            this.connection = null;
            this.transaction = null;
            this.builder = null;
            this.htGlobalData = null;
            try
            {
                this._connString = i_ConnString;
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
        }

        public void AppendTransSql(string sSql)
        {
            if ((sSql != null) && (sSql != ""))
            {
                if (this.builder == null)
                {
                    this.builder = new StringBuilder();
                    this.builder.Append(sSql);
                }
                else
                {
                    this.builder.Append("ァ" + sSql);
                }
            }
        }

        public int BeginTrans()
        {
            try
            {
                this.ClearConn();
                this.connection = new SqlConnection(this._connString);
                this.connection.Open();
                this.transaction = this.connection.BeginTransaction();
                return 1;
            }
            catch (Exception exception)
            {
                if (this.connection != null)
                {
                    this.connection.Close();
                }
                this.connection = null;
                this.transaction = null;
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            return -1;
        }

        public int BeginTrans(IsolationLevel level)
        {
            try
            {
                if ((this.connection != null) && (this.connection.State == ConnectionState.Open))
                {
                    this.connection.Close();
                }
                this.connection = new SqlConnection(this._connString);
                this.connection.Open();
                this.transaction = this.connection.BeginTransaction(level);
                return 1;
            }
            catch (Exception exception)
            {
                if (this.connection != null)
                {
                    this.connection.Close();
                }
                this.connection = null;
                this.transaction = null;
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            return -1;
        }

        public void ClearConn()
        {
            if ((this.connection != null) && (this.connection.State == ConnectionState.Open))
            {
                this.connection.Close();
            }
            this.connection = null;
        }

        public void ClearTransSql()
        {
            this.builder = null;
        }

        public int EndTrans(bool IsCommit)
        {
            int num = -1;
            try
            {
                try
                {
                    if ((this.transaction == null) || (this.transaction.Connection == null))
                    {
                        return num;
                    }
                    if (IsCommit)
                    {
                        this.transaction.Commit();
                    }
                    else
                    {
                        this.transaction.Rollback();
                    }
                    this.builder = null;
                    num = 1;
                }
                catch (Exception exception)
                {
                    this.ErrException = exception;
                    this.ErrMessage = exception.Message;
                    num = -1;
                }
            }
            finally
            {
                if ((this.connection != null) && (this.connection.State == ConnectionState.Open))
                {
                    this.connection.Close();
                }
                this.connection = null;
                this.transaction = null;
            }
            return num;
        }

        public DataSet ExecuteDataSet(string cmd)
        {
            try
            {
                return SqlHelper.ExecuteDataset(this._connString, CommandType.Text, cmd);
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
                return null;
            }
        }

        public DataSet ExecuteDataSet(string spName, params object[] parameterValues)
        {
            try
            {
                return SqlHelper.ExecuteDataset(this._connString, spName, parameterValues);
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
                return null;
            }
        }

        public DataTable ExecuteDataTable(string cmd)
        {
            DataTable table = null;
            try
            {
                table = SqlHelper.ExecuteDataTable(this._connString, CommandType.Text, cmd);
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            return table;
        }

        public DataTable ExecuteDataTable(string cmd, bool bGlobal)
        {
            DataTable table = null;
            if (bGlobal)
            {
                object obj2 = this.GlobalData[cmd];
                if (obj2 is DataTable)
                {
                    return (obj2 as DataTable);
                }
            }
            try
            {
                table = SqlHelper.ExecuteDataTable(this._connString, CommandType.Text, cmd);
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            if (bGlobal)
            {
                if (table != null)
                {
                    this.GlobalData[cmd] = table;
                }
                return table;
            }
            if ((table != null) && (this.GlobalData[cmd] != null))
            {
                this.GlobalData[cmd] = table;
            }
            return table;
        }

        public DataTable ExecuteDataTable(string spName, params SqlParameter[] commandParameters)
        {
            DataTable table = null;
            try
            {
                table = SqlHelper.ExecuteDataTable(this._connString, spName, commandParameters);
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            return table;
        }

        public DataTable ExecuteDataTable(string spName, params object[] commandParameters)
        {
            DataTable table = null;
            try
            {
                table = SqlHelper.ExecuteDataTable(this._connString, spName, commandParameters);
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            return table;
        }

        public DataTable ExecuteDataTableByTableName(string sTableName)
        {
            string cmd = "SELECT * FROM " + sTableName;
            return this.ExecuteDataTable(cmd);
        }

        public DataTable ExecuteDataTableSP(string spName)
        {
            DataTable table = null;
            try
            {
                table = SqlHelper.ExecuteDataTable(this._connString, spName, (object[])null);
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            return table;
        }

        public DataView ExecuteDataView(string cmd)
        {
            DataTable table = null;
            DataView view = null;
            try
            {
                table = SqlHelper.ExecuteDataTable(this._connString, CommandType.Text, cmd);
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            if (table != null)
            {
                view = new DataView(table);
            }
            return view;
        }

        public DataView ExecuteDataView(string spName, params SqlParameter[] commandParameters)
        {
            DataTable table = null;
            DataView view = null;
            try
            {
                table = SqlHelper.ExecuteDataTable(this._connString, spName, commandParameters);
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            if (table != null)
            {
                view = new DataView(table);
            }
            return view;
        }

        public DataView ExecuteDataView(string spName, params object[] commandParameters)
        {
            DataTable table = null;
            DataView view = null;
            try
            {
                table = SqlHelper.ExecuteDataTable(this._connString, spName, commandParameters);
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            if (table != null)
            {
                view = new DataView(table);
            }
            return view;
        }

        public DataView ExecuteDataViewSP(string spName)
        {
            DataTable table = null;
            DataView view = null;
            try
            {
                table = SqlHelper.ExecuteDataTable(this._connString, spName, (object[])null);
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            if (table != null)
            {
                view = new DataView(table);
            }
            return view;
        }

        public int ExecuteNonQuery(string cmd)
        {
            int num = -1;
            try
            {
                num = SqlHelper.ExecuteNonQuery(this._connString, CommandType.Text, cmd);
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            return num;
        }

        public int ExecuteNonQuery(string spName, params SqlParameter[] commandParameters)
        {
            int num = -1;
            try
            {
                num = SqlHelper.ExecuteNonQuery(this._connString, CommandType.StoredProcedure, spName, commandParameters);
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            return num;
        }

        public int ExecuteNonQuery(string spName, params object[] parameterValues)
        {
            int num = -1;
            try
            {
                num = SqlHelper.ExecuteNonQuery(this._connString, spName, parameterValues);
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            return num;
        }

        public int ExecuteNonQuerySP(string spName)
        {
            int num = -1;
            try
            {
                num = SqlHelper.ExecuteNonQuery(this._connString, spName, (object[])null);
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            return num;
        }

        public int ExecuteNonQuerySPRet(string spName, params SqlParameter[] commandParameters)
        {
            int num = -1;
            try
            {
                num = SqlHelper.ExecuteNonQuery(this._connString, CommandType.StoredProcedure, spName, commandParameters);
                if (commandParameters[commandParameters.Length - 1].Direction == ParameterDirection.ReturnValue)
                {
                    num = (int)commandParameters[commandParameters.Length - 1].Value;
                }
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            return num;
        }

        public SqlDataReader ExecuteReader(string spName, params object[] parametervalues)
        {
            SqlDataReader reader = null;
            try
            {
                reader = SqlHelper.ExecuteReader(this._connString, spName, parametervalues);
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
                return null;
            }
            return reader;
        }

        public object ExecuteScalar(string spName, params SqlParameter[] parametervalues)
        {
            try
            {
                return SqlHelper.ExecuteScalar(this._connString, CommandType.StoredProcedure, spName, parametervalues);
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
                return null;
            }
        }

        public object ExecuteScalar(string spName, params object[] parametervalues)
        {
            try
            {
                return SqlHelper.ExecuteScalar(this._connString, spName, parametervalues);
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
                return null;
            }
        }

        public int ExecuteTransSP(string spName, params SqlParameter[] commandParameters)
        {
            int num = -1;
            try
            {
                if (this.transaction == null)
                {
                    return -1;
                }
                num = SqlHelper.ExecuteNonQuery(this.transaction, CommandType.StoredProcedure, spName, commandParameters);
                if (commandParameters[commandParameters.Length - 1].Direction == ParameterDirection.ReturnValue)
                {
                    num = (int)commandParameters[commandParameters.Length - 1].Value;
                }
            }
            catch (Exception exception)
            {
                num = -1;
                this.EndTrans(false);
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            return num;
        }

        public int ExecuteTransSql(string cmd)
        {
            int num = -1;
            try
            {
                if (this.transaction == null)
                {
                    return -1;
                }
                num = SqlHelper.ExecuteNonQuery(this.transaction, CommandType.Text, cmd);
            }
            catch (Exception exception)
            {
                num = -1;
                this.EndTrans(false);
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            return num;
        }

        public int ExecuteTransSqlCollection()
        {
            int num = -1;
            int num2 = 0;
            try
            {
                if ((this.builder == null) || (this.builder.ToString().Length <= 0))
                {
                    return 0;
                }
                string[] strArray = this.builder.ToString().Split(new char[] { 'ァ' });
                if (strArray.Length == 1)
                {
                    return this.ExecuteNonQuery(strArray[0]);
                }
                num = this.BeginTrans();
                if (num > 0)
                {
                    foreach (string str in strArray)
                    {
                        if ((str != null) && (str != ""))
                        {
                            if (this.transaction == null)
                            {
                                return -1;
                            }
                            num = SqlHelper.ExecuteNonQuery(this.transaction, CommandType.Text, str);
                            if (num < 0)
                            {
                                break;
                            }
                            num2++;
                        }
                        else
                        {
                            num = -1;
                            break;
                        }
                    }
                }
                else
                {
                    return -1;
                }
                if (num >= 0)
                {
                    this.EndTrans(true);
                }
                else
                {
                    this.EndTrans(false);
                }
            }
            catch (Exception exception)
            {
                num = -1;
                this.EndTrans(false);
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            if (num >= 0)
            {
                return num2;
            }
            return num;
        }

        public int ExecuteTransSqlCollection(string[] sqlCollection)
        {
            int num = -1;
            int num2 = 0;
            if (sqlCollection.Length == 1)
            {
                return this.ExecuteNonQuery(sqlCollection[0]);
            }
            try
            {
                num = this.BeginTrans();
                if (num > 0)
                {
                    foreach (string str in sqlCollection)
                    {
                        if ((str != null) && (str != ""))
                        {
                            if (this.transaction == null)
                            {
                                return -1;
                            }
                            num = SqlHelper.ExecuteNonQuery(this.transaction, CommandType.Text, str);
                            if (num < 0)
                            {
                                break;
                            }
                            num2++;
                        }
                        else
                        {
                            num = -1;
                            break;
                        }
                    }
                }
                else
                {
                    return -1;
                }
                if (num > 0)
                {
                    this.EndTrans(true);
                }
                else
                {
                    this.EndTrans(false);
                }
            }
            catch (Exception exception)
            {
                num = -1;
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            if (num >= 0)
            {
                return num2;
            }
            return num;
        }

        public int ExecuteTransSqlCollection(bool bForceCommit)
        {
            int num = -1;
            int num2 = 0;
            try
            {
                if ((this.builder == null) || (this.builder.ToString().Length <= 0))
                {
                    if (bForceCommit)
                    {
                        return 0;
                    }
                    return 1;
                }
                string[] strArray = this.builder.ToString().Split(new char[] { 'ァ' });
                if (strArray.Length == 1)
                {
                    return this.ExecuteNonQuery(strArray[0]);
                }
                num = this.BeginTrans();
                if (num > 0)
                {
                    foreach (string str in strArray)
                    {
                        if ((str != null) && (str != ""))
                        {
                            if (this.transaction == null)
                            {
                                return -1;
                            }
                            num = SqlHelper.ExecuteNonQuery(this.transaction, CommandType.Text, str);
                            if (bForceCommit)
                            {
                                if (num <= 0)
                                {
                                    break;
                                }
                                num2++;
                            }
                            else
                            {
                                if (num < 0)
                                {
                                    break;
                                }
                                num2++;
                            }
                        }
                        else
                        {
                            num = -1;
                            break;
                        }
                    }
                }
                else
                {
                    return -1;
                }
                if (bForceCommit)
                {
                    if (num > 0)
                    {
                        this.EndTrans(true);
                    }
                    else
                    {
                        this.EndTrans(false);
                    }
                }
                else if (num >= 0)
                {
                    this.EndTrans(true);
                }
                else
                {
                    this.EndTrans(false);
                }
            }
            catch (Exception exception)
            {
                num = -1;
                this.EndTrans(false);
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            if (bForceCommit)
            {
                if (num > 0)
                {
                    return num2;
                }
                return num;
            }
            if (num >= 0)
            {
                return num2;
            }
            return num;
        }

        public int ExecuteTransSqlCollection(ArrayList sqlList)
        {
            int num = -1;
            int num2 = 0;
            if (sqlList.Count == 1)
            {
                return this.ExecuteNonQuery(sqlList[0].ToString());
            }
            try
            {
                num = this.BeginTrans();
                if (num > 0)
                {
                    foreach (object obj2 in sqlList)
                    {
                        if ((obj2 != null) && (obj2.ToString() != ""))
                        {
                            if (this.transaction == null)
                            {
                                return -1;
                            }
                            num = SqlHelper.ExecuteNonQuery(this.transaction, CommandType.Text, obj2.ToString());
                            if (num < 0)
                            {
                                break;
                            }
                            num2++;
                        }
                        else
                        {
                            num = -1;
                            break;
                        }
                    }
                }
                else
                {
                    return -1;
                }
                if (num > 0)
                {
                    this.EndTrans(true);
                }
                else
                {
                    this.EndTrans(false);
                }
            }
            catch (Exception exception)
            {
                num = -1;
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            if (num >= 0)
            {
                return num2;
            }
            return num;
        }

        public void FillDataTable(DataTable dt, string commandText)
        {
            if ((this._connString == null) || (this._connString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                connection.Open();
                if (connection == null)
                {
                    throw new ArgumentNullException("connection");
                }
                SqlCommand command = new SqlCommand();
                bool mustCloseConnection = false;
                SqlHelper.PrepareCommand(command, connection, null, CommandType.Text, commandText, null, out mustCloseConnection);
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    dt.Clear();
                    adapter.Fill(dt);
                    command.Parameters.Clear();
                    if (mustCloseConnection)
                    {
                        connection.Close();
                    }
                }
            }
        }

        public void FillDataTable(DataTable dt, string commandText, int iStart, int iMax)
        {
            if ((this._connString == null) || (this._connString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                connection.Open();
                if (connection == null)
                {
                    throw new ArgumentNullException("connection");
                }
                SqlCommand command = new SqlCommand();
                bool mustCloseConnection = false;
                SqlHelper.PrepareCommand(command, connection, null, CommandType.Text, commandText, null, out mustCloseConnection);
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataSet dataSet = new DataSet();
                    dataSet.Tables.Add(dt);
                    adapter.Fill(dataSet, iStart, iMax, dt.TableName);
                    command.Parameters.Clear();
                    if (mustCloseConnection)
                    {
                        connection.Close();
                    }
                }
            }
        }
        /// <summary>
        /// 通过存储过程来绑定数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="prname"></param>
        /// <param name="parameter"></param>
        public void FillDataTable(DataTable dt, string prname, SqlParameter[] parameter)
        {
            if ((this._connString == null) || (this._connString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }

            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                connection.Open();
                if (connection == null)
                {
                    throw new ArgumentNullException("connection");
                }
                SqlCommand command = new SqlCommand();
                bool mustCloseConnection = false;

                SqlHelper.PrepareCommand(command, connection, null, CommandType.StoredProcedure, prname, parameter, out mustCloseConnection);

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    dt.Clear();
                    adapter.Fill(dt);
                    command.Parameters.Clear();
                    if (mustCloseConnection)
                    {
                        connection.Close();
                    }
                }
            }


        }
        public DataRow GetDataRow(string sql)
        {
            DataRow row = null;
            try
            {
                using (DataTable table = this.ExecuteDataTable(sql))
                {
                    if ((table != null) && (table.Rows.Count > 0))
                    {
                        row = table.Rows[0];
                    }
                }
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            return row;
        }

        public DataRow GetDataRow(string cmd, bool bGlobal)
        {
            DataRow row = null;
            if (bGlobal)
            {
                object obj2 = this.GlobalData[cmd];
                if (obj2 is DataRow)
                {
                    return (obj2 as DataRow);
                }
            }
            try
            {
                using (DataTable table = this.ExecuteDataTable(cmd))
                {
                    if ((table != null) && (table.Rows.Count > 0))
                    {
                        row = table.Rows[0];
                    }
                }
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            if (bGlobal)
            {
                if (row != null)
                {
                    this.GlobalData[cmd] = row;
                }
                return row;
            }
            if ((row != null) && (this.GlobalData[cmd] != null))
            {
                this.GlobalData[cmd] = row;
            }
            return row;
        }

        public int GetInt(string sql)
        {
            int dBInt = -2147483648;
            try
            {
                dBInt = ConvertUtil.GetDBInt(this.GetObject(sql));
            }
            catch (Exception exception)
            {
                dBInt = -2147483648;
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            return dBInt;
        }

        public long GetLong(string sql)
        {
            long num = -9223372036854775808L;
            try
            {
                num = Convert.ToInt64(this.GetObject(sql));
            }
            catch (Exception exception)
            {
                num = -9223372036854775808L;
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            return num;
        }

        public decimal GetMaxDecimal(string sTable, string sField)
        {
            sTable = string.Format("SELECT MAX({0}) FROM {1}", sField, sTable);
            object obj2 = SqlHelper.ExecuteScalar(this._connString, CommandType.Text, sTable);
            if ((obj2 == null) || (obj2 is DBNull))
            {
                return 0M;
            }
            return Convert.ToDecimal(obj2);
        }

        public float GetMaxDouble(string sTable, string sField)
        {
            sTable = string.Format("SELECT MAX({0}) FROM {1}", sField, sTable);
            object obj2 = SqlHelper.ExecuteScalar(this._connString, CommandType.Text, sTable);
            if ((obj2 == null) || (obj2 is DBNull))
            {
                return 0f;
            }
            return Convert.ToSingle(obj2);
        }

        public int GetMaxInt(string sTable, string sField)
        {
            sTable = string.Format("SELECT MAX({0}) FROM {1}", sField, sTable);
            object obj2 = SqlHelper.ExecuteScalar(this._connString, CommandType.Text, sTable);
            if ((obj2 == null) || (obj2 is DBNull))
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
        }

        public long GetMaxLong(string sTable, string sField)
        {
            sTable = string.Format("SELECT MAX({0}) FROM {1}", sField, sTable);
            object obj2 = SqlHelper.ExecuteScalar(this._connString, CommandType.Text, sTable);
            if ((obj2 == null) || (obj2 is DBNull))
            {
                return 0L;
            }
            return Convert.ToInt64(obj2);
        }

        public string GetMaxString(string sTable, string sField)
        {
            sTable = string.Format("SELECT MAX({0}) FROM {1}", sField, sTable);
            object obj2 = SqlHelper.ExecuteScalar(this._connString, CommandType.Text, sTable);
            if (null == obj2)
            {
                return "";
            }
            return Convert.ToString(obj2);
        }

        //public DXDataView GetMemRecord(string cmd)
        //{
        //    DXDataView view = null;
        //    try
        //    {
        //        DataTable dt = this.ExecuteDataTable(cmd);
        //        if (null != dt)
        //        {
        //            view = new DXDataView(dt);
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        this.ErrException = exception;
        //        this.ErrMessage = exception.Message;
        //    }
        //    return view;
        //}

        //public DXDataView GetMemRecord(string spName, params SqlParameter[] commandParameter)
        //{
        //    DXDataView view = null;
        //    try
        //    {
        //        DataTable dt = this.ExecuteDataTable(spName, commandParameter);
        //        if (null != dt)
        //        {
        //            view = new DXDataView(dt);
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        this.ErrException = exception;
        //        this.ErrMessage = exception.Message;
        //    }
        //    return view;
        //}

        public decimal GetMinDecimal(string sTable, string sField)
        {
            sTable = string.Format("SELECT MIN({0}) FROM {1}", sField, sTable);
            object obj2 = SqlHelper.ExecuteScalar(this._connString, CommandType.Text, sTable);
            if ((obj2 == null) || (obj2 is DBNull))
            {
                return 0M;
            }
            return Convert.ToDecimal(obj2);
        }

        public float GetMinDouble(string sTable, string sField)
        {
            sTable = string.Format("SELECT MIN({0}) FROM {1}", sField, sTable);
            object obj2 = SqlHelper.ExecuteScalar(this._connString, CommandType.Text, sTable);
            if ((obj2 == null) || (obj2 is DBNull))
            {
                return 0f;
            }
            return Convert.ToSingle(obj2);
        }

        public int GetMinInt(string sTable, string sField)
        {
            sTable = string.Format("SELECT MIN({0}) FROM {1}", sField, sTable);
            object obj2 = SqlHelper.ExecuteScalar(this._connString, CommandType.Text, sTable);
            if ((obj2 == null) || (obj2 is DBNull))
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
        }

        public long GetMinLong(string sTable, string sField)
        {
            sTable = string.Format("SELECT MIN({0}) FROM {1}", sField, sTable);
            object obj2 = SqlHelper.ExecuteScalar(this._connString, CommandType.Text, sTable);
            if ((obj2 == null) || (obj2 is DBNull))
            {
                return 0L;
            }
            return Convert.ToInt64(obj2);
        }

        public string GetMinString(string sTable, string sField)
        {
            sTable = string.Format("SELECT MIN({0}) FROM {1}", sField, sTable);
            object obj2 = SqlHelper.ExecuteScalar(this._connString, CommandType.Text, sTable);
            if (null == obj2)
            {
                return "";
            }
            return Convert.ToString(obj2);
        }

        public object GetObject(string sql)
        {
            return SqlHelper.ExecuteScalar(this._connString, CommandType.Text, sql);
        }

        public int GetSqlCollectionCount()
        {
            if ((this.builder == null) || (this.builder.ToString().Length <= 0))
            {
                return 0;
            }
            return this.builder.ToString().Split(new char[] { 'ァ' }).Length;
        }

        public string GetString(string sql)
        {
            string str = null;
            try
            {
                str = Convert.ToString(this.GetObject(sql));
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
            }
            return str;
        }

        public object GetTransObject(string sql)
        {
            object obj2 = null;
            try
            {
                if (this.transaction == null)
                {
                    return obj2;
                }
                obj2 = SqlHelper.ExecuteScalar(this.transaction, CommandType.Text, sql);
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
                return null;
            }
            return obj2;
        }

        public bool IsOpenDB()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this._connString))
                {
                    connection.Open();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
                return false;
            }
        }

        public bool IsOpenDB(string connString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                this.ErrException = exception;
                this.ErrMessage = exception.Message;
                return false;
            }
        }

        public void RefreshGlobalData()
        {
            if (this.GlobalData != null)
            {
                this.GlobalData.Clear();
            }
        }

        public string ConnString
        {
            get
            {
                return this._connString;
            }
            set
            {
                if (this._connString != value)
                {
                    this._connString = value;
                }
            }
        }

        public string ErrString
        {
            get
            {
                if ((this.ErrMessage != null) && (this.ErrMessage.Length > 0))
                {
                    string str = string.Copy(this.ErrMessage);
                    this.ErrMessage = "";
                    str.Replace('\'', ' ');
                    str.Replace('\\', ' ');
                    str.Replace('\n', ' ');
                    str.Replace('\r', ' ');
                    str.Trim();
                    return str;
                }
                return "";
            }
            set
            {
                this.ErrException = new Exception("用户定义的异常");
                this.ErrMessage = value;
            }
        }

        public Hashtable GlobalData
        {
            get
            {
                if (this.htGlobalData == null)
                {
                    this.htGlobalData = Hashtable.Synchronized(new Hashtable(40));
                }
                return this.htGlobalData;
            }
        }
    }
}

