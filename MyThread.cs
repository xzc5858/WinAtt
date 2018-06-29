using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using System.Threading.Tasks;
using System.Threading;
//using System.Diagnostics;
using System.Windows.Forms;
using System.Data;
namespace WinAtt
{
    public class MyThread
    {
        //ContainerControl m_sender = null;
        //Delegate m_senderDelegate = null;
        DataTable dt;
        public MyThread()
        {
            //attdt = new DataTable();
            //attdt.Columns.Add(new DataColumn("EnrollNumber", typeof(string)));
            //attdt.Columns.Add(new DataColumn("VerifyMethod", typeof(int)));
            //attdt.Columns.Add(new DataColumn("InOutMode", typeof(int)));
            //attdt.Columns.Add(new DataColumn("date", typeof(DateTime)));
            //attdt.Columns.Add(new DataColumn("WorkCode", typeof(int)));
        }
        public void RunProcess(object obj)
        {
            Thread.CurrentThread.IsBackground = true; //make them a daemon
            //DataTable dt = DBLinker.Linker.ExecuteDataTable("SELECT *  FROM [d_Machines]");
            dt = (DataTable)obj;
            //m_senderDelegate = (System.Delegate)objArray[1];

            ////dt = (DataTable)obj;
            //dr = (DataRow)obj;
            LocalRunProcess();
        }

        void LocalRunProcess()
        {
            foreach (DataRow dr in dt.Rows)
            {
                ZKAccess FG = new ZKAccess(Convert.ToInt32(dr["SerialPort"]), Convert.ToInt32(dr["Port"]), Convert.ToInt32(dr["MachineNumber"]), Convert.ToInt32(dr["Baudrate"]), dr["IP"].ToString());
                if (FG.Connect_Net())
                {
                    DBLinker.Linker.ExecuteNonQuery("UPDATE d_machines SET  ConnectType = 1 WHERE ID=" + dr["ID"].ToString());
                    FG.m_CZKEMClass.EnableDevice(FG.COM_MACHINENUMBER, false);
                    int idwErrorCode = 0;
                    string sdwEnrollNumber = "";
                    int idwVerifyMode = 0;
                    int idwInOutMode = 0;
                    int idwYear = 0;
                    int idwMonth = 0;
                    int idwDay = 0;
                    int idwHour = 0;
                    int idwMinute = 0;
                    int idwSecond = 0;
                    int idwWorkcode = 0;
                    int y = 0;
                    FG.m_CZKEMClass.EnableDevice(FG.COM_MACHINENUMBER, false);//disable the device
                    if (FG.m_CZKEMClass.ReadGeneralLogData(FG.COM_MACHINENUMBER))//read all the attendance records to the memory
                    {
                        while (FG.m_CZKEMClass.SSR_GetGeneralLogData(FG.COM_MACHINENUMBER, out sdwEnrollNumber, out idwVerifyMode,
                                    out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                        {
                            string t = idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString();
                            DataRow sqlDt = DBLinker.Linker.GetDataRow("SELECT * FROM  d_atttransaction where  EnrollNumber='" + sdwEnrollNumber + "' and date='" + t + "'");
                            if (sqlDt == null)
                            {
                                string sql = "INSERT INTO d_atttransaction (EnrollNumber ,VerifyMethod ,InOutMode ,date ,WorkCode,MachineID) VALUES ('" + sdwEnrollNumber + "'," + idwVerifyMode + "," + idwInOutMode + ",'" + t + "'," + idwWorkcode + "," + FG.COM_MACHINENUMBER.ToString() + ")";
                                DBLinker.Linker.ExecuteNonQuery(sql);
                                y++;
                            }
                        }

                        DBLinker.Linker.ExecuteNonQuery("INSERT INTO d_machinesstate (sn,zt ,sj) VALUES  ('" + FG.COM_MACHINENUMBER.ToString() + "','下载完成',getdate())");
                        FG.m_CZKEMClass.ClearGLog(FG.COM_MACHINENUMBER);
                        FG.m_CZKEMClass.RefreshData(FG.COM_MACHINENUMBER);
                    }
                    else
                    {
                        FG.m_CZKEMClass.GetLastError(ref idwErrorCode);
                        DBLinker.Linker.ExecuteNonQuery("INSERT INTO d_machinesstate (sn,zt ,sj) VALUES  ('" + FG.COM_MACHINENUMBER.ToString() + "','下载失败',getdate())");
                    }


                    FG.m_CZKEMClass.EnableDevice(FG.COM_MACHINENUMBER, true);
                }
                else
                {
                    FG.m_CZKEMClass.Disconnect();
                    DBLinker.Linker.ExecuteNonQuery("INSERT INTO d_machinesstate (sn,zt ,sj) VALUES  ('" + FG.COM_MACHINENUMBER.ToString() + "','连接失败',getdate())");
                    DBLinker.Linker.ExecuteNonQuery("UPDATE d_machines SET  ConnectType = 0 WHERE ID=" + dr["ID"].ToString());
                }

            }
        }


    }



}

