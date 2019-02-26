using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using MySql.Data.MySqlClient;
using qcloudsms_csharp;
using qcloudsms_csharp.json;
using qcloudsms_csharp.httpclient;

namespace WirelessDoor_server_master
{
    public partial class MainForm : Form
    {
        //服务器地址
        //IPAddress[] HOST = Dns.GetHostAddresses("k806034232.6655.la");
        //服务器地址
        //IPAddress HOST = IPAddress.Parse("172.20.10.3");
        IPAddress HOST = IPAddress.Parse("172.19.22.153");
        //服务器端口号
        private const int port = 8086;
        //会议室接收标记
        public bool getFlag = false;

        Thread threadWatch = null; // 负责监听客户端连接请求的 线程
        Socket socketWatch = null; // 负责监听客户端连接的socket模块

        /* socket连接对象字典 */
        Dictionary<string, Socket> dict = new Dictionary<string, Socket>();
        /* 客户端连接对象进程字典 */
        Dictionary<string, Thread> dictThread = new Dictionary<string, Thread>();

        //数据库信息
        string database = "room";
        string username = "root";
        //string passwd = "Dll960220";
        string passwd = "LL960220";

        //短信发送appid
        int appid = 1400178112;
        string appkey = "6402f72c5c2d15fac7f124d78d6f4759";
        int[] templateId = { 265637, 270334, 265636, 266224, 280196 };//注册，预约成功，失败，提醒，报警
        string smsSign = "会易云";

        //数据接收标志
        bool reciveGet = false;

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            CreatDataBase();
            ReLoadDataGridView();
        }

        /// <summary>
        /// 新建数据库
        /// </summary>
        private void CreatDataBase()
        {
            MySqlConnection myconn = new MySqlConnection("Host =localhost"
                                                        + ";Database=" + database
                                                        + ";Username=" + username
                                                        + ";Password=" + passwd + ";");
            try
            {
                /* 新建用户信息表 */
                //打开数据库
                myconn.Open();
                //新建SQL指令
                MySqlCommand mycom = myconn.CreateCommand();
                //构造SQL指令
                string sql = string.Format("CREATE TABLE IF NOT EXISTS userInfo(" +
                                           "userID INTEGER PRIMARY KEY," +        //用户序号
                                           "userName VARCHAR(6)," +               //用户名
                                           "authority VARCHAR(20)," +             //登录名
                                           "passwd VARCHAR(20)," +                //密码
                                           "sex VARCHAR(2)," +                    //性别
                                           "tel VARCHAR(20)," +                   //电话
                                           "qq VARCHAR(20)," +                    //qq
                                           "email VARCHAR(20)," +                 //邮箱
                                           "birthday VARCHAR(20)," +              //生日
                                           "recodeDate VARCHAR(20));");           //注册时间
                mycom.CommandText = sql;

                mycom.CommandType = CommandType.Text;

                mycom.ExecuteNonQuery();
                //关闭数据库
                myconn.Close();

                /* 新建会议室信息表 */
                myconn.Open();
                //sqlite指令语句
                sql = string.Format("CREATE TABLE IF NOT EXISTS roomInfo(" +
                                    "roomID INTEGER PRIMARY KEY," +         //会议室号
                                    "roomName TEXT," +                      //会议室名称
                                    "roomState TEXT," +                     //会议室状态
                                    "netState TEXT," +                      //在线状态
                                    "ip TEXT);");                           //ip地址
                mycom.CommandText = sql;

                mycom.CommandType = CommandType.Text;

                //如果表不存在，创建会议室信息表 
                mycom.ExecuteNonQuery();
                //关闭数据库，防止资源占用
                myconn.Close();
                
                /* 新建预约信息表 */
                myconn.Open();
                sql = string.Format("CREATE TABLE IF NOT EXISTS reservationInfo(" +
                                    "roomID INTEGER," +             //会议室号
                                    "roomName TEXT," +              //会议室名称
                                    "roomState TEXT," +             //会议室状态（预约，拒绝）
                                    "msgState TEXT," +              //信息状态（处理，未处理）
                                    "passwd VARCHAR(6)," +          //验证码
                                    "authority VARCHAR(20)," +      //用户号
                                    "userName VARCHAR(6)," +        //用户名
                                    "beginTime VARCHAR(20)," +      //开始时间
                                    "endTime VARCHAR(20)," +        //结束时间
                                    "reason TEXT);");               //申请理由
                mycom.CommandText = sql;

                mycom.CommandType = CommandType.Text;
                //如果表不存在，创建信息表 
                mycom.ExecuteNonQuery();
                myconn.Dispose();
                //关闭数据库，防止资源占用
                myconn.Close();

                /* 新建操作记录表 */
                myconn.Open();
                sql = string.Format("CREATE TABLE IF NOT EXISTS loginfo(" +
                                    "roomID INTEGER," +             //会议室号
                                    "roomName TEXT," +              //会议室名称
                                    "authority VARCHAR(20)," +      //用户号
                                    "userName VARCHAR(6)," +        //用户名
                                    "beginTime VARCHAR(20)," +      //开始时间
                                    "endTime VARCHAR(20)," +        //结束时间
                                    "reason TEXT," +                //申请理由
                                    "msgState TEXT," +              //申请结果
                                    "msgReason TEXT);");            //理由
                mycom.CommandText = sql;

                mycom.CommandType = CommandType.Text;
                //如果表不存在，创建信息表 
                mycom.ExecuteNonQuery();
                myconn.Dispose();
                //关闭数据库，防止资源占用
                myconn.Close();


            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                myconn.Dispose();
                myconn.Close();
            }
        }

