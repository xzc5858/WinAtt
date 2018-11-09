using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using zkemkeeper;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
namespace WinAtt
{
    public class ZKAccess
    {
        #region 初始化

        public System.Windows.Forms.ListBox lbRTShow;
        public System.Windows.Forms.ListView lvLogs;

        public static  ZKAccess Instance = new ZKAccess();
        //私有构造函数
        public ZKAccess()
        {
            this.m_CZKEMClass = new CZKEMClass();
            this.COM_ConnectType = 1;
            this.COM_TCP_PORT = 0x1112;
            this.COM_MACHINENUMBER = 0x69;
            this.COM_BAUDRATE = 0x9600;
            this.COM_IPAddress = "192.168.1.220";
            this.COM_IsConnected = false;
            this.COM_IsVerify = false;
            this.LstVerify = new List<int>();
            this.COM_iVerifyCount = 0;
            this.IsOnline = true;
            this.iGLCount = 0;
            this.iIndex = 0;
        }

        public ZKAccess(int conntype, int tcp_port, int machinenumber, int baudrate, string ip)
        {
            COM_ConnectType = conntype;
            COM_TCP_PORT = tcp_port;
            COM_MACHINENUMBER = machinenumber;
            COM_BAUDRATE = baudrate;
            COM_IPAddress = ip;
            lbRTShow = new System.Windows.Forms.ListBox();
            lvLogs = new System.Windows.Forms.ListView();

        }
        /// <summary>
        /// 接口类
        /// </summary>
        public zkemkeeper.CZKEMClass m_CZKEMClass = new zkemkeeper.CZKEMClass();
        /// <summary>
        /// 链接类型
        /// </summary>
        public int COM_ConnectType = 1;
        /// <summary>
        /// TCP访问中的端口
        /// </summary>
        public int COM_TCP_PORT = 4370;



        /// <summary>
        /// 机器码
        /// </summary>
        public int COM_MACHINENUMBER = 105;

        /// <summary>
        /// 串口访问波特率
        /// </summary>
        public int COM_BAUDRATE = 38400;

        /// <summary>
        /// 指纹机IP地址
        /// </summary>
        public string COM_IPAddress = "192.168.1.220";

        /// <summary>
        /// 是否连接
        /// </summary>
        public bool COM_IsConnected = false;

        /// <summary>
        /// 是否在验证指纹
        /// </summary>
        public bool COM_IsVerify = false;

        /// <summary>
        /// 本次验证的列表
        /// </summary>
        public List<int> LstVerify = new List<int>();

        /// <summary>
        /// 校验次数
        /// </summary>
        public int COM_iVerifyCount = 0;

        /// <summary>
        /// 在线/离线方式
        /// </summary>
        public bool IsOnline = true;

        #endregion

        #region 修改IP

        /// <summary>
        /// 修改新的IP
        /// </summary>
        /// <param name="ip"></param>
        public void SetIPAddress(string ip)
        {

            this.COM_IPAddress = ip;

            Disconnect();
            Connect_Net();

        }


        /// <summary>
        /// 设置是否连接
        /// </summary>
        /// <param name="isOnline"></param>
        public void SetModel(bool isOnline)
        {
            this.IsOnline = isOnline;
        }

        #endregion

        #region 修改时间

        /// <summary>
        /// 
        /// </summary>
        public void SetMachineTime()
        {
            m_CZKEMClass.SetDeviceTime(COM_MACHINENUMBER);
        }
        #endregion

        #region 连接判断

