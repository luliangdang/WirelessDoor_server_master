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
    public partial class PersonForm : Form
    {
        //数据库信息
        string database = "room";
        string username = "root";
        //string passwd = "Dll960220";
        string passwd = "LL960220";

        public PersonForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PersonForm_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            ReloadDataGridView();
        }

        /// <summary>
        /// 刷新数据表格
        /// </summary>
        private void ReloadDataGridView()
        {
            MySqlConnection myconn = new MySqlConnection("Host =localhost"
                                                        + ";Database=" + database
                                                        + ";Username=" + username
                                                        + ";Password=" + passwd + ";");
            dgvPerson.Rows.Clear();
            try
            {
                //打开数据库
                myconn.Open();
                //新建SQL指令
                MySqlCommand mycom = myconn.CreateCommand();

                string sql = string.Format("SELECT * FROM userInfo;");

                mycom.CommandText = sql;
                mycom.CommandType = CommandType.Text;
                //执行查询指令
                MySqlDataReader reader = mycom.ExecuteReader();
                while (reader.Read())
                {
                    int index = dgvPerson.Rows.Add();

                    dgvPerson.Rows[index].Cells[0].Value = reader[1].ToString();    //用户名
                    dgvPerson.Rows[index].Cells[1].Value = reader[2].ToString();    //电话
                    dgvPerson.Rows[index].Cells[2].Value = reader[6].ToString();    //QQ
                    dgvPerson.Rows[index].Cells[3].Value = reader[7].ToString();    //邮箱
                    dgvPerson.Rows[index].Cells[4].Value = reader[9].ToString();    //注册时间
                }
                //释放reader的资源
                reader.Dispose();
                reader.Close();
                //关闭数据库，防止数据库被锁定
                myconn.Dispose();
                myconn.Close();
            }
            catch (MySqlException ex)
            {
                //显示错误信息
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
        /// DataGridView双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvPerson_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgvPerson.CurrentCell.RowIndex;
            tbUserName.Text = dgvPerson.Rows[index].Cells[0].Value.ToString();
            tbTel.Text = dgvPerson.Rows[index].Cells[1].Value.ToString();
            tbQQ.Text = dgvPerson.Rows[index].Cells[2].Value.ToString();
            tbMail.Text = dgvPerson.Rows[index].Cells[3].Value.ToString();
            ShowMsg("数据读取成功！");
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSet_Click(object sender, EventArgs e)
        {
            if (tbUserName.Text != "")
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
                    sql = "UPDATE userinfo SET qq=\"" + tbQQ.Text + "\" WHERE userName=\"" + tbUserName.Text + "\";";
                    //MessageBox.Show(sql);
                    mycom.CommandText = sql;

                    mycom.ExecuteNonQuery();//执行查询
                    //transacter.Commit();//提交
                    myconn.Close();

                    myconn.Open();
                    mycom = myconn.CreateCommand();
                    sql = "UPDATE userinfo SET email=\"" + tbMail.Text + "\" WHERE userName=\"" + tbUserName.Text + "\";";
                    //MessageBox.Show(sql);
                    mycom.CommandText = sql;

                    mycom.ExecuteNonQuery();//执行查询
                    //transacter.Commit();//提交
                    mycom.Dispose();//释放reader使用的资源，防止database is lock异常产生
                    //关闭数据库，防止数据库被锁定
                    myconn.Dispose();
                    myconn.Close();
                    ReloadDataGridView();
                    ShowMsg("数据修改成功");
                    InitTextBox();
                }
                catch (MySqlException se)
                {
                    ShowMsg(se.ToString());
                    //MessageBox.Show(se.ToString());
                    //关闭数据库，防止数据库被锁定
                    myconn.Dispose();
                    myconn.Close();
                    ShowMsg("数据修改失败");
                    InitTextBox();
                }
            }
        }

        /// <summary>
        /// 清除TextBox内容
        /// </summary>
        private void InitTextBox()
        {
            tbUserName.Text = null;
            tbTel.Text = null;
            tbQQ.Text = null;
            tbMail.Text = null;
        }


    }
}