        /// <summary>
        /// 重载dataGridView数据
        /// </summary>
        private void ReLoadDataGridView()
        {
            MySqlConnection myconn = new MySqlConnection("Host =localhost" +
                                                         ";Database=" + database +
                                                         ";Username=" + username +
                                                         ";Password=" + passwd + ";");
            dgvReservation.Rows.Clear();
            try
            {
                //连接数据库
                myconn.Open();
                //新建SQL指令
                MySqlCommand mycom = myconn.CreateCommand();
                string sql = string.Format("SELECT * FROM reservationinfo;");
                mycom.CommandText = sql;

                mycom.CommandType = CommandType.Text;
                //执行查询指令
                MySqlDataReader reader = mycom.ExecuteReader();
                while (reader.Read())
                {
                    int index = dgvReservation.Rows.Add();
                    dgvReservation.Rows[index].Cells[0].Value = reader.GetString(1);   //会议室名称
                    dgvReservation.Rows[index].Cells[1].Value = reader.GetString(3);   //处理状况
                    dgvReservation.Rows[index].Cells[2].Value = reader.GetString(6);   //用户名
                    dgvReservation.Rows[index].Cells[3].Value = reader.GetString(7);   //开始时间
                    dgvReservation.Rows[index].Cells[4].Value = reader.GetString(8);   //结束时间
                    dgvReservation.Rows[index].Cells[5].Value = reader.GetString(9);   //申请理由
                }
                //释放reader的资源
                reader.Dispose();
                reader.Close();
                //关闭数据库，防止数据库被锁定
                myconn.Dispose();
                myconn.Close();
            }
            catch (MySqlException se)
            {
                ShowMsg(se.ToString());
                //MessageBox.Show(se.ToString());
                //关闭数据库，防止数据库被锁定
                myconn.Dispose();
                myconn.Close();
            }
            dgvRoom.Rows.Clear();
            try
            {
                //打开数据库
                myconn.Open();
                //新建SQL指令
                MySqlCommand mycom = myconn.CreateCommand();

                string sql = string.Format("SELECT * FROM roomInfo;");

                mycom.CommandText = sql;
                mycom.CommandType = CommandType.Text;
                //执行查询指令
                MySqlDataReader reader = mycom.ExecuteReader();
                while (reader.Read())
                {
                    int index = dgvRoom.Rows.Add();

                    dgvRoom.Rows[index].Cells[0].Value = reader[0].ToString();
                    dgvRoom.Rows[index].Cells[1].Value = reader[1].ToString();
                    dgvRoom.Rows[index].Cells[2].Value = reader[2].ToString();
                    dgvRoom.Rows[index].Cells[3].Value = reader[3].ToString();
                    dgvRoom.Rows[index].Cells[4].Value = reader[4].ToString();
                }
                //释放reader的资源
                reader.Dispose();
                reader.Close();
                //关闭数据库，防止数据库被锁定
                myconn.Dispose();
                myconn.Close();
            }
            catch (MySqlException se)
            {
                ShowMsg(se.ToString());
                //关闭数据库，防止数据库被锁定
                myconn.Dispose();
                myconn.Close();
            }
        }

