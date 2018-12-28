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

namespace WirelessDoor_server_master
{
    public partial class MainForm : Form
    {
        //服务器地址
        IPAddress HOST = IPAddress.Parse("113.250.104.49");
        //服务器端口号
        private const int port = 8086;

        Thread threadWatch = null; // 负责监听客户端连接请求的 线程；  
        Socket socketWatch = null; // 负责监听客户端连接的socket模块

        /* socket连接对象字典 */
        Dictionary<string, Socket> dict = new Dictionary<string, Socket>();
        /* 客户端连接对象进程字典 */
        Dictionary<string, Thread> dictThread = new Dictionary<string, Thread>();

        //数据库信息
        string database = "room";
        String username = "root";
        string passwd = "Dll960220";

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
                //打开数据库，若文件不存在会自动创建
                myconn.Open();
                //新建SQL指令
                MySqlCommand mycom = myconn.CreateCommand();
                //构造SQL指令
                string sql = string.Format("CREATE TABLE IF NOT EXISTS user(" +
                                           "userID INTEGER PRIMARY KEY," +        //用户序号
                                           "userName VARCHAR(6)," +               //用户名
                                           "authority TINYINT," +                 //登录名
                                           "passwd VARCHAR(20)," +                //密码
                                           "sex VARCHAR(2)," +                    //性别
                                           "tel VARCHAR(20)," +                   //电话
                                           "qq VARCHAR(20)," +                    //qq
                                           "email VARCHAR(20)," +                 //邮箱
                                           "birthday VARCHAR(20)," +              //生日
                                           "recodeDate VARCHAR(20));");              //注册时间
                mycom.CommandText = sql;

                mycom.CommandType = CommandType.Text;

                mycom.ExecuteNonQuery();
                //关闭数据库
                myconn.Close();

                myconn.Open();
                //sqlite指令语句
                sql = string.Format("CREATE TABLE IF NOT EXISTS room(" +
                                    "roomID INTEGER PRIMARY KEY," +
                                    "roomName TEXT," +
                                    "roomState TINYINT," +
                                    "userName VARCHAR(6)," +
                                    "beginTime VARCHAR(20)," +
                                    "endTime VARCHAR(20)," +
                                    "reason TEXT);");

                mycom.CommandText = sql;

                mycom.CommandType = CommandType.Text;

                //如果表不存在，创建会议室信息表 
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
                        //decode_data(recive_data, sokClient.RemoteEndPoint.ToString()); //解析接收数据
                        //reloadDataGridView();                   //重载表格数据
                    }
                    else  //防止断开时来的“”数据 将其彻底断开
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
            if (rtMessage.Text != "") { rtMessage.Text += "\r\n"; }
            rtMessage.Text += DateTime.Now.ToString("HH:mm:ss:") + str;
            rtMessage.Select(rtMessage.Text.Length, 0);//将光标设置到最末尾
            rtMessage.ScrollToCaret();  //将滚动条设置到光标处
        }

        private void 开启服务器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (开启服务器ToolStripMenuItem.Text == "开启服务器")
            {
                开启服务器ToolStripMenuItem.Text = "关闭服务器";
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
                byte[] arrMsg = System.Text.Encoding.UTF8.GetBytes("000004"); // 将要发送的字符串转换成Utf-8字节数组； 
                byte[] arrSendMsg = new byte[arrMsg.Length + 1]; // 上次写的时候把这一段给弄掉了，实在是抱歉哈~ 用来标识发送是数据而不是文件，如果没有这一段的客户端就接收不到消息了~~~ 
                foreach (Socket s in dict.Values)
                {
                    s.Send(arrMsg);
                }
                ShowMsg("关闭成功！！");
                socketWatch.Close();
            }
        }

        private void 人员管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 会议室管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