        /// <summary>
        /// 连接设备(网络)
        /// </summary>
        public bool Connect_Net()
        {
            if (COM_IsConnected == true)
            {
                return true;
            }
            switch (COM_ConnectType)
            {
                case 1:
                    COM_IsConnected = m_CZKEMClass.Connect_Net(COM_IPAddress, COM_TCP_PORT);
                    break;
                case 2:
                    COM_IsConnected = m_CZKEMClass.Connect_Com(COM_TCP_PORT, COM_MACHINENUMBER, COM_BAUDRATE);
                    break;
                case 3:
                    COM_IsConnected = m_CZKEMClass.Connect_USB(COM_MACHINENUMBER);
                    break;
                default:
                    COM_IsConnected = m_CZKEMClass.Connect_Net(COM_IPAddress, COM_TCP_PORT);
                    break;
            }

            if (COM_IsConnected == true)
            {
                if (m_CZKEMClass.RegEvent(COM_MACHINENUMBER, 65535))
                {
                    this.m_CZKEMClass.OnFinger += new zkemkeeper._IZKEMEvents_OnFingerEventHandler(m_CZKEMClass_OnFinger);
                    this.m_CZKEMClass.OnVerify += new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(m_CZKEMClass_OnVerify);
                    this.m_CZKEMClass.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(m_CZKEMClass_OnAttTransactionEx);
                    this.m_CZKEMClass.OnFingerFeature += new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(m_CZKEMClass_OnFingerFeature);
                    this.m_CZKEMClass.OnEnrollFingerEx += new zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(m_CZKEMClass_OnEnrollFingerEx);
                    this.m_CZKEMClass.OnDeleteTemplate += new zkemkeeper._IZKEMEvents_OnDeleteTemplateEventHandler(m_CZKEMClass_OnDeleteTemplate);
                    this.m_CZKEMClass.OnNewUser += new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(m_CZKEMClass_OnNewUser);
                    this.m_CZKEMClass.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(m_CZKEMClass_OnHIDNum);
                    this.m_CZKEMClass.OnAlarm += new zkemkeeper._IZKEMEvents_OnAlarmEventHandler(m_CZKEMClass_OnAlarm);
                    this.m_CZKEMClass.OnDoor += new zkemkeeper._IZKEMEvents_OnDoorEventHandler(m_CZKEMClass_OnDoor);
                    this.m_CZKEMClass.OnWriteCard += new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(m_CZKEMClass_OnWriteCard);
                    this.m_CZKEMClass.OnEmptyCard += new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(m_CZKEMClass_OnEmptyCard);
                }
                lbRTShow.Items.Add("设备连接成功,设备码:" + COM_MACHINENUMBER.ToString());
                return true;
            }
            else
            {
                int idwErrorCode = 0;
                m_CZKEMClass.GetLastError(ref idwErrorCode);
                //MessageUtil.ShowError("不能连上设备,错误代码=" + idwErrorCode.ToString());
                lbRTShow.Items.Add("不能连上设备,错误代码=" + idwErrorCode.ToString());
                return false;
            }

        }

        /// <summary>
        /// 断开网络
        /// </summary>
        public void Disconnect()
        {
            m_CZKEMClass.Disconnect();
            this.m_CZKEMClass.OnFinger -= new zkemkeeper._IZKEMEvents_OnFingerEventHandler(m_CZKEMClass_OnFinger);
            this.m_CZKEMClass.OnVerify -= new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(m_CZKEMClass_OnVerify);
            this.m_CZKEMClass.OnAttTransactionEx -= new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(m_CZKEMClass_OnAttTransactionEx);
            this.m_CZKEMClass.OnFingerFeature -= new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(m_CZKEMClass_OnFingerFeature);
            this.m_CZKEMClass.OnEnrollFingerEx -= new zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(m_CZKEMClass_OnEnrollFingerEx);
            this.m_CZKEMClass.OnDeleteTemplate -= new zkemkeeper._IZKEMEvents_OnDeleteTemplateEventHandler(m_CZKEMClass_OnDeleteTemplate);
            this.m_CZKEMClass.OnNewUser -= new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(m_CZKEMClass_OnNewUser);
            this.m_CZKEMClass.OnHIDNum -= new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(m_CZKEMClass_OnHIDNum);
            this.m_CZKEMClass.OnAlarm -= new zkemkeeper._IZKEMEvents_OnAlarmEventHandler(m_CZKEMClass_OnAlarm);
            this.m_CZKEMClass.OnDoor -= new zkemkeeper._IZKEMEvents_OnDoorEventHandler(m_CZKEMClass_OnDoor);
            this.m_CZKEMClass.OnWriteCard -= new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(m_CZKEMClass_OnWriteCard);
            this.m_CZKEMClass.OnEmptyCard -= new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(m_CZKEMClass_OnEmptyCard);
            COM_IsConnected = false;
            //DBLinker.Linker.ExecuteNonQuery("UPDATE [d_Machines] SET  [ConnectType] = 0 WHERE MachineNumber=" + COM_MACHINENUMBER.ToString());
            //lbRTShow.Items.Add("设备断开，机器码：" + COM_MACHINENUMBER.ToString());


        }

