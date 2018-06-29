
 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
namespace WinAtt
{
    public class DBLinker
    {
        protected static string _connString = global::WinAtt.Properties.Settings.Default.attConnectionString;
        protected static SqlLinker _linker = null;

        public static void CheckBillID(string MainTableName, DataRow drCurrent)
        {
            if ((((drCurrent != null) && (drCurrent.Table != null)) && drCurrent.Table.Columns.Contains("BillCode")) && (ConvertUtil.GetDBString(drCurrent["BillCode"]) != ""))
            {
                string sql = string.Concat(new object[] { "SELECT count(*) FROM ", MainTableName, " WHERE BillCode = '", drCurrent["BillCode"], "'" });
                if (Linker.GetInt(sql) > 0)
                {
                    drCurrent["BillCode"] = GetBillID(MainTableName);
                    drCurrent.EndEdit();
                }
            }
        }

        public static void GetBillDataSet(DataSet ds, string sTableMain, string sTableDetail, string sWhereSql)
        {
            GetBillDataSet(ds, sTableMain, "", sTableDetail, "", sWhereSql);
        }

        public static void GetBillDataSet(DataSet ds, string sTableMain, string sTableDetail, string sDetailSql, string sWhereSql)
        {
            GetBillDataSet(ds, sTableMain, "", sTableDetail, sDetailSql, sWhereSql);
        }

        public static void GetBillDataSet(DataSet ds, string sTableMain, string sMainSql, string sTableDetail, string sDetailSql, string sWhereSql)
        {
            string commandText = "";
            if (ConvertUtil.GetDBString(sMainSql) == "")
            {
                commandText = " SELECT * FROM " + sTableMain + " WHERE 1 = 1 AND " + sWhereSql;
            }
            else
            {
                commandText = sMainSql + " WHERE 1 = 1 AND " + sWhereSql;
            }
            Linker.FillDataTable(ds.Tables[sTableMain], commandText);
            if (ConvertUtil.GetDBString(sMainSql) == "")
            {
                commandText = "SELECT * FROM " + sTableDetail + " WHERE 1 = 1 AND " + sWhereSql;
            }
            else
            {
                commandText = sDetailSql + " WHERE 1 = 1 AND " + sWhereSql;
            }
            Linker.FillDataTable(ds.Tables[sTableDetail], commandText);
        }

        public static string GetBillID(string sTableName)
        {
            return GetBillID(sTableName, "BillCode");
        }

