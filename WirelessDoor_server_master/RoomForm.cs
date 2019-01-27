using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using qcloudsms_csharp;
using qcloudsms_csharp.json;
using qcloudsms_csharp.httpclient;

namespace WirelessDoor_server_master
{
    public partial class RoomForm : Form
    {
        //数据库信息
        string database = "room";
        string username = "root";
        string passwd = "Dll960220";
        //string passwd = "LL960220";

        //短信发送appid
        int appid = 1400178112;
        string appkey = "6402f72c5c2d15fac7f124d78d6f4759";
        int[] templateId = { 265637, 270334, 265636, 266224 };//注册，预约成功，失败，提醒
        string smsSign = "会易云";

        public RoomForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoomForm_Load(object sender, EventArgs e)
        {
            ReloadDateGridiew();
        }

        /// <summary>
        /// 刷新数据表格
        /// </summary>
        private void ReloadDateGridiew()
        {
            MySqlConnection myconn = new MySqlConnection("Host =localhost"
                                                        + ";Database=" + database
                                                        + ";Username=" + username
                                                        + ";Password=" + passwd + ";");
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

                    dgvRoom.Rows[index].Cells[0].Value = reader[1].ToString();
                    dgvRoom.Rows[index].Cells[1].Value = reader[2].ToString();
                    dgvRoom.Rows[index].Cells[2].Value = reader[3].ToString();
                    dgvRoom.Rows[index].Cells[3].Value = reader[4].ToString();
                }
                //释放reader的资源
                reader.Dispose();
                reader.Close();
                //关闭数据库，防止数据库被锁定
                myconn.Dispose();
                myconn.Close();
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                //关闭数据库，防止数据库被锁定
                myconn.Dispose();
                myconn.Close();
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
                MessageBox.Show(e.ToString());
            }
            catch (HTTPException e)
            {
                MessageBox.Show(e.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return res;
        }

        /// <summary>
        /// DataGridView双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRoom_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// 新增会议室信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 删除会议室信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 查询会议室信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