        #endregion

        #region 实时事件

        //当机器上指纹头上检测到有指纹时触发该消息
        private void m_CZKEMClass_OnFinger()
        {
            //取日志
            lbRTShow.Items.Add("有指纹时触发");
        }

        //当用户验证时触发该消息
        private void m_CZKEMClass_OnVerify(int iUserID)
        {
            //if (lbRTShow.Items.Count > 100000)

            lbRTShow.Items.Add("验证中....");
            if (iUserID != -1)
            {
                lbRTShow.Items.Add("验证通过,用户ID: " + iUserID.ToString());
            }
            else
            {
                lbRTShow.Items.Add("验证错误 ");
            }
        }

        //登记用户指纹时，当有指纹按下时触发该消息
        private void m_CZKEMClass_OnFingerFeature(int iScore)
        {
            if (iScore <= 0)
            {
                //没通过
                lbRTShow.Items.Add("您的指纹质量较差");
            }
            else
            {
                //通过
                //统计3次
                COM_iVerifyCount++;
                lbRTShow.Items.Add("您的指纹验证通过..Score:　" + iScore.ToString());
            }
        }
        int iGLCount = 0;
        int iIndex = 0;
        /// <summary>
        /// 如果您的指纹（或你的卡）通过了验证，此事件将被触发
        /// </summary>
        /// <param name="sEnrollNumber">登记号码</param>
        /// <param name="iIsInValid">IsInValid为该记录是否为有效记录，1 为无效记录，0 为有效记录。</param>
        /// <param name="iAttState">AttState为考勤状态默认 0—Check-In 1—Check-Out 2—Break-Out 3—Break-In 4—OT-In 5—OT-Out</param>
        /// <param name="iVerifyMethod">0 为密码验证，1为指纹验证，2 为卡验证</param>
        /// <param name="iYear"></param>
        /// <param name="iMonth"></param>
        /// <param name="iDay"></param>
        /// <param name="iHour"></param>
        /// <param name="iMinute"></param>
        /// <param name="iSecond"></param>
        /// <param name="iWorkCode">WorkCode返回验证时WorkCode值，当机器无Workcode功能时，该值返回0.</param>
        private void m_CZKEMClass_OnAttTransactionEx(string sEnrollNumber, int iIsInValid, int iAttState, int iVerifyMethod, int iYear, int iMonth, int iDay, int iHour, int iMinute, int iSecond, int iWorkCode)
        {

            string t = iYear.ToString() + "-" + iMonth.ToString() + "-" + iDay.ToString() + " " + iHour.ToString() + ":" + iMinute.ToString() + ":" + iSecond.ToString();
            //string sql = "INSERT INTO [d_AttTransaction] ([EnrollNumber] ,[VerifyMethod] ,[InOutMode] ,[date] ,[WorkCode]) VALUES ('" + sdwEnrollNumber + "'," + idwVerifyMode + "," + idwInOutMode + ",'" + t + "'," + idwWorkcode + ")";

            //SqlParameter[] sqlParameter = { new System.Data.SqlClient.SqlParameter("@EnrollNumber", System.Data.SqlDbType.NVarChar), new System.Data.SqlClient.SqlParameter("@VerifyMethod", SqlDbType.Int), new SqlParameter("@InOutMode", SqlDbType.Int), new SqlParameter("@WorkCode", SqlDbType.Int), new SqlParameter("@AttState", SqlDbType.Int), new SqlParameter("@date", SqlDbType.DateTime) };
            //sqlParameter[0].Value = sEnrollNumber;
            //sqlParameter[1].Value = iVerifyMethod;
            ////sqlParameter[2].Value = iIsInValid;
            //sqlParameter[3].Value = iWorkCode;
            //sqlParameter[4].Value = iAttState;
            //sqlParameter[5].Value = DXTravel.DXData.Common.ConvertUtil.GetDateTime(t);
            //int tu = DXTravel.DXData.DB.DBLinker.Linker.ExecuteNonQuery("Proc_InsertAttTransaction", sqlParameter);
            //if (tu >= 0)
            //{
            //    iGLCount++;
            //    lbRTShow.Items.Add(sEnrollNumber + "录入记录成功");
            //    //if (m_CZKEMClass.SSR_GetUserInfo(COM_MACHINENUMBER, sEnrollNumber, out name, out  pass, out  privilege, out  enabled))
            //    lvLogs.Items.Add(iGLCount.ToString());
            //    lvLogs.Items[iIndex].SubItems.Add(sEnrollNumber.ToString());//modify by Darcy on Nov.26 2009
            //    lvLogs.Items[iIndex].SubItems.Add(iVerifyMethod.ToString());
            //    lvLogs.Items[iIndex].SubItems.Add(iIsInValid.ToString());
            //    lvLogs.Items[iIndex].SubItems.Add(t);
            //    lvLogs.Items[iIndex].SubItems.Add(iWorkCode.ToString());
            //    //lvLogs.Items[iIndex].SubItems.Add(name);
            //    iIndex++;
            //}
            //else lbRTShow.Items.Add(sEnrollNumber + "录入记录失败");

        }