        public static string GetBillID(string sTableName, string sFieldName)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[5];
                commandParameters[0] = new SqlParameter("@i_tablename", SqlDbType.VarChar);
                commandParameters[0].Direction = ParameterDirection.Input;
                commandParameters[0].Value = sTableName;
                commandParameters[1] = new SqlParameter("@i_fieldname", SqlDbType.VarChar);
                commandParameters[1].Direction = ParameterDirection.Input;
                commandParameters[1].Value = sFieldName;
                commandParameters[2] = new SqlParameter("@i_generatedate", SqlDbType.VarChar);
                commandParameters[2].Direction = ParameterDirection.Input;
                commandParameters[2].Value = DateTime.Now.ToString("yyyy-MM-dd");
                commandParameters[3] = new SqlParameter("@i_count", SqlDbType.Int);
                commandParameters[3].Direction = ParameterDirection.Input;
                commandParameters[3].Value = 1;
                commandParameters[4] = new SqlParameter("@o_billcode", SqlDbType.VarChar);
                commandParameters[4].Direction = ParameterDirection.Output;
                commandParameters[4].Size = 500;
                commandParameters[4].Value = "";
                Linker.ExecuteNonQuery("proc_generatebillcode", commandParameters);
                return ConvertUtil.GetDBString(commandParameters[4].Value);
            }
            catch
            {
                return "";
            }
        }

        public static void GetCurrentBill(DataTable dt, string sTableMain, string sMainSql, string sKey1, string keyMember1)
        {
            GetCurrentBill(dt, sTableMain, sMainSql, sKey1, keyMember1, "", "-1");
        }

        public static void GetCurrentBill(DataSet ds, string sTableMain, string sMainSql, string sTableDetail, string sDetailSql, string sKey1, string keyMember1)
        {
            GetCurrentBill(ds, sTableMain, sMainSql, sTableDetail, sDetailSql, sKey1, keyMember1, "", "");
        }

        public static void GetCurrentBill(DataTable dt, string sTableMain, string sMainSql, string sKey1, string keyMember1, string sKey2, string keyMember2)
        {
            if (ConvertUtil.GetDBString(sMainSql) == "")
            {
                sMainSql = "SELECT * FROM " + sTableMain + " WHERE " + GetTableKeyWhere(sTableMain, sKey1, keyMember1, sKey2, keyMember2);
            }
            else
            {
                sMainSql = sMainSql + " WHERE " + GetTableKeyWhere(sTableMain, sKey1, keyMember1, sKey2, keyMember2);
            }
            Linker.FillDataTable(dt, sMainSql);
        }

        public static void GetCurrentBill(DataSet ds, string sTableMain, string sMainSql, string sTableDetail, string sDetailSql, string sKey1, string keyMember1, string sKey2, string keyMember2)
        {
            if (ConvertUtil.GetDBString(sMainSql) == "")
            {
                sMainSql = "SELECT * FROM " + sTableMain + " WHERE " + GetTableKeyWhere(sTableMain, sKey1, keyMember1, sKey2, keyMember2);
            }
            else
            {
                sMainSql = sMainSql + " WHERE " + GetTableKeyWhere(sTableMain, sKey1, keyMember1, sKey2, keyMember2);
            }
            Linker.FillDataTable(ds.Tables[sTableMain], sMainSql);
            if (ConvertUtil.GetDBString(sTableDetail) != "")
            {
                if (ConvertUtil.GetDBString(sDetailSql) == "")
                {
                    sDetailSql = "SELECT * FROM " + sTableDetail + " WHERE " + GetTableKeyWhere(sTableDetail, sKey1, keyMember1, sKey2, keyMember2);
                }
                else
                {
                    sDetailSql = sDetailSql + " WHERE " + GetTableKeyWhere(sTableDetail, sKey1, keyMember1, sKey2, keyMember2);
                }
                Linker.FillDataTable(ds.Tables[sTableDetail], sDetailSql);
            }
        }

        public static DateTime GetDBCurrentDate()
        {
            try
            {
                return Convert.ToDateTime(Linker.GetObject("select getdate()"));
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public static string GetDBCurrentDateString()
        {
            try
            {
                return Linker.GetString("select getdate()");
            }
            catch
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        //public static string GetDelSql(string sTableName, string sPrimaryKey1, DataRow dr)
        //{
        //    return GetDelSql(sTableName, sPrimaryKey1, "", "", dr);
        //}

        //public static string GetDelSql(string sTableName, string sPrimaryKey1, string sPrimaryKey2, DataRow dr)
        //{
        //    return GetDelSql(sTableName, sPrimaryKey1, sPrimaryKey2, "", dr);
        //}

        //public static string GetDelSql(string sTableName, string sPrimaryKey1, string sPrimaryKey2, string sPrimaryKey3, DataRow dr)
        //{
        //    if (null == dr)
        //    {
        //        return "";
        //    }
        //    DataTable tableSet = TableCache.GetTableSet(sTableName);
        //    string str = "  DELETE FROM " + sTableName + " WHERE 1 = 1 ";
        //    if (dr.Table.Columns.Contains("TimeRemark") && tableSet.Columns.Contains("TimeRemark"))
        //    {
        //        object obj2 = str;
        //        str = string.Concat(new object[] { obj2, " AND TimeRemark = '", dr["TimeRemark", DataRowVersion.Original], "' " });
        //    }
        //    string str3 = str;
        //    str = str3 + " AND " + sPrimaryKey1 + " = '" + dr[sPrimaryKey1, DataRowVersion.Original].ToString().Trim() + "'";
        //    if (ConvertUtil.GetDBString(sPrimaryKey2) != "")
        //    {
        //        str3 = str;
        //        str = str3 + " AND " + sPrimaryKey2 + " = '" + dr[sPrimaryKey2, DataRowVersion.Original].ToString().Trim() + "'";
        //    }
        //    if (ConvertUtil.GetDBString(sPrimaryKey3) != "")
        //    {
        //        str3 = str;
        //        str = str3 + " AND " + sPrimaryKey3 + " = '" + dr[sPrimaryKey3, DataRowVersion.Original].ToString().Trim() + "'";
        //    }
        //    return str;
        //}

        public static void GetFirstBillD(DataTable dt, string sTableMain, string sMainSql, string sKey1, string sKey2)
        {
            GetFirstBillD(dt, sTableMain, sMainSql, "", sKey1, sKey2);
        }

        public static void GetFirstBillD(DataTable dt, string sTableMain, string sMainSql, string sWhereSql, string sKey1, string sKey2)
        {
            string sql = "";
            if (ConvertUtil.GetDBString(sWhereSql) == "")
            {
                sql = "SELECT TOP 1 " + GetTableKeyColumn(sKey1, sKey2) + " FROM " + sTableMain + " order by " + sKey1 + " asc";
            }
            else
            {
                sql = "SELECT TOP 1 " + GetTableKeyColumn(sKey1, sKey2) + " FROM " + sTableMain + " WHERE " + sWhereSql + " order by " + sKey1 + " asc";
            }
            string dBString = "";
            string str3 = "";
            if (ConvertUtil.GetDBString(sKey2) == "")
            {
                dBString = Linker.GetString(sql);
            }
            else
            {
                DataRow dataRow = Linker.GetDataRow(sql);
                dBString = ConvertUtil.GetDBString(dataRow[sKey1]);
                str3 = ConvertUtil.GetDBString(dataRow[sKey2]);
            }
            GetCurrentBill(dt, sTableMain, sMainSql, sKey1, dBString, sKey2, str3);
        }

        public static void GetFirstBillD(DataSet ds, string sTableMain, string sMainSql, string sTableDetail, string sDetailSql, string sKey1, string sKey2)
        {
            GetFirstBillD(ds, sTableMain, sMainSql, sTableDetail, sDetailSql, "", sKey1, sKey2);
        }

        public static void GetFirstBillD(DataSet ds, string sTableMain, string sMainSql, string sTableDetail, string sDetailSql, string sWhereSql, string sKey1, string sKey2)
        {
            string sql = "";
            if (ConvertUtil.GetDBString(sWhereSql) == "")
            {
                sql = "SELECT TOP 1 " + GetTableKeyColumn(sKey1, sKey2) + " FROM " + sTableMain + " order by " + sKey1 + " asc";
            }
            else
            {
                sql = "SELECT TOP 1 " + GetTableKeyColumn(sKey1, sKey2) + " FROM " + sTableMain + " WHERE " + sWhereSql + " order by " + sKey1 + " asc";
            }
            string dBString = "";
            string str3 = "";
            if (ConvertUtil.GetDBString(sKey2) == "")
            {
                dBString = Linker.GetString(sql);
            }
            else
            {
                DataRow dataRow = Linker.GetDataRow(sql);
                dBString = ConvertUtil.GetDBString(dataRow[sKey1]);
                str3 = ConvertUtil.GetDBString(dataRow[sKey2]);
            }
            GetCurrentBill(ds, sTableMain, sMainSql, sTableDetail, sDetailSql, sKey1, dBString, sKey2, str3);
        }

        public static void GetFirstBillS(DataTable dt, string sTableMain, string sMainSql, string sKey1)
        {
            GetFirstBillD(dt, sTableMain, sMainSql, "", sKey1, "");
        }

        public static void GetFirstBillS(DataTable dt, string sTableMain, string sMainSql, string sWhereSql, string sKey1)
        {
            GetFirstBillD(dt, sTableMain, sMainSql, sWhereSql, sKey1, "");
        }

        public static void GetFirstBillS(DataSet ds, string sTableMain, string sMainSql, string sTableDetail, string sDetailSql, string sKey1)
        {
            GetFirstBillD(ds, sTableMain, sMainSql, sTableDetail, sDetailSql, "", sKey1, "");
        }

        public static void GetFirstBillS(DataSet ds, string sTableMain, string sMainSql, string sTableDetail, string sDetailSql, string sWhereSql, string sKey1)
        {
            GetFirstBillD(ds, sTableMain, sMainSql, sTableDetail, sDetailSql, sWhereSql, sKey1, "");
        }

        //public static string GetInsertSql(string sTableName, DataRow dr)
        //{
        //    if ((dr == null) || (null == dr.Table))
        //    {
        //        return "";
        //    }
        //    CheckBillID(sTableName, dr);
        //    DataTable tableSet = TableCache.GetTableSet(sTableName);
        //    StringBuilder builder = new StringBuilder(20);
        //    StringBuilder builder2 = new StringBuilder(20);
        //    builder.Append("    INSERT INTO " + sTableName + "(");
        //    builder2.Append("    VALUES(");
        //    foreach (DataColumn column in tableSet.Columns)
        //    {
        //        if (!dr.Table.Columns.Contains(column.ColumnName) || (((!ConvertUtil.DBCompare(column.ColumnName, "TimeRemark") && !ConvertUtil.DBCompare(column.ColumnName, "NewDate")) && !ConvertUtil.DBCompare(column.ColumnName, "NewUser")) && (dr[column.ColumnName] == DBNull.Value)))
        //        {
        //            continue;
        //        }
        //        if (ConvertUtil.DBCompare(column.ColumnName, "TimeRemark"))
        //        {
        //            dr[column.ColumnName] = DateTime.Now.Ticks;
        //        }
        //        if (ConvertUtil.DBCompare(column.ColumnName, "NewDate") && (dr[column.ColumnName] == DBNull.Value))
        //        {
        //            dr[column.ColumnName] = ConvertUtil.GetNowLongDateString();
        //        }
        //        if (ConvertUtil.DBCompare(column.ColumnName, "NewUser"))
        //        {
        //            dr[column.ColumnName] = LoginUserUtil.Instance.UserName;
        //        }
        //        builder.Append(column.ColumnName + ", ");
        //        if (column.DataType == typeof(DateTime))
        //        {
        //            DateTime dt = Convert.ToDateTime(dr[column.ColumnName]);
        //            builder2.Append("'" + ConvertUtil.GetLongDateString(dt) + "', ");
        //        }
        //        else if (column.DataType.Equals(typeof(byte[])))
        //        {
        //            StringBuilder builder3 = null;
        //            byte[] buffer = (byte[])dr[column.ColumnName];
        //            if (buffer != null)
        //            {
        //                builder3 = new StringBuilder("0x", (buffer.Length * 2) + 2);
        //                foreach (byte num in buffer)
        //                {
        //                    builder3.AppendFormat("{0:X2}", num);
        //                }
        //            }
        //            if (builder3 != null)
        //            {
        //                builder2.Append(builder3.ToString() + ", ");
        //            }
        //            else
        //            {
        //                builder2.Append("null, ");
        //            }
        //        }
        //        else
        //        {
        //            builder2.Append("'" + dr[column.ColumnName].ToString().Trim().Replace("'", "''") + "', ");
        //        }
        //    }
        //    builder.Remove(builder.Length - 2, 2);
        //    builder.Append(")");
        //    builder2.Remove(builder2.Length - 2, 2);
        //    builder2.Append(")");
        //    builder.Append(builder2.ToString());
        //    return builder.ToString();
        //}

        public static void GetLastBillD(DataTable dt, string sTableMain, string sMainSql, string sKey1, string sKey2)
        {
            GetLastBillD(dt, sTableMain, sMainSql, "", sKey1, sKey2);
        }

        public static void GetLastBillD(DataTable dt, string sTableMain, string sMainSql, string sWhereSql, string sKey1, string sKey2)
        {
            string sql = "";
            if (ConvertUtil.GetDBString(sWhereSql) == "")
            {
                sql = "SELECT TOP 1 " + GetTableKeyColumn(sKey1, sKey2) + " FROM " + sTableMain + " order by " + sKey1 + " desc";
            }
            else
            {
                sql = "SELECT TOP 1 " + GetTableKeyColumn(sKey1, sKey2) + " FROM " + sTableMain + " WHERE " + sWhereSql + " order by " + sKey1 + " desc";
            }
            string dBString = "";
            string str3 = "";
            if (ConvertUtil.GetDBString(sKey2) == "")
            {
                dBString = Linker.GetString(sql);
            }
            else
            {
                DataRow dataRow = Linker.GetDataRow(sql);
                dBString = ConvertUtil.GetDBString(dataRow[sKey1]);
                str3 = ConvertUtil.GetDBString(dataRow[sKey2]);
            }
            GetCurrentBill(dt, sTableMain, sMainSql, sKey1, dBString, sKey2, str3);
        }

        public static void GetLastBillD(DataSet ds, string sTableMain, string sMainSql, string sTableDetail, string sDetailSql, string sKey1, string sKey2)
        {
            GetLastBillD(ds, sTableMain, sMainSql, sTableDetail, sDetailSql, "", sKey1, sKey2);
        }

        public static void GetLastBillD(DataSet ds, string sTableMain, string sMainSql, string sTableDetail, string sDetailSql, string sWhereSql, string sKey1, string sKey2)
        {
            string sql = "";
            if (ConvertUtil.GetDBString(sWhereSql) == "")
            {
                sql = "SELECT TOP 1 " + GetTableKeyColumn(sKey1, sKey2) + " FROM " + sTableMain + " order by " + sKey1 + " desc";
            }
            else
            {
                sql = "SELECT TOP 1 " + GetTableKeyColumn(sKey1, sKey2) + " FROM " + sTableMain + " WHERE " + sWhereSql + " order by " + sKey1 + " desc";
            }
            string dBString = "";
            string str3 = "";
            if (ConvertUtil.GetDBString(sKey2) == "")
            {
                dBString = Linker.GetString(sql);
            }
            else
            {
                DataRow dataRow = Linker.GetDataRow(sql);
                dBString = ConvertUtil.GetDBString(dataRow[sKey1]);
                str3 = ConvertUtil.GetDBString(dataRow[sKey2]);
            }
            GetCurrentBill(ds, sTableMain, sMainSql, sTableDetail, sDetailSql, sKey1, dBString, sKey2, str3);
        }

        public static void GetLastBillS(DataTable dt, string sTableMain, string sMainSql, string sKey1)
        {
            GetLastBillD(dt, sTableMain, sMainSql, "", sKey1, "");
        }

        public static void GetLastBillS(DataTable dt, string sTableMain, string sMainSql, string sWhereSql, string sKey1)
        {
            GetLastBillD(dt, sTableMain, sMainSql, sWhereSql, sKey1, "");
        }

        public static void GetLastBillS(DataSet ds, string sTableMain, string sMainSql, string sTableDetail, string sDetailSql, string sKey1)
        {
            GetLastBillD(ds, sTableMain, sMainSql, sTableDetail, sDetailSql, "", sKey1);
        }

        public static void GetLastBillS(DataSet ds, string sTableMain, string sMainSql, string sTableDetail, string sDetailSql, string sWhereSql, string sKey1)
        {
            GetLastBillD(ds, sTableMain, sMainSql, sTableDetail, sDetailSql, sWhereSql, sKey1);
        }

        public static void GetMainBillDataSet(DataSet ds, string sTableMain, string sTableDetail, string sJoinSql, string sWhereSql)
        {
            GetMainBillDataSet(ds, sTableMain, "", sTableDetail, "", sJoinSql, sWhereSql);
        }

        public static void GetMainBillDataSet(DataSet ds, string sTableMain, string sTableDetail, string sDetailSql, string sJoinSql, string sWhereSql)
        {
            GetMainBillDataSet(ds, sTableMain, "", sTableDetail, sDetailSql, sJoinSql, sWhereSql);
        }

        public static void GetMainBillDataSet(DataSet ds, string sTableMain, string sMainSql, string sTableDetail, string sDetailSql, string sJoinSql, string sWhereSql)
        {
            string commandText = "";
            if (ConvertUtil.GetDBString(sMainSql) == "")
            {
                commandText = " SELECT * FROM " + sTableMain + " WHERE 1 = 1 AND " + sWhereSql;
            }
            else
            {
                commandText = sMainSql + " WHERE 1 = 1 AND " + sWhereSql;
            }
            Linker.FillDataTable(ds.Tables[sTableMain], commandText);
            if (ConvertUtil.GetDBString(sMainSql) == "")
            {
                commandText = "SELECT * FROM " + sTableDetail + sJoinSql + " WHERE 1 = 1 AND " + sWhereSql;
            }
            else
            {
                commandText = sDetailSql + sJoinSql + " WHERE 1 = 1 AND " + sWhereSql;
            }
            Linker.FillDataTable(ds.Tables[sTableDetail], commandText);
        }

        public static void GetMainTable(DataTable dt, string sSql)
        {
            Linker.FillDataTable(dt, sSql);
        }
        /// <summary>
        /// 通过存储过程绑定数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="prname"></param>
        /// <param name="parameter"></param>
        public static void GetMainTable(DataTable dt, string prname, SqlParameter[] parameter)
        {
            Linker.FillDataTable(dt, prname, parameter);
        }
        public static void GetMainTable(DataSet ds, string sTableMain, string sWhereSql)
        {
            GetMainTable(ds.Tables[sTableMain], sTableMain, "", sWhereSql);
        }

        public static void GetMainTable(DataTable dt, string sTableMain, string sWhereSql)
        {
            GetMainTable(dt, sTableMain, "", sWhereSql);
        }

        public static void GetMainTable(DataSet ds, string sTableMain, string sMainSql, string sWhereSql)
        {
            GetMainTable(ds.Tables[sTableMain], sTableMain, sMainSql, sWhereSql);
        }
        /// <summary>
        /// 获取一个表的数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sTableMain"></param>
        /// <param name="sMainSql"></param>
        /// <param name="sWhereSql"></param>
        public static void GetMainTable(DataTable dt, string sTableMain, string sMainSql, string sWhereSql)
        {
            StringBuilder builder = new StringBuilder(5);
            if (ConvertUtil.GetDBString(sMainSql) == "")
            {
                builder.Append(" SELECT * FROM ");
                builder.Append(sTableMain);
            }
            else
            {
                builder.Append(sMainSql);
            }
            if (ConvertUtil.GetDBString(sWhereSql) != "")
            {
                builder.Append(" WHERE ");
                builder.Append(sWhereSql);
            }
            Linker.FillDataTable(dt, builder.ToString());
        }

        public static int GetNewKey(string sTableName)
        {
            return GetNewKey(sTableName, 1);
        }

        public static int GetNewKey(string sTableName, int iCount)
        {
            try
            {
                object[] parametervalues = new object[] { sTableName, iCount, -1 };
                return Convert.ToInt32(Linker.ExecuteScalar("proc_generatetablekey", parametervalues));
            }
            catch
            {
                return -1;
            }
        }

        public static void GetNextBillD(DataTable dt, string sTableMain, string sMainSql, string sKey1, string keyMember1, string sKey2, string keyMember2)
        {
            GetNextBillD(dt, sTableMain, sMainSql, "", sKey1, keyMember1, sKey2, keyMember2);
        }

        public static void GetNextBillD(DataTable dt, string sTableMain, string sMainSql, string sWhereSql, string sKey1, string keyMember1, string sKey2, string keyMember2)
        {
            string sql = "";
            if (ConvertUtil.GetDBString(sWhereSql) == "")
            {
                sql = "SELECT TOP 1 " + GetTableKeyColumn(sKey1, sKey2) + " FROM " + sTableMain + " WHERE " + GetTableKeyWhere(sTableMain, sKey1, keyMember1, sKey2, keyMember2) + " order by " + sKey1 + " asc";
            }
            else
            {
                sql = "SELECT TOP 1 " + GetTableKeyColumn(sKey1, sKey2) + " FROM " + sTableMain + " WHERE " + GetTableKeyWhere(sTableMain, sKey1, keyMember1, sKey2, keyMember2) + " AND " + sWhereSql + " order by " + sKey1 + " asc";
            }
            keyMember1 = "";
            keyMember2 = "";
            if (ConvertUtil.GetDBString(sKey2) == "")
            {
                keyMember1 = Linker.GetString(sql);
            }
            else
            {
                DataRow dataRow = Linker.GetDataRow(sql);
                keyMember1 = ConvertUtil.GetDBString(dataRow[sKey1]);
                keyMember2 = ConvertUtil.GetDBString(dataRow[sKey2]);
            }
            if (ConvertUtil.GetDBInt(keyMember1) <= 0)
            {
                GetFirstBillD(dt, sTableMain, sMainSql, sWhereSql, sKey1, sKey2);
            }
            else
            {
                GetCurrentBill(dt, sTableMain, sMainSql, sKey1, keyMember1, sKey2, keyMember2);
            }
        }

        public static void GetNextBillD(DataSet ds, string sTableMain, string sMainSql, string sTableDetail, string sDetailSql, string sKey1, string keyMember1, string sKey2, string keyMember2)
        {
            GetNextBillD(ds, sTableMain, sMainSql, sTableDetail, sDetailSql, "", sKey1, keyMember1, sKey2, keyMember2);
        }

        public static void GetNextBillD(DataSet ds, string sTableMain, string sMainSql, string sTableDetail, string sDetailSql, string sWhereSql, string sKey1, string keyMember1, string sKey2, string keyMember2)
        {
            string sql = "";
            if (ConvertUtil.GetDBString(sWhereSql) == "")
            {
                sql = "SELECT TOP 1 " + GetTableKeyColumn(sKey1, sKey2) + " FROM " + sTableMain + " WHERE " + sKey1 + " > " + keyMember1 + GetTableKey2Where(sTableMain, sKey2, keyMember2) + " order by " + sKey1 + " asc";
            }
            else
            {
                sql = "SELECT TOP 1 " + GetTableKeyColumn(sKey1, sKey2) + " FROM " + sTableMain + " WHERE " + sKey1 + " > " + keyMember1 + GetTableKey2Where(sTableMain, sKey2, keyMember2) + " AND " + sWhereSql + " order by " + sKey1 + " asc";
            }
            if (ConvertUtil.GetDBString(sKey2) == "")
            {
                keyMember1 = Linker.GetString(sql);
            }
            else
            {
                DataRow dataRow = Linker.GetDataRow(sql);
                keyMember1 = ConvertUtil.GetDBString(dataRow[sKey1]);
                keyMember2 = ConvertUtil.GetDBString(dataRow[sKey2]);
            }
            if (ConvertUtil.GetDBInt(keyMember1) <= 0)
            {
                GetFirstBillD(ds, sTableMain, sMainSql, sTableDetail, sDetailSql, sWhereSql, sKey1, sKey2);
            }
            else
            {
                GetCurrentBill(ds, sTableMain, sMainSql, sTableDetail, sDetailSql, sKey1, keyMember1, sKey2, keyMember2);
            }
        }

        public static void GetNextBillS(DataTable dt, string sTableMain, string sMainSql, string sKey1, string keyMember1)
        {
            GetNextBillD(dt, sTableMain, sMainSql, "", sKey1, keyMember1, "", "");
        }

        public static void GetNextBillS(DataTable dt, string sTableMain, string sMainSql, string sWhereSql, string sKey1, string keyMember1)
        {
            GetNextBillD(dt, sTableMain, sMainSql, sWhereSql, sKey1, keyMember1, "", "");
        }

        public static void GetNextBillS(DataSet ds, string sTableMain, string sMainSql, string sTableDetail, string sDetailSql, string sKey1, string keyMember1)
        {
            GetNextBillD(ds, sTableMain, sMainSql, sTableDetail, sDetailSql, "", sKey1, keyMember1, "", "");
        }

        public static void GetNextBillS(DataSet ds, string sTableMain, string sMainSql, string sTableDetail, string sDetailSql, string sWhereSql, string sKey1, string keyMember1)
        {
            GetNextBillD(ds, sTableMain, sMainSql, sTableDetail, sDetailSql, sWhereSql, sKey1, keyMember1, "", "");
        }

        public static void GetPrevBillD(DataTable dt, string sTableMain, string sMainSql, string sKey1, string keyMember1, string sKey2, string keyMember2)
        {
            GetPrevBillD(dt, sTableMain, sMainSql, "", sKey1, keyMember1, sKey2, keyMember2);
        }

        public static void GetPrevBillD(DataTable dt, string sTableMain, string sMainSql, string sWhereSql, string sKey1, string keyMember1, string sKey2, string keyMember2)
        {
            string sql = "";
            if (ConvertUtil.GetDBString(sWhereSql) == "")
            {
                sql = "SELECT TOP 1 " + GetTableKeyColumn(sKey1, sKey2) + " FROM " + sTableMain + " WHERE " + GetTableKeyWhere(sTableMain, sKey1, keyMember1, sKey2, keyMember2) + " order by " + sKey1 + " desc";
            }
            else
            {
                sql = "SELECT TOP 1 " + GetTableKeyColumn(sKey1, sKey2) + " FROM " + sTableMain + " WHERE " + GetTableKeyWhere(sTableMain, sKey1, keyMember1, sKey2, keyMember2) + " AND " + sWhereSql + " order by " + sKey1 + " desc";
            }
            if (ConvertUtil.GetDBString(sKey2) == "")
            {
                keyMember1 = Linker.GetString(sql);
            }
            else
            {
                DataRow dataRow = Linker.GetDataRow(sql);
                keyMember1 = ConvertUtil.GetDBString(dataRow[sKey1]);
                keyMember2 = ConvertUtil.GetDBString(dataRow[sKey2]);
            }
            if (ConvertUtil.GetDBInt(keyMember1) <= 0)
            {
                GetLastBillD(dt, sTableMain, sMainSql, sWhereSql, sKey1, sKey2);
            }
            else
            {
                GetCurrentBill(dt, sTableMain, sMainSql, sKey1, keyMember1, sKey2, keyMember2);
            }
        }

        public static void GetPrevBillD(DataSet ds, string sTableMain, string sMainSql, string sTableDetail, string sDetailSql, string sKey1, string keyMember1, string sKey2, string keyMember2)
        {
            GetPrevBillD(ds, sTableMain, sMainSql, sTableDetail, sDetailSql, "", sKey1, keyMember1, sKey2, keyMember2);
        }

        public static void GetPrevBillD(DataSet ds, string sTableMain, string sMainSql, string sTableDetail, string sDetailSql, string sWhereSql, string sKey1, string keyMember1, string sKey2, string keyMember2)
        {
            string sql = "";
            if (ConvertUtil.GetDBString(sWhereSql) == "")
            {
                sql = "SELECT TOP 1 " + GetTableKeyColumn(sKey1, sKey2) + " FROM " + sTableMain + " WHERE " + sKey1 + " < " + keyMember1 + GetTableKey2Where(sTableMain, sKey2, keyMember2) + " order by " + sKey1 + " desc";
            }
            else
            {
                sql = "SELECT TOP 1 " + GetTableKeyColumn(sKey1, sKey2) + " FROM " + sTableMain + " WHERE " + sKey1 + " < " + keyMember1 + GetTableKey2Where(sTableMain, sKey2, keyMember2) + " AND " + sWhereSql + " order by " + sKey1 + " desc";
            }
            if (ConvertUtil.GetDBString(sKey2) == "")
            {
                keyMember1 = Linker.GetString(sql);
            }
            else
            {
                DataRow dataRow = Linker.GetDataRow(sql);
                keyMember1 = ConvertUtil.GetDBString(dataRow[sKey1]);
                keyMember2 = ConvertUtil.GetDBString(dataRow[sKey2]);
            }
            if (ConvertUtil.GetDBInt(keyMember1) <= 0)
            {
                GetLastBillD(ds, sTableMain, sMainSql, sTableDetail, sDetailSql, sWhereSql, sKey1, sKey2);
            }
            else
            {
                GetCurrentBill(ds, sTableMain, sMainSql, sTableDetail, sDetailSql, sKey1, keyMember1, sKey2, keyMember2);
            }
        }

        public static void GetPrevBillS(DataTable dt, string sTableMain, string sMainSql, string sKey1, string keyMember1)
        {
            GetPrevBillD(dt, sTableMain, sMainSql, "", sKey1, keyMember1, "", "");
        }

        public static void GetPrevBillS(DataTable dt, string sTableMain, string sMainSql, string sWhereSql, string sKey1, string keyMember1)
        {
            GetPrevBillD(dt, sTableMain, sMainSql, sWhereSql, sKey1, keyMember1, "", "");
        }

        public static void GetPrevBillS(DataSet ds, string sTableMain, string sMainSql, string sTableDetail, string sDetailSql, string sKey1, string keyMember1)
        {
            GetPrevBillD(ds, sTableMain, sMainSql, sTableDetail, sDetailSql, "", sKey1, keyMember1, "", "");
        }

        public static void GetPrevBillS(DataSet ds, string sTableMain, string sMainSql, string sTableDetail, string sDetailSql, string sWhereSql, string sKey1, string keyMember1)
        {
            GetPrevBillD(ds, sTableMain, sMainSql, sTableDetail, sDetailSql, sWhereSql, sKey1, keyMember1, "", "");
        }

        public static SqlParameter GetSqlParameter(string paramName, SqlDbType dbType, object oValue)
        {
            SqlParameter parameter = new SqlParameter(paramName, dbType);
            parameter.Value = oValue;
            return parameter;
        }

        public static SqlParameter GetSqlParameter(string paramName, SqlDbType dbType, ParameterDirection direction, object oValue)
        {
            SqlParameter parameter = new SqlParameter(paramName, dbType);
            parameter.Value = oValue;
            parameter.Direction = direction;
            return parameter;
        }

        public static SqlParameter GetSqlParameter(string paramName, SqlDbType dbType, int size, object oValue)
        {
            SqlParameter parameter = new SqlParameter(paramName, dbType, size);
            parameter.Value = oValue;
            return parameter;
        }

        public static SqlParameter GetSqlParameter(string paramName, SqlDbType dbType, int size, ParameterDirection direction, object oValue)
        {
            SqlParameter parameter = new SqlParameter(paramName, dbType, size);
            parameter.Value = oValue;
            parameter.Direction = direction;
            return parameter;
        }

        private static string GetTableKey2Where(string sTableMain, string sKey2, string keyMember2)
        {
            string str = "";
            if (ConvertUtil.GetDBString(sKey2) != "")
            {
                string str3 = str;
                str = str3 + " AND " + sTableMain + "." + sKey2 + " = '" + keyMember2 + "'";
            }
            return str;
        }

        private static string GetTableKeyColumn(string sKey1, string sKey2)
        {
            string str = " " + sKey1;
            if (ConvertUtil.GetDBString(sKey2) != "")
            {
                str = str + ", " + sKey2 + " ";
            }
            return str;
        }

        private static string GetTableKeyWhere(string sTableMain, string sKey1, string keyMember1, string sKey2, string keyMember2)
        {
            string str = sTableMain + "." + sKey1 + " = '" + keyMember1 + "'";
            if (ConvertUtil.GetDBString(sKey2) != "")
            {
                string str3 = str;
                str = str3 + " AND " + sTableMain + "." + sKey2 + " = '" + keyMember2 + "'";
            }
            return str;
        }

        //public static string GetUpdateSql(string sTableName, string sPrimaryKey, DataRow dr)
        //{
        //    return GetUpdateSql(sTableName, sPrimaryKey, "", "", dr, true);
        //}

        //public static string GetUpdateSql(string sTableName, string sPrimaryKey, DataRow dr, bool bUpdateCommon)
        //{
        //    return GetUpdateSql(sTableName, sPrimaryKey, "", "", dr, bUpdateCommon);
        //}

        //public static string GetUpdateSql(string sTableName, string sPrimaryKey1, string sPrimaryKey2, DataRow dr)
        //{
        //    return GetUpdateSql(sTableName, sPrimaryKey1, sPrimaryKey2, "", dr, true);
        //}

        //public static string GetUpdateSql(string sTableName, string sPrimaryKey1, string sPrimaryKey2, string sPrimaryKey3, DataRow dr)
        //{
        //    return GetUpdateSql(sTableName, sPrimaryKey1, sPrimaryKey2, sPrimaryKey3, dr, true);
        //}

        //public static string GetUpdateSql(string sTableName, string sPrimaryKey1, string sPrimaryKey2, string sPrimaryKey3, DataRow dr, bool bUpdateCommon)
        //{
        //    if ((dr == null) || (null == dr.Table))
        //    {
        //        return "";
        //    }
        //    dr.EndEdit();
        //    if (dr.RowState == DataRowState.Unchanged)
        //    {
        //        return "";
        //    }
        //    DataTable tableSet = TableCache.GetTableSet(sTableName);
        //    StringBuilder builder = new StringBuilder(10);
        //    string str = "    UPDATE " + sTableName + " SET ";
        //    builder.Append(str);
        //    foreach (DataColumn column in tableSet.Columns)
        //    {
        //        if (((ConvertUtil.DBCompare(column.ColumnName, sPrimaryKey1) || ConvertUtil.DBCompare(column.ColumnName, sPrimaryKey2)) || ConvertUtil.DBCompare(column.ColumnName, sPrimaryKey3)) || !dr.Table.Columns.Contains(column.ColumnName))
        //        {
        //            continue;
        //        }
        //        if (ConvertUtil.DBCompare(column.ColumnName, "ModifyDate") && bUpdateCommon)
        //        {
        //            dr[column.ColumnName] = ConvertUtil.GetNowLongDateString();
        //        }
        //        if (ConvertUtil.DBCompare(column.ColumnName, "ModifyUser") && bUpdateCommon)
        //        {
        //            dr[column.ColumnName] = LoginUserUtil.Instance.UserName;
        //        }
        //        if ((string.Compare(column.ColumnName, "TimeRemark", true) == 0) && bUpdateCommon)
        //        {
        //            dr[column.ColumnName] = DateTime.Now.Ticks.ToString();
        //        }
        //        if (!dr[column.ColumnName, DataRowVersion.Current].Equals(dr[column.ColumnName, DataRowVersion.Original]))
        //        {
        //            if (dr[column.ColumnName] == DBNull.Value)
        //            {
        //                builder.Append(column.ColumnName + "= null , ");
        //                continue;
        //            }
        //            if (column.DataType.Equals(typeof(byte[])))
        //            {
        //                StringBuilder builder2 = null;
        //                byte[] buffer = (byte[])dr[column.ColumnName];
        //                if (buffer != null)
        //                {
        //                    builder2 = new StringBuilder("0x", (buffer.Length * 2) + 2);
        //                    foreach (byte num in buffer)
        //                    {
        //                        builder2.AppendFormat("{0:X2}", num);
        //                    }
        //                }
        //                if (builder2 != null)
        //                {
        //                    builder.Append(column.ColumnName + "=" + builder2.ToString() + ", ");
        //                }
        //                else
        //                {
        //                    builder.Append(column.ColumnName + " = null, ");
        //                }
        //                continue;
        //            }
        //            if (column.DataType == typeof(DateTime))
        //            {
        //                DateTime dt = Convert.ToDateTime(dr[column.ColumnName]);
        //                builder.Append(column.ColumnName + "='" + ConvertUtil.GetLongDateString(dt) + "', ");
        //            }
        //            else
        //            {
        //                builder.Append(column.ColumnName + "='" + dr[column.ColumnName].ToString().Trim().Replace("'", "''") + "', ");
        //            }
        //        }
        //    }
        //    if (str == builder.ToString())
        //    {
        //        return "";
        //    }
        //    builder.Remove(builder.Length - 2, 2);
        //    builder.Append(" WHERE " + sPrimaryKey1 + " ='" + dr[sPrimaryKey1].ToString().Trim() + "'");
        //    if (ConvertUtil.GetDBString(sPrimaryKey2) != "")
        //    {
        //        builder.Append(" AND " + sPrimaryKey2 + " ='" + dr[sPrimaryKey2].ToString().Trim() + "'");
        //    }
        //    if (ConvertUtil.GetDBString(sPrimaryKey3) != "")
        //    {
        //        builder.Append(" AND " + sPrimaryKey3 + " ='" + dr[sPrimaryKey3].ToString().Trim() + "'");
        //    }
        //    if ((dr.Table.Columns.Contains("TimeRemark") && tableSet.Columns.Contains("TimeRemark")) && bUpdateCommon)
        //    {
        //        builder.Append(" AND TimeRemark ='" + dr["TimeRemark", DataRowVersion.Original] + "'");
        //    }
        //    return builder.ToString();
        //}

        public static string ConnString
        {
            get
            {
                return _connString;
            }
            set
            {
                _connString = value;
                if (_linker != null)
                {
                    _linker.ConnString = _connString;
                }
            }
        }

        public static SqlLinker Linker
        {
            get
            {
                if (null == _linker)
                {
                    _linker = new SqlLinker(_connString);
                }
                return _linker;
            }
        }
    }
}


