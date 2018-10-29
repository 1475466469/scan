using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace scan
{
    public partial class Form3 : Form
    {
        private BLL.loginBLL n = new BLL.loginBLL();
        private t_ADMM_UsrMst admin;
        private BLL.Log log = new BLL.Log();
        public Form3()
        {
            InitializeComponent();
        }
        public Form3(t_ADMM_UsrMst t)
        {
            this.admin = t;
            InitializeComponent();
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                if (textEdit1.Text != "")
                {
                    if (textEdit3.Text.Trim() == textEdit2.Text.Trim())
                    {
                        n.UpdatePwd(admin.fUsrID, textEdit1.Text.Trim(), textEdit3.Text.Trim());

                        MessageBox.Show("密码修改成功");
                        log.Wirtefile(admin.fUsrID + "密码修改成功");
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("两次密码输入不一致");
                    }
                }
                else
                {
                    MessageBox.Show("请输入原密码");
                }

            }catch(Exception ex)
            {
                log.Wirtefile(admin.fUsrID + "密码修改出现错误："+ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