        /// <summary>
        /// 登记指纹时触发该事件当你登记你的手指，这个事件将被触发。 （该事件只能由TFT屏幕设备被触发）
        /// </summary>
        /// <param name="sEnrollNumber"></param>
        /// <param name="iFingerIndex"></param>
        /// <param name="iActionResult"></param>
        /// <param name="iTemplateLength"></param>
        private void m_CZKEMClass_OnEnrollFingerEx(string sEnrollNumber, int iFingerIndex, int iActionResult, int iTemplateLength)
        {
            if (iActionResult == 0)
            {
                lbRTShow.Items.Add("登记指纹触发....");
                lbRTShow.Items.Add("....用户ID: " + sEnrollNumber + " Index: " + iFingerIndex.ToString() + " tmpLen: " + iTemplateLength.ToString());
            }
            else
            {
                lbRTShow.Items.Add("登记指纹 Error,actionResult=" + iActionResult.ToString());
            }
        }

        //当你删除一个一个指纹模板，此事件将被触发
        private void m_CZKEMClass_OnDeleteTemplate(int iEnrollNumber, int iFingerIndex)
        {
            lbRTShow.Items.Add("删除指纹事件触发...");
            lbRTShow.Items.Add("...用户ID=" + iEnrollNumber.ToString() + " FingerIndex=" + iFingerIndex.ToString());
        }

        //当成功登记新用户时触发该消息
        private void m_CZKEMClass_OnNewUser(int iEnrollNumber)
        {
            lbRTShow.Items.Add("成功登记新用户触发...");
            lbRTShow.Items.Add("...新用户ID=" + iEnrollNumber.ToString());
        }

        //当你刷卡的设备，此事件将被触发，以显示你的卡号。
        private void m_CZKEMClass_OnHIDNum(int iCardNumber)
        {
            lbRTShow.Items.Add("刷卡事件被触发...");
            lbRTShow.Items.Add("...卡号=" + iCardNumber.ToString());
        }

        //当拆解机器或胁迫报警发生时，触发该事件。
        private void m_CZKEMClass_OnAlarm(int iAlarmType, int iEnrollNumber, int iVerified)
        {
            lbRTShow.Items.Add("拆解机器触发事件...");
            lbRTShow.Items.Add("...AlarmType=" + iAlarmType.ToString());
            lbRTShow.Items.Add("...EnrollNumber=" + iEnrollNumber.ToString());
            lbRTShow.Items.Add("...Verified=" + iVerified.ToString());
        }

        //感应器事件
        private void m_CZKEMClass_OnDoor(int iEventType)
        {
            lbRTShow.Items.Add("感应开门事件触发...");
            lbRTShow.Items.Add("...EventType=" + iEventType.ToString());
        }