        /// <summary>
        /// 监听子函数
        /// </summary>
        void WatchConnecting()
        {
            while (true)  // 持续不断的监听客户端的连接请求；  
            {
                // 开始监听客户端连接请求，Accept方法会阻断当前的线程；  
                Socket sokConnection = socketWatch.Accept(); // 一旦监听到一个客户端的请求，就返回一个与该客户端通信的 套接字；  
                // 想列表控件中添加客户端的IP信息；  
                lbOnline.Items.Add(sokConnection.RemoteEndPoint.ToString());
                // 将与客户端连接的 套接字 对象添加到集合中；  
                dict.Add(sokConnection.RemoteEndPoint.ToString(), sokConnection);
                //txtMsg.AppendText("客户端连接，地址为：");
                ShowMsg("客户端连接，地址为：" + sokConnection.RemoteEndPoint.ToString());
                //ShowMsg(sokConnection.RemoteEndPoint.ToString());

                //接收数据线程
                Thread thr = new Thread(RecMsg);
                thr.IsBackground = true;
                thr.Start(sokConnection);
                dictThread.Add(sokConnection.RemoteEndPoint.ToString(), thr);  //  将新建的线程 添加 到线程的集合中去。  
            }
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sokConnectionparn"></param>
        private void RecMsg(object sokConnectionparn)
        {
            string recive_data;
            Socket sokClient = sokConnectionparn as Socket;
            while (true)
            {
                // 定义一个2M的缓存区
                byte[] arrMsgRec = new byte[1024*1024];
                // 将接受到的数据存入到输入  arrMsgRec中；  
                int length = -1;
                try
                {
                    length = sokClient.Receive(arrMsgRec); // 接收数据，并返回数据的长度； 
                    string strMsg = System.Text.Encoding.UTF8.GetString(arrMsgRec, 0, length);// 将接受到的字节数据转化成字符串
                    if (strMsg != "")
                    {
                        ShowMsg("数据来自:" + sokClient.RemoteEndPoint.ToString());
                        ShowMsg(strMsg);
                        recive_data = strMsg;
                        decode_data(recive_data, sokClient.RemoteEndPoint.ToString()); //解析接收数据
                        ReLoadDataGridView();                   //重载表格数据
                    }
                    else  //防止断开时来的""数据 将其彻底断开
                    {
                        ShowMsg("数据来自:" + sokClient.RemoteEndPoint.ToString());
                        ShowMsg(strMsg + "已断开");
                        dict.Remove(sokClient.RemoteEndPoint.ToString());
                        // 从通信线程集合中删除被中断连接的通信线程对象；  
                        dictThread.Remove(sokClient.RemoteEndPoint.ToString());
                        // 从列表中移除被中断的连接IP  
                        lbOnline.Items.Remove(sokClient.RemoteEndPoint.ToString());
                        //这个关闭只是关闭了单个客户端的连接
                        sokClient.Close();
                        break;
                    }
                }
                catch (SocketException se)
                {
                    ShowMsg("异常：" + se.Message);
                    // 从 通信套接字 集合中删除被中断连接的通信套接字；  
                    dict.Remove(sokClient.RemoteEndPoint.ToString());
                    // 从通信线程集合中删除被中断连接的通信线程对象；  
                    dictThread.Remove(sokClient.RemoteEndPoint.ToString());
                    // 从列表中移除被中断的连接IP  
                    lbOnline.Items.Remove(sokClient.RemoteEndPoint.ToString());
                    sokClient.Close();
                    break;
                }
            }
        }

        /// <summary>
        /// 显示日志
        /// </summary>
        /// <param name="str"></param>
        void ShowMsg(string str)
        {
            if (rtLogo.Text != "") { rtLogo.Text += "\r\n"; }
            rtLogo.Text += DateTime.Now.ToString("HH:mm:ss:") + str;
            rtLogo.Select(rtLogo.Text.Length, 0);//将光标设置到最末尾
            rtLogo.ScrollToCaret();  //将滚动条设置到光标处
        }

        /// <summary>
        /// 开启服务器监听进程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 开启服务器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (开启服务器ToolStripMenuItem.Text == "开启服务器")
            {
                // 创建负责监听的套接字，注意其中的参数；  
                socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // 获得文本框中的IP对象；  
                IPAddress address = HOST;
                // 创建包含ip和端口号的网络节点对象；  
                IPEndPoint endPoint = new IPEndPoint(address, port);
                try
                {
                    // 将负责监听的套接字绑定到唯一的ip和端口上；  
                    socketWatch.Bind(endPoint);
                    开启服务器ToolStripMenuItem.Text = "关闭服务器";
                }
                catch (SocketException se)
                {
                    MessageBox.Show("异常：" + se.Message);
                    return;
                }
                // 设置监听队列的长度；  
                socketWatch.Listen(10);
                // 创建负责监听的线程；  
                threadWatch = new Thread(WatchConnecting);
                threadWatch.IsBackground = true;
                threadWatch.Start();
                ShowMsg("服务器启动监听成功！");
            }
            else if (开启服务器ToolStripMenuItem.Text == "关闭服务器")
            {
                开启服务器ToolStripMenuItem.Text = "开启服务器";
                byte[] arrMsg = System.Text.Encoding.UTF8.GetBytes("@SOFF"); // 将要发送的字符串转换成Utf-8字节数组； 
                byte[] arrSendMsg = new byte[arrMsg.Length + 1]; // 上次写的时候把这一段给弄掉了，实在是抱歉哈~ 用来标识发送是数据而不是文件，如果没有这一段的客户端就接收不到消息了~~~ 
                foreach (Socket s in dict.Values)
                {
                    s.Send(arrMsg);
                }
                ShowMsg("关闭成功！！");
                socketWatch.Close();
                this.Close();
            }
        }

