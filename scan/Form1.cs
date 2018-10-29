using BLL;
using DevExpress.XtraSplashScreen;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            OpenPW();


        }
        private loginBLL n = new loginBLL();
        private Log log = new Log();
        private void checkButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                login();
               
            }
            catch(Exception ex)
            {
                loginLoad.Hide();
                MessageBox.Show(ex.Message);
                log.Wirtefile(Usrid.Text + "登陆引发异常："+ex.StackTrace);
            }
            
        }
        private void Pwd_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.GetHashCode() == 13)
                {
                    if (Usrid.Text.Trim() != "")
                    {
                        if (Pwd.Text.Trim() != "")
                        {
                            login();
                        }
                        else
                        {
                            MessageBox.Show("请输入密码！");
                        }
                    }
                    else
                    {
                        MessageBox.Show("请输入用户名！");
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Wirtefile(Usrid.Text + "登陆引发异常：" + ex.Message);
            }
            
        }

        private void login()
        {
            loginLoad.Show();
            if (Usrid.Text.Trim() != "" & Pwd.Text.Trim() != "")
            {

               t_ADMM_UsrMst admin = n.LoginHandle(Usrid.Text.Trim(), Pwd.Text.Trim());
                    if (admin != null)
                    {

                        this.Hide();
                    log.Wirtefile(Usrid.Text + "登陆成功！");
                    SavePW();
                    new Form2(admin).ShowDialog();

                    }
                    else
                    {
                        MessageBox.Show("用户名或密码错误");
                    log.Wirtefile(Usrid.Text + "登陆用户名或密码错误");
                }
                    loginLoad.Hide();
            }
            else
            {
                loginLoad.Hide();
                MessageBox.Show("用户名或密码不能为空");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Usrid_EditValueChanged(object sender, EventArgs e)
        {

        }

        protected override void OnClosing(CancelEventArgs e)
        {

            
                Application.Exit();
           

        }


        Dictionary<string, UserInfo> users = new Dictionary<string, UserInfo>();
        public void SavePW()//保存用户和密码
        {
            try
            {

                UserInfo user = new UserInfo();
                // 登录时 如果没有Data.bin文件就创建、有就打开
                FileStream fs = new FileStream("usr.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                BinaryFormatter bf = new BinaryFormatter();
                // 保存在实体类属性中
                user.LoginID = Usrid.Text.Trim();
                //保存密码选中状态
                if (checkBoxXpw.Checked)
                    user.Pwd = Pwd.Text.Trim();
                else
                    user.Pwd = "";
                //选在集合中是否存在用户名 
                if (users.ContainsKey(user.LoginID))
                {
                    //如果有清掉
                    users.Remove(user.LoginID);
                }
                //添加用户信息到集合
                users.Add(user.LoginID, user);
                //写入文件
                bf.Serialize(fs, users);
                //关闭
                fs.Close();
            }catch(Exception ex)
            {
              
                MessageBox.Show(ex.Message);
            }
        }


        public void OpenPW()//打开用户和密码
        {
            try
            {


                FileStream fs = new FileStream("usr.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                //fs.Seek(0, SeekOrigin.Begin);

                if (fs.Length > 0)
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    fs.Position = 0;
                    //读出存在Data.bin 里的用户信息
                    users = bf.Deserialize(fs) as Dictionary<string, UserInfo>;
                    //循环添加到Combox1
                    foreach (UserInfo user in users.Values)
                    {
                        Usrid.Items.Add(user.LoginID);
                    }

                    //combox1 用户名默认选中第一个
                    if (Usrid.Items.Count > 0)
                    {
                        Usrid.SelectedIndex = Usrid.Items.Count - 1;
                    }

                }
                fs.Close();
         }catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
                throw ex;
            }
}

        private void Usrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                FileStream fs = new FileStream("usr.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                if (fs.Length > 0)
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    users = bf.Deserialize(fs) as Dictionary<string, UserInfo>;
                    for (int i = 0; i < users.Count; i++)
                    {
                        if (Usrid.Text != "")
                        {
                            if (users.ContainsKey(Usrid.Text) && users[Usrid.Text].Pwd != "")
                            {
                                Pwd.Text = users[Usrid.Text].Pwd;
                                checkBoxXpw.Checked = true;
                            }
                            else
                            {
                                Pwd.Text = "";
                                checkBoxXpw.Checked = false;
                            }
                        }
                    }
                }
                fs.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

      
    }

    [Serializable]
    public class UserInfo
    {
        public string LoginID
        {
            get;
            set;
        }

        public string Pwd
        {
            get;
            set;
        }

    }






















}