        //当你emptyed的Mifare卡，此事件将被触发。
        private void m_CZKEMClass_OnEmptyCard(int iActionResult)
        {
            lbRTShow.Items.Add("Mifare卡事件将被触发...");
            if (iActionResult == 0)
            {
                lbRTShow.Items.Add("...空Mifare卡OK");
            }
            else
            {
                lbRTShow.Items.Add("...空Mifare卡错误");
            }
        }

        //当你写入Mifare卡，此事件将被触发。
        private void m_CZKEMClass_OnWriteCard(int iEnrollNumber, int iActionResult, int iLength)
        {
            lbRTShow.Items.Add("写入Mifare卡事件触发...");
            if (iActionResult == 0)
            {
                lbRTShow.Items.Add("...写入Mifare卡 OK");
                lbRTShow.Items.Add("...EnrollNumber=" + iEnrollNumber.ToString());
                lbRTShow.Items.Add("...TmpLength=" + iLength.ToString());
            }
            else
            {
                lbRTShow.Items.Add("...Write Failed");
            }
        }

        //After function GetRTLog() is called ,RealTime Events will be triggered. 
        //When you are using these two functions, it will request data from the device forwardly.
        private void rtTimer_Tick(object sender, EventArgs e)
        {

        }

        #endregion

        #region 脱机处理
        /// <summary>
        /// 开门100/10=10秒;
        /// </summary>
        /// <returns></returns>
        public bool UnLock()
        {
            int state = 0;
            bool onclock = false;
            if (m_CZKEMClass.GetDoorState(COM_MACHINENUMBER, ref state))
            {
                if (state == 1)
                    onclock = m_CZKEMClass.ACUnlock(COM_MACHINENUMBER, 100);
            }
            return onclock;
        }

        /// <summary>
        /// 上传数据
        /// </summary>
        //public void UploadData()
        //{
        //    //C_Keeper.
        //}


        /// <summary>
        /// 考勤记录下载流程
        /// </summary>
        public void DownData()
        {
            dwwork = 0;
            if (Connect_Net())
            {
                if (m_CZKEMClass.ReadGeneralLogData(COM_MACHINENUMBER))
                {
                    m_CZKEMClass.SSR_GetGeneralLogData(COM_MACHINENUMBER, out enrollnumber, out dwvmode, out dwinmode, out dwyear, out dwmonth, out dwday, out dwhour, out dwmin, out dwsecond, ref dwwork);
                }

            }
        }
        /// <summary>
        /// 操作记录下载流程
        /// </summary>
        public void DownSuperLogData()
        {
            if (Connect_Net())
            {
                m_CZKEMClass.EnableDevice(COM_MACHINENUMBER, false);
                m_CZKEMClass.ReadAllUserID(COM_MACHINENUMBER);
                m_CZKEMClass.ReadAllTemplate(COM_MACHINENUMBER);

                if (m_CZKEMClass.ReadSuperLogData(COM_MACHINENUMBER))
                {
                    m_CZKEMClass.SSR_GetSuperLogData(COM_MACHINENUMBER, out number, out admin, out user, out manipulation, out time, out params1, out params2, out params3);
                }
            }
        }

        /// <summary>
        /// 下载用户信息,指纹模板等流程
        /// </summary>
        public void DownUserInfo()
        {
            if (Connect_Net())
            {

                if (m_CZKEMClass.ReadAllUserID(COM_MACHINENUMBER))
                {
                    if (m_CZKEMClass.ReadAllTemplate(COM_MACHINENUMBER))
                    {
                        m_CZKEMClass.SSR_GetAllUserInfo(COM_MACHINENUMBER, out enrollnumber, out name, out pass, out privilege, out enabled);
                    }
                }


            }


        }
        string name, pass;
        int privilege;
        bool enabled;

        //public void d()
        //{
        //    m_CZKEMClass.RegEvent(COM_MACHINENUMBER, eventmark);
        //}
        string enrollnumber;
        int dwvmode, dwinmode, dwyear, dwmonth, dwday, dwhour, dwmin, dwsecond;
        int dwwork;