        /// <summary>
        /// 解析接收数据
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ip"></param>
        private void decode_data(string msg,string ip)
        {
            byte[] arrMsg;
            MySqlConnection myconn = new MySqlConnection("Host =localhost" +
                                                         ";Database=" + database +
                                                         ";Username=" + username +
                                                         ";Password=" + passwd + ";");
            string data = msg;
            //判别数据发送端身份
            if (data.IndexOf('@') != -1)
            {
                char remoteDevice = Convert.ToChar(data.Substring(1, 1));
                int CMD = Convert.ToInt16(data.Substring(2, 1));
                try
                {
                    switch (remoteDevice)
                    {
                        case 'P':           //PC端
                            {
                                /*
                                switch (CMD)
                                {
                                    case 1:         //预约请求
                                        {
                                            ReLoadDataGridView();
                                            break;
                                        }
                                    case 2:         //修改请求
                                        {
                                            break;
                                        }
                                    case 3:         //取消请求
                                        {
                                            break;
                                        }
                                    default:    break;
                                }
                                */
                                string userGet = data.Substring(7, 11);
                                arrMsg = System.Text.Encoding.UTF8.GetBytes("@S" + CMD.ToString() + userGet + "OK\r\n");
                                dict[ip].Send(arrMsg);
                                Thread.Sleep(2000);
                                ReLoadDataGridView();
                                break;
                            }
                        case 'R':           //树莓派端
                            {
                                string deviceID = data.Substring(3, 4);
                                string userGet = data.Substring(7, 11);
                                string roomID = Convert.ToInt16(deviceID).ToString();
                                ShowMsg("用户名：" + userGet);

                                int errorState = 0;
                                if (userGet.IndexOf("unkown") != -1)
                                {
                                    errorState = 1;
                                    userGet = "00000000000";
                                    ShowMsg("用户尚未注册！");
                                }
                                else
                                {
                                    ShowMsg("已识别到用户" + userGet);
                                    //连接数据库
                                    myconn.Open();
                                    //新建SQL指令
                                    MySqlCommand mycom = myconn.CreateCommand();

                                    string sql = string.Format("SELECT * FROM reservationinfo WHERE roomID=\"" + roomID + "\" and authority=\"" + userGet + "\";");
                                    //ShowMsg(sql);

                                    mycom.CommandText = sql;

                                    mycom.CommandType = CommandType.Text;
                                    //执行查询指令
                                    MySqlDataReader reader = mycom.ExecuteReader();
                                    //ShowMsg("查询到数据有" + mycom.ExecuteScalar().ToString() + "行");

                                    while (reader.Read())
                                    {
                                        if (reader.HasRows)
                                        {
                                            errorState = CheckTime(reader.GetString(7), reader.GetString(8), false);
                                            if (errorState == 0)
                                            {
                                                ShowMsg("找到预约记录！");
                                                string msgSend = "@S2" + deviceID + reader.GetString(8) + "2\r\n";
                                                ShowMsg(msgSend);
                                                arrMsg = System.Text.Encoding.UTF8.GetBytes(msgSend);
                                                SendDevice(Convert.ToUInt16(deviceID), arrMsg);
                                                Thread.Sleep(2000);
                                                break;
                                            }
                                        }
                                    }

                                    //释放reader的资源
                                    reader.Dispose();
                                    reader.Close();
                                    //关闭数据库，防止数据库被锁定
                                    myconn.Dispose();
                                    myconn.Close();
                                }

                                arrMsg = System.Text.Encoding.UTF8.GetBytes("@S" + CMD.ToString() + deviceID + userGet + errorState.ToString() + "OK\r\n");
                                SendDevice(Convert.ToUInt16(deviceID), arrMsg);
                                break;
                            }
                        case 'D':           //硬件端
                            {
                                string deviceID = data.Substring(3, 4);
                                getFlag = true;
                                switch (CMD)
                                {
                                    case 1:     //联机回复
                                        {
                                            reciveGet = true;

                                            ShowMsg("会议室" + dgvRoom.Rows[Convert.ToUInt16(deviceID) - 1].Cells[1].Value.ToString() + "状态更新成功");
                                            UpdateRoom(Convert.ToUInt16(deviceID), "在线", ip);
                                            arrMsg = System.Text.Encoding.UTF8.GetBytes("@S2" + deviceID + System.DateTime.Now.ToString("yyyyMMddHHmmss") + "1\r\n");
                                            dict[ip].Send(arrMsg);
                                            break;
                                        }
                                    case 2:     //设定时间回复
                                        {
                                            reciveGet = true;
                                            UpdateRoom(Convert.ToUInt16(deviceID), "在线", ip);
                                            break;
                                        }
                                    case 3:     //开门回复
                                        {
                                            UpdateRoom(Convert.ToUInt16(deviceID), "在线", ip);

                                            if (data.Length > 11)
                                            {
                                                string getPasswd = data.Substring(7, 6);
                                                string roomID = Convert.ToUInt16(deviceID).ToString();
                                                //连接数据库
                                                myconn.Open();
                                                //新建SQL指令
                                                MySqlCommand mycom = myconn.CreateCommand();

                                                string sql = string.Format("SELECT * FROM reservationinfo WHERE passwd=\"" + getPasswd + "\";");
                                                mycom.CommandText = sql;

                                                mycom.CommandType = CommandType.Text;
                                                //执行查询指令
                                                MySqlDataReader reader = mycom.ExecuteReader();
                                                //ShowMsg("查询到数据有" + mycom.ExecuteScalar().ToString() + "行");

                                                if (reader.Read())
                                                {
                                                    if (reader.HasRows)
                                                    {
                                                        string userID = reader.GetString(5);
                                                        int res = CheckTime(reader.GetString(7), reader.GetString(8), true);
                                                        string msgSend = "@S" + CMD.ToString() + deviceID + userID + res.ToString() + "\r\n";
                                                        ShowMsg(msgSend);
                                                        arrMsg = System.Text.Encoding.UTF8.GetBytes(msgSend);
                                                        dict[ip].Send(arrMsg);
                                                        Thread.Sleep(2000);
                                                        msgSend = "@S2" + deviceID + reader.GetString(8) + "2\r\n";
                                                        ShowMsg(msgSend);
                                                        arrMsg = System.Text.Encoding.UTF8.GetBytes(msgSend);
                                                        dict[ip].Send(arrMsg);
                                                    }
                                                }
                                                else
                                                {
                                                    arrMsg = System.Text.Encoding.UTF8.GetBytes("@S" + CMD.ToString() + deviceID + "000000000002\r\n");
                                                    ShowMsg(arrMsg.ToString());
                                                    dict[ip].Send(arrMsg);
                                                }

                                                //释放reader的资源
                                                reader.Dispose();
                                                reader.Close();
                                                //关闭数据库，防止数据库被锁定
                                                myconn.Dispose();
                                                myconn.Close();
                                            }
                                            break;
                                        }
                                    case 5:
                                        {
                                            UpdateRoom(Convert.ToUInt16(deviceID), "在线", ip);

                                            myconn.Open();
                                            //新建SQL指令
                                            MySqlCommand mycom = myconn.CreateCommand();

                                            deviceID = Convert.ToUInt16(deviceID).ToString();

                                            string sql = string.Format("SELECT roomname FROM roominfo WHERE roomID=\"" + deviceID + "\";");
                                            mycom.CommandText = sql;

                                            mycom.CommandType = CommandType.Text;
                                            //执行查询指令
                                            MySqlDataReader reader = mycom.ExecuteReader();
                                            if (reader.Read())
                                            {
                                                string[] Parmer = new string[1];
                                                Parmer[0] = reader[0].ToString();
                                                SmsSend("18323832890", 4, Parmer);
                                            }

                                            //释放reader的资源
                                            reader.Dispose();
                                            reader.Close();
                                            //关闭数据库，防止数据库被锁定
                                            myconn.Dispose();
                                            myconn.Close();

                                            break;
                                        }
                                }
                                break;
                            }
                        default: break;
                    }
                }
                catch (Exception ex)
                {
                    ShowMsg(ex.ToString());
                }
            }
        }

