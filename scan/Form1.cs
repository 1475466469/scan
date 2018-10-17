using BLL;
using DevExpress.XtraSplashScreen;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            
        }
        private loginBLL n = new loginBLL();
        private void checkButton1_CheckedChanged(object sender, EventArgs e)
        {
           
           

                login();
                

            
            

            

        }

        

       
        private void Pwd_KeyUp(object sender, KeyEventArgs e)
        {
            using (DevExpress.Utils.WaitDialogForm wdf = new DevExpress.Utils.WaitDialogForm("请稍等...", "登陆中...", new Size(50, 40)))
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
                            DevExpress.XtraEditors.XtraMessageBox.Show("请输入密码！");
                        }
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("请输入用户名！");
                    }
                }
            }
        }

        private void login()
        {
           
            if (Usrid.Text.Trim() != "" & Pwd.Text.Trim() != "")
            {

                

                try
                {
                    t_ADMM_UsrMst admin = n.LoginHandle(Usrid.Text.Trim(), Pwd.Text.Trim());
                    if (admin != null)
                    {

                        this.Hide();
                        new Form2(admin).ShowDialog();

                    }
                    else
                    {

                        DevExpress.XtraEditors.XtraMessageBox.Show("用户名或密码错误");
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                };
               
            }
            else
            {
               
                DevExpress.XtraEditors.XtraMessageBox.Show("用户名或密码不能为空");

            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
