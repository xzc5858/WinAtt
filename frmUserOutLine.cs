using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinAtt
{
    public partial class frmUserOutLine : Form
    {
        public frmUserOutLine()
        {
            InitializeComponent();
            userdata = new DataTable();
            userdata.Columns.Add(new DataColumn("EnrollNumber", typeof(string)));
            userdata.Columns.Add(new DataColumn("UserName", typeof(string)));
            userdata.Columns.Add(new DataColumn("Privilege", typeof(int)));
            userdata.Columns.Add(new DataColumn("MPassword", typeof(string)));
            userdata.Columns.Add(new DataColumn("IsEnabled", typeof(int)));
            DataGridViewTextBoxColumn t1 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn t2 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn t3 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn t4 = new DataGridViewTextBoxColumn();
            //DataGridViewTextBoxColumn t5 = new DataGridViewTextBoxColumn();
            DataGridViewCheckBoxColumn t5 = new DataGridViewCheckBoxColumn();
            t1.HeaderText = "用户";
            t1.DataPropertyName = "UserName";
            t1.Name = "userDataGridViewTextBoxColumn";

            t2.HeaderText = "考勤号";
            t2.DataPropertyName = "EnrollNumber";
            t2.Name = "EnrollNumberDataGridViewTextBoxColumn";

            t3.HeaderText = "编号";
            t3.DataPropertyName = "Privilege";
            t3.Name = "PrivilegeDataGridViewTextBoxColumn";

            t4.HeaderText = "密码";
            t4.DataPropertyName = "MPassword";
            t4.Name = "MPasswordDataGridViewTextBoxColumn";

            t5.HeaderText = "密码";
            t5.DataPropertyName = "IsEnabled";
            t5.Name = "IsEnabledDataGridViewTextBoxColumn";

            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
               t1,t2,t3,t4,t5
            });
        }
        private delegate void CmnLoadDataHandler(DataRow dr, out string b);
        private delegate void CmnloadMdataHandler(DataRow dr, out DataTable dt, out string st);
        private DataTable userdata, machinesdata;
        private void frmUserOutLine_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“attDataSet.d_machines”中。您可以根据需要移动或删除它。
            this.d_machinesTableAdapter.Fill(this.attDataSet.d_machines);
            // TODO: 这行代码将数据加载到表“users.d_user”中。您可以根据需要移动或删除它。
            this.d_userTableAdapter.Fill(this.users.d_user);

        }
        /// <summary>
        /// 上传用户数据到机器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                CmnLoadDataHandler handler = new CmnLoadDataHandler(this.CmnUpLoadData);
                using (CmnLoadDataForm form = new CmnLoadDataForm())
                    for (int i = 0; i < dataGridView2.SelectedRows.Count; i++)
                    {

                    }

            }
            //for (int i = 0; i < LGridView.RowCount; i++)
            //{
            //    if (LGridView.GetDataRow(i)["xuanding"].ToString() == "1")
            //    {
            //        this.gridView.CloseSaveEditor();
            //        CmnLoadDataHandler handler = new CmnLoadDataHandler(this.CmnUpLoadData);
            //        using (CmnLoadDataForm form = new CmnLoadDataForm())
            //        {
            //            string b = "";
            //            DataRow dr = LGridView.GetDataRow(i);
            //            IAsyncResult result = handler.BeginInvoke(dr, out b, null, null);
            //            form.AsyncResult = result;
            //            form.ShowDialog(this);
            //            handler.EndInvoke(out b, result);
            //            DXTravel.DXGUI.Util.MessageUtil.ShowInfo(b);
            //            RTable.GetMainTableByWhere(" AllowLogin=1");
            //            gridControl.DataSource = RTable;

            //        }

            //    }
            //}

        }
        /// 上传用户数据到机器
        private void CmnUpLoadData(DataRow dr, out string b)
        {
            b = "";
            ZKAccess FG = new ZKAccess(ConvertUtil.GetDBInt(dr["SerialPort"]), ConvertUtil.GetDBInt(dr["Port"]), ConvertUtil.GetDBInt(dr["MachineNumber"]), ConvertUtil.GetDBInt(dr["Baudrate"]), dr["IP"].ToString());
            if (FG.Connect_Net())
            {
                int idwErrorCode = 0;
                string sdwEnrollNumber = "";
                string sName = "";
                int idwFingerIndex = 0;
                string sTmpData = "";
                ///权限级别0:一般用户1:登记员2:管理员3:超级管理员
                int iPrivilege = 0;
                string sPassword = "";
                string sEnabled = "";
                bool bEnabled = true;
                int iFlag = 1;
                int iFaceLength = 128 * 1024;
                int iFaceIndex = 50;
                byte[] sFaceData = new byte[iFaceLength];

                int iUpdateFlag = 1;

                FG.m_CZKEMClass.EnableDevice(FG.COM_MACHINENUMBER, false);
                //    if (FG.m_CZKEMClass.BeginBatchUpdate(FG.COM_MACHINENUMBER, iUpdateFlag))//create memory space for batching data
                //    {
                //        string sLastEnrollNumber = "";//the former enrollnumber you have upload(define original value as 0)
                //        for (int i = 0; i < gridView.RowCount; i++)
                //        {
                //            DataRow newdr = gridView.GetDataRow(i);
                //            if (newdr["uploaded"].ToString() == "1")
                //            {
                //                sdwEnrollNumber = newdr["EnrollNumber"].ToString();
                //                sName = newdr["UserName"].ToString();
                //                idwFingerIndex = ConvertUtil.GetDBInt(newdr["FingerIndex"]);
                //                sTmpData = newdr["Finger"].ToString();
                //                iPrivilege = ConvertUtil.GetDBInt(newdr["Privilege"]);
                //                sPassword = newdr["MPassword"].ToString();
                //                sEnabled = newdr["IsEnabled"].ToString();
                //                iFlag = ConvertUtil.GetDBInt(newdr["Flag"]);
                //                iFaceLength = ConvertUtil.GetDBInt(newdr["FingerLength"]);

                //                sFaceData = (byte[])newdr["photo"];
                //                if (sEnabled == "true")
                //                {
                //                    bEnabled = true;
                //                }
                //                else
                //                {
                //                    bEnabled = false;
                //                }
                //                if (sdwEnrollNumber != sLastEnrollNumber)//identify whether the user information(except fingerprint templates) has been uploaded
                //                {
                //                    if (FG.m_CZKEMClass.SSR_SetUserInfo(FG.COM_MACHINENUMBER, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled))//upload user information to the memory
                //                    {
                //                        FG.m_CZKEMClass.SetUserTmpExStr(FG.COM_MACHINENUMBER, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData);//upload templates information to the memory
                //                    }
                //                    else
                //                    {
                //                        FG.m_CZKEMClass.GetLastError(ref idwErrorCode);

                //                        FG.m_CZKEMClass.EnableDevice(FG.COM_MACHINENUMBER, true);
                //                        return;
                //                    }

                //                }
                //                else//the current fingerprint and the former one belongs the same user,that is ,one user has more than one template
                //                {
                //                    FG.m_CZKEMClass.SetUserTmpExStr(FG.COM_MACHINENUMBER, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData);
                //                }
                //                //更新照片模版。先不要用，免得出错，丢失数据。试验能够成功再使用。
                //                //FG.m_CZKEMClass.SetUserFace(FG.COM_MACHINENUMBER, sdwEnrollNumber, 50,ref  sFaceData[0], iFaceLength);
                //                sLastEnrollNumber = sdwEnrollNumber;//change the value of iLastEnrollNumber dynamicly
                //            }
                //        }
                //    }
                //    FG.m_CZKEMClass.BatchUpdate(FG.COM_MACHINENUMBER);//upload all the information in the memory
                //    FG.m_CZKEMClass.RefreshData(FG.COM_MACHINENUMBER);//the data in the device should be refreshed
                //    FG.m_CZKEMClass.EnableDevice(FG.COM_MACHINENUMBER, true);
                //    b = "上传完成";
            }
            else
            {
                listBox1.Items.Add(dr["MachineNumber"].ToString() + "指纹机无法连机");
            }

        }
        /// <summary>
        /// 下载用户数据到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    //if (userdata.Rows[i]["xuanding"].ToString() == "1")
                    //{
                    CmnLoadDataHandler handler = new CmnLoadDataHandler(this.CmnDownLoadData);
                    using (CmnLoadDataForm form = new CmnLoadDataForm())
                    {
                        string b = "";
                        DataRow dr = (dataGridView1.SelectedRows[i].DataBoundItem as DataRowView).Row;
                        listBox1.Items.Add("开始连接指纹机" + dr["MachineNumber"].ToString());
                        IAsyncResult result = handler.BeginInvoke(dr, out b, null, null);
                        form.AsyncResult = result;
                        form.ShowDialog(this);
                        handler.EndInvoke(out b, result);
                        listBox1.Items.Add(b);
                        //DXTravel.DXGUI.Util.MessageUtil.ShowInfo(b);
                        //RTable.GetMainTableByWhere(" AllowLogin=1");
                        //dataGridView3.DataSource = userdata;

                    }

                }
            }
        }

        /// <summary>
        /// 下载更新数据库的用户数据
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="b"></param>
        protected void CmnDownLoadData(DataRow dr, out string b)
        {
            int count = 0;
            int sumcount = 0;
            string rtstr = "";
            b = "";
            ZKAccess FG = new ZKAccess(ConvertUtil.GetDBInt(dr["SerialPort"]), ConvertUtil.GetDBInt(dr["Port"]), ConvertUtil.GetDBInt(dr["MachineNumber"]), ConvertUtil.GetDBInt(dr["Baudrate"]), dr["IP"].ToString());
            if (FG.Connect_Net())
            {
                b = dr["MachineNumber"].ToString() + "指纹机连接成功";
                string sdwEnrollNumber = "";
                string sName = "";
                string sPassword = "";
                int iPrivilege = 0;
                bool bEnabled = false;

                int idwFingerIndex;
                string sTmpData = "";

                int iTmpLength = 0;
                int iFlag = 0;


                int iFaceLength = 128 * 1024;
                int iFaceIndex = 50;
                byte[] sFaceData = new byte[iFaceLength];
                FG.m_CZKEMClass.EnableDevice(FG.COM_MACHINENUMBER, false);
                //Cursor = Cursors.WaitCursor;

                FG.m_CZKEMClass.ReadAllUserID(FG.COM_MACHINENUMBER);//read all the user information to the memory
                FG.m_CZKEMClass.ReadAllTemplate(FG.COM_MACHINENUMBER);//read all the users' fingerprint templates to the memory
                while (FG.m_CZKEMClass.SSR_GetAllUserInfo(FG.COM_MACHINENUMBER, out sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
                {

                    sumcount = sumcount + 1;
                    //if (FG.m_CZKEMClass.GetUserFaceStr(FG.COM_MACHINENUMBER, sdwEnrollNumber, iFaceIndex, ref sFaceData, ref iFaceLength))//get the face templates from the memory
                    //{

                    //}
                    if (FG.m_CZKEMClass.GetUserFace(FG.COM_MACHINENUMBER, sdwEnrollNumber, iFaceIndex, ref sFaceData[0], ref iFaceLength))
                    {

                    }
                    for (idwFingerIndex = 0; idwFingerIndex < 10; idwFingerIndex++)
                    {
                        if (FG.m_CZKEMClass.GetUserTmpExStr(FG.COM_MACHINENUMBER, sdwEnrollNumber, idwFingerIndex, out iFlag, out sTmpData, out iTmpLength))//get the corresponding templates string and length from the memory
                        {

                            if (DBLinker.Linker.GetDataRow("select 1 from d_user where EnrollNumber='" + sdwEnrollNumber.ToString() + "'") == null)
                            {
                                string sql = "insert into [d_user] ([LoginName],[UserName],[UserPassword] ,[AllowLogin],[MachineNumber] ,[EnrollNumber] ,[FingerIndex] ,[FaceIndex],[Finger] ,[photo],[Flag] ,[MPassword] ,[Privilege] ,[IsEnabled],FingerLength,FaceLength) VALUES ('" + sdwEnrollNumber + "','" + sName + "','r3B6n6Eq5z8=' ,1," + FG.COM_MACHINENUMBER.ToString() + " ,'" + sdwEnrollNumber + "' ," + idwFingerIndex + " ," + iFaceIndex + ",'" + sTmpData + "','" + sFaceData + "'," + iFlag.ToString() + " ,'" + sPassword + "' ," + iPrivilege.ToString() + " ,'" + bEnabled + "'," + iTmpLength + "," + iFaceLength + ")";
                                if (DBLinker.Linker.ExecuteNonQuery(sql) != -1)
                                    count = count + 1;
                                else
                                    rtstr = rtstr + sql + "\r\n";
                            }
                            else
                            {
                                string sql = "UPDATE [d_user] SET [MachineNumber] =" + FG.COM_MACHINENUMBER.ToString() + ",[FingerIndex] = " + idwFingerIndex + ",[FaceIndex] = " + iFaceIndex + " ,[Finger] =  '" + sTmpData + "',[photo] = '" + sFaceData + "' ,[Flag] =  " + iFlag.ToString() + " ,[MPassword] ='" + sPassword + "' ,[Privilege] =" + iPrivilege.ToString() + " ,[IsEnabled] =  '" + bEnabled + "',[FingerLength] = " + iTmpLength + " ,[FaceLength] = " + iFaceLength + " where  [EnrollNumber]='" + sdwEnrollNumber + "'";
                                if (DBLinker.Linker.ExecuteNonQuery(sql) != -1)
                                    count = count + 1;
                                else
                                    rtstr = rtstr + sql + "\r\n";
                            }

                        }
                    }
                }

                FG.m_CZKEMClass.EnableDevice(FG.COM_MACHINENUMBER, true);

                b = ("下载或更新用户信息" + count.ToString() + "条,合计" + sumcount.ToString() + "条，错误提示：\r\n　" + rtstr);


            }
            else
            {
                b = dr["MachineNumber"].ToString() + "指纹机连接失败";
            }

        }
        /// <summary>
        /// 查看机器人员列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {

                CmnloadMdataHandler handler = new CmnloadMdataHandler(this.CmnLookData);
                using (CmnLoadDataForm form = new CmnLoadDataForm())
                {
                    for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        DataRow dr = (dataGridView1.SelectedRows[i].DataBoundItem as DataRowView).Row;
                        listBox1.Items.Add("开始连接指纹机" + dr["MachineNumber"].ToString());
                        DataTable d = (DataTable)userdata.Clone();
                        string st;
                        IAsyncResult result = handler.BeginInvoke(dr, out d, out st, null, null);
                        form.AsyncResult = result;
                        form.ShowDialog(this);
                        handler.EndInvoke(out d, out st, result);
                        listBox1.Items.Add(st);
                        dataGridView3.DataSource = d;

                    }

                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void CmnLookData(DataRow dr, out DataTable dt, out string st)
        {

            //dt = (DataTable)RTable.Clone();
            dt = (DataTable)userdata.Clone();
            ZKAccess FG = new ZKAccess(ConvertUtil.GetDBInt(dr["SerialPort"]), ConvertUtil.GetDBInt(dr["Port"]), ConvertUtil.GetDBInt(dr["MachineNumber"]), ConvertUtil.GetDBInt(dr["Baudrate"]), dr["IP"].ToString());


            if (FG.Connect_Net())
            {

                st = "连接成功！";
                string sdwEnrollNumber = "";
                string sName = "";
                string sPassword = "";
                int iPrivilege = 0;
                bool bEnabled = false;

                //int idwFingerIndex;
                //string sTmpData = "";

                //int iTmpLength = 0;
                //int iFlag = 0;

                //string sFaceData = "";
                //int iFaceLength = 0;
                //int iFaceIndex = 50;
                FG.m_CZKEMClass.EnableDevice(FG.COM_MACHINENUMBER, false);
                //Cursor = Cursors.WaitCursor;

                FG.m_CZKEMClass.ReadAllUserID(FG.COM_MACHINENUMBER);//read all the user information to the memory
                FG.m_CZKEMClass.ReadAllTemplate(FG.COM_MACHINENUMBER);//read all the users' fingerprint templates to the memory
                while (FG.m_CZKEMClass.SSR_GetAllUserInfo(FG.COM_MACHINENUMBER, out sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
                {
                    DataRow newdr = dt.NewRow();
                    newdr["EnrollNumber"] = sdwEnrollNumber.ToString();
                    newdr["UserName"] = sName.ToString();
                    newdr["Privilege"] = iPrivilege;
                    newdr["MPassword"] = sPassword;
                    newdr["IsEnabled"] = bEnabled;
                    dt.Rows.Add(newdr);
                }

                FG.m_CZKEMClass.EnableDevice(FG.COM_MACHINENUMBER, true);

            }
            else
            {
                st = dr["MachineNumber"].ToString() + "指纹机连接失败";
            }



        }

    }
}