        /// <summary>
        /// 检测验证码时间
        /// </summary>
        /// <param name="getTime"></param>
        /// <returns></returns>
        private int CheckTime(string Time1, string Time2, bool code)
        {
            int errorState = 0;
            string time1 = Time1.Substring(0, 4) + '-' + Time1.Substring(4, 2) + '-' + Time1.Substring(6, 2) + ' ' + Time1.Substring(8, 2) + ':' + Time1.Substring(10, 2);
            string time2 = Time2.Substring(0, 4) + '-' + Time2.Substring(4, 2) + '-' + Time2.Substring(6, 2) + ' ' + Time2.Substring(8, 2) + ':' + Time2.Substring(10, 2);
            if (DateTime.Compare(Convert.ToDateTime(time1), DateTime.Now) > 10)
            {
                errorState = 3;
            }
            else if (DateTime.Compare(Convert.ToDateTime(time2), DateTime.Now) < 0 && code == false)
            {
                errorState = 2;
            }
            else if (DateTime.Compare(Convert.ToDateTime(time2), DateTime.Now) < 0 && code == true)
            {
                errorState = 4;
            }
            return errorState;
        }

        /// <summary>
        /// 更新会议室状态
        /// </summary>
        /// <param name="roomID"></param>
        /// <param name="ip"></param>
        private void UpdateRoom(int roomID, string netState, string ip)
        {
            MySqlConnection myconn = new MySqlConnection("Host =localhost" +
                                                         ";Database=" + database +
                                                         ";Username=" + username +
                                                         ";Password=" + passwd + ";");
            try
            {
                //连接数据库
                myconn.Open();
                //新建SQL指令
                MySqlCommand mycom = myconn.CreateCommand();
                string sql = string.Format("");
                sql = "UPDATE roominfo SET netState=\""+ netState +"\" WHERE roomID=\"" + roomID + "\";";
                //MessageBox.Show(sql);
                mycom.CommandText = sql;

                //MySqlTransaction transacter = myconn.BeginTransaction();
                //mycom.Transaction = transacter;

                mycom.ExecuteNonQuery();//执行查询
                //transacter.Commit();//提交
                myconn.Close();

                myconn.Open();
                mycom = myconn.CreateCommand();
                sql = "UPDATE roominfo SET ip=\"" + ip + "\" WHERE roomID=\""+ roomID +"\";";
                //MessageBox.Show(sql);
                mycom.CommandText = sql;

                //MySqlTransaction transacter = myconn.BeginTransaction();
                //mycom.Transaction = transacter;

                mycom.ExecuteNonQuery();//执行查询
                //transacter.Commit();//提交
                mycom.Dispose();//释放reader使用的资源，防止database is lock异常产生
                //关闭数据库，防止数据库被锁定
                myconn.Dispose();
                myconn.Close();
            }
            catch (MySqlException se)
            {
                ShowMsg(se.ToString());
                //MessageBox.Show(se.ToString());
                //关闭数据库，防止数据库被锁定
                myconn.Dispose();
                myconn.Close();
            }
        }