        /// <summary>
        /// 
        /// </summary>
        int number;
        string admin;
        string user;
        int manipulation;
        string time;
        int params1, params2, params3;
        #endregion

        #region 添加用户(要重启才能生效)
        //public bool DoorAddUser(PensionInfoModel piModel)
        //{
        //    if (!Connect_Net())
        //    {
        //        return false;                                                               //连不上
        //    }

        //    try
        //    {
        //        DoorDeleteUser(piModel);                                                //先删除用户

        //        int prl = 0;                                                            //用户权限；0，普通用户；1，登记员；2，管理员3，超级管理员


        //        bool enAble = true;
        //        if (!m_CZKEMClass.SetUserInfo(COM_MACHINENUMBER, piModel.PensionID, piModel.Name, "", prl, enAble))
        //        {
        //            return false;
        //        }

        //        int Tzs = 1;                                //时间段，为空的时候，恢复为组的设置，不为空时，则指定一个用户自己的设置。
        //        bool res = m_CZKEMClass.SetUserTZs(COM_MACHINENUMBER, piModel.PensionID, ref Tzs);

        //        //添加指纹(最多5个指纹)
        //        if (piModel.Finger1.Length > 0)
        //        {
        //            res = m_CZKEMClass.SetUserTmpStr(COM_MACHINENUMBER, piModel.PensionID, 0, piModel.Finger1);
        //        }



        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("上传错误：" + ex.Message);
        //        return false;
        //    }
        //}
        //#endregion


        //#region 删除用户(要重启才能生效)
        //public bool DoorDeleteUser(PensionInfoModel piModel)
        //{


        //    try
        //    {
        //        if (!Connect_Net())
        //        {
        //            return false;                                                               //连不上
        //        }


        //        int dwEMchNum = 1;
        //        int dwBackupNum = 12;
        //        if (!m_CZKEMClass.DeleteEnrollData(COM_MACHINENUMBER, piModel.PensionID, dwEMchNum, dwBackupNum))
        //        {
        //            return false;
        //        }

        //        m_CZKEMClass.RefreshData(COM_MACHINENUMBER);

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Disconnect();
        //        clsCommon.ErrorLog(ex);
        //        return false;
        //    }
        //}
        #endregion

        #region 其他
        public static bool ToPing(string ipaddress)
        {
            //Ping pingSender = new Ping();
            //PingOptions options = new PingOptions();

            //options.DontFragment = true;

            //// Create a buffer of 32 bytes of data to be transmitted.
            //string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            //byte[] buffer = Encoding.ASCII.GetBytes(data);
            //int timeout = 500;
            //PingReply reply = pingSender.Send(ipaddress, timeout, buffer, options);

            //return (reply.Status == IPStatus.Success);
            return false;
        }

        public static bool ToPing(string ipaddress, int timeOut)
        {
            //Ping pingSender = new Ping();
            //PingOptions options = new PingOptions();

            //options.DontFragment = true;

            //// Create a buffer of 32 bytes of data to be transmitted.
            //string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            //byte[] buffer = Encoding.ASCII.GetBytes(data);
            //PingReply reply = pingSender.Send(ipaddress, timeOut, buffer, options);

            //return (reply.Status == IPStatus.Success);
            return false;
        }
        #endregion

        #region 在线处理函数
        public bool Enroll(string EnrollNum, out string sTmpData)
        {
            sTmpData = "";
            COM_iVerifyCount = 0;

            try
            {
                if (!Connect_Net())
                {
                    return false;                                                               //连不上
                }


                m_CZKEMClass.CancelOperation();                                                 //取消机器当前的指纹登记状态
                m_CZKEMClass.SSR_DelUserTmpExt(COM_MACHINENUMBER, EnrollNum.ToString(), 0);     //将要保存的指纹位置清空


                if (m_CZKEMClass.StartEnrollEx(EnrollNum.ToString(), 0, 1))
                {

                    //MessageUtil.ShowInfo("请按三次手指，按完之后点击确定");
                    m_CZKEMClass.RefreshData(COM_MACHINENUMBER);

                    m_CZKEMClass.StartIdentify();                                               //使机器进入等待状态

                    //sTmpData = GetFinger(EnrollNum);          //读取很慢，先不读取

                    return true;
                }
                else
                {
                    int idwErrorCode = 0;
                    m_CZKEMClass.GetLastError(ref idwErrorCode);
                    //MessageUtil.ShowInfo("添加用户失败,错误代码=" + idwErrorCode.ToString());
                    return false;
                }


            }
            catch (Exception ex)
            {
                Disconnect();
                //clsCommon.ErrorLog(ex);
                return false;
            }
        }

