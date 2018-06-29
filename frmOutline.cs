using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace WinAtt
{
    public partial class frmOutline : Form
    {
        ZKAccess FG = ZKAccess.Instance;
        DataTable dv;
        private delegate void CmnXhLoadHandler(out double b);
        private delegate void CmnZDLoadDataHandler(DataRow dr, out DataTable dt,out string cback);
        public DataTable attdt;
        public frmOutline()
        {
            InitializeComponent();
            attdt = new DataTable();
            attdt.Columns.Add(new DataColumn("EnrollNumber", typeof(string)));
            attdt.Columns.Add(new DataColumn("VerifyMethod", typeof(int)));
            attdt.Columns.Add(new DataColumn("InOutMode", typeof(int)));
            attdt.Columns.Add(new DataColumn("date", typeof(DateTime)));
            attdt.Columns.Add(new DataColumn("WorkCode", typeof(int)));
        }

        private void frmOutline_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“attDataSet.d_machines”中。您可以根据需要移动或删除它。
            this.d_machinesTableAdapter.Fill(this.attDataSet.d_machines);

        }
        /// <summary>
        /// 下载指定主机数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null)
            {
                CmnZDLoadDataHandler handler = new CmnZDLoadDataHandler(ZDCmnDownLoadData);

                using (CmnLoadDataForm form = new CmnLoadDataForm("下载数据中..."))
                {
                    for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        DataRow dv = (dataGridView1.SelectedRows[i].DataBoundItem as DataRowView).Row;
                        DataTable dt = (DataTable)attdt.Clone();
                        string str = "";
                        listBox1.Items.Add("开始连接指纹机" + dv["MachineNumber"].ToString());
                        IAsyncResult result = handler.BeginInvoke(dv, out dt,out str, null, null);
                        form.AsyncResult = result;
                        form.ShowDialog(this);
                        handler.EndInvoke(out dt,out str, result);
                        listBox1.Items.Add(str);
                    }
                }

            }
        }
        /// <summary>
        /// 循环下载所有数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                timer1.Start();
                CmnXhLoadHandler handler = new CmnXhLoadHandler(XHCmnDownLoadData);
                using (CmnLoadDataForm form = new CmnLoadDataForm("循环下载数据正在运行中..."))
                {
                    double b;
                    IAsyncResult result = handler.BeginInvoke(out b, null, null);
                    form.AsyncResult = result;
                    form.ShowDialog(this);
                    handler.EndInvoke(out b, result);
                }
            }
        }


        /// <summary>
        /// 循环下载数据
        /// </summary>
        /// <param name="b"></param>
        public void XHCmnDownLoadData(out double b)
        {
            TimeSpan start = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan end = new TimeSpan(DateTime.Now.AddYears(1).Ticks);
            TimeSpan span = end.Subtract(start).Duration();
            b = span.TotalSeconds;
            while (true)
            {

            }
        }

        /// <summary>
        /// 指定下载
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="dt"></param>
        public void ZDCmnDownLoadData(DataRow dv, out DataTable dt,out string callback)
        {
            dt = (DataTable)attdt.Clone();
            callback = "";
            ZKAccess FG = new ZKAccess(Convert.ToInt32(dv["SerialPort"]), Convert.ToInt32(dv["Port"]), Convert.ToInt32(dv["MachineNumber"]), Convert.ToInt32(dv["Baudrate"]), dv["IP"].ToString());

            if (FG.Connect_Net())
            {

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

                FG.m_CZKEMClass.EnableDevice(FG.COM_MACHINENUMBER, false);//disable the device
                if (FG.m_CZKEMClass.ReadGeneralLogData(FG.COM_MACHINENUMBER))//read all the attendance records to the memory
                {
                    while (FG.m_CZKEMClass.SSR_GetGeneralLogData(FG.COM_MACHINENUMBER, out sdwEnrollNumber, out idwVerifyMode,
                                out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                    {
                        string t = idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString();
                        string sql = "SELECT * FROM d_atttransaction where [EnrollNumber]='" + sdwEnrollNumber + "' and [date]='" + t + "'";
                        DataRow ds = DBLinker.Linker.GetDataRow(sql);
                        //DataRow sqlDr =SQLHelper.GetDataSet(SQLHelper.Conn, CommandType.Text,sql,null).Tables[0].DefaultView[0].Row;
                        if (ds == null)
                        {
                            //DataRow ndr = dt.NewRow();
                            
                            //ndr["EnrollNumber"] = sdwEnrollNumber;
                            //ndr["VerifyMethod"] = idwVerifyMode;
                            //ndr["InOutMode"] = idwInOutMode;
                            //ndr["date"] = t;
                            //ndr["WorkCode"] = idwWorkcode;
                            //dt.Rows.Add(ndr);
                            sql = "INSERT INTO d_atttransaction (EnrollNumber,VerifyMethod ,InOutMode ,date ,WorkCode,MachineID) VALUES ('" + sdwEnrollNumber + "'," + idwVerifyMode + "," + idwInOutMode + ",'" + t + "'," + idwWorkcode + "," + FG.COM_MACHINENUMBER.ToString() + ")";
                            DBLinker.Linker.ExecuteNonQuery(sql);

                        }


                    }
                    //DBLinker.Linker.ExecuteNonQuery("INSERT INTO d_machinesstate (sn,zt ,sj) VALUES  ('" + FG.COM_MACHINENUMBER.ToString() + "','下载完成',getdate())");
                    string sql2 = "INSERT INTO [d_MachinesState] ([sn],[zt] ,[sj]) VALUES  ('" + FG.COM_MACHINENUMBER.ToString() + "','下载完成',getdate())";
                    DBLinker.Linker.ExecuteNonQuery(sql2);

                }
                else
                {
                    //Cursor = Cursors.Default;
                    FG.m_CZKEMClass.GetLastError(ref idwErrorCode);

                    if (idwErrorCode != 0)
                    {
                        MessageBox.Show("读取终端数据错误，代码：" + idwErrorCode.ToString());
                        callback = ("读取终端数据错误，代码：" + idwErrorCode.ToString());
                        //listBox1.Items.Add("读取终端数据错误，代码：" + idwErrorCode.ToString());
                    }
                    else
                    {
                        MessageBox.Show("终端没有任何数据");
                        callback = "终端没有任何数据";
                        //listBox1.Items.Add("终端没有任何数据");
                    }
                    DBLinker.Linker.ExecuteNonQuery("INSERT INTO [d_MachinesState] ([sn],[zt] ,[sj]) VALUES  ('" + FG.COM_MACHINENUMBER.ToString() + "','下载失败',getdate())");
                }

                FG.m_CZKEMClass.ClearGLog(FG.COM_MACHINENUMBER);
                FG.m_CZKEMClass.RefreshData(FG.COM_MACHINENUMBER);
                FG.m_CZKEMClass.EnableDevice(FG.COM_MACHINENUMBER, true);

            }
            else
            {
                MessageBox.Show("连接机器失败");
                callback = ("连接机器失败");
                FG.m_CZKEMClass.Disconnect();
                DBLinker.Linker.ExecuteNonQuery("INSERT INTO [d_MachinesState] ([sn],[zt] ,[sj]) VALUES  ('" + FG.COM_MACHINENUMBER.ToString() + "','连接失败',getdate())");
                //SQLHelper.ExecuteNonQuery("UPDATE [d_Machines] SET  [ConnectType] = 0 WHERE ID=" + dr["ID"].ToString());
            }
        }

        /// <summary>
        /// 月/1;周/2;日/3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            DataRow dr = DBLinker.Linker.GetDataRow("SELECT * FROM d_system");
            bool start = false;
            DateTime t, n;
            t = n = DateTime.Now;

            switch (dr["updatefrequency"].ToString())
            {
                case "1":
                    DateTime m = Convert.ToDateTime(t.ToString("yyyy-MM-") + Convert.ToDateTime(dr["updateDate"]).ToString("dd HH:mm:ss"));
                    if (DateTime.Compare(m, n) <= 0)
                    {
                        start = true;
                    }
                    break;
                case "2":
                    DayOfWeek wk = Convert.ToDateTime(dr["updateDate"]).DayOfWeek;
                    DayOfWeek nwk = t.DayOfWeek;
                    if (wk == nwk)
                    {
                        DateTime dday = Convert.ToDateTime(t.ToString("yyyy-MM-dd") + " " + Convert.ToDateTime(dr["updateDate"]).ToString("HH:mm:ss"));
                        if (DateTime.Compare(dday, n) <= 0)
                        {
                            start = true;
                        }
                    }

                    break;
                case "3":
                    DateTime d = Convert.ToDateTime(t.ToString("yyyy-MM-dd") + " " + Convert.ToDateTime(dr["updateDate"]).ToString("HH:mm:ss"));
                    //TimeSpan ts = d.Subtract(n).Duration();
                    //string aa = d.ToString();
                    //string bb = n.ToString();
                    //string ss = ts.Seconds.ToString();
                    if (n >= d)
                    {
                        start = true;
                    }
                    break;
            }
            /////可以下载，
            if (start)
            {
                ///是否正在下载，不然会多台机子下载。
                bool DownloadIng = Convert.ToBoolean(dr["DownloadIng"]);
                ///假如今天下载，是否已经完成。不然会反复地下载。
                bool complete = Convert.ToBoolean(dr["complete"]);
                

                if (!(DownloadIng || complete))
                {

                    DBLinker.Linker.ExecuteNonQuery("UPDATE d_system set DownloadIng=1");
                    timer1.Stop();
                    MyThread wc = new MyThread();
                    DataTable dv = DBLinker.Linker.ExecuteDataTable("SELECT *  FROM [d_Machines]");
                    bool rc = ThreadPool.QueueUserWorkItem(new WaitCallback(wc.RunProcess), dv);
                    if (rc)
                    {
                        DBLinker.Linker.ExecuteNonQuery("UPDATE d_system set complete=1,DownloadIng=0,DownloadTime=GETDATE()");
                        this.timer1.Start();
                    }

                }
                else
                {
                    //callback = "等待下载中";
                    //listBox1.Items.Add("等待下载中");
                }
            }
            else
            {
                DBLinker.Linker.ExecuteNonQuery("UPDATE d_system set complete=0");
                listBox1.Items.Add("下载完成");

            }
            //如果只是这个时间可以下载，但是不符合以上两个条件，也不能下载。
        }
    }
}