        /// <summary>
        /// 发送消息至会议室端
        /// </summary>
        /// <param name="roomID"></param>
        /// <param name="msg"></param>
        private void SendDevice(int roomID, byte[] msg)
        {
            MySqlConnection myconn = new MySqlConnection("Host =localhost" +
                                                         ";Database=" + database +
                                                         ";Username=" + username +
                                                         ";Password=" + passwd + ";");
            try
            {
                //连接数据库
                myconn.Open();
                //新建SQL指令
                MySqlCommand mycom = myconn.CreateCommand();
                string sql = string.Format("SELECT ip FROM roominfo WHERE roomID=" + roomID.ToString() + ";");
                mycom.CommandText = sql;
                //mycom.Parameters.AddRange(new[] { "@roomID", roomID });

                mycom.CommandType = CommandType.Text;
                //执行查询指令
                MySqlDataReader reader = mycom.ExecuteReader();

                if (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        ShowMsg("发送数据到" + roomID.ToString());
                        dict[reader.GetString(0)].Send(msg);
                        int timeout = 0;
                        while (getFlag == false)
                        {
                            timeout++;
                            Thread.Sleep(100);
                            if (timeout > 20)
                            {
                                UpdateRoom(roomID, "离线", null);
                                break;
                            }
                        }
                        getFlag = false;
                        
                    }
                }

                //释放reader的资源
                reader.Dispose();
                reader.Close();
                //关闭数据库，防止数据库被锁定
                myconn.Dispose();
                myconn.Close();
            }
            catch (MySqlException sqle)
            {
                ShowMsg(sqle.ToString());
                //MessageBox.Show(sqle.ToString());
                //关闭数据库，防止数据库被锁定
                myconn.Dispose();
                myconn.Close();
            }
            catch (SocketException se)
            {
                ShowMsg(se.ToString());
                //MessageBox.Show(se.ToString());
                //关闭数据库，防止数据库被锁定
                myconn.Dispose();
                myconn.Close();
                UpdateRoom(roomID, "离线", null);
            }
        }

        /// <summary>
        /// 保存操作记录
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="set"></param>
        /// <param name="reason"></param>
        private void saveLog(string msg, string set, string reason)
        {

        }

        /// <summary>
        /// 按时间生成验证码
        /// </summary>
        /// <returns></returns>
        private string CreateNewPasswd()
        {
            string getPaswd;

            long id = System.DateTime.Now.ToBinary();
            int num = Convert.ToInt32(id.ToString().Substring(12, 6));
            Random rd = new Random();
            num = (num + rd.Next(100000, 1000000))%1000000;
            getPaswd = num.ToString().PadLeft(6,'0');
            ShowMsg(getPaswd);

            return getPaswd;
        }

        /// <summary>
        /// 发送通知短信
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="templateNumber"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private bool SmsSend(string phoneNumber, int templateNumber, string[] param)
        {
            bool res = false;
            try
            {
                SmsSingleSender ssender = new SmsSingleSender(appid, appkey);
                var result = ssender.sendWithParam("86", phoneNumber, templateId[templateNumber], param, smsSign, "", "");  // 签名参数未提供或者为空时，会使用默认签名发送短信
                res = true;
            }
            catch (JSONException e)
            {
                ShowMsg(e.ToString());
                //MessageBox.Show(e.ToString());
            }
            catch (HTTPException e)
            {
                ShowMsg(e.ToString());
                //MessageBox.Show(e.ToString());
            }
            catch (Exception e)
            {
                ShowMsg(e.ToString());
                //MessageBox.Show(e.ToString());
            }
            return res;
        }

        /// <summary>
        /// 进入人员管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 人员管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonForm personForm = new PersonForm();
            personForm.ShowDialog();
        }

        /// <summary>
        /// 进入会议室管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 会议室管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RoomForm roomForm = new RoomForm();
            roomForm.ShowDialog();
        }

        /// <summary>
        /// 同意预约请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAgree_Click(object sender, EventArgs e)
        {
            if (tbRoomName.Text != null && tbUserName.Text != null)
            {
                string Key = CreateNewPasswd();
                //UPDATE reservationinfo SET passwd="258632" WHERE beginTime="2019-01-23 18:00" and roomName="" and endTime="";
                string roomName = tbRoomName.Text;
                string userName = tbUserName.Text;
                string beginTime = tbBeginTime.Text;
                string endTime = tbEndTime.Text;
                MySqlConnection myconn = new MySqlConnection("Host =localhost" +
                                                         ";Database=" + database +
                                                         ";Username=" + username +
                                                         ";Password=" + passwd + ";");
                try
                {
                    //连接数据库
                    myconn.Open();
                    //新建SQL指令
                    MySqlCommand mycom = myconn.CreateCommand();
                    string sql = string.Format("");
                    sql = "UPDATE reservationinfo SET roomState=@roomState WHERE roomName=@roomName and beginTime=@beginTime and endTime=@endTime;";
                    //MessageBox.Show(sql);
                    mycom.CommandText = sql;
                    mycom.Parameters.AddRange(new[] {
                                              new MySqlParameter("@roomState","预约"),
                                              new MySqlParameter("@roomName",roomName),
                                              new MySqlParameter("@beginTime",beginTime),
                                              new MySqlParameter("@endTime",endTime)
                                              });

                    //MySqlTransaction transacter = myconn.BeginTransaction();
                    //mycom.Transaction = transacter;

                    mycom.ExecuteNonQuery();//执行查询
                    //transacter.Commit();//提交
                    myconn.Close();

                    myconn.Open();
                    mycom = myconn.CreateCommand();
                    sql = "UPDATE reservationinfo SET msgState=@msgState WHERE roomName=@roomName and beginTime=@beginTime and endTime=@endTime;";
                    //MessageBox.Show(sql);
                    mycom.CommandText = sql;
                    mycom.Parameters.AddRange(new[] {
                                              new MySqlParameter("@msgState","已处理"),
                                              new MySqlParameter("@roomName",roomName),
                                              new MySqlParameter("@beginTime",beginTime),
                                              new MySqlParameter("@endTime",endTime)
                                              });

                    //MySqlTransaction transacter = myconn.BeginTransaction();
                    //mycom.Transaction = transacter;

                    mycom.ExecuteNonQuery();//执行查询
                    //transacter.Commit();//提交
                    myconn.Close();

                    myconn.Open();
                    mycom = myconn.CreateCommand();
                    sql = "UPDATE reservationinfo SET passwd=@passwd WHERE roomName=@roomName and beginTime=@beginTime and endTime=@endTime;";
                    //MessageBox.Show(sql);
                    mycom.CommandText = sql;
                    mycom.Parameters.AddRange(new[] {
                                              new MySqlParameter("@passwd",Key),
                                              new MySqlParameter("@roomName",roomName),
                                              new MySqlParameter("@beginTime",beginTime),
                                              new MySqlParameter("@endTime",endTime)
                                              });

                    //MySqlTransaction transacter = myconn.BeginTransaction();
                    //mycom.Transaction = transacter;

                    mycom.ExecuteNonQuery();//执行查询
                    myconn.Close();

                    myconn.Open();
                    mycom = myconn.CreateCommand();

                    sql = "SELECT authority FROM userinfo WHERE userName=\"" + tbUserName.Text + "\";";
                    mycom.CommandText = sql;

                    //执行查询指令
                    MySqlDataReader reader = mycom.ExecuteReader();

                    if (reader.Read())
                    {
                        string timeBegin = tbBeginTime.Text.Substring(6, 2) + "日" +
                                           tbBeginTime.Text.Substring(8, 2) + "时";
                        string timeEnd = tbEndTime.Text.Substring(6, 2) + "日" +
                                         tbEndTime.Text.Substring(8, 2) + "时";
                        if (Convert.ToUInt16(tbBeginTime.Text.Substring(10, 2)) != 0)
                        {
                            timeBegin = timeBegin + tbBeginTime.Text.Substring(10, 2) + "分";
                        }
                        if (Convert.ToUInt16(tbEndTime.Text.Substring(10, 2)) != 0)
                        {
                            timeEnd = timeEnd + tbEndTime.Text.Substring(10, 2) + "分";
                        }
                        string[] Parmer = new string[4];
                        Parmer[0] = string.Format(tbRoomName.Text);
                        Parmer[1] = string.Format(timeBegin);
                        Parmer[2] = string.Format(timeEnd);
                        Parmer[3] = string.Format(Key);
                        //发送短信
                        SmsSend(reader.GetString(0), 1, Parmer);
                    }

                    //释放reader的资源
                    reader.Dispose();
                    reader.Close();
                    //transacter.Commit();//提交
                    mycom.Dispose();//释放reader使用的资源，防止database is lock异常产生
                    //transacter.Dispose();//释放reader使用的资源，防止database is lock异常产生

                    //关闭数据库，防止数据库被锁定
                    myconn.Dispose();
                    myconn.Close();
                }
                catch (MySqlException se)
                {
                    ShowMsg(se.ToString());
                    //MessageBox.Show(se.ToString());
                    //关闭数据库，防止数据库被锁定
                    myconn.Dispose();
                    myconn.Close();
                }
                saveLog("12345", "同意", "");
                InitAllBox();
                ReLoadDataGridView();
            }
        }

        /// <summary>
        /// 拒绝预约请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCancel_Click(object sender, EventArgs e)
        {
            if (tbRoomName.Text != null && tbUserName.Text != null)
            {
                if (rtMessage.Text != "")
                {
                    string roomName = tbRoomName.Text;
                    string userName = tbUserName.Text;
                    string beginTime = tbBeginTime.Text;
                    string endTime = tbEndTime.Text;
                    MySqlConnection myconn = new MySqlConnection("Host =localhost" +
                                                         ";Database=" + database +
                                                         ";Username=" + username +
                                                         ";Password=" + passwd + ";");
                    try
                    {
                        //连接数据库
                        myconn.Open();
                        //新建SQL指令
                        MySqlCommand mycom = myconn.CreateCommand();
                        string sql = string.Format("");
                        sql = "UPDATE reservationinfo SET roomState=@roomState WHERE roomName=@roomName and beginTime=@beginTime and endTime=@endTime;";
                        //MessageBox.Show(sql);
                        mycom.CommandText = sql;
                        mycom.Parameters.AddRange(new[] {
                                              new MySqlParameter("@roomState","拒绝"),
                                              new MySqlParameter("roomName",roomName),
                                              new MySqlParameter("@beginTime",beginTime),
                                              new MySqlParameter("@endTime",endTime)
                                              });

                        //MySqlTransaction transacter = myconn.BeginTransaction();
                        //mycom.Transaction = transacter;

                        mycom.ExecuteNonQuery();//执行查询
                                                //transacter.Commit();//提交
                        myconn.Close();

                        myconn.Open();
                        mycom = myconn.CreateCommand();
                        sql = "UPDATE reservationinfo SET msgState=@msgState WHERE roomName=@roomName and beginTime=@beginTime and endTime=@endTime;";
                        //MessageBox.Show(sql);
                        mycom.CommandText = sql;
                        mycom.Parameters.AddRange(new[] {
                                              new MySqlParameter("@msgState","已处理"),
                                              new MySqlParameter("roomName",roomName),
                                              new MySqlParameter("@beginTime",beginTime),
                                              new MySqlParameter("@endTime",endTime)
                                              });

                        //MySqlTransaction transacter = myconn.BeginTransaction();
                        //mycom.Transaction = transacter;

                        mycom.ExecuteNonQuery();//执行查询
                        myconn.Close();

                        myconn.Open();
                        mycom = myconn.CreateCommand();

                        sql = "SELECT authority FROM userinfo WHERE userName=\"" + tbUserName.Text + "\";";
                        mycom.CommandText = sql;

                        //执行查询指令
                        MySqlDataReader reader = mycom.ExecuteReader();

                        if (reader.Read())
                        {
                            string[] Parmer = new string[1];
                            Parmer[0] = string.Format(roomName);
                            //发送短信
                            SmsSend(reader.GetString(0), 2, Parmer);
                        }
                        //释放reader的资源
                        reader.Dispose();
                        reader.Close();
                        //transacter.Commit();//提交
                        mycom.Dispose();//释放reader使用的资源，防止database is lock异常产生
                        //transacter.Dispose();//释放reader使用的资源，防止database is lock异常产生

                        //关闭数据库，防止数据库被锁定
                        myconn.Dispose();
                        myconn.Close();
                    }
                    catch (MySqlException se)
                    {
                        ShowMsg(se.ToString());
                        //MessageBox.Show(se.ToString());
                        //关闭数据库，防止数据库被锁定
                        myconn.Dispose();
                        myconn.Close();
                    }
                    InitAllBox();
                    ReLoadDataGridView();
                }
                else
                {
                    MessageBox.Show("请填写拒绝理由");
                }
            }
        }

        /// <summary>
        /// 申请信息数据表格双击触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReservation_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            bool auto_check = false;    //自动处理标志
            MySqlConnection myconn = new MySqlConnection("Host =localhost" +
                                                         ";Database=" + database +
                                                         ";Username=" + username +
                                                         ";Password=" + passwd + ";");
            int index = dgvReservation.CurrentRow.Index;     //获取选择行号
            if (dgvReservation.Rows[index].Cells[1].Value.ToString() == "未处理")
            {
                tbRoomName.Text = dgvReservation.Rows[index].Cells[0].Value.ToString();
                tbUserName.Text = dgvReservation.Rows[index].Cells[2].Value.ToString();
                tbBeginTime.Text = dgvReservation.Rows[index].Cells[3].Value.ToString();
                tbEndTime.Text = dgvReservation.Rows[index].Cells[4].Value.ToString();
                rtReason.Text = dgvReservation.Rows[index].Cells[5].Value.ToString();
                ShowMsg("读取信息成功");
                try
                {
                    //连接数据库
                    myconn.Open();
                    //新建SQL指令
                    MySqlCommand mycom = myconn.CreateCommand();
                    string sql = string.Format("SELECT * FROM roominfo WHERE roomName=\"" + tbRoomName.Text + "\";");
                    mycom.CommandText = sql;

                    mycom.CommandType = CommandType.Text;
                    //执行查询指令
                    MySqlDataReader reader = mycom.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader.GetString(2) == "不可预约")
                        {
                            rtMessage.Text = "当前会议室不可预约";
                            auto_check = true;
                        }
                    }

                    //释放reader的资源
                    reader.Dispose();
                    reader.Close();
                    //关闭数据库，防止数据库被锁定
                    myconn.Dispose();
                    myconn.Close();
                    if (auto_check == true)
                    {
                        Thread.Sleep(1000);
                        btCancel_Click(sender, e);
                    }
                }
                catch (MySqlException sqle)
                {
                    ShowMsg(sqle.ToString());
                    //关闭数据库，防止数据库被锁定
                    myconn.Dispose();
                    myconn.Close();
                }
            }
            else if (dgvReservation.Rows[index].Cells[1].Value.ToString() == "已处理")
            {
                ShowMsg("此预约信息已处理");
            }
        }

        /// <summary>
        /// 清空所有输入框内容
        /// </summary>
        private void InitAllBox()
        {
            tbRoomName.Text = null;
            tbUserName.Text = null;
            tbBeginTime.Text = null;
            tbEndTime.Text = null;
            rtReason.Text = null;
            rtMessage.Text = null;
        }

        /// <summary>
        /// 刷新表格信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 刷新表格信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReLoadDataGridView();
        }

        /// <summary>
        /// 清除日志显示窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 清除显示日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtLogo.Text = "";   //清空内容
            rtLogo.Select(rtLogo.Text.Length, 0);//将光标设置到最末尾
            rtLogo.ScrollToCaret();  //将滚动条设置到光标处
        }

        /// <summary>
        /// 刷新会议室状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 刷新状态ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rows = dgvRoom.SelectedRows.Count;      //获取选中总行数

            for (int i = 0; i < rows; i++)
            {
                string deviceID = dgvRoom.SelectedRows[i].Cells[0].Value.ToString();
                string deviceName = dgvRoom.SelectedRows[i].Cells[1].Value.ToString();
                string ip = dgvRoom.SelectedRows[i].Cells[4].Value.ToString();
                if (dgvRoom.Rows[i].Cells[3].Value.ToString() == "离线")
                {
                    ShowMsg("会议室" + deviceName + "已离线");
                }
                else if (dgvRoom.Rows[i].Cells[3].Value.ToString() != "离线")
                {
                    if (dict.ContainsKey(ip))
                    {
                        byte[] arrMsg = System.Text.Encoding.UTF8.GetBytes("@S1" + int.Parse(deviceID).ToString().PadLeft(4, '0') + "\r\n");
                        reciveGet = false;
                        dict[dgvRoom.Rows[i].Cells[4].Value.ToString()].Send(arrMsg);
                        int timeout = 0;
                        while (reciveGet == false && timeout < 5)
                        {
                            timeout++;
                            Thread.Sleep(1000);
                        }
                        if (timeout > 5 && reciveGet == false)
                        {
                            UpdateRoom(int.Parse(deviceID), "离线", null);
                            ShowMsg("会议室" + deviceName + "已离线");
                        }
                        else if (reciveGet == true)
                        {
                            reciveGet = false;
                        }
                    }
                    else
                    {
                        UpdateRoom(int.Parse(deviceID), "离线", null);
                    }
                }
            }
        }

        /// <summary>
        /// 表格右击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRoom_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dgvRoom.Rows[e.RowIndex].Selected == false)
                    {
                        dgvRoom.ClearSelection();
                        dgvRoom.Rows[e.RowIndex].Selected = true;
                    }
                    if (dgvRoom.SelectedRows.Count == 1)
                    {
                        dgvRoom.CurrentCell = dgvRoom.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    //弹出操作菜单
                    cmsdgvRoom.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        
    }
}
