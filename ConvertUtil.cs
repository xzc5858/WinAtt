using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAtt
{
    /// <summary>
    /// 格式化的类
    /// </summary>
    public class ConvertUtil
    {
        public static int CompareDate(DateTime dtDateTime, string sdtMin)
        {
            return string.Compare(dtDateTime.ToString("yyyy-MM-dd"), sdtMin, true);
        }

        public static int CompareDate(object objDateTime, string sdtMin)
        {
            return string.Compare(Convert.ToDateTime(objDateTime).ToString("yyyy-MM-dd"), sdtMin, true);
        }

        public static bool DBCompare(object objA, object objB)
        {
            if ((objA == null) && (null == objB))
            {
                return true;
            }
            if ((objA is DBNull) && (objB is DBNull))
            {
                return true;
            }
            if ((objA == null) || (null == objB))
            {
                return false;
            }
            return (string.Compare(objA.ToString(), objB.ToString(), true) == 0);
        }

        public static DateTime GetDateTime(string sDateTime)
        {
            try
            {
                return Convert.ToDateTime(sDateTime);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static bool GetDBBool(object oValue)
        {
            if (((oValue == null) || (oValue is DBNull)) || (oValue.ToString() == ""))
            {
                return false;
            }
            return (oValue.ToString() == "1");
        }

        public static bool GetDBBool(string sValue)
        {
            return (sValue == "1");
        }

        public static string GetDBBoolString(bool bValue)
        {
            if (bValue)
            {
                return "1";
            }
            return "0";
        }

        public static string GetDBBoolString(object oValue)
        {
            if (((oValue != null) && (oValue.ToString() != "")) && ((oValue.ToString().ToUpper() == "TRUE") || (oValue.ToString() == "1")))
            {
                return "1";
            }
            return "0";
        }

        public static DateTime GetDBDate(object oDate)
        {
            try
            {
                return Convert.ToDateTime(oDate);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static decimal GetDBDecimal(object obj)
        {
            if (((obj == null) || (obj is DBNull)) || (obj.ToString() == ""))
            {
                return 0M;
            }
            return Convert.ToDecimal(obj);
        }

        public static decimal GetDBDecimal2(object obj)
        {
            if (((obj == null) || (obj is DBNull)) || (obj.ToString() == ""))
            {
                return 0M;
            }
            return decimal.Round(Convert.ToDecimal(obj), 2);
        }

        public static int GetDBInt(bool bCheck)
        {
            return Convert.ToInt16(bCheck);
        }

        public static int GetDBInt(object obj)
        {
            if (((obj == null) || (obj is DBNull)) || (obj.ToString() == ""))
            {
                return 0;
            }
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return 0;
            }
        }
        public static double GetDBDouble(object obj)
        {
            if (((obj == null) || (obj is DBNull)) || (obj.ToString() == ""))
            {
                return 0;
            }
            try
            {
                return Convert.ToDouble(obj);
            }
            catch
            {
                return 0;
            }

        }
        public static object GetDBIntORNull(object obj)
        {
            if (((obj == null) || (obj is DBNull)) || (obj.ToString() == ""))
            {
                return DBNull.Value;
            }
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return DBNull.Value;
            }
        }

        public static long GetDBLong(object obj)
        {
            if (((obj == null) || (obj is DBNull)) || (obj.ToString() == ""))
            {
                return 0L;
            }
            try
            {
                return Convert.ToInt64(obj);
            }
            catch
            {
                return 0L;
            }
        }

        public static object GetDBObject(object obj)
        {
            if ((obj == null) || (obj is DBNull))
            {
                return null;
            }
            return obj;
        }

        public static short GetDBShort(object obj)
        {
            if (((obj == null) || (obj is DBNull)) || (obj.ToString() == ""))
            {
                return 0;
            }
            try
            {
                return Convert.ToInt16(obj);
            }
            catch
            {
                return 0;
            }
        }

        public static string GetDBString(object obj)
        {
            if ((obj == null) || (obj is DBNull))
            {
                return "";
            }
            return obj.ToString();
        }

        public static string GetLongDateString(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetNowLongDateString()
        {
            return DBNow.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetNowShortDateString()
        {
            return DBNow.ToString("yyyy-MM-dd");
        }

        public static string GetShortDateString(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }

        public static string GetShortDateString(object objDate)
        {
            DateTime minValue = DateTime.MinValue;
            try
            {
                minValue = Convert.ToDateTime(objDate);
            }
            catch
            {
            }
            return minValue.ToString("yyyy-MM-dd");
        }

        public static bool IsDBDateNull(object obj)
        {
            if ((obj == null) || (obj is DBNull))
            {
                return true;
            }
            DateTime minValue = DateTime.MinValue;
            try
            {
                minValue = Convert.ToDateTime(obj);
            }
            catch
            {
                return true;
            }
            return (minValue == DateTime.MinValue);
        }

        public static bool IsDBNull(object obj)
        {
            return ((obj == null) || (obj is DBNull));
        }

        public static bool IsNumeric(string sValue)
        {
            try
            {
                decimal.Parse(sValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //public static bool IsNumericByRegex(string sValue)
        //{
        //    Regex regex = new Regex("^d+$");
        //    return regex.Match(sValue).Success;
        //}

        public static DateTime DBNow
        {
            get
            {
                return DBLinker.GetDBCurrentDate();
            }
        }
    }
}