        public bool SendPhoto(string EnroollNumber, byte image, int LengTh)
        {
            bool b = false;
            try
            {
                if (!Connect_Net())
                {
                    return false;                                                               //连不上
                }
                b = m_CZKEMClass.SetUserFace(COM_MACHINENUMBER, EnroollNumber, 50, ref image, LengTh);
            }
            catch (Exception ex)
            {
                //Disconnect();
                //clsCommon.ErrorLog(ex);
                return false;
            }
            return b;
        }

        /// <summary>
        /// 是否有用户
        /// </summary>
        /// <returns></returns>
        public bool IsUser(int EnrollNum)
        {
            try
            {
                if (!Connect_Net())
                {
                    return false;                                                               //连不上
                }

                string sdwEnrollNumber = "";
                string sName = "";
                string sPassword = "";
                int iPrivilege = 0;
                bool bEnabled = false;

                m_CZKEMClass.ReadAllUserID(COM_MACHINENUMBER);          //read all the user information to the memory
                m_CZKEMClass.ReadAllTemplate(COM_MACHINENUMBER);        //read all the users' fingerprint templates to the memory
                while (m_CZKEMClass.SSR_GetAllUserInfo(COM_MACHINENUMBER, out sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
                {
                    if (EnrollNum == int.Parse(sdwEnrollNumber))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Disconnect();
                //clsCommon.ErrorLog(ex);
                return false;
            }
        }

        /// <summary>
        /// 取指纹
        /// </summary>
        /// <returns></returns>
        public string GetFinger(int EnrollNum)
        {
            try
            {
                if (!Connect_Net())
                {
                    return "";                                                               //连不上
                }

                string sdwEnrollNumber = "";
                string sName = "";
                string sPassword = "";
                int iPrivilege = 0;
                bool bEnabled = false;

                string sTmpData = "";
                int iTmpLength = 0;
                int iFlag = 1;

                m_CZKEMClass.EnableDevice(COM_MACHINENUMBER, false);


                m_CZKEMClass.ReadAllUserID(COM_MACHINENUMBER);          //read all the user information to the memory
                m_CZKEMClass.ReadAllTemplate(COM_MACHINENUMBER);        //read all the users' fingerprint templates to the memory
                while (m_CZKEMClass.SSR_GetAllUserInfo(COM_MACHINENUMBER, out sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
                {
                    if (EnrollNum.ToString() == sdwEnrollNumber)
                    {
                        if (m_CZKEMClass.GetUserTmpExStr(COM_MACHINENUMBER, sdwEnrollNumber, 0, out iFlag, out sTmpData, out iTmpLength))//get the corresponding templates string and length from the memory
                        {
                            break;
                        }
                    }
                }

                m_CZKEMClass.EnableDevice(COM_MACHINENUMBER, true);

                return sTmpData + "";
            }
            catch (Exception ex)
            {
                Disconnect();
                //clsCommon.ErrorLog(ex);
                return "";
            }
        }

        ///// <summary>
        ///// 获取用户指纹信息
        ///// </summary>
        ///// <param name="dwEnrollNumber"></param>
        ///// <returns></returns>
        //public bool GetUserInfo(string dwEnrollNumber)
        //{
        //    bool b = false;
        //    if (m_CZKEMClass.SSR_GetUserInfo(COM_MACHINENUMBER, dwEnrollNumber, name, pass, privilege, enabled))
        //        b = true;
        //    else b = false;
        //    return b;
        //}

        #endregion

    }
}
