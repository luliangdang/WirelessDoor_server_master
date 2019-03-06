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
        string passwd = "*******";

        //短信发送appid
        int appid = **********;
        string appkey = "******************************";
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
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
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
        /// DataGridView双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRoom_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgvRoom.CurrentRow.Index;
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
                string sql = string.Format("SELECT * FROM roominfo WHERE roomName=\"" + dgvRoom.Rows[index].Cells[1].Value.ToString() + "\";");
                mycom.CommandText = sql;

                mycom.CommandType = CommandType.Text;
                //执行查询指令
                MySqlDataReader reader = mycom.ExecuteReader();

                if (reader.Read())
                {
                    tbRoomID.Text = reader.GetString(0);
                    tbRoomName.Text = reader.GetString(1);
                    cbRoomState.Text = reader.GetString(2);
                }

                //释放reader的资源
                reader.Dispose();
                reader.Close();
                //关闭数据库，防止数据库被锁定
                myconn.Dispose();
                myconn.Close();
                ShowMsg("数据读取成功");
            }
            catch (MySqlException sqle)
            {
                ShowMsg(sqle.ToString());
                //关闭数据库，防止数据库被锁定
                myconn.Dispose();
                myconn.Close();
            }
        }

        /// <summary>
        /// 清除文本框内容
        /// </summary>
        private void InitAllBox()
        {
            tbRoomID.Text = null;
            tbRoomName.Text = null;
            cbRoomState.Text = null;
        }

        /// <summary>
        /// 设置会议室状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSet_Click(object sender, EventArgs e)
        {
            bool issearch = false;
            MySqlConnection myconn = new MySqlConnection("Host =localhost" +
                                                         ";Database=" + database +
                                                         ";Username=" + username +
                                                         ";Password=" + passwd + ";");
            for (int i = 0; i < dgvRoom.RowCount; i++)
            {
                if (tbRoomID.Text == dgvRoom.Rows[i].Cells[0].Value.ToString())
                {
                    issearch = true;
                    break;
                }
            }

            if (tbRoomID.Text != "" && tbRoomName.Text != "")
            {
                if (issearch == true)
                {
                    try
                    {
                        //连接数据库
                        myconn.Open();
                        //新建SQL指令
                        MySqlCommand mycom = myconn.CreateCommand();
                        string sql = string.Format("");
                        sql = "UPDATE roominfo SET roomName=@roomName WHERE roomID=@roomID;";
                        //MessageBox.Show(sql);
                        mycom.CommandText = sql;
                        mycom.Parameters.AddRange(new[] {
                                              new MySqlParameter("@roomName",tbRoomName.Text),
                                              new MySqlParameter("@roomID",tbRoomID.Text)
                                              });

                        //MySqlTransaction transacter = myconn.BeginTransaction();
                        //mycom.Transaction = transacter;

                        mycom.ExecuteNonQuery();//执行查询
                                                //transacter.Commit();//提交
                        myconn.Close();

                        //连接数据库
                        myconn.Open();
                        //新建SQL指令
                        mycom = myconn.CreateCommand();
                        sql = "UPDATE roominfo SET roomState=@roomState WHERE roomID=@roomID;";
                        //MessageBox.Show(sql);
                        mycom.CommandText = sql;
                        mycom.Parameters.AddRange(new[] {
                                              new MySqlParameter("@roomState",cbRoomState.Text),
                                              new MySqlParameter("@roomID",tbRoomID.Text)
                                              });

                        //MySqlTransaction transacter = myconn.BeginTransaction();
                        //mycom.Transaction = transacter;

                        mycom.ExecuteNonQuery();//执行查询
                        //transacter.Commit();//提交
                        mycom.Dispose();//释放reader使用的资源，防止database is lock异常产生
                        //transacter.Dispose();//释放reader使用的资源，防止database is lock异常产生

                        //关闭数据库，防止数据库被锁定
                        myconn.Dispose();
                        myconn.Close();
                        ShowMsg("会议室" + tbRoomName.Text + "信息修改成功");
                        InitAllBox();
                        ReloadDateGridiew();
                    }
                    catch (MySqlException sqle)
                    {
                        ShowMsg(sqle.ToString());
                        //关闭数据库，防止数据库被锁定
                        myconn.Dispose();
                        myconn.Close();
                    }
                }
                else if (issearch == false)
                {
                    myconn.Open();
                    MySqlTransaction transaction = myconn.BeginTransaction();//事务必须在try外面赋值不然catch里的
                    try
                    {
                        MySqlCommand mycom = myconn.CreateCommand();
                        string sql = string.Format("INSERT INTO roominfo values(@roomID,@roomName,@roomState,@netState,@ip);");
                        mycom.CommandText = sql;
                        mycom.Parameters.AddRange(new[] {
                                                  new MySqlParameter("@roomID",tbRoomID.Text),
                                                  new MySqlParameter("@roomName",tbRoomName.Text),
                                                  new MySqlParameter("@roomState","不可预约"),
                                                  new MySqlParameter("@netState","离线"),
                                                  new MySqlParameter("@ip",null)
                                                  });

                        mycom.CommandType = CommandType.Text;

                        mycom.ExecuteNonQuery();
                        transaction.Commit();

                        mycom.Dispose();
                        transaction.Dispose();
                        //关闭数据库，防止数据库被锁定
                        myconn.Dispose();
                        myconn.Close();
                        InitAllBox();
                        ReloadDateGridiew();
                        ShowMsg("新增会议室" + tbRoomID.Text + "成功");
                    }
                    catch (MySqlException sqle)
                    {
                        ShowMsg(sqle.ToString());
                        transaction.Dispose();
                        //关闭数据库，防止数据库被锁定
                        myconn.Dispose();
                        myconn.Close();
                    }
                }
            }
            else ShowMsg("请正确填写信息");
        }

        /// <summary>
        /// 新增会议室信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MySqlConnection myconn = new MySqlConnection("Host =localhost" +
                                                         ";Database=" + database +
                                                         ";Username=" + username +
                                                         ";Password=" + passwd + ";");
            try
            {
                int index = 0;
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
                    index = reader.GetUInt16(0);
                }
                //释放reader的资源
                reader.Dispose();
                reader.Close();
                //关闭数据库，防止数据库被锁定
                myconn.Close();
                index++;
                tbRoomID.Text = index.ToString();
                tbRoomName.Text = null;
                cbRoomState.Text = null;
            }
            catch (MySqlException sqle)
            {
                ShowMsg(sqle.ToString());
            }
        }

        /// <summary>
        /// 删除会议室信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMsg("权限不足，不可删除");
        }

        /// <summary>
        /// 更新会议室信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReloadDateGridiew();
        }

        /// <summary>
        /// 查询会议室信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row = dgvRoom.Rows.Count;//得到总行数 
            int cell = dgvRoom.Rows[1].Cells.Count;//得到总列数
            int rowIndex = dgvRoom.CurrentRow.Index;//获取选择行号
            string searchString = "";
            string cellString = "";
            //向后查找
            for (int i = rowIndex + 1; i < row; i++)
            {
                for (int j = 0; j < cell - 1; j++)
                {
                    searchString = tbSearch.Text.Trim();
                    cellString = dgvRoom.Rows[i].Cells[j].Value.ToString().Trim();
                    //精确查找定位
                    if (cellString.IndexOf(searchString) != -1)
                    {
                        //对比TexBox中的值是否与dataGridView中的值相同（上面这句） 
                        dgvRoom.CurrentCell = dgvRoom[j, i];//定位到相同的单元格 
                        dgvRoom.Rows[i].Selected = true;//定位到行 
                        return;//返回
                    }
                }
            }
            //向前查找
            for (int i = 0; i < rowIndex + 1; i++)//得到总行数并在之内循环 
            {
                for (int j = 0; j < cell - 1; j++)//得到总列数并在之内循环 
                {
                    searchString = tbSearch.Text.Trim();
                    cellString = dgvRoom.Rows[i].Cells[j].Value.ToString().Trim();
                    //精确查找定位
                    if (cellString.IndexOf(searchString) != -1)
                    {
                        //对比TexBox中的值是否与dataGridView中的值相同（上面这句） 
                        dgvRoom.CurrentCell = dgvRoom[j, i];//定位到相同的单元格 
                        dgvRoom.Rows[i].Selected = true;//定位到行 
                        return;//返回
                    }
                }
            }
            MessageBox.Show("找不到" + tbSearch.Text, "会议室管理");
        }

        /// <summary>
        /// 表格的右键菜单相应函数
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
